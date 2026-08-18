using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Web.UI;
using Telerik.Web.UI.Scheduling;

namespace Telerik.Web.UI.Scheduler.Views.Year
{
	// Token: 0x0200084E RID: 2126
	internal abstract class ModelBase : SchedulerModel
	{
		// Token: 0x170019A1 RID: 6561
		// (get) Token: 0x06004E6A RID: 20074 RVA: 0x000F5CEC File Offset: 0x000F3EEC
		public int NumberOfMonths
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x170019A2 RID: 6562
		// (get) Token: 0x06004E6B RID: 20075 RVA: 0x000F5CF0 File Offset: 0x000F3EF0
		public int WeeksInMonth
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x170019A3 RID: 6563
		// (get) Token: 0x06004E6C RID: 20076 RVA: 0x000F5CF3 File Offset: 0x000F3EF3
		public int WeekLength
		{
			get
			{
				return DateHelper.GetWeekLength(this.SelectedDate, this.Owner.FirstDayOfWeek, this.Owner.LastDayOfWeek);
			}
		}

		// Token: 0x170019A4 RID: 6564
		// (get) Token: 0x06004E6D RID: 20077 RVA: 0x000F5D16 File Offset: 0x000F3F16
		// (set) Token: 0x06004E6E RID: 20078 RVA: 0x000F5D1E File Offset: 0x000F3F1E
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

		// Token: 0x170019A5 RID: 6565
		// (get) Token: 0x06004E6F RID: 20079 RVA: 0x000F5D27 File Offset: 0x000F3F27
		// (set) Token: 0x06004E70 RID: 20080 RVA: 0x000F5D2F File Offset: 0x000F3F2F
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

		// Token: 0x170019A6 RID: 6566
		// (get) Token: 0x06004E71 RID: 20081 RVA: 0x000F5D38 File Offset: 0x000F3F38
		public override DateTime SelectedDate
		{
			get
			{
				return this.Owner.SelectedDate.Date;
			}
		}

		// Token: 0x170019A7 RID: 6567
		// (get) Token: 0x06004E72 RID: 20082 RVA: 0x000F5D58 File Offset: 0x000F3F58
		public override DateTime NextPeriodDate
		{
			get
			{
				return this.SelectedDate.AddYears(1);
			}
		}

		// Token: 0x170019A8 RID: 6568
		// (get) Token: 0x06004E73 RID: 20083 RVA: 0x000F5D74 File Offset: 0x000F3F74
		public override DateTime PreviousPeriodDate
		{
			get
			{
				return this.SelectedDate.AddYears(-1);
			}
		}

		// Token: 0x170019A9 RID: 6569
		// (get) Token: 0x06004E74 RID: 20084 RVA: 0x000F5D90 File Offset: 0x000F3F90
		// (set) Token: 0x06004E75 RID: 20085 RVA: 0x000F5D98 File Offset: 0x000F3F98
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

		// Token: 0x170019AA RID: 6570
		// (get) Token: 0x06004E76 RID: 20086 RVA: 0x000F5DA1 File Offset: 0x000F3FA1
		// (set) Token: 0x06004E77 RID: 20087 RVA: 0x000F5DA9 File Offset: 0x000F3FA9
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

		// Token: 0x170019AB RID: 6571
		// (get) Token: 0x06004E78 RID: 20088 RVA: 0x000F5DB2 File Offset: 0x000F3FB2
		public override bool ReadOnly
		{
			get
			{
				return this.Owner.YearView.ReadOnlyResolved;
			}
		}

		// Token: 0x170019AC RID: 6572
		// (get) Token: 0x06004E79 RID: 20089 RVA: 0x000F5DC4 File Offset: 0x000F3FC4
		public override string CssClass
		{
			get
			{
				return "rsYearView";
			}
		}

		// Token: 0x170019AD RID: 6573
		// (get) Token: 0x06004E7A RID: 20090 RVA: 0x000F5DCB File Offset: 0x000F3FCB
		// (set) Token: 0x06004E7B RID: 20091 RVA: 0x000F5DD3 File Offset: 0x000F3FD3
		public List<IList<TimeSlot>> DaySlots
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

		// Token: 0x06004E7C RID: 20092 RVA: 0x000F5DDC File Offset: 0x000F3FDC
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		protected ModelBase(IScheduler owner)
		{
			this.Owner = owner;
			this.Appointments = new AppointmentCollection();
			this.DaySlots = new List<IList<TimeSlot>>();
			DateTime firstDayOfYear = DateHelper.GetFirstDayOfYear(this.SelectedDate);
			DateTime firstDayOfYear2 = DateHelper.GetFirstDayOfYear(this.SelectedDate.AddYears(1));
			this.VisibleRangeStart = owner.DisplayToUtc(firstDayOfYear);
			this.VisibleRangeEnd = owner.DisplayToUtc(firstDayOfYear2);
		}

		// Token: 0x06004E7D RID: 20093 RVA: 0x000F5E48 File Offset: 0x000F4048
		public override IEnumerable<ScriptReference> GetScriptReferences()
		{
			return new ScriptReference[]
			{
				new ScriptReference("Telerik.Web.UI.Scheduler.Views.Year.Model.js", Assembly.GetExecutingAssembly().FullName)
			};
		}

		// Token: 0x06004E7E RID: 20094 RVA: 0x000F5E74 File Offset: 0x000F4074
		public override void HandleInsert(ISchedulerTimeSlot targetSlot, ISchedulerTimeSlot lastSlot, Appointment appointmentToInsert)
		{
		}

		// Token: 0x06004E7F RID: 20095 RVA: 0x000F5E76 File Offset: 0x000F4076
		public override void HandleMove(Appointment appointment, ISchedulerTimeSlot sourceSlot, ISchedulerTimeSlot targetSlot, bool editSeries)
		{
		}

		// Token: 0x06004E80 RID: 20096 RVA: 0x000F5E78 File Offset: 0x000F4078
		public override void HandleResize(Appointment appointment, ISchedulerTimeSlot sourceSlot, DateTime appointmentStart, DateTime appointmentEnd, bool editSeries)
		{
		}

		// Token: 0x04001382 RID: 4994
		public const int _numberOfMonths = 12;

		// Token: 0x04001383 RID: 4995
		public const int _weeksInMonth = 6;

		// Token: 0x04001384 RID: 4996
		public const int FullWeekLength = 7;

		// Token: 0x04001385 RID: 4997
		private IScheduler _owner;

		// Token: 0x04001386 RID: 4998
		private AppointmentCollection _appointments;

		// Token: 0x04001387 RID: 4999
		private DateTime _visibleRangeStart;

		// Token: 0x04001388 RID: 5000
		private DateTime _visibleRangeEnd;

		// Token: 0x04001389 RID: 5001
		private List<IList<TimeSlot>> _daySlots;
	}
}
