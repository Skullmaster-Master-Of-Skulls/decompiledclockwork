using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Agenda
{
	// Token: 0x02000841 RID: 2113
	internal class Model : ModelBase
	{
		// Token: 0x1700198D RID: 6541
		// (get) Token: 0x06004E23 RID: 20003 RVA: 0x000F4D6F File Offset: 0x000F2F6F
		// (set) Token: 0x06004E24 RID: 20004 RVA: 0x000F4D77 File Offset: 0x000F2F77
		protected IAgendaTimeSlotFactory SlotFactory { get; set; }

		// Token: 0x1700198E RID: 6542
		// (get) Token: 0x06004E25 RID: 20005 RVA: 0x000F4D80 File Offset: 0x000F2F80
		// (set) Token: 0x06004E26 RID: 20006 RVA: 0x000F4D88 File Offset: 0x000F2F88
		public IList<TimeSlot> DaySlots { get; protected set; }

		// Token: 0x06004E27 RID: 20007 RVA: 0x000F4D91 File Offset: 0x000F2F91
		public Model(IScheduler owner) : this(owner, new TimeSlotFactory())
		{
		}

		// Token: 0x06004E28 RID: 20008 RVA: 0x000F4D9F File Offset: 0x000F2F9F
		public Model(IScheduler owner, IAgendaTimeSlotFactory slotFactory) : base(owner)
		{
			this.DaySlots = new List<TimeSlot>();
			this.SlotFactory = slotFactory;
		}

		// Token: 0x06004E29 RID: 20009 RVA: 0x000F4DBC File Offset: 0x000F2FBC
		public override void DataBind(AppointmentCollection appointments)
		{
			this.Appointments.Clear();
			this.DaySlots.Clear();
			this.Appointments.AddRange(appointments.GetAppointmentsInRange(this.VisibleRangeStart, this.VisibleRangeEnd));
			DateTime date = this.Owner.UtcToDisplay(this.VisibleRangeStart);
			DateTime dateTime = date.AddDays(1.0);
			for (int i = 0; i < base.NumberOfDays; i++)
			{
				this.CreateDaySlot(this.Owner.DisplayToUtc(date), this.Owner.DisplayToUtc(dateTime), i);
				date = dateTime;
				dateTime = dateTime.AddDays(1.0);
			}
		}

		// Token: 0x06004E2A RID: 20010 RVA: 0x000F4E64 File Offset: 0x000F3064
		private void CreateDaySlot(DateTime dayStart, DateTime dayEnd, int dayIndex)
		{
			List<Appointment> list = (List<Appointment>)this.Appointments.GetAppointmentsInRange(dayStart, dayEnd);
			list.Sort(this.Owner.AppointmentComparer);
			TimeSlot timeSlot = this.SlotFactory.CreateTimeSlot(list, this, dayStart, dayEnd);
			this.DaySlots.Add(timeSlot);
			timeSlot.DayIndex = dayIndex;
		}

		// Token: 0x06004E2B RID: 20011 RVA: 0x000F4EB8 File Offset: 0x000F30B8
		public override ISchedulerRenderer GetRenderer()
		{
			View view;
			if (this.Owner.AgendaView.GroupingDirectionResolved == GroupingDirection.Vertical)
			{
				view = new VerticalView(this);
			}
			else
			{
				view = new HorizontalView(this);
			}
			return new Renderer(view);
		}

		// Token: 0x06004E2C RID: 20012 RVA: 0x000F4EF0 File Offset: 0x000F30F0
		public override ISchedulerTimeSlot GetSlotByIndex(string index)
		{
			int num;
			if (!int.TryParse(index, out num))
			{
				throw new InvalidOperationException("Cannot parse slot index: " + index);
			}
			int num2 = this.DaySlots.Count - 1;
			if (num < 0 || num2 < num)
			{
				throw new IndexOutOfRangeException("Index out of range: " + num);
			}
			return this.DaySlots[num];
		}

		// Token: 0x06004E2D RID: 20013 RVA: 0x000F4F50 File Offset: 0x000F3150
		public override ISchedulerTimeSlot GetAppointmentSlot(Appointment appointment)
		{
			foreach (TimeSlot timeSlot in this.DaySlots)
			{
				if (timeSlot.ContainsAppointment(appointment))
				{
					return timeSlot;
				}
			}
			return null;
		}

		// Token: 0x06004E2E RID: 20014 RVA: 0x000F4FA8 File Offset: 0x000F31A8
		public override IList<ISchedulerTimeSlot> GetTimeSlots()
		{
			List<ISchedulerTimeSlot> list = new List<ISchedulerTimeSlot>();
			foreach (TimeSlot item in this.DaySlots)
			{
				list.Add(item);
			}
			return list;
		}
	}
}
