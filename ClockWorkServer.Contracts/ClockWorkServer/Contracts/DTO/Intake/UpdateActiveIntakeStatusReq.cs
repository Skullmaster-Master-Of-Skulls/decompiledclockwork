using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Intake
{
	// Token: 0x020005DA RID: 1498
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateActiveIntakeStatusReq : BaseMessageReq
	{
		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06001EA1 RID: 7841 RVA: 0x0000DEE6 File Offset: 0x0000C0E6
		// (set) Token: 0x06001EA2 RID: 7842 RVA: 0x0000DEEE File Offset: 0x0000C0EE
		[DataMember]
		public int[] IntakePersonIds { get; set; }

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06001EA3 RID: 7843 RVA: 0x0000DEF7 File Offset: 0x0000C0F7
		// (set) Token: 0x06001EA4 RID: 7844 RVA: 0x0000DEFF File Offset: 0x0000C0FF
		[DataMember]
		public Guid NewIntakeStatusId { get; set; }
	}
}
