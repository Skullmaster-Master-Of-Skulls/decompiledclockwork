using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x0200021F RID: 543
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	public class DataGridViewTextBoxEditingControl : TextBox, IDataGridViewEditingControl
	{
		// Token: 0x0600234F RID: 9039 RVA: 0x000A8582 File Offset: 0x000A6782
		public DataGridViewTextBoxEditingControl()
		{
			base.TabStop = false;
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x000A8591 File Offset: 0x000A6791
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level5)
			{
				return new DataGridViewTextBoxEditingControlAccessibleObjectLevel5(this);
			}
			if (AccessibilityImprovements.Level3)
			{
				return new DataGridViewTextBoxEditingControlAccessibleObject(this);
			}
			if (AccessibilityImprovements.Level2)
			{
				return new DataGridViewEditingControlAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06002351 RID: 9041 RVA: 0x000A85C3 File Offset: 0x000A67C3
		// (set) Token: 0x06002352 RID: 9042 RVA: 0x000A85CB File Offset: 0x000A67CB
		public virtual DataGridView EditingControlDataGridView
		{
			get
			{
				return this.dataGridView;
			}
			set
			{
				this.dataGridView = value;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06002353 RID: 9043 RVA: 0x000A85D4 File Offset: 0x000A67D4
		// (set) Token: 0x06002354 RID: 9044 RVA: 0x000A85DD File Offset: 0x000A67DD
		public virtual object EditingControlFormattedValue
		{
			get
			{
				return this.GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting);
			}
			set
			{
				this.Text = (string)value;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06002355 RID: 9045 RVA: 0x000A85EB File Offset: 0x000A67EB
		// (set) Token: 0x06002356 RID: 9046 RVA: 0x000A85F3 File Offset: 0x000A67F3
		public virtual int EditingControlRowIndex
		{
			get
			{
				return this.rowIndex;
			}
			set
			{
				this.rowIndex = value;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06002357 RID: 9047 RVA: 0x000A85FC File Offset: 0x000A67FC
		// (set) Token: 0x06002358 RID: 9048 RVA: 0x000A8604 File Offset: 0x000A6804
		public virtual bool EditingControlValueChanged
		{
			get
			{
				return this.valueChanged;
			}
			set
			{
				this.valueChanged = value;
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06002359 RID: 9049 RVA: 0x0003071E File Offset: 0x0002E91E
		public virtual Cursor EditingPanelCursor
		{
			get
			{
				return Cursors.Default;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x0600235A RID: 9050 RVA: 0x000A860D File Offset: 0x000A680D
		public virtual bool RepositionEditingControlOnValueChange
		{
			get
			{
				return this.repositionOnValueChange;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x0600235B RID: 9051 RVA: 0x000A8615 File Offset: 0x000A6815
		internal override bool SupportsUiaProviders
		{
			get
			{
				return AccessibilityImprovements.Level3;
			}
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x000A861C File Offset: 0x000A681C
		public virtual void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
		{
			this.Font = dataGridViewCellStyle.Font;
			if (dataGridViewCellStyle.BackColor.A < 255)
			{
				Color backColor = Color.FromArgb(255, dataGridViewCellStyle.BackColor);
				this.BackColor = backColor;
				this.dataGridView.EditingPanel.BackColor = backColor;
			}
			else
			{
				this.BackColor = dataGridViewCellStyle.BackColor;
			}
			this.ForeColor = dataGridViewCellStyle.ForeColor;
			if (dataGridViewCellStyle.WrapMode == DataGridViewTriState.True)
			{
				base.WordWrap = true;
			}
			base.TextAlign = DataGridViewTextBoxEditingControl.TranslateAlignment(dataGridViewCellStyle.Alignment);
			this.repositionOnValueChange = (dataGridViewCellStyle.WrapMode == DataGridViewTriState.True && (dataGridViewCellStyle.Alignment & DataGridViewTextBoxEditingControl.anyTop) == DataGridViewContentAlignment.NotSet);
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x000A86D0 File Offset: 0x000A68D0
		public virtual bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
		{
			Keys keys = keyData & Keys.KeyCode;
			if (keys != Keys.Return)
			{
				switch (keys)
				{
				case Keys.Prior:
				case Keys.Next:
					if (this.valueChanged)
					{
						return true;
					}
					break;
				case Keys.End:
				case Keys.Home:
					if (this.SelectionLength != this.Text.Length)
					{
						return true;
					}
					break;
				case Keys.Left:
					if ((this.RightToLeft == RightToLeft.No && (this.SelectionLength != 0 || base.SelectionStart != 0)) || (this.RightToLeft == RightToLeft.Yes && (this.SelectionLength != 0 || base.SelectionStart != this.Text.Length)))
					{
						return true;
					}
					break;
				case Keys.Up:
					if (this.Text.IndexOf("\r\n") >= 0 && base.SelectionStart + this.SelectionLength >= this.Text.IndexOf("\r\n"))
					{
						return true;
					}
					break;
				case Keys.Right:
					if ((this.RightToLeft == RightToLeft.No && (this.SelectionLength != 0 || base.SelectionStart != this.Text.Length)) || (this.RightToLeft == RightToLeft.Yes && (this.SelectionLength != 0 || base.SelectionStart != 0)))
					{
						return true;
					}
					break;
				case Keys.Down:
				{
					int startIndex = base.SelectionStart + this.SelectionLength;
					if (this.Text.IndexOf("\r\n", startIndex) != -1)
					{
						return true;
					}
					break;
				}
				case Keys.Delete:
					if (this.SelectionLength > 0 || base.SelectionStart < this.Text.Length)
					{
						return true;
					}
					break;
				}
			}
			else if ((keyData & (Keys.Shift | Keys.Control | Keys.Alt)) == Keys.Shift && this.Multiline && base.AcceptsReturn)
			{
				return true;
			}
			return !dataGridViewWantsInputKey;
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x0009B7F9 File Offset: 0x000999F9
		public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
		{
			return this.Text;
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x000A8877 File Offset: 0x000A6A77
		public virtual void PrepareEditingControlForEdit(bool selectAll)
		{
			if (selectAll)
			{
				base.SelectAll();
				return;
			}
			base.SelectionStart = this.Text.Length;
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x000A8894 File Offset: 0x000A6A94
		private void NotifyDataGridViewOfValueChange()
		{
			this.valueChanged = true;
			this.dataGridView.NotifyCurrentCellDirty(true);
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x000A88A9 File Offset: 0x000A6AA9
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			if (AccessibilityImprovements.Level3)
			{
				base.AccessibilityObject.RaiseAutomationEvent(20005);
			}
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x000A88CA File Offset: 0x000A6ACA
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			this.dataGridView.OnMouseWheelInternal(e);
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x000A88D8 File Offset: 0x000A6AD8
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			this.NotifyDataGridViewOfValueChange();
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x000A88E8 File Offset: 0x000A6AE8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessKeyEventArgs(ref Message m)
		{
			Keys keys = (Keys)((int)m.WParam);
			if (keys != Keys.LineFeed)
			{
				if (keys != Keys.Return)
				{
					if (keys == Keys.A)
					{
						if (m.Msg == 256 && Control.ModifierKeys == Keys.Control)
						{
							base.SelectAll();
							return true;
						}
					}
				}
				else if (m.Msg == 258 && (Control.ModifierKeys != Keys.Shift || !this.Multiline || !base.AcceptsReturn))
				{
					return true;
				}
			}
			else if (m.Msg == 258 && Control.ModifierKeys == Keys.Control && this.Multiline && base.AcceptsReturn)
			{
				return true;
			}
			return base.ProcessKeyEventArgs(ref m);
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x000A8990 File Offset: 0x000A6B90
		private static HorizontalAlignment TranslateAlignment(DataGridViewContentAlignment align)
		{
			if ((align & DataGridViewTextBoxEditingControl.anyRight) != DataGridViewContentAlignment.NotSet)
			{
				return HorizontalAlignment.Right;
			}
			if ((align & DataGridViewTextBoxEditingControl.anyCenter) != DataGridViewContentAlignment.NotSet)
			{
				return HorizontalAlignment.Center;
			}
			return HorizontalAlignment.Left;
		}

		// Token: 0x04000E8D RID: 3725
		private static readonly DataGridViewContentAlignment anyTop = (DataGridViewContentAlignment)7;

		// Token: 0x04000E8E RID: 3726
		private static readonly DataGridViewContentAlignment anyRight = (DataGridViewContentAlignment)1092;

		// Token: 0x04000E8F RID: 3727
		private static readonly DataGridViewContentAlignment anyCenter = (DataGridViewContentAlignment)546;

		// Token: 0x04000E90 RID: 3728
		private DataGridView dataGridView;

		// Token: 0x04000E91 RID: 3729
		private bool valueChanged;

		// Token: 0x04000E92 RID: 3730
		private bool repositionOnValueChange;

		// Token: 0x04000E93 RID: 3731
		private int rowIndex;
	}
}
