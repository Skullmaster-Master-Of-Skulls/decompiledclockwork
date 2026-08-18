using System;
using System.Collections.Generic;

namespace NewBooker.Entities.AutoTestBooking.Booker2
{
	// Token: 0x020000AE RID: 174
	public class TryToBookResult
	{
		// Token: 0x06000409 RID: 1033 RVA: 0x0000D196 File Offset: 0x0000B396
		public TryToBookResult()
		{
			this.RoomIdsConsidered = new List<int>();
			this.AssetsRequiredAtSomePoint = new List<string>();
			this.NoticesForAllPotentialBookings = new List<string>();
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0000D1C4 File Offset: 0x0000B3C4
		// (set) Token: 0x0600040B RID: 1035 RVA: 0x0000D1CC File Offset: 0x0000B3CC
		public IList<TryToBookFailure> Failures { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000D1D5 File Offset: 0x0000B3D5
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x0000D1DD File Offset: 0x0000B3DD
		public IList<TryToBookWarning> Warnings { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000D1E6 File Offset: 0x0000B3E6
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x0000D1EE File Offset: 0x0000B3EE
		public IList<TryToBookPotentialBooking> PotentialBookings { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0000D1F7 File Offset: 0x0000B3F7
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x0000D1FF File Offset: 0x0000B3FF
		public IList<string> Messages { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000D208 File Offset: 0x0000B408
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x0000D210 File Offset: 0x0000B410
		public IList<string> NoticesForAllPotentialBookings { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000D219 File Offset: 0x0000B419
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x0000D221 File Offset: 0x0000B421
		public bool StudentIsDoubleBooked { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x0000D22A File Offset: 0x0000B42A
		// (set) Token: 0x06000417 RID: 1047 RVA: 0x0000D232 File Offset: 0x0000B432
		public bool StudentAlreadyHadAnotherTestBookedForSameDayAndCourse { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0000D23B File Offset: 0x0000B43B
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x0000D243 File Offset: 0x0000B443
		public IList<int> RoomIdsConsidered { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0000D24C File Offset: 0x0000B44C
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x0000D254 File Offset: 0x0000B454
		public IList<string> AssetsRequiredAtSomePoint { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000D25D File Offset: 0x0000B45D
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x0000D265 File Offset: 0x0000B465
		public IList<int> IconIdsToBookWith { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0000D26E File Offset: 0x0000B46E
		// (set) Token: 0x0600041F RID: 1055 RVA: 0x0000D276 File Offset: 0x0000B476
		public IList<int> AccommodationCidsForEmail { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x0000D27F File Offset: 0x0000B47F
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x0000D287 File Offset: 0x0000B487
		public IList<DateTime> StartDateTimesNotUseableBecauseOfTimetableConflict { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0000D290 File Offset: 0x0000B490
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x0000D298 File Offset: 0x0000B498
		public int AppliedBreakMinutes { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0000D2A1 File Offset: 0x0000B4A1
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x0000D2A9 File Offset: 0x0000B4A9
		public IList<string> DebuggingLogItems { get; set; }
	}
}
