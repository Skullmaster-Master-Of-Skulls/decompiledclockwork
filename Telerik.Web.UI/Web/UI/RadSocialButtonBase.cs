using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x0200087D RID: 2173
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class RadSocialButtonBase : StateManager
	{
		// Token: 0x06005067 RID: 20583 RVA: 0x000FB67D File Offset: 0x000F987D
		public RadSocialButtonBase()
		{
		}

		// Token: 0x06005068 RID: 20584 RVA: 0x000FB685 File Offset: 0x000F9885
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RadSocialButtonBase(SocialNetType type)
		{
			this.SocialNetType = type;
		}

		// Token: 0x17001A4E RID: 6734
		// (get) Token: 0x06005069 RID: 20585 RVA: 0x000FB694 File Offset: 0x000F9894
		// (set) Token: 0x0600506A RID: 20586 RVA: 0x000FB6B5 File Offset: 0x000F98B5
		[DefaultValue("")]
		[Category("Behavior")]
		[Description("Specifies the social net type of the button")]
		public virtual SocialNetType SocialNetType
		{
			get
			{
				return (SocialNetType)(base.ViewState["SocialNetType"] ?? SocialNetType.GoogleBookmarks);
			}
			set
			{
				base.ViewState["SocialNetType"] = value;
			}
		}

		// Token: 0x17001A4F RID: 6735
		// (get) Token: 0x0600506B RID: 20587 RVA: 0x000FB6CD File Offset: 0x000F98CD
		// (set) Token: 0x0600506C RID: 20588 RVA: 0x000FB6ED File Offset: 0x000F98ED
		[Category("Behavior")]
		[DefaultValue("")]
		[Description("Specifies the URL to share. Defaults to the current page.")]
		public virtual string UrlToShare
		{
			get
			{
				return (string)(base.ViewState["UrlToShare"] ?? string.Empty);
			}
			set
			{
				base.ViewState["UrlToShare"] = value;
			}
		}

		// Token: 0x17001A50 RID: 6736
		// (get) Token: 0x0600506D RID: 20589 RVA: 0x000FB700 File Offset: 0x000F9900
		// (set) Token: 0x0600506E RID: 20590 RVA: 0x000FB720 File Offset: 0x000F9920
		[DefaultValue("")]
		[Category("Behavior")]
		[Description("Specifies the title of the shared post. Defaults to the page title.")]
		public virtual string TitleToShare
		{
			get
			{
				return (string)(base.ViewState["TitleToShare"] ?? string.Empty);
			}
			set
			{
				base.ViewState["TitleToShare"] = value;
			}
		}
	}
}
