using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using static A_Winform_Library.GUI.GraphicHelper;
using System.ComponentModel;

namespace A_Winform_Library.GUI
{
    public class viCircularLoading : Panel
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

        private float radius = 120;

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

        private CircularLoadingSpeed animation_speed = CircularLoadingSpeed.Medium;
        [Category("A Settings")]
        public CircularLoadingSpeed AnimationSpeed
        {
            get
            {
                return animation_speed;
            }
            set
            {
                animation_speed = value;
            }
        }

        private Thread animate_thread;

        public viCircularLoading()
        {
            this.helper = new GraphicHelper();

            this.Size = new Size(200, 200);
            this.radius = 100;
        }

        public void StartAnimation()
        {
            animate_thread = new Thread(new ThreadStart(AnimateLoading));
            animate_thread.Start();
        }

        public void StopAnimation()
        {
            animate_thread.Abort();
            animate_thread = null;
        }

        public void AnimateLoading()
        {
            while (true)
            {
                progress = progress >= 100 ? 0 : progress + 1;

                Thread.Sleep((int)animation_speed);
                this.Invalidate();
            }
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

                float progress_fraction = this.progress / 100f;
                float current_progress = 360f * progress_fraction;
                float radius = this.Width / 2;

                switch (this.IndicatorType)
                {
                    case ProgressIndicatorTypes.Bar:
                        Rectangle rect_indicator = new Rectangle(this.indicator_size * 2, this.indicator_size * 2, this.Width - this.indicator_size * 4, this.Width - this.indicator_size * 4);

                        pen_indicator.Alignment = PenAlignment.Inset;
                        e.Graphics.DrawArc(pen_indicator, rect_indicator, current_progress, 2);
                        e.Graphics.DrawArc(pen_indicator, rect_indicator, current_progress + 60, 2);

                        Pen pen_indicator1 = new Pen(indicator_color, radius * progress_fraction);
                        pen_indicator1.Alignment = PenAlignment.Inset;
                        e.Graphics.DrawArc(pen_indicator1, rect_indicator, current_progress + 10, 2);
                        e.Graphics.DrawArc(pen_indicator1, rect_indicator, current_progress + 50, 2);

                        Pen pen_indicator2 = new Pen(indicator_color, radius * progress_fraction);
                        pen_indicator2.Alignment = PenAlignment.Inset;
                        e.Graphics.DrawArc(pen_indicator2, rect_indicator, current_progress + 18, 2);
                        e.Graphics.DrawArc(pen_indicator2, rect_indicator, current_progress + 42, 2);

                        Pen pen_indicator3 = new Pen(indicator_color, radius * progress_fraction);
                        pen_indicator2.Alignment = PenAlignment.Inset;
                        e.Graphics.DrawArc(pen_indicator3, rect_indicator, current_progress + 30, 2);
                        break;
                    case ProgressIndicatorTypes.Pointer:
                        RectangleF rect_ahead1 = this.PointerPosition(this.indicator_size * 0.75f, current_progress + 30);
                        RectangleF rect_ahead2 = this.PointerPosition(this.indicator_size * 0.5f, current_progress + 50);
                        RectangleF rect_ahead3 = this.PointerPosition(this.indicator_size * 0.25f, current_progress + 60);
                        RectangleF rect_head = this.PointerPosition(this.indicator_size, current_progress);
                        RectangleF rect_tail1 = this.PointerPosition(this.indicator_size * 0.75f, current_progress - 30);
                        RectangleF rect_tail2 = this.PointerPosition(this.indicator_size * 0.5f, current_progress - 50);
                        RectangleF rect_tail3 = this.PointerPosition(this.indicator_size * 0.25f, current_progress - 60);

                        e.Graphics.FillEllipse(brush_head, rect_ahead1);
                        e.Graphics.FillEllipse(brush_head, rect_ahead2);
                        e.Graphics.FillEllipse(brush_head, rect_ahead3);
                        e.Graphics.FillEllipse(brush_head, rect_head);
                        e.Graphics.FillEllipse(brush_head, rect_tail1);
                        e.Graphics.FillEllipse(brush_head, rect_tail2);
                        e.Graphics.FillEllipse(brush_head, rect_tail3);
                        break;
                    default:
                        break;
                }
            }
        }

        private RectangleF PointerPosition(float radius, float degree)
        {
            Point center = helper.RadiantPosition(this.Width / 2, this.Width / 2, degree, (this.Width / 2) - this.indicator_size);

            return new RectangleF(center.X - radius, center.Y - radius, radius * 2, radius * 2);
        }
    }
}
