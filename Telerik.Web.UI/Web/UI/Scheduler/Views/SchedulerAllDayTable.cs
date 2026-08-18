using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduler.Views
{
	// Token: 0x02000E77 RID: 3703
	internal class SchedulerAllDayTable : SchedulerTable
	{
		// Token: 0x17002C62 RID: 11362
		// (get) Token: 0x06008C67 RID: 35943 RVA: 0x001FDA9B File Offset: 0x001FBC9B
		// (set) Token: 0x06008C68 RID: 35944 RVA: 0x001FDAA3 File Offset: 0x001FBCA3
		public RadScheduler Owner
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

		// Token: 0x17002C63 RID: 11363
		// (get) Token: 0x06008C69 RID: 35945 RVA: 0x001FDAAC File Offset: 0x001FBCAC
		// (set) Token: 0x06008C6A RID: 35946 RVA: 0x001FDAB4 File Offset: 0x001FBCB4
		public IList<ISchedulerTimeSlot> AllDaySlots
		{
			get
			{
				return this._allDaySlots;
			}
			protected set
			{
				this._allDaySlots = value;
			}
		}

		// Token: 0x17002C64 RID: 11364
		// (get) Token: 0x06008C6B RID: 35947 RVA: 0x001FDABD File Offset: 0x001FBCBD
		// (set) Token: 0x06008C6C RID: 35948 RVA: 0x001FDAC5 File Offset: 0x001FBCC5
		public bool ShowInsertArea
		{
			get
			{
				return this._showInsertArea;
			}
			set
			{
				this._showInsertArea = value;
			}
		}

		// Token: 0x06008C6D RID: 35949 RVA: 0x001FDACE File Offset: 0x001FBCCE
		public SchedulerAllDayTable(RadScheduler owner)
		{
			this.Owner = owner;
			this._showInsertArea = true;
		}

		// Token: 0x06008C6E RID: 35950 RVA: 0x001FDAE4 File Offset: 0x001FBCE4
		public virtual void AddRow(IList<ISchedulerTimeSlot> allDaySlots, Dictionary<string, List<AppointmentControl>> appointmentControls)
		{
			this.AllDaySlots = allDaySlots;
			this.CssClass = "rsAllDayTable";
			if (this.Owner.OverflowBehavior != OverflowBehavior.Scroll)
			{
				base.Style["border-right"] = "0px none";
			}
			TableRow tableRow = new TableRow();
			this.Controls.Add(tableRow);
			tableRow.CssClass = "rsAllDayRow";
			this.CreateAllDayCells(tableRow, appointmentControls);
			if (allDaySlots.Count > 0)
			{
				allDaySlots[allDaySlots.Count - 1].CssClass = "rsLastCell";
			}
		}

		// Token: 0x06008C6F RID: 35951 RVA: 0x001FDB6C File Offset: 0x001FBD6C
		protected virtual void CreateAllDayCells(WebControl row, Dictionary<string, List<AppointmentControl>> appointmentControls)
		{
			int num = this.AllDaySlots.Count;
			int num2 = 1;
			foreach (ISchedulerTimeSlot schedulerTimeSlot in this.AllDaySlots)
			{
				TableCell tableCell = new TableCell();
				row.Controls.Add(tableCell);
				schedulerTimeSlot.Control = tableCell;
				foreach (AppointmentControl appointmentControl in appointmentControls[schedulerTimeSlot.Index])
				{
					AllDayAppointmentControl allDayAppointmentControl = (AllDayAppointmentControl)appointmentControl;
					WebControl webControl = this.Owner.CreateWrapper();
					webControl.Style["z-index"] = num.ToString();
					tableCell.Controls.Add(webControl);
					webControl.Controls.Add(allDayAppointmentControl);
					if (SchedulerAllDayTable.ShowAllDayEditForm(allDayAppointmentControl.Appointment, schedulerTimeSlot))
					{
						tableCell.Controls.Add(schedulerTimeSlot.FormContainer);
					}
					int rowIndex = allDayAppointmentControl.Row.RowIndex;
					int num3 = tableCell.Controls.IndexOf(webControl);
					for (int i = num3; i < rowIndex; i++)
					{
						WebControl child = this.Owner.CreateSpacer();
						tableCell.Controls.AddAt(num3, child);
					}
				}
				bool flag = tableCell.Controls.Count == 0;
				if (this.ShowInsertArea || flag)
				{
					this.AddSpacer(tableCell, 1);
				}
				if (schedulerTimeSlot.FormContainer != null && schedulerTimeSlot.FormContainer.Mode == SchedulerFormMode.Insert)
				{
					WebControl webControl2 = (WebControl)tableCell.Controls[0];
					webControl2.Style["z-index"] = (this.AllDaySlots.Count + 1).ToString();
					webControl2.Controls.Add(schedulerTimeSlot.FormContainer);
				}
				num--;
				num2 = Math.Max(num2, tableCell.Controls.Count);
			}
			row.Style[HtmlTextWriterStyle.Height] = SchedulerUnit.GetValue((double)num2 * this.Owner.RowHeight.Value, this.Owner.RowHeight.Type);
		}

		// Token: 0x06008C70 RID: 35952 RVA: 0x001FDDD0 File Offset: 0x001FBFD0
		public virtual void AddPadding(int targetHeight)
		{
			foreach (object obj in this.Rows[0].Cells)
			{
				TableCell tableCell = (TableCell)obj;
				this.AddSpacer(tableCell, targetHeight - tableCell.Controls.Count);
			}
		}

		// Token: 0x06008C71 RID: 35953 RVA: 0x001FDE44 File Offset: 0x001FC044
		protected void AddSpacer(Control cell, int count)
		{
			for (int i = 0; i < count; i++)
			{
				WebControl child = this.Owner.CreateSpacer();
				cell.Controls.Add(child);
			}
		}

		// Token: 0x06008C72 RID: 35954 RVA: 0x001FDE78 File Offset: 0x001FC078
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			foreach (object obj in this.Rows)
			{
				TableRow tableRow = (TableRow)obj;
				foreach (object obj2 in tableRow.Cells)
				{
					TableCell tableCell = (TableCell)obj2;
					if (tableCell.Controls.Count > 0)
					{
						WebControl webControl = tableCell.Controls[tableCell.Controls.Count - 1] as WebControl;
						if (webControl != null)
						{
							WebControl webControl2 = webControl;
							webControl2.CssClass += " rsLastSpacingWrapper";
						}
					}
				}
			}
		}

		// Token: 0x06008C73 RID: 35955 RVA: 0x001FDF68 File Offset: 0x001FC168
		protected static bool ShowAllDayEditForm(Appointment appointmentToRender, ISchedulerTimeSlot slot)
		{
			RadScheduler owner = appointmentToRender.Owner;
			SchedulerFormContainer formContainer = slot.FormContainer;
			if (formContainer == null || formContainer.Mode != SchedulerFormMode.Edit)
			{
				return false;
			}
			Appointment activeFormAppointment = owner.ActiveFormAppointment;
			if (!owner.EditingRecurringSeries)
			{
				if (activeFormAppointment.RecurrenceState == RecurrenceState.Exception)
				{
					if (appointmentToRender.RecurrenceState == RecurrenceState.Master)
					{
						return activeFormAppointment.RecurrenceParentID.Equals(appointmentToRender.ID);
					}
					if (appointmentToRender.RecurrenceState == RecurrenceState.Occurrence)
					{
						return appointmentToRender.RecurrenceParentID.Equals(activeFormAppointment.RecurrenceParentID);
					}
				}
				return appointmentToRender.ID.Equals(activeFormAppointment.ID);
			}
			if (appointmentToRender.RecurrenceState == RecurrenceState.Master)
			{
				return appointmentToRender.ID.Equals(activeFormAppointment.ID);
			}
			return appointmentToRender.RecurrenceParentID.Equals(activeFormAppointment.ID);
		}

		// Token: 0x04002770 RID: 10096
		private RadScheduler _owner;

		// Token: 0x04002771 RID: 10097
		private IList<ISchedulerTimeSlot> _allDaySlots;

		// Token: 0x04002772 RID: 10098
		private bool _showInsertArea;
	}
}
