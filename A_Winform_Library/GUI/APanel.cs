using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using A_Winform_Library.GUI;
using System.ComponentModel;

namespace A_Winform_Library.GUI
{
    public class APanel : Panel
    {
        private GraphicHelper helper;

        private int border_width = 5;
        [Category("Advance Settings")]
        public int BorderWidth
        {
            get => border_width;
            set
            {
                if (value > 1 && value < this.border_radius)
                {
                    border_width = value;
                    this.Invalidate();
                }
            }
        }

        private int border_radius = 30;
        [Category("Advance Settings")]
        public int BorderRadius
        {
            get => border_radius;
            set
            {
                if (value >= this.Width / 2)
                {
                    border_radius = this.Width / 2;
                }
                else if (value <= 5)
                {
                    border_radius = 5;
                }
                else
                {
                    border_radius = value;
                }

                this.Invalidate();
            }
        }

        private Color border_color = Color.DarkGray;
        [Category("Advance Settings")]
        public Color BorderColor
        {
            get
            {
                return border_color;
            }
            set
            {
                border_color = value;
                this.Invalidate();
            }
        }

        private bool use_gradient = false;
        [Category("Advance Settings")]
        public bool UseGradientColor
        {
            get
            {
                return use_gradient;
            }
            set
            {
                use_gradient = value;
                this.Invalidate();
            }
        }

        private Color start_color = Color.Gainsboro;
        [Category("Advance Settings")]
        public Color StartColor
        {
            get
            {
                return start_color;
            }
            set
            {
                start_color = value;
                this.Invalidate();
            }
        }

        private Color stop_color = Color.Gainsboro;
        [Category("Advance Settings")]
        public Color StopColor
        {
            get
            {
                return stop_color;
            }
            set
            {
                stop_color = value;
                this.Invalidate();
            }
        }

        private int gradient_angle = 45;
        [Category("Advance Settings")]
        public int GradientAngle
        {
            get => gradient_angle;
            set
            {
                gradient_angle = value;
                this.Invalidate();
            }
        }

        private bool use_angle_scale = false;
        [Category("Advance Settings")]
        public bool GradientAngleScale
        {
            get
            {
                return use_angle_scale;
            }
            set
            {
                use_angle_scale = value;
                this.Invalidate();
            }
        }

        public APanel()
        {
            this.helper = new GraphicHelper();

            this.Size = new Size(200, 200);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (this.Width < 10) this.Width = 10;
            if (this.Height < 10) this.Height = 10;

            if (this.Width < this.Height)
            {
                if (this.border_radius > this.Width)
                {
                    this.border_radius = this.Width;
                }
            }
            else
            {
                if (this.border_radius > this.Height)
                {
                    this.border_radius = this.Height;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle frame_border = new Rectangle(0, 0, this.Width, this.Height);
            RectangleF frame_fill = new RectangleF(0, 0, this.Width, this.Height);

            using (GraphicsPath path_border = this.helper.GetFigurePath(frame_border, this.border_radius))
            using (GraphicsPath path_surface = this.helper.GetFigurePath(frame_fill, this.border_radius))
            using (Pen pen_border = new Pen(this.border_color, this.border_width))
            {
                this.Region = new Region(path_border);

                if (use_gradient)
                {
                    LinearGradientBrush brush_head = new LinearGradientBrush(frame_fill, start_color, stop_color, gradient_angle, use_angle_scale);
                    e.Graphics.FillRectangle(brush_head, frame_fill);
                }
                else
                {
                    SolidBrush brush_head = new SolidBrush(this.BackColor);
                    e.Graphics.FillRectangle(brush_head, frame_fill);
                }

                pen_border.Alignment = PenAlignment.Inset;
                e.Graphics.DrawPath(pen_border, path_border);
            }
        }
    }
}
