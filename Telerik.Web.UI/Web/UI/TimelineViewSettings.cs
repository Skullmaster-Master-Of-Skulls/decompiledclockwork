using System;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001A47 RID: 6727
	[XmlRoot("Group")]
	public class TimelineViewSettings : GroupableViewSettings
	{
		// Token: 0x06010506 RID: 66822 RVA: 0x003A4633 File Offset: 0x003A2833
		internal TimelineViewSettings(IScheduler owner, StateBag viewState) : base(owner, "TimelineViewSettings", viewState)
		{
		}

		// Token: 0x17004F2A RID: 20266
		// (get) Token: 0x06010507 RID: 66823 RVA: 0x003A4642 File Offset: 0x003A2842
		// (set) Token: 0x06010508 RID: 66824 RVA: 0x003A4670 File Offset: 0x003A2870
		[NotifyParentProperty(true)]
		[Description("The starting time of the Timeline view.")]
		[DefaultValue(typeof(TimeSpan), "00:00:00")]
		public TimeSpan StartTime
		{
			get
			{
				return (TimeSpan)(base.ViewState["StartTime"] ?? TimeSpan.FromHours(0.0));
			}
			set
			{
				base.ViewState["StartTime"] = value;
				base.Owner.NotifyDataPropertyChanged();
			}
		}

		// Token: 0x17004F2B RID: 20267
		// (get) Token: 0x06010509 RID: 66825 RVA: 0x003A4693 File Offset: 0x003A2893
		// (set) Token: 0x0601050A RID: 66826 RVA: 0x003A46B4 File Offset: 0x003A28B4
		[NotifyParentProperty(true)]
		[DefaultValue(3)]
		[Description("The number of slots to display in timeline view.")]
		public int NumberOfSlots
		{
			get
			{
				return (int)(base.ViewState["NumberOfSlots"] ?? 3);
			}
			set
			{
				base.ViewState["NumberOfSlots"] = value;
				base.Owner.NotifyDataPropertyChanged();
			}
		}

		// Token: 0x17004F2C RID: 20268
		// (get) Token: 0x0601050B RID: 66827 RVA: 0x003A46D7 File Offset: 0x003A28D7
		// (set) Token: 0x0601050C RID: 66828 RVA: 0x003A4705 File Offset: 0x003A2905
		[DefaultValue(typeof(TimeSpan), "1.00:00:00")]
		[Description("The duration of each slot in timeline view.")]
		[NotifyParentProperty(true)]
		public TimeSpan SlotDuration
		{
			get
			{
				return (TimeSpan)(base.ViewState["SlotDuration"] ?? TimeSpan.FromDays(1.0));
			}
			set
			{
				base.ViewState["SlotDuration"] = value;
				base.Owner.NotifyDataPropertyChanged();
			}
		}

		// Token: 0x17004F2D RID: 20269
		// (get) Token: 0x0601050D RID: 66829 RVA: 0x003A4728 File Offset: 0x003A2928
		// (set) Token: 0x0601050E RID: 66830 RVA: 0x003A4748 File Offset: 0x003A2948
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[DefaultValue("d")]
		[Description("Format string for the date in the Timeline view header (e.g. \"D\", \"yyyy-MM-dd\").")]
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

		// Token: 0x17004F2E RID: 20270
		// (get) Token: 0x0601050F RID: 66831 RVA: 0x003A475B File Offset: 0x003A295B
		// (set) Token: 0x06010510 RID: 66832 RVA: 0x003A477B File Offset: 0x003A297B
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[DefaultValue("d")]
		[Description("Format string for the date in the timeline column header (e.g. \"D\", \"yyyy-MM-dd\").")]
		public string ColumnHeaderDateFormat
		{
			get
			{
				return (string)(base.ViewState["ColumnHeaderDateFormat"] ?? "d");
			}
			set
			{
				base.ViewState["ColumnHeaderDateFormat"] = value;
			}
		}

		// Token: 0x17004F2F RID: 20271
		// (get) Token: 0x06010511 RID: 66833 RVA: 0x003A478E File Offset: 0x003A298E
		// (set) Token: 0x06010512 RID: 66834 RVA: 0x003A47AF File Offset: 0x003A29AF
		[NotifyParentProperty(true)]
		[DefaultValue(1)]
		[Category("Behavior")]
		[Description("Specifies how many rows/columns a time lable should span.")]
		public int TimeLabelSpan
		{
			get
			{
				return (int)(base.ViewState["TimeLabelSpan"] ?? 1);
			}
			set
			{
				base.ViewState["TimeLabelSpan"] = value;
			}
		}

		// Token: 0x17004F30 RID: 20272
		// (get) Token: 0x06010513 RID: 66835 RVA: 0x003A47C7 File Offset: 0x003A29C7
		// (set) Token: 0x06010514 RID: 66836 RVA: 0x003A47E8 File Offset: 0x003A29E8
		[Category("Behavior")]
		[DefaultValue(AppointmentSortingMode.PerSlot)]
		[Description("Specifies the sorting mode to use when rendering the appointments.")]
		[NotifyParentProperty(true)]
		public AppointmentSortingMode SortingMode
		{
			get
			{
				return (AppointmentSortingMode)(base.ViewState["SortingMode"] ?? AppointmentSortingMode.PerSlot);
			}
			set
			{
				base.ViewState["SortingMode"] = value;
			}
		}

		// Token: 0x17004F31 RID: 20273
		// (get) Token: 0x06010515 RID: 66837 RVA: 0x003A4800 File Offset: 0x003A2A00
		// (set) Token: 0x06010516 RID: 66838 RVA: 0x003A4821 File Offset: 0x003A2A21
		[DefaultValue(true)]
		[Category("Behavior")]
		[Description("Specifies whether to show an empty area at the end of each time slot that can be used to insert appointments.")]
		[NotifyParentProperty(true)]
		public bool ShowInsertArea
		{
			get
			{
				return (bool)(base.ViewState["ShowInsertArea"] ?? true);
			}
			set
			{
				base.ViewState["ShowInsertArea"] = value;
			}
		}

		// Token: 0x17004F32 RID: 20274
		// (get) Token: 0x06010517 RID: 66839 RVA: 0x003A4839 File Offset: 0x003A2A39
		// (set) Token: 0x06010518 RID: 66840 RVA: 0x003A485A File Offset: 0x003A2A5A
		[Description("Gets or sets a value indicating whether the appointment start and end time should be rendered exactly")]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public bool EnableExactTimeRendering
		{
			get
			{
				return (bool)(base.ViewState["EnableExactTimeRendering"] ?? false);
			}
			set
			{
				base.ViewState["EnableExactTimeRendering"] = value;
			}
		}

		// Token: 0x06010519 RID: 66841 RVA: 0x003A4872 File Offset: 0x003A2A72
		internal override JavaScriptConverter GetConverter()
		{
			return new TimeLineViewSettingsConverter();
		}

		// Token: 0x17004F33 RID: 20275
		// (get) Token: 0x0601051A RID: 66842 RVA: 0x003A4879 File Offset: 0x003A2A79
		protected internal bool EnableExactTimeRenderingResolved
		{
			get
			{
				return (bool)(base.ViewState["EnableExactTimeRendering"] ?? base.Owner.EnableExactTimeRendering);
			}
		}
	}
}
