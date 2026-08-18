using System;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001A42 RID: 6722
	public class WeekViewSettings : BaseMultiDayViewSettings
	{
		// Token: 0x060104DE RID: 66782 RVA: 0x003A42E2 File Offset: 0x003A24E2
		internal WeekViewSettings(IScheduler scheduler, StateBag ownerViewState) : base(scheduler, "WeekViewSettings", ownerViewState)
		{
		}

		// Token: 0x060104DF RID: 66783 RVA: 0x003A42F1 File Offset: 0x003A24F1
		internal WeekViewSettings(IScheduler owner, string keyPrefix, StateBag ownerViewState) : base(owner, keyPrefix, ownerViewState)
		{
		}

		// Token: 0x17004F1A RID: 20250
		// (get) Token: 0x060104E0 RID: 66784 RVA: 0x003A42FC File Offset: 0x003A24FC
		// (set) Token: 0x060104E1 RID: 66785 RVA: 0x003A431C File Offset: 0x003A251C
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[DefaultValue("d")]
		[Description("Format string for the date in the week view header (e.g. \"D\", \"yyyy-MM-dd\").")]
		public override string HeaderDateFormat
		{
			get
			{
				return (string)(base.ViewState["HeaderDateFormat"] ?? "d");
			}
			set
			{
				base.ViewState["HeaderDateFormat"] = value;
			}
		}

		// Token: 0x17004F1B RID: 20251
		// (get) Token: 0x060104E2 RID: 66786 RVA: 0x003A432F File Offset: 0x003A252F
		// (set) Token: 0x060104E3 RID: 66787 RVA: 0x003A434F File Offset: 0x003A254F
		[DefaultValue("ddd, d")]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[Description("Format string for the date in the week column header (e.g. \"ddd, d\").")]
		public virtual string ColumnHeaderDateFormat
		{
			get
			{
				return (string)(base.ViewState["ColumnHeaderDateFormat"] ?? "ddd, d");
			}
			set
			{
				base.ViewState["ColumnHeaderDateFormat"] = value;
			}
		}

		// Token: 0x060104E4 RID: 66788 RVA: 0x003A4362 File Offset: 0x003A2562
		internal override JavaScriptConverter GetConverter()
		{
			return new WeekViewSettingsConverter();
		}
	}
}
