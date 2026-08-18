using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Timeline
{
	// Token: 0x02001A99 RID: 6809
	internal class Model : ModelBase
	{
		// Token: 0x17004FF9 RID: 20473
		// (get) Token: 0x06010784 RID: 67460 RVA: 0x003AE85A File Offset: 0x003ACA5A
		// (set) Token: 0x06010785 RID: 67461 RVA: 0x003AE862 File Offset: 0x003ACA62
		public AppointmentFilter AppointmentFilter { get; set; }

		// Token: 0x17004FFA RID: 20474
		// (get) Token: 0x06010786 RID: 67462 RVA: 0x003AE86B File Offset: 0x003ACA6B
		// (set) Token: 0x06010787 RID: 67463 RVA: 0x003AE873 File Offset: 0x003ACA73
		protected ITimelineTimeSlotFactory SlotFactory { get; set; }

		// Token: 0x06010788 RID: 67464 RVA: 0x003AE87C File Offset: 0x003ACA7C
		public Model(IScheduler owner) : this(owner, new TimeSlotFactory())
		{
		}

		// Token: 0x06010789 RID: 67465 RVA: 0x003AE88A File Offset: 0x003ACA8A
		public Model(IScheduler owner, ITimelineTimeSlotFactory slotFactory) : base(owner)
		{
			this.SlotFactory = slotFactory;
			this.AppointmentFilter = new AppointmentFilter();
		}

		// Token: 0x0601078A RID: 67466 RVA: 0x003AE8A8 File Offset: 0x003ACAA8
		public override void DataBind(AppointmentCollection appointments)
		{
			this.Appointments.Clear();
			base.IntervalSlots.Clear();
			this.Appointments.AddRange(appointments.GetAppointmentsInRange(this.VisibleRangeStart, this.VisibleRangeEnd));
			TimeSpan slotDuration = this.Owner.TimelineView.SlotDuration;
			DateTime date = this.Owner.UtcToDisplay(this.VisibleRangeStart);
			DateTime date2 = date.Add(slotDuration);
			for (int i = 0; i < this.Owner.TimelineView.NumberOfSlots; i++)
			{
				this.CreateSlot(this.Owner.DisplayToUtc(date), this.Owner.DisplayToUtc(date2), i);
				date = date.Add(slotDuration);
				date2 = date.Add(slotDuration);
			}
		}

		// Token: 0x0601078B RID: 67467 RVA: 0x003AE960 File Offset: 0x003ACB60
		private void CreateSlot(DateTime intervalStart, DateTime intervalEnd, int intervalIndex)
		{
			IList<Appointment> appointments = this.AppointmentFilter.GetAppointments(intervalStart, intervalEnd, base.IntervalSlots, this.Appointments, this.Owner.AppointmentComparer);
			TimeSlot timeSlot = this.SlotFactory.CreateTimeSlot(appointments, this, intervalStart, intervalEnd);
			base.IntervalSlots.Add(timeSlot);
			timeSlot.IntervalIndex = intervalIndex;
		}

		// Token: 0x0601078C RID: 67468 RVA: 0x003AE9B8 File Offset: 0x003ACBB8
		public override void HandleMove(Appointment appointment, ISchedulerTimeSlot sourceSlot, ISchedulerTimeSlot targetSlot, bool editSeries)
		{
			DateTime date = this.Owner.UtcToDisplay(targetSlot.Start) + (this.Owner.UtcToDisplay(appointment.Start) - this.Owner.UtcToDisplay(sourceSlot.Start));
			TimeSpan value = this.Owner.UtcToDisplay(appointment.End) - this.Owner.UtcToDisplay(appointment.Start);
			DateTime end = this.Owner.DisplayToUtc(date.Add(value));
			DateTime start = this.Owner.DisplayToUtc(date);
			this.Owner.HandleMove(appointment, start, end, editSeries, null);
		}

		// Token: 0x0601078D RID: 67469 RVA: 0x003AEA60 File Offset: 0x003ACC60
		public override void HandleInsert(ISchedulerTimeSlot targetSlot, ISchedulerTimeSlot lastSlot, Appointment appointmentToInsert)
		{
			appointmentToInsert.Start = targetSlot.Start;
			appointmentToInsert.End = targetSlot.End;
			if (appointmentToInsert.RecurrenceState == RecurrenceState.Master)
			{
				appointmentToInsert.RecurrenceRule = this.CreateDefaultRecurrenceRule(appointmentToInsert);
			}
			this.Owner.HandleInsert(appointmentToInsert);
			this.Owner.ActiveSlotIndex = targetSlot.Index;
		}

		// Token: 0x0601078E RID: 67470 RVA: 0x003AEAB8 File Offset: 0x003ACCB8
		public override void HandleResize(Appointment appointment, ISchedulerTimeSlot sourceSlot, DateTime appointmentStart, DateTime appointmentEnd, bool editSeries)
		{
			this.Owner.HandleResize(appointment, appointmentStart, appointmentEnd, editSeries);
		}

		// Token: 0x0601078F RID: 67471 RVA: 0x003AEACB File Offset: 0x003ACCCB
		public override ISchedulerRenderer GetRenderer()
		{
			return new Renderer(new View(this));
		}

		// Token: 0x06010790 RID: 67472 RVA: 0x003AEAD8 File Offset: 0x003ACCD8
		public override ISchedulerTimeSlot GetSlotByIndex(string index)
		{
			int num;
			if (!int.TryParse(index, out num))
			{
				throw new InvalidOperationException("Cannot parse slot index: " + index);
			}
			int num2 = base.IntervalSlots.Count - 1;
			if (num < 0 || num2 < num)
			{
				throw new IndexOutOfRangeException("Index out of range: " + num);
			}
			return base.IntervalSlots[num];
		}

		// Token: 0x06010791 RID: 67473 RVA: 0x003AEB38 File Offset: 0x003ACD38
		public override ISchedulerTimeSlot GetAppointmentSlot(Appointment appointment)
		{
			foreach (TimeSlot timeSlot in base.IntervalSlots)
			{
				if (timeSlot.ContainsAppointment(appointment))
				{
					return timeSlot;
				}
			}
			return null;
		}

		// Token: 0x06010792 RID: 67474 RVA: 0x003AEB90 File Offset: 0x003ACD90
		public override IList<ISchedulerTimeSlot> GetTimeSlots()
		{
			List<ISchedulerTimeSlot> list = new List<ISchedulerTimeSlot>();
			foreach (TimeSlot item in base.IntervalSlots)
			{
				list.Add(item);
			}
			return list;
		}
	}
}
