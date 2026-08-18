using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Scheduler.Views.Week;

namespace Telerik.Web.UI.Scheduler.Views
{
	// Token: 0x02001AA7 RID: 6823
	internal class RowBuilder
	{
		// Token: 0x17005011 RID: 20497
		// (get) Token: 0x060107D6 RID: 67542 RVA: 0x003AF5D0 File Offset: 0x003AD7D0
		// (set) Token: 0x060107D7 RID: 67543 RVA: 0x003AF5D8 File Offset: 0x003AD7D8
		private List<DayViewBlock> Blocks
		{
			get
			{
				return this._blocks;
			}
			set
			{
				this._blocks = value;
			}
		}

		// Token: 0x17005012 RID: 20498
		// (get) Token: 0x060107D8 RID: 67544 RVA: 0x003AF5E1 File Offset: 0x003AD7E1
		private DayViewBlock CurrentBlock
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

		// Token: 0x17005013 RID: 20499
		// (get) Token: 0x060107D9 RID: 67545 RVA: 0x003AF60A File Offset: 0x003AD80A
		// (set) Token: 0x060107DA RID: 67546 RVA: 0x003AF612 File Offset: 0x003AD812
		public int RowCount
		{
			get
			{
				return this._rowCount;
			}
			protected set
			{
				this._rowCount = value;
			}
		}

		// Token: 0x17005014 RID: 20500
		// (get) Token: 0x060107DB RID: 67547 RVA: 0x003AF61B File Offset: 0x003AD81B
		// (set) Token: 0x060107DC RID: 67548 RVA: 0x003AF623 File Offset: 0x003AD823
		protected int MaxColumnWidth
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

		// Token: 0x17005015 RID: 20501
		// (get) Token: 0x060107DD RID: 67549 RVA: 0x003AF62C File Offset: 0x003AD82C
		// (set) Token: 0x060107DE RID: 67550 RVA: 0x003AF634 File Offset: 0x003AD834
		public IList<TimeSlot> SlotList
		{
			get
			{
				return this._slotList;
			}
			protected set
			{
				this._slotList = value;
			}
		}

		// Token: 0x17005016 RID: 20502
		// (get) Token: 0x060107DF RID: 67551 RVA: 0x003AF63D File Offset: 0x003AD83D
		// (set) Token: 0x060107E0 RID: 67552 RVA: 0x003AF645 File Offset: 0x003AD845
		private List<List<Control>> RowContents
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

		// Token: 0x17005017 RID: 20503
		// (get) Token: 0x060107E1 RID: 67553 RVA: 0x003AF64E File Offset: 0x003AD84E
		// (set) Token: 0x060107E2 RID: 67554 RVA: 0x003AF656 File Offset: 0x003AD856
		public bool RenderEmptySpace { get; set; }

		// Token: 0x060107E3 RID: 67555 RVA: 0x003AF65F File Offset: 0x003AD85F
		public RowBuilder(IList<TimeSlot> slotList, int maxColumnWidth) : this(slotList, maxColumnWidth, true)
		{
		}

		// Token: 0x060107E4 RID: 67556 RVA: 0x003AF66C File Offset: 0x003AD86C
		public RowBuilder(IList<TimeSlot> slotList, int maxColumnWidth, bool renderEmptySpace)
		{
			this.SlotList = slotList;
			this.RowCount = this.SlotList.Count;
			this.RowContents = new List<List<Control>>();
			this.Blocks = new List<DayViewBlock>();
			this.MaxColumnWidth = maxColumnWidth;
			this.RenderEmptySpace = renderEmptySpace;
			int count = this.SlotList.Count;
			foreach (TimeSlot timeSlot in this.SlotList)
			{
				List<Control> list = new List<Control>();
				this.RowContents.Add(list);
				TableCell tableCell = new TableCell();
				timeSlot.Control = tableCell;
				this.ApplyStyles(timeSlot);
				list.Add(tableCell);
				tableCell.Controls.Add(this.CreateDayViewCell(timeSlot, count--));
			}
			foreach (DayViewBlock dayViewBlock in this.Blocks)
			{
				foreach (object obj in dayViewBlock.Appointments)
				{
					AppointmentControl appointmentControl = (AppointmentControl)obj;
					appointmentControl.CalculateSize();
				}
			}
		}

		// Token: 0x060107E5 RID: 67557 RVA: 0x003AF7D8 File Offset: 0x003AD9D8
		private void ApplyStyles(TimeSlot slot)
		{
			List<string> list = new List<string>();
			if (!slot.IsWorkHour)
			{
				list.Add("rsNonWorkHour");
			}
			if (slot.DayOfWeek == DayOfWeek.Saturday)
			{
				list.Add("rsSatCol");
			}
			else if (slot.DayOfWeek == DayOfWeek.Sunday)
			{
				list.Add("rsSunCol");
			}
			slot.CssClass = (slot.Control.CssClass = string.Join(" ", list.ToArray()));
		}

		// Token: 0x060107E6 RID: 67558 RVA: 0x003AF84B File Offset: 0x003ADA4B
		public List<Control> GetRowContent(int rowIndex)
		{
			return this.RowContents[rowIndex];
		}

		// Token: 0x060107E7 RID: 67559 RVA: 0x003AF85C File Offset: 0x003ADA5C
		private Control CreateDayViewCell(SchedulerTimeSlot slot, int zIndex)
		{
			Control control = new Control();
			DayViewCellWrapper dayViewCellWrapper = new DayViewCellWrapper(zIndex, this.RenderEmptySpace);
			control.Controls.Add(dayViewCellWrapper);
			this.CreateAppointmentControls(dayViewCellWrapper, slot);
			if (slot.FormContainer != null)
			{
				dayViewCellWrapper.ZIndex = this.SlotList.Count + 2;
				dayViewCellWrapper.Controls.Add(slot.FormContainer);
			}
			return control;
		}

		// Token: 0x060107E8 RID: 67560 RVA: 0x003AF8C0 File Offset: 0x003ADAC0
		private void CreateAppointmentControls(Control container, SchedulerTimeSlot slot)
		{
			DateTime start = this.SlotList[0].Start;
			DateTime end = this.SlotList[this.SlotList.Count - 1].End;
			foreach (Appointment appointment in slot.Appointments)
			{
				if (appointment.Visible)
				{
					DayViewAppointmentControl dayViewAppointmentControl = new DayViewAppointmentControl(appointment, start, end, slot.Owner.Owner.MinutesPerRow, slot.Owner.EnableExactTimeRendering);
					this.AddToSizingBlocks(dayViewAppointmentControl);
					container.Controls.Add(dayViewAppointmentControl);
				}
			}
		}

		// Token: 0x060107E9 RID: 67561 RVA: 0x003AF97C File Offset: 0x003ADB7C
		private void AddToSizingBlocks(DayViewAppointmentControl control)
		{
			if (this.CurrentBlock == null)
			{
				this.Blocks.Add(new DayViewBlock(this.MaxColumnWidth));
			}
			else if (!this.CurrentBlock.OverlapsWith(control))
			{
				this.Blocks.Add(new DayViewBlock(this.MaxColumnWidth));
			}
			this.CurrentBlock.Add(control);
		}

		// Token: 0x040049DD RID: 18909
		private IList<TimeSlot> _slotList;

		// Token: 0x040049DE RID: 18910
		private int _rowCount;

		// Token: 0x040049DF RID: 18911
		private List<List<Control>> _rowContents;

		// Token: 0x040049E0 RID: 18912
		private List<DayViewBlock> _blocks;

		// Token: 0x040049E1 RID: 18913
		private int _maxColumnWidth;
	}
}
