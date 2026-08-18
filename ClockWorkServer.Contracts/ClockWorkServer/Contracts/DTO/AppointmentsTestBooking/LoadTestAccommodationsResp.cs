using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009F3 RID: 2547
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestAccommodationsResp
	{
		// Token: 0x17001320 RID: 4896
		// (get) Token: 0x0600350A RID: 13578 RVA: 0x00019CF3 File Offset: 0x00017EF3
		// (set) Token: 0x0600350B RID: 13579 RVA: 0x00019CFB File Offset: 0x00017EFB
		[DataMember]
		public List<AccommodationForTestDTO> AccommodationsForTest { get; set; }
	}
}
