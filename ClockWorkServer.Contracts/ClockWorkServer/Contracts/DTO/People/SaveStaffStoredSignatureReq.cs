using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003AA RID: 938
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveStaffStoredSignatureReq : BaseMessageReq
	{
		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x00009D85 File Offset: 0x00007F85
		// (set) Token: 0x060014F8 RID: 5368 RVA: 0x00009D8D File Offset: 0x00007F8D
		[DataMember]
		public int StaffPersonId { get; set; }

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x00009D96 File Offset: 0x00007F96
		// (set) Token: 0x060014FA RID: 5370 RVA: 0x00009D9E File Offset: 0x00007F9E
		[DataMember]
		public byte[] SignatureBytes { get; set; }
	}
}
