using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduler.Views.Week
{
	// Token: 0x02001A73 RID: 6771
	internal class Renderer : RendererBase
	{
		// Token: 0x17004FB1 RID: 20401
		// (get) Token: 0x0601068D RID: 67213 RVA: 0x003AAAFE File Offset: 0x003A8CFE
		public new Model Model
		{
			get
			{
				return this.View.Model as Model;
			}
		}

		// Token: 0x17004FB2 RID: 20402
		// (get) Token: 0x0601068E RID: 67214 RVA: 0x003AAB10 File Offset: 0x003A8D10
		public new View View
		{
			get
			{
				return base.View as View;
			}
		}

		// Token: 0x0601068F RID: 67215 RVA: 0x003AAB1D File Offset: 0x003A8D1D
		public Renderer(ISchedulerView view) : base(view.Model as ModelBase, view)
		{
		}

		// Token: 0x17004FB3 RID: 20403
		// (get) Token: 0x06010690 RID: 67216 RVA: 0x003AAB31 File Offset: 0x003A8D31
		protected override int MaxColumnWidth
		{
			get
			{
				if (this.Owner.ReadOnly || !this.View.EffectiveViewSettings.ShowInsertArea)
				{
					return 100;
				}
				return 90;
			}
		}

		// Token: 0x06010691 RID: 67217 RVA: 0x003AAB58 File Offset: 0x003A8D58
		public override Control GetInnerContent()
		{
			Control control = new Control();
			SchedulerTopTable schedulerTopTable = SchedulerRenderer.CreateTopTable(control, this.Model.CssClass);
			if (this.Owner.WeekView.ShowDateHeadersResolved)
			{
				base.AddColumnHeaders(schedulerTopTable);
			}
			base.AddHoursColumn(schedulerTopTable);
			base.AddAllDayRowContent(schedulerTopTable);
			this.CreateInnerContentTable(schedulerTopTable.ContentScrollArea, this.Model.DaySlots);
			this.SetScrollAreaOverflow(schedulerTopTable);
			this.SetTopTableStyles(schedulerTopTable);
			return control.Controls[0];
		}

		// Token: 0x06010692 RID: 67218 RVA: 0x003AABD8 File Offset: 0x003A8DD8
		protected override SchedulerHeader CreateSchedulerHeader(ViewHeader header)
		{
			SchedulerHeader schedulerHeader = new SchedulerHeader(header.Text, header.SubHeadersVisible, HtmlTextWriterTag.A)
			{
				CssClass = "rsDateHeader"
			};
			schedulerHeader.Attributes["href"] = "#" + header.Date.ToString("yyyy-MM-dd");
			return schedulerHeader;
		}

		// Token: 0x06010693 RID: 67219 RVA: 0x003AAC34 File Offset: 0x003A8E34
		protected override void CreateAllDayContent(WebControl allDayContentWrapper)
		{
			SchedulerAllDayTable schedulerAllDayTable = new SchedulerAllDayTable(this.Owner);
			schedulerAllDayTable.ShowInsertArea = this.View.EffectiveViewSettings.ShowAllDayInsertArea;
			allDayContentWrapper.Controls.Add(schedulerAllDayTable);
			List<ISchedulerTimeSlot> list = new List<ISchedulerTimeSlot>(this.Model.AllDaySlots.Count);
			foreach (TimeSlot item in this.Model.AllDaySlots)
			{
				list.Add(item);
			}
			AllDayLayout allDayLayout = new AllDayLayout(list);
			schedulerAllDayTable.AddRow(list, allDayLayout.AppointmentControls);
			this.SetContentTableWidth(schedulerAllDayTable);
		}
	}
}
