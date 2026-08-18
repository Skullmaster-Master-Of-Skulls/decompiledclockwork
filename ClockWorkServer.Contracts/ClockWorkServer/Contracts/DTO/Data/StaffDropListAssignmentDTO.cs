using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Data
{
	// Token: 0x020006F5 RID: 1781
	[DataContract(Namespace = "http://tpro.ca")]
	public class StaffDropListAssignmentDTO
	{
		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06002462 RID: 9314 RVA: 0x000109B2 File Offset: 0x0000EBB2
		// (set) Token: 0x06002463 RID: 9315 RVA: 0x000109BA File Offset: 0x0000EBBA
		[DataMember]
		public virtual int DataId { get; set; }

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06002464 RID: 9316 RVA: 0x000109C3 File Offset: 0x0000EBC3
		// (set) Token: 0x06002465 RID: 9317 RVA: 0x000109CB File Offset: 0x0000EBCB
		[DataMember]
		public BasicPersonDTO Student { get; set; }
	}
}
