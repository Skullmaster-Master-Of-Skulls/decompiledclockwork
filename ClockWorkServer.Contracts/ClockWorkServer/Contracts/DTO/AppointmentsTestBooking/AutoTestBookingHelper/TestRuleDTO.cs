using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000AA8 RID: 2728
	[DataContract(Namespace = "http://tpro.ca")]
	public class TestRuleDTO
	{
		// Token: 0x17001531 RID: 5425
		// (get) Token: 0x060039DD RID: 14813 RVA: 0x0001C18F File Offset: 0x0001A38F
		// (set) Token: 0x060039DE RID: 14814 RVA: 0x0001C197 File Offset: 0x0001A397
		[DataMember]
		public int OrderNum { get; set; }

		// Token: 0x17001532 RID: 5426
		// (get) Token: 0x060039DF RID: 14815 RVA: 0x0001C1A0 File Offset: 0x0001A3A0
		// (set) Token: 0x060039E0 RID: 14816 RVA: 0x0001C1A8 File Offset: 0x0001A3A8
		[DataMember]
		public bool IncludeNonVirtualRooms { get; set; }

		// Token: 0x17001533 RID: 5427
		// (get) Token: 0x060039E1 RID: 14817 RVA: 0x0001C1B1 File Offset: 0x0001A3B1
		// (set) Token: 0x060039E2 RID: 14818 RVA: 0x0001C1B9 File Offset: 0x0001A3B9
		[DataMember]
		public bool IncludeVirtualRooms { get; set; }

		// Token: 0x17001534 RID: 5428
		// (get) Token: 0x060039E3 RID: 14819 RVA: 0x0001C1C2 File Offset: 0x0001A3C2
		// (set) Token: 0x060039E4 RID: 14820 RVA: 0x0001C1CA File Offset: 0x0001A3CA
		[DataMember]
		public int MinutesPre { get; set; }

		// Token: 0x17001535 RID: 5429
		// (get) Token: 0x060039E5 RID: 14821 RVA: 0x0001C1D3 File Offset: 0x0001A3D3
		// (set) Token: 0x060039E6 RID: 14822 RVA: 0x0001C1DB File Offset: 0x0001A3DB
		[DataMember]
		public int MinutesPost { get; set; }

		// Token: 0x17001536 RID: 5430
		// (get) Token: 0x060039E7 RID: 14823 RVA: 0x0001C1E4 File Offset: 0x0001A3E4
		// (set) Token: 0x060039E8 RID: 14824 RVA: 0x0001C1EC File Offset: 0x0001A3EC
		[DataMember]
		public List<int> RoomIdsToExclud { get; set; }

		// Token: 0x17001537 RID: 5431
		// (get) Token: 0x060039E9 RID: 14825 RVA: 0x0001C1F5 File Offset: 0x0001A3F5
		// (set) Token: 0x060039EA RID: 14826 RVA: 0x0001C1FD File Offset: 0x0001A3FD
		[DataMember]
		public bool ShiftTimeToMatchEndOfDay { get; set; }

		// Token: 0x17001538 RID: 5432
		// (get) Token: 0x060039EB RID: 14827 RVA: 0x0001C206 File Offset: 0x0001A406
		// (set) Token: 0x060039EC RID: 14828 RVA: 0x0001C20E File Offset: 0x0001A40E
		[DataMember]
		public bool ShiftTimeToMatchStartOfDay { get; set; }

		// Token: 0x17001539 RID: 5433
		// (get) Token: 0x060039ED RID: 14829 RVA: 0x0001C217 File Offset: 0x0001A417
		// (set) Token: 0x060039EE RID: 14830 RVA: 0x0001C21F File Offset: 0x0001A41F
		[DataMember]
		public bool EnforceOverlapWithClassTime { get; set; }

		// Token: 0x1700153A RID: 5434
		// (get) Token: 0x060039EF RID: 14831 RVA: 0x0001C228 File Offset: 0x0001A428
		// (set) Token: 0x060039F0 RID: 14832 RVA: 0x0001C230 File Offset: 0x0001A430
		[DataMember]
		public bool StopLookingIfFoundAtLeastOne { get; set; }

		// Token: 0x1700153B RID: 5435
		// (get) Token: 0x060039F1 RID: 14833 RVA: 0x0001C239 File Offset: 0x0001A439
		// (set) Token: 0x060039F2 RID: 14834 RVA: 0x0001C241 File Offset: 0x0001A441
		[DataMember]
		public bool ShiftTimeAroundTimetable { get; set; }

		// Token: 0x1700153C RID: 5436
		// (get) Token: 0x060039F3 RID: 14835 RVA: 0x0001C24A File Offset: 0x0001A44A
		// (set) Token: 0x060039F4 RID: 14836 RVA: 0x0001C252 File Offset: 0x0001A452
		[DataMember]
		public bool IgnoreAssetRules { get; set; }

		// Token: 0x1700153D RID: 5437
		// (get) Token: 0x060039F5 RID: 14837 RVA: 0x0001C25B File Offset: 0x0001A45B
		// (set) Token: 0x060039F6 RID: 14838 RVA: 0x0001C263 File Offset: 0x0001A463
		[DataMember]
		public int EnforceOverlapWithClassTime_firstXMinutes { get; set; }
	}
}
