using System;
using System.ComponentModel;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x02001057 RID: 4183
	[ClientScriptResource("Telerik.Web.UI.Widgets.ImageProperties", "Telerik.Web.UI.Common.Core.js")]
	[ToolboxItem(false)]
	public class SetImagePropertiesDialog : UserControlBase, IClientParameterConsumer
	{
		// Token: 0x1700363A RID: 13882
		// (get) Token: 0x0600A8FB RID: 43259 RVA: 0x0024B6EC File Offset: 0x002498EC
		public override string DialogName
		{
			get
			{
				return "SetImageProperties";
			}
		}

		// Token: 0x1700363B RID: 13883
		// (get) Token: 0x0600A8FC RID: 43260 RVA: 0x0024B6F4 File Offset: 0x002498F4
		// (set) Token: 0x0600A8FD RID: 43261 RVA: 0x0024B71D File Offset: 0x0024991D
		[DefaultValue(true)]
		public bool StandAlone
		{
			get
			{
				object obj = this.ViewState["StandAlone"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["StandAlone"] = value;
			}
		}

		// Token: 0x1700363C RID: 13884
		// (get) Token: 0x0600A8FE RID: 43262 RVA: 0x0024B738 File Offset: 0x00249938
		// (set) Token: 0x0600A8FF RID: 43263 RVA: 0x0024B761 File Offset: 0x00249961
		[DefaultValue(false)]
		public bool EnableThumbnailLinking
		{
			get
			{
				object obj = this.ViewState["EnableThumbnailLinking"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["EnableThumbnailLinking"] = value;
			}
		}

		// Token: 0x0600A900 RID: 43264 RVA: 0x0024B77C File Offset: 0x0024997C
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (!this.StandAlone)
			{
				HtmlControl htmlControl = this.FindControl("controlButtonsRow") as HtmlControl;
				if (htmlControl != null)
				{
					htmlControl.Visible = false;
				}
				HtmlControl htmlControl2 = this.FindControl("imageCallerRow") as HtmlControl;
				if (htmlControl2 != null)
				{
					htmlControl2.Visible = false;
				}
				HtmlControl htmlControl3 = this.FindControl("thumbRow") as HtmlControl;
				if (htmlControl3 != null && this.EnableThumbnailLinking)
				{
					htmlControl3.Visible = true;
				}
			}
			ImageDialogCaller imageDialogCaller = (ImageDialogCaller)base.FindControlRecursive("ImageSrc");
			if (imageDialogCaller != null)
			{
				imageDialogCaller.Text = base.ToolsLocalization.ImageManager;
			}
		}
	}
}
