using System.Drawing;

namespace Graphics2D
{
    /// <summary>
    /// Реализует построение отметок вдоль центральных осей координатной плоскости на возвращаемом полотне.
    /// </summary>
    class Marks : IPainting
    {
        /// <summary>
        /// Растояние.
        /// </summary>
        private Axis _size = (0, 0);
        /// <summary>
        /// Расстояние по X и Y, на котором будут расставляться осевые отметки.
        /// </summary>
        public Axis Size 
        { 
            get => _size;
            set
            {
                _size.X = (value.X >= 0) ? value.X : 0;
                _size.Y = (value.Y >= 0) ? value.Y : 0;
            }
        }

        /// <summary>
        /// Ширина отметок.
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// Карандаш, которым будут нарисованы отметки.
        /// </summary>
        public Pen Pen = new Pen(Color.Black, 2);



        public Bitmap Paint(CoordinatePlane space)
        {
            Bitmap bitmap = new Bitmap((int)space.Size.X, (int)space.Size.Y);
            Graphics draw = Graphics.FromImage(bitmap);

            Axis size = space.GridSize;
            Axis center = space.Center;

            if (Size.X != 0)
            {
                for (float i = center.X; i < space.Size.X; i += Size.X)
                {
                    draw.DrawLine(Pen, i * size.X, (center.Y * size.Y) - Width / 2,
                        i * size.X, (center.Y * size.Y) + Width / 2);
                }
                for (float i = center.X; i > 0; i -= Size.X)
                {
                    draw.DrawLine(Pen, i * size.X, (center.Y * size.Y) - Width / 2,
                        i * size.X, (center.Y * size.Y) + Width / 2);
                }
            }

            if (Size.Y != 0)
            {
                for (float i = center.Y; i < space.Size.Y; i += Size.Y)
                {
                    draw.DrawLine(Pen, (center.X * size.X) - Width / 2, i * size.Y,
                        (center.X * size.X) + Width / 2, i * size.Y);
                }
                for (float i = center.Y; i > 0; i -= Size.Y)
                {
                    draw.DrawLine(Pen, (center.X * size.X) - Width / 2, i * size.Y,
                        (center.X * size.X) + Width / 2, i * size.Y);
                }
            }

            return bitmap;
        }
    }
}
