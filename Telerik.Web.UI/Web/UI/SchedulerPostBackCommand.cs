using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001312 RID: 4882
	[EditorBrowsable(EditorBrowsableState.Never)]
	public enum SchedulerPostBackCommand
	{
		// Token: 0x04003589 RID: 13705
		Insert,
		// Token: 0x0400358A RID: 13706
		InsertAppointment,
		// Token: 0x0400358B RID: 13707
		Resize,
		// Token: 0x0400358C RID: 13708
		Edit,
		// Token: 0x0400358D RID: 13709
		Move,
		// Token: 0x0400358E RID: 13710
		MoveToAllDay,
		// Token: 0x0400358F RID: 13711
		Delete,
		// Token: 0x04003590 RID: 13712
		Click,
		// Token: 0x04003591 RID: 13713
		GoToPrevious,
		// Token: 0x04003592 RID: 13714
		GoToNext,
		// Token: 0x04003593 RID: 13715
		GoToToday,
		// Token: 0x04003594 RID: 13716
		SwitchFullTime,
		// Token: 0x04003595 RID: 13717
		SwitchToDayView,
		// Token: 0x04003596 RID: 13718
		SwitchToWeekView,
		// Token: 0x04003597 RID: 13719
		SwitchToMonthView,
		// Token: 0x04003598 RID: 13720
		SwitchToTimelineView,
		// Token: 0x04003599 RID: 13721
		SwitchToMultiDayView,
		// Token: 0x0400359A RID: 13722
		SwitchToAgendaView,
		// Token: 0x0400359B RID: 13723
		SwitchToYearView,
		// Token: 0x0400359C RID: 13724
		SwitchToSelectedDay,
		// Token: 0x0400359D RID: 13725
		SwitchToSelectedMonth,
		// Token: 0x0400359E RID: 13726
		NavigateToNextPeriod,
		// Token: 0x0400359F RID: 13727
		NavigateToPreviousPeriod,
		// Token: 0x040035A0 RID: 13728
		NavigateToSelectedDate,
		// Token: 0x040035A1 RID: 13729
		UpdateAppointment,
		// Token: 0x040035A2 RID: 13730
		AdvancedInsert,
		// Token: 0x040035A3 RID: 13731
		AdvancedInsertRecurring,
		// Token: 0x040035A4 RID: 13732
		AdvancedEdit,
		// Token: 0x040035A5 RID: 13733
		ContextMenuDelete,
		// Token: 0x040035A6 RID: 13734
		ContextMenuEdit,
		// Token: 0x040035A7 RID: 13735
		ContextMenuTimeSlotCommand,
		// Token: 0x040035A8 RID: 13736
		ContextMenuAppointmentCommand
	}
}
