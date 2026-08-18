using System;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000820 RID: 2080
	public class YearViewSettings : GroupableViewSettings
	{
		// Token: 0x06004CCE RID: 19662 RVA: 0x000F153B File Offset: 0x000EF73B
		internal YearViewSettings(IScheduler scheduler, StateBag ownerViewState) : base(scheduler, "YearViewSettings", ownerViewState)
		{
		}

		// Token: 0x17001914 RID: 6420
		// (get) Token: 0x06004CCF RID: 19663 RVA: 0x000F154C File Offset: 0x000EF74C
		// (set) Token: 0x06004CD0 RID: 19664 RVA: 0x000F1575 File Offset: 0x000EF775
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Description("Controls the visibility of the tab for the current view in the view chooser")]
		public override bool UserSelectable
		{
			get
			{
				object obj = base.ViewState["UserSelectable"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["UserSelectable"] = value;
			}
		}

		// Token: 0x17001915 RID: 6421
		// (get) Token: 0x06004CD1 RID: 19665 RVA: 0x000F158D File Offset: 0x000EF78D
		// (set) Token: 0x06004CD2 RID: 19666 RVA: 0x000F15AD File Offset: 0x000EF7AD
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[Description("Format string for the date in the Year view header (e.g. \"yy\", \"yyyy\").")]
		[DefaultValue("yyyy")]
		public string HeaderDateFormat
		{
			get
			{
				return ((string)base.ViewState["HeaderDateFormat"]) ?? "yyyy";
			}
			set
			{
				base.ViewState["HeaderDateFormat"] = value;
			}
		}

		// Token: 0x17001916 RID: 6422
		// (get) Token: 0x06004CD3 RID: 19667 RVA: 0x000F15C0 File Offset: 0x000EF7C0
		// (set) Token: 0x06004CD4 RID: 19668 RVA: 0x000F15E1 File Offset: 0x000EF7E1
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[Description("Controls the visibility of the month headers for the year view")]
		[Category("Appearance")]
		public bool ShowMonthHeaders
		{
			get
			{
				return (bool)(base.ViewState["ShowMonthHeaders"] ?? true);
			}
			set
			{
				base.ViewState["ShowMonthHeaders"] = value;
			}
		}

		// Token: 0x17001917 RID: 6423
		// (get) Token: 0x06004CD5 RID: 19669 RVA: 0x000F15F9 File Offset: 0x000EF7F9
		// (set) Token: 0x06004CD6 RID: 19670 RVA: 0x000F1619 File Offset: 0x000EF819
		[NotifyParentProperty(true)]
		[DefaultValue("dd")]
		[Category("Appearance")]
		[Description("The day header date format string in Year View (e.g. \"dd\").")]
		public string DayHeaderDateFormat
		{
			get
			{
				return (string)(base.ViewState["DayHeaderDateFormat"] ?? "dd");
			}
			set
			{
				base.ViewState["DayHeaderDateFormat"] = value;
			}
		}

		// Token: 0x17001918 RID: 6424
		// (get) Token: 0x06004CD7 RID: 19671 RVA: 0x000F162C File Offset: 0x000EF82C
		// (set) Token: 0x06004CD8 RID: 19672 RVA: 0x000F164C File Offset: 0x000EF84C
		[NotifyParentProperty(true)]
		[DefaultValue("ddd")]
		[Description("The column header date format string in Year View (e.g. \"ddd\").")]
		[Category("Appearance")]
		public string ColumnHeaderDateFormat
		{
			get
			{
				return (string)(base.ViewState["ColumnHeaderDateFormat"] ?? "ddd");
			}
			set
			{
				base.ViewState["ColumnHeaderDateFormat"] = value;
			}
		}

		// Token: 0x17001919 RID: 6425
		// (get) Token: 0x06004CD9 RID: 19673 RVA: 0x000F165F File Offset: 0x000EF85F
		// (set) Token: 0x06004CDA RID: 19674 RVA: 0x000F167F File Offset: 0x000EF87F
		[NotifyParentProperty(true)]
		[Description("The month header date format string in Year View (e.g. \"MMMM\").")]
		[Category("Appearance")]
		[DefaultValue("MMMM")]
		public string MonthHeaderDateFormat
		{
			get
			{
				return (string)(base.ViewState["MonthHeaderDateFormat"] ?? "MMMM");
			}
			set
			{
				base.ViewState["MonthHeaderDateFormat"] = value;
			}
		}

		// Token: 0x06004CDB RID: 19675 RVA: 0x000F1692 File Offset: 0x000EF892
		internal override JavaScriptConverter GetConverter()
		{
			return new YearViewSettingsConverter();
		}
	}
}
