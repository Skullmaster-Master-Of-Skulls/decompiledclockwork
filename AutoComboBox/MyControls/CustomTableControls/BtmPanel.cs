using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x02000079 RID: 121
	public class BtmPanel : UserControl
	{
		// Token: 0x060004D0 RID: 1232 RVA: 0x00026F98 File Offset: 0x00025F98
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00026FD0 File Offset: 0x00025FD0
		private void InitializeComponent()
		{
			this.btn_OK = new Button();
			this.btn_Cancel = new Button();
			this.btn_Apply = new Button();
			base.SuspendLayout();
			this.btn_OK.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.btn_OK.Location = new Point(85, 4);
			this.btn_OK.Name = "btn_OK";
			this.btn_OK.Size = new Size(75, 23);
			this.btn_OK.TabIndex = 1;
			this.btn_OK.Text = "OK";
			this.btn_OK.UseVisualStyleBackColor = true;
			this.btn_OK.Click += this.btn_OK_Click;
			this.btn_Cancel.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.btn_Cancel.Location = new Point(166, 4);
			this.btn_Cancel.Name = "btn_Cancel";
			this.btn_Cancel.Size = new Size(75, 23);
			this.btn_Cancel.TabIndex = 2;
			this.btn_Cancel.Text = "Cancel";
			this.btn_Cancel.UseVisualStyleBackColor = true;
			this.btn_Cancel.Click += this.btn_Cancel_Click;
			this.btn_Apply.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.btn_Apply.Location = new Point(4, 4);
			this.btn_Apply.Name = "btn_Apply";
			this.btn_Apply.Size = new Size(75, 23);
			this.btn_Apply.TabIndex = 3;
			this.btn_Apply.Text = "Apply";
			this.btn_Apply.UseVisualStyleBackColor = true;
			this.btn_Apply.Click += this.btn_Apply_Click;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.btn_Apply);
			base.Controls.Add(this.btn_Cancel);
			base.Controls.Add(this.btn_OK);
			base.Name = "BtmPanel";
			base.Size = new Size(246, 30);
			base.ResumeLayout(false);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00027224 File Offset: 0x00026224
		public BtmPanel(Form form, BtmPanel.BoolReturnMethod commit, BtmPanel.VoidReturnMethod reset, bool showApply)
		{
			this.InitializeComponent();
			this.__commit = commit;
			this.__reset = reset;
			this.__form = form;
			this.__form.DialogResult = DialogResult.Cancel;
			this.btn_Apply.Visible = showApply;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00027278 File Offset: 0x00026278
		private void btn_Apply_Click(object sender, EventArgs e)
		{
			this.CommitAndDo(this.__reset);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00027288 File Offset: 0x00026288
		private void btn_OK_Click(object sender, EventArgs e)
		{
			this.CommitAndDo(new BtmPanel.VoidReturnMethod(this.CloseFormOK));
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0002729E File Offset: 0x0002629E
		private void CloseFormOK()
		{
			this.__form.DialogResult = DialogResult.OK;
			this.__form.Close();
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x000272BC File Offset: 0x000262BC
		private void CommitAndDo(BtmPanel.VoidReturnMethod action)
		{
			if (this.__commit())
			{
				action();
			}
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x000272E5 File Offset: 0x000262E5
		private void btn_Cancel_Click(object sender, EventArgs e)
		{
			this.__form.Close();
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x000272F4 File Offset: 0x000262F4
		public void setAnchorAndGoToDefaultLocation(AnchorStyles anchor, Size parentSize)
		{
			this.setAnchorAndGoToDefaultLocation(anchor, parentSize, 0, 0);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00027304 File Offset: 0x00026304
		public void setAnchorAndGoToDefaultLocation(AnchorStyles anchor, Size parentSize, int HORZ_PAD, int VERT_PAD)
		{
			this.Anchor = anchor;
			base.Location = new Point(((this.Anchor & AnchorStyles.Left) == AnchorStyles.Left) ? HORZ_PAD : (parentSize.Width - base.Width - HORZ_PAD), ((this.Anchor & AnchorStyles.Top) == AnchorStyles.Top) ? VERT_PAD : (parentSize.Height - base.Height - VERT_PAD));
		}

		// Token: 0x04000403 RID: 1027
		private IContainer components = null;

		// Token: 0x04000404 RID: 1028
		private Button btn_OK;

		// Token: 0x04000405 RID: 1029
		private Button btn_Cancel;

		// Token: 0x04000406 RID: 1030
		private Button btn_Apply;

		// Token: 0x04000407 RID: 1031
		private Form __form;

		// Token: 0x04000408 RID: 1032
		private BtmPanel.BoolReturnMethod __commit;

		// Token: 0x04000409 RID: 1033
		private BtmPanel.VoidReturnMethod __reset;

		// Token: 0x0200007A RID: 122
		// (Invoke) Token: 0x060004DB RID: 1243
		public delegate void VoidReturnMethod();

		// Token: 0x0200007B RID: 123
		// (Invoke) Token: 0x060004DF RID: 1247
		public delegate bool BoolReturnMethod();
	}
}
