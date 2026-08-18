using System;
using System.Collections.Generic;

namespace ClockWorkWebAPI.TestBooking
{
	// Token: 0x02000034 RID: 52
	[Serializable]
	public class PotentialRoom
	{
		// Token: 0x0600029A RID: 666 RVA: 0x000110EE File Offset: 0x0000F2EE
		public PotentialRoom(Room room, int score)
		{
			this.room = room;
			this.score = score;
			this.availabilityStartTimeForTheDay = DateTime.MinValue;
			this.availabilityEndTimeForTheDay = DateTime.MinValue;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0001111C File Offset: 0x0000F31C
		// (set) Token: 0x0600029C RID: 668 RVA: 0x00011134 File Offset: 0x0000F334
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

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00011140 File Offset: 0x0000F340
		// (set) Token: 0x0600029E RID: 670 RVA: 0x00011158 File Offset: 0x0000F358
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

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00011164 File Offset: 0x0000F364
		public Room Room
		{
			get
			{
				return this.room;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0001117C File Offset: 0x0000F37C
		public int Score
		{
			get
			{
				return this.score;
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00011194 File Offset: 0x0000F394
		public static List<int> GetRoomPids(List<PotentialRoom> rooms)
		{
			List<int> list = new List<int>(rooms.Count);
			foreach (PotentialRoom potentialRoom in rooms)
			{
				list.Add(potentialRoom.Room.RoomId);
			}
			return list;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00011204 File Offset: 0x0000F404
		public bool IsAvailableByStartAndEndofDayAvailabilityTimes(DateTime sd, DateTime ed)
		{
			return this.availabilityStartTimeForTheDay != DateTime.MinValue && this.availabilityEndTimeForTheDay != DateTime.MinValue && this.availabilityStartTimeForTheDay <= sd && this.availabilityEndTimeForTheDay >= ed;
		}

		// Token: 0x04000175 RID: 373
		private Room room;

		// Token: 0x04000176 RID: 374
		private int score;

		// Token: 0x04000177 RID: 375
		private DateTime availabilityStartTimeForTheDay;

		// Token: 0x04000178 RID: 376
		private DateTime availabilityEndTimeForTheDay;
	}
}
