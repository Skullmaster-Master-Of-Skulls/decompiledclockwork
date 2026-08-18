using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Web.UI;

namespace Telerik.Web.UI.Scheduler.Views.Agenda
{
	// Token: 0x0200082E RID: 2094
	internal abstract class ModelBase : SchedulerModel
	{
		// Token: 0x06004D8D RID: 19853 RVA: 0x000F3494 File Offset: 0x000F1694
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		protected ModelBase(IScheduler owner)
		{
			this.Owner = owner;
			this.Appointments = new AppointmentCollection();
			this.NumberOfDays = this.Owner.AgendaView.NumberOfDays;
			this.VisibleRangeStart = this.Owner.DisplayToUtc(this.SelectedDate);
			this.VisibleRangeEnd = this.Owner.DisplayToUtc(this.SelectedDate.AddDays((double)this.NumberOfDays));
		}

		// Token: 0x1700195A RID: 6490
		// (get) Token: 0x06004D8E RID: 19854 RVA: 0x000F350C File Offset: 0x000F170C
		// (set) Token: 0x06004D8F RID: 19855 RVA: 0x000F3514 File Offset: 0x000F1714
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

		// Token: 0x1700195B RID: 6491
		// (get) Token: 0x06004D90 RID: 19856 RVA: 0x000F351D File Offset: 0x000F171D
		// (set) Token: 0x06004D91 RID: 19857 RVA: 0x000F3525 File Offset: 0x000F1725
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

		// Token: 0x1700195C RID: 6492
		// (get) Token: 0x06004D92 RID: 19858 RVA: 0x000F3530 File Offset: 0x000F1730
		public override DateTime SelectedDate
		{
			get
			{
				return this.Owner.SelectedDate.Date;
			}
		}

		// Token: 0x1700195D RID: 6493
		// (get) Token: 0x06004D93 RID: 19859 RVA: 0x000F3550 File Offset: 0x000F1750
		public override DateTime NextPeriodDate
		{
			get
			{
				return this.SelectedDate.AddDays((double)this.NumberOfDays);
			}
		}

		// Token: 0x1700195E RID: 6494
		// (get) Token: 0x06004D94 RID: 19860 RVA: 0x000F3574 File Offset: 0x000F1774
		public override DateTime PreviousPeriodDate
		{
			get
			{
				return this.SelectedDate.AddDays((double)(-(double)this.NumberOfDays));
			}
		}

		// Token: 0x1700195F RID: 6495
		// (get) Token: 0x06004D95 RID: 19861 RVA: 0x000F3597 File Offset: 0x000F1797
		// (set) Token: 0x06004D96 RID: 19862 RVA: 0x000F359F File Offset: 0x000F179F
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

		// Token: 0x17001960 RID: 6496
		// (get) Token: 0x06004D97 RID: 19863 RVA: 0x000F35A8 File Offset: 0x000F17A8
		// (set) Token: 0x06004D98 RID: 19864 RVA: 0x000F35B0 File Offset: 0x000F17B0
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

		// Token: 0x17001961 RID: 6497
		// (get) Token: 0x06004D99 RID: 19865 RVA: 0x000F35B9 File Offset: 0x000F17B9
		// (set) Token: 0x06004D9A RID: 19866 RVA: 0x000F35C1 File Offset: 0x000F17C1
		public int NumberOfDays
		{
			get
			{
				return this._numberOfDays;
			}
			set
			{
				this._numberOfDays = value;
			}
		}

		// Token: 0x17001962 RID: 6498
		// (get) Token: 0x06004D9B RID: 19867 RVA: 0x000F35CA File Offset: 0x000F17CA
		public override bool ReadOnly
		{
			get
			{
				return this.Owner.AgendaView.ReadOnlyResolved;
			}
		}

		// Token: 0x17001963 RID: 6499
		// (get) Token: 0x06004D9C RID: 19868 RVA: 0x000F35DC File Offset: 0x000F17DC
		public override string CssClass
		{
			get
			{
				return "rsAgendaView";
			}
		}

		// Token: 0x06004D9D RID: 19869 RVA: 0x000F35E4 File Offset: 0x000F17E4
		public override IEnumerable<ScriptReference> GetScriptReferences()
		{
			return new ScriptReference[]
			{
				new ScriptReference("Telerik.Web.UI.Scheduler.Views.Agenda.Model.js", Assembly.GetExecutingAssembly().FullName)
			};
		}

		// Token: 0x06004D9E RID: 19870 RVA: 0x000F3610 File Offset: 0x000F1810
		public override void HandleResize(Appointment appointment, ISchedulerTimeSlot sourceSlot, DateTime appointmentStart, DateTime appointmentEnd, bool editSeries)
		{
		}

		// Token: 0x06004D9F RID: 19871 RVA: 0x000F3612 File Offset: 0x000F1812
		public override void HandleMove(Appointment appointment, ISchedulerTimeSlot sourceSlot, ISchedulerTimeSlot targetSlot, bool editSeries)
		{
		}

		// Token: 0x06004DA0 RID: 19872 RVA: 0x000F3614 File Offset: 0x000F1814
		public override void HandleInsert(ISchedulerTimeSlot targetSlot, ISchedulerTimeSlot lastSlot, Appointment appointmentToInsert)
		{
		}

		// Token: 0x0400135F RID: 4959
		private IScheduler _owner;

		// Token: 0x04001360 RID: 4960
		private AppointmentCollection _appointments;

		// Token: 0x04001361 RID: 4961
		private DateTime _visibleRangeStart;

		// Token: 0x04001362 RID: 4962
		private DateTime _visibleRangeEnd;

		// Token: 0x04001363 RID: 4963
		private int _numberOfDays;
	}
}
