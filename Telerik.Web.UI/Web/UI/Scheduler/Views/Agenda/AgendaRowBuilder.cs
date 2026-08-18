using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduler.Views.Agenda
{
	// Token: 0x02000847 RID: 2119
	internal class AgendaRowBuilder
	{
		// Token: 0x17001996 RID: 6550
		// (get) Token: 0x06004E40 RID: 20032 RVA: 0x000F525C File Offset: 0x000F345C
		internal int RowCount
		{
			get
			{
				return this.RowContents.Count;
			}
		}

		// Token: 0x17001997 RID: 6551
		// (get) Token: 0x06004E41 RID: 20033 RVA: 0x000F5269 File Offset: 0x000F3469
		// (set) Token: 0x06004E42 RID: 20034 RVA: 0x000F5271 File Offset: 0x000F3471
		internal List<List<Control>> RowContents
		{
			get
			{
				return this._rowContents;
			}
			set
			{
				this._rowContents = value;
			}
		}

		// Token: 0x06004E43 RID: 20035 RVA: 0x000F527A File Offset: 0x000F347A
		public AgendaRowBuilder()
		{
		}

		// Token: 0x06004E44 RID: 20036 RVA: 0x000F5282 File Offset: 0x000F3482
		public AgendaRowBuilder(IList<TimeSlot> slotList) : this()
		{
			this.CreateRows(slotList);
		}

		// Token: 0x06004E45 RID: 20037 RVA: 0x000F5294 File Offset: 0x000F3494
		protected void CreateRows(IList<TimeSlot> slotList)
		{
			this.RowContents = new List<List<Control>>();
			foreach (TimeSlot timeSlot in slotList)
			{
				if (timeSlot.Appointments.Count > 0)
				{
					this.CreateSlotContent(timeSlot);
				}
			}
		}

		// Token: 0x06004E46 RID: 20038 RVA: 0x000F52F8 File Offset: 0x000F34F8
		public List<Control> GetRowContent(int rowIndex)
		{
			return this.RowContents[rowIndex];
		}

		// Token: 0x06004E47 RID: 20039 RVA: 0x000F5308 File Offset: 0x000F3508
		private void CreateSlotContent(SchedulerTimeSlot slot)
		{
			bool flag = true;
			foreach (Appointment appointment in slot.Appointments)
			{
				List<Control> list = new List<Control>();
				this.RowContents.Add(list);
				if (flag && slot.Owner.Owner.AgendaView.ShowDateHeadersResolved)
				{
					TableHeaderCell tableHeaderCell = this.CreateDayHeader(slot);
					list.Add(tableHeaderCell);
					tableHeaderCell.RowSpan = slot.Appointments.Count;
					flag = false;
				}
				TableCell item = this.CreateTimeHeader(slot, appointment);
				list.Add(item);
				TableCell item2 = this.CreateAppointmentCell(slot, appointment);
				list.Add(item2);
			}
		}

		// Token: 0x06004E48 RID: 20040 RVA: 0x000F53CC File Offset: 0x000F35CC
		protected TableHeaderCell CreateDayHeader(SchedulerTimeSlot slot)
		{
			DateTime date = slot.Owner.Owner.UtcToDisplay(slot.Start).Date;
			TableHeaderCell tableHeaderCell = new TableHeaderCell();
			tableHeaderCell.CssClass = "rsAgendaDateHeader";
			WebControl webControl = new WebControl(HtmlTextWriterTag.Span);
			webControl.CssClass = "rsDateBox";
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Strong);
			WebControl webControl3 = new WebControl(HtmlTextWriterTag.A);
			webControl3.Attributes["href"] = "#" + date.ToString("yyyy-MM-dd");
			webControl3.CssClass = "rsDateHeader";
			webControl3.Controls.Add(new LiteralControl(date.Day.ToString()));
			webControl2.Controls.Add(webControl3);
			webControl.Controls.Add(webControl2);
			WebControl webControl4 = new WebControl(HtmlTextWriterTag.Em);
			webControl4.Controls.Add(new LiteralControl(((RadScheduler)slot.Owner.Owner).Culture.DateTimeFormat.GetDayName(date.DayOfWeek)));
			webControl.Controls.Add(webControl4);
			WebControl webControl5 = new WebControl(HtmlTextWriterTag.Small);
			webControl5.Controls.Add(new LiteralControl(date.ToString("MMMM, yyyy", ((RadScheduler)slot.Owner.Owner).Culture)));
			webControl.Controls.Add(webControl5);
			tableHeaderCell.Controls.Add(webControl);
			return tableHeaderCell;
		}

		// Token: 0x06004E49 RID: 20041 RVA: 0x000F553C File Offset: 0x000F373C
		protected TableCell CreateTimeHeader(SchedulerTimeSlot slot, Appointment appointment)
		{
			string timeHeaderFormatString = this.GetTimeHeaderFormatString(slot, appointment);
			return new TableCell
			{
				Text = string.Format(timeHeaderFormatString, appointment.Owner.UtcToDisplay(appointment.Start).ToShortTimeString(), appointment.Owner.UtcToDisplay(appointment.End).ToShortTimeString())
			};
		}

		// Token: 0x06004E4A RID: 20042 RVA: 0x000F5598 File Offset: 0x000F3798
		private string GetTimeHeaderFormatString(SchedulerTimeSlot slot, Appointment appointment)
		{
			DateTime start = slot.Start;
			DateTime end = slot.End;
			DateTime start2 = appointment.Start;
			DateTime end2 = appointment.End;
			string allDay = slot.Owner.Owner.Localization.AllDay;
			if (!(start2 < start))
			{
				if (start2 == start)
				{
					if (end2 == end)
					{
						return allDay;
					}
					if (end2 > end)
					{
						return string.Format("{0} &#187;", allDay);
					}
				}
				else if (end2 > end)
				{
					return "{0} &#187;";
				}
				return "{0} - {1}";
			}
			if (end2 == end)
			{
				return string.Format("&#187; {0}", allDay);
			}
			if (end2 > end)
			{
				return string.Format("&#187; {0} &#187;", allDay);
			}
			return "&#187; {1}";
		}

		// Token: 0x06004E4B RID: 20043 RVA: 0x000F5654 File Offset: 0x000F3854
		protected TableCell CreateAppointmentCell(SchedulerTimeSlot slot, Appointment appointment)
		{
			Appointment activeFormAppointment = ((RadScheduler)slot.Owner.Owner).ActiveFormAppointment;
			TableCell tableCell = new TableCell();
			this.CreateAppointmentControl(tableCell, appointment);
			if (slot.FormContainer != null && activeFormAppointment != null && activeFormAppointment.ID != null && activeFormAppointment.ID.Equals(appointment.ID))
			{
				DayViewCellWrapper dayViewCellWrapper = new DayViewCellWrapper(1000);
				tableCell.Controls.Add(dayViewCellWrapper);
				dayViewCellWrapper.Controls.Add(slot.FormContainer);
			}
			return tableCell;
		}

		// Token: 0x06004E4C RID: 20044 RVA: 0x000F56D4 File Offset: 0x000F38D4
		private void CreateAppointmentControl(Control container, Appointment appointment)
		{
			AgendaViewAppointmentControl child = new AgendaViewAppointmentControl(appointment);
			container.Controls.Add(child);
		}

		// Token: 0x0400137D RID: 4989
		private List<List<Control>> _rowContents;
	}
}
