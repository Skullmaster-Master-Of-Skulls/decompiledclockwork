using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI.Scheduler.Views
{
	// Token: 0x0200082C RID: 2092
	internal interface ISchedulerModel
	{
		// Token: 0x17001946 RID: 6470
		// (get) Token: 0x06004D57 RID: 19799
		IScheduler Owner { get; }

		// Token: 0x17001947 RID: 6471
		// (get) Token: 0x06004D58 RID: 19800
		AppointmentCollection Appointments { get; }

		// Token: 0x17001948 RID: 6472
		// (get) Token: 0x06004D59 RID: 19801
		DateTime SelectedDate { get; }

		// Token: 0x17001949 RID: 6473
		// (get) Token: 0x06004D5A RID: 19802
		DateTime NextPeriodDate { get; }

		// Token: 0x1700194A RID: 6474
		// (get) Token: 0x06004D5B RID: 19803
		DateTime PreviousPeriodDate { get; }

		// Token: 0x1700194B RID: 6475
		// (get) Token: 0x06004D5C RID: 19804
		DateTime VisibleRangeStart { get; }

		// Token: 0x1700194C RID: 6476
		// (get) Token: 0x06004D5D RID: 19805
		DateTime VisibleRangeEnd { get; }

		// Token: 0x1700194D RID: 6477
		// (get) Token: 0x06004D5E RID: 19806
		bool ReadOnly { get; }

		// Token: 0x1700194E RID: 6478
		// (get) Token: 0x06004D5F RID: 19807
		bool EnableExactTimeRendering { get; }

		// Token: 0x1700194F RID: 6479
		// (get) Token: 0x06004D60 RID: 19808
		string CssClass { get; }

		// Token: 0x06004D61 RID: 19809
		IEnumerable<ScriptReference> GetScriptReferences();

		// Token: 0x06004D62 RID: 19810
		void DataBind(AppointmentCollection appointments);

		// Token: 0x06004D63 RID: 19811
		ISchedulerRenderer GetRenderer();

		// Token: 0x06004D64 RID: 19812
		void ProcessPostBackCommand(SchedulerPostBackEvent postBack);

		// Token: 0x06004D65 RID: 19813
		ISchedulerTimeSlot GetSlotByIndex(string index);

		// Token: 0x06004D66 RID: 19814
		ISchedulerTimeSlot GetAppointmentSlot(Appointment appointment);

		// Token: 0x06004D67 RID: 19815
		IList<ISchedulerTimeSlot> GetTimeSlots();

		// Token: 0x06004D68 RID: 19816
		Dictionary<string, ContextMenuAction> GetTimeSlotContextMenuCommands();

		// Token: 0x06004D69 RID: 19817
		void HandleInsert(ISchedulerTimeSlot targetSlot, ISchedulerTimeSlot lastSlot, Appointment appointmentToInsert);

		// Token: 0x06004D6A RID: 19818
		IList<RadMenuItem> GetTimeSlotContextMenuItems();

		// Token: 0x06004D6B RID: 19819
		void DescribeModelData(string propertyName, JavaScriptSerializer serializer, IScriptDescriptor descriptor);
	}
}
