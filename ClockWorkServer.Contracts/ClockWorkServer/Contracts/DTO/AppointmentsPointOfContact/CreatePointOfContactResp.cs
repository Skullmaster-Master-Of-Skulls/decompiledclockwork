using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsPointOfContact
{
	// Token: 0x0200091A RID: 2330
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreatePointOfContactResp
	{
		// Token: 0x170010BA RID: 4282
		// (get) Token: 0x06002F3B RID: 12091 RVA: 0x000167BE File Offset: 0x000149BE
		// (set) Token: 0x06002F3C RID: 12092 RVA: 0x000167C6 File Offset: 0x000149C6
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
