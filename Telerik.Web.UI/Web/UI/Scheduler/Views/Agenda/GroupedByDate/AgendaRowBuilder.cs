using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Scheduler.Views.Agenda.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views.Agenda.GroupedByDate
{
	// Token: 0x02000848 RID: 2120
	internal class AgendaRowBuilder : AgendaRowBuilder
	{
		// Token: 0x17001998 RID: 6552
		// (get) Token: 0x06004E4D RID: 20045 RVA: 0x000F56F4 File Offset: 0x000F38F4
		protected RadScheduler Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x06004E4E RID: 20046 RVA: 0x000F56FC File Offset: 0x000F38FC
		public AgendaRowBuilder(IList<TimeSlot> slotList, RadScheduler owner)
		{
			this._owner = owner;
			this.CreateRows(slotList);
		}

		// Token: 0x06004E4F RID: 20047 RVA: 0x000F5714 File Offset: 0x000F3914
		protected new void CreateRows(IList<TimeSlot> slotList)
		{
			base.RowContents = new List<List<Control>>();
			bool addDateHeader = true;
			int num = -1;
			foreach (TimeSlot timeSlot in slotList)
			{
				if (timeSlot.Appointments.Count > 0)
				{
					bool addResourceHeader = false;
					int modelIndex = (timeSlot as TimeSlot).ModelIndex;
					if (modelIndex != num)
					{
						addResourceHeader = true;
						num = modelIndex;
					}
					this.CreateSlotContent(timeSlot, addDateHeader, addResourceHeader);
					addDateHeader = false;
				}
			}
		}

		// Token: 0x06004E50 RID: 20048 RVA: 0x000F579C File Offset: 0x000F399C
		private void CreateSlotContent(SchedulerTimeSlot slot, bool addDateHeader, bool addResourceHeader)
		{
			foreach (Appointment appointment in slot.Appointments)
			{
				List<Control> list = new List<Control>();
				base.RowContents.Add(list);
				if (addDateHeader && slot.Owner.Owner.AgendaView.ShowDateHeadersResolved)
				{
					TableHeaderCell item = base.CreateDayHeader(slot);
					list.Add(item);
					addDateHeader = false;
				}
				if (addResourceHeader && slot.Owner.Owner.AgendaView.ShowResourceHeadersResolved)
				{
					TableCell tableCell = this.CreateResourceHeader(slot);
					list.Add(tableCell);
					tableCell.RowSpan = slot.Appointments.Count;
					addResourceHeader = false;
				}
				TableCell item2 = base.CreateTimeHeader(slot, appointment);
				list.Add(item2);
				TableCell item3 = base.CreateAppointmentCell(slot, appointment);
				list.Add(item3);
			}
		}

		// Token: 0x06004E51 RID: 20049 RVA: 0x000F588C File Offset: 0x000F3A8C
		protected TableCell CreateResourceHeader(SchedulerTimeSlot slot)
		{
			Resource resource = slot.Resource;
			TableHeaderCell tableHeaderCell = new TableHeaderCell
			{
				CssClass = "rsResourceHeader"
			};
			SchedulerResourceContainer schedulerResourceContainer = new SchedulerResourceContainer(this.Owner);
			schedulerResourceContainer.Resource = resource;
			resource.HeaderControls.Add(schedulerResourceContainer);
			tableHeaderCell.Controls.Add(schedulerResourceContainer);
			this.Owner.ResourceHeaderTemplate.InstantiateIn(schedulerResourceContainer);
			return tableHeaderCell;
		}

		// Token: 0x0400137E RID: 4990
		private RadScheduler _owner;
	}
}
