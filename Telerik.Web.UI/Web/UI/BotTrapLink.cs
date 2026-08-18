using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020016C2 RID: 5826
	internal class BotTrapLink : StateManager, IAutoBotDiscoveryStrategy, ISpamProtector
	{
		// Token: 0x170044EA RID: 17642
		// (get) Token: 0x0600E0D8 RID: 57560 RVA: 0x0031F649 File Offset: 0x0031D849
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
		}

		// Token: 0x170044EB RID: 17643
		// (get) Token: 0x0600E0D9 RID: 57561 RVA: 0x0031F651 File Offset: 0x0031D851
		// (set) Token: 0x0600E0DA RID: 57562 RVA: 0x0031F659 File Offset: 0x0031D859
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
			set
			{
				this.errorMessage = value;
			}
		}

		// Token: 0x170044EC RID: 17644
		// (get) Token: 0x0600E0DB RID: 57563 RVA: 0x0031F662 File Offset: 0x0031D862
		// (set) Token: 0x0600E0DC RID: 57564 RVA: 0x0031F66A File Offset: 0x0031D86A
		public string LabelText
		{
			get
			{
				return this.labelText;
			}
			set
			{
				this.labelText = value;
			}
		}

		// Token: 0x170044ED RID: 17645
		// (get) Token: 0x0600E0DD RID: 57565 RVA: 0x0031F673 File Offset: 0x0031D873
		// (set) Token: 0x0600E0DE RID: 57566 RVA: 0x0031F67B File Offset: 0x0031D87B
		public string PrevGuid
		{
			get
			{
				return this.prevGuid;
			}
			set
			{
				this.prevGuid = value;
			}
		}

		// Token: 0x0600E0DF RID: 57567 RVA: 0x0031F684 File Offset: 0x0031D884
		public BotTrapLink()
		{
			this.isValid = true;
			this.errorMessage = string.Empty;
			this.guid = Guid.NewGuid().ToString();
			this.prevGuid = this.guid;
		}

		// Token: 0x0600E0E0 RID: 57568 RVA: 0x0031F6D0 File Offset: 0x0031D8D0
		public void AddChildControls(Control container)
		{
			Image image = new Image();
			image.ID = "OnePxImage";
			image.ImageUrl = "#";
			image.Width = 1;
			image.Height = 1;
			image.AlternateText = "";
			HyperLink hyperLink = new HyperLink();
			hyperLink.NavigateUrl = "BotHandler.axd?guid=" + this.guid;
			hyperLink.Controls.Add(image);
			TextBox textBox = new TextBox();
			textBox.ID = "BotTrapGuid";
			textBox.Text = this.guid;
			textBox.ReadOnly = true;
			textBox.Style[HtmlTextWriterStyle.Display] = "none";
			Label label = new Label();
			label.ID = "BotTrapGuidLabel";
			label.Text = this.labelText;
			label.Style[HtmlTextWriterStyle.Display] = "none";
			label.AssociatedControlID = textBox.ID;
			container.Controls.Add(label);
			container.Controls.Add(hyperLink);
			container.Controls.Add(textBox);
		}

		// Token: 0x170044EE RID: 17646
		// (get) Token: 0x0600E0E1 RID: 57569 RVA: 0x0031F7DC File Offset: 0x0031D9DC
		// (set) Token: 0x0600E0E2 RID: 57570 RVA: 0x0031F808 File Offset: 0x0031DA08
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
			}
		}

		// Token: 0x0600E0E3 RID: 57571 RVA: 0x0031F830 File Offset: 0x0031DA30
		public void LoadPostBackData(Control container)
		{
			TextBox textBox = container.FindControl("BotTrapGuid") as TextBox;
			if (textBox != null)
			{
				this.postData = textBox.Text;
				if (this.postData == null)
				{
					this.postData = string.Empty;
				}
			}
		}

		// Token: 0x0600E0E4 RID: 57572 RVA: 0x0031F870 File Offset: 0x0031DA70
		public void ValidatePostBackData()
		{
			if (!string.IsNullOrEmpty(this.prevGuid))
			{
				string a = HttpRuntime.Cache.Get(this.prevGuid) as string;
				this.isValid = !(a == "BotGuid");
				this.RemoveGuidFromCache();
				this.prevGuid = this.guid;
			}
		}

		// Token: 0x0600E0E5 RID: 57573 RVA: 0x0031F8C6 File Offset: 0x0031DAC6
		public void PreRenderHandler()
		{
		}

		// Token: 0x0600E0E6 RID: 57574 RVA: 0x0031F8C8 File Offset: 0x0031DAC8
		private void RemoveGuidFromCache()
		{
			HttpRuntime.Cache.Remove(this.prevGuid);
		}

		// Token: 0x04004106 RID: 16646
		private bool isValid;

		// Token: 0x04004107 RID: 16647
		private string errorMessage;

		// Token: 0x04004108 RID: 16648
		private string postData;

		// Token: 0x04004109 RID: 16649
		private readonly string guid;

		// Token: 0x0400410A RID: 16650
		private string prevGuid;

		// Token: 0x0400410B RID: 16651
		private string labelText;
	}
}
