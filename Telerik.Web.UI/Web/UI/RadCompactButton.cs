using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200087F RID: 2175
	public class RadCompactButton : RadSocialButton
	{
		// Token: 0x17001A5B RID: 6747
		// (get) Token: 0x06005085 RID: 20613 RVA: 0x000FBA0E File Offset: 0x000F9C0E
		// (set) Token: 0x06005086 RID: 20614 RVA: 0x000FBA12 File Offset: 0x000F9C12
		[DefaultValue(SocialNetType.CompactButton)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Category("Behavior")]
		[Browsable(false)]
		public override SocialNetType SocialNetType
		{
			get
			{
				return SocialNetType.CompactButton;
			}
			set
			{
				this.SocialNetType = SocialNetType.CompactButton;
			}
		}

		// Token: 0x17001A5C RID: 6748
		// (get) Token: 0x06005087 RID: 20615 RVA: 0x000FBA1C File Offset: 0x000F9C1C
		// (set) Token: 0x06005088 RID: 20616 RVA: 0x000FBA3C File Offset: 0x000F9C3C
		[Description("Specifies the title of the compact dialog.")]
		[DefaultValue("Share on")]
		[Category("Appearance")]
		public string DialogTitle
		{
			get
			{
				return (string)(base.ViewState["DialogTitle"] ?? "Share on");
			}
			set
			{
				base.ViewState["DialogTitle"] = value;
			}
		}

		// Token: 0x17001A5D RID: 6749
		// (get) Token: 0x06005089 RID: 20617 RVA: 0x000FBA4F File Offset: 0x000F9C4F
		// (set) Token: 0x0600508A RID: 20618 RVA: 0x000FBA56 File Offset: 0x000F9C56
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue("")]
		[Category("Behavior")]
		public override string UrlToShare
		{
			get
			{
				return string.Empty;
			}
			set
			{
				base.ViewState["UrlToShare"] = value;
			}
		}

		// Token: 0x17001A5E RID: 6750
		// (get) Token: 0x0600508B RID: 20619 RVA: 0x000FBA69 File Offset: 0x000F9C69
		// (set) Token: 0x0600508C RID: 20620 RVA: 0x000FBA70 File Offset: 0x000F9C70
		[DefaultValue("")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Category("Behavior")]
		public override string TitleToShare
		{
			get
			{
				return string.Empty;
			}
			set
			{
				base.ViewState["TitleToShare"] = value;
			}
		}
	}
}
