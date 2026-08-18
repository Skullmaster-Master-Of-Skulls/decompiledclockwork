using System;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x020001CB RID: 459
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	public class DataGridViewComboBoxEditingControl : ComboBox, IDataGridViewEditingControl
	{
		// Token: 0x06002040 RID: 8256 RVA: 0x0009B6A3 File Offset: 0x000998A3
		public DataGridViewComboBoxEditingControl()
		{
			base.TabStop = false;
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x0009B6B2 File Offset: 0x000998B2
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level3)
			{
				return new DataGridViewComboBoxEditingControlAccessibleObject(this);
			}
			if (AccessibilityImprovements.Level2)
			{
				return new DataGridViewEditingControlAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06002042 RID: 8258 RVA: 0x0009B6D6 File Offset: 0x000998D6
		// (set) Token: 0x06002043 RID: 8259 RVA: 0x0009B6DE File Offset: 0x000998DE
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

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06002044 RID: 8260 RVA: 0x0009B6E7 File Offset: 0x000998E7
		// (set) Token: 0x06002045 RID: 8261 RVA: 0x0009B6F0 File Offset: 0x000998F0
		public virtual object EditingControlFormattedValue
		{
			get
			{
				return this.GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting);
			}
			set
			{
				string text = value as string;
				if (text != null)
				{
					this.Text = text;
					if (string.Compare(text, this.Text, true, CultureInfo.CurrentCulture) != 0)
					{
						this.SelectedIndex = -1;
					}
				}
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06002046 RID: 8262 RVA: 0x0009B729 File Offset: 0x00099929
		// (set) Token: 0x06002047 RID: 8263 RVA: 0x0009B731 File Offset: 0x00099931
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

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06002048 RID: 8264 RVA: 0x0009B73A File Offset: 0x0009993A
		// (set) Token: 0x06002049 RID: 8265 RVA: 0x0009B742 File Offset: 0x00099942
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

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x0600204A RID: 8266 RVA: 0x0003071E File Offset: 0x0002E91E
		public virtual Cursor EditingPanelCursor
		{
			get
			{
				return Cursors.Default;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x0600204B RID: 8267 RVA: 0x00011A20 File Offset: 0x0000FC20
		public virtual bool RepositionEditingControlOnValueChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x0009B74C File Offset: 0x0009994C
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
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x0009B7BD File Offset: 0x000999BD
		public virtual bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
		{
			return (keyData & Keys.KeyCode) == Keys.Down || (keyData & Keys.KeyCode) == Keys.Up || (base.DroppedDown && (keyData & Keys.KeyCode) == Keys.Escape) || (keyData & Keys.KeyCode) == Keys.Return || !dataGridViewWantsInputKey;
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x0009B7F9 File Offset: 0x000999F9
		public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
		{
			return this.Text;
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x0009B801 File Offset: 0x00099A01
		public virtual void PrepareEditingControlForEdit(bool selectAll)
		{
			if (selectAll)
			{
				base.SelectAll();
			}
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x0009B80C File Offset: 0x00099A0C
		private void NotifyDataGridViewOfValueChange()
		{
			this.valueChanged = true;
			this.dataGridView.NotifyCurrentCellDirty(true);
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x0009B821 File Offset: 0x00099A21
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			base.OnSelectedIndexChanged(e);
			if (this.SelectedIndex != -1)
			{
				this.NotifyDataGridViewOfValueChange();
			}
		}

		// Token: 0x04000D97 RID: 3479
		private DataGridView dataGridView;

		// Token: 0x04000D98 RID: 3480
		private bool valueChanged;

		// Token: 0x04000D99 RID: 3481
		private int rowIndex;
	}
}
