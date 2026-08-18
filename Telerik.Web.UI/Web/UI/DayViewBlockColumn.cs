using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020012CD RID: 4813
	internal class DayViewBlockColumn
	{
		// Token: 0x17004168 RID: 16744
		// (get) Token: 0x0600CA4F RID: 51791 RVA: 0x002D2610 File Offset: 0x002D0810
		// (set) Token: 0x0600CA50 RID: 51792 RVA: 0x002D2618 File Offset: 0x002D0818
		public ArrayList AppointmentControls
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

		// Token: 0x17004169 RID: 16745
		// (get) Token: 0x0600CA51 RID: 51793 RVA: 0x002D2621 File Offset: 0x002D0821
		// (set) Token: 0x0600CA52 RID: 51794 RVA: 0x002D2629 File Offset: 0x002D0829
		public DayViewBlock Block
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

		// Token: 0x0600CA53 RID: 51795 RVA: 0x002D2632 File Offset: 0x002D0832
		public DayViewBlockColumn(int maxColumnWidth)
		{
			this.MaxColumnWidth = maxColumnWidth;
			this._appointmentControls = new ArrayList();
		}

		// Token: 0x1700416A RID: 16746
		// (get) Token: 0x0600CA54 RID: 51796 RVA: 0x002D264C File Offset: 0x002D084C
		// (set) Token: 0x0600CA55 RID: 51797 RVA: 0x002D2654 File Offset: 0x002D0854
		private int MaxColumnWidth
		{
			get
			{
				return this.maxColumnWidth;
			}
			set
			{
				this.maxColumnWidth = value;
			}
		}

		// Token: 0x1700416B RID: 16747
		// (get) Token: 0x0600CA56 RID: 51798 RVA: 0x002D265D File Offset: 0x002D085D
		public float Width
		{
			get
			{
				return (float)this.MaxColumnWidth / (float)this.Block.Columns.Count;
			}
		}

		// Token: 0x1700416C RID: 16748
		// (get) Token: 0x0600CA57 RID: 51799 RVA: 0x002D2678 File Offset: 0x002D0878
		public float Left
		{
			get
			{
				return (float)this.MaxColumnWidth / (float)this.Block.Columns.Count * (float)this.Block.Columns.IndexOf(this);
			}
		}

		// Token: 0x0600CA58 RID: 51800 RVA: 0x002D26A8 File Offset: 0x002D08A8
		public bool CanAdd(AppointmentControl controlToAdd)
		{
			foreach (object obj in this.AppointmentControls)
			{
				AppointmentControl appointmentControl = (AppointmentControl)obj;
				if (appointmentControl.OverlapsWith(controlToAdd))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600CA59 RID: 51801 RVA: 0x002D270C File Offset: 0x002D090C
		public void Add(AppointmentControl control)
		{
			this.AppointmentControls.Add(control);
			control.Column = this;
		}

		// Token: 0x0400350C RID: 13580
		private ArrayList _appointmentControls;

		// Token: 0x0400350D RID: 13581
		private DayViewBlock _block;

		// Token: 0x0400350E RID: 13582
		private int maxColumnWidth;
	}
}
