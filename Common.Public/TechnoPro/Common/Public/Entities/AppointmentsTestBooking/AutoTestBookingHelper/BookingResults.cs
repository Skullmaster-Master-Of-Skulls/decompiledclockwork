using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x0200053A RID: 1338
	[Serializable]
	public class BookingResults
	{
		// Token: 0x170011DF RID: 4575
		// (get) Token: 0x06002A96 RID: 10902 RVA: 0x0002CF42 File Offset: 0x0002B142
		// (set) Token: 0x06002A97 RID: 10903 RVA: 0x0002CF4A File Offset: 0x0002B14A
		public bool? NoRoomAvailability { get; set; }

		// Token: 0x170011E0 RID: 4576
		// (get) Token: 0x06002A98 RID: 10904 RVA: 0x0002CF53 File Offset: 0x0002B153
		// (set) Token: 0x06002A99 RID: 10905 RVA: 0x0002CF5B File Offset: 0x0002B15B
		public bool? OnlyVirtualRoomsToLookAt { get; set; }

		// Token: 0x170011E1 RID: 4577
		// (get) Token: 0x06002A9A RID: 10906 RVA: 0x0002CF64 File Offset: 0x0002B164
		// (set) Token: 0x06002A9B RID: 10907 RVA: 0x0002CF6C File Offset: 0x0002B16C
		public bool? FailedTimetableCheck { get; set; }

		// Token: 0x170011E2 RID: 4578
		// (get) Token: 0x06002A9C RID: 10908 RVA: 0x0002CF75 File Offset: 0x0002B175
		// (set) Token: 0x06002A9D RID: 10909 RVA: 0x0002CF7D File Offset: 0x0002B17D
		public bool? StudentIsDoubleBooked { get; set; }

		// Token: 0x170011E3 RID: 4579
		// (get) Token: 0x06002A9E RID: 10910 RVA: 0x0002CF86 File Offset: 0x0002B186
		// (set) Token: 0x06002A9F RID: 10911 RVA: 0x0002CF8E File Offset: 0x0002B18E
		public bool? RoomIsDoubleBooked { get; set; }

		// Token: 0x06002AA0 RID: 10912 RVA: 0x0002CF98 File Offset: 0x0002B198
		public BookingResults()
		{
			this.NoRoomAvailability = null;
			this.OnlyVirtualRoomsToLookAt = null;
			this.FailedTimetableCheck = null;
			this.StudentIsDoubleBooked = null;
			this.RoomIsDoubleBooked = null;
		}

		// Token: 0x06002AA1 RID: 10913 RVA: 0x0002D000 File Offset: 0x0002B200
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

		// Token: 0x06002AA2 RID: 10914 RVA: 0x0002D0CC File Offset: 0x0002B2CC
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
