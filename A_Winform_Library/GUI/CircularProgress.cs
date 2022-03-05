using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using static A_Winform_Library.GUI.GraphicHelper;
using System.ComponentModel;

namespace A_Winform_Library.GUI
{
    public class viCircularProgress : Panel
    {
        private GraphicHelper helper;

        private ProgressIndicatorTypes indicator_type = ProgressIndicatorTypes.Bar;
        [Category("A Settings")]
        public ProgressIndicatorTypes IndicatorType
        {
            get => indicator_type;
            set
            {
                indicator_type = value;
                this.Invalidate();
            }
        }

        private int radius = 120;

        private bool use_track = true;
        [Category("A Settings")]
        public bool UseProgressTrack
        {
            get => use_track;
            set
            {
                use_track = value;
                this.Invalidate();
            }
        }

        private int track_width = 10;
        [Category("A Settings")]
        public int TrackWidth
        {
            get => track_width;
            set
            {
                if (value > 5 && value < this.radius)
                {
                    track_width = value;
                    this.Invalidate();
                }
            }
        }

        private Color track_color = Color.DarkGray;
        [Category("A Settings")]
        public Color TrackColor
        {
            get
            {
                return track_color;
            }
            set
            {
                track_color = value;
                this.Invalidate();
            }

        }

        private int indicator_size = 15;
        [Category("A Settings")]
        public int IndicatorSize
        {
            get => indicator_size;
            set
            {
                if (value <= this.track_width + 5)
                {
                    indicator_size = this.track_width + 5;
                }
                else
                {
                    indicator_size = value;
                }

                this.Invalidate();
            }
        }

        private Color indicator_color = Color.SteelBlue;
        [Category("A Settings")]
        public Color IndicatorColor
        {
            get
            {
                return indicator_color;
            }
            set
            {
                indicator_color = value;
                this.Invalidate();
            }
        }

        private float progress = 100f;
        [Category("A Settings")]
        public float ProgressPercentage
        {
            get => progress;
            set
            {
                if (value > 0f && value <= 100f)
                {
                    progress = value;
                    this.Invalidate();
                }
            }
        }

        public viCircularProgress()
        {
            this.helper = new GraphicHelper();

            this.Size = new Size(200, 200);
            this.radius = 100;
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);

            if (this.Width < this.indicator_size * 3)
            {
                this.Width = this.indicator_size * 2;
            }
            else
            {
                this.Size = new Size(this.Width, this.Width);
                this.radius = this.Width / 2;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle outer_frame = new Rectangle(0, 0, this.Width, this.Width);
            Rectangle rect_track = new Rectangle(this.indicator_size, this.indicator_size, this.Width - this.indicator_size * 2, this.Width - this.indicator_size * 2);
            Rectangle rect_indicator = new Rectangle(this.indicator_size, this.indicator_size, this.Width - this.indicator_size * 2, this.Width - this.indicator_size * 2);
            RectangleF rect_head = this.HeadPosition(360f * (this.progress / 100f));

            using (GraphicsPath path_outer = helper.GetFigurePath(rect_track, this.Width - 10))
            using (SolidBrush brush_head = new SolidBrush(indicator_color))
            using (Pen pen_surface = new Pen(Color.Transparent, 1))
            using (Pen pen_track = new Pen(track_color, this.track_width))
            using (Pen pen_indicator = new Pen(indicator_color, this.indicator_size))
            {
                pen_track.Alignment = PenAlignment.Center;
                pen_indicator.Alignment = PenAlignment.Center;

                //frame
                e.Graphics.DrawRectangle(pen_surface, outer_frame);

                //track
                if (this.use_track)
                {
                    e.Graphics.DrawArc(pen_track, rect_track, 0, 360);
                }

                switch (this.IndicatorType)
                {
                    case ProgressIndicatorTypes.Bar:
                        e.Graphics.DrawArc(pen_indicator, rect_indicator, 0, 360f * (this.progress / 100f));
                        break;
                    case ProgressIndicatorTypes.Pointer:
                        e.Graphics.FillEllipse(brush_head, rect_head);
                        break;
                    default:
                        e.Graphics.DrawArc(pen_indicator, rect_indicator, 0, 360f * (this.progress / 100f));
                        break;
                }
            }
        }

        private RectangleF HeadPosition(float degree)
        {
            Point center = helper.RadiantPosition(this.radius, this.radius, degree, this.radius - this.track_width);

            return new RectangleF(center.X - (this.indicator_size), center.Y - (this.indicator_size), this.indicator_size * 2, this.indicator_size * 2);
        }
    }
}
