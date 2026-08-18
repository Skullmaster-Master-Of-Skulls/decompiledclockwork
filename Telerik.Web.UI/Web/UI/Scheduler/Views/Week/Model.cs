using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;

namespace Telerik.Web.UI.Scheduler.Views.Week
{
	// Token: 0x02001A6A RID: 6762
	internal class Model : ModelBase
	{
		// Token: 0x17004F93 RID: 20371
		// (get) Token: 0x0601062A RID: 67114 RVA: 0x003A8B53 File Offset: 0x003A6D53
		// (set) Token: 0x0601062B RID: 67115 RVA: 0x003A8B5B File Offset: 0x003A6D5B
		public AppointmentFilter AppointmentFilter { get; set; }

		// Token: 0x17004F94 RID: 20372
		// (get) Token: 0x0601062C RID: 67116 RVA: 0x003A8B64 File Offset: 0x003A6D64
		// (set) Token: 0x0601062D RID: 67117 RVA: 0x003A8B6C File Offset: 0x003A6D6C
		public IList<IList<TimeSlot>> DaySlots { get; protected set; }

		// Token: 0x17004F95 RID: 20373
		// (get) Token: 0x0601062E RID: 67118 RVA: 0x003A8B75 File Offset: 0x003A6D75
		// (set) Token: 0x0601062F RID: 67119 RVA: 0x003A8B7D File Offset: 0x003A6D7D
		public IList<TimeSlot> AllDaySlots { get; protected set; }

		// Token: 0x17004F96 RID: 20374
		// (get) Token: 0x06010630 RID: 67120 RVA: 0x003A8B86 File Offset: 0x003A6D86
		// (set) Token: 0x06010631 RID: 67121 RVA: 0x003A8B8E File Offset: 0x003A6D8E
		public TimeSpan WorkDayStartTime { get; set; }

		// Token: 0x17004F97 RID: 20375
		// (get) Token: 0x06010632 RID: 67122 RVA: 0x003A8B97 File Offset: 0x003A6D97
		// (set) Token: 0x06010633 RID: 67123 RVA: 0x003A8B9F File Offset: 0x003A6D9F
		public TimeSpan WorkDayEndTime { get; set; }

		// Token: 0x17004F98 RID: 20376
		// (get) Token: 0x06010634 RID: 67124 RVA: 0x003A8BA8 File Offset: 0x003A6DA8
		// (set) Token: 0x06010635 RID: 67125 RVA: 0x003A8BB0 File Offset: 0x003A6DB0
		protected IWeekTimeSlotFactory SlotFactory { get; set; }

		// Token: 0x06010636 RID: 67126 RVA: 0x003A8BB9 File Offset: 0x003A6DB9
		public Model(IScheduler owner) : this(owner, owner.WeekView.WorkDayStartTimeResolved, owner.WeekView.WorkDayEndTimeResolved)
		{
		}

		// Token: 0x06010637 RID: 67127 RVA: 0x003A8BD8 File Offset: 0x003A6DD8
		public Model(IScheduler owner, TimeSpan workDayStartTime, TimeSpan workDayEndTime) : this(owner, new TimeSlotFactory(), workDayStartTime, workDayEndTime)
		{
		}

		// Token: 0x06010638 RID: 67128 RVA: 0x003A8BE8 File Offset: 0x003A6DE8
		public Model(IScheduler owner, IWeekTimeSlotFactory slotFactory, TimeSpan workDayStartTime, TimeSpan workDayEndTime) : base(owner)
		{
			this.DaySlots = new List<IList<TimeSlot>>(base.NumberOfDays);
			this.AllDaySlots = new List<TimeSlot>();
			this.SlotFactory = slotFactory;
			this.WorkDayStartTime = workDayStartTime;
			this.WorkDayEndTime = workDayEndTime;
			this.AppointmentFilter = new AppointmentFilter();
		}

		// Token: 0x06010639 RID: 67129 RVA: 0x003A8C3C File Offset: 0x003A6E3C
		public override void DataBind(AppointmentCollection appointments)
		{
			this.Appointments.Clear();
			this.AllDaySlots.Clear();
			this.DaySlots.Clear();
			this.Appointments.AddRange(appointments.GetAppointmentsInRange(this.VisibleRangeStart, this.VisibleRangeEnd));
			IList<DayInterval> visibleDays = base.GetVisibleDays();
			foreach (DayInterval item in visibleDays)
			{
				this.CreateAllDaySlots(item.DayStart, visibleDays.IndexOf(item), this.Owner.ShowAllDayRow);
				this.CreateDaySlots(item.DayStart, item.DayEnd, visibleDays.IndexOf(item));
			}
		}

