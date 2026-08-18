using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001AAC RID: 6828
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	public class RadSplitBarAdjacentPanesNames : StateManager
	{
		// Token: 0x1700502C RID: 20524
		// (get) Token: 0x06010811 RID: 67601 RVA: 0x003B00C6 File Offset: 0x003AE2C6
		// (set) Token: 0x06010812 RID: 67602 RVA: 0x003B00E6 File Offset: 0x003AE2E6
		[Localizable(true)]
		[DefaultValue("left")]
		[NotifyParentProperty(true)]
		public string LeftPaneName
		{
			get
			{
				return ((string)base.ViewState["LeftPaneName"]) ?? "left";
			}
			set
			{
				base.ViewState["LeftPaneName"] = value;
			}
		}

		// Token: 0x1700502D RID: 20525
		// (get) Token: 0x06010813 RID: 67603 RVA: 0x003B00F9 File Offset: 0x003AE2F9
		// (set) Token: 0x06010814 RID: 67604 RVA: 0x003B0119 File Offset: 0x003AE319
		[DefaultValue("right")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string RightPaneName
		{
			get
			{
				return ((string)base.ViewState["RightPaneName"]) ?? "right";
			}
			set
			{
				base.ViewState["RightPaneName"] = value;
			}
		}

		// Token: 0x1700502E RID: 20526
		// (get) Token: 0x06010815 RID: 67605 RVA: 0x003B012C File Offset: 0x003AE32C
		// (set) Token: 0x06010816 RID: 67606 RVA: 0x003B014C File Offset: 0x003AE34C
		[NotifyParentProperty(true)]
		[DefaultValue("top")]
		[Localizable(true)]
		public string TopPaneName
		{
			get
			{
				return ((string)base.ViewState["TopPaneName"]) ?? "top";
			}
			set
			{
				base.ViewState["TopPaneName"] = value;
			}
		}

		// Token: 0x1700502F RID: 20527
		// (get) Token: 0x06010817 RID: 67607 RVA: 0x003B015F File Offset: 0x003AE35F
		// (set) Token: 0x06010818 RID: 67608 RVA: 0x003B017F File Offset: 0x003AE37F
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("bottom")]
		public string BottomPaneName
		{
			get
			{
				return ((string)base.ViewState["BottomPaneName"]) ?? "bottom";
			}
			set
			{
				base.ViewState["BottomPaneName"] = value;
			}
		}
	}
}
