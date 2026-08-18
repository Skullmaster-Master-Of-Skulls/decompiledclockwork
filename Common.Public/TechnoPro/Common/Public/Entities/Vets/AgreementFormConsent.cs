using System;

namespace TechnoPro.Common.Public.Entities.Vets
{
	// Token: 0x020000FA RID: 250
	public class AgreementFormConsent
	{
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0000ECE6 File Offset: 0x0000CEE6
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x0000ECEE File Offset: 0x0000CEEE
		public DateTime DateConsentedTo { get; set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0000ECF7 File Offset: 0x0000CEF7
		// (set) Token: 0x060005D2 RID: 1490 RVA: 0x0000ECFF File Offset: 0x0000CEFF
		public int StudentWhoConsentedPersonId { get; set; }
	}
}
