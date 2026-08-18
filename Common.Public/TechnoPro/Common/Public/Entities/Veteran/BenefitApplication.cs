using System;
using TechnoPro.Common.Public.Entities.Academic;

namespace TechnoPro.Common.Public.Entities.Veteran
{
	// Token: 0x0200010E RID: 270
	public class BenefitApplication
	{
		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x0000F2FB File Offset: 0x0000D4FB
		// (set) Token: 0x06000659 RID: 1625 RVA: 0x0000F303 File Offset: 0x0000D503
		public int StudentPersonId { get; set; }

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x0000F30C File Offset: 0x0000D50C
		// (set) Token: 0x0600065B RID: 1627 RVA: 0x0000F314 File Offset: 0x0000D514
		public Semester Semester { get; set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x0000F31D File Offset: 0x0000D51D
		// (set) Token: 0x0600065D RID: 1629 RVA: 0x0000F325 File Offset: 0x0000D525
		public VeteranChapter Chapter { get; set; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x0000F32E File Offset: 0x0000D52E
		// (set) Token: 0x0600065F RID: 1631 RVA: 0x0000F336 File Offset: 0x0000D536
		public eVeteranRequestStatus CounselorStatus { get; set; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x0000F33F File Offset: 0x0000D53F
		// (set) Token: 0x06000661 RID: 1633 RVA: 0x0000F347 File Offset: 0x0000D547
		public eVeteranRequestStatus AdministratorStatus { get; set; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x0000F350 File Offset: 0x0000D550
		// (set) Token: 0x06000663 RID: 1635 RVA: 0x0000F358 File Offset: 0x0000D558
		public bool RegistrationComplete { get; set; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x0000F361 File Offset: 0x0000D561
		// (set) Token: 0x06000665 RID: 1637 RVA: 0x0000F369 File Offset: 0x0000D569
		public bool ConsentAgreementComplete { get; set; }
	}
}
