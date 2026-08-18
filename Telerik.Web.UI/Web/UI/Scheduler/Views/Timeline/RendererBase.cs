using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

namespace Telerik.Web.UI.Scheduler.Views.Timeline
{
	// Token: 0x02001A8A RID: 6794
	internal abstract class RendererBase : SchedulerRenderer
	{
		// Token: 0x17004FE6 RID: 20454
		// (get) Token: 0x06010736 RID: 67382 RVA: 0x003AD225 File Offset: 0x003AB425
		// (set) Token: 0x06010737 RID: 67383 RVA: 0x003AD22D File Offset: 0x003AB42D
		public ModelBase Model
		{
			get
			{
				return this._model;
			}
			protected set
			{
				this._model = value;
			}
		}

		// Token: 0x17004FE7 RID: 20455
		// (get) Token: 0x06010738 RID: 67384 RVA: 0x003AD236 File Offset: 0x003AB436
		// (set) Token: 0x06010739 RID: 67385 RVA: 0x003AD23E File Offset: 0x003AB43E
		public override ISchedulerView View
		{
			get
			{
				return this._view;
			}
			protected set
			{
				this._view = value;
			}
		}

		// Token: 0x17004FE8 RID: 20456
		// (get) Token: 0x0601073A RID: 67386 RVA: 0x003AD247 File Offset: 0x003AB447
		public override bool ShouldRenderFooter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17004FE9 RID: 20457
		// (get) Token: 0x0601073B RID: 67387 RVA: 0x003AD24A File Offset: 0x003AB44A
		protected override RadScheduler Owner
		{
			get
			{
				return this.Model.Owner as RadScheduler;
			}
		}

		// Token: 0x17004FEA RID: 20458
		// (get) Token: 0x0601073C RID: 67388 RVA: 0x003AD25C File Offset: 0x003AB45C
		// (set) Token: 0x0601073D RID: 67389 RVA: 0x003AD264 File Offset: 0x003AB464
		protected SchedulerContentPanel ContentPanel
		{
			get
			{
				return this._contentPanel;
			}
			set
			{
				this._contentPanel = value;
			}
		}

		// Token: 0x0601073E RID: 67390 RVA: 0x003AD26D File Offset: 0x003AB46D
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RendererBase(View view, ModelBase model)
		{
			this.View = view;
			this.Model = model;
		}

		// Token: 0x0601073F RID: 67391 RVA: 0x003AD284 File Offset: 0x003AB484
		protected override void CreateNavigationPane(Control container)
		{
			DateTime dateTime = this.Owner.UtcToDisplay(this.Model.VisibleRangeStart);
			DateTime dateTime2;
			if (this.Model.VisibleRangeEnd.Date == this.Model.VisibleRangeEnd && this.Owner.TimelineView.SlotDuration.TotalDays.Equals((double)this.Owner.TimelineView.SlotDuration.Days))
			{
				dateTime2 = this.Owner.UtcToDisplay(this.Model.VisibleRangeEnd.AddDays(-1.0));
			}
			else
			{
				dateTime2 = this.Owner.UtcToDisplay(this.Model.VisibleRangeEnd - this.Owner.TimelineView.SlotDuration);
			}
			string dateLabel = string.Format("{0} - {1}", dateTime.ToString(this.Owner.TimelineView.HeaderDateFormat, this.Owner.Culture), dateTime2.ToString(this.Owner.TimelineView.HeaderDateFormat, this.Owner.Culture));
			container.Controls.Add(base.GetHeaderFactory(dateLabel, this.Owner).CreateHeaderControl());
		}

		// Token: 0x17004FEB RID: 20459
		// (get) Token: 0x06010740 RID: 67392 RVA: 0x003AD3CE File Offset: 0x003AB5CE
		protected virtual string ContentPanelCssClass
		{
			get
			{
				return "";
			}
		}

		// Token: 0x06010741 RID: 67393 RVA: 0x003AD3D5 File Offset: 0x003AB5D5
		protected SchedulerAllDayTable CreateInnerContentTable(Control container, ModelBase contentModel)
		{
			return this.CreateInnerContentTable(container, contentModel.IntervalSlots, 0);
		}

		// Token: 0x06010742 RID: 67394 RVA: 0x003AD3E8 File Offset: 0x003AB5E8
		protected SchedulerAllDayTable CreateInnerContentTable(Control container, IList<TimeSlot> intervalSlots, int minimumRowCount)
		{
			SchedulerAllDayTable schedulerAllDayTable = this.CreateInnerContentTable(container, new List<IList<TimeSlot>>
			{
				intervalSlots
			});
			schedulerAllDayTable.AddPadding(minimumRowCount);
			return schedulerAllDayTable;
		}

		// Token: 0x06010743 RID: 67395 RVA: 0x003AD414 File Offset: 0x003AB614
		protected SchedulerAllDayTable CreateInnerContentTable(Control container, IList<IList<TimeSlot>> allTimeLineSlots)
		{
			SchedulerAllDayTable schedulerAllDayTable = this.CreateTimelineTable();
			container.Controls.Add(schedulerAllDayTable);
			foreach (IList<TimeSlot> list in allTimeLineSlots)
			{
				List<ISchedulerTimeSlot> list2 = new List<ISchedulerTimeSlot>(list.Count);
				foreach (TimeSlot item in list)
				{
					list2.Add(item);
				}
				AllDayLayout allDayLayout = this.CreateLayout(list2);
				schedulerAllDayTable.AddRow(list2, allDayLayout.AppointmentControls);
			}
			return schedulerAllDayTable;
		}

		// Token: 0x06010744 RID: 67396 RVA: 0x003AD4D0 File Offset: 0x003AB6D0
		protected SchedulerAllDayTable CreateInnerContentTable(Control container, IList<ISchedulerTimeSlot> slots, Dictionary<string, List<AppointmentControl>> appointmentControls)
		{
			SchedulerAllDayTable schedulerAllDayTable = this.CreateTimelineTable();
			container.Controls.Add(schedulerAllDayTable);
			schedulerAllDayTable.AddRow(slots, appointmentControls);
			return schedulerAllDayTable;
		}

		// Token: 0x06010745 RID: 67397 RVA: 0x003AD4FC File Offset: 0x003AB6FC
		protected virtual AllDayLayout CreateLayout(List<ISchedulerTimeSlot> timeSlots)
		{
			TimelineLayout timelineLayout = new TimelineLayout(timeSlots);
			if (this.Owner.TimelineView.SortingMode == AppointmentSortingMode.Global)
			{
				timelineLayout.AppointmentComparer = this.Owner.AppointmentComparer;
			}
			return timelineLayout;
		}

		// Token: 0x06010746 RID: 67398 RVA: 0x003AD534 File Offset: 0x003AB734
		protected virtual SchedulerAllDayTable CreateTimelineTable()
		{
			SchedulerAllDayTable schedulerAllDayTable = new TimelineAllDayTable(this.Owner);
			schedulerAllDayTable.Style["table-layout"] = "fixed";
			schedulerAllDayTable.ShowInsertArea = this.Owner.TimelineView.ShowInsertArea;
			return schedulerAllDayTable;
		}

		// Token: 0x040049BD RID: 18877
		private ModelBase _model;

		// Token: 0x040049BE RID: 18878
		private ISchedulerView _view;

		// Token: 0x040049BF RID: 18879
		private SchedulerContentPanel _contentPanel;
	}
}
