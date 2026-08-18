using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x0200035F RID: 863
	public class DayViewSettings : BaseViewSettings, IDayViewSettings, IViewSettings
	{
		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06001DC7 RID: 7623 RVA: 0x0005D12C File Offset: 0x0005B32C
		public override GanttViewType Type
		{
			get
			{
				return GanttViewType.DayView;
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06001DC8 RID: 7624 RVA: 0x0005D12F File Offset: 0x0005B32F
		// (set) Token: 0x06001DC9 RID: 7625 RVA: 0x0005D150 File Offset: 0x0005B350
		[Description("Determines the hour span for each cell in DayView")]
		[ClientPropertyName("hourSpan")]
		[Category("Appearance")]
		public virtual int HourSpan
		{
			get
			{
				return (int)(base.ViewState["HourSpan"] ?? 1);
			}
			set
			{
				base.ViewState["HourSpan"] = value;
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06001DCA RID: 7626 RVA: 0x0005D168 File Offset: 0x0005B368
		// (set) Token: 0x06001DCB RID: 7627 RVA: 0x0005D188 File Offset: 0x0005B388
		[Category("Appearance")]
		[Description("The day header date format string in DayView. It gets overwritten by the DayHeaderTemplate if the template is not empty.")]
		[DefaultValue("ddd M/dd")]
		public virtual string DayHeaderDateFormat
		{
			get
			{
				return (string)(base.ViewState["DayHeaderDateFormat"] ?? "ddd M/dd");
			}
			set
			{
				base.ViewState["DayHeaderDateFormat"] = value;
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06001DCC RID: 7628 RVA: 0x0005D19B File Offset: 0x0005B39B
		// (set) Token: 0x06001DCD RID: 7629 RVA: 0x0005D1BB File Offset: 0x0005B3BB
		[Category("Appearance")]
		[DefaultValue("t")]
		[Description("The time header date format string in DayView. It gets overwritten by the TimeHeaderTemplate if the template is not empty.")]
		public virtual string TimeHeaderDateFormat
		{
			get
			{
				return (string)(base.ViewState["TimeHeaderDateFormat"] ?? "t");
			}
			set
			{
				base.ViewState["TimeHeaderDateFormat"] = value;
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06001DCE RID: 7630 RVA: 0x0005D1CE File Offset: 0x0005B3CE
		// (set) Token: 0x06001DCF RID: 7631 RVA: 0x0005D1EE File Offset: 0x0005B3EE
		[DefaultValue("")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("The client template used to render the header time slots in 'day' view.")]
		[ClientControlProperty]
		[ClientPropertyName("timeHeaderTemplate")]
		public string TimeHeaderTemplate
		{
			get
			{
				return (string)(base.ViewState["TimeHeaderTemplate"] ?? "");
			}
			set
			{
				base.ViewState["TimeHeaderTemplate"] = value;
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06001DD0 RID: 7632 RVA: 0x0005D201 File Offset: 0x0005B401
		// (set) Token: 0x06001DD1 RID: 7633 RVA: 0x0005D221 File Offset: 0x0005B421
		[ClientControlProperty]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[ClientPropertyName("dayHeaderTemplate")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[DefaultValue("")]
		[Description("The client template used to render the header day slots in 'day' and 'week' view.")]
		public string DayHeaderTemplate
		{
			get
			{
				return (string)(base.ViewState["DayHeaderTemplate"] ?? "");
			}
			set
			{
				base.ViewState["DayHeaderTemplate"] = value;
			}
		}
	}
}
