using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduler.Views.Timeline.GroupedByResource
{
	// Token: 0x02001A8B RID: 6795
	internal class Renderer : RendererBase
	{
		// Token: 0x17004FEC RID: 20460
		// (get) Token: 0x06010747 RID: 67399 RVA: 0x003AD579 File Offset: 0x003AB779
		public new Model Model
		{
			get
			{
				return this.View.Model as Model;
			}
		}

		// Token: 0x06010748 RID: 67400 RVA: 0x003AD58B File Offset: 0x003AB78B
		public Renderer(View view) : base(view, view.Model as ModelBase)
		{
		}

		// Token: 0x17004FED RID: 20461
		// (get) Token: 0x06010749 RID: 67401 RVA: 0x003AD59F File Offset: 0x003AB79F
		protected override string ContentPanelCssClass
		{
			get
			{
				return "rs" + this.Owner.TimelineView.GroupingDirectionResolved;
			}
		}

		// Token: 0x0601074A RID: 67402 RVA: 0x003AD5C0 File Offset: 0x003AB7C0
		public override Control GetInnerContent()
		{
			Control control = new Control();
			SchedulerTopTable schedulerTopTable = SchedulerRenderer.CreateTopTable(control, this.Model.CssClass);
			schedulerTopTable.ShowRowHeaders = false;
			if (this.Owner.TimelineView.ShowResourceHeadersResolved || this.Owner.TimelineView.ShowDateHeadersResolved)
			{
				base.AddHorizontalHeaders(schedulerTopTable);
			}
			if (this.Owner.TimelineView.GroupingDirectionResolved == GroupingDirection.Horizontal)
			{
				this.CreateHorizontalContent(schedulerTopTable.ContentScrollArea);
			}
			else
			{
				this.CreateVerticalContent(schedulerTopTable);
				this.AddVerticalHeaders(schedulerTopTable);
			}
			this.SetScrollAreaOverflow(schedulerTopTable);
			this.SetTopTableStyles(schedulerTopTable);
			return control.Controls[0];
		}

		// Token: 0x0601074B RID: 67403 RVA: 0x003AD660 File Offset: 0x003AB860
		protected virtual void CreateVerticalContent(SchedulerTopTable topTable)
		{
			IList<IList<TimeSlot>> list = new List<IList<TimeSlot>>();
			foreach (Model model in this.Model.TimelineModels)
			{
				list.Add(model.IntervalSlots);
			}
			Table table = base.CreateInnerContentTable(topTable.ContentScrollArea, list);
			string value = this.GetDefaultContentTableWidth();
			if (this.Owner.UseHorizontalScrolling)
			{
				double value2 = this.Owner.ColumnWidth.Value * (double)this.Owner.TimelineView.NumberOfSlots;
				value = SchedulerUnit.GetValue(value2, this.Owner.ColumnWidth.Type);
			}
			table.Style[HtmlTextWriterStyle.Width] = value;
		}

		// Token: 0x0601074C RID: 67404 RVA: 0x003AD738 File Offset: 0x003AB938
		protected virtual void AddVerticalHeaders(SchedulerTopTable topTable)
		{
			topTable.ShowRowHeaders = this.Owner.TimelineView.ShowResourceHeadersResolved;
			if (!topTable.ShowRowHeaders)
			{
				return;
			}
			foreach (ViewHeader viewHeader in this.View.RowHeaders)
			{
				SchedulerHeader schedulerHeader = this.CreateSchedulerHeader(viewHeader);
				schedulerHeader.CssClass = viewHeader.ClassName;
				topTable.VerticalHeaderPanel.AddHeader(schedulerHeader);
				WebControl webControl = (WebControl)schedulerHeader.Parent.Parent;
				webControl.Style[HtmlTextWriterStyle.Height] = SchedulerUnit.GetValue(this.Owner.RowHeight.Value * (double)viewHeader.SubHeaders.Count, this.Owner.RowHeight.Type);
			}
		}

		// Token: 0x0601074D RID: 67405 RVA: 0x003AD820 File Offset: 0x003ABA20
		protected virtual void CreateHorizontalContent(Control container)
		{
			string value = this.GetDefaultContentTableWidth();
			if (this.Owner.UseHorizontalScrolling)
			{
				int num = this.Model.TimelineModels.Count * this.Owner.TimelineView.NumberOfSlots;
				double value2 = this.Owner.ColumnWidth.Value * (double)num;
				value = SchedulerUnit.GetValue(value2, this.Owner.ColumnWidth.Type);
			}
			List<ISchedulerTimeSlot> list = new List<ISchedulerTimeSlot>();
			Dictionary<string, List<AppointmentControl>> dictionary = new Dictionary<string, List<AppointmentControl>>();
			foreach (Model model in this.Model.TimelineModels)
			{
				List<ISchedulerTimeSlot> list2 = new List<ISchedulerTimeSlot>();
				foreach (TimeSlot timeSlot in model.IntervalSlots)
				{
					TimeSlot item = (TimeSlot)timeSlot;
					list2.Add(item);
					list.Add(item);
				}
				TimelineLayout timelineLayout = new TimelineLayout(list2);
				if (this.Owner.TimelineView.SortingMode == AppointmentSortingMode.Global)
				{
					timelineLayout.AppointmentComparer = this.Owner.AppointmentComparer;
				}
				foreach (string key in timelineLayout.AppointmentControls.Keys)
				{
					dictionary.Add(key, timelineLayout.AppointmentControls[key]);
				}
			}
			SchedulerAllDayTable schedulerAllDayTable = base.CreateInnerContentTable(container, list, dictionary);
			schedulerAllDayTable.AddPadding(this.Model.MaximumRowCount);
			schedulerAllDayTable.Style[HtmlTextWriterStyle.Width] = value;
		}

		// Token: 0x0601074E RID: 67406 RVA: 0x003AD9FC File Offset: 0x003ABBFC
		protected string GetDefaultContentTableWidth()
		{
			string result = string.Empty;
			if (HttpContext.Current != null)
			{
				HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
				if (!browser.IsBrowser("IE") || browser.MajorVersion >= 8)
				{
					result = Unit.Percentage(100.0).ToString();
				}
			}
			return result;
		}

		// Token: 0x0601074F RID: 67407 RVA: 0x003ADA5C File Offset: 0x003ABC5C
		internal void CreateColumnHeader(Control container)
		{
			SchedulerColumnHeaderPanel child = new SchedulerColumnHeaderPanel(this.Owner, this.View, this.Owner.TimelineView.GroupingDirectionResolved, "rs" + this.Owner.TimelineView.GroupingDirectionResolved);
			container.Controls.Add(child);
		}
	}
}
