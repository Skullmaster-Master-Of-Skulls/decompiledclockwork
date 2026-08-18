using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020016C4 RID: 5828
	internal class InvisibleTextBox : StateManager, IAutoBotDiscoveryStrategy, ISpamProtector, IDisposable
	{
		// Token: 0x170044F0 RID: 17648
		// (get) Token: 0x0600E0EA RID: 57578 RVA: 0x0031F964 File Offset: 0x0031DB64
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
		}

		// Token: 0x170044F1 RID: 17649
		// (get) Token: 0x0600E0EB RID: 57579 RVA: 0x0031F96C File Offset: 0x0031DB6C
		// (set) Token: 0x0600E0EC RID: 57580 RVA: 0x0031F98C File Offset: 0x0031DB8C
		public string LabelText
		{
			get
			{
				return (string)(base.ViewState["LabelText"] ?? "Do not fill this textbox.");
			}
			set
			{
				base.ViewState["LabelText"] = value;
			}
		}

		// Token: 0x0600E0ED RID: 57581 RVA: 0x0031F9A0 File Offset: 0x0031DBA0
		public void AddChildControls(Control container)
		{
			this.invisibleTextBox.ID = "InvisibleTextBox";
			this.invisibleTextBox.Style[HtmlTextWriterStyle.Display] = "none";
			this.hiddenLabel.ID = "HiddenLabel";
			this.hiddenLabel.AssociatedControlID = this.invisibleTextBox.ID;
			this.hiddenLabel.Text = this.LabelText;
			this.hiddenLabel.Style[HtmlTextWriterStyle.Display] = "none";
			container.Controls.Add(this.hiddenLabel);
			container.Controls.Add(this.invisibleTextBox);
		}

		// Token: 0x170044F2 RID: 17650
		// (get) Token: 0x0600E0EE RID: 57582 RVA: 0x0031FA44 File Offset: 0x0031DC44
		// (set) Token: 0x0600E0EF RID: 57583 RVA: 0x0031FA70 File Offset: 0x0031DC70
		public bool Visible
		{
			get
			{
				object obj = base.ViewState["Visible"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["Visible"] = value;
				this.invisibleTextBox.Visible = value;
				this.hiddenLabel.Visible = value;
			}
		}

		// Token: 0x0600E0F0 RID: 57584 RVA: 0x0031FAB0 File Offset: 0x0031DCB0
		public void LoadPostBackData(Control container)
		{
			TextBox textBox = container.FindControl("InvisibleTextBox") as TextBox;
			if (textBox != null)
			{
				this.postData = textBox.Text;
			}
		}

		// Token: 0x0600E0F1 RID: 57585 RVA: 0x0031FADD File Offset: 0x0031DCDD
		public void ValidatePostBackData()
		{
			this.isValid = string.IsNullOrEmpty(this.postData);
		}

		// Token: 0x0600E0F2 RID: 57586 RVA: 0x0031FAF0 File Offset: 0x0031DCF0
		public void PreRenderHandler()
		{
		}

		// Token: 0x0600E0F3 RID: 57587 RVA: 0x0031FAF2 File Offset: 0x0031DCF2
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.invisibleTextBox != null)
				{
					this.invisibleTextBox.Dispose();
				}
				if (this.hiddenLabel != null)
				{
					this.hiddenLabel.Dispose();
				}
			}
		}

		// Token: 0x0600E0F4 RID: 57588 RVA: 0x0031FB1D File Offset: 0x0031DD1D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400410C RID: 16652
		private TextBox invisibleTextBox = new TextBox();

		// Token: 0x0400410D RID: 16653
		private Label hiddenLabel = new Label();

		// Token: 0x0400410E RID: 16654
		private bool isValid = true;

		// Token: 0x0400410F RID: 16655
		private string postData;
	}
}
