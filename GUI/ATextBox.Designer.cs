
namespace A_Winform_Library.GUI
{
    partial class ATextBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                this.tbox_text.KeyPress -= textbox_KeyPress;
                this.tbox_text.Click -= textbox_Click;
                this.tbox_text.TextChanged -= textbox_TextChanged;
                this.tbox_text.Enter -= textbox_Enter;
                this.tbox_text.Leave -= textbox_Leave;
                this.tbox_text.MouseEnter -= textbox_MouseEnter;
                this.tbox_text.MouseLeave -= textbox_MouseLeave;
                this.tbox_text.Enter -= textbox_Enter;

                this._textbox_TextChanged = null;

                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbox_text = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbox_text
            // 
            this.tbox_text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbox_text.CausesValidation = false;
            this.tbox_text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbox_text.Location = new System.Drawing.Point(7, 7);
            this.tbox_text.Name = "tbox_text";
            this.tbox_text.Size = new System.Drawing.Size(236, 13);
            this.tbox_text.TabIndex = 1;
            this.tbox_text.Click += new System.EventHandler(this.textbox_Click);
            this.tbox_text.TextChanged += new System.EventHandler(this.textbox_TextChanged);
            this.tbox_text.Enter += new System.EventHandler(this.textbox_Enter);
            this.tbox_text.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textbox_KeyPress);
            this.tbox_text.Leave += new System.EventHandler(this.textbox_Leave);
            this.tbox_text.MouseEnter += new System.EventHandler(this.textbox_MouseEnter);
            this.tbox_text.MouseLeave += new System.EventHandler(this.textbox_MouseLeave);
            // 
            // ATextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbox_text);
            this.Name = "ATextBox";
            this.Padding = new System.Windows.Forms.Padding(7);
            this.Size = new System.Drawing.Size(250, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbox_text;
    }
}
