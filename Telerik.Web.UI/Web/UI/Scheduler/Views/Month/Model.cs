using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Month
{
	// Token: 0x02001A7E RID: 6782
	internal class Model : ModelBase
	{
		// Token: 0x17004FC5 RID: 20421
		// (get) Token: 0x060106C9 RID: 67273 RVA: 0x003AB701 File Offset: 0x003A9901
		// (set) Token: 0x060106CA RID: 67274 RVA: 0x003AB709 File Offset: 0x003A9909
		protected IMonthTimeSlotFactory SlotFactory
		{
			get
			{
				return this._slotFactory;
			}
			set
			{
				this._slotFactory = value;
			}
		}

		// Token: 0x060106CB RID: 67275 RVA: 0x003AB712 File Offset: 0x003A9912
		public Model(IScheduler owner) : this(owner, new TimeSlotFactory())
		{
		}

		// Token: 0x060106CC RID: 67276 RVA: 0x003AB720 File Offset: 0x003A9920
		public Model(IScheduler owner, IMonthTimeSlotFactory slotFactory) : base(owner)
		{
			this.SlotFactory = slotFactory;
		}

		// Token: 0x060106CD RID: 67277 RVA: 0x003AB730 File Offset: 0x003A9930
		public override void DataBind(AppointmentCollection appointments)
		{
			this.Appointments.Clear();
			base.DaySlots.Clear();
			this.Appointments.AddRange(appointments.GetAppointmentsInRange(this.VisibleRangeStart, this.VisibleRangeEnd));
			DateTime dateTime = this.Owner.UtcToDisplay(this.VisibleRangeStart);
			for (int i = 0; i < base.NumberOfWeeks; i++)
			{
				IList<Appointment> trimmedAppointments = new List<Appointment>();
				DateTime date = dateTime.AddDays((double)(i * 7));
				for (int j = 0; j < base.WeekLength; j++)
				{
					int dayIndex = i * base.WeekLength + j;
					DateTime date2 = date.AddDays((double)j);
					DateTime date3 = date2.AddDays(1.0);
					this.CreateDaySlot(this.Owner.DisplayToUtc(date2), this.Owner.DisplayToUtc(date3), dayIndex, this.Owner.DisplayToUtc(date), trimmedAppointments);
				}
			}
		}

		// Token: 0x060106CE RID: 67278 RVA: 0x003AB81C File Offset: 0x003A9A1C
		private void CreateDaySlot(DateTime dayStart, DateTime dayEnd, int dayIndex, DateTime weekStart, IList<Appointment> trimmedAppointments)
		{
			IList<Appointment> appointmentsStartingInRange = this.Appointments.GetAppointmentsStartingInRange(dayStart, dayEnd);
			List<Appointment> list = (List<Appointment>)this.Appointments.GetAppointmentsInRange(dayStart, dayEnd);
			List<Appointment> list2 = new List<Appointment>(appointmentsStartingInRange);
			bool hasMoreAppointments = list.Count - this.Owner.MonthView.VisibleAppointmentsPerDay > 0;
			Appointment[] array = list.ToArray();
			foreach (Appointment appointment in array)
			{
				if ((base.IsGroupedByDate || (weekStart == dayStart && appointment.Start < weekStart)) && !appointmentsStartingInRange.Contains(appointment))
				{
					list2.Add(appointment);
				}
				if (trimmedAppointments.Contains(appointment))
				{
					hasMoreAppointments = true;
					list.Remove(appointment);
				}
			}
			list2.Sort(this.Owner.AppointmentComparer);
			int num = list.Count - this.Owner.MonthView.VisibleAppointmentsPerDay;
			num = Math.Min(num, list2.Count);
			if (num > 0)
			{
				int index = list2.Count - num;
				List<Appointment> range = list2.GetRange(index, num);
				foreach (Appointment item in range)
				{
					trimmedAppointments.Add(item);
				}
				list2.RemoveRange(index, num);
			}
			TimeSlot timeSlot = this.SlotFactory.CreateTimeSlot(list2, this, dayStart, dayEnd);
			base.DaySlots.Add(timeSlot);
			timeSlot.DayIndex = dayIndex;
			timeSlot.DayOfWeek = this.Owner.UtcToDisplay(dayStart).DayOfWeek;
			timeSlot.HasMoreAppointments = hasMoreAppointments;
		}

		// Token: 0x060106CF RID: 67279 RVA: 0x003AB9CC File Offset: 0x003A9BCC
		public override ISchedulerRenderer GetRenderer()
		{
			return new Renderer(new View(this));
		}

		// Token: 0x060106D0 RID: 67280 RVA: 0x003AB9DC File Offset: 0x003A9BDC
		public override void HandleMove(Appointment appointment, ISchedulerTimeSlot sourceSlot, ISchedulerTimeSlot targetSlot, bool editSeries)
		{
			DateTime date = this.Owner.UtcToDisplay(targetSlot.Start) + (this.Owner.UtcToDisplay(appointment.Start) - this.Owner.UtcToDisplay(sourceSlot.Start));
			TimeSpan value = this.Owner.UtcToDisplay(appointment.End) - this.Owner.UtcToDisplay(appointment.Start);
			DateTime end = this.Owner.DisplayToUtc(date.Add(value));
			DateTime start = this.Owner.DisplayToUtc(date);
			this.Owner.HandleMove(appointment, start, end, editSeries, null);
		}

		// Token: 0x060106D1 RID: 67281 RVA: 0x003ABA84 File Offset: 0x003A9C84
		public override void HandleInsert(ISchedulerTimeSlot targetSlot, ISchedulerTimeSlot lastSlot, Appointment appointmentToInsert)
		{
			appointmentToInsert.Start = targetSlot.Start;
			appointmentToInsert.End = ((lastSlot != null) ? lastSlot.End : targetSlot.End);
			if (appointmentToInsert.RecurrenceState == RecurrenceState.Master)
			{
				appointmentToInsert.RecurrenceRule = this.CreateDefaultRecurrenceRule(appointmentToInsert);
			}
			this.Owner.HandleInsert(appointmentToInsert);
			this.Owner.ActiveSlotIndex = targetSlot.Index;
		}

		// Token: 0x060106D2 RID: 67282 RVA: 0x003ABAE8 File Offset: 0x003A9CE8
		public override ISchedulerTimeSlot GetSlotByIndex(string index)
		{
			int num;
			if (!int.TryParse(index, out num))
			{
				throw new InvalidOperationException("Cannot parse slot index: " + index);
			}
			int num2 = base.DaySlots.Count - 1;
			if (num < 0 || num2 < num)
			{
				throw new IndexOutOfRangeException("Index out of range: " + num);
			}
			return base.DaySlots[num];
		}

		// Token: 0x060106D3 RID: 67283 RVA: 0x003ABB48 File Offset: 0x003A9D48
		public override ISchedulerTimeSlot GetAppointmentSlot(Appointment appointment)
		{
			foreach (TimeSlot timeSlot in base.DaySlots)
			{
				if (timeSlot.ContainsAppointment(appointment))
				{
					return timeSlot;
				}
			}
			return null;
		}

		// Token: 0x060106D4 RID: 67284 RVA: 0x003ABBA0 File Offset: 0x003A9DA0
		public override IList<ISchedulerTimeSlot> GetTimeSlots()
		{
			List<ISchedulerTimeSlot> list = new List<ISchedulerTimeSlot>();
			foreach (TimeSlot item in base.DaySlots)
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x040049AA RID: 18858
		private IMonthTimeSlotFactory _slotFactory;
	}
}
