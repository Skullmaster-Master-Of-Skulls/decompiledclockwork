using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020012CC RID: 4812
	internal class DayViewBlock
	{
		// Token: 0x17004163 RID: 16739
		// (get) Token: 0x0600CA43 RID: 51779 RVA: 0x002D22B7 File Offset: 0x002D04B7
		public ArrayList Appointments
		{
			get
			{
				return this._appointmentControls;
			}
		}

		// Token: 0x0600CA44 RID: 51780 RVA: 0x002D22BF File Offset: 0x002D04BF
		public DayViewBlock(int maxColumnWidth)
		{
			this.MaxColumnWidth = maxColumnWidth;
			this._columns = new ArrayList();
			this._appointmentControls = new ArrayList();
		}

		// Token: 0x17004164 RID: 16740
		// (get) Token: 0x0600CA45 RID: 51781 RVA: 0x002D22E4 File Offset: 0x002D04E4
		// (set) Token: 0x0600CA46 RID: 51782 RVA: 0x002D22EC File Offset: 0x002D04EC
		private int MaxColumnWidth
		{
			get
			{
				return this._maxColumnWidth;
			}
			set
			{
				this._maxColumnWidth = value;
			}
		}

		// Token: 0x17004165 RID: 16741
		// (get) Token: 0x0600CA47 RID: 51783 RVA: 0x002D22F5 File Offset: 0x002D04F5
		// (set) Token: 0x0600CA48 RID: 51784 RVA: 0x002D22FD File Offset: 0x002D04FD
		public ArrayList Columns
		{
			get
			{
				return this._columns;
			}
			set
			{
				this._columns = value;
			}
		}

		// Token: 0x0600CA49 RID: 51785 RVA: 0x002D2306 File Offset: 0x002D0506
		public void Add(AppointmentControl control)
		{
			this._appointmentControls.Add(control);
			this.ArrangeColumns();
		}

		// Token: 0x0600CA4A RID: 51786 RVA: 0x002D231C File Offset: 0x002D051C
		private DayViewBlockColumn CreateColumn()
		{
			DayViewBlockColumn dayViewBlockColumn = new DayViewBlockColumn(this.MaxColumnWidth);
			this.Columns.Add(dayViewBlockColumn);
			dayViewBlockColumn.Block = this;
			return dayViewBlockColumn;
		}

		// Token: 0x0600CA4B RID: 51787 RVA: 0x002D234C File Offset: 0x002D054C
		private void ArrangeColumns()
		{
			this.Columns = new ArrayList();
			foreach (object obj in this._appointmentControls)
			{
				AppointmentControl appointmentControl = (AppointmentControl)obj;
				appointmentControl.Column = null;
			}
			this.CreateColumn();
			foreach (object obj2 in this._appointmentControls)
			{
				AppointmentControl appointmentControl2 = (AppointmentControl)obj2;
				foreach (object obj3 in this.Columns)
				{
					DayViewBlockColumn dayViewBlockColumn = (DayViewBlockColumn)obj3;
					if (dayViewBlockColumn.CanAdd(appointmentControl2))
					{
						dayViewBlockColumn.Add(appointmentControl2);
						break;
					}
				}
				if (appointmentControl2.Column == null)
				{
					DayViewBlockColumn dayViewBlockColumn2 = this.CreateColumn();
					dayViewBlockColumn2.Add(appointmentControl2);
				}
			}
		}

		// Token: 0x0600CA4C RID: 51788 RVA: 0x002D2478 File Offset: 0x002D0678
		public bool OverlapsWith(AppointmentControl control)
		{
			if (this._appointmentControls.Count == 0)
			{
				return false;
			}
			if (control.Appointment.Duration == TimeSpan.Zero && this.BoxEnd == this.BoxStart)
			{
				return control.BoxStart == this.BoxStart;
			}
			if (this.BoxStart == this.BoxEnd)
			{
				return this.BoxStart < control.BoxEnd && this.BoxEnd >= control.BoxStart;
			}
			return this.BoxStart <= control.BoxEnd && this.BoxEnd > control.BoxStart;
		}

		// Token: 0x17004166 RID: 16742
		// (get) Token: 0x0600CA4D RID: 51789 RVA: 0x002D2530 File Offset: 0x002D0730
		public DateTime BoxStart
		{
			get
			{
				DateTime dateTime = DateTime.MaxValue;
				foreach (object obj in this._appointmentControls)
				{
					AppointmentControl appointmentControl = (AppointmentControl)obj;
					if (appointmentControl.BoxStart < dateTime)
					{
						dateTime = appointmentControl.BoxStart;
					}
				}
				return dateTime;
			}
		}

		// Token: 0x17004167 RID: 16743
		// (get) Token: 0x0600CA4E RID: 51790 RVA: 0x002D25A0 File Offset: 0x002D07A0
		public DateTime BoxEnd
		{
			get
			{
				DateTime dateTime = DateTime.MinValue;
				foreach (object obj in this._appointmentControls)
				{
					AppointmentControl appointmentControl = (AppointmentControl)obj;
					if (appointmentControl.BoxEnd > dateTime)
					{
						dateTime = appointmentControl.BoxEnd;
					}
				}
				return dateTime;
			}
		}

		// Token: 0x04003509 RID: 13577
		private ArrayList _columns;

		// Token: 0x0400350A RID: 13578
		private ArrayList _appointmentControls;

		// Token: 0x0400350B RID: 13579
		private int _maxColumnWidth;
	}
}
