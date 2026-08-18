using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BFB RID: 3067
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaVolunteersAssignedToMediaJobReq : BaseMessageReq
	{
		// Token: 0x170017D5 RID: 6101
		// (get) Token: 0x06004097 RID: 16535 RVA: 0x0001FB0F File Offset: 0x0001DD0F
		// (set) Token: 0x06004098 RID: 16536 RVA: 0x0001FB17 File Offset: 0x0001DD17
		[DataMember]
		public int MediaJobId { get; set; }
	}
}
