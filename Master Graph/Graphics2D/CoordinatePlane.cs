using System.Drawing;

namespace Graphics2D
{
    /// <summary>
    /// Определяет значение для указания функции вдоль, какой оси чертить график
    /// </summary>
    public enum Along
    {
        X, Y
    }

    /// <summary>
    /// Представляет пару значений соответсвующих оси X и Y.
    /// </summary>
    public struct Axis
    {
        /// <summary>
        /// Значение по оси Х.
        /// </summary>
        public float X;
        /// <summary>
        /// Значение по оси Y.
        /// </summary>
        public float Y;

        /// <summary>
        /// Значение объекту можно здать через кортеж.
        /// </summary>
        public static implicit operator Axis((float X, float Y) axis)
            => new Axis(axis.X, axis.Y);

        /// <summary>
        /// Значение объекта можно присвоить кортежу.
        /// </summary>
        public static implicit operator (float X, float Y)(Axis axis)
            => (axis.X, axis.Y);

        public Axis(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Возвращает строковое представление значений X и Y
        /// </summary>
        public override string ToString() => $"({X} ; {Y})";
    }

    /// <summary>
    /// Представляет интерфейс для классов, которые могут наноситься на полотно.
    /// </summary>
    public interface IPainting
    {
        /// <summary>
        /// Возвращает полотно с нанесенной на нем графикой.
        /// </summary>
        /// <param name="space">Координатноая плоскость предоставляющая параметры для построения графики.</param>
        Bitmap Paint(CoordinatePlane space);
    }

    /// <summary>
    /// Представляет координатную плоскость.
    /// </summary>
    public abstract class CoordinatePlane
    {
        /// <summary>
        /// Ширина и высота координатной плоскости в пикселях.
        /// </summary>
        public virtual Axis Size { get; set; }

        /// <summary>
        /// Шаг.
        /// </summary>
        private Axis _sizeGrid = (1, 1);
        /// <summary>
        /// Представляет расстояние между полосами сетки координатной плоскости.
        /// </summary>
        public virtual Axis GridSize
        {
            get => _sizeGrid;
            set
            {
                _sizeGrid.X = (value.X >= 1 && value.X <= Size.X) ? value.X : _sizeGrid.X;
                _sizeGrid.Y = (value.Y >= 1 && value.Y <= Size.Y) ? value.Y : _sizeGrid.Y;
            }
        }

        /// <summary>
        /// Центр.
        /// </summary>
        private Axis _center;
        /// <summary>
        /// Представляет координаты для проведения координатных осей.
        /// Задается не в пикселях, а в количестве шагов.
        /// </summary>
        public virtual Axis Center
        {
            get => _center;
            set
            {
                if(value.X >= Size.X / GridSize.X)
                {
                    _center.X = (int)(Size.X / GridSize.X);
                }
                else if(value.X <= 0)
                {
                    _center.X = 0;
                }
                else
                {
                    _center.X = value.X;
                }

                if (value.Y >= Size.Y / GridSize.Y)
                {
                    _center.Y = (int)(Size.Y / GridSize.Y);
                }
                else if (value.Y <= 0)
                {
                    _center.Y = 0;
                }
                else
                {
                    _center.Y = value.Y;
                }
            }
        }
    }
}
