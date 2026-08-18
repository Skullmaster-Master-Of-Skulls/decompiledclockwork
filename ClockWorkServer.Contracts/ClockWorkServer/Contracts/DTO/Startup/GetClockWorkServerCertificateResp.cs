using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Startup
{
	// Token: 0x02000268 RID: 616
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetClockWorkServerCertificateResp
	{
		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x00006A84 File Offset: 0x00004C84
		// (set) Token: 0x06000E2B RID: 3627 RVA: 0x00006A8C File Offset: 0x00004C8C
		[DataMember]
		public string CertificatePublicKey { get; set; }

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x00006A95 File Offset: 0x00004C95
		// (set) Token: 0x06000E2D RID: 3629 RVA: 0x00006A9D File Offset: 0x00004C9D
		[DataMember]
		public string IdentityDNS { get; set; }

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000E2E RID: 3630 RVA: 0x00006AA6 File Offset: 0x00004CA6
		// (set) Token: 0x06000E2F RID: 3631 RVA: 0x00006AAE File Offset: 0x00004CAE
		[DataMember]
		public string Thumbprint { get; set; }
	}
}
