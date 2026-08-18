using System;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001A43 RID: 6723
	public class MultiDayViewSettings : WeekViewSettings
	{
		// Token: 0x060104E5 RID: 66789 RVA: 0x003A4369 File Offset: 0x003A2569
		internal MultiDayViewSettings(IScheduler scheduler, StateBag ownerViewState) : base(scheduler, "MultiDayViewSettings", ownerViewState)
		{
		}

		// Token: 0x17004F1C RID: 20252
		// (get) Token: 0x060104E6 RID: 66790 RVA: 0x003A4378 File Offset: 0x003A2578
		// (set) Token: 0x060104E7 RID: 66791 RVA: 0x003A4380 File Offset: 0x003A2580
		[Description("Format string for the date in the multi-day view header (e.g. \"D\", \"yyyy-MM-dd\").")]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[DefaultValue("d")]
		public override string HeaderDateFormat
		{
			get
			{
				return base.HeaderDateFormat;
			}
			set
			{
				base.HeaderDateFormat = value;
			}
		}

		// Token: 0x17004F1D RID: 20253
		// (get) Token: 0x060104E8 RID: 66792 RVA: 0x003A4389 File Offset: 0x003A2589
		// (set) Token: 0x060104E9 RID: 66793 RVA: 0x003A4391 File Offset: 0x003A2591
		[Description("Format string for the date in the multi-day column header (e.g. \"ddd, d\").")]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[DefaultValue("ddd, d")]
		public override string ColumnHeaderDateFormat
		{
			get
			{
				return base.ColumnHeaderDateFormat;
			}
			set
			{
				base.ColumnHeaderDateFormat = value;
			}
		}

		// Token: 0x17004F1E RID: 20254
		// (get) Token: 0x060104EA RID: 66794 RVA: 0x003A439A File Offset: 0x003A259A
		// (set) Token: 0x060104EB RID: 66795 RVA: 0x003A43BB File Offset: 0x003A25BB
		[Description("The number of visible days in multi-day view.")]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[DefaultValue(5)]
		public int NumberOfDays
		{
			get
			{
				return (int)(base.ViewState["NumberOfDays"] ?? 5);
			}
			set
			{
				base.ViewState["NumberOfDays"] = value;
				base.Owner.NotifyDataPropertyChanged();
			}
		}

		// Token: 0x17004F1F RID: 20255
		// (get) Token: 0x060104EC RID: 66796 RVA: 0x003A43E0 File Offset: 0x003A25E0
		// (set) Token: 0x060104ED RID: 66797 RVA: 0x003A4409 File Offset: 0x003A2609
		[Category("Appearance")]
		[Description("Controls the visibility of the tab for the current view in the view chooser")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
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

		// Token: 0x060104EE RID: 66798 RVA: 0x003A4421 File Offset: 0x003A2621
		internal override JavaScriptConverter GetConverter()
		{
			return new MultiDayViewSettingsConverter();
		}
	}
}
