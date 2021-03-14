using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Graphics2D
{
    /// <summary>
    /// Исключение гинерируемое при некорректном обращении к множеству рисуемых элементов пространства.
    /// </summary>
    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException() { }
        public override string Message => "Рисуемый элемент по указанному имени не найден!";
    }
    /// <summary>
    /// Исключение гинерируемое при попытке повторного добавления ключа в множество рисуемых элементов.
    /// </summary>
    public class ElementException : Exception
    {
        public ElementException() { }
        public override string Message => "Рисуемый элемент c указанным именем уже cуществует!";
    }


    /// <summary>
    /// Предоставляет рисуемую координатную плоскость для рисования разного рода графики 
    /// реализуемой классами, производными от интерфейса IPainting.
    /// </summary>
    public class PaintingCoordinatePlane : CoordinatePlane
    {
        /// <summary>
        /// Панель содержащая рисуемую координатную плоскость в качестве фонового изображения.
        /// </summary>
        private Panel _plane;

        /// <summary>
        /// Возвращает размеры рисуемой координатной плоскости.
        /// </summary>
        public override Axis Size => (_plane.Width, _plane.Height);

        /// <summary>
        /// Событие пеерерисовывает элементы при ищменении размера плоскости.
        /// </summary>
        private void _plane_SizeChanged(object sender, EventArgs e)
        {
            foreach (var n in _paintings.Keys) 
            {
                _paintings[n] = (this[n], this[n].Paint(this));
            }
            Draw();
            if (SizeChanged != null) SizeChanged(Size);
        }

        /// <summary>
        /// Вызывается при изменении размеров рисуемой координатной плоскости.
        /// </summary>
        public event Action<Axis> SizeChanged;


        /// <summary>
        /// Возвращает или задает размеры рисуемой координатной сетки.
        /// </summary>
        public override Axis GridSize
        {
            get => base.GridSize;
            set
            {
                base.GridSize = value;
                foreach (var n in _paintings.Keys) 
                {
                    _paintings[n] = (this[n], this[n].Paint(this));
                }
                Draw();
                if (GridSizeChanged != null) GridSizeChanged(GridSize);
            }
        }

        /// <summary>
        /// Вызывается при изменении размеров сетки рисуемой координатной плоскости.
        /// </summary>
        public event Action<Axis> GridSizeChanged;


        /// <summary>
        /// Возвращает или задает центы рисуемой координатной плоскости.
        /// </summary>
        public override Axis Center
        {
            get => base.Center;
            set
            {
                base.Center = value;
                foreach (var n in _paintings.Keys)
                {
                    if (!(this[n] is Grid)) 
                    { 
                        _paintings[n] = (this[n], this[n].Paint(this)); 
                    }
                }
                Draw();
                if (CenterChanged != null) CenterChanged(Center);
            }
        }

        /// <summary>
        /// Вызывается при изменении центров рисуемой координатной плоскости.
        /// </summary>
        public event Action<Axis> CenterChanged;



        /// <summary>
        /// Конструктор устанавливает начальные значения рисуемой координатной плоскости.
        /// </summary>
        /// <param name="plane">Панель содержащая плоскость</param>
        public PaintingCoordinatePlane(in Panel plane)
        {
            _plane = plane;
            _plane.SizeChanged += _plane_SizeChanged;
        }



        /// <summary>
        /// Список рисуемых объектов и соответствующих им плоскостей с нарисованной графикой.
        /// </summary>
        private Dictionary<string, (IPainting Paint, Bitmap Plane)> _paintings 
            = new Dictionary<string, (IPainting Paint, Bitmap Plane)>();


        /// <summary>
        /// Позволяет задать или получить рисуемый элемент плоскости.
        /// </summary>
        public IPainting this[string name]
        {
            get
            {
                try { return _paintings[name].Paint; }
                catch { throw new ElementNotFoundException(); }
            }
            set
            {
                try
                {
                    _paintings[name] = (value, value.Paint(this));
                    Draw();
                }
                catch { throw new ElementNotFoundException(); }
            }
        }

        /// <summary>
        /// Помещает рисуемый элемент на плоскость.
        /// </summary>
        public void Add(string name, IPainting paint)
        {
            try { _paintings.Add(name, (paint, paint.Paint(this))); }
            catch
            { throw new ElementException(); }
            Draw();
        }
        
        /// <summary>
        /// Удаляет рисуемый элемент с плоскости.
        /// </summary>
        public void RemoveAt(string name)
        {
            try
            {
                _paintings.Remove(name);
                Draw();
            }
            catch { throw new ElementNotFoundException(); }
        }

        /// <summary>
        /// Возвращает количество рисуемых элементов плоскости.
        /// </summary>
        public int Count => _paintings.Count;



        /// <summary>
        /// Построить графику.
        /// </summary>
        private void Draw()
        {
            Bitmap bitmap = new Bitmap((int)Size.X, (int)Size.Y);
            Graphics draw = Graphics.FromImage(bitmap);

            foreach (var p in _paintings.Values)
            {
                draw.DrawImage(p.Plane, 0f, 0f, Size.X, Size.Y);
            }

            _plane.BackgroundImage = bitmap;
        }
    }
}
