using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduler.Views.Year
{
	// Token: 0x02000850 RID: 2128
	internal abstract class RendererBase : SchedulerRenderer
	{
		// Token: 0x170019B3 RID: 6579
		// (get) Token: 0x06004E8F RID: 20111 RVA: 0x000F61A0 File Offset: 0x000F43A0
		public override bool ShouldRenderFooter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170019B4 RID: 6580
		// (get) Token: 0x06004E90 RID: 20112 RVA: 0x000F61A3 File Offset: 0x000F43A3
		// (set) Token: 0x06004E91 RID: 20113 RVA: 0x000F61AB File Offset: 0x000F43AB
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

		// Token: 0x170019B5 RID: 6581
		// (get) Token: 0x06004E92 RID: 20114 RVA: 0x000F61B4 File Offset: 0x000F43B4
		protected override RadScheduler Owner
		{
			get
			{
				return this.Model.Owner as RadScheduler;
			}
		}

		// Token: 0x170019B6 RID: 6582
		// (get) Token: 0x06004E93 RID: 20115 RVA: 0x000F61C6 File Offset: 0x000F43C6
		// (set) Token: 0x06004E94 RID: 20116 RVA: 0x000F61CE File Offset: 0x000F43CE
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

		// Token: 0x06004E95 RID: 20117 RVA: 0x000F61D7 File Offset: 0x000F43D7
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		protected RendererBase(ISchedulerView view, ModelBase model)
		{
			this.View = view;
			this.Model = model;
		}

		// Token: 0x06004E96 RID: 20118 RVA: 0x000F61F0 File Offset: 0x000F43F0
		protected override void CreateNavigationPane(Control container)
		{
			string dateLabel = this.Model.SelectedDate.ToString(this.Owner.YearView.HeaderDateFormat, this.Owner.Culture);
			container.Controls.Add(base.GetHeaderFactory(dateLabel, this.Owner).CreateHeaderControl());
		}

		// Token: 0x06004E97 RID: 20119 RVA: 0x000F624C File Offset: 0x000F444C
		protected Table CreateInnerContentTable(Control container, int monthIndex, ModelBase model)
		{
			SchedulerTable schedulerTable = new SchedulerTable();
			container.Controls.Add(schedulerTable);
			if (this.Owner.YearView.ShowMonthHeaders || this.Owner.YearView.ShowDateHeadersResolved)
			{
				this.AddHeaders(schedulerTable, monthIndex);
			}
			this.PopulateInnerContentTable(schedulerTable, model.DaySlots[monthIndex]);
			return schedulerTable;
		}

		// Token: 0x06004E98 RID: 20120 RVA: 0x000F62AC File Offset: 0x000F44AC
		protected void AddHeaders(SchedulerTable contentTable, int monthIndex)
		{
			ViewHeader viewHeader = ((View)this.View).MonthHeaders[monthIndex];
			if (this.Owner.YearView.ShowMonthHeaders)
			{
				TableHeaderRow row = new TableHeaderRow
				{
					TableSection = TableRowSection.TableHeader
				};
				contentTable.Rows.Add(row);
				this.AddHeader(viewHeader, row, true);
			}
			if (viewHeader.SubHeaders.Count > 0)
			{
				TableHeaderRow row2 = new TableHeaderRow
				{
					TableSection = TableRowSection.TableHeader
				};
				contentTable.Rows.Add(row2);
				foreach (ViewHeader viewHeader2 in viewHeader.SubHeaders)
				{
					this.AddHeader(viewHeader2, row2, false);
				}
			}
		}

		// Token: 0x06004E99 RID: 20121 RVA: 0x000F6380 File Offset: 0x000F4580
		protected void AddMonths(Control container, ModelBase model)
		{
			Panel panel = new Panel
			{
				CssClass = "rsYearMonthsWrap"
			};
			container.Controls.Add(panel);
			for (int i = 0; i < model.DaySlots.Count; i++)
			{
				Panel panel2 = new Panel
				{
					CssClass = "rsYearMonthWrap"
				};
				panel.Controls.Add(panel2);
				if (this.Owner.UseHorizontalScrolling)
				{
					this.SetColumnWidth(panel2);
				}
				this.CreateInnerContentTable(panel2, i, model);
			}
		}

		// Token: 0x06004E9A RID: 20122 RVA: 0x000F6404 File Offset: 0x000F4604
		private void PopulateInnerContentTable(SchedulerTable contentTable, IList<TimeSlot> daySlots)
		{
			for (int i = 0; i < this.Model.WeeksInMonth; i++)
			{
				TableRow tableRow = new TableRow();
				contentTable.Rows.Add(tableRow);
				tableRow.Height = this.Owner.RowHeight;
				for (int j = 0; j < this.Model.WeekLength; j++)
				{
					TimeSlot timeSlot = daySlots[i * this.Model.WeekLength + j];
					TableCell tableCell = new TableCell();
					tableRow.Cells.Add(tableCell);
					if (!timeSlot.IsOtherMonth)
					{
						this.AddDateHeader(tableCell, timeSlot.Start);
					}
					this.SetCellCssClass(timeSlot, tableCell);
				}
			}
		}

		// Token: 0x06004E9B RID: 20123 RVA: 0x000F64B1 File Offset: 0x000F46B1
		protected virtual void SetColumnWidth(WebControl container)
		{
			container.Width = this.Owner.ColumnWidth;
		}

		// Token: 0x06004E9C RID: 20124 RVA: 0x000F64C4 File Offset: 0x000F46C4
		private void AddDateHeader(Control container, DateTime slotStart)
		{
			DateTime dateTime = this.Owner.UtcToDisplay(slotStart);
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("a");
			container.Controls.Add(htmlGenericControl);
			htmlGenericControl.Attributes["href"] = "#" + dateTime.ToString("yyyy-MM-dd");
			string dayHeaderDateFormat = this.Owner.YearView.DayHeaderDateFormat;
			if (dayHeaderDateFormat == "dd")
			{
				htmlGenericControl.InnerText = dateTime.Day.ToString();
			}
			else
			{
				htmlGenericControl.InnerText = dateTime.ToString(dayHeaderDateFormat, this.Owner.Culture);
			}
			htmlGenericControl.Attributes["title"] = dateTime.ToString("d", this.Owner.Culture);
			htmlGenericControl.Attributes["class"] = "rsDateHeader";
		}

		// Token: 0x06004E9D RID: 20125 RVA: 0x000F65A8 File Offset: 0x000F47A8
		protected void SetCellCssClass(TimeSlot slot, WebControl cell)
		{
			DateTime start = slot.Start;
			List<string> list = new List<string>();
			if (this.Owner.UtcToDisplay(start).Date == this.Owner.VisualToday.Date && !slot.IsOtherMonth)
			{
				list.Add("rsTodayCell");
			}
			DayOfWeek dayOfWeek = this.Owner.UtcToDisplay(start).DayOfWeek;
			if (dayOfWeek == DayOfWeek.Saturday)
			{
				list.Add("rsSatCol");
			}
			else if (dayOfWeek == DayOfWeek.Sunday)
			{
				list.Add("rsSunCol");
			}
			if (slot.IsOtherMonth)
			{
				list.Add("rsOtherMonth");
			}
			else if (slot.Appointments.Count > 0)
			{
				list.Add("rsOccupiedSlot");
			}
			cell.CssClass = string.Join(" ", list.ToArray());
		}

		// Token: 0x06004E9E RID: 20126 RVA: 0x000F6680 File Offset: 0x000F4880
		private void AddHeader(ViewHeader viewHeader, TableHeaderRow row, bool addLink)
		{
			TableHeaderCell tableHeaderCell = new TableHeaderCell();
			if (viewHeader.ColumnSpan != 1)
			{
				tableHeaderCell.ColumnSpan = viewHeader.ColumnSpan;
			}
			if (addLink)
			{
				HtmlGenericControl htmlGenericControl = new HtmlGenericControl("a");
				tableHeaderCell.Controls.Add(htmlGenericControl);
				htmlGenericControl.InnerText = viewHeader.Text;
				htmlGenericControl.Attributes["href"] = "#" + viewHeader.Date.ToString("yyyy-MM-dd");
				htmlGenericControl.Attributes["title"] = viewHeader.Text;
				htmlGenericControl.Attributes["class"] = "rsMonthHeader";
			}
			else
			{
				tableHeaderCell.Controls.Add(new LiteralControl(viewHeader.Text));
			}
			row.Controls.Add(tableHeaderCell);
		}

		// Token: 0x0400138D RID: 5005
		private ModelBase _model;

		// Token: 0x0400138E RID: 5006
		private ISchedulerView _view;
	}
}
