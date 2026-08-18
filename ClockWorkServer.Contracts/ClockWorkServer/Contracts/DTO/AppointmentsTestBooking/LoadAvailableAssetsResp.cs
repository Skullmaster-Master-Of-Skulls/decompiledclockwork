using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009C0 RID: 2496
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAvailableAssetsResp
	{
		// Token: 0x17001299 RID: 4761
		// (get) Token: 0x060033C5 RID: 13253 RVA: 0x000192ED File Offset: 0x000174ED
		// (set) Token: 0x060033C6 RID: 13254 RVA: 0x000192F5 File Offset: 0x000174F5
		[DataMember]
		public IList<AssetDTO> Assets { get; set; }
	}
}
