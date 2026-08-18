using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Web.UI;
using Telerik.Web.UI.Scheduling;

namespace Telerik.Web.UI.Scheduler.Views.Month
{
	// Token: 0x02001A5A RID: 6746
	internal abstract class ModelBase : SchedulerModel
	{
		// Token: 0x17004F69 RID: 20329
		// (get) Token: 0x060105BC RID: 67004 RVA: 0x003A73D0 File Offset: 0x003A55D0
		public override string CssClass
		{
			get
			{
				return "rsMonthView";
			}
		}

		// Token: 0x17004F6A RID: 20330
		// (get) Token: 0x060105BD RID: 67005 RVA: 0x003A73D7 File Offset: 0x003A55D7
		// (set) Token: 0x060105BE RID: 67006 RVA: 0x003A73DF File Offset: 0x003A55DF
		public override IScheduler Owner
		{
			get
			{
				return this._owner;
			}
			protected set
			{
				this._owner = value;
			}
		}

		// Token: 0x17004F6B RID: 20331
		// (get) Token: 0x060105BF RID: 67007 RVA: 0x003A73E8 File Offset: 0x003A55E8
		public override DateTime SelectedDate
		{
			get
			{
				return this.Owner.SelectedDate.Date;
			}
		}

		// Token: 0x17004F6C RID: 20332
		// (get) Token: 0x060105C0 RID: 67008 RVA: 0x003A7408 File Offset: 0x003A5608
		public override DateTime NextPeriodDate
		{
			get
			{
				return this.SelectedDate.AddMonths(1);
			}
		}

		// Token: 0x17004F6D RID: 20333
		// (get) Token: 0x060105C1 RID: 67009 RVA: 0x003A7424 File Offset: 0x003A5624
		public override DateTime PreviousPeriodDate
		{
			get
			{
				return this.SelectedDate.AddMonths(-1);
			}
		}

		// Token: 0x17004F6E RID: 20334
		// (get) Token: 0x060105C2 RID: 67010 RVA: 0x003A7440 File Offset: 0x003A5640
		// (set) Token: 0x060105C3 RID: 67011 RVA: 0x003A7448 File Offset: 0x003A5648
		public override AppointmentCollection Appointments
		{
			get
			{
				return this._appointments;
			}
			protected set
			{
				this._appointments = value;
			}
		}

		// Token: 0x17004F6F RID: 20335
		// (get) Token: 0x060105C4 RID: 67012 RVA: 0x003A7451 File Offset: 0x003A5651
		// (set) Token: 0x060105C5 RID: 67013 RVA: 0x003A7459 File Offset: 0x003A5659
		public override DateTime VisibleRangeStart
		{
			get
			{
				return this._visibleRangeStart;
			}
			protected set
			{
				this._visibleRangeStart = value;
			}
		}

		// Token: 0x17004F70 RID: 20336
		// (get) Token: 0x060105C6 RID: 67014 RVA: 0x003A7462 File Offset: 0x003A5662
		// (set) Token: 0x060105C7 RID: 67015 RVA: 0x003A746A File Offset: 0x003A566A
		public override DateTime VisibleRangeEnd
		{
			get
			{
				return this._visibleRangeEnd;
			}
			protected set
			{
				this._visibleRangeEnd = value;
			}
		}

		// Token: 0x17004F71 RID: 20337
		// (get) Token: 0x060105C8 RID: 67016 RVA: 0x003A7473 File Offset: 0x003A5673
		// (set) Token: 0x060105C9 RID: 67017 RVA: 0x003A747B File Offset: 0x003A567B
		public IList<TimeSlot> DaySlots
		{
			get
			{
				return this._daySlots;
			}
			protected set
			{
				this._daySlots = value;
			}
		}

		// Token: 0x17004F72 RID: 20338
		// (get) Token: 0x060105CA RID: 67018 RVA: 0x003A7484 File Offset: 0x003A5684
		public int WeekLength
		{
			get
			{
				return DateHelper.GetWeekLength(this.SelectedDate, this.Owner.FirstDayOfWeek, this.Owner.LastDayOfWeek);
			}
		}

		// Token: 0x17004F73 RID: 20339
		// (get) Token: 0x060105CB RID: 67019 RVA: 0x003A74A7 File Offset: 0x003A56A7
		// (set) Token: 0x060105CC RID: 67020 RVA: 0x003A74AF File Offset: 0x003A56AF
		public int NumberOfWeeks
		{
			get
			{
				return this._numberOfWeeks;
			}
			protected set
			{
				this._numberOfWeeks = value;
			}
		}

