using System;

namespace TechnoPro.Common.Public.Entities.ClockWorkServerConnection
{
	// Token: 0x02000450 RID: 1104
	public class CertificateInfo
	{
		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x06002175 RID: 8565 RVA: 0x000256D9 File Offset: 0x000238D9
		// (set) Token: 0x06002176 RID: 8566 RVA: 0x000256E1 File Offset: 0x000238E1
		public string CertificatePublicKey { get; set; }

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x06002177 RID: 8567 RVA: 0x000256EA File Offset: 0x000238EA
		// (set) Token: 0x06002178 RID: 8568 RVA: 0x000256F2 File Offset: 0x000238F2
		public string SubjectName { get; set; }

		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x06002179 RID: 8569 RVA: 0x000256FB File Offset: 0x000238FB
		// (set) Token: 0x0600217A RID: 8570 RVA: 0x00025703 File Offset: 0x00023903
		public string Thumbprint { get; set; }

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x0600217B RID: 8571 RVA: 0x0002570C File Offset: 0x0002390C
		// (set) Token: 0x0600217C RID: 8572 RVA: 0x00025714 File Offset: 0x00023914
		public string IdentityDNS { get; set; }
	}
}
