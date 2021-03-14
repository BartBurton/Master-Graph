using System;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;

namespace Graphics2D
{
    /// <summary>
    /// Исключение гинерируемое при некорректном обращении к множеству точек.
    /// </summary>
    public class PointNotFoundException : Exception
    {
        public PointNotFoundException() { }
        public override string Message => "Точка по указанному индексу не найдена!";
    }


    /// <summary>
    /// Реализует нанесение множества точек на возвращаемом полотне.
    /// </summary>
    public class Points : IPainting
    {
        /// <summary>
        /// Список точек.
        /// </summary>
        private List<Axis> _dots = new List<Axis>();

        /// <summary>
        /// Возвращает количество точек в списке.
        /// </summary>
        public int Count => _dots.Count;

        /// <summary>
        /// Возвращает или устанавливает координаты точки по указанному индексу.
        /// </summary>
        public Axis this[int index]
        {
            get
            {
                try
                { return _dots[index]; }

                catch(ArgumentOutOfRangeException)
                { throw new PointNotFoundException(); }
            }
            set
            {
                try
                { _dots[index] = value; }

                catch (ArgumentOutOfRangeException)
                { throw new PointNotFoundException(); }
            }
        }

        /// <summary>
        /// Добавляет указанные точки в список точек.
        /// </summary>
        public void Add(params Axis[] points)
        {
            _dots.AddRange(points);
        }

        /// <summary>
        /// Удаляет указанную точку.
        /// </summary>
        public void Remove(Axis index)
        {
            if(!_dots.Remove(index))
            { throw new PointNotFoundException(); }
        }

        /// <summary>
        /// Карандаш, которым будут нанесены точки.
        /// </summary>
        public Pen Pen { get; set; } = new Pen(Color.Black, 2);

        public Points() { }
        public Points(params Axis[] dots)
        {
            _dots = dots.ToList();
        }



        public Bitmap Paint(CoordinatePlane space)
        {
            Bitmap bitmap = new Bitmap((int)space.Size.X, (int)space.Size.Y);
            Graphics draw = Graphics.FromImage(bitmap);

            Axis size = space.GridSize;
            Axis center = space.Center;

            float len = Pen.Width / 2f;

            foreach (var dot in _dots)
            {
                try
                {
                    draw.DrawEllipse(Pen,
                        ((dot.X + center.X) * size.X) - len,
                        ((center.Y - dot.Y) * size.Y) - len,
                        Pen.Width, Pen.Width);
                }
                catch { }
            }

            return bitmap;
        }
    }
}
