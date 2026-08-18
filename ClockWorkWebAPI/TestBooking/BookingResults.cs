using System;
using System.Collections.Generic;

namespace ClockWorkWebAPI.TestBooking
{
	// Token: 0x02000030 RID: 48
	[Serializable]
	public class BookingResults
	{
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600026A RID: 618 RVA: 0x000104D6 File Offset: 0x0000E6D6
		// (set) Token: 0x0600026B RID: 619 RVA: 0x000104DE File Offset: 0x0000E6DE
		public bool? NoRoomAvailability { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600026C RID: 620 RVA: 0x000104E7 File Offset: 0x0000E6E7
		// (set) Token: 0x0600026D RID: 621 RVA: 0x000104EF File Offset: 0x0000E6EF
		public bool? OnlyVirtualRoomsToLookAt { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600026E RID: 622 RVA: 0x000104F8 File Offset: 0x0000E6F8
		// (set) Token: 0x0600026F RID: 623 RVA: 0x00010500 File Offset: 0x0000E700
		public bool? FailedTimetableCheck { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000270 RID: 624 RVA: 0x00010509 File Offset: 0x0000E709
		// (set) Token: 0x06000271 RID: 625 RVA: 0x00010511 File Offset: 0x0000E711
		public bool? StudentIsDoubleBooked { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0001051A File Offset: 0x0000E71A
		// (set) Token: 0x06000273 RID: 627 RVA: 0x00010522 File Offset: 0x0000E722
		public bool? RoomIsDoubleBooked { get; set; }

		// Token: 0x06000274 RID: 628 RVA: 0x0001052C File Offset: 0x0000E72C
		public BookingResults()
		{
			this.NoRoomAvailability = null;
			this.OnlyVirtualRoomsToLookAt = null;
			this.FailedTimetableCheck = null;
			this.StudentIsDoubleBooked = null;
			this.RoomIsDoubleBooked = null;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00010594 File Offset: 0x0000E794
		public override string ToString()
		{
			List<string> list = new List<string>();
			string text = this.ToString(this.NoRoomAvailability, "Couldn't find any room availability to fit");
			bool flag = !string.IsNullOrEmpty(text);
			if (flag)
			{
				list.Add(text);
			}
			text = this.ToString(this.FailedTimetableCheck, "Couldn't find a time that doesn't conflict with the student's timetable");
			bool flag2 = !string.IsNullOrEmpty(text);
			if (flag2)
			{
				list.Add(text);
			}
			text = this.ToString(this.StudentIsDoubleBooked, "Couldn't find a time where the student is not double-booked with something else");
			bool flag3 = !string.IsNullOrEmpty(text);
			if (flag3)
			{
				list.Add(text);
			}
			text = this.ToString(this.RoomIsDoubleBooked, "Couldn't find a time where any appropriate room is not double-booked with something else");
			bool flag4 = !string.IsNullOrEmpty(text);
			if (flag4)
			{
				list.Add(text);
			}
			return string.Join("\n", list.ToArray());
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00010660 File Offset: 0x0000E860
		private string ToString(bool? val, string title)
		{
			bool flag = val == null;
			string result;
			if (flag)
			{
				result = string.Format("{0}: Un-determined", title);
			}
			else
			{
				bool value = val.Value;
				if (value)
				{
					result = string.Format("{0}: True", title);
				}
				else
				{
					result = "";
				}
			}
			return result;
		}
	}
}
