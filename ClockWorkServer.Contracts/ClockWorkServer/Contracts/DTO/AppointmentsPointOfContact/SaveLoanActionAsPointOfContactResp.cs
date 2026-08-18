using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsPointOfContact
{
	// Token: 0x0200091F RID: 2335
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveLoanActionAsPointOfContactResp
	{
		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x06002F50 RID: 12112 RVA: 0x00016846 File Offset: 0x00014A46
		// (set) Token: 0x06002F51 RID: 12113 RVA: 0x0001684E File Offset: 0x00014A4E
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
