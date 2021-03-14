using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace Graphics2D
{
    /// <summary>
    /// Исключение гинерируемое при некорректном обращении к множеству точек.
    /// </summary>
    public class SplaynPointNotFoundException : Exception
    {
        public SplaynPointNotFoundException() { }
        public override string Message => "Точка по указанному индексу не найдена!";
    }

    /// <summary>
    /// Реализует построение квадратичного сплайна на возвращаемом полотне.
    /// </summary>
    class Splayn : IPainting
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
                { throw new PointNotFoundException(); }
            }
            set
            {
                try
                { 
                    _dots[index] = value;
                    MakeSplayn();
                }

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
            MakeSplayn();
        }

        /// <summary>
        /// Удаляет указанную точку.
        /// </summary>
        public void Remove(Axis index)
        {
            if (!_dots.Remove(index))
            { throw new PointNotFoundException(); }
            MakeSplayn();
        }

        /// <summary>
        /// Шаг.
        /// </summary>
        private float _step = 1;
        /// <summary>
        /// Опредеояет шаг для вычисления следующей координаты сплайна.
        /// </summary>
        public float Step 
        {
            get => _step;
            set
            {
                _step = (value > 0) ? value : _step;
                MakeSplayn();
            }
        }

        /// <summary>
        /// Карандаш, которым будет нанесен сплайн.
        /// </summary>
        public Pen Pen { get; set; } = new Pen(Color.Black, 1);


        public Splayn() { }
        /// <summary>
        /// Рекомендуется использовать данный конструктор класса для создания объекта класса, 
        /// так как создание экземпляра при помощи синтаксиса инициализации объектов или отдельной инициализации
        /// полей будет происходить дольше из-за повторной инициализации списка точек для указанных параметров.
        /// </summary>
        public Splayn(float step, params Axis[] axes)
        {
            _dots = axes.ToList();
            _step = step;
            MakeSplayn();
        }


        //Аргументы точек x в формуле квадратичного сплайна
        private float[] a, b, c, z;

        /// <summary>
        /// Определить аргумент а.
        /// </summary>
        static private float A(float z, float x1, float x2, float y1, float y2) => 
            ((z * (x2 - x1)) + (y1 - y2)) / ((x1 - x2) * (x1 - x2));

        /// <summary>
        /// Определить аргумент b.
        /// </summary>
        static private float B(float z, float x1, float x2, float y1, float y2) => 
            ((z * ((x1 * x1) - (x2 * x2))) + (2 * x2 * (y2 - y1))) / ((x1 - x2) * (x1 - x2));

        /// <summary>
        /// Определить аргумент c.
        /// </summary>
        static private float C(float z, float x1, float x2, float y1, float y2) =>
            ((x1 * x1 * y2) - (x1 * x2 * ((2 * y2) + (x1 * z))) + ((x2 * x2) * (y1 + (x1 * z)))) / ((x1 - x2) * (x1 - x2));

        /// <summary>
        /// Определить z.
        /// </summary>
        static private float Z(float a, float x, float b) => 
            (2 * a * x) + b;

        /// <summary>
        /// Инициализирует аргументы функции квадратичного сплайна для каждой точки указанной для построения.
        /// </summary>
        private void MakeSplayn()
        {
            //Имеющиеся точки нужно отсортировать вдоль оси Х
            if (_dots.Count == 0) return;
            _dots = _dots.OrderBy((d => d.X)).ToList(); 
            int n = _dots.Count;

            a = new float[n - 1];
            b = new float[n - 1];
            c = new float[n - 1];
            z = new float[n];
            z[0] = 0.1f;

            //Инициализируем аргументы каждой точки
            for (int i = 1; i < n; i++)
            {
                a[i - 1]    =   A(z[i - 1], _dots[i].X, _dots[i - 1].X, _dots[i].Y, _dots[i - 1].Y);
                b[i - 1]    =   B(z[i - 1], _dots[i].X, _dots[i - 1].X, _dots[i].Y, _dots[i - 1].Y);
                c[i - 1]    =   C(z[i - 1], _dots[i].X, _dots[i - 1].X, _dots[i].Y, _dots[i - 1].Y);
                z[i]        =   Z(a[i - 1], _dots[i].X, b[i - 1]);
            }

            MakePoints();
        }

        /// <summary>
        /// Переинициализируемы список точек соответствующий, параметрам переданным для построения сплайна.
        /// </summary>
        private List<Axis> Points;

        /// <summary>
        /// По найденным аргументам точек находит все точки от наименьшего Х до наибольшего
        /// сдвигаясь на указанный шаг.
        /// </summary>
        private void MakePoints()
        {
            Points = new List<Axis>();

            float x = 0;
            float y = 0;

            for (int i = 0; i < _dots.Count - 1; i++)
            {
                x = _dots[i].X;
                while (x < _dots[i + 1].X)
                {
                    y = (a[i] * x * x) + (b[i] * x) + c[i];
                    Points.Add((x, y));
                    x += Step;
                }
            }
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

                    try
                    {
                        draw.DrawLine(Pen, x1, y1, x2, y2);
                    }
                    catch { }

                    x1 = x2;
                    y1 = y2;
                }
            }

            return bitmap;
        }
    }
}