		// Token: 0x0601063A RID: 67130 RVA: 0x003A8CFC File Offset: 0x003A6EFC
		public override ISchedulerTimeSlot GetSlotByIndex(string index)
		{
			string[] array = index.Split(new char[]
			{
				':'
			});
			if (array.Length != 3)
			{
				throw new InvalidOperationException("Invalid slot index: " + index);
			}
			int num;
			bool flag = int.TryParse(array[0], out num);
			int num2;
			flag = (int.TryParse(array[1], out num2) && flag);
			int num3;
			if (!int.TryParse(array[2], out num3) || !flag)
			{
				throw new InvalidOperationException("Cannot parse slot index: " + index);
			}
			if (num > 1 || num < 0)
			{
				throw new IndexOutOfRangeException("Part index expected to be 0 or 1, but was " + num);
			}
			if (num == 0)
			{
				if (num3 >= this.AllDaySlots.Count)
				{
					throw new IndexOutOfRangeException("All day slot index out of range: " + num3);
				}
				return this.AllDaySlots[num3];
			}
			else
			{
				if (num3 >= this.DaySlots.Count)
				{
					throw new IndexOutOfRangeException("Cell index out of range: " + num3);
				}
				if (num2 >= this.DaySlots[num3].Count)
				{
					throw new IndexOutOfRangeException("Row index out of range: " + num2);
				}
				return this.DaySlots[num3][num2];
			}
		}

		// Token: 0x0601063B RID: 67131 RVA: 0x003A8E34 File Offset: 0x003A7034
		public override ISchedulerTimeSlot GetAppointmentSlot(Appointment appointment)
		{
			foreach (IList<TimeSlot> list in this.DaySlots)
			{
				foreach (TimeSlot timeSlot in list)
				{
					if (timeSlot.ContainsAppointment(appointment))
					{
						return timeSlot;
					}
				}
			}
			foreach (TimeSlot timeSlot2 in this.AllDaySlots)
			{
				if (timeSlot2.ContainsAppointment(appointment))
				{
					return timeSlot2;
				}
			}
			return null;
		}

		// Token: 0x0601063C RID: 67132 RVA: 0x003A8F0C File Offset: 0x003A710C
		public override ISchedulerRenderer GetRenderer()
		{
			return new Renderer(new View(this));
		}

		// Token: 0x0601063D RID: 67133 RVA: 0x003A8F1C File Offset: 0x003A711C
		public override IEnumerable<ScriptReference> GetScriptReferences()
		{
			return new ScriptReference[]
			{
				new ScriptReference("Telerik.Web.UI.Scheduler.Views.Week.Model.js", Assembly.GetExecutingAssembly().FullName)
			};
		}

		// Token: 0x0601063E RID: 67134 RVA: 0x003A8F48 File Offset: 0x003A7148
		public TimeSpan GetDurationOfMovedAppointment(Appointment appointment, ISchedulerTimeSlot sourceSlot, ISchedulerTimeSlot targetSlot)
		{
			bool isAllDaySlot = ((TimeSlot)targetSlot).IsAllDaySlot;
			bool isAllDaySlot2 = ((TimeSlot)sourceSlot).IsAllDaySlot;
			TimeSpan result = this.Owner.UtcToDisplay(appointment.End) - this.Owner.UtcToDisplay(appointment.Start);
			if (isAllDaySlot && !isAllDaySlot2)
			{
				result = targetSlot.Duration;
			}
			if (isAllDaySlot2 && !isAllDaySlot)
			{
				result = TimeSpan.FromMinutes((double)(this.Owner.MinutesPerRow * this.Owner.NumberOfHoveredRows));
			}
			return result;
		}

