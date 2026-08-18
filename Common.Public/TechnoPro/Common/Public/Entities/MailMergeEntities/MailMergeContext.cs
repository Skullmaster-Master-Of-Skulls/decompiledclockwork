using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities
{
	// Token: 0x020002C0 RID: 704
	public class MailMergeContext
	{
		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06001542 RID: 5442 RVA: 0x0001ABD4 File Offset: 0x00018DD4
		// (set) Token: 0x06001543 RID: 5443 RVA: 0x0001ABDC File Offset: 0x00018DDC
		public int PersonId { get; set; }

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06001544 RID: 5444 RVA: 0x0001ABE5 File Offset: 0x00018DE5
		// (set) Token: 0x06001545 RID: 5445 RVA: 0x0001ABED File Offset: 0x00018DED
		public int AppointmentId { get; set; }

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06001546 RID: 5446 RVA: 0x0001ABF6 File Offset: 0x00018DF6
		// (set) Token: 0x06001547 RID: 5447 RVA: 0x0001ABFE File Offset: 0x00018DFE
		public int LuCourseId { get; set; }

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06001548 RID: 5448 RVA: 0x0001AC07 File Offset: 0x00018E07
		// (set) Token: 0x06001549 RID: 5449 RVA: 0x0001AC0F File Offset: 0x00018E0F
		public List<int> LuCourseIds { get; set; }

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x0001AC18 File Offset: 0x00018E18
		// (set) Token: 0x0600154B RID: 5451 RVA: 0x0001AC20 File Offset: 0x00018E20
		public int InstructorId { get; set; }

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x0600154C RID: 5452 RVA: 0x0001AC29 File Offset: 0x00018E29
		// (set) Token: 0x0600154D RID: 5453 RVA: 0x0001AC31 File Offset: 0x00018E31
		public int CaseId { get; set; }

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x0001AC3A File Offset: 0x00018E3A
		// (set) Token: 0x0600154F RID: 5455 RVA: 0x0001AC42 File Offset: 0x00018E42
		public int PerDateId { get; set; }

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x0001AC4B File Offset: 0x00018E4B
		// (set) Token: 0x06001551 RID: 5457 RVA: 0x0001AC53 File Offset: 0x00018E53
		public int WhoAmId { get; set; }

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x0001AC5C File Offset: 0x00018E5C
		// (set) Token: 0x06001553 RID: 5459 RVA: 0x0001AC64 File Offset: 0x00018E64
		public int? CourseId { get; set; }

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x0001AC6D File Offset: 0x00018E6D
		// (set) Token: 0x06001555 RID: 5461 RVA: 0x0001AC75 File Offset: 0x00018E75
		public int ExamId { get; set; }

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x0001AC7E File Offset: 0x00018E7E
		// (set) Token: 0x06001557 RID: 5463 RVA: 0x0001AC86 File Offset: 0x00018E86
		public int ServiceProviderId { get; set; }

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x0001AC8F File Offset: 0x00018E8F
		// (set) Token: 0x06001559 RID: 5465 RVA: 0x0001AC97 File Offset: 0x00018E97
		public string WebSettingContext { get; set; }

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x0001ACA0 File Offset: 0x00018EA0
		// (set) Token: 0x0600155B RID: 5467 RVA: 0x0001ACA8 File Offset: 0x00018EA8
		public string DefaultDateFormat { get; set; }

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x0001ACB1 File Offset: 0x00018EB1
		// (set) Token: 0x0600155D RID: 5469 RVA: 0x0001ACB9 File Offset: 0x00018EB9
		public string DefaultTimeFormat { get; set; }

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x0001ACC2 File Offset: 0x00018EC2
		// (set) Token: 0x0600155F RID: 5471 RVA: 0x0001ACCA File Offset: 0x00018ECA
		public string ProductUniqueId { get; set; }

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06001560 RID: 5472 RVA: 0x0001ACD3 File Offset: 0x00018ED3
		// (set) Token: 0x06001561 RID: 5473 RVA: 0x0001ACDB File Offset: 0x00018EDB
		public int CatalogId { get; set; }

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x0001ACE4 File Offset: 0x00018EE4
		// (set) Token: 0x06001563 RID: 5475 RVA: 0x0001ACEC File Offset: 0x00018EEC
		public int LoanId { get; set; }

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x0001ACF5 File Offset: 0x00018EF5
		// (set) Token: 0x06001565 RID: 5477 RVA: 0x0001ACFD File Offset: 0x00018EFD
		public int AlternateFormatRequestId { get; set; }

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x0001AD06 File Offset: 0x00018F06
		// (set) Token: 0x06001567 RID: 5479 RVA: 0x0001AD0E File Offset: 0x00018F0E
		public int AlternateFormatPublisherId { get; set; }

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x0001AD17 File Offset: 0x00018F17
		// (set) Token: 0x06001569 RID: 5481 RVA: 0x0001AD1F File Offset: 0x00018F1F
		public int AlternateFormatVendorId { get; set; }

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x0600156A RID: 5482 RVA: 0x0001AD28 File Offset: 0x00018F28
		// (set) Token: 0x0600156B RID: 5483 RVA: 0x0001AD30 File Offset: 0x00018F30
		public int AltPersonId { get; set; }

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x0600156C RID: 5484 RVA: 0x0001AD39 File Offset: 0x00018F39
		// (set) Token: 0x0600156D RID: 5485 RVA: 0x0001AD41 File Offset: 0x00018F41
		public Guid AlternateFormatMediaContentId { get; set; }

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x0600156E RID: 5486 RVA: 0x0001AD4A File Offset: 0x00018F4A
		// (set) Token: 0x0600156F RID: 5487 RVA: 0x0001AD52 File Offset: 0x00018F52
		public int PeopleOnlineFormId { get; set; }
	}
}
