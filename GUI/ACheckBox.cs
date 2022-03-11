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
    public class ACheckBox : CheckBox
    {
        private Color back_color = Color.SteelBlue;
        [Category("A Settings")]
        public Color OnBackColor
        {
            get => this.back_color;
            set
            {
                back_color = value;

                this.Invalidate();
            }
        }

        private Color toggle_color = Color.WhiteSmoke;
        [Category("A Settings")]
        public Color OnToggleColor
        {
            get => this.toggle_color;
            set
            {
                toggle_color = value;

                this.Invalidate();
            }
        }

        private Color off_backcolor = Color.Gray;
        [Category("A Settings")]
        public Color OffBackColor
        {
            get => this.off_backcolor; 
            set
            {
                off_backcolor = value;

                this.Invalidate();
            }
        }

        private Color off_togglecolor = Color.Gainsboro;
        [Category("A Settings")]
        public Color OffToggleColor
        {
            get => this.off_togglecolor;
            set
            {
                off_togglecolor = value;

                this.Invalidate();
            }
        }

        [Category("A Settings")]
        [Browsable(false)]
        public override string Text
        {
            get => base.Text;
            set { }
        }

        private bool solid_style = true;
        [Category("A Settings")]
        [DefaultValue(true)]
        public bool SolidStyle
        {
            get => solid_style;
            set
            {
                solid_style = value;

                this.Invalidate();
            }
        }

        public ACheckBox()
        {
            this.MinimumSize = new Size(45, 22);
        }

        private GraphicsPath GetFigurePath()
        {
            int arc_size = this.Height - 1;
            Rectangle left_arc = new Rectangle(0, 0, arc_size, arc_size);
            Rectangle right_arc = new Rectangle(this.Width - arc_size - 2, 0, arc_size, arc_size);

            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(left_arc, 90, 180);
            path.AddArc(right_arc, 270, 180);
            path.CloseFigure();

            return path;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int toggle_size = this.Height - 5;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.Clear(this.Parent.BackColor);

            if (this.Checked)
            {
                if (solid_style)
                {
                    e.Graphics.FillPath(new SolidBrush(back_color), GetFigurePath());
                }
                else
                {
                    e.Graphics.DrawPath(new Pen(back_color, 2), GetFigurePath());
                }

                e.Graphics.FillEllipse(new SolidBrush(toggle_color),
                  new Rectangle(this.Width - this.Height + 1, 2, toggle_size, toggle_size));
            }
            else
            {
                if (solid_style)
                {
                    e.Graphics.FillPath(new SolidBrush(off_backcolor), GetFigurePath());
                }
                else
                {
                    e.Graphics.DrawPath(new Pen(off_backcolor, 2), GetFigurePath());
                }

                e.Graphics.FillEllipse(new SolidBrush(off_togglecolor),
                  new Rectangle(2, 2, toggle_size, toggle_size));
            }
        }
    }
}
