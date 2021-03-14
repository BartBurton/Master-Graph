using System.Drawing;

namespace Graphics2D
{
    /// <summary>
    /// Реализует построение координатной сетки на возвращаемом полотне.
    /// </summary>
    class Grid : IPainting
    {
        /// <summary>
        /// Карандаш, которым будут нанесены линии сетки.
        /// </summary>
        public Pen Pen { get; set; } = new Pen(Color.Black, 2);



        public Bitmap Paint(CoordinatePlane space)
        {
            Bitmap bitmap = new Bitmap((int)space.Size.X, (int)space.Size.Y);
            Graphics draw = Graphics.FromImage(bitmap);

            for (float i = 0; i < space.Size.X; i += space.GridSize.X)
            {
                draw.DrawLine(Pen, i, 0, i, space.Size.Y);
            }
            for (float i = 0; i < space.Size.Y; i += space.GridSize.Y)
            {
                draw.DrawLine(Pen, 0, i, space.Size.X, i);
            }

            return bitmap;
        }
    }
}
