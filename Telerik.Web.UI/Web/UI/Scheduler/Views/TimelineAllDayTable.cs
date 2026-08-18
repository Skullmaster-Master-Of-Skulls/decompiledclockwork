using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduler.Views
{
	// Token: 0x02000E78 RID: 3704
	internal class TimelineAllDayTable : SchedulerAllDayTable
	{
		// Token: 0x06008C74 RID: 35956 RVA: 0x001FE023 File Offset: 0x001FC223
		public TimelineAllDayTable(RadScheduler owner) : base(owner)
		{
		}

		// Token: 0x06008C75 RID: 35957 RVA: 0x001FE02C File Offset: 0x001FC22C
		protected override void CreateAllDayCells(WebControl row, Dictionary<string, List<AppointmentControl>> appointmentControls)
		{
			int num = 1;
			int num2 = 0;
			TableCell tableCell = null;
			Resource res = null;
			List<WebControl> list = new List<WebControl>();
			for (int i = 0; i < base.AllDaySlots.Count; i++)
			{
				ISchedulerTimeSlot schedulerTimeSlot = base.AllDaySlots[i];
				TableCell tableCell2 = new TableCell();
				row.Controls.Add(tableCell2);
				schedulerTimeSlot.Control = tableCell2;
				if (i == 0)
				{
					tableCell = tableCell2;
					tableCell2.CssClass = (schedulerTimeSlot.CssClass = "rsFirstCell");
				}
				if (schedulerTimeSlot.Resource != null && !schedulerTimeSlot.Resource.Equals(res))
				{
					tableCell = tableCell2;
					tableCell2.CssClass = (schedulerTimeSlot.CssClass = "rsFirstCell");
					res = schedulerTimeSlot.Resource;
					list = new List<WebControl>();
					num2 = 0;
				}
				foreach (AppointmentControl appointmentControl in appointmentControls[schedulerTimeSlot.Index])
				{
					AllDayAppointmentControl allDayAppointmentControl = (AllDayAppointmentControl)appointmentControl;
					int rowIndex = allDayAppointmentControl.Row.RowIndex;
					for (int j = list.Count; j <= rowIndex; j++)
					{
						WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
						webControl.CssClass = "rsWrap";
						webControl.Style[HtmlTextWriterStyle.Height] = base.Owner.RowHeight.ToString();
						list.Add(webControl);
						tableCell.Controls.Add(webControl);
					}
					list[rowIndex].Controls.Add(allDayAppointmentControl);
					if (SchedulerAllDayTable.ShowAllDayEditForm(allDayAppointmentControl.Appointment, schedulerTimeSlot))
					{
						schedulerTimeSlot.FormContainer.LeftOffset = num2;
						list[rowIndex].Controls.Add(schedulerTimeSlot.FormContainer);
					}
				}
				bool flag = i == base.AllDaySlots.Count - 1;
				bool flag2 = flag || (base.AllDaySlots[i].Resource != null && !base.AllDaySlots[i].Resource.Equals(base.AllDaySlots[i + 1].Resource));
				if (flag || flag2)
				{
					bool flag3 = tableCell.Controls.Count == 0;
					if (base.ShowInsertArea || flag3)
					{
						base.AddSpacer(tableCell, 1);
					}
					else
					{
						WebControl webControl2 = (WebControl)tableCell.Controls[tableCell.Controls.Count - 1];
						webControl2.Style[HtmlTextWriterStyle.Height] = base.Owner.AdjustedRowHeight;
					}
				}
				if (schedulerTimeSlot.FormContainer != null && schedulerTimeSlot.FormContainer.Mode == SchedulerFormMode.Insert)
				{
					if (tableCell2.Controls.Count == 0)
					{
						base.AddSpacer(tableCell2, 1);
					}
					WebControl webControl3 = (WebControl)tableCell2.Controls[0];
					webControl3.Controls.Add(schedulerTimeSlot.FormContainer);
				}
				num = Math.Max(num, tableCell.Controls.Count);
				num2++;
			}
			row.Style[HtmlTextWriterStyle.Height] = SchedulerUnit.GetValue((double)num * base.Owner.RowHeight.Value, base.Owner.RowHeight.Type);
		}

		// Token: 0x06008C76 RID: 35958 RVA: 0x001FE38C File Offset: 0x001FC58C
		public override void AddPadding(int targetHeight)
		{
		}
	}
}
