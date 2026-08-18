using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Web.UI;

namespace Telerik.Web.UI.Scheduler.Views.Timeline
{
	// Token: 0x02001A87 RID: 6791
	internal abstract class ModelBase : SchedulerModel
	{
		// Token: 0x17004FD5 RID: 20437
		// (get) Token: 0x0601070A RID: 67338 RVA: 0x003AC9B4 File Offset: 0x003AABB4
		// (set) Token: 0x0601070B RID: 67339 RVA: 0x003AC9BC File Offset: 0x003AABBC
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

		// Token: 0x17004FD6 RID: 20438
		// (get) Token: 0x0601070C RID: 67340 RVA: 0x003AC9C5 File Offset: 0x003AABC5
		public override DateTime SelectedDate
		{
			get
			{
				return this.Owner.SelectedDate;
			}
		}

		// Token: 0x17004FD7 RID: 20439
		// (get) Token: 0x0601070D RID: 67341 RVA: 0x003AC9D4 File Offset: 0x003AABD4
		public override DateTime NextPeriodDate
		{
			get
			{
				return this.SelectedDate.Add(this.Duration);
			}
		}

		// Token: 0x17004FD8 RID: 20440
		// (get) Token: 0x0601070E RID: 67342 RVA: 0x003AC9F8 File Offset: 0x003AABF8
		public override DateTime PreviousPeriodDate
		{
			get
			{
				return this.SelectedDate.Add(-this.Duration);
			}
		}

		// Token: 0x17004FD9 RID: 20441
		// (get) Token: 0x0601070F RID: 67343 RVA: 0x003ACA1E File Offset: 0x003AAC1E
		public override bool EnableExactTimeRendering
		{
			get
			{
				return this.Owner.TimelineView.EnableExactTimeRenderingResolved;
			}
		}

		// Token: 0x17004FDA RID: 20442
		// (get) Token: 0x06010710 RID: 67344 RVA: 0x003ACA30 File Offset: 0x003AAC30
		public override string CssClass
		{
			get
			{
				return "rsTimelineView";
			}
		}

		// Token: 0x17004FDB RID: 20443
		// (get) Token: 0x06010711 RID: 67345 RVA: 0x003ACA37 File Offset: 0x003AAC37
		// (set) Token: 0x06010712 RID: 67346 RVA: 0x003ACA3F File Offset: 0x003AAC3F
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

		// Token: 0x17004FDC RID: 20444
		// (get) Token: 0x06010713 RID: 67347 RVA: 0x003ACA48 File Offset: 0x003AAC48
		// (set) Token: 0x06010714 RID: 67348 RVA: 0x003ACA50 File Offset: 0x003AAC50
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

		// Token: 0x17004FDD RID: 20445
		// (get) Token: 0x06010715 RID: 67349 RVA: 0x003ACA59 File Offset: 0x003AAC59
		// (set) Token: 0x06010716 RID: 67350 RVA: 0x003ACA61 File Offset: 0x003AAC61
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

		// Token: 0x17004FDE RID: 20446
		// (get) Token: 0x06010717 RID: 67351 RVA: 0x003ACA6A File Offset: 0x003AAC6A
		// (set) Token: 0x06010718 RID: 67352 RVA: 0x003ACA72 File Offset: 0x003AAC72
		public IList<TimeSlot> IntervalSlots
		{
			get
			{
				return this._intervalSlots;
			}
			protected set
			{
				this._intervalSlots = value;
			}
		}

		// Token: 0x17004FDF RID: 20447
		// (get) Token: 0x06010719 RID: 67353 RVA: 0x003ACA7B File Offset: 0x003AAC7B
		// (set) Token: 0x0601071A RID: 67354 RVA: 0x003ACA83 File Offset: 0x003AAC83
		protected TimeSpan Duration
		{
			get
			{
				return this._duration;
			}
			set
			{
				this._duration = value;
			}
		}

		// Token: 0x17004FE0 RID: 20448
		// (get) Token: 0x0601071B RID: 67355 RVA: 0x003ACA8C File Offset: 0x003AAC8C
		public override bool ReadOnly
		{
			get
			{
				return this.Owner.TimelineView.ReadOnlyResolved;
			}
		}

		// Token: 0x0601071C RID: 67356 RVA: 0x003ACAA0 File Offset: 0x003AACA0
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		protected ModelBase(IScheduler owner)
		{
			this.Owner = owner;
			this.Appointments = new AppointmentCollection();
			this.IntervalSlots = new List<TimeSlot>(this.Owner.TimelineView.NumberOfSlots);
			long value = this.Owner.TimelineView.SlotDuration.Ticks * (long)this.Owner.TimelineView.NumberOfSlots;
			this.Duration = TimeSpan.FromTicks(value);
			DateTime date = this.SelectedDate.Add(this.Owner.TimelineView.StartTime);
			this.VisibleRangeStart = this.Owner.DisplayToUtc(date);
			this.VisibleRangeEnd = this.Owner.DisplayToUtc(date.Add(this.Duration));
		}

		// Token: 0x0601071D RID: 67357 RVA: 0x003ACB68 File Offset: 0x003AAD68
		public override IEnumerable<ScriptReference> GetScriptReferences()
		{
			return new ScriptReference[]
			{
				new ScriptReference("Telerik.Web.UI.Scheduler.Views.Timeline.Model.js", Assembly.GetExecutingAssembly().FullName)
			};
		}

		// Token: 0x040049B4 RID: 18868
		private IScheduler _owner;

		// Token: 0x040049B5 RID: 18869
		private AppointmentCollection _appointments;

		// Token: 0x040049B6 RID: 18870
		private DateTime _visibleRangeStart;

		// Token: 0x040049B7 RID: 18871
		private DateTime _visibleRangeEnd;

		// Token: 0x040049B8 RID: 18872
		private IList<TimeSlot> _intervalSlots;

		// Token: 0x040049B9 RID: 18873
		private TimeSpan _duration;
	}
}
