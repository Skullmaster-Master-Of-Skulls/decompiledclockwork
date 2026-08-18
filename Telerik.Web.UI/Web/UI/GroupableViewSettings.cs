using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200081F RID: 2079
	public class GroupableViewSettings : ViewSettings
	{
		// Token: 0x06004CC4 RID: 19652 RVA: 0x000F13F2 File Offset: 0x000EF5F2
		internal GroupableViewSettings(IScheduler owner, string keyPrefix, StateBag ownerViewState) : base(owner, keyPrefix, ownerViewState)
		{
		}

		// Token: 0x1700190E RID: 6414
		// (get) Token: 0x06004CC5 RID: 19653 RVA: 0x000F1400 File Offset: 0x000EF600
		internal string GroupByResolved
		{
			get
			{
				return (base.ViewState["GroupBy"] != null) ? this.GroupBy : base.Owner.GroupBy;
			}
		}

		// Token: 0x1700190F RID: 6415
		// (get) Token: 0x06004CC6 RID: 19654 RVA: 0x000F1434 File Offset: 0x000EF634
		internal GroupingDirection GroupingDirectionResolved
		{
			get
			{
				if (base.ViewState["GroupingDirection"] == null)
				{
					return base.Owner.GroupingDirection;
				}
				return this.GroupingDirection;
			}
		}

		// Token: 0x17001910 RID: 6416
		// (get) Token: 0x06004CC7 RID: 19655 RVA: 0x000F145A File Offset: 0x000EF65A
		internal bool ShowResourceHeadersResolved
		{
			get
			{
				if (base.ViewState["ShowResourceHeaders"] == null)
				{
					return base.Owner.ShowResourceHeaders;
				}
				return this.ShowResourceHeaders;
			}
		}

		// Token: 0x17001911 RID: 6417
		// (get) Token: 0x06004CC8 RID: 19656 RVA: 0x000F1480 File Offset: 0x000EF680
		// (set) Token: 0x06004CC9 RID: 19657 RVA: 0x000F14A0 File Offset: 0x000EF6A0
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("The name of the resource type to group by.")]
		[Category("Layout")]
		public virtual string GroupBy
		{
			get
			{
				return (string)(base.ViewState["GroupBy"] ?? string.Empty);
			}
			set
			{
				base.ViewState["GroupBy"] = value;
				base.Owner.NotifyDataPropertyChanged();
			}
		}

		// Token: 0x17001912 RID: 6418
		// (get) Token: 0x06004CCA RID: 19658 RVA: 0x000F14BE File Offset: 0x000EF6BE
		// (set) Token: 0x06004CCB RID: 19659 RVA: 0x000F14DF File Offset: 0x000EF6DF
		[DefaultValue(GroupingDirection.Horizontal)]
		[Category("Layout")]
		[NotifyParentProperty(true)]
		[Description("Grouping direction.")]
		public GroupingDirection GroupingDirection
		{
			get
			{
				return (GroupingDirection)(base.ViewState["GroupingDirection"] ?? GroupingDirection.Horizontal);
			}
			set
			{
				base.ViewState["GroupingDirection"] = value;
				base.Owner.NotifyDataPropertyChanged();
			}
		}

		// Token: 0x17001913 RID: 6419
		// (get) Token: 0x06004CCC RID: 19660 RVA: 0x000F1502 File Offset: 0x000EF702
		// (set) Token: 0x06004CCD RID: 19661 RVA: 0x000F1523 File Offset: 0x000EF723
		[Category("Appearance")]
		[Description("Controls the visibility of the resource headers for the current view")]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public bool ShowResourceHeaders
		{
			get
			{
				return (bool)(base.ViewState["ShowResourceHeaders"] ?? true);
			}
			set
			{
				base.ViewState["ShowResourceHeaders"] = value;
			}
		}
	}
}
