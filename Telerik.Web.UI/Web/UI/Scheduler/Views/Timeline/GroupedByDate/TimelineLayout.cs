using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Timeline.GroupedByDate
{
	// Token: 0x02001A8E RID: 6798
	internal class TimelineLayout : AllDayLayout
	{
		// Token: 0x06010759 RID: 67417 RVA: 0x003ADE07 File Offset: 0x003AC007
		public TimelineLayout(IEnumerable<ISchedulerTimeSlot> slots) : this(slots, true)
		{
		}

		// Token: 0x0601075A RID: 67418 RVA: 0x003ADE11 File Offset: 0x003AC011
		public TimelineLayout(IEnumerable<ISchedulerTimeSlot> slots, bool registerAppointmentControls) : base(slots, registerAppointmentControls)
		{
		}

		// Token: 0x0601075B RID: 67419 RVA: 0x003ADE1C File Offset: 0x003AC01C
		protected override AllDayAppointmentControl CreateAppointmentControl(Appointment appointment, ISchedulerTimeSlot slot, bool registerAppointmentControls)
		{
			TimelineAppointmentControl timelineAppointmentControl = new TimelineAppointmentControl(appointment, slot, registerAppointmentControls);
			this.AddToSizingBlocks(timelineAppointmentControl);
			return timelineAppointmentControl;
		}

		// Token: 0x0601075C RID: 67420 RVA: 0x003ADE3A File Offset: 0x003AC03A
		protected override void AddToSizingBlocks(AllDayAppointmentControl control)
		{
			base.Blocks.Add(new AllDayBlock());
			base.CurrentBlock.Add(control);
		}
	}
}
