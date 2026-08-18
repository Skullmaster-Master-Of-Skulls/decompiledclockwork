using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x0200035D RID: 861
	public class MonthViewSettings : BaseViewSettings, IMonthViewSettings, IViewSettings
	{
		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06001DB1 RID: 7601 RVA: 0x0005CF3C File Offset: 0x0005B13C
		public override GanttViewType Type
		{
			get
			{
				return GanttViewType.MonthView;
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06001DB2 RID: 7602 RVA: 0x0005CF3F File Offset: 0x0005B13F
		// (set) Token: 0x06001DB3 RID: 7603 RVA: 0x0005CF69 File Offset: 0x0005B169
		[Description("")]
		[Category("Behavior")]
		public override Unit SlotWidth
		{
			get
			{
				return (Unit)(base.ViewState["SlotWidth"] ?? Unit.Pixel(150));
			}
			set
			{
				base.ViewState["SlotWidth"] = value;
			}
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06001DB4 RID: 7604 RVA: 0x0005CF81 File Offset: 0x0005B181
		// (set) Token: 0x06001DB5 RID: 7605 RVA: 0x0005CFA1 File Offset: 0x0005B1A1
		[DefaultValue("MMM, yyyy")]
		[Category("Appearance")]
		[Description("The month header date format string in MonthView. It gets overwritten by the MonthHeaderTemplate if the template is not empty.")]
		public virtual string MonthHeaderDateFormat
		{
			get
			{
				return (string)(base.ViewState["MonthHeaderDateFormat"] ?? "MMM, yyyy");
			}
			set
			{
				base.ViewState["MonthHeaderDateFormat"] = value;
			}
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06001DB6 RID: 7606 RVA: 0x0005CFB4 File Offset: 0x0005B1B4
		// (set) Token: 0x06001DB7 RID: 7607 RVA: 0x0005CFD4 File Offset: 0x0005B1D4
		[DefaultValue("ddd M/dd")]
		[Description("The week header date format string in MonthView. It gets overwritten by the WeekHeaderTemplate if the template is not empty.")]
		[Category("Appearance")]
		public virtual string WeekHeaderDateFormat
		{
			get
			{
				return (string)(base.ViewState["WeekHeaderDateFormat"] ?? "ddd M/dd");
			}
			set
			{
				base.ViewState["WeekHeaderDateFormat"] = value;
			}
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06001DB8 RID: 7608 RVA: 0x0005CFE7 File Offset: 0x0005B1E7
		// (set) Token: 0x06001DB9 RID: 7609 RVA: 0x0005D007 File Offset: 0x0005B207
		[Browsable(false)]
		[DefaultValue("")]
		[Description("The client template used to render the header week slots in 'day' and 'week' view.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[ClientControlProperty]
		[ClientPropertyName("weekHeaderTemplate")]
		public string WeekHeaderTemplate
		{
			get
			{
				return (string)(base.ViewState["WeekHeaderTemplate"] ?? "");
			}
			set
			{
				base.ViewState["WeekHeaderTemplate"] = value;
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06001DBA RID: 7610 RVA: 0x0005D01A File Offset: 0x0005B21A
		// (set) Token: 0x06001DBB RID: 7611 RVA: 0x0005D03A File Offset: 0x0005B23A
		[DefaultValue("")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("The client template used to render the header month slots in 'month' and 'year' views.")]
		[ClientControlProperty]
		[ClientPropertyName("monthHeaderTemplate")]
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
	}
}
