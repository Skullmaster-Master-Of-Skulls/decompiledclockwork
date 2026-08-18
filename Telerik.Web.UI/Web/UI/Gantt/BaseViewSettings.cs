using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x0200034A RID: 842
	public abstract class BaseViewSettings : StateManager
	{
		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x0005AB5C File Offset: 0x00058D5C
		// (set) Token: 0x06001CC3 RID: 7363 RVA: 0x0005AB7D File Offset: 0x00058D7D
		[Category("Appearance")]
		[DefaultValue(true)]
		[Description("Controls the visibility of the tab for the current view in the view chooser")]
		public virtual bool UserSelectable
		{
			get
			{
				return (bool)(base.ViewState["UserSelectable"] ?? true);
			}
			set
			{
				base.ViewState["UserSelectable"] = value;
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06001CC4 RID: 7364
		[Description("The view type - Day, Week, Month or Year")]
		[Category("Misc")]
		public abstract GanttViewType Type { get; }

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06001CC5 RID: 7365 RVA: 0x0005AB95 File Offset: 0x00058D95
		// (set) Token: 0x06001CC6 RID: 7366 RVA: 0x0005ABBC File Offset: 0x00058DBC
		[Description("")]
		[Category("Behavior")]
		public virtual Unit SlotWidth
		{
			get
			{
				return (Unit)(base.ViewState["SlotWidth"] ?? Unit.Pixel(100));
			}
			set
			{
				base.ViewState["SlotWidth"] = value;
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06001CC7 RID: 7367 RVA: 0x0005ABD4 File Offset: 0x00058DD4
		// (set) Token: 0x06001CC8 RID: 7368 RVA: 0x0005ABF0 File Offset: 0x00058DF0
		[DefaultValue(null)]
		[Description("Gets or sets the date to which the timeline of the currently selected view is scrolled.")]
		[Category("Behavior")]
		public DateTime? SelectedDate
		{
			get
			{
				return (DateTime?)(base.ViewState["SelectedDate"] ?? null);
			}
			set
			{
				base.ViewState["SelectedDate"] = value;
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06001CC9 RID: 7369 RVA: 0x0005AC08 File Offset: 0x00058E08
		// (set) Token: 0x06001CCA RID: 7370 RVA: 0x0005AC24 File Offset: 0x00058E24
		[DefaultValue(null)]
		[Description("Gets or sets the start range of the currently selected view.")]
		[Category("Behavior")]
		public DateTime? RangeStart
		{
			get
			{
				return (DateTime?)(base.ViewState["RangeStart"] ?? null);
			}
			set
			{
				base.ViewState["RangeStart"] = value;
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06001CCB RID: 7371 RVA: 0x0005AC3C File Offset: 0x00058E3C
		// (set) Token: 0x06001CCC RID: 7372 RVA: 0x0005AC58 File Offset: 0x00058E58
		[Category("Behavior")]
		[DefaultValue(null)]
		[Description("Gets or sets the end range of the currently selected view.")]
		public DateTime? RangeEnd
		{
			get
			{
				return (DateTime?)(base.ViewState["RangeEnd"] ?? null);
			}
			set
			{
				base.ViewState["RangeEnd"] = value;
			}
		}
	}
}
