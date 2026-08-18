using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerConnection
{
	// Token: 0x02000880 RID: 2176
	[DataContract(Namespace = "http://tpro.ca")]
	public class CertificateInfoDTO
	{
		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x06002C16 RID: 11286 RVA: 0x00014DF9 File Offset: 0x00012FF9
		// (set) Token: 0x06002C17 RID: 11287 RVA: 0x00014E01 File Offset: 0x00013001
		[DataMember]
		public string CertificatePublicKey { get; set; }

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06002C18 RID: 11288 RVA: 0x00014E0A File Offset: 0x0001300A
		// (set) Token: 0x06002C19 RID: 11289 RVA: 0x00014E12 File Offset: 0x00013012
		[DataMember]
		public string SubjectName { get; set; }

		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06002C1A RID: 11290 RVA: 0x00014E1B File Offset: 0x0001301B
		// (set) Token: 0x06002C1B RID: 11291 RVA: 0x00014E23 File Offset: 0x00013023
		[DataMember]
		public string Thumbprint { get; set; }

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x06002C1C RID: 11292 RVA: 0x00014E2C File Offset: 0x0001302C
		// (set) Token: 0x06002C1D RID: 11293 RVA: 0x00014E34 File Offset: 0x00013034
		[DataMember]
		public string IdentityDNS { get; set; }
	}
}
