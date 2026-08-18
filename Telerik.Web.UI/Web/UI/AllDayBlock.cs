using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x020012C6 RID: 4806
	internal class AllDayBlock
	{
		// Token: 0x17004137 RID: 16695
		// (get) Token: 0x0600C9A3 RID: 51619 RVA: 0x002D0080 File Offset: 0x002CE280
		public List<AllDayAppointmentControl> Appointments
		{
			get
			{
				return this._appointmentControls;
			}
		}

		// Token: 0x0600C9A4 RID: 51620 RVA: 0x002D0088 File Offset: 0x002CE288
		public AllDayBlock()
		{
			this._rows = new List<AllDayRow>();
			this._appointmentControls = new List<AllDayAppointmentControl>();
		}

		// Token: 0x17004138 RID: 16696
		// (get) Token: 0x0600C9A5 RID: 51621 RVA: 0x002D00A6 File Offset: 0x002CE2A6
		// (set) Token: 0x0600C9A6 RID: 51622 RVA: 0x002D00AE File Offset: 0x002CE2AE
		public List<AllDayRow> Rows
		{
			get
			{
				return this._rows;
			}
			set
			{
				this._rows = value;
			}
		}

		// Token: 0x17004139 RID: 16697
		// (get) Token: 0x0600C9A7 RID: 51623 RVA: 0x002D00B8 File Offset: 0x002CE2B8
		public DateTime BoxStart
		{
			get
			{
				DateTime dateTime = DateTime.MaxValue;
				foreach (AllDayAppointmentControl allDayAppointmentControl in this._appointmentControls)
				{
					if (allDayAppointmentControl.BoxStart < dateTime)
					{
						dateTime = allDayAppointmentControl.BoxStart;
					}
				}
				return dateTime;
			}
		}

		// Token: 0x1700413A RID: 16698
		// (get) Token: 0x0600C9A8 RID: 51624 RVA: 0x002D0120 File Offset: 0x002CE320
		public DateTime BoxEnd
		{
			get
			{
				DateTime dateTime = DateTime.MinValue;
				foreach (AllDayAppointmentControl allDayAppointmentControl in this._appointmentControls)
				{
					if (allDayAppointmentControl.BoxEnd > dateTime)
					{
						dateTime = allDayAppointmentControl.BoxEnd;
					}
				}
				return dateTime;
			}
		}

		// Token: 0x0600C9A9 RID: 51625 RVA: 0x002D0188 File Offset: 0x002CE388
		public void Add(AllDayAppointmentControl control)
		{
			this._appointmentControls.Add(control);
			this.ArrangeRows();
		}

		// Token: 0x0600C9AA RID: 51626 RVA: 0x002D019C File Offset: 0x002CE39C
		public bool OverlapsWith(AllDayAppointmentControl control)
		{
			return this._appointmentControls.Count != 0 && ((this.BoxStart == control.BoxStart && control.BoxStart == control.BoxEnd) || (this.BoxStart <= control.BoxEnd && this.BoxEnd > control.BoxStart));
		}

		// Token: 0x0600C9AB RID: 51627 RVA: 0x002D0208 File Offset: 0x002CE408
		private AllDayRow CreateRow()
		{
			AllDayRow allDayRow = new AllDayRow();
			this.Rows.Add(allDayRow);
			allDayRow.Block = this;
			return allDayRow;
		}

		// Token: 0x0600C9AC RID: 51628 RVA: 0x002D0230 File Offset: 0x002CE430
		private void ArrangeRows()
		{
			this.Rows = new List<AllDayRow>();
			foreach (AllDayAppointmentControl allDayAppointmentControl in this._appointmentControls)
			{
				allDayAppointmentControl.Row = null;
			}
			this.CreateRow();
			foreach (AllDayAppointmentControl allDayAppointmentControl2 in this._appointmentControls)
			{
				foreach (AllDayRow allDayRow in this.Rows)
				{
					if (allDayRow.CanAdd(allDayAppointmentControl2))
					{
						allDayRow.Add(allDayAppointmentControl2);
						break;
					}
				}
				if (allDayAppointmentControl2.Row == null)
				{
					AllDayRow allDayRow2 = this.CreateRow();
					allDayRow2.Add(allDayAppointmentControl2);
				}
			}
		}

		// Token: 0x040034F1 RID: 13553
		private List<AllDayRow> _rows;

		// Token: 0x040034F2 RID: 13554
		private readonly List<AllDayAppointmentControl> _appointmentControls;
	}
}