		// Token: 0x17004F74 RID: 20340
		// (get) Token: 0x060105CD RID: 67021 RVA: 0x003A74B8 File Offset: 0x003A56B8
		public override bool ReadOnly
		{
			get
			{
				return this.Owner.MonthView.ReadOnlyResolved;
			}
		}

		// Token: 0x060105CE RID: 67022 RVA: 0x003A74CC File Offset: 0x003A56CC
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		protected ModelBase(IScheduler owner)
		{
			this.Owner = owner;
			this.Appointments = new AppointmentCollection();
			this.DaySlots = new List<TimeSlot>();
			DateTime firstDayOfMonth = DateHelper.GetFirstDayOfMonth(this.SelectedDate);
			DateTime lastDayOfMonth = DateHelper.GetLastDayOfMonth(this.SelectedDate);
			DateTime startOfWeek = DateHelper.GetStartOfWeek(firstDayOfMonth, owner.FirstDayOfWeek);
			if (DateHelper.GetEndOfWeek(startOfWeek, owner.FirstDayOfWeek, this.WeekLength).Month == this.SelectedDate.Month)
			{
				this.VisibleRangeStart = startOfWeek;
			}
			else
			{
				this.VisibleRangeStart = startOfWeek.AddDays(7.0);
			}
			DateTime endOfWeek = DateHelper.GetEndOfWeek(lastDayOfMonth, owner.FirstDayOfWeek, 7);
			this.NumberOfWeeks = (endOfWeek - this.VisibleRangeStart).Days / 7;
			this.VisibleRangeStart = owner.DisplayToUtc(this.VisibleRangeStart);
			this.VisibleRangeEnd = owner.DisplayToUtc(DateHelper.GetEndOfWeek(lastDayOfMonth, owner.FirstDayOfWeek, this.WeekLength));
		}

		// Token: 0x060105CF RID: 67023 RVA: 0x003A75C8 File Offset: 0x003A57C8
		public override IEnumerable<ScriptReference> GetScriptReferences()
		{
			return new ScriptReference[]
			{
				new ScriptReference("Telerik.Web.UI.Scheduler.Views.Month.Model.js", Assembly.GetExecutingAssembly().FullName)
			};
		}

		// Token: 0x060105D0 RID: 67024 RVA: 0x003A75F4 File Offset: 0x003A57F4
		public IList<ISchedulerTimeSlot> GetWeekSlots(int weekIndex)
		{
			List<ISchedulerTimeSlot> list = new List<ISchedulerTimeSlot>(this.WeekLength);
			for (int i = 0; i < this.WeekLength; i++)
			{
				list.Add(this.DaySlots[weekIndex * this.WeekLength + i]);
			}
			return list;
		}

		// Token: 0x060105D1 RID: 67025 RVA: 0x003A763A File Offset: 0x003A583A
		public override void HandleResize(Appointment appointment, ISchedulerTimeSlot sourceSlot, DateTime appointmentStart, DateTime appointmentEnd, bool editSeries)
		{
			this.Owner.HandleResize(appointment, appointmentStart, appointmentEnd, editSeries);
		}

		// Token: 0x17004F75 RID: 20341
		// (get) Token: 0x060105D2 RID: 67026 RVA: 0x003A764D File Offset: 0x003A584D
		protected bool IsGroupedByDate
		{
			get
			{
				return !string.IsNullOrEmpty(this.Owner.GroupBy) && this.Owner.GroupBy.Trim().ToLowerInvariant().StartsWith("date,");
			}
		}

		// Token: 0x0400498B RID: 18827
		public const int FullWeekLength = 7;

		// Token: 0x0400498C RID: 18828
		private AppointmentCollection _appointments;

		// Token: 0x0400498D RID: 18829
		private IScheduler _owner;

		// Token: 0x0400498E RID: 18830
		private DateTime _visibleRangeEnd;

		// Token: 0x0400498F RID: 18831
		private DateTime _visibleRangeStart;

		// Token: 0x04004990 RID: 18832
		private IList<TimeSlot> _daySlots;

		// Token: 0x04004991 RID: 18833
		private int _numberOfWeeks;
	}
}
