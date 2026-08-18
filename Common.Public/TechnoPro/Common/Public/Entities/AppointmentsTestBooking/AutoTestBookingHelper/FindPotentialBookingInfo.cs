using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x0200053F RID: 1343
	[Serializable]
	public class FindPotentialBookingInfo
	{
		// Token: 0x170011EB RID: 4587
		// (get) Token: 0x06002AC2 RID: 10946 RVA: 0x0002DC10 File Offset: 0x0002BE10
		public bool DebugMode
		{
			get
			{
				return this.debugMode;
			}
		}

		// Token: 0x170011EC RID: 4588
		// (get) Token: 0x06002AC3 RID: 10947 RVA: 0x0002DC28 File Offset: 0x0002BE28
		public int Pid
		{
			get
			{
				return this.pid;
			}
		}

		// Token: 0x170011ED RID: 4589
		// (get) Token: 0x06002AC4 RID: 10948 RVA: 0x0002DC40 File Offset: 0x0002BE40
		public int Lucid
		{
			get
			{
				return this.lucid;
			}
		}

		// Token: 0x170011EE RID: 4590
		// (get) Token: 0x06002AC5 RID: 10949 RVA: 0x0002DC58 File Offset: 0x0002BE58
		public DateTime DayToLookIn
		{
			get
			{
				return this.dayToLookIn;
			}
		}

		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x06002AC6 RID: 10950 RVA: 0x0002DC70 File Offset: 0x0002BE70
		public Test ClassTest
		{
			get
			{
				return this.classTest;
			}
		}

		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x06002AC7 RID: 10951 RVA: 0x0002DC88 File Offset: 0x0002BE88
		public IList<Accommodation> Accommodations
		{
			get
			{
				return this.accommodations;
			}
		}

		// Token: 0x170011F1 RID: 4593
		// (get) Token: 0x06002AC8 RID: 10952 RVA: 0x0002DCA0 File Offset: 0x0002BEA0
		public IList<Asset> AvailableAssets
		{
			get
			{
				return this.AvailableAssets;
			}
		}

		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x06002AC9 RID: 10953 RVA: 0x0002DCB8 File Offset: 0x0002BEB8
		public IList<Room> AvailableRooms
		{
			get
			{
				return this.availableRooms;
			}
		}

		// Token: 0x170011F3 RID: 4595
		// (get) Token: 0x06002ACA RID: 10954 RVA: 0x0002DCD0 File Offset: 0x0002BED0
		public IList<SpecialAccommodation> SpecialAccommodations
		{
			get
			{
				return this.specialAccommodations;
			}
		}

		// Token: 0x06002ACB RID: 10955 RVA: 0x0002DCE8 File Offset: 0x0002BEE8
		public FindPotentialBookingInfo(bool debugMode, int pid, int lucid, DateTime dayToLookIn, Test classTest, IList<Accommodation> accommodations, IList<Asset> availableAssets, IList<Room> availableRooms, IList<SpecialAccommodation> specialAccommodations)
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

		// Token: 0x04001E76 RID: 7798
		private bool debugMode;

		// Token: 0x04001E77 RID: 7799
		private int pid;

		// Token: 0x04001E78 RID: 7800
		private int lucid;

		// Token: 0x04001E79 RID: 7801
		private DateTime dayToLookIn;

		// Token: 0x04001E7A RID: 7802
		private Test classTest;

		// Token: 0x04001E7B RID: 7803
		private IList<Accommodation> accommodations;

		// Token: 0x04001E7C RID: 7804
		private IList<Asset> availableAssets;

		// Token: 0x04001E7D RID: 7805
		private IList<Room> availableRooms;

		// Token: 0x04001E7E RID: 7806
		private IList<SpecialAccommodation> specialAccommodations;
	}
}
