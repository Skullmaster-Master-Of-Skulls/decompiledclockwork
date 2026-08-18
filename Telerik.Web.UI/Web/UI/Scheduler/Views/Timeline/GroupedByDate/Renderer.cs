using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Scheduler.Views.Timeline.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views.Timeline.GroupedByDate
{
	// Token: 0x02001A8C RID: 6796
	internal class Renderer : Renderer
	{
		// Token: 0x06010750 RID: 67408 RVA: 0x003ADAB6 File Offset: 0x003ABCB6
		public Renderer(View view) : base(view)
		{
		}

		// Token: 0x17004FEE RID: 20462
		// (get) Token: 0x06010751 RID: 67409 RVA: 0x003ADABF File Offset: 0x003ABCBF
		public new Model Model
		{
			get
			{
				return this.View.Model as Model;
			}
		}

		// Token: 0x06010752 RID: 67410 RVA: 0x003ADAD1 File Offset: 0x003ABCD1
		public override Control GetContent()
		{
			if (this.Owner.UsingWebServiceBinding)
			{
				throw new InvalidOperationException("Date grouped TimelineView is not supported when using Web Service binding");
			}
			return base.GetContent();
		}

		// Token: 0x06010753 RID: 67411 RVA: 0x003ADAF4 File Offset: 0x003ABCF4
		protected override void CreateVerticalContent(SchedulerTopTable topTable)
		{
			IList<IList<TimeSlot>> list = new List<IList<TimeSlot>>();
			for (int i = 0; i < this.Owner.TimelineView.NumberOfSlots; i++)
			{
				List<TimeSlot> list2 = new List<TimeSlot>();
				foreach (Model model in this.Model.TimelineModels)
				{
					list2.Add(model.IntervalSlots[i]);
				}
				list.Add(list2);
			}
			Table table = base.CreateInnerContentTable(topTable.ContentScrollArea, list);
			string value = base.GetDefaultContentTableWidth();
			if (this.Owner.UseHorizontalScrolling)
			{
				double value2 = this.Owner.ColumnWidth.Value * (double)this.Model.TimelineModels.Count;
				value = SchedulerUnit.GetValue(value2, this.Owner.ColumnWidth.Type);
			}
			table.Style[HtmlTextWriterStyle.Width] = value;
		}

		// Token: 0x06010754 RID: 67412 RVA: 0x003ADC00 File Offset: 0x003ABE00
		protected override void CreateHorizontalContent(Control container)
		{
			string value = base.GetDefaultContentTableWidth();
			SchedulerAllDayTable schedulerAllDayTable = this.CreateTimelineTable();
			container.Controls.Add(schedulerAllDayTable);
			List<ISchedulerTimeSlot> list = new List<ISchedulerTimeSlot>();
			Dictionary<string, List<AppointmentControl>> dictionary = new Dictionary<string, List<AppointmentControl>>();
			if (this.Owner.UseHorizontalScrolling)
			{
				int num = this.Model.TimelineModels.Count * this.Owner.TimelineView.NumberOfSlots;
				double value2 = this.Owner.ColumnWidth.Value * (double)num;
				value = SchedulerUnit.GetValue(value2, this.Owner.ColumnWidth.Type);
			}
			for (int i = 0; i < this.Owner.TimelineView.NumberOfSlots; i++)
			{
				foreach (Model model in this.Model.TimelineModels)
				{
					TimeSlot timeSlot = model.IntervalSlots[i];
					list.Add(timeSlot);
					AllDayLayout allDayLayout = this.CreateLayout(new List<ISchedulerTimeSlot>
					{
						timeSlot
					});
					dictionary.Add(timeSlot.Index, allDayLayout.AppointmentControls[timeSlot.Index]);
				}
			}
			schedulerAllDayTable.AddRow(list, dictionary);
			schedulerAllDayTable.AddPadding(this.Model.MaximumRowCount);
			schedulerAllDayTable.Style[HtmlTextWriterStyle.Width] = value;
		}

		// Token: 0x06010755 RID: 67413 RVA: 0x003ADD7C File Offset: 0x003ABF7C
		protected override AllDayLayout CreateLayout(List<ISchedulerTimeSlot> timeSlots)
		{
			TimelineLayout timelineLayout = new TimelineLayout(timeSlots);
			if (this.Owner.TimelineView.SortingMode == AppointmentSortingMode.Global)
			{
				timelineLayout.AppointmentComparer = this.Owner.AppointmentComparer;
			}
			return timelineLayout;
		}

		// Token: 0x06010756 RID: 67414 RVA: 0x003ADDB4 File Offset: 0x003ABFB4
		protected override SchedulerAllDayTable CreateTimelineTable()
		{
			SchedulerAllDayTable schedulerAllDayTable = new SchedulerAllDayTable(this.Owner);
			schedulerAllDayTable.Style["table-layout"] = "fixed";
			schedulerAllDayTable.ShowInsertArea = this.Owner.TimelineView.ShowInsertArea;
			return schedulerAllDayTable;
		}
	}
}
