using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Graphics2D
{
    /// <summary>
    /// Исключение гинерируемое при некорректном обращении к множеству точек.
    /// </summary>
    public class DiscreteGraphPointNotFoundException : Exception
    {
        public DiscreteGraphPointNotFoundException() { }
        public override string Message => "Точка по указанному индексу не найдена!";
    }


    /// <summary>
    /// Реализует построение линий между точками на возвращаемом полотне.
    /// </summary>
    public class DiscreteGraph : IPainting
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

                catch (ArgumentOutOfRangeException)
                { throw new DiscreteGraphPointNotFoundException(); }
            }
            set
            {
                try
                { _dots[index] = value; }

                catch (ArgumentOutOfRangeException)
                { throw new DiscreteGraphPointNotFoundException(); }
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
            if (!_dots.Remove(index))
            { throw new DiscreteGraphPointNotFoundException(); }
        }


        /// <summary>
        /// Карандаш, которым будет нанесен дискретный график.
        /// </summary>
        public Pen Pen { get; set; } = new Pen(Color.Black, 2);

        public DiscreteGraph() { }
        public DiscreteGraph(params Axis[] dots)
        {
            _dots = dots.ToList();
        }


        public Bitmap Paint(CoordinatePlane space)
        {
            Bitmap bitmap = new Bitmap((int)space.Size.X, (int)space.Size.Y);
            Graphics draw = Graphics.FromImage(bitmap);

            Axis size = space.GridSize;
            Axis center = space.Center;
            float x1, y1, x2, y2;


            List<Axis>.Enumerator enumerator = _dots.GetEnumerator();

            enumerator.MoveNext();
            x1 = (enumerator.Current.X + center.X) * size.X;
            y1 = (center.Y - enumerator.Current.Y) * size.Y;

            for (int i = 1; i < _dots.Count; i++)
            {
                enumerator.MoveNext();
                x2 = (enumerator.Current.X + center.X) * size.X;
                y2 = (center.Y - enumerator.Current.Y) * size.Y;

                try
                {
                    draw.DrawLine(Pen, x1, y1, x2, y2);
                }
                catch { }

                x1 = x2;
                y1 = y2;
            }

            return bitmap;
        }
    }
}
