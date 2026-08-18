using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views
{
	// Token: 0x02001A49 RID: 6729
	internal class AllDayLayout
	{
		// Token: 0x17004F36 RID: 20278
		// (get) Token: 0x06010522 RID: 66850 RVA: 0x003A4C8E File Offset: 0x003A2E8E
		public Dictionary<string, List<AppointmentControl>> AppointmentControls
		{
			get
			{
				if (this._appointmentControls == null)
				{
					this._appointmentControls = this.CreateAppointmentControls(this.Slots, this.RegisterAppointmentControls);
				}
				return this._appointmentControls;
			}
		}

		// Token: 0x17004F37 RID: 20279
		// (get) Token: 0x06010523 RID: 66851 RVA: 0x003A4CB6 File Offset: 0x003A2EB6
		// (set) Token: 0x06010524 RID: 66852 RVA: 0x003A4CBE File Offset: 0x003A2EBE
		public IComparer<Appointment> AppointmentComparer { get; set; }

		// Token: 0x17004F38 RID: 20280
		// (get) Token: 0x06010525 RID: 66853 RVA: 0x003A4CC7 File Offset: 0x003A2EC7
		public int ActualRowCount
		{
			get
			{
				if (this._appointmentControls == null)
				{
					this._appointmentControls = this.CreateAppointmentControls(this.Slots, this.RegisterAppointmentControls);
				}
				return this.actualRowCount;
			}
		}

		// Token: 0x17004F39 RID: 20281
		// (get) Token: 0x06010526 RID: 66854 RVA: 0x003A4CEF File Offset: 0x003A2EEF
		// (set) Token: 0x06010527 RID: 66855 RVA: 0x003A4CF7 File Offset: 0x003A2EF7
		protected List<AllDayBlock> Blocks { get; set; }

		// Token: 0x17004F3A RID: 20282
		// (get) Token: 0x06010528 RID: 66856 RVA: 0x003A4D00 File Offset: 0x003A2F00
		protected AllDayBlock CurrentBlock
		{
			get
			{
				if (this.Blocks.Count == 0)
				{
					return null;
				}
				return this.Blocks[this.Blocks.Count - 1];
			}
		}

		// Token: 0x17004F3B RID: 20283
		// (get) Token: 0x06010529 RID: 66857 RVA: 0x003A4D29 File Offset: 0x003A2F29
		// (set) Token: 0x0601052A RID: 66858 RVA: 0x003A4D31 File Offset: 0x003A2F31
		private IEnumerable<ISchedulerTimeSlot> Slots { get; set; }

		// Token: 0x17004F3C RID: 20284
		// (get) Token: 0x0601052B RID: 66859 RVA: 0x003A4D3A File Offset: 0x003A2F3A
		// (set) Token: 0x0601052C RID: 66860 RVA: 0x003A4D42 File Offset: 0x003A2F42
		private bool RegisterAppointmentControls { get; set; }

		// Token: 0x0601052D RID: 66861 RVA: 0x003A4D4B File Offset: 0x003A2F4B
		public AllDayLayout(IEnumerable<ISchedulerTimeSlot> slots) : this(slots, true)
		{
		}

		// Token: 0x0601052E RID: 66862 RVA: 0x003A4D55 File Offset: 0x003A2F55
		public AllDayLayout(IEnumerable<ISchedulerTimeSlot> slots, bool registerAppointmentControls)
		{
			this.Blocks = new List<AllDayBlock>();
			this.Slots = slots;
			this.RegisterAppointmentControls = registerAppointmentControls;
		}

		// Token: 0x0601052F RID: 66863 RVA: 0x003A4D78 File Offset: 0x003A2F78
		protected virtual void AddToSizingBlocks(AllDayAppointmentControl control)
		{
			if (this.CurrentBlock == null)
			{
				this.Blocks.Add(new AllDayBlock());
			}
			else if (!this.CurrentBlock.OverlapsWith(control))
			{
				this.Blocks.Add(new AllDayBlock());
			}
			this.CurrentBlock.Add(control);
		}

		// Token: 0x06010530 RID: 66864 RVA: 0x003A4DCC File Offset: 0x003A2FCC
		protected virtual AllDayAppointmentControl CreateAppointmentControl(Appointment appointment, ISchedulerTimeSlot slot, bool registerAppointmentControls)
		{
			AllDayAppointmentControl allDayAppointmentControl = new AllDayAppointmentControl(appointment, slot, registerAppointmentControls);
			this.AddToSizingBlocks(allDayAppointmentControl);
			return allDayAppointmentControl;
		}

		// Token: 0x06010531 RID: 66865 RVA: 0x003A4DEC File Offset: 0x003A2FEC
		private Dictionary<string, List<AppointmentControl>> CreateAppointmentControls(IEnumerable<ISchedulerTimeSlot> slots, bool registerAppointmentControls)
		{
			List<AppointmentControl> list = new List<AppointmentControl>();
			Dictionary<string, List<AppointmentControl>> dictionary = new Dictionary<string, List<AppointmentControl>>();
			Dictionary<Appointment, ISchedulerTimeSlot> dictionary2 = new Dictionary<Appointment, ISchedulerTimeSlot>();
			foreach (ISchedulerTimeSlot schedulerTimeSlot in slots)
			{
				List<AppointmentControl> value = new List<AppointmentControl>();
				dictionary.Add(schedulerTimeSlot.Index, value);
				foreach (Appointment key in schedulerTimeSlot.Appointments)
				{
					dictionary2.Add(key, schedulerTimeSlot);
				}
			}
			List<Appointment> list2 = new List<Appointment>(dictionary2.Keys);
			if (this.AppointmentComparer != null)
			{
				list2.Sort(this.AppointmentComparer);
			}
			foreach (Appointment appointment in list2)
			{
				ISchedulerTimeSlot schedulerTimeSlot2 = dictionary2[appointment];
				AllDayAppointmentControl allDayAppointmentControl = this.CreateAppointmentControl(appointment, schedulerTimeSlot2, registerAppointmentControls);
				list.Add(allDayAppointmentControl);
				List<AppointmentControl> list3 = dictionary[schedulerTimeSlot2.Index];
				list3.Add(allDayAppointmentControl);
				int rowIndex = allDayAppointmentControl.Row.RowIndex;
				this.actualRowCount = Math.Max(rowIndex + 1, this.actualRowCount);
			}
			foreach (AppointmentControl appointmentControl in list)
			{
				AllDayAppointmentControl allDayAppointmentControl2 = (AllDayAppointmentControl)appointmentControl;
				allDayAppointmentControl2.CalculateSize();
			}
			return dictionary;
		}

		// Token: 0x04004972 RID: 18802
		private Dictionary<string, List<AppointmentControl>> _appointmentControls;

		// Token: 0x04004973 RID: 18803
		private int actualRowCount;
	}
}
