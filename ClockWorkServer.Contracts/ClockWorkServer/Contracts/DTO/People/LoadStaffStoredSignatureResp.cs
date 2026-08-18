using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003A7 RID: 935
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStaffStoredSignatureResp
	{
		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x00009D63 File Offset: 0x00007F63
		// (set) Token: 0x060014F1 RID: 5361 RVA: 0x00009D6B File Offset: 0x00007F6B
		[DataMember]
		public byte[] SignatureBytes { get; set; }
	}
}
