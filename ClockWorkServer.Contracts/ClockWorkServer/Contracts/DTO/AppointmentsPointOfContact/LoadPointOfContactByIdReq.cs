using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsPointOfContact
{
	// Token: 0x02000921 RID: 2337
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPointOfContactByIdReq : BaseMessageReq
	{
		// Token: 0x170010C4 RID: 4292
		// (get) Token: 0x06002F56 RID: 12118 RVA: 0x00016868 File Offset: 0x00014A68
		// (set) Token: 0x06002F57 RID: 12119 RVA: 0x00016870 File Offset: 0x00014A70
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
