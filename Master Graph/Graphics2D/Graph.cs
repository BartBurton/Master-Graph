using System;
using System.Drawing;
using System.Collections.Generic;

namespace Graphics2D
{
    /// <summary>
    /// Реализует построение графика функции на возвращаемом полотне.
    /// </summary>
    public class Graph : IPainting
    {
        /// <summary>
        /// Формула.
        /// </summary>
        private Func<float, float> _formula;
        /// <summary>
        /// Определяет формулу, по которой построится график.
        /// </summary>
        public Func<float, float> Formula
        {
            get => _formula;
            set
            {
                _formula = value;
                MakeDots(Along);
            }
        }

        /// <summary>
        /// Ось.
        /// </summary>
        private Along _along;
        /// <summary>
        /// Ось, вдоль которой необходимо начертить график.
        /// </summary>
        public Along Along
        {
            get => _along;
            set
            {
                _along = value;
                MakeDots(Along);
            }
        }

        /// <summary>
        /// Начало.
        /// </summary>
        private float _start = 0;
        /// <summary>
        /// Определяет точку-начало построения графика.
        /// </summary>
        public float Start
        {
            get => _start;
            set
            {
                _start = value;
                MakeDots(Along);
            }
        }

        /// <summary>
        /// Шаг.
        /// </summary>
        private float _step = 1;
        /// <summary>
        /// Опредеояет шаг для вычисления следующей координаты графика.
        /// </summary>
        public float Step
        {
            get => _step;
            set
            {
                _step = (value > 0) ? value : _step;
                MakeDots(Along);
            }
        }

        /// <summary>
        /// Конец.
        /// </summary>
        private float _end = 0;
        /// <summary>
        /// Определяет точку-конец построения графика.
        /// </summary>
        public float End
        {
            get => _end;
            set
            {
                _end = value;
                MakeDots(Along);
            }
        }

        /// <summary>
        /// Определяет карандаш, которым будет отричовываться график.
        /// </summary>
        public Pen Pen { get; set; } = new Pen(Color.Black, 2);


        public Graph() { }
        /// <summary>
        /// Рекомендуется использовать данный конструктор класса для создания объекта класса, 
        /// так как создание экземпляра при помощи синтаксиса инициализации объектов или отдельной инициализации
        /// полей будет происходить дольше из-за повторной инициализации списка точек для указанных параметров.
        /// </summary>
        /// <param name="formula">Формула построения графика</param>
        /// <param name="along">Ось, вдоль которой строится график</param>
        /// <param name="start">Начало построения</param>
        /// <param name="step">Шаг</param>
        /// <param name="end">Конец построения</param>
        public Graph(Func<float, float> formula, Along along, float start, float step, float end)
        {
            _formula = formula;
            _along = along;
            _start = start;
            _step = step;
            _end = end;
            MakeDots(Along);
        }


        /// <summary>
        /// Список переинициализируемый при каждом изменении параметров построения графика,
        /// содержит координаты X и Y для данных параметров.
        /// </summary>
        private List<Axis> Points;
        /// <summary>
        /// Инициализирует список точек.
        /// </summary>
        private void MakeDots(Along axis)
        {
            if (Formula == null) return;

            Points = new List<Axis>();

            float dot = Start;

            switch (Along)
            {
                case Along.X:
                    {
                        while (dot <= End)
                        {
                            Points.Add((dot, Formula(dot)));
                            dot += Step;
                        }
                    }
                    break;
                case Along.Y:
                    {
                        while (dot <= End)
                        {
                            Points.Add((Formula(dot), dot));
                            dot += Step;
                        }
                    }
                    break;
                default:
                    break;
            }

            if (Points.Count == 0) Points = null;
        }



        public Bitmap Paint(CoordinatePlane space)
        {
            Bitmap bitmap = new Bitmap((int)space.Size.X, (int)space.Size.Y);
            Graphics draw = Graphics.FromImage(bitmap);

            Axis size = space.GridSize;
            Axis center = space.Center;
            float x1, y1, x2, y2;


            if (Points != null)
            {
                List<Axis>.Enumerator enumerator = Points.GetEnumerator();

                enumerator.MoveNext();
                x1 = (enumerator.Current.X + center.X) * size.X;
                y1 = (center.Y - enumerator.Current.Y) * size.Y;

                for (int i = 1; i < Points.Count; i++)
                {
                    enumerator.MoveNext();
                    x2 = (enumerator.Current.X + center.X) * size.X;
                    y2 = (center.Y - enumerator.Current.Y) * size.Y;

                    try {
                        draw.DrawLine(Pen, x1, y1, x2, y2);
                    } catch { }

                    x1 = x2;
                    y1 = y2;
                }
            }

            return bitmap;
        }
    }
}
