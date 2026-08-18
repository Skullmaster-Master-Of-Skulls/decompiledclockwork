using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002E6 RID: 742
	internal partial class FormatStringDialog : Form
	{
		// Token: 0x06001DBD RID: 7613 RVA: 0x000B4B5C File Offset: 0x000B2D5C
		public FormatStringDialog(ITypeDescriptorContext context)
		{
			this.context = context;
			this.InitializeComponent();
			string @string = SR.GetString("RTL");
			if (@string.Equals("RTL_False"))
			{
				this.RightToLeft = RightToLeft.No;
				this.RightToLeftLayout = false;
				return;
			}
			this.RightToLeft = RightToLeft.Yes;
			this.RightToLeftLayout = true;
		}

		// Token: 0x1700065A RID: 1626
		// (set) Token: 0x06001DBE RID: 7614 RVA: 0x000B4BB1 File Offset: 0x000B2DB1
		public DataGridViewCellStyle DataGridViewCellStyle
		{
			set
			{
				this.dgvCellStyle = value;
				this.listControl = null;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001DBF RID: 7615 RVA: 0x000B4BC1 File Offset: 0x000B2DC1
		public bool Dirty
		{
			get
			{
				return this.dirty || this.formatControl1.Dirty;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (set) Token: 0x06001DC0 RID: 7616 RVA: 0x000B4BD8 File Offset: 0x000B2DD8
		public ListControl ListControl
		{
			set
			{
				this.listControl = value;
				this.dgvCellStyle = null;
			}
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x000B4BE8 File Offset: 0x000B2DE8
		private void FormatStringDialog_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			this.FormatStringDialog_HelpRequestHandled();
			e.Cancel = true;
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x000B4BF7 File Offset: 0x000B2DF7
		private void FormatStringDialog_HelpRequested(object sender, HelpEventArgs e)
		{
			this.FormatStringDialog_HelpRequestHandled();
			e.Handled = true;
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x000B4C08 File Offset: 0x000B2E08
		private void FormatStringDialog_HelpRequestHandled()
		{
			IHelpService helpService = this.context.GetService(typeof(IHelpService)) as IHelpService;
			if (helpService != null)
			{
				helpService.ShowHelpFromKeyword("vs.FormatStringDialog");
			}
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x000B4C40 File Offset: 0x000B2E40
		internal void FormatControlFinishedLoading()
		{
			this.okButton.Top = this.formatControl1.Bottom + 5;
			this.cancelButton.Top = this.formatControl1.Bottom + 5;
			int rightSideOffset = FormatStringDialog.GetRightSideOffset(this.formatControl1);
			int rightSideOffset2 = FormatStringDialog.GetRightSideOffset(this.cancelButton);
			this.okButton.Left += rightSideOffset - rightSideOffset2;
			this.cancelButton.Left += rightSideOffset - rightSideOffset2;
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x000B4CC0 File Offset: 0x000B2EC0
		private static int GetRightSideOffset(Control ctl)
		{
			int num = ctl.Width;
			while (ctl != null)
			{
				num += ctl.Left;
				ctl = ctl.Parent;
			}
			return num;
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x000B4CEC File Offset: 0x000B2EEC
		private void FormatStringDialog_Load(object sender, EventArgs e)
		{
			string text = (this.dgvCellStyle != null) ? this.dgvCellStyle.Format : this.listControl.FormatString;
			object obj = (this.dgvCellStyle != null) ? this.dgvCellStyle.NullValue : null;
			string formatType = string.Empty;
			if (!string.IsNullOrEmpty(text))
			{
				formatType = FormatControl.FormatTypeStringFromFormatString(text);
			}
			if (this.dgvCellStyle != null)
			{
				this.formatControl1.NullValueTextBoxEnabled = true;
			}
			else
			{
				this.formatControl1.NullValueTextBoxEnabled = false;
			}
			this.formatControl1.FormatType = formatType;
			FormatControl.FormatTypeClass formatTypeItem = this.formatControl1.FormatTypeItem;
			if (formatTypeItem != null)
			{
				formatTypeItem.PushFormatStringIntoFormatType(text);
			}
			else
			{
				this.formatControl1.FormatType = SR.GetString("BindingFormattingDialogFormatTypeNoFormatting");
			}
			this.formatControl1.NullValue = ((obj != null) ? obj.ToString() : "");
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x00003937 File Offset: 0x00001B37
		public void End()
		{
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x000B504F File Offset: 0x000B324F
		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.dirty = false;
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x000B5058 File Offset: 0x000B3258
		private void okButton_Click(object sender, EventArgs e)
		{
			this.PushChanges();
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x000B5060 File Offset: 0x000B3260
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if ((keyData & Keys.Modifiers) != Keys.None)
			{
				return base.ProcessDialogKey(keyData);
			}
			Keys keys = keyData & Keys.KeyCode;
			if (keys == Keys.Return)
			{
				base.DialogResult = DialogResult.OK;
				this.PushChanges();
				base.Close();
				return true;
			}
			if (keys != Keys.Escape)
			{
				return base.ProcessDialogKey(keyData);
			}
			this.dirty = false;
			base.DialogResult = DialogResult.Cancel;
			base.Close();
			return true;
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x000B50C4 File Offset: 0x000B32C4
		private void PushChanges()
		{
			FormatControl.FormatTypeClass formatTypeItem = this.formatControl1.FormatTypeItem;
			if (formatTypeItem != null)
			{
				if (this.dgvCellStyle != null)
				{
					this.dgvCellStyle.Format = formatTypeItem.FormatString;
					this.dgvCellStyle.NullValue = this.formatControl1.NullValue;
				}
				else
				{
					this.listControl.FormatString = formatTypeItem.FormatString;
				}
				this.dirty = true;
			}
		}

		// Token: 0x040017A2 RID: 6050
		private ITypeDescriptorContext context;

		// Token: 0x040017A6 RID: 6054
		private bool dirty;

		// Token: 0x040017A7 RID: 6055
		private DataGridViewCellStyle dgvCellStyle;

		// Token: 0x040017A8 RID: 6056
		private ListControl listControl;
	}
}
