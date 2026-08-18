using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000930 RID: 2352
	[Serializable]
	public class TimelineItemImage : StateManager, IAttributeAccessor
	{
		// Token: 0x06005939 RID: 22841 RVA: 0x0010FF70 File Offset: 0x0010E170
		public TimelineItemImage()
		{
			if (this.Attributes == null)
			{
				this.Attributes = new Dictionary<string, string>();
			}
		}

		// Token: 0x17001D6B RID: 7531
		// (get) Token: 0x0600593A RID: 22842 RVA: 0x0010FF8B File Offset: 0x0010E18B
		// (set) Token: 0x0600593B RID: 22843 RVA: 0x0010FF93 File Offset: 0x0010E193
		[Browsable(false)]
		public TimelineItem Owner { get; set; }

		// Token: 0x17001D6C RID: 7532
		// (get) Token: 0x0600593C RID: 22844 RVA: 0x0010FF9C File Offset: 0x0010E19C
		// (set) Token: 0x0600593D RID: 22845 RVA: 0x0010FFA4 File Offset: 0x0010E1A4
		public string Src { get; set; }

		// Token: 0x0600593E RID: 22846 RVA: 0x0010FFAD File Offset: 0x0010E1AD
		public string GetAttribute(string key)
		{
			return this.Attributes[key];
		}

		// Token: 0x0600593F RID: 22847 RVA: 0x0010FFBB File Offset: 0x0010E1BB
		public void SetAttribute(string key, string value)
		{
			this.Attributes[key] = value;
		}

		// Token: 0x17001D6D RID: 7533
		// (get) Token: 0x06005940 RID: 22848 RVA: 0x0010FFCA File Offset: 0x0010E1CA
		// (set) Token: 0x06005941 RID: 22849 RVA: 0x0010FFE6 File Offset: 0x0010E1E6
		[Browsable(false)]
		public Dictionary<string, string> Attributes
		{
			get
			{
				return (Dictionary<string, string>)(base.ViewState["Attributes"] ?? null);
			}
			set
			{
				base.ViewState["Attributes"] = value;
			}
		}
	}
}
