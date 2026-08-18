using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x0200018E RID: 398
	public class DataGridTextBoxColumn : DataGridColumnStyle
	{
		// Token: 0x0600184A RID: 6218 RVA: 0x0005743D File Offset: 0x0005563D
		public DataGridTextBoxColumn() : this(null, null)
		{
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x00057447 File Offset: 0x00055647
		public DataGridTextBoxColumn(PropertyDescriptor prop) : this(prop, null, false)
		{
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x00057452 File Offset: 0x00055652
		public DataGridTextBoxColumn(PropertyDescriptor prop, string format) : this(prop, format, false)
		{
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x00057460 File Offset: 0x00055660
		public DataGridTextBoxColumn(PropertyDescriptor prop, string format, bool isDefault) : base(prop, isDefault)
		{
			this.edit = new DataGridTextBox();
			this.edit.BorderStyle = BorderStyle.None;
			this.edit.Multiline = true;
			this.edit.AcceptsReturn = true;
			this.edit.Visible = false;
			this.Format = format;
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x000574CC File Offset: 0x000556CC
		public DataGridTextBoxColumn(PropertyDescriptor prop, bool isDefault) : this(prop, null, isDefault)
		{
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x0600184F RID: 6223 RVA: 0x000574D7 File Offset: 0x000556D7
		[Browsable(false)]
		public virtual TextBox TextBox
		{
			get
			{
				return this.edit;
			}
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x000574DF File Offset: 0x000556DF
		internal override bool KeyPress(int rowNum, Keys keyData)
		{
			return this.edit.IsInEditOrNavigateMode && base.KeyPress(rowNum, keyData);
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x000574F8 File Offset: 0x000556F8
		protected override void SetDataGridInColumn(DataGrid value)
		{
			base.SetDataGridInColumn(value);
			if (this.edit.ParentInternal != null)
			{
				this.edit.ParentInternal.Controls.Remove(this.edit);
			}
			if (value != null)
			{
				value.Controls.Add(this.edit);
			}
			this.edit.SetDataGrid(value);
		}

		// Token: 0x1700057A RID: 1402
		// (set) Token: 0x06001852 RID: 6226 RVA: 0x00057554 File Offset: 0x00055754
		[SRDescription("FormatControlFormatDescr")]
		[DefaultValue(null)]
		public override PropertyDescriptor PropertyDescriptor
		{
			set
			{
				base.PropertyDescriptor = value;
				if (this.PropertyDescriptor != null && this.PropertyDescriptor.PropertyType != typeof(object))
				{
					this.typeConverter = TypeDescriptor.GetConverter(this.PropertyDescriptor.PropertyType);
					this.parseMethod = this.PropertyDescriptor.PropertyType.GetMethod("Parse", new Type[]
					{
						typeof(string),
						typeof(IFormatProvider)
					});
				}
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001853 RID: 6227 RVA: 0x000575DD File Offset: 0x000557DD
		// (set) Token: 0x06001854 RID: 6228 RVA: 0x000575E8 File Offset: 0x000557E8
		[DefaultValue(null)]
		[Editor("System.Windows.Forms.Design.DataGridColumnStyleFormatEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string Format
		{
			get
			{
				return this.format;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (this.format == null || !this.format.Equals(value))
				{
					this.format = value;
					if (this.format.Length == 0 && this.typeConverter != null && !this.typeConverter.CanConvertFrom(typeof(string)))
					{
						this.ReadOnly = true;
					}
					this.Invalidate();
				}
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001855 RID: 6229 RVA: 0x00057655 File Offset: 0x00055855
		// (set) Token: 0x06001856 RID: 6230 RVA: 0x0005765D File Offset: 0x0005585D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IFormatProvider FormatInfo
		{
			get
			{
				return this.formatInfo;
			}
			set
			{
				if (this.formatInfo == null || !this.formatInfo.Equals(value))
				{
					this.formatInfo = value;
				}
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001857 RID: 6231 RVA: 0x0005767C File Offset: 0x0005587C
		// (set) Token: 0x06001858 RID: 6232 RVA: 0x00057684 File Offset: 0x00055884
		public override bool ReadOnly
		{
			get
			{
				return base.ReadOnly;
			}
			set
			{
				if (!value && (this.format == null || this.format.Length == 0) && this.typeConverter != null && !this.typeConverter.CanConvertFrom(typeof(string)))
				{
					return;
				}
				base.ReadOnly = value;
			}
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x000072B6 File Offset: 0x000054B6
		private void DebugOut(string s)
		{
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x000576D0 File Offset: 0x000558D0
		protected internal override void ConcedeFocus()
		{
			this.edit.Bounds = Rectangle.Empty;
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x000576E4 File Offset: 0x000558E4
		protected void HideEditBox()
		{
			bool focused = this.edit.Focused;
			this.edit.Visible = false;
			if (focused && this.DataGridTableStyle != null && this.DataGridTableStyle.DataGrid != null && this.DataGridTableStyle.DataGrid.CanFocus)
			{
				this.DataGridTableStyle.DataGrid.FocusInternal();
			}
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x00057744 File Offset: 0x00055944
		protected internal override void UpdateUI(CurrencyManager source, int rowNum, string displayText)
		{
			this.edit.Text = this.GetText(this.GetColumnValueAtRow(source, rowNum));
			if (!this.edit.ReadOnly && displayText != null)
			{
				this.edit.Text = displayText;
			}
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x0005777B File Offset: 0x0005597B
		protected void EndEdit()
		{
			this.edit.IsInEditOrNavigateMode = true;
			this.DebugOut("Ending Edit");
			this.Invalidate();
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x0005779C File Offset: 0x0005599C
		protected internal override Size GetPreferredSize(Graphics g, object value)
		{
			Size result = Size.Ceiling(g.MeasureString(this.GetText(value), this.DataGridTableStyle.DataGrid.Font));
			result.Width += this.xMargin * 2 + this.DataGridTableStyle.GridLineWidth;
			result.Height += this.yMargin;
			return result;
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x00057803 File Offset: 0x00055A03
		protected internal override int GetMinimumHeight()
		{
			return base.FontHeight + this.yMargin + 3;
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x00057814 File Offset: 0x00055A14
		protected internal override int GetPreferredHeight(Graphics g, object value)
		{
			int num = 0;
			int num2 = 0;
			string text = this.GetText(value);
			while (num != -1 && num < text.Length)
			{
				num = text.IndexOf("\r\n", num + 1);
				num2++;
			}
			return base.FontHeight * num2 + this.yMargin;
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x0005785E File Offset: 0x00055A5E
		protected internal override void Abort(int rowNum)
		{
			this.RollBack();
			this.HideEditBox();
			this.EndEdit();
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x00057874 File Offset: 0x00055A74
		protected internal override void EnterNullValue()
		{
			if (this.ReadOnly)
			{
				return;
			}
			if (!this.edit.Visible)
			{
				return;
			}
			if (!this.edit.IsInEditOrNavigateMode)
			{
				return;
			}
			this.edit.Text = this.NullText;
			this.edit.IsInEditOrNavigateMode = false;
			if (this.DataGridTableStyle != null && this.DataGridTableStyle.DataGrid != null)
			{
				this.DataGridTableStyle.DataGrid.ColumnStartedEditing(this.edit.Bounds);
			}
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x000578F4 File Offset: 0x00055AF4
		protected internal override bool Commit(CurrencyManager dataSource, int rowNum)
		{
			this.edit.Bounds = Rectangle.Empty;
			if (this.edit.IsInEditOrNavigateMode)
			{
				return true;
			}
			try
			{
				object obj = this.edit.Text;
				if (this.NullText.Equals(obj))
				{
					obj = Convert.DBNull;
					this.edit.Text = this.NullText;
				}
				else if (this.format != null && this.format.Length != 0 && this.parseMethod != null && this.FormatInfo != null)
				{
					obj = this.parseMethod.Invoke(null, new object[]
					{
						this.edit.Text,
						this.FormatInfo
					});
					if (obj is IFormattable)
					{
						this.edit.Text = ((IFormattable)obj).ToString(this.format, this.formatInfo);
					}
					else
					{
						this.edit.Text = obj.ToString();
					}
				}
				else if (this.typeConverter != null && this.typeConverter.CanConvertFrom(typeof(string)))
				{
					obj = this.typeConverter.ConvertFromString(this.edit.Text);
					this.edit.Text = this.typeConverter.ConvertToString(obj);
				}
				this.SetColumnValueAtRow(dataSource, rowNum, obj);
			}
			catch
			{
				this.RollBack();
				return false;
			}
			this.DebugOut("OnCommit completed without Exception.");
			this.EndEdit();
			return true;
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x00057A84 File Offset: 0x00055C84
		protected internal override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly, string displayText, bool cellIsVisible)
		{
			this.DebugOut("Begining Edit, rowNum :" + rowNum.ToString(CultureInfo.InvariantCulture));
			Rectangle rc = bounds;
			this.edit.ReadOnly = (readOnly || this.ReadOnly || this.DataGridTableStyle.ReadOnly);
			this.edit.Text = this.GetText(this.GetColumnValueAtRow(source, rowNum));
			if (!this.edit.ReadOnly && displayText != null)
			{
				this.DataGridTableStyle.DataGrid.ColumnStartedEditing(bounds);
				this.edit.IsInEditOrNavigateMode = false;
				this.edit.Text = displayText;
			}
			if (cellIsVisible)
			{
				bounds.Offset(this.xMargin, 2 * this.yMargin);
				bounds.Width -= this.xMargin;
				bounds.Height -= 2 * this.yMargin;
				this.DebugOut("edit bounds: " + bounds.ToString());
				this.edit.Bounds = bounds;
				this.edit.Visible = true;
				this.edit.TextAlign = this.Alignment;
			}
			else
			{
				this.edit.Bounds = Rectangle.Empty;
			}
			this.edit.RightToLeft = this.DataGridTableStyle.DataGrid.RightToLeft;
			this.edit.FocusInternal();
			this.editRow = rowNum;
			if (!this.edit.ReadOnly)
			{
				this.oldValue = this.edit.Text;
			}
			if (displayText == null)
			{
				this.edit.SelectAll();
			}
			else
			{
				int length = this.edit.Text.Length;
				this.edit.Select(length, 0);
			}
			if (this.edit.Visible)
			{
				this.DataGridTableStyle.DataGrid.Invalidate(rc);
			}
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x00057C5F File Offset: 0x00055E5F
		internal override string GetDisplayText(object value)
		{
			return this.GetText(value);
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x00057C68 File Offset: 0x00055E68
		private string GetText(object value)
		{
			if (value is DBNull)
			{
				return this.NullText;
			}
			if (this.format != null && this.format.Length != 0 && value is IFormattable)
			{
				try
				{
					return ((IFormattable)value).ToString(this.format, this.formatInfo);
				}
				catch
				{
					goto IL_84;
				}
			}
			if (this.typeConverter != null && this.typeConverter.CanConvertTo(typeof(string)))
			{
				return (string)this.typeConverter.ConvertTo(value, typeof(string));
			}
			IL_84:
			if (value == null)
			{
				return "";
			}
			return value.ToString();
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x0004F65E File Offset: 0x0004D85E
		protected internal override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum)
		{
			this.Paint(g, bounds, source, rowNum, false);
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x00057D1C File Offset: 0x00055F1C
		protected internal override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, bool alignToRight)
		{
			string text = this.GetText(this.GetColumnValueAtRow(source, rowNum));
			this.PaintText(g, bounds, text, alignToRight);
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x00057D44 File Offset: 0x00055F44
		protected internal override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, Brush backBrush, Brush foreBrush, bool alignToRight)
		{
			string text = this.GetText(this.GetColumnValueAtRow(source, rowNum));
			this.PaintText(g, bounds, text, backBrush, foreBrush, alignToRight);
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x00057D70 File Offset: 0x00055F70
		protected void PaintText(Graphics g, Rectangle bounds, string text, bool alignToRight)
		{
			this.PaintText(g, bounds, text, this.DataGridTableStyle.BackBrush, this.DataGridTableStyle.ForeBrush, alignToRight);
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x00057D94 File Offset: 0x00055F94
		protected void PaintText(Graphics g, Rectangle textBounds, string text, Brush backBrush, Brush foreBrush, bool alignToRight)
		{
			Rectangle rectangle = textBounds;
			StringFormat stringFormat = new StringFormat();
			if (alignToRight)
			{
				stringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
			}
			stringFormat.Alignment = ((this.Alignment == HorizontalAlignment.Left) ? StringAlignment.Near : ((this.Alignment == HorizontalAlignment.Center) ? StringAlignment.Center : StringAlignment.Far));
			stringFormat.FormatFlags |= StringFormatFlags.NoWrap;
			g.FillRectangle(backBrush, rectangle);
			rectangle.Offset(0, 2 * this.yMargin);
			rectangle.Height -= 2 * this.yMargin;
			g.DrawString(text, this.DataGridTableStyle.DataGrid.Font, foreBrush, rectangle, stringFormat);
			stringFormat.Dispose();
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x00057E40 File Offset: 0x00056040
		private void RollBack()
		{
			this.edit.Text = this.oldValue;
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x00057E53 File Offset: 0x00056053
		protected internal override void ReleaseHostedControl()
		{
			if (this.edit.ParentInternal != null)
			{
				this.edit.ParentInternal.Controls.Remove(this.edit);
			}
		}

		// Token: 0x04000AD5 RID: 2773
		private int xMargin = 2;

		// Token: 0x04000AD6 RID: 2774
		private int yMargin = 1;

		// Token: 0x04000AD7 RID: 2775
		private string format;

		// Token: 0x04000AD8 RID: 2776
		private TypeConverter typeConverter;

		// Token: 0x04000AD9 RID: 2777
		private IFormatProvider formatInfo;

		// Token: 0x04000ADA RID: 2778
		private MethodInfo parseMethod;

		// Token: 0x04000ADB RID: 2779
		private DataGridTextBox edit;

		// Token: 0x04000ADC RID: 2780
		private string oldValue;

		// Token: 0x04000ADD RID: 2781
		private int editRow = -1;
	}
}
