using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduler.Views.Month.GroupedByDate
{
	// Token: 0x02001A55 RID: 6741
	internal class Renderer : RendererBase
	{
		// Token: 0x17004F62 RID: 20322
		// (get) Token: 0x0601059E RID: 66974 RVA: 0x003A6ABC File Offset: 0x003A4CBC
		public new Model Model
		{
			get
			{
				return this.View.Model as Model;
			}
		}

		// Token: 0x0601059F RID: 66975 RVA: 0x003A6ACE File Offset: 0x003A4CCE
		public Renderer(ISchedulerView view) : base(view, view.Model as ModelBase)
		{
		}

		// Token: 0x17004F63 RID: 20323
		// (get) Token: 0x060105A0 RID: 66976 RVA: 0x003A6AE2 File Offset: 0x003A4CE2
		protected override string ContentPanelCssClass
		{
			get
			{
				return "rs" + this.Owner.MonthView.GroupingDirectionResolved;
			}
		}

		// Token: 0x060105A1 RID: 66977 RVA: 0x003A6B04 File Offset: 0x003A4D04
		public override Control GetInnerContent()
		{
			Control control = new Control();
			SchedulerTopTable schedulerTopTable = SchedulerRenderer.CreateTopTable(control, this.Model.CssClass);
			if (this.Owner.MonthView.ShowResourceHeadersResolved || this.Owner.MonthView.ShowDateHeadersResolved)
			{
				base.AddHorizontalHeaders(schedulerTopTable);
			}
			if (this.Owner.MonthView.GroupingDirectionResolved == GroupingDirection.Horizontal)
			{
				schedulerTopTable.ShowRowHeaders = false;
				this.CreateContent(schedulerTopTable.ContentScrollArea);
			}
			else
			{
				ContentTable contentTable = this.CreateInnerContentTable(schedulerTopTable.ContentScrollArea);
				base.AddVerticalHeaders(schedulerTopTable, this.Model.WeekLength * contentTable.GetMaxRowHeight(0));
			}
			this.SetScrollAreaOverflow(schedulerTopTable);
			return control.Controls[0];
		}

		// Token: 0x060105A2 RID: 66978 RVA: 0x003A6BB6 File Offset: 0x003A4DB6
		public override Control GetContent()
		{
			if (this.Owner.UsingWebServiceBinding)
			{
				throw new InvalidOperationException("Date grouped MonthView is not supported when using Web Service binding");
			}
			return base.GetContent();
		}

		// Token: 0x060105A3 RID: 66979 RVA: 0x003A6BD8 File Offset: 0x003A4DD8
		protected override void AddContentCells(Control row)
		{
			TableCell tableCell = new TableCell();
			tableCell.VerticalAlign = VerticalAlign.Top;
			tableCell.Width = Unit.Percentage(100.0);
			tableCell.CssClass = "rsContentContainerCell";
			row.Controls.Add(tableCell);
			this.CreateInnerContentTable(tableCell);
		}

		// Token: 0x060105A4 RID: 66980 RVA: 0x003A6C28 File Offset: 0x003A4E28
		protected override void PopulateInnerContentTable(ContentTable contentTable, ModelBase model)
		{
			for (int i = 0; i < this.Model.NumberOfWeeks; i++)
			{
				TableRow tableRow = new TableRow();
				tableRow.CssClass = "rsRow";
				contentTable.Rows.Add(tableRow);
				for (int j = 0; j < this.Model.WeekLength; j++)
				{
					bool renderDate = true;
					foreach (Model contentModel in this.Model.MonthModels)
					{
						this.AddCell(i, j, tableRow, contentModel, renderDate);
						renderDate = false;
					}
				}
				contentTable.SyncCellHeight(i);
			}
			if (!base.AdaptiveRowHeight)
			{
				contentTable.SetMinimumCellHeight(this.Owner.MonthView.MinimumRowHeight);
				contentTable.SyncCellHeight();
			}
		}

		// Token: 0x060105A5 RID: 66981 RVA: 0x003A6D04 File Offset: 0x003A4F04
		protected override void CreateColumnHeader(Control container)
		{
			SchedulerColumnHeaderPanel child = new SchedulerColumnHeaderPanel(this.Owner, this.View, this.Owner.MonthView.GroupingDirectionResolved, "rs" + this.Owner.MonthView.GroupingDirectionResolved);
			container.Controls.Add(child);
		}

		// Token: 0x060105A6 RID: 66982 RVA: 0x003A6D60 File Offset: 0x003A4F60
		protected ContentTable CreateInnerContentTable(Control container)
		{
			ContentTable contentTable = new ContentTable();
			container.Controls.Add(contentTable);
			this.PopulateInnerContentTable(contentTable, this.Model);
			this.SetContentTableWidth(contentTable);
			return contentTable;
		}

		// Token: 0x060105A7 RID: 66983 RVA: 0x003A6D94 File Offset: 0x003A4F94
		protected void AddCell(int weekIndex, int dayIndex, TableRow tableRow, Model contentModel, bool renderDate)
		{
			TimeSlot timeSlot = contentModel.DaySlots[weekIndex * contentModel.WeekLength + dayIndex];
			TableCell tableCell = new TableCell();
			tableRow.Cells.Add(tableCell);
			base.SetCellCssClass(dayIndex, timeSlot, tableCell);
			int zIndex = (this.Model.NumberOfWeeks - weekIndex) * (this.Model.WeekLength - dayIndex);
			List<AppointmentControl> list = new List<AppointmentControl>();
			foreach (Appointment appointment in timeSlot.Appointments)
			{
				MonthViewAppointmentControl item = new MonthViewAppointmentControl(appointment, timeSlot, true, this.Model.WeekLength);
				list.Add(item);
			}
			base.AddCellContent(tableCell, timeSlot, renderDate, zIndex, list);
		}
	}
}
