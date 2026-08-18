using System;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000825 RID: 2085
	public class AgendaViewSettings : GroupableViewSettings
	{
		// Token: 0x06004D1D RID: 19741 RVA: 0x000F2A78 File Offset: 0x000F0C78
		internal AgendaViewSettings(IScheduler scheduler, StateBag ownerViewState) : base(scheduler, "AgendaViewSettings", ownerViewState)
		{
		}

		// Token: 0x1700192D RID: 6445
		// (get) Token: 0x06004D1E RID: 19742 RVA: 0x000F2A88 File Offset: 0x000F0C88
		// (set) Token: 0x06004D1F RID: 19743 RVA: 0x000F2AB1 File Offset: 0x000F0CB1
		[Category("Appearance")]
		[DefaultValue(false)]
		[Description("Controls the visibility of the tab for the current view in the view chooser")]
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

		// Token: 0x1700192E RID: 6446
		// (get) Token: 0x06004D20 RID: 19744 RVA: 0x000F2AC9 File Offset: 0x000F0CC9
		// (set) Token: 0x06004D21 RID: 19745 RVA: 0x000F2AE9 File Offset: 0x000F0CE9
		[NotifyParentProperty(true)]
		[DefaultValue("d")]
		[Category("Appearance")]
		[Description("Format string for the date in the Agenda view header (e.g. \"D\", \"yyyy-MM-dd\").")]
		public string HeaderDateFormat
		{
			get
			{
				return ((string)base.ViewState["HeaderDateFormat"]) ?? "d";
			}
			set
			{
				base.ViewState["HeaderDateFormat"] = value;
			}
		}

		// Token: 0x1700192F RID: 6447
		// (get) Token: 0x06004D22 RID: 19746 RVA: 0x000F2AFC File Offset: 0x000F0CFC
		// (set) Token: 0x06004D23 RID: 19747 RVA: 0x000F2B1D File Offset: 0x000F0D1D
		[Category("Appearance")]
		[Description("The number of visible days in agenda view.")]
		[NotifyParentProperty(true)]
		[DefaultValue(7)]
		public int NumberOfDays
		{
			get
			{
				return (int)(base.ViewState["NumberOfDays"] ?? 7);
			}
			set
			{
				base.ViewState["NumberOfDays"] = value;
				base.Owner.NotifyDataPropertyChanged();
			}
		}

		// Token: 0x17001930 RID: 6448
		// (get) Token: 0x06004D24 RID: 19748 RVA: 0x000F2B40 File Offset: 0x000F0D40
		// (set) Token: 0x06004D25 RID: 19749 RVA: 0x000F2B61 File Offset: 0x000F0D61
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[DefaultValue(true)]
		[Description("Controls the visibility of the column headers for the agenda view")]
		public bool ShowColumnHeaders
		{
			get
			{
				return (bool)(base.ViewState["ShowColumnHeaders"] ?? true);
			}
			set
			{
				base.ViewState["ShowColumnHeaders"] = value;
			}
		}

		// Token: 0x17001931 RID: 6449
		// (get) Token: 0x06004D26 RID: 19750 RVA: 0x000F2B79 File Offset: 0x000F0D79
		// (set) Token: 0x06004D27 RID: 19751 RVA: 0x000F2B9E File Offset: 0x000F0D9E
		[Category("Appearance")]
		[DefaultValue(typeof(Unit), "")]
		[Description("The width of the date column")]
		public Unit ResourceColumnWidth
		{
			get
			{
				return (Unit)(base.ViewState["ResourceColumnWidth"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["ResourceColumnWidth"] = value;
			}
		}

		// Token: 0x17001932 RID: 6450
		// (get) Token: 0x06004D28 RID: 19752 RVA: 0x000F2BB6 File Offset: 0x000F0DB6
		// (set) Token: 0x06004D29 RID: 19753 RVA: 0x000F2BDB File Offset: 0x000F0DDB
		[Description("The width of the date column")]
		[DefaultValue(typeof(Unit), "")]
		[Category("Appearance")]
		public Unit DateColumnWidth
		{
			get
			{
				return (Unit)(base.ViewState["DateColumnWidth"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["DateColumnWidth"] = value;
			}
		}

		// Token: 0x17001933 RID: 6451
		// (get) Token: 0x06004D2A RID: 19754 RVA: 0x000F2BF3 File Offset: 0x000F0DF3
		// (set) Token: 0x06004D2B RID: 19755 RVA: 0x000F2C18 File Offset: 0x000F0E18
		[Description("The width of the time column")]
		[Category("Appearance")]
		[DefaultValue(typeof(Unit), "")]
		public Unit TimeColumnWidth
		{
			get
			{
				return (Unit)(base.ViewState["TimeColumnWidth"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["TimeColumnWidth"] = value;
			}
		}

		// Token: 0x17001934 RID: 6452
		// (get) Token: 0x06004D2C RID: 19756 RVA: 0x000F2C30 File Offset: 0x000F0E30
		// (set) Token: 0x06004D2D RID: 19757 RVA: 0x000F2C55 File Offset: 0x000F0E55
		[DefaultValue(typeof(Unit), "")]
		[Description("The width of the appointment column")]
		[Category("Appearance")]
		public Unit AppointmentColumnWidth
		{
			get
			{
				return (Unit)(base.ViewState["AppointmentColumnWidth"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["AppointmentColumnWidth"] = value;
			}
		}

		// Token: 0x17001935 RID: 6453
		// (get) Token: 0x06004D2E RID: 19758 RVA: 0x000F2C6D File Offset: 0x000F0E6D
		// (set) Token: 0x06004D2F RID: 19759 RVA: 0x000F2C8E File Offset: 0x000F0E8E
		[DefaultValue(ResourceMarkerType.None)]
		[Description("Resource marker type.")]
		[Category("Appearance")]
		public ResourceMarkerType ResourceMarkerType
		{
			get
			{
				return (ResourceMarkerType)(base.ViewState["ResourceMarkerType"] ?? ResourceMarkerType.None);
			}
			set
			{
				base.ViewState["ResourceMarkerType"] = value;
			}
		}

		// Token: 0x06004D30 RID: 19760 RVA: 0x000F2CA6 File Offset: 0x000F0EA6
		internal override JavaScriptConverter GetConverter()
		{
			return new AgendaViewSettingsConverter();
		}
	}
}