		// Token: 0x0601063F RID: 67135 RVA: 0x003A8FC8 File Offset: 0x003A71C8
		public override void HandleMove(Appointment appointment, ISchedulerTimeSlot sourceSlot, ISchedulerTimeSlot targetSlot, bool editSeries)
		{
			bool isAllDaySlot = ((TimeSlot)targetSlot).IsAllDaySlot;
			bool isAllDaySlot2 = ((TimeSlot)sourceSlot).IsAllDaySlot;
			bool flag = isAllDaySlot ? isAllDaySlot2 : (!isAllDaySlot2);
			TimeSpan durationOfMovedAppointment = this.GetDurationOfMovedAppointment(appointment, sourceSlot, targetSlot);
			DateTime dateTime = targetSlot.Start;
			DateTime end = dateTime.Add(durationOfMovedAppointment);
			if (flag)
			{
				dateTime += appointment.Start - sourceSlot.Start;
				end = this.Owner.DisplayToUtc(this.Owner.UtcToDisplay(dateTime).Add(durationOfMovedAppointment));
			}
			this.Owner.HandleMove(appointment, dateTime, end, editSeries, null);
		}

		// Token: 0x06010640 RID: 67136 RVA: 0x003A9068 File Offset: 0x003A7268
		public override void HandleInsert(ISchedulerTimeSlot targetSlot, ISchedulerTimeSlot lastSlot, Appointment appointmentToInsert)
		{
			appointmentToInsert.Start = targetSlot.Start;
			bool flag = this.AllDaySlots.Contains((TimeSlot)targetSlot);
			if (lastSlot != null)
			{
				appointmentToInsert.End = lastSlot.End;
			}
			else if (flag)
			{
				appointmentToInsert.End = targetSlot.End;
			}
			else
			{
				DateTime date = this.Owner.UtcToDisplay(targetSlot.Start.AddMinutes(targetSlot.Duration.TotalMinutes * (double)this.Owner.NumberOfHoveredRows));
				appointmentToInsert.End = this.Owner.DisplayToUtc(date);
			}
			if (appointmentToInsert.RecurrenceState == RecurrenceState.Master)
			{
				appointmentToInsert.RecurrenceRule = this.CreateDefaultRecurrenceRule(appointmentToInsert);
			}
			this.Owner.HandleInsert(appointmentToInsert);
			this.Owner.ActiveSlotIndex = targetSlot.Index;
		}

		// Token: 0x06010641 RID: 67137 RVA: 0x003A912F File Offset: 0x003A732F
		public override void HandleResize(Appointment appointment, ISchedulerTimeSlot sourceSlot, DateTime appointmentStart, DateTime appointmentEnd, bool editSeries)
		{
			this.Owner.HandleResize(appointment, appointmentStart, appointmentEnd, editSeries);
		}

		// Token: 0x06010642 RID: 67138 RVA: 0x003A9144 File Offset: 0x003A7344
		public override IList<ISchedulerTimeSlot> GetTimeSlots()
		{
			List<ISchedulerTimeSlot> list = new List<ISchedulerTimeSlot>();
			foreach (TimeSlot item in this.AllDaySlots)
			{
				list.Add(item);
			}
			foreach (IList<TimeSlot> list2 in this.DaySlots)
			{
				foreach (TimeSlot item2 in list2)
				{
					list.Add(item2);
				}
			}
			return list;
		}

		// Token: 0x06010643 RID: 67139 RVA: 0x003A9214 File Offset: 0x003A7414
		private void CreateAllDaySlots(DateTime dayStart, int dayIndex, bool populateWithAppointments)
		{
			DateTime dateTime = this.Owner.UtcDayStart(dayStart);
			DateTime dateTime2 = this.Owner.DisplayToUtc(this.Owner.UtcToDisplay(dayStart).Date.AddDays(1.0));
			IList<Appointment> appointmentsList;
			if (populateWithAppointments)
			{
				appointmentsList = this.AppointmentFilter.GetAllDayAppointments(dateTime, dateTime2, this.AllDaySlots, this.Appointments, this.Owner.AppointmentComparer);
			}
			else
			{
				appointmentsList = new List<Appointment>();
			}
			TimeSlot timeSlot = this.SlotFactory.CreateTimeSlot(appointmentsList, this, dateTime, dateTime2);
			this.InitTimeSlot(timeSlot, 0, dayIndex, dateTime, 0, true);
			this.AllDaySlots.Add(timeSlot);
		}

