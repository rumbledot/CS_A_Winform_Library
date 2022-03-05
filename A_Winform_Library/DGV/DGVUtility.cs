using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace A_Winform_Library.DGV
{
    public enum DGVColumnFormats
    {
        None,
        Not_Specified,
        Decimal,
        Decimal_3,
        Decimal_4,
        Decimal_0,
        Decimal_3_0,
        Decimal_4_0,
        Integer,
        Integer_Set,
        Integer_0,
        Percentage,
        Percentage_0,
        Percentage_Int,
        Percentage_Int_0,
        Clean_Date,
        Precise_Date,
        Standard_Date,
    }

    public static class DGVUtility
    {
        public static DataGridView Initialise(this DataGridView dgv, bool read_only = true)
        {
            dgv.ReadOnly = read_only;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgv.AllowUserToResizeColumns = false;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AutoGenerateColumns = false;
            //dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            return dgv;
        }

        public static DataGridViewColumn NewColumn<T>(this DataGridViewColumn col, string column_header, string object_property_name = "", bool read_only = true)
        {
            col = new DataGridViewTextBoxColumn();

            col.HeaderText = column_header;
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col.ReadOnly = read_only;
            col.Name = column_header;

            if (!string.IsNullOrEmpty(object_property_name))
            {
                col.DataPropertyName = object_property_name;

                return col;
            }
            else
            {
                column_header.Replace(' ', '_').ToLower();

                col.DataPropertyName = $"{typeof(T).GetType().Name}_{column_header}";

                return col;
            }
        }

        public static DataGridViewColumn NewColumn(this DataGridViewColumn col, string column_header, string object_property_name = "", bool read_only = true)
        {
            col = new DataGridViewTextBoxColumn();

            col.HeaderText = column_header;
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            col.ReadOnly = read_only;
            col.Name = column_header;

            if (!string.IsNullOrEmpty(object_property_name))
            {
                col.DataPropertyName = object_property_name;

                return col;
            }
            else
            {
                column_header.Replace(' ', '_').ToLower();

                col.DataPropertyName = column_header;

                return col;
            }
        }

        public static DataGridViewImageColumn NewImageColumn(this DataGridViewImageColumn col, string column_header, string object_property_name = "", bool read_only = true)
        {
            col = new DataGridViewImageColumn();

            col.HeaderText = column_header;
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col.ReadOnly = read_only;
            col.Name = column_header;

            return col;
        }

        public static DataGridViewButtonColumn NewButtonColumn(this DataGridViewButtonColumn col, string column_header, string object_property_name = "")
        {
            col = new DataGridViewButtonColumn();

            col.HeaderText = column_header;
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col.ReadOnly = false;
            col.Name = column_header;

            return col;
        }

        public static DataGridViewDisableButtonColumn NewButtonColumn(this DataGridViewDisableButtonColumn col, string column_header, string object_property_name = "")
        {
            col = new DataGridViewDisableButtonColumn();

            col.HeaderText = column_header;
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col.ReadOnly = false;
            col.Name = column_header;

            return col;
        }

        public static DataGridViewColumn NewButtonColumn(this DataGridViewColumn col, string column_name, string object_property_name = "")
        {
            col = new DataGridViewTextBoxColumn();

            col.HeaderText = "..";
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col.ReadOnly = true;
            col.DataPropertyName = object_property_name;
            col.Name = column_name;

            col.DefaultCellStyle.BackColor = Color.LightGray;

            return col;
        }

        public static DataGridViewCheckBoxColumn NewCheckBoxColumn<T>(this DataGridViewCheckBoxColumn col, string column_header, string object_property_name = "")
        {
            col = new DataGridViewCheckBoxColumn();

            col.HeaderText = column_header;
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col.ReadOnly = false;
            col.Name = column_header;

            if (!string.IsNullOrEmpty(object_property_name))
            {
                col.DataPropertyName = object_property_name;

                return col;
            }
            else
            {
                column_header.Replace(' ', '_').ToLower();

                col.DataPropertyName = $"{typeof(T).GetType().Name}_{column_header}";

                return col;
            }
        }

        public static DataGridViewCheckBoxColumn NewCheckBoxColumn(this DataGridViewCheckBoxColumn col, string column_header, string object_property_name = "")
        {
            col = new DataGridViewCheckBoxColumn();

            col.HeaderText = column_header;
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col.ReadOnly = false;
            col.Name = column_header;

            if (!string.IsNullOrEmpty(object_property_name))
            {
                col.DataPropertyName = object_property_name;

                return col;
            }
            else
            {
                column_header.Replace(' ', '_').ToLower();

                col.DataPropertyName = column_header;

                return col;
            }
        }

        public static DataGridViewComboBoxColumn NewComboBoxColumn<T>(this DataGridViewComboBoxColumn col,
            string column_header, string data_display_member, string data_value_member, List<T> source,
            string object_property_name,
            DataGridViewComboBoxDisplayStyle display_style = DataGridViewComboBoxDisplayStyle.DropDownButton)
        {
            try
            {
                col = new DataGridViewComboBoxColumn();

                col.Name = column_header;
                col.HeaderText = column_header;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.ReadOnly = false;
                col.DisplayStyle = display_style;

                col.DataPropertyName = object_property_name;

                foreach (var item in source)
                {
                    col.Items.Add(item);
                }

                //col.Items.AddRange(source);
                col.DisplayMember = data_display_member;
                col.ValueMember = data_value_member;

                return col;
            }
            catch (Exception ex)
            {
                throw new Exception($"NewComboBoxColumn Exception \n {ex.Message} \n {ex.StackTrace}");
            }
        }

        public static DataGridViewComboBoxColumn NewComboBoxColumn(this DataGridViewComboBoxColumn col,
            string column_header, string data_display_member, string data_value_member, DataTable source_dt,
            string object_property_name,
            DataGridViewComboBoxDisplayStyle display_style = DataGridViewComboBoxDisplayStyle.DropDownButton)
        {
            col = new DataGridViewComboBoxColumn();

            col.Name = column_header;
            col.HeaderText = column_header;
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col.ReadOnly = false;
            col.DisplayStyle = display_style;

            col.DataPropertyName = object_property_name;

            col.DataSource = source_dt;
            col.DisplayMember = data_display_member;
            col.ValueMember = data_value_member;

            return col;
        }

        public static DataGridViewColumn ColumnWidth(this DataGridViewColumn col, int column_width, DataGridViewTriState resizeable = DataGridViewTriState.False, DataGridViewAutoSizeColumnMode resize_mode = DataGridViewAutoSizeColumnMode.None)
        {
            col.Width = column_width;
            if (resize_mode == DataGridViewAutoSizeColumnMode.Fill) col.MinimumWidth = column_width;
            col.AutoSizeMode = resize_mode;
            col.Resizable = resizeable;

            return col;
        }

        public static DataGridViewColumn ColumnWidth(this DataGridViewColumn col, int column_minimum_width, int column_width, DataGridViewTriState resizeable = DataGridViewTriState.False, DataGridViewAutoSizeColumnMode resize_mode = DataGridViewAutoSizeColumnMode.None)
        {
            col.Width = column_width;
            col.MinimumWidth = column_minimum_width;
            col.AutoSizeMode = resize_mode;
            col.Resizable = resizeable;

            return col;
        }

        public static DataGridViewColumn ColumnFill(this DataGridViewColumn col, int minimum_width = 100, DataGridViewTriState resizeable = DataGridViewTriState.False)
        {
            col.MinimumWidth = minimum_width;
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            col.Resizable = resizeable;

            return col;
        }

        public static DataGridViewColumn HiddenColumn(this DataGridViewColumn col)
        {
            col.Visible = false;
            return col;
        }

        public static DataGridViewColumn AlignRight(this DataGridViewColumn col)
        {
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            return col;
        }

        public static DataGridViewColumn AlignLeft(this DataGridViewColumn col)
        {
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            return col;
        }

        public static DataGridViewColumn AlignCenter(this DataGridViewColumn col)
        {
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            return col;
        }

        public static DataGridViewColumn Visible(this DataGridViewColumn col, bool visible = true)
        {
            col.Visible = visible;
            return col;
        }

        public static string FormatString(DGVColumnFormats format)
        {
            switch (format)
            {
                case DGVColumnFormats.Decimal:
                    return "#,0.00;-#,0.00;''";
                case DGVColumnFormats.Decimal_0:
                    return "#,0.00;-#,0.00;0.00";
                case DGVColumnFormats.Decimal_3:
                    return "#,0.000;-#,0.000;''";
                case DGVColumnFormats.Decimal_3_0:
                    return "#,0.000;-#,0.000;0.000";
                case DGVColumnFormats.Decimal_4:
                    return "#,0.0000;-#,0.0000;''";
                case DGVColumnFormats.Decimal_4_0:
                    return "#,0.0000;-#,0.0000;0.0000";
                case DGVColumnFormats.Integer:
                    return "#,0;-#,0;''";
                case DGVColumnFormats.Integer_Set:
                    return "#,0;'';''";
                case DGVColumnFormats.Integer_0:
                    return "#,0;-#,0;0";
                case DGVColumnFormats.Percentage:
                    return "#,0.00 %;-#,0.00 %;''";
                case DGVColumnFormats.Percentage_0:
                    return "#,0.00 %;-#,0.00 %;0.00 %";
                case DGVColumnFormats.Percentage_Int:
                    return "#,0 %;-#,0 %;''";
                case DGVColumnFormats.Percentage_Int_0:
                    return "#,0 %;-#,0 %;0 %";
                case DGVColumnFormats.Clean_Date:
                    return "d MMM yyyy";
                case DGVColumnFormats.Precise_Date:
                    return "dd/MM/yyyy HH:mm";
                case DGVColumnFormats.Standard_Date:
                    return "dd/MM/yyyy";
                case DGVColumnFormats.Not_Specified:
                case DGVColumnFormats.None:
                default:
                    return "";
            }
        }

        public static DataGridViewColumn CellFormat(this DataGridViewColumn col, DGVColumnFormats format = DGVColumnFormats.Not_Specified)
        {
            col.DefaultCellStyle.Format = FormatString(format);

            return col;
        }

        public static DataGridViewRow GetSelectedRow(this DataGridView dgv)
        {
            try
            {
                if (dgv.CurrentCell != null)
                {
                    return dgv.CurrentCell.OwningRow;
                }
                else if (dgv.SelectedRows.Count > 0)
                {
                    return dgv.SelectedRows[0];
                }
                else if (dgv.SelectedCells.Count > 0)
                {
                    return dgv.SelectedCells[0].OwningRow;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public static object GetRow(this DataGridView dgv)
        {
            try
            {
                if (dgv.DataSource is BindingSource)
                {
                    BindingSource Source = (BindingSource)dgv.DataSource;
                    return Source.Current;
                }
                else
                {
                    DataGridViewRow Row = dgv.GetSelectedRow();
                    if (Row is null) return null;

                    return Row.DataBoundItem;
                }
            }
            catch
            {
                return null;
            }
        }

        public static DataGridViewRow GetDGVRow(this DataGridView dgv)
        {
            if (dgv.SelectedRows is null
                || dgv.SelectedRows.Count <= 0
                || dgv.SelectedRows[0].Index < 0) return null;

            return dgv.Rows[dgv.SelectedRows[0].Index];
        }

        public static T CellValue<T>(this DataGridViewRow row, int col_index, T default_value = default(T))
        {
            try
            {
                if (row.Cells[col_index].Value == null
                    || row.Cells[col_index].Value == DBNull.Value)
                {
                    return default_value;
                }

                return (T)Convert.ChangeType(row.Cells[col_index].Value, typeof(T));
            }
            catch
            {
                return default(T);
            }
        }

        public static T CellValue<T>(this DataGridViewRow row, string col_name, T default_value = default(T))
        {
            try
            {
                if (row.Cells[col_name].Value == null
                    || row.Cells[col_name].Value == DBNull.Value)
                {
                    return default_value;
                }

                return (T)Convert.ChangeType(row.Cells[col_name].Value, typeof(T));
            }
            catch
            {
                return default(T);
            }
        }

        public class DataGridViewDisableButtonColumn : DataGridViewButtonColumn
        {
            public DataGridViewDisableButtonColumn()
            {
                this.CellTemplate = new DataGridViewDisableButtonCell();
            }
        }

        public class DataGridViewDisableButtonCell : DataGridViewButtonCell
        {
            private bool enabledValue;
            public bool Enabled
            {
                get
                {
                    return enabledValue;
                }
                set
                {
                    if (enabledValue == value) return;
                    enabledValue = value;
                    // force the cell to be re-painted
                    if (DataGridView != null) DataGridView.InvalidateCell(this);
                }
            }

            // Override the Clone method so that the Enabled property is copied.
            public override object Clone()
            {
                var cell = (DataGridViewDisableButtonCell)base.Clone();
                cell.Enabled = Enabled;
                return cell;
            }

            // By default, enable the button cell.
            public DataGridViewDisableButtonCell()
            {
                this.enabledValue = true;
            }

            protected override void Paint(
                Graphics graphics,
                Rectangle clipBounds,
                Rectangle cellBounds,
                int rowIndex,
                DataGridViewElementStates elementState,
                object value,
                object formattedValue,
                string errorText,
                DataGridViewCellStyle cellStyle,
                DataGridViewAdvancedBorderStyle advancedBorderStyle,
                DataGridViewPaintParts paintParts)
            {
                // The button cell is disabled, so paint the border, background, and disabled button for the cell. 
                if (!enabledValue)
                {
                    var currentContext = BufferedGraphicsManager.Current;

                    using (var myBuffer = currentContext.Allocate(graphics, cellBounds))
                    {
                        // Draw the cell background, if specified. 
                        if ((paintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
                        {
                            using (var cellBackground = new SolidBrush(cellStyle.BackColor))
                            {
                                myBuffer.Graphics.FillRectangle(cellBackground, cellBounds);
                            }
                        }

                        // Draw the cell borders, if specified. 
                        if ((paintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border)
                        {
                            PaintBorder(myBuffer.Graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
                        }

                        // Calculate the area in which to draw the button.
                        var buttonArea = cellBounds;
                        var buttonAdjustment = BorderWidths(advancedBorderStyle);
                        buttonArea.X += buttonAdjustment.X;
                        buttonArea.Y += buttonAdjustment.Y;
                        buttonArea.Height -= buttonAdjustment.Height;
                        buttonArea.Width -= buttonAdjustment.Width;

                        // Draw the disabled button.                
                        ButtonRenderer.DrawButton(myBuffer.Graphics, buttonArea, PushButtonState.Disabled);

                        // Draw the disabled button text.  
                        var formattedValueString = FormattedValue as string;
                        if (formattedValueString != null)
                        {
                            TextRenderer.DrawText(myBuffer.Graphics, formattedValueString, DataGridView.Font, buttonArea, SystemColors.GrayText, TextFormatFlags.PreserveGraphicsTranslateTransform | TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                        }

                        myBuffer.Render();
                    }
                }
                else
                {
                    // The button cell is enabled, so let the base class handle the painting. 
                    base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
                }
            }
        }
    }
}
