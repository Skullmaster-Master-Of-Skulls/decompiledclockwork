using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x0200034B RID: 843
	public class YearViewSettings : BaseViewSettings, IYearViewSettings, IViewSettings
	{
		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06001CCE RID: 7374 RVA: 0x0005AC78 File Offset: 0x00058E78
		public override GanttViewType Type
		{
			get
			{
				return GanttViewType.YearView;
			}
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06001CCF RID: 7375 RVA: 0x0005AC7B File Offset: 0x00058E7B
		// (set) Token: 0x06001CD0 RID: 7376 RVA: 0x0005AC9C File Offset: 0x00058E9C
		[Category("Appearance")]
		[DefaultValue(false)]
		[Description("Controls the visibility of the tab for the current view in the view chooser")]
		public override bool UserSelectable
		{
			get
			{
				return (bool)(base.ViewState["UserSelectable"] ?? false);
			}
			set
			{
				base.ViewState["UserSelectable"] = value;
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06001CD1 RID: 7377 RVA: 0x0005ACB4 File Offset: 0x00058EB4
		// (set) Token: 0x06001CD2 RID: 7378 RVA: 0x0005ACD4 File Offset: 0x00058ED4
		[DefaultValue("yyyy")]
		[Description("The year header date format string in YearView. It gets overwritten by the YearHeaderTemplate if the template is not empty.")]
		[Category("Appearance")]
		public virtual string YearHeaderDateFormat
		{
			get
			{
				return (string)(base.ViewState["YearHeaderDateFormat"] ?? "yyyy");
			}
			set
			{
				base.ViewState["YearHeaderDateFormat"] = value;
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x0005ACE7 File Offset: 0x00058EE7
		// (set) Token: 0x06001CD4 RID: 7380 RVA: 0x0005AD07 File Offset: 0x00058F07
		[Description("The month header date format string in YearView. It gets overwritten by the MonthHeaderTemplate if the template is not empty.")]
		[DefaultValue("MMM")]
		[Category("Appearance")]
		public virtual string MonthHeaderDateFormat
		{
			get
			{
				return (string)(base.ViewState["MonthHeaderDateFormat"] ?? "MMM");
			}
			set
			{
				base.ViewState["MonthHeaderDateFormat"] = value;
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06001CD5 RID: 7381 RVA: 0x0005AD1A File Offset: 0x00058F1A
		// (set) Token: 0x06001CD6 RID: 7382 RVA: 0x0005AD3A File Offset: 0x00058F3A
		[Browsable(false)]
		[ClientPropertyName("monthHeaderTemplate")]
		[DefaultValue("")]
		[Description("The client template used to render the header month slots in 'month' and 'year' views.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[ClientControlProperty]
		public string MonthHeaderTemplate
		{
			get
			{
				return (string)(base.ViewState["MonthHeaderTemplate"] ?? "");
			}
			set
			{
				base.ViewState["MonthHeaderTemplate"] = value;
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06001CD7 RID: 7383 RVA: 0x0005AD4D File Offset: 0x00058F4D
		// (set) Token: 0x06001CD8 RID: 7384 RVA: 0x0005AD6D File Offset: 0x00058F6D
		[DefaultValue("")]
		[ClientControlProperty]
		[Browsable(false)]
		[Description("The client template used to render the header year slots in 'year' view.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[ClientPropertyName("yearHeaderTemplate")]
		public string YearHeaderTemplate
		{
			get
			{
				return (string)(base.ViewState["YearHeaderTemplate"] ?? "");
			}
			set
			{
				base.ViewState["YearHeaderTemplate"] = value;
			}
		}
	}
}
