using System;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.CourseRegistrations
{
	// Token: 0x0200043B RID: 1083
	public class StudentWithCourseAndAccommodationInfo
	{
		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x060020C7 RID: 8391 RVA: 0x00024DDC File Offset: 0x00022FDC
		// (set) Token: 0x060020C8 RID: 8392 RVA: 0x00024DE4 File Offset: 0x00022FE4
		public BasicPerson Student { get; set; }

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x060020C9 RID: 8393 RVA: 0x00024DED File Offset: 0x00022FED
		// (set) Token: 0x060020CA RID: 8394 RVA: 0x00024DF5 File Offset: 0x00022FF5
		public LookupCourseBase CourseBase { get; set; }

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x060020CB RID: 8395 RVA: 0x00024DFE File Offset: 0x00022FFE
		// (set) Token: 0x060020CC RID: 8396 RVA: 0x00024E06 File Offset: 0x00023006
		public DateTime? DateLetterIssued { get; set; }

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x060020CD RID: 8397 RVA: 0x00024E0F File Offset: 0x0002300F
		// (set) Token: 0x060020CE RID: 8398 RVA: 0x00024E17 File Offset: 0x00023017
		public DateTime? DateLetterReturned { get; set; }

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x060020CF RID: 8399 RVA: 0x00024E20 File Offset: 0x00023020
		// (set) Token: 0x060020D0 RID: 8400 RVA: 0x00024E28 File Offset: 0x00023028
		public bool SelfRegIsApproved { get; set; }

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x060020D1 RID: 8401 RVA: 0x00024E31 File Offset: 0x00023031
		// (set) Token: 0x060020D2 RID: 8402 RVA: 0x00024E39 File Offset: 0x00023039
		public DateTime? AccommodationExpiryDate { get; set; }

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x060020D3 RID: 8403 RVA: 0x00024E42 File Offset: 0x00023042
		// (set) Token: 0x060020D4 RID: 8404 RVA: 0x00024E4A File Offset: 0x0002304A
		public bool NoInstructorViewEnabled { get; set; }
	}
}
