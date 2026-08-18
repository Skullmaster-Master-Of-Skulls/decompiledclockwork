using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000472 RID: 1138
	public sealed class DropDownTreeButtonSettings : ObjectWithState
	{
		// Token: 0x060028DE RID: 10462 RVA: 0x00084420 File Offset: 0x00082620
		internal DropDownTreeButtonSettings(StateBag ownerViewState) : base("DropDownTreeButtonSettings", ownerViewState)
		{
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x060028DF RID: 10463 RVA: 0x0008442E File Offset: 0x0008262E
		// (set) Token: 0x060028E0 RID: 10464 RVA: 0x0008444F File Offset: 0x0008264F
		[Description("Whether to show the delete button")]
		[DefaultValue(false)]
		public bool ShowClear
		{
			get
			{
				return (bool)(base.ViewState["ShowClear"] ?? false);
			}
			set
			{
				base.ViewState["ShowClear"] = value;
			}
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x060028E1 RID: 10465 RVA: 0x00084467 File Offset: 0x00082667
		// (set) Token: 0x060028E2 RID: 10466 RVA: 0x00084488 File Offset: 0x00082688
		[Description("Whether to show the delete button")]
		[DefaultValue(false)]
		public bool ShowCheckAll
		{
			get
			{
				return (bool)(base.ViewState["ShowCheckAll"] ?? false);
			}
			set
			{
				base.ViewState["ShowCheckAll"] = value;
			}
		}
	}
}
