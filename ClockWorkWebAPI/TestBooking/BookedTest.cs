using System;
using System.Collections.Generic;
using System.Data;

namespace ClockWorkWebAPI.TestBooking
{
	// Token: 0x0200002E RID: 46
	[Serializable]
	public class BookedTest
	{
		// Token: 0x06000255 RID: 597 RVA: 0x00010258 File Offset: 0x0000E458
		public BookedTest(int pid, int rid, DateTime startDate, DateTime endDate, DateTime classStartDate, DateTime classEndDate, int appTypeId, int lucid, List<Accommodation> accommodationsToUse)
		{
			this.accommodationsToUse = accommodationsToUse;
			this.pid = pid;
			this.rid = rid;
			this.lucid = lucid;
			this.appTypeId = appTypeId;
			this.startDateTime = startDate;
			this.endDateTime = endDate;
			this.classStartDate = classStartDate;
			this.classEndDate = classEndDate;
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000256 RID: 598 RVA: 0x000102B4 File Offset: 0x0000E4B4
		// (set) Token: 0x06000257 RID: 599 RVA: 0x000102CC File Offset: 0x0000E4CC
		public DataTable DynamicData
		{
			get
			{
				return this.dynamicData;
			}
			set
			{
				this.dynamicData = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000258 RID: 600 RVA: 0x000102D8 File Offset: 0x0000E4D8
		public DateTime StartDateTime
		{
			get
			{
				return this.startDateTime;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000259 RID: 601 RVA: 0x000102F0 File Offset: 0x0000E4F0
		public DateTime EndDateTime
		{
			get
			{
				return this.endDateTime;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00010308 File Offset: 0x0000E508
		public DateTime ClassStartDate
		{
			get
			{
				return this.classStartDate;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00010320 File Offset: 0x0000E520
		public DateTime ClassEndDate
		{
			get
			{
				return this.classEndDate;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600025C RID: 604 RVA: 0x00010338 File Offset: 0x0000E538
		public int Pid
		{
			get
			{
				return this.pid;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00010350 File Offset: 0x0000E550
		public int Lucid
		{
			get
			{
				return this.lucid;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00010368 File Offset: 0x0000E568
		public int AppTypeId
		{
			get
			{
				return this.appTypeId;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00010380 File Offset: 0x0000E580
		public int Rid
		{
			get
			{
				return this.rid;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00010398 File Offset: 0x0000E598
		public List<Accommodation> AccommodationsToUse
		{
			get
			{
				return this.accommodationsToUse;
			}
		}

		// Token: 0x0400014D RID: 333
		private DateTime startDateTime;

		// Token: 0x0400014E RID: 334
		private DateTime endDateTime;

		// Token: 0x0400014F RID: 335
		private DateTime classStartDate;

		// Token: 0x04000150 RID: 336
		private DateTime classEndDate;

		// Token: 0x04000151 RID: 337
		private int pid;

		// Token: 0x04000152 RID: 338
		private int lucid;

		// Token: 0x04000153 RID: 339
		private int rid;

		// Token: 0x04000154 RID: 340
		private int appTypeId;

		// Token: 0x04000155 RID: 341
		private List<Accommodation> accommodationsToUse;

		// Token: 0x04000156 RID: 342
		private DataTable dynamicData;
	}
}
