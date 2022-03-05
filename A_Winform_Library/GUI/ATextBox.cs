using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace A_Winform_Library.GUI
{
    [DefaultEvent("_text_changed")]
    public partial class ATextBox : UserControl, IDisposable
    {
        private const int max_border_radius = 14;

        private Color border_color = Color.MediumSlateBlue;
        [Category("Advance Settings")]
        public Color BorderColor
        {
            get => border_color;
            set
            {
                border_color = value;
                this.Invalidate();
            }
        }

        private int border_size = 2;
        [Category("Advance Settings")]
        public int BorderSize
        {
            get => border_size;
            set
            {
                border_size = value;
                this.Invalidate();
            }
        }

        private int border_radius = 10;
        [Category("Advance Settings")]
        public int BorderRadius
        {
            get
            {
                return this.border_radius;
            }
            set
            {
                if (Multiline && (value <= max_border_radius))
                {
                    this.border_radius = value;
                }
                else if (Multiline && (value > max_border_radius))
                {
                    this.border_radius = max_border_radius;
                }
                else if (!Multiline && (value <= this.Height / 2))
                {
                    this.border_radius = value;
                }
                else if (!Multiline && (value > this.Height / 2))
                {
                    this.border_radius = this.Height / 2;
                }

                this.Invalidate();
            }
        }

        public HorizontalAlignment TextAlignment
        {
            get
            {
                return this.tbox_text.TextAlign;
            }
            set
            {
                this.tbox_text.TextAlign = value;
            }
        }

        private bool underline_style = false;
        [Category("Advance Settings")]
        public bool UnderlineStyle
        {
            get => underline_style;
            set
            {
                underline_style = value;
                this.Invalidate();
            }
        }

        private Color border_focus_color = Color.HotPink;
        [Category("Advance Settings")]
        public Color BorderFocusColor
        {
            get => border_focus_color;
            set => border_focus_color = value;
        }

        private bool is_focused = false;

        private Color placeholder_color = Color.DarkGray;
        [Category("Advance Settings")]
        public Color PlaceholderColor
        {
            get => placeholder_color;
            set
            {
                placeholder_color = value;
                if (is_passwordchar)
                {
                    this.tbox_text.ForeColor = value;
                }
            }
        }

        private string placeholder_text = "";
        [Category("Advance Settings")]
        public string PlaceholderText
        {
            get
            {
                return placeholder_text;
            }
            set
            {
                placeholder_text = value;
                this.tbox_text.Text = "";
                this.SetPlaceHolder();
            }
        }

        private bool is_placeholder = false;
        [Category("Advance Settings")]
        public bool IsPlaceholder
        {
            get => is_placeholder;
            set => is_placeholder = value;
        }

        private bool is_passwordchar = false;
        [Category("Advance Settings")]
        public bool IsPasswordchar
        {
            get => is_passwordchar;
            set
            {
                is_passwordchar = value;
                this.tbox_text.UseSystemPasswordChar = value;
            }
        }

        public bool PasswordChar
        {
            get
            {
                return this.tbox_text.UseSystemPasswordChar;
            }
            set
            {
                this.tbox_text.UseSystemPasswordChar = value;
            }
        }

        public bool Multiline
        {
            get
            {
                return this.tbox_text.Multiline;
            }
            set
            {
                this.tbox_text.Multiline = value;
            }
        }

        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                this.tbox_text.BackColor = value;
            }
        }

        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                base.ForeColor = value;
                this.tbox_text.ForeColor = value;
            }
        }

        public override Font Font
        {
            get => base.Font;
            set
            {
                base.Font = value;
                this.tbox_text.Font = value;
                if (this.DesignMode)
                {
                    this.UpdateControlHeight();
                }
            }
        }

        public override string Text
        {
            get
            {
                if (is_placeholder) return "";
                return this.tbox_text.Text.Trim();
            }
            set
            {
                this.tbox_text.Text = value;
                this.SetPlaceHolder();
            }
        }

        public event EventHandler _textbox_TextChanged;

        public ATextBox()
        {
            InitializeComponent();
        }

        public ATextBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private GraphicsPath GetFigurePath(RectangleF rect, float radius)
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graph = e.Graphics;

            if (this.border_radius > 1)
            {
                var rect_border_smooth = this.ClientRectangle;
                var rect_border = Rectangle.Inflate(rect_border_smooth, -border_size, -border_size);
                int smooth_size = border_size > 0 ? border_size : 1;

                using (GraphicsPath path_border_smooth = GetFigurePath(rect_border_smooth, border_radius))
                using (GraphicsPath path_border = GetFigurePath(rect_border, border_radius - border_size))
                using (Pen pen_border_smooth = new Pen(this.Parent.BackColor, smooth_size))
                using (Pen pen_border = new Pen(border_color, border_size))
                {
                    this.Region = new Region(path_border_smooth);

                    if (this.border_radius > 15) SetTextboxRoundedRegion();

                    graph.SmoothingMode = SmoothingMode.AntiAlias;
                    pen_border.Alignment = PenAlignment.Center;

                    if (this.is_focused)
                    {
                        pen_border.Color = border_focus_color;
                    }

                    if (underline_style)
                    {
                        graph.DrawPath(pen_border_smooth, path_border_smooth);
                        graph.SmoothingMode = SmoothingMode.None;
                        graph.DrawLine(pen_border, 0, this.Height - 1, this.Width, this.Height - 1);
                    }
                    else
                    {
                        graph.DrawPath(pen_border_smooth, path_border_smooth);
                        graph.DrawPath(pen_border, path_border);
                    }
                }
            }
            else
            {
                using (Pen pen_border = new Pen(border_color, border_size))
                {
                    this.Region = new Region(this.ClientRectangle);

                    pen_border.Alignment = PenAlignment.Inset;

                    if (this.is_focused)
                    {
                        pen_border.Color = border_focus_color;
                    }

                    if (underline_style)
                    {
                        graph.DrawLine(pen_border, 0, this.Height - 1, this.Width, this.Height - 1);
                    }
                    else
                    {
                        graph.DrawRectangle(pen_border, 0, 0, this.Width - 0.5f, this.Height - 0.5f);
                    }
                }
            }
        }

        private void SetTextboxRoundedRegion()
        {
            GraphicsPath path_text;
            if (Multiline)
            {
                path_text = GetFigurePath(this.tbox_text.ClientRectangle, border_radius - border_size);
                this.tbox_text.Region = new Region(path_text);
            }
            else
            {
                path_text = GetFigurePath(this.tbox_text.ClientRectangle, border_size * 2);
                this.tbox_text.Region = new Region(path_text);
            }
        }

        private void SetPlaceHolder()
        {
            if (string.IsNullOrEmpty(this.tbox_text.Text) && !placeholder_text.Equals(string.Empty))
            {
                is_placeholder = true;
                this.tbox_text.Text = placeholder_text;
                this.tbox_text.ForeColor = placeholder_color;
                if (is_passwordchar)
                {
                    this.tbox_text.UseSystemPasswordChar = false;
                }
            }
        }

        private void RemovePlaceHolder()
        {
            if (is_placeholder && !placeholder_text.Equals(string.Empty))
            {
                is_placeholder = false;
                this.tbox_text.Text = "";
                this.tbox_text.ForeColor = this.ForeColor;
                if (is_passwordchar)
                {
                    this.tbox_text.UseSystemPasswordChar = true;
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.DesignMode)
            {
                this.UpdateControlHeight();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.UpdateControlHeight();
        }

        private void UpdateControlHeight()
        {
            if (!this.tbox_text.Multiline)
            {
                int text_height = TextRenderer.MeasureText("Text", this.Font).Height + 1;
                this.tbox_text.Multiline = true;
                this.tbox_text.MinimumSize = new Size(0, text_height);
                this.tbox_text.Multiline = false;

                this.Height = this.tbox_text.Height + this.Padding.Top + this.Padding.Bottom;
            }
        }

        private void textbox_TextChanged(object sender, EventArgs e)
        {
            if (this._textbox_TextChanged != null)
            {
                this._textbox_TextChanged.Invoke(sender, e);
            }
        }

        private void textbox_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void textbox_MouseEnter(object sender, EventArgs e)
        {
            this.OnMouseEnter(e);
        }

        private void textbox_MouseLeave(object sender, EventArgs e)
        {
            this.OnMouseLeave(e);
        }

        private void textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.OnKeyPress(e);
        }

        private void textbox_Enter(object sender, EventArgs e)
        {
            this.is_focused = true;
            this.Invalidate();
            this.RemovePlaceHolder();
        }

        private void textbox_Leave(object sender, EventArgs e)
        {
            this.OnLeave(e);
            this.is_focused = false;
            this.Invalidate();
            this.SetPlaceHolder();
        }
    }
}
