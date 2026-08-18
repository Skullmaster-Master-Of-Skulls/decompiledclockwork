using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009EA RID: 2538
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadSittingByIdResp
	{
		// Token: 0x17001310 RID: 4880
		// (get) Token: 0x060034E1 RID: 13537 RVA: 0x00019BE3 File Offset: 0x00017DE3
		// (set) Token: 0x060034E2 RID: 13538 RVA: 0x00019BEB File Offset: 0x00017DEB
		[DataMember]
		public SittingDTO Sitting { get; set; }
	}
}
