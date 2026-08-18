using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000931 RID: 2353
	[Serializable]
	public class TimelineItemAction : StateManager, IAttributeAccessor
	{
		// Token: 0x06005942 RID: 22850 RVA: 0x0010FFF9 File Offset: 0x0010E1F9
		public TimelineItemAction()
		{
			if (this.Attributes == null)
			{
				this.Attributes = new Dictionary<string, string>();
			}
		}

		// Token: 0x17001D6E RID: 7534
		// (get) Token: 0x06005943 RID: 22851 RVA: 0x00110014 File Offset: 0x0010E214
		// (set) Token: 0x06005944 RID: 22852 RVA: 0x0011001C File Offset: 0x0010E21C
		[Browsable(false)]
		public TimelineItem Owner { get; set; }

		// Token: 0x17001D6F RID: 7535
		// (get) Token: 0x06005945 RID: 22853 RVA: 0x00110025 File Offset: 0x0010E225
		// (set) Token: 0x06005946 RID: 22854 RVA: 0x0011002D File Offset: 0x0010E22D
		public string Text { get; set; }

		// Token: 0x17001D70 RID: 7536
		// (get) Token: 0x06005947 RID: 22855 RVA: 0x00110036 File Offset: 0x0010E236
		// (set) Token: 0x06005948 RID: 22856 RVA: 0x0011003E File Offset: 0x0010E23E
		public string Url { get; set; }

		// Token: 0x17001D71 RID: 7537
		// (get) Token: 0x06005949 RID: 22857 RVA: 0x00110047 File Offset: 0x0010E247
		// (set) Token: 0x0600594A RID: 22858 RVA: 0x00110063 File Offset: 0x0010E263
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

		// Token: 0x0600594B RID: 22859 RVA: 0x00110076 File Offset: 0x0010E276
		public string GetAttribute(string key)
		{
			return this.Attributes[key];
		}

		// Token: 0x0600594C RID: 22860 RVA: 0x00110084 File Offset: 0x0010E284
		public void SetAttribute(string key, string value)
		{
			this.Attributes[key] = value;
		}
	}
}
