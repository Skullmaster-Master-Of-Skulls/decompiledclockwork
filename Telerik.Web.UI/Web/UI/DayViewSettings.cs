using System;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001A44 RID: 6724
	public class DayViewSettings : BaseMultiDayViewSettings
	{
		// Token: 0x060104EF RID: 66799 RVA: 0x003A4428 File Offset: 0x003A2628
		internal DayViewSettings(IScheduler scheduler, StateBag ownerViewState) : base(scheduler, "DayViewSettings", ownerViewState)
		{
		}

		// Token: 0x17004F20 RID: 20256
		// (get) Token: 0x060104F0 RID: 66800 RVA: 0x003A4437 File Offset: 0x003A2637
		internal override bool ShowDateHeadersResolved
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17004F21 RID: 20257
		// (get) Token: 0x060104F1 RID: 66801 RVA: 0x003A443A File Offset: 0x003A263A
		// (set) Token: 0x060104F2 RID: 66802 RVA: 0x003A445A File Offset: 0x003A265A
		[NotifyParentProperty(true)]
		[Description("Format string for the date in the day view header (e.g. \"D\", \"yyyy-MM-dd\").")]
		[Category("Appearance")]
		[DefaultValue("D")]
		public override string HeaderDateFormat
		{
			get
			{
				return (string)(base.ViewState["HeaderDateFormat"] ?? "D");
			}
			set
			{
				base.ViewState["HeaderDateFormat"] = value;
			}
		}

		// Token: 0x17004F22 RID: 20258
		// (get) Token: 0x060104F3 RID: 66803 RVA: 0x003A446D File Offset: 0x003A266D
		// (set) Token: 0x060104F4 RID: 66804 RVA: 0x003A4475 File Offset: 0x003A2675
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool ShowDateHeaders
		{
			get
			{
				return base.ShowDateHeaders;
			}
			set
			{
				base.ShowDateHeaders = value;
			}
		}

		// Token: 0x060104F5 RID: 66805 RVA: 0x003A447E File Offset: 0x003A267E
		internal override JavaScriptConverter GetConverter()
		{
			return new DayViewSettingsConverter();
		}
	}
}
