using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Winform_Library.GUI
{
    public class GraphicHelper
    {
        public enum ProgressIndicatorTypes
        {
            Bar,
            Pointer
        }

        public enum CircularLoadingSpeed
        {
            Slow = 200,
            Medium = 100,
            Fast = 50
        }

        public double DegreeRadiant(float degree)
        {
            return Math.PI * (degree / 180f);
        }

        public Point RadiantPosition(int center_x, int center_y, float degree, int distance)
        {
            var rad = this.DegreeRadiant(degree);
            var pos_x = center_x + Math.Cos(rad) * distance;
            var pos_y = center_y + Math.Sin(rad) * distance;

            return new Point((int)pos_x, (int)pos_y);
        }
        public GraphicsPath GetFigurePath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}
