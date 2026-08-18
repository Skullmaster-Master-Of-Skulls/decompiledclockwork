using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000919 RID: 2329
	public class TileBadge : StateManager
	{
		// Token: 0x17001D2A RID: 7466
		// (get) Token: 0x0600584B RID: 22603 RVA: 0x0010DB51 File Offset: 0x0010BD51
		// (set) Token: 0x0600584C RID: 22604 RVA: 0x0010DB6D File Offset: 0x0010BD6D
		[Category("Behavior")]
		[Description("Gets or sets the size of the pointer.")]
		[DefaultValue(null)]
		public int? Value
		{
			get
			{
				return (int?)(base.ViewState["Value"] ?? null);
			}
			set
			{
				base.ViewState["Value"] = value;
			}
		}

		// Token: 0x17001D2B RID: 7467
		// (get) Token: 0x0600584D RID: 22605 RVA: 0x0010DB85 File Offset: 0x0010BD85
		// (set) Token: 0x0600584E RID: 22606 RVA: 0x0010DBA5 File Offset: 0x0010BDA5
		[Description("Gets or sets the url of the image which will be renderd in the badge.")]
		[DefaultValue("")]
		[Category("Behavior")]
		[UrlProperty]
		public string ImageUrl
		{
			get
			{
				return ((string)base.ViewState["ImageUrl"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x17001D2C RID: 7468
		// (get) Token: 0x0600584F RID: 22607 RVA: 0x0010DBB8 File Offset: 0x0010BDB8
		// (set) Token: 0x06005850 RID: 22608 RVA: 0x0010DBD9 File Offset: 0x0010BDD9
		[DefaultValue(TileBadgeType.None)]
		[Category("Behavior")]
		[Description("Gets or sets predefined image of the badge.")]
		public TileBadgeType PredefinedType
		{
			get
			{
				return (TileBadgeType)(base.ViewState["PredefinedType"] ?? TileBadgeType.None);
			}
			set
			{
				base.ViewState["PredefinedType"] = value;
			}
		}

		// Token: 0x06005851 RID: 22609 RVA: 0x0010DBF1 File Offset: 0x0010BDF1
		internal string GetBadgeImageUrl()
		{
			if (!string.IsNullOrEmpty(this.ImageUrl))
			{
				return this.ImageUrl;
			}
			return "";
		}
	}
}
