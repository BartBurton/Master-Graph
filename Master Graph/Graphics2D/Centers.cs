using System.Drawing;

namespace Graphics2D
{
    /// <summary>
    /// Реализует построение центральных осей координатной плоскости на возвращаемом полотне.
    /// </summary>
    class Centers : IPainting
    {
        /// <summary>
        /// Карандаш, которым будут нанесены линии центров.
        /// </summary>
        public Pen Pen { get; set; } = new Pen(Color.Black, 2);



        public Bitmap Paint(CoordinatePlane space)
        {
            Bitmap bitmap = new Bitmap((int)space.Size.X, (int)space.Size.Y);
            Graphics draw = Graphics.FromImage(bitmap);

            draw.DrawLine(Pen,
                space.Center.X * space.GridSize.X, 0,
                space.Center.X * space.GridSize.X, space.Size.Y);

            draw.DrawLine(Pen, 0,
                space.Center.Y * space.GridSize.Y, space.Size.X,
                space.Center.Y * space.GridSize.Y);

            return bitmap;
        }
    }
}
