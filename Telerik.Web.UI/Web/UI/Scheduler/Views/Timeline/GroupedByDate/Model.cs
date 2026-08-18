using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using Telerik.Web.UI.Scheduler.Views.Timeline.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views.Timeline.GroupedByDate
{
	// Token: 0x02001A89 RID: 6793
	internal class Model : Model
	{
		// Token: 0x06010730 RID: 67376 RVA: 0x003AD0C0 File Offset: 0x003AB2C0
		public Model(IScheduler owner, string groupingResourceName) : base(owner, groupingResourceName)
		{
		}

		// Token: 0x06010731 RID: 67377 RVA: 0x003AD0CC File Offset: 0x003AB2CC
		public override IEnumerable<ScriptReference> GetScriptReferences()
		{
			List<ScriptReference> list = new List<ScriptReference>();
			list.AddRange(base.GetScriptReferences());
			list.Add(new ScriptReference("Telerik.Web.UI.Scheduler.Views.Timeline.GroupedByDate.Model.js", Assembly.GetExecutingAssembly().FullName));
			return list;
		}

		// Token: 0x06010732 RID: 67378 RVA: 0x003AD108 File Offset: 0x003AB308
		public override ISchedulerRenderer GetRenderer()
		{
			View view;
			if (this.Owner.TimelineView.GroupingDirectionResolved == GroupingDirection.Vertical)
			{
				view = new VerticalView(this);
			}
			else
			{
				view = new HorizontalView(this);
			}
			return new Renderer(view);
		}

		// Token: 0x06010733 RID: 67379 RVA: 0x003AD140 File Offset: 0x003AB340
		public int GetMaximumRowCount()
		{
			int num = 0;
			foreach (Model model in base.TimelineModels)
			{
				List<ISchedulerTimeSlot> list = new List<ISchedulerTimeSlot>(model.IntervalSlots.Count);
				foreach (TimeSlot item in model.IntervalSlots)
				{
					list.Add(item);
				}
				TimelineLayout timelineLayout = new TimelineLayout(list, false);
				num = Math.Max(num, timelineLayout.ActualRowCount);
			}
			return num;
		}

		// Token: 0x06010734 RID: 67380 RVA: 0x003AD1F8 File Offset: 0x003AB3F8
		public override void HandleResize(Appointment appointment, ISchedulerTimeSlot sourceSlot, DateTime appointmentStart, DateTime appointmentEnd, bool editSeries)
		{
			throw new NotSupportedException("Appointment resize is not supported in Timeline view grouped by date");
		}

		// Token: 0x06010735 RID: 67381 RVA: 0x003AD204 File Offset: 0x003AB404
		protected override Model CreateModel(ITimelineTimeSlotFactory slotFactory)
		{
			Model model = base.CreateModel(slotFactory);
			model.AppointmentFilter = new AppointmentFilter();
			return model;
		}
	}
}
