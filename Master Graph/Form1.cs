using System;
using System.Windows.Forms;
using System.Drawing;
using Graphics2D;
using static System.Math;

namespace MasterGraph
{
    public partial class Form1 : Form
    {
        //Создадим рисуемую координатную плоскость
        //и ее базовые элементы
        PaintingCoordinatePlane _space;
        Grid _grid = new Grid();
        Centers _center = new Centers();
        Marks _marks = new Marks();

        /// <summary>
        /// Конструктор формы.
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            //Поместим рисуемую координатную плоскость на панель Plane
            _space = new PaintingCoordinatePlane(Plane);
            //Зададим начальные парметры плоскости
            _space.GridSize = (6, 231);
            _space.Center = (0, 4);
            _space.CenterChanged += _space_CenterChanged;
            _space.GridSizeChanged += _space_GridSizeChanged;
            _space.SizeChanged += _space_SizeChanged;

            //Зададим параметры базовых элементов плоскости
            _grid.Pen = new Pen(Color.Black, 1);
            _center.Pen = new Pen(Color.White, 1);
            _marks.Pen = new Pen(Color.White, 1);
            _marks.Width = Min(Plane.Width, Plane.Height) * 0.02f;

            //Поместим элеметы на плоскость
            _space.Add("Grid", _grid);
            _space.Add("Center", _center);
            _space.Add("Marks", _marks);

            //в ручную добавим обработчик события
            //вращения мыши
            MouseWheel += Form1_MouseWheel;



            _space.Add("Порабола", new Graph(Parabola, Along.X, -10, 0.1f, 10) 
            { Pen = new Pen(Color.Yellow, 2) });
                
        }


        #region Обработка команд
        /// <summary>
        /// Устанавливает размеры и координаты внутренних элементов формы при изменении размеров окна
        /// </summary>
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            Plane.Width = Width - 40;
            Plane.Height = Height - 145;