		// Token: 0x06010644 RID: 67140 RVA: 0x003A92BC File Offset: 0x003A74BC
		private void CreateDaySlots(DateTime dayStart, DateTime dayEnd, int dayIndex)
		{
			bool flag = this.Owner.TimeZonesProvider.OperationTimeZone.IsTransitionFrame(dayStart, dayEnd);
			dayStart = this.Owner.UtcToDisplay(dayStart);
			dayEnd = this.Owner.UtcToDisplay(dayEnd);
			IList<TimeSlot> list = new List<TimeSlot>();
			bool isFirstSlot = true;
			DateTime dateTime = dayStart;
			int num = 0;
			while (dateTime < dayEnd)
			{
				DateTime dateTime2 = dateTime.AddMinutes((double)this.Owner.MinutesPerRow);
				DateTime dateTime3 = this.Owner.DisplayToUtc(dateTime);
				DateTime dateTime4 = this.Owner.DisplayToUtc(dateTime2);
				IList<Appointment> dayAppointments = this.GetDayAppointments(dateTime3, dateTime4, isFirstSlot);
				TimeSlot timeSlot = this.SlotFactory.CreateTimeSlot(dayAppointments, this, dateTime3, dateTime4);
				this.InitTimeSlot(timeSlot, num, dayIndex, dateTime3, 1, false);
				list.Add(timeSlot);
				dateTime = dateTime2;
				isFirstSlot = false;
				num++;
			}
			if (flag)
			{
				list = this.AdjustDummyDstTimeSlots(list);
			}
			this.DaySlots.Add(list);
		}

		// Token: 0x06010645 RID: 67141 RVA: 0x003A93A4 File Offset: 0x003A75A4
		private IList<TimeSlot> AdjustDummyDstTimeSlots(IList<TimeSlot> daySlots)
		{
			int count = daySlots.Count;
			IList<TimeSlot> list = new List<TimeSlot>();
			for (int i = 0; i < count; i++)
			{
				TimeSlot timeSlot = daySlots[i];
				for (int j = i + 1; j < count; j++)
				{
					TimeSlot timeSlot2 = daySlots[j];
					if (timeSlot2.Start == timeSlot.Start)
					{
						timeSlot = this.SlotFactory.CreateTimeSlot(new AppointmentCollection(), this, timeSlot2.Start, timeSlot2.End);
						this.InitTimeSlot(timeSlot, timeSlot2.RowIndex, timeSlot2.CellIndex, timeSlot2.Start, 1, false);
						break;
					}
				}
				list.Add(timeSlot);
			}
			return list;
		}

		// Token: 0x06010646 RID: 67142 RVA: 0x003A9450 File Offset: 0x003A7650
		private void InitTimeSlot(TimeSlot slot, int rowIndex, int dayIndex, DateTime timeSlotStart, int partIndex, bool isAllDay)
		{
			slot.PartIndex = partIndex;
			slot.RowIndex = rowIndex;
			slot.CellIndex = dayIndex;
			if (isAllDay)
			{
				slot.IsAllDaySlot = true;
			}
			else
			{
				TimeSpan timeOfDay = this.Owner.UtcToDisplay(timeSlotStart).TimeOfDay;
				slot.IsWorkHour = (timeOfDay >= this.WorkDayStartTime && timeOfDay < this.WorkDayEndTime);
			}
			slot.DayOfWeek = this.Owner.UtcToDisplay(timeSlotStart).DayOfWeek;
		}

		// Token: 0x06010647 RID: 67143 RVA: 0x003A94D4 File Offset: 0x003A76D4
		private IList<Appointment> GetDayAppointments(DateTime timeSlotStart, DateTime timeSlotEnd, bool isFirstSlot)
		{
			AppointmentCollection appointmentCollection = new AppointmentCollection(this.Appointments.GetAppointmentsStartingInRange(timeSlotStart, timeSlotEnd));
			if (isFirstSlot)
			{
				IList<Appointment> appointmentsInRange = this.Appointments.GetAppointmentsInRange(timeSlotStart, timeSlotEnd);
				foreach (Appointment appointment in appointmentsInRange)
				{
					if (appointment.Start < timeSlotStart)
					{
						appointmentCollection.Add(appointment);
					}
				}
			}
			if (this.Owner.ShowAllDayRow)
			{
				DateTime rangeStart = this.Owner.UtcDayStart(timeSlotStart);
				DateTime rangeEnd = this.Owner.DisplayToUtc(this.Owner.UtcToDisplay(timeSlotStart).Date.AddDays(1.0));
				appointmentCollection.Remove(appointmentCollection.GetAppointmentsEnclosingRange(rangeStart, rangeEnd));
			}
			appointmentCollection.Sort(this.Owner.AppointmentComparer);
			return appointmentCollection.ToArray();
		}
	}
}
