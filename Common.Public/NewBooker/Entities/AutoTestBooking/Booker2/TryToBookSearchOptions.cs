using System;
using System.Collections.Generic;

namespace NewBooker.Entities.AutoTestBooking.Booker2
{
	// Token: 0x020000B2 RID: 178
	public class TryToBookSearchOptions
	{
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0000D439 File Offset: 0x0000B639
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x0000D441 File Offset: 0x0000B641
		public IList<TryToBookRule> Rules { get; set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0000D44A File Offset: 0x0000B64A
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x0000D452 File Offset: 0x0000B652
		public int MaxNumberOfPotentialTestsToReturn { get; set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x0000D45B File Offset: 0x0000B65B
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x0000D463 File Offset: 0x0000B663
		public bool AllowStudentsToBookSameCourseSameDay { get; set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0000D46C File Offset: 0x0000B66C
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x0000D474 File Offset: 0x0000B674
		public bool AllowToBookWithoutAnyAccommodations { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0000D47D File Offset: 0x0000B67D
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x0000D485 File Offset: 0x0000B685
		public int MaxNumberOfDaysAfterClass { get; set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0000D48E File Offset: 0x0000B68E
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x0000D496 File Offset: 0x0000B696
		public int MaxNumberOfDaysBeforeClass { get; set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000D49F File Offset: 0x0000B69F
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x0000D4A7 File Offset: 0x0000B6A7
		public bool AllowStudentsToBeDoubleBooked { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0000D4B0 File Offset: 0x0000B6B0
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x0000D4B8 File Offset: 0x0000B6B8
		public bool MatchUpTimetable { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0000D4C1 File Offset: 0x0000B6C1
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x0000D4C9 File Offset: 0x0000B6C9
		public IDictionary<int, IList<int>> RoomAvailabilityScheduleMappings { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0000D4D2 File Offset: 0x0000B6D2
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x0000D4DA File Offset: 0x0000B6DA
		public int BufferMinutesPre { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0000D4E3 File Offset: 0x0000B6E3
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x0000D4EB File Offset: 0x0000B6EB
		public int BufferMinutesPost { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0000D4F4 File Offset: 0x0000B6F4
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x0000D4FC File Offset: 0x0000B6FC
		public bool RestrictRoomByCampusEnabled { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0000D505 File Offset: 0x0000B705
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x0000D50D File Offset: 0x0000B70D
		public bool IgnoreSpecialAccommodations { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0000D516 File Offset: 0x0000B716
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x0000D51E File Offset: 0x0000B71E
		public int BookingAlreadyExistsAppointmentId { get; set; }
	}
}