            _marks.Width = Min(Plane.Width, Plane.Height) * 0.02f;
            _space["Marks"] = _marks;
        }

        private string _sizeText = "Размер\n";
        /// <summary>
        /// Передает размеры координатной плоскости в метку.
        /// </summary>
        private void _space_SizeChanged(Axis axis)
        {
            LabelSize.Text = _sizeText + $"({(int)(axis.X / _space.GridSize.X)} ; " +
                $"{(int)(axis.Y / _space.GridSize.Y)})";
        }

        private string _gridSizeText = "Сетка\n";
        /// <summary>
        /// Передает размеры координатной сетки в метку.
        /// </summary>
        private void _space_GridSizeChanged(Axis axis)
        {
            LabelGridSize.Text = _gridSizeText + axis.ToString();
        }

        private string _centerText = "Центры\n";
        /// <summary>
        /// Передает координаты центров в метку.
        /// </summary>
        private void _space_CenterChanged(Axis axis)
        {
            LabelCenters.Text = _centerText + axis.ToString();
        }

        private string _marksText = "Отметки\n";
        /// <summary>
        /// Устанавливает отметку на рисуемой координатной плоскости
        /// и передает размеры отметок в метку.
        /// </summary>
        private void ChangeMarks()
        {
            _space["Marks"] = _marks;
            LabelMarks.Text = _marksText + _marks.Size.ToString();
        }

        /// <summary>
        /// Инкремент.
        /// </summary>
        private int _increment = 1;
        private string _incText = "Инкремент\n";
        /// <summary>
        /// Устанавливает величину инкремента при изменении параметров координатной плоскости 
        /// и передает это значение в метку.
        /// </summary>
        private int _inc
        {
            get => _increment;
            set
            {
                _increment = (value > 0) ? value : _increment;
                LabelIncrement.Text = _incText + _increment.ToString();
            }
        }


        /// <summary>
        /// Устанавливает программу в состояние изменения размеров координатной сетки по Х.
        /// </summary>
        private bool ScalingX;
        /// <summary>
        /// Устанавливает программу в состояние изменения размеров координатной сетки по Y.
        /// </summary>
        private bool ScalingY;
        /// <summary>
        /// Устанавливает программу в состояние изменения центра координатной плоскости по Х.
        /// </summary>
        private bool MovingX;
        /// <summary>
        /// Устанавливает программу в состояние изменения центра координатной плоскости по Y.
        /// </summary>
        private bool MovingY;
        /// <summary>
        /// Устанавливает программу в состояние изменения расстояния отметок координатной плоскости по Х.
        /// </summary>
        private bool MarksX;
        /// <summary>
        /// Устанавливает программу в состояние изменения расстояния отметок координатной плоскости по Y.
        /// </summary>
        private bool MarksY;

        /// <summary>
        /// В зависсимости от текущего состояния изменения, изменяет параметры координатной плоскости
        /// на указанный игкремент.
        /// </summary>
        private void Transformation(int inc)
        {
            if (ScalingX)
            {
                _space.GridSize = (_space.GridSize.X + inc, _space.GridSize.Y);
            }
            else if (ScalingY)
            {
                _space.GridSize = (_space.GridSize.X, _space.GridSize.Y + inc);
            }
            else if (MovingX)
            {
                _space.Center = (_space.Center.X + inc, _space.Center.Y);
            }
            else if (MovingY)
            {
                _space.Center = (_space.Center.X, _space.Center.Y + inc);
            }
            else if (MarksX)
            {
                _marks.Size = (_marks.Size.X + inc, _marks.Size.Y);
                ChangeMarks();
            }
            else if (MarksY)
            {
                _marks.Size = (_marks.Size.X, _marks.Size.Y + inc);
                ChangeMarks();
            }
        }

        /// <summary>
        /// В зависимости от клавиши вызывает изменение параметров координатной плоскости
        /// либо изменяет значение инкремента.
        /// </summary>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 187)
            {
                Transformation(_inc);
            }
            else if (e.KeyValue == 189)
            {
                Transformation(-_inc);
            }
            else if (e.KeyData == Keys.Up ||
                e.KeyData == (Keys.Up | Keys.Control) ||
                e.KeyData == (Keys.Up | Keys.Shift))
            {
                _inc++;
            }
            else if (e.KeyData == Keys.Down ||
                e.KeyData == (Keys.Down | Keys.Control) ||
                e.KeyData == (Keys.Down | Keys.Shift))
            {
                _inc--;
            }
        }

        /// <summary>
        /// При вращении колеса мыши изменяет параметры коодинатной сетки.
        /// </summary>
        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            Transformation((e.Delta > 0) ? _inc : -_inc);
        }

        /// <summary>
        /// Устанавливает ссостояние изменения координатной плоскости.
        /// </summary>
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '\u0018':
                    ScalingX = true;
                    break;
                case '\u001a':
                    ScalingY = true;
                    break;
                case 'X':
                    MovingX = true;
                    break;
                case 'Z':
                    MovingY = true;
                    break;
                case 'x':
                    MarksX = true;
                    break;
                case 'z':
                    MarksY = true;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Сбрасывает состояние изменения координатной плоскости.
        /// </summary>
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 88)
            {
                ScalingX = MovingX = MarksX = false;
            }
            else if (e.KeyValue == 90)
            {
                ScalingY = MovingY = MarksY = false;
            }
        }

        /// <summary>
        /// Выводит окно справки программы.
        /// </summary>
        private void LabelHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Для трансформации координатной плоскости используются следующие комбинации клавишь " +
                "в сочетании с поворотом колеса мыши или нажатием + или -:\n" +
                "Ctrl + X - Именение размера координатной сетки по X.\n" +
                "Ctrl + Z - Именение размера координатной сетки по Y.\n" +
                "Shift + X - Именение координаты оси X.\n" +
                "Shift + Z - Именение координаты оси Y.\n" +
                "X - Изменение расстояния между отметками по оси Х\n" +
                "Z - Изменение расстояния между отметками по оси Y\n" +
                "Up - Увеличение инкремента на 1\n" +
                "Down - Уменьшение инкремента на 1\n" +
                "\nИспользуйте английскую раскладку!"
                , "Справка");
        }
        #endregion


        #region Какая-нибудь лабораторная

        private float Parabola(float x) => x * x;

        #endregion
    }
}
