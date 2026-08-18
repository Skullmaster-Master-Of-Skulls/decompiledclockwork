using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000316 RID: 790
	internal class MaskedTextBoxTextEditorDropDown : UserControl
	{
		// Token: 0x06001F32 RID: 7986 RVA: 0x000BB644 File Offset: 0x000B9844
		public MaskedTextBoxTextEditorDropDown(MaskedTextBox maskedTextBox)
		{
			this.cloneMtb = MaskedTextBoxDesigner.GetDesignMaskedTextBox(maskedTextBox);
			this.errorProvider = new ErrorProvider();
			((ISupportInitialize)this.errorProvider).BeginInit();
			base.SuspendLayout();
			this.cloneMtb.Dock = DockStyle.Fill;
			this.cloneMtb.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
			this.cloneMtb.ResetOnPrompt = true;
			this.cloneMtb.SkipLiterals = true;
			this.cloneMtb.ResetOnSpace = true;
			this.cloneMtb.Name = "MaskedTextBoxClone";
			this.cloneMtb.TabIndex = 0;
			this.cloneMtb.MaskInputRejected += this.maskedTextBox_MaskInputRejected;
			this.cloneMtb.KeyDown += this.maskedTextBox_KeyDown;
			this.errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
			this.errorProvider.ContainerControl = this;
			base.Controls.Add(this.cloneMtb);
			this.BackColor = SystemColors.Control;
			base.BorderStyle = BorderStyle.FixedSingle;
			base.Name = "MaskedTextBoxTextEditorDropDown";
			base.Padding = new Padding(16);
			base.Size = new Size(100, 52);
			((ISupportInitialize)this.errorProvider).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001F33 RID: 7987 RVA: 0x000BB77F File Offset: 0x000B997F
		public string Value
		{
			get
			{
				if (this.cancel)
				{
					return null;
				}
				return this.cloneMtb.Text;
			}
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x000BB796 File Offset: 0x000B9996
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if (keyData == Keys.Escape)
			{
				this.cancel = true;
			}
			return base.ProcessDialogKey(keyData);
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x000BB7AB File Offset: 0x000B99AB
		private void maskedTextBox_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
		{
			this.errorProvider.SetError(this.cloneMtb, MaskedTextBoxDesigner.GetMaskInputRejectedErrorMessage(e));
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x000BB7C4 File Offset: 0x000B99C4
		private void maskedTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			this.errorProvider.Clear();
		}

		// Token: 0x04001805 RID: 6149
		private bool cancel;

		// Token: 0x04001806 RID: 6150
		private MaskedTextBox cloneMtb;

		// Token: 0x04001807 RID: 6151
		private ErrorProvider errorProvider;
	}
}
