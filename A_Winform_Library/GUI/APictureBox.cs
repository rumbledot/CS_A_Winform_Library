using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace A_Winform_Library.GUI
{
    public class APictureBox : PictureBox
    {
        private int border_size = 2;
        [Category("Advance Settings")]
        public int BorderSize
        {
            get { return border_size; }
            set
            {
                border_size = value;
                this.Invalidate();
            }
        }

        private Color border_color1 = Color.RoyalBlue;
        [Category("Advance Settings")]
        public Color BorderColor1
        {
            get { return border_color1; }
            set
            {
                border_color1 = value;
                this.Invalidate();
            }
        }

        private Color border_color2 = Color.HotPink;
        [Category("Advance Settings")]
        public Color BorderColor2
        {
            get { return border_color2; }
            set
            {
                border_color2 = value;
                this.Invalidate();
            }
        }

        private DashStyle border_line_style = DashStyle.Solid;
        [Category("Advance Settings")]
        public DashStyle BorderLineStyle
        {
            get { return border_line_style; }
            set
            {
                border_line_style = value;
                this.Invalidate();
            }
        }

        private DashCap border_cap_style = DashCap.Flat;
        [Category("Advance Settings")]
        public DashCap BorderCapStyle
        {
            get { return border_cap_style; }
            set
            {
                border_cap_style = value;
                this.Invalidate();
            }
        }

        private float gradient_angle = 50F;
        [Category("Advance Settings")]
        public float GradientAngle
        {
            get { return gradient_angle; }
            set
            {
                gradient_angle = value;
                this.Invalidate();
            }
        }

        public APictureBox()
        {
            this.Size = new Size(100, 100);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Size = new Size(this.Width, this.Width);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            var graph = pe.Graphics;
            var rect_contour_smooth = Rectangle.Inflate(this.ClientRectangle, -1, -1);
            var rect_border = Rectangle.Inflate(rect_contour_smooth, -border_size, -border_size);
            var smooth_size = border_size > 0 ? border_size * 3 : 1;

            using (var border_G_color = new LinearGradientBrush(rect_border, border_color1, border_color2, gradient_angle))
            using (var path_region = new GraphicsPath())
            using (var pen_smooth = new Pen(this.Parent.BackColor, smooth_size))
            using (var pen_border = new Pen(border_G_color, border_size))
            {
                graph.SmoothingMode = SmoothingMode.AntiAlias;
                pen_border.DashStyle = border_line_style;
                pen_border.DashCap = border_cap_style;
                path_region.AddEllipse(rect_contour_smooth);

                this.Region = new Region(path_region);

                graph.DrawEllipse(pen_smooth, rect_contour_smooth);
                if (border_size > 0)
                {
                    graph.DrawEllipse(pen_border, rect_border);
                }
            }
        }
    }
}
