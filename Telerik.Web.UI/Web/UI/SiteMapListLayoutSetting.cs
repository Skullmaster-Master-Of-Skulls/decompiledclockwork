using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001ABD RID: 6845
	public class SiteMapListLayoutSetting : ObjectWithState
	{
		// Token: 0x060108FB RID: 67835 RVA: 0x003B1E82 File Offset: 0x003B0082
		public SiteMapListLayoutSetting(string keyPrefix, StateBag ownerViewState) : base(keyPrefix, ownerViewState)
		{
		}

		// Token: 0x17005086 RID: 20614
		// (get) Token: 0x060108FC RID: 67836 RVA: 0x003B1E8C File Offset: 0x003B008C
		// (set) Token: 0x060108FD RID: 67837 RVA: 0x003B1EAD File Offset: 0x003B00AD
		[DefaultValue(1)]
		[NotifyParentProperty(true)]
		public int RepeatColumns
		{
			get
			{
				return (int)(base.ViewState["RepeatColumns"] ?? 1);
			}
			set
			{
				base.ViewState["RepeatColumns"] = Math.Max(value, 1);
			}
		}

		// Token: 0x17005087 RID: 20615
		// (get) Token: 0x060108FE RID: 67838 RVA: 0x003B1ECB File Offset: 0x003B00CB
		// (set) Token: 0x060108FF RID: 67839 RVA: 0x003B1EEC File Offset: 0x003B00EC
		[Description("Whether the columns are repeated vertically or horizontally")]
		[DefaultValue(SiteMapRepeatDirection.Horizontal)]
		[NotifyParentProperty(true)]
		public SiteMapRepeatDirection RepeatDirection
		{
			get
			{
				return (SiteMapRepeatDirection)(base.ViewState["RepeatDirection"] ?? SiteMapRepeatDirection.Horizontal);
			}
			set
			{
				base.ViewState["RepeatDirection"] = value;
			}
		}

		// Token: 0x17005088 RID: 20616
		// (get) Token: 0x06010900 RID: 67840 RVA: 0x003B1F04 File Offset: 0x003B0104
		// (set) Token: 0x06010901 RID: 67841 RVA: 0x003B1F25 File Offset: 0x003B0125
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool AlignRows
		{
			get
			{
				return (bool)(base.ViewState["AlignRows"] ?? false);
			}
			set
			{
				base.ViewState["AlignRows"] = value;
			}
		}
	}
}
