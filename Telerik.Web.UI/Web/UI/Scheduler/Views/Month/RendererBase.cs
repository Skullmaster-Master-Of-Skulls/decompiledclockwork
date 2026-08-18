using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduler.Views.Month
{
	// Token: 0x02001A54 RID: 6740
	internal abstract class RendererBase : SchedulerRenderer
	{
		// Token: 0x17004F5B RID: 20315
		// (get) Token: 0x06010587 RID: 66951 RVA: 0x003A61C6 File Offset: 0x003A43C6
		// (set) Token: 0x06010588 RID: 66952 RVA: 0x003A61CE File Offset: 0x003A43CE
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

		// Token: 0x17004F5C RID: 20316
		// (get) Token: 0x06010589 RID: 66953 RVA: 0x003A61D7 File Offset: 0x003A43D7
		// (set) Token: 0x0601058A RID: 66954 RVA: 0x003A61DF File Offset: 0x003A43DF
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

		// Token: 0x17004F5D RID: 20317
		// (get) Token: 0x0601058B RID: 66955 RVA: 0x003A61E8 File Offset: 0x003A43E8
		protected override RadScheduler Owner
		{
			get
			{
				return this.Model.Owner as RadScheduler;
			}
		}

		// Token: 0x17004F5E RID: 20318
		// (get) Token: 0x0601058C RID: 66956 RVA: 0x003A61FA File Offset: 0x003A43FA
		public override bool ShouldRenderFooter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17004F5F RID: 20319
		// (get) Token: 0x0601058D RID: 66957 RVA: 0x003A61FD File Offset: 0x003A43FD
		// (set) Token: 0x0601058E RID: 66958 RVA: 0x003A6205 File Offset: 0x003A4405
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

		// Token: 0x17004F60 RID: 20320
		// (get) Token: 0x0601058F RID: 66959 RVA: 0x003A620E File Offset: 0x003A440E
		protected bool AdaptiveRowHeight
		{
			get
			{
				return (string.IsNullOrEmpty(this.Owner.MonthView.GroupByResolved) || this.Owner.MonthView.GroupingDirectionResolved != GroupingDirection.Vertical) && this.Owner.MonthView.AdaptiveRowHeight;
			}
		}

		// Token: 0x06010590 RID: 66960 RVA: 0x003A624C File Offset: 0x003A444C
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		protected RendererBase(ISchedulerView view, ModelBase model)
		{
			this.View = view;
			this.Model = model;
		}

		// Token: 0x06010591 RID: 66961 RVA: 0x003A6262 File Offset: 0x003A4462
		protected virtual void AddContentCells(Control row)
		{
		}

		// Token: 0x06010592 RID: 66962
		protected abstract void CreateColumnHeader(Control container);

		// Token: 0x06010593 RID: 66963 RVA: 0x003A6264 File Offset: 0x003A4464
		protected virtual void CreateContent(Control container)
		{
			this.ContentPanel = new SchedulerContentPanel(this.Owner, this.ContentPanelCssClass);
			SchedulerContentPanel contentPanel = this.ContentPanel;
			contentPanel.CssClass = contentPanel.CssClass + " " + this.Model.CssClass;
			container.Controls.Add(this.ContentPanel);
			this.SetContentTableWidth(this.ContentPanel.ContentTable);
			this.AddContentCells(this.ContentPanel.ContentRow);
		}

		// Token: 0x17004F61 RID: 20321
		// (get) Token: 0x06010594 RID: 66964 RVA: 0x003A62E1 File Offset: 0x003A44E1
		protected virtual string ContentPanelCssClass
		{
			get
			{
				return "";
			}
		}

		// Token: 0x06010595 RID: 66965 RVA: 0x003A62E8 File Offset: 0x003A44E8
		protected virtual void PopulateInnerContentTable(ContentTable contentTable, ModelBase model)
		{
			for (int i = 0; i < model.NumberOfWeeks; i++)
			{
				TableRow tableRow = new TableRow();
				tableRow.CssClass = "rsRow";
				contentTable.Rows.Add(tableRow);
				IList<ISchedulerTimeSlot> weekSlots = model.GetWeekSlots(i);
				MonthWeekLayout monthWeekLayout = new MonthWeekLayout(weekSlots, true);
				for (int j = 0; j < weekSlots.Count; j++)
				{
					ISchedulerTimeSlot schedulerTimeSlot = weekSlots[j];
					TableCell tableCell = new TableCell();
					tableRow.Cells.Add(tableCell);
					this.SetCellCssClass(j, schedulerTimeSlot, tableCell);
					int zIndex = model.WeekLength - j;
					IList<AppointmentControl> slotControls = monthWeekLayout.AppointmentControls[schedulerTimeSlot.Index];
					this.AddCellContent(tableCell, schedulerTimeSlot, true, zIndex, slotControls);
				}
				contentTable.SyncCellHeight(i);
				for (int k = 0; k < weekSlots.Count; k++)
				{
					ISchedulerTimeSlot schedulerTimeSlot2 = weekSlots[k];
					if (((TimeSlot)schedulerTimeSlot2).HasMoreAppointments)
					{
						int zIndex2 = model.WeekLength - k;
						MonthViewCellWrapper monthViewCellWrapper = new MonthViewCellWrapper(zIndex2);
						tableRow.Cells[k].Controls.Add(monthViewCellWrapper);
						monthViewCellWrapper.CssClass = " rsShowMore";
						monthViewCellWrapper.Controls.Add(new LiteralControl(this.Owner.Localization.ShowMore));
					}
				}
			}
			if (!this.AdaptiveRowHeight)
			{
				contentTable.SetMinimumCellHeight(this.Owner.MonthView.MinimumRowHeight);
				contentTable.SyncCellHeight();
			}
			this.ApplyContentTableCellStyles(contentTable);
		}

		// Token: 0x06010596 RID: 66966 RVA: 0x003A6464 File Offset: 0x003A4664
		protected void ApplyContentTableCellStyles(ContentTable contentTable)
		{
			foreach (TableCell tableCell in contentTable.AllCells)
			{
				WebControl webControl = null;
				foreach (object obj in tableCell.Controls)
				{
					WebControl webControl2 = (WebControl)obj;
					webControl2.Style[HtmlTextWriterStyle.Height] = this.Owner.RowHeight.ToString();
					webControl = webControl2;
				}
				if (webControl != null)
				{
					WebControl webControl3 = webControl;
					webControl3.CssClass += " rsLastWrap";
				}
			}
		}

		// Token: 0x06010597 RID: 66967 RVA: 0x003A653C File Offset: 0x003A473C
		protected void SetCellCssClass(int dayIndex, ISchedulerTimeSlot slot, WebControl cell)
		{
			DateTime start = slot.Start;
			List<string> list = new List<string>();
			if (dayIndex == this.Model.WeekLength - 1)
			{
				list.Add("rsLastCell");
			}
			if (this.Owner.UtcToDisplay(start).Date == this.Owner.VisualToday.Date)
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
			if (this.Owner.UtcToDisplay(start).Month != this.Model.SelectedDate.Month)
			{
				list.Add("rsOtherMonth");
			}
			slot.Control = cell;
			slot.CssClass = string.Join(" ", list.ToArray());
		}

		// Token: 0x06010598 RID: 66968 RVA: 0x003A6634 File Offset: 0x003A4834
		protected override void CreateNavigationPane(Control container)
		{
			string dateLabel = this.Model.SelectedDate.ToString(this.Owner.MonthView.HeaderDateFormat, this.Owner.Culture);
			container.Controls.Add(base.GetHeaderFactory(dateLabel, this.Owner).CreateHeaderControl());
		}

		// Token: 0x06010599 RID: 66969 RVA: 0x003A6690 File Offset: 0x003A4890
		protected void AddCellContent(Control container, ISchedulerTimeSlot daySlot, bool renderDate, int zIndex, IList<AppointmentControl> slotControls)
		{
			if (daySlot.FormContainer != null)
			{
				MonthViewCellWrapper monthViewCellWrapper = new MonthViewCellWrapper(2000);
				container.Controls.Add(monthViewCellWrapper);
				monthViewCellWrapper.Controls.Add(daySlot.FormContainer);
			}
			this.AddCellAppointments(container, daySlot, zIndex, slotControls);
			MonthViewCellWrapper monthViewCellWrapper2 = new MonthViewCellWrapper(zIndex);
			MonthViewCellWrapper monthViewCellWrapper3 = monthViewCellWrapper2;
			monthViewCellWrapper3.CssClass += " rsDateWrap";
			container.Controls.AddAt(0, monthViewCellWrapper2);
			if (renderDate)
			{
				this.AddDateHeader(monthViewCellWrapper2, daySlot.Start);
			}
		}

		// Token: 0x0601059A RID: 66970 RVA: 0x003A6714 File Offset: 0x003A4914
		private void AddDateHeader(Control container, DateTime slotStart)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			htmlGenericControl.Attributes["class"] = "rsDateBox";
			container.Controls.Add(htmlGenericControl);
			DateTime dateTime = this.Owner.UtcToDisplay(slotStart);
			HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("a");
			htmlGenericControl.Controls.Add(htmlGenericControl2);
			htmlGenericControl2.Attributes["href"] = "#" + dateTime.ToString("yyyy-MM-dd");
			bool flag = dateTime.Date == this.Model.VisibleRangeStart.Date && dateTime.Month == this.Owner.SelectedDate.Month;
			string text = (dateTime.Day == 1 || flag) ? this.Owner.MonthView.FirstDayHeaderDateFormat : this.Owner.MonthView.DayHeaderDateFormat;
			if (text == "dd" || this.Owner.ResolvedRenderMode == RenderMode.Mobile)
			{
				htmlGenericControl2.InnerText = dateTime.Day.ToString();
			}
			else
			{
				htmlGenericControl2.InnerText = dateTime.ToString(text, this.Owner.Culture);
			}
			htmlGenericControl2.Attributes["title"] = dateTime.ToString("d", this.Owner.Culture);
			htmlGenericControl2.Attributes["class"] = "rsDateHeader";
		}

		// Token: 0x0601059B RID: 66971 RVA: 0x003A6898 File Offset: 0x003A4A98
		protected void AddCellAppointments(Control cell, ISchedulerTimeSlot daySlot, int zIndex, IList<AppointmentControl> slotControls)
		{
			foreach (AppointmentControl appointmentControl in slotControls)
			{
				MonthViewAppointmentControl monthViewAppointmentControl = (MonthViewAppointmentControl)appointmentControl;
				int num = slotControls.IndexOf(monthViewAppointmentControl);
				if (monthViewAppointmentControl.Row != null)
				{
					num = monthViewAppointmentControl.Row.RowIndex;
				}
				int count = cell.Controls.Count;
				for (int i = count; i < num; i++)
				{
					MonthViewCellWrapper monthViewCellWrapper = new MonthViewCellWrapper(zIndex);
					monthViewCellWrapper.Style[HtmlTextWriterStyle.Height] = this.Owner.RowHeight.ToString();
					cell.Controls.Add(monthViewCellWrapper);
				}
				MonthViewCellWrapper monthViewCellWrapper2 = new MonthViewCellWrapper(zIndex);
				cell.Controls.Add(monthViewCellWrapper2);
				monthViewCellWrapper2.Style[HtmlTextWriterStyle.Height] = this.Owner.RowHeight.ToString();
				monthViewCellWrapper2.Controls.Add(monthViewAppointmentControl);
			}
		}

		// Token: 0x0601059C RID: 66972 RVA: 0x003A69A8 File Offset: 0x003A4BA8
		protected void AddVerticalHeaders(SchedulerTopTable topTable, int totalRowCount)
		{
			this.AddVerticalHeaders(topTable, totalRowCount, 0);
		}

		// Token: 0x0601059D RID: 66973 RVA: 0x003A69B4 File Offset: 0x003A4BB4
		protected void AddVerticalHeaders(SchedulerTopTable topTable, int totalRowCount, int totalBorderHeight)
		{
			topTable.ShowRowHeaders = this.Owner.MonthView.ShowResourceHeadersResolved;
			if (!topTable.ShowRowHeaders)
			{
				return;
			}
			foreach (ViewHeader viewHeader in this.View.RowHeaders)
			{
				SchedulerHeader schedulerHeader = this.CreateSchedulerHeader(viewHeader);
				schedulerHeader.CssClass = viewHeader.ClassName;
				foreach (ViewHeader viewHeader2 in viewHeader.SubHeaders)
				{
					SchedulerHeader schedulerHeader2 = this.CreateSchedulerHeader(viewHeader2);
					schedulerHeader2.CssClass = viewHeader2.ClassName;
					schedulerHeader.SubHeaders.Add(schedulerHeader2);
				}
				topTable.VerticalHeaderPanel.AddHeader(schedulerHeader);
			}
			base.SetTableHeight(topTable.VerticalHeaderPanel.InnerTable, totalRowCount, this.Owner.RowHeight, totalBorderHeight);
		}

		// Token: 0x04004985 RID: 18821
		private ModelBase _model;

		// Token: 0x04004986 RID: 18822
		private ISchedulerView _view;

		// Token: 0x04004987 RID: 18823
		private SchedulerContentPanel _contentPanel;
	}
}
