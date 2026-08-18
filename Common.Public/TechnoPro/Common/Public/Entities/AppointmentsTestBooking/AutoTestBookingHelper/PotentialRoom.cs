using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000542 RID: 1346
	[Serializable]
	public class PotentialRoom
	{
		// Token: 0x06002B13 RID: 11027 RVA: 0x0002E2BA File Offset: 0x0002C4BA
		public PotentialRoom(Room room, int score)
		{
			this.room = room;
			this.score = score;
			this.availabilityStartTimeForTheDay = DateTime.MinValue;
			this.availabilityEndTimeForTheDay = DateTime.MinValue;
		}

		// Token: 0x17001215 RID: 4629
		// (get) Token: 0x06002B14 RID: 11028 RVA: 0x0002E2E8 File Offset: 0x0002C4E8
		// (set) Token: 0x06002B15 RID: 11029 RVA: 0x0002E300 File Offset: 0x0002C500
		public DateTime AvailabilityStartTimeForTheDay
		{
			get
			{
				return this.availabilityStartTimeForTheDay;
			}
			set
			{
				this.availabilityStartTimeForTheDay = value;
			}
		}

		// Token: 0x17001216 RID: 4630
		// (get) Token: 0x06002B16 RID: 11030 RVA: 0x0002E30C File Offset: 0x0002C50C
		// (set) Token: 0x06002B17 RID: 11031 RVA: 0x0002E324 File Offset: 0x0002C524
		public DateTime AvailabilityEndTimeForTheDay
		{
			get
			{
				return this.availabilityEndTimeForTheDay;
			}
			set
			{
				this.availabilityEndTimeForTheDay = value;
			}
		}

		// Token: 0x17001217 RID: 4631
		// (get) Token: 0x06002B18 RID: 11032 RVA: 0x0002E330 File Offset: 0x0002C530
		public Room Room
		{
			get
			{
				return this.room;
			}
		}

		// Token: 0x17001218 RID: 4632
		// (get) Token: 0x06002B19 RID: 11033 RVA: 0x0002E348 File Offset: 0x0002C548
		public int Score
		{
			get
			{
				return this.score;
			}
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x0002E360 File Offset: 0x0002C560
		public static IList<int> GetRoomPids(IList<PotentialRoom> rooms)
		{
			IList<int> list = new List<int>(rooms.Count);
			foreach (PotentialRoom potentialRoom in rooms)
			{
				list.Add(potentialRoom.Room.RoomId);
			}
			return list;
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x0002E3CC File Offset: 0x0002C5CC
		public bool IsAvailableByStartAndEndofDayAvailabilityTimes(DateTime sd, DateTime ed)
		{
			return this.availabilityStartTimeForTheDay != DateTime.MinValue && this.availabilityEndTimeForTheDay != DateTime.MinValue && this.availabilityStartTimeForTheDay <= sd && this.availabilityEndTimeForTheDay >= ed;
		}

		// Token: 0x04001EA0 RID: 7840
		private Room room;

		// Token: 0x04001EA1 RID: 7841
		private int score;

		// Token: 0x04001EA2 RID: 7842
		private DateTime availabilityStartTimeForTheDay;

		// Token: 0x04001EA3 RID: 7843
		private DateTime availabilityEndTimeForTheDay;
	}
}
