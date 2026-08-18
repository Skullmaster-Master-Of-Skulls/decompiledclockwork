using System;
using System.Collections.Generic;

namespace ClockWorkWebAPI.TestBooking
{
	// Token: 0x02000032 RID: 50
	[Serializable]
	public class FindPotentialBookingInfo
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000283 RID: 643 RVA: 0x00010EF4 File Offset: 0x0000F0F4
		public bool DebugMode
		{
			get
			{
				return this.debugMode;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000284 RID: 644 RVA: 0x00010F0C File Offset: 0x0000F10C
		public int Pid
		{
			get
			{
				return this.pid;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00010F24 File Offset: 0x0000F124
		public int Lucid
		{
			get
			{
				return this.lucid;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000286 RID: 646 RVA: 0x00010F3C File Offset: 0x0000F13C
		public DateTime DayToLookIn
		{
			get
			{
				return this.dayToLookIn;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00010F54 File Offset: 0x0000F154
		public Test ClassTest
		{
			get
			{
				return this.classTest;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00010F6C File Offset: 0x0000F16C
		public List<Accommodation> Accommodations
		{
			get
			{
				return this.accommodations;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00010F84 File Offset: 0x0000F184
		public List<Asset> AvailableAssets
		{
			get
			{
				return this.AvailableAssets;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00010F9C File Offset: 0x0000F19C
		public List<Room> AvailableRooms
		{
			get
			{
				return this.availableRooms;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00010FB4 File Offset: 0x0000F1B4
		public List<SpecialAccommodation> SpecialAccommodations
		{
			get
			{
				return this.specialAccommodations;
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00010FCC File Offset: 0x0000F1CC
		public FindPotentialBookingInfo(bool debugMode, int pid, int lucid, DateTime dayToLookIn, Test classTest, List<Accommodation> accommodations, List<Asset> availableAssets, List<Room> availableRooms, List<SpecialAccommodation> specialAccommodations)
		{
			this.debugMode = debugMode;
			this.pid = pid;
			this.lucid = lucid;
			this.dayToLookIn = dayToLookIn;
			this.classTest = classTest;
			this.accommodations = accommodations;
			this.availableAssets = availableAssets;
			this.availableRooms = availableRooms;
			this.specialAccommodations = specialAccommodations;
		}

		// Token: 0x04000166 RID: 358
		private bool debugMode;

		// Token: 0x04000167 RID: 359
		private int pid;

		// Token: 0x04000168 RID: 360
		private int lucid;

		// Token: 0x04000169 RID: 361
		private DateTime dayToLookIn;

		// Token: 0x0400016A RID: 362
		private Test classTest;

		// Token: 0x0400016B RID: 363
		private List<Accommodation> accommodations;

		// Token: 0x0400016C RID: 364
		private List<Asset> availableAssets;

		// Token: 0x0400016D RID: 365
		private List<Room> availableRooms;

		// Token: 0x0400016E RID: 366
		private List<SpecialAccommodation> specialAccommodations;
	}
}
