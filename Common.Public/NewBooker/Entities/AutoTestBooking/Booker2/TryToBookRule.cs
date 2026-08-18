using System;
using System.Collections.Generic;

namespace NewBooker.Entities.AutoTestBooking.Booker2
{
	// Token: 0x020000B0 RID: 176
	public class TryToBookRule
	{
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0000D318 File Offset: 0x0000B518
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x0000D320 File Offset: 0x0000B520
		public eTryToBookRuleRoomUsage RoomUsage { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0000D329 File Offset: 0x0000B529
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x0000D331 File Offset: 0x0000B531
		public int AllowedMinutesBefore { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000D33A File Offset: 0x0000B53A
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x0000D342 File Offset: 0x0000B542
		public int AllowedMinutesAfter { get; set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000D34B File Offset: 0x0000B54B
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x0000D353 File Offset: 0x0000B553
		public bool StopLookingIfFoundAtLeastOne { get; set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x0000D35C File Offset: 0x0000B55C
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x0000D364 File Offset: 0x0000B564
		public bool ShiftTimeToMatchStartOfDay { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0000D36D File Offset: 0x0000B56D
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x0000D375 File Offset: 0x0000B575
		public bool ShiftTimeToMatchEndOfDay { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0000D37E File Offset: 0x0000B57E
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x0000D386 File Offset: 0x0000B586
		public bool EnforceOverlapWithClassTime { get; set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0000D38F File Offset: 0x0000B58F
		// (set) Token: 0x06000442 RID: 1090 RVA: 0x0000D397 File Offset: 0x0000B597
		public int? OnlyOverlapFirstXMinutesOfClassTest { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0000D3A0 File Offset: 0x0000B5A0
		// (set) Token: 0x06000444 RID: 1092 RVA: 0x0000D3A8 File Offset: 0x0000B5A8
		public bool AllowShiftingTimeToWorkAroundTimetableForOtherCourses { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x0000D3B1 File Offset: 0x0000B5B1
		// (set) Token: 0x06000446 RID: 1094 RVA: 0x0000D3B9 File Offset: 0x0000B5B9
		public int TimetableShiftMaxNumMinutesBeforeClassTime { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x0000D3C2 File Offset: 0x0000B5C2
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x0000D3CA File Offset: 0x0000B5CA
		public int TimetableShiftMaxNumMinutesAfterClassTime { get; set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0000D3D3 File Offset: 0x0000B5D3
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x0000D3DB File Offset: 0x0000B5DB
		public bool IgnoreAssetRules { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0000D3E4 File Offset: 0x0000B5E4
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x0000D3EC File Offset: 0x0000B5EC
		public IList<int> RoomsToExclude { get; set; }
	}
}
