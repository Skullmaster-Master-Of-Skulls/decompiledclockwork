using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A01 RID: 2561
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadProctorByIdResp
	{
		// Token: 0x1700132B RID: 4907
		// (get) Token: 0x0600352E RID: 13614 RVA: 0x00019DAE File Offset: 0x00017FAE
		// (set) Token: 0x0600352F RID: 13615 RVA: 0x00019DB6 File Offset: 0x00017FB6
		[DataMember]
		public ProctorDTO Proctor { get; set; }
	}
}
