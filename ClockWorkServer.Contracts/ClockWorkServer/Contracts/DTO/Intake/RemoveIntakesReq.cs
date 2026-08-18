using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Intake
{
	// Token: 0x020005E8 RID: 1512
	[DataContract(Namespace = "http://tpro.ca")]
	public class RemoveIntakesReq : BaseMessageReq
	{
		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06001EC9 RID: 7881 RVA: 0x0000DFC3 File Offset: 0x0000C1C3
		// (set) Token: 0x06001ECA RID: 7882 RVA: 0x0000DFCB File Offset: 0x0000C1CB
		[DataMember]
		public int[] IntakePersonIds { get; set; }
	}
}
