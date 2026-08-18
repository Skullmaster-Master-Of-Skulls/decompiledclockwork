using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace System.Windows.Forms
{
	// Token: 0x0200017D RID: 381
	public class DataGridBoolColumn : DataGridColumnStyle
	{
		// Token: 0x060015E1 RID: 5601 RVA: 0x0004F100 File Offset: 0x0004D300
		public DataGridBoolColumn()
		{
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x0004F150 File Offset: 0x0004D350
		public DataGridBoolColumn(PropertyDescriptor prop) : base(prop)
		{
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x0004F1A0 File Offset: 0x0004D3A0
		public DataGridBoolColumn(PropertyDescriptor prop, bool isDefault) : base(prop, isDefault)
		{
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060015E4 RID: 5604 RVA: 0x0004F1F1 File Offset: 0x0004D3F1
		// (set) Token: 0x060015E5 RID: 5605 RVA: 0x0004F1F9 File Offset: 0x0004D3F9
		[TypeConverter(typeof(StringConverter))]
		[DefaultValue(true)]
		public object TrueValue
		{
			get
			{
				return this.trueValue;
			}
			set
			{
				if (!this.trueValue.Equals(value))
				{
					this.trueValue = value;
					this.OnTrueValueChanged(EventArgs.Empty);
					this.Invalidate();
				}
			}
		}

		// Token: 0x140000E1 RID: 225
		// (add) Token: 0x060015E6 RID: 5606 RVA: 0x0004F221 File Offset: 0x0004D421
		// (remove) Token: 0x060015E7 RID: 5607 RVA: 0x0004F234 File Offset: 0x0004D434
		public event EventHandler TrueValueChanged
		{
			add
			{
				base.Events.AddHandler(DataGridBoolColumn.EventTrueValue, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridBoolColumn.EventTrueValue, value);
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x0004F247 File Offset: 0x0004D447
		// (set) Token: 0x060015E9 RID: 5609 RVA: 0x0004F24F File Offset: 0x0004D44F
		[TypeConverter(typeof(StringConverter))]
		[DefaultValue(false)]
		public object FalseValue
		{
			get
			{
				return this.falseValue;
			}
			set
			{
				if (!this.falseValue.Equals(value))
				{
					this.falseValue = value;
					this.OnFalseValueChanged(EventArgs.Empty);
					this.Invalidate();
				}
			}
		}

		// Token: 0x140000E2 RID: 226
		// (add) Token: 0x060015EA RID: 5610 RVA: 0x0004F277 File Offset: 0x0004D477
		// (remove) Token: 0x060015EB RID: 5611 RVA: 0x0004F28A File Offset: 0x0004D48A
		public event EventHandler FalseValueChanged
		{
			add
			{
				base.Events.AddHandler(DataGridBoolColumn.EventFalseValue, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridBoolColumn.EventFalseValue, value);
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060015EC RID: 5612 RVA: 0x0004F29D File Offset: 0x0004D49D
		// (set) Token: 0x060015ED RID: 5613 RVA: 0x0004F2A5 File Offset: 0x0004D4A5
		[TypeConverter(typeof(StringConverter))]
		public object NullValue
		{
			get
			{
				return this.nullValue;
			}
			set
			{
				if (!this.nullValue.Equals(value))
				{
					this.nullValue = value;
					this.OnFalseValueChanged(EventArgs.Empty);
					this.Invalidate();
				}
			}
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x0004F2CD File Offset: 0x0004D4CD
		protected internal override void ConcedeFocus()
		{
			base.ConcedeFocus();
			this.isSelected = false;
			this.isEditing = false;
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x0004F2E4 File Offset: 0x0004D4E4
		private Rectangle GetCheckBoxBounds(Rectangle bounds, bool alignToRight)
		{
			if (alignToRight)
			{
				return new Rectangle(bounds.X + (bounds.Width - DataGridBoolColumn.idealCheckSize) / 2, bounds.Y + (bounds.Height - DataGridBoolColumn.idealCheckSize) / 2, (bounds.Width < DataGridBoolColumn.idealCheckSize) ? bounds.Width : DataGridBoolColumn.idealCheckSize, DataGridBoolColumn.idealCheckSize);
			}
			return new Rectangle(Math.Max(0, bounds.X + (bounds.Width - DataGridBoolColumn.idealCheckSize) / 2), Math.Max(0, bounds.Y + (bounds.Height - DataGridBoolColumn.idealCheckSize) / 2), (bounds.Width < DataGridBoolColumn.idealCheckSize) ? bounds.Width : DataGridBoolColumn.idealCheckSize, DataGridBoolColumn.idealCheckSize);
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x0004F3AC File Offset: 0x0004D5AC
		protected internal override object GetColumnValueAtRow(CurrencyManager lm, int row)
		{
			object columnValueAtRow = base.GetColumnValueAtRow(lm, row);
			object result = Convert.DBNull;
			if (columnValueAtRow.Equals(this.trueValue))
			{
				result = true;
			}
			else if (columnValueAtRow.Equals(this.falseValue))
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x0004F3F8 File Offset: 0x0004D5F8
		private bool IsReadOnly()
		{
			bool flag = this.ReadOnly;
			if (this.DataGridTableStyle != null)
			{
				flag = (flag || this.DataGridTableStyle.ReadOnly);
				if (this.DataGridTableStyle.DataGrid != null)
				{
					flag = (flag || this.DataGridTableStyle.DataGrid.ReadOnly);
				}
			}
			return flag;
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x0004F44C File Offset: 0x0004D64C
		protected internal override void SetColumnValueAtRow(CurrencyManager lm, int row, object value)
		{
			object value2 = null;
			if (true.Equals(value))
			{
				value2 = this.TrueValue;
			}
			else if (false.Equals(value))
			{
				value2 = this.FalseValue;
			}
			else if (Convert.IsDBNull(value))
			{
				value2 = this.NullValue;
			}
			this.currentValue = value2;
			base.SetColumnValueAtRow(lm, row, value2);
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x0004F4A4 File Offset: 0x0004D6A4
		protected internal override Size GetPreferredSize(Graphics g, object value)
		{
			return new Size(DataGridBoolColumn.idealCheckSize + 2, DataGridBoolColumn.idealCheckSize + 2);
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x0004F4B9 File Offset: 0x0004D6B9
		protected internal override int GetMinimumHeight()
		{
			return DataGridBoolColumn.idealCheckSize + 2;
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x0004F4B9 File Offset: 0x0004D6B9
		protected internal override int GetPreferredHeight(Graphics g, object value)
		{
			return DataGridBoolColumn.idealCheckSize + 2;
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x0004F4C2 File Offset: 0x0004D6C2
		protected internal override void Abort(int rowNum)
		{
			this.isSelected = false;
			this.isEditing = false;
			this.Invalidate();
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x0004F4D8 File Offset: 0x0004D6D8
		protected internal override bool Commit(CurrencyManager dataSource, int rowNum)
		{
			this.isSelected = false;
			this.Invalidate();
			if (!this.isEditing)
			{
				return true;
			}
			this.SetColumnValueAtRow(dataSource, rowNum, this.currentValue);
			this.isEditing = false;
			return true;
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x0004F508 File Offset: 0x0004D708
		protected internal override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly, string displayText, bool cellIsVisible)
		{
			this.isSelected = true;
			DataGrid dataGrid = this.DataGridTableStyle.DataGrid;
			if (!dataGrid.Focused)
			{
				dataGrid.FocusInternal();
			}
			if (!readOnly && !this.IsReadOnly())
			{
				this.editingRow = rowNum;
				this.currentValue = this.GetColumnValueAtRow(source, rowNum);
			}
			base.Invalidate();
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x0004F55E File Offset: 0x0004D75E
		internal override bool KeyPress(int rowNum, Keys keyData)
		{
			if (this.isSelected && this.editingRow == rowNum && !this.IsReadOnly() && (keyData & Keys.KeyCode) == Keys.Space)
			{
				this.ToggleValue();
				this.Invalidate();
				return true;
			}
			return base.KeyPress(rowNum, keyData);
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x0004F59A File Offset: 0x0004D79A
		internal override bool MouseDown(int rowNum, int x, int y)
		{
			base.MouseDown(rowNum, x, y);
			if (this.isSelected && this.editingRow == rowNum && !this.IsReadOnly())
			{
				this.ToggleValue();
				this.Invalidate();
				return true;
			}
			return false;
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x0004F5D0 File Offset: 0x0004D7D0
		private void OnTrueValueChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridBoolColumn.EventTrueValue] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x0004F600 File Offset: 0x0004D800
		private void OnFalseValueChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridBoolColumn.EventFalseValue] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x0004F630 File Offset: 0x0004D830
		private void OnAllowNullChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridBoolColumn.EventAllowNull] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x0004F65E File Offset: 0x0004D85E
		protected internal override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum)
		{
			this.Paint(g, bounds, source, rowNum, false);
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x0004F66C File Offset: 0x0004D86C
		protected internal override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, bool alignToRight)
		{
			this.Paint(g, bounds, source, rowNum, this.DataGridTableStyle.BackBrush, this.DataGridTableStyle.ForeBrush, alignToRight);
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x0004F694 File Offset: 0x0004D894
		protected internal override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, Brush backBrush, Brush foreBrush, bool alignToRight)
		{
			object obj = (this.isEditing && this.editingRow == rowNum) ? this.currentValue : this.GetColumnValueAtRow(source, rowNum);
			ButtonState buttonState = ButtonState.Inactive;
			if (!Convert.IsDBNull(obj))
			{
				buttonState = (((bool)obj) ? ButtonState.Checked : ButtonState.Normal);
			}
			Rectangle checkBoxBounds = this.GetCheckBoxBounds(bounds, alignToRight);
			Region clip = g.Clip;
			g.ExcludeClip(checkBoxBounds);
			Brush brush = this.DataGridTableStyle.IsDefault ? this.DataGridTableStyle.DataGrid.SelectionBackBrush : this.DataGridTableStyle.SelectionBackBrush;
			if (this.isSelected && this.editingRow == rowNum && !this.IsReadOnly())
			{
				g.FillRectangle(brush, bounds);
			}
			else
			{
				g.FillRectangle(backBrush, bounds);
			}
			g.Clip = clip;
			if (buttonState == ButtonState.Inactive)
			{
				ControlPaint.DrawMixedCheckBox(g, checkBoxBounds, ButtonState.Checked);
			}
			else
			{
				ControlPaint.DrawCheckBox(g, checkBoxBounds, buttonState);
			}
			if (this.IsReadOnly() && this.isSelected && source.Position == rowNum)
			{
				bounds.Inflate(-1, -1);
				Pen pen = new Pen(brush);
				pen.DashStyle = DashStyle.Dash;
				g.DrawRectangle(pen, bounds);
				pen.Dispose();
				bounds.Inflate(1, 1);
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x0004F7C8 File Offset: 0x0004D9C8
		// (set) Token: 0x06001602 RID: 5634 RVA: 0x0004F7D0 File Offset: 0x0004D9D0
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("DataGridBoolColumnAllowNullValue")]
		public bool AllowNull
		{
			get
			{
				return this.allowNull;
			}
			set
			{
				if (this.allowNull != value)
				{
					this.allowNull = value;
					if (!value && Convert.IsDBNull(this.currentValue))
					{
						this.currentValue = false;
						this.Invalidate();
					}
					this.OnAllowNullChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x140000E3 RID: 227
		// (add) Token: 0x06001603 RID: 5635 RVA: 0x0004F80F File Offset: 0x0004DA0F
		// (remove) Token: 0x06001604 RID: 5636 RVA: 0x0004F822 File Offset: 0x0004DA22
		public event EventHandler AllowNullChanged
		{
			add
			{
				base.Events.AddHandler(DataGridBoolColumn.EventAllowNull, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridBoolColumn.EventAllowNull, value);
			}
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x0004F835 File Offset: 0x0004DA35
		protected internal override void EnterNullValue()
		{
			if (!this.AllowNull || this.IsReadOnly())
			{
				return;
			}
			if (this.currentValue != Convert.DBNull)
			{
				this.currentValue = Convert.DBNull;
				this.Invalidate();
			}
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x0004F866 File Offset: 0x0004DA66
		private void ResetNullValue()
		{
			this.NullValue = Convert.DBNull;
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x0004F873 File Offset: 0x0004DA73
		private bool ShouldSerializeNullValue()
		{
			return this.nullValue != Convert.DBNull;
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x0004F888 File Offset: 0x0004DA88
		private void ToggleValue()
		{
			if (this.currentValue is bool && !(bool)this.currentValue)
			{
				this.currentValue = true;
			}
			else if (this.AllowNull)
			{
				if (Convert.IsDBNull(this.currentValue))
				{
					this.currentValue = false;
				}
				else
				{
					this.currentValue = Convert.DBNull;
				}
			}
			else
			{
				this.currentValue = false;
			}
			this.isEditing = true;
			this.DataGridTableStyle.DataGrid.ColumnStartedEditing(Rectangle.Empty);
		}

		// Token: 0x04000A14 RID: 2580
		private static readonly int idealCheckSize = 14;

		// Token: 0x04000A15 RID: 2581
		private bool isEditing;

		// Token: 0x04000A16 RID: 2582
		private bool isSelected;

		// Token: 0x04000A17 RID: 2583
		private bool allowNull = true;

		// Token: 0x04000A18 RID: 2584
		private int editingRow = -1;

		// Token: 0x04000A19 RID: 2585
		private object currentValue = Convert.DBNull;

		// Token: 0x04000A1A RID: 2586
		private object trueValue = true;

		// Token: 0x04000A1B RID: 2587
		private object falseValue = false;

		// Token: 0x04000A1C RID: 2588
		private object nullValue = Convert.DBNull;

		// Token: 0x04000A1D RID: 2589
		private static readonly object EventTrueValue = new object();

		// Token: 0x04000A1E RID: 2590
		private static readonly object EventFalseValue = new object();

		// Token: 0x04000A1F RID: 2591
		private static readonly object EventAllowNull = new object();
	}
}
