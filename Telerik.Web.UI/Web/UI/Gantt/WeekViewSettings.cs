using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x0200035E RID: 862
	public class WeekViewSettings : BaseViewSettings, IWeekViewSettings, IViewSettings
	{
		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06001DBD RID: 7613 RVA: 0x0005D055 File Offset: 0x0005B255
		public override GanttViewType Type
		{
			get
			{
				return GanttViewType.WeekView;
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06001DBE RID: 7614 RVA: 0x0005D058 File Offset: 0x0005B258
		// (set) Token: 0x06001DBF RID: 7615 RVA: 0x0005D078 File Offset: 0x0005B278
		[DefaultValue("ddd M/dd")]
		[Description("The week header date format string in WeekView. It gets overwritten by the WeekHeaderTemplate if the template is not empty.")]
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

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06001DC0 RID: 7616 RVA: 0x0005D08B File Offset: 0x0005B28B
		// (set) Token: 0x06001DC1 RID: 7617 RVA: 0x0005D0AB File Offset: 0x0005B2AB
		[DefaultValue("ddd M/dd")]
		[Description("The day header date format string in WeekView. It gets overwritten by the DayHeaderTemplate if the template is not empty.")]
		[Category("Appearance")]
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

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06001DC2 RID: 7618 RVA: 0x0005D0BE File Offset: 0x0005B2BE
		// (set) Token: 0x06001DC3 RID: 7619 RVA: 0x0005D0DE File Offset: 0x0005B2DE
		[DefaultValue("")]
		[ClientControlProperty]
		[ClientPropertyName("dayHeadertemplate")]
		[Browsable(false)]
		[Description("The client template used to render the header day slots in 'day' view.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
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

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06001DC4 RID: 7620 RVA: 0x0005D0F1 File Offset: 0x0005B2F1
		// (set) Token: 0x06001DC5 RID: 7621 RVA: 0x0005D111 File Offset: 0x0005B311
		[DefaultValue("")]
		[Browsable(false)]
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
	}
}
