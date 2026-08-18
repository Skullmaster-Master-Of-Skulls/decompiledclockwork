using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Intake
{
	// Token: 0x020005DE RID: 1502
	[DataContract(Namespace = "http://tpro.ca")]
	public class RemoveIntakeReq : BaseMessageReq
	{
		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06001EAD RID: 7853 RVA: 0x0000DF2A File Offset: 0x0000C12A
		// (set) Token: 0x06001EAE RID: 7854 RVA: 0x0000DF32 File Offset: 0x0000C132
		[DataMember]
		public string StudentNumber { get; set; }
	}
}
