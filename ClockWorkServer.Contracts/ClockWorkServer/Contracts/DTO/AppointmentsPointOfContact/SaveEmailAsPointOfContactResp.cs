using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsPointOfContact
{
	// Token: 0x0200091D RID: 2333
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveEmailAsPointOfContactResp
	{
		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x06002F4A RID: 12106 RVA: 0x00016824 File Offset: 0x00014A24
		// (set) Token: 0x06002F4B RID: 12107 RVA: 0x0001682C File Offset: 0x00014A2C
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
