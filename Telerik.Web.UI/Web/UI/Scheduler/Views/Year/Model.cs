using System;
using System.Collections.Generic;
using Telerik.Web.UI.Scheduling;

namespace Telerik.Web.UI.Scheduler.Views.Year
{
	// Token: 0x02000857 RID: 2135
	internal class Model : ModelBase
	{
		// Token: 0x170019BF RID: 6591
		// (get) Token: 0x06004EBB RID: 20155 RVA: 0x000F6D0C File Offset: 0x000F4F0C
		// (set) Token: 0x06004EBC RID: 20156 RVA: 0x000F6D14 File Offset: 0x000F4F14
		protected IYearTimeSlotFactory SlotFactory
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

		// Token: 0x06004EBD RID: 20157 RVA: 0x000F6D1D File Offset: 0x000F4F1D
		public Model(IScheduler owner) : this(owner, new TimeSlotFactory())
		{
		}

		// Token: 0x06004EBE RID: 20158 RVA: 0x000F6D2B File Offset: 0x000F4F2B
		public Model(IScheduler owner, IYearTimeSlotFactory slotFactory) : base(owner)
		{
			this.SlotFactory = slotFactory;
		}

		// Token: 0x06004EBF RID: 20159 RVA: 0x000F6D3C File Offset: 0x000F4F3C
		public override void DataBind(AppointmentCollection appointments)
		{
			this.Appointments.Clear();
			base.DaySlots.Clear();
			this.Appointments.AddRange(appointments.GetAppointmentsInRange(this.VisibleRangeStart, this.VisibleRangeEnd));
			DateTime dateTime = this.Owner.UtcToDisplay(this.VisibleRangeStart);
			for (int i = 0; i < base.NumberOfMonths; i++)
			{
				IList<TimeSlot> list = new List<TimeSlot>();
				DateTime selectedDate = dateTime.AddMonths(i);
				DateTime startOfWeek = DateHelper.GetStartOfWeek(selectedDate, this.Owner.FirstDayOfWeek);
				for (int j = 0; j < base.WeeksInMonth; j++)
				{
					DateTime dateTime2 = startOfWeek.AddDays((double)(j * 7));
					for (int k = 0; k < base.WeekLength; k++)
					{
						int dayIndex = j * base.WeekLength + k;
						DateTime date = dateTime2.AddDays((double)k);
						DateTime date2 = date.AddDays(1.0);
						TimeSlot timeSlot = this.CreateDaySlot(this.Owner.DisplayToUtc(date), this.Owner.DisplayToUtc(date2), i, dayIndex);
						if (date.Month != selectedDate.Month)
						{
							timeSlot.IsOtherMonth = (date.Month != selectedDate.Month);
						}
						list.Add(timeSlot);
					}
				}
				base.DaySlots.Add(list);
			}
		}

		// Token: 0x06004EC0 RID: 20160 RVA: 0x000F6E9C File Offset: 0x000F509C
		private TimeSlot CreateDaySlot(DateTime dayStart, DateTime dayEnd, int monthIndex, int dayIndex)
		{
			List<Appointment> list = (List<Appointment>)this.Appointments.GetAppointmentsInRange(dayStart, dayEnd);
			list.Sort(this.Owner.AppointmentComparer);
			TimeSlot timeSlot = this.SlotFactory.CreateTimeSlot(list, this, dayStart, dayEnd);
			timeSlot.MonthIndex = monthIndex;
			timeSlot.DayIndex = dayIndex;
			return timeSlot;
		}

		// Token: 0x06004EC1 RID: 20161 RVA: 0x000F6EED File Offset: 0x000F50ED
		public override ISchedulerRenderer GetRenderer()
		{
			return new Renderer(new View(this));
		}

		// Token: 0x06004EC2 RID: 20162 RVA: 0x000F6EFC File Offset: 0x000F50FC
		public override ISchedulerTimeSlot GetSlotByIndex(string index)
		{
			string[] array = index.Split(new char[]
			{
				':'
			});
			if (array.Length != 2)
			{
				throw new InvalidOperationException("Invalid slot index: " + index);
			}
			int num;
			bool flag = int.TryParse(array[0], out num);
			int num2;
			if (!int.TryParse(array[1], out num2) || !flag)
			{
				throw new InvalidOperationException("Cannot parse slot index: " + index);
			}
			if (num >= base.DaySlots.Count)
			{
				throw new IndexOutOfRangeException("Month index out of range: " + num);
			}
			if (num2 >= base.DaySlots[num].Count)
			{
				throw new IndexOutOfRangeException("Day index out of range: " + num2);
			}
			return base.DaySlots[num][num2];
		}

		// Token: 0x06004EC3 RID: 20163 RVA: 0x000F6FC8 File Offset: 0x000F51C8
		public override ISchedulerTimeSlot GetAppointmentSlot(Appointment appointment)
		{
			foreach (IList<TimeSlot> list in base.DaySlots)
			{
				foreach (TimeSlot timeSlot in list)
				{
					if (timeSlot.ContainsAppointment(appointment))
					{
						return timeSlot;
					}
				}
			}
			return null;
		}

		// Token: 0x06004EC4 RID: 20164 RVA: 0x000F705C File Offset: 0x000F525C
		public override IList<ISchedulerTimeSlot> GetTimeSlots()
		{
			List<ISchedulerTimeSlot> list = new List<ISchedulerTimeSlot>();
			foreach (IList<TimeSlot> list2 in base.DaySlots)
			{
				foreach (TimeSlot item in list2)
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x04001399 RID: 5017
		private IYearTimeSlotFactory _slotFactory;
	}
}
