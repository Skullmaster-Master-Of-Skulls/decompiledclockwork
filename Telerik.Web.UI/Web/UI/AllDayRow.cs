using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x020012C7 RID: 4807
	internal class AllDayRow
	{
		// Token: 0x1700413B RID: 16699
		// (get) Token: 0x0600C9AD RID: 51629 RVA: 0x002D0338 File Offset: 0x002CE538
		// (set) Token: 0x0600C9AE RID: 51630 RVA: 0x002D0340 File Offset: 0x002CE540
		public List<AllDayAppointmentControl> AppointmentControls
		{
			get
			{
				return this._appointmentControls;
			}
			set
			{
				this._appointmentControls = value;
			}
		}

		// Token: 0x1700413C RID: 16700
		// (get) Token: 0x0600C9AF RID: 51631 RVA: 0x002D0349 File Offset: 0x002CE549
		// (set) Token: 0x0600C9B0 RID: 51632 RVA: 0x002D0351 File Offset: 0x002CE551
		public AllDayBlock Block
		{
			get
			{
				return this._block;
			}
			set
			{
				this._block = value;
			}
		}

		// Token: 0x0600C9B1 RID: 51633 RVA: 0x002D035A File Offset: 0x002CE55A
		public AllDayRow()
		{
			this._appointmentControls = new List<AllDayAppointmentControl>();
		}

		// Token: 0x1700413D RID: 16701
		// (get) Token: 0x0600C9B2 RID: 51634 RVA: 0x002D036D File Offset: 0x002CE56D
		public int RowIndex
		{
			get
			{
				return this.Block.Rows.IndexOf(this);
			}
		}

		// Token: 0x1700413E RID: 16702
		// (get) Token: 0x0600C9B3 RID: 51635 RVA: 0x002D0380 File Offset: 0x002CE580
		private bool IsLastRow
		{
			get
			{
				return this.Block.Rows.IndexOf(this) == this.Block.Rows.Count - 1;
			}
		}

		// Token: 0x0600C9B4 RID: 51636 RVA: 0x002D03A8 File Offset: 0x002CE5A8
		public bool CanAdd(AllDayAppointmentControl controlToAdd)
		{
			foreach (AllDayAppointmentControl allDayAppointmentControl in this.AppointmentControls)
			{
				if (allDayAppointmentControl.OverlapsWith(controlToAdd))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600C9B5 RID: 51637 RVA: 0x002D0404 File Offset: 0x002CE604
		public void Add(AllDayAppointmentControl control)
		{
			this.AppointmentControls.Add(control);
			control.Row = this;
		}

		// Token: 0x040034F3 RID: 13555
		private List<AllDayAppointmentControl> _appointmentControls;

		// Token: 0x040034F4 RID: 13556
		private AllDayBlock _block;
	}
}
