using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A24 RID: 2596
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadSittingsByDateRangeResp
	{
		// Token: 0x17001356 RID: 4950
		// (get) Token: 0x060035A7 RID: 13735 RVA: 0x0001A089 File Offset: 0x00018289
		// (set) Token: 0x060035A8 RID: 13736 RVA: 0x0001A091 File Offset: 0x00018291
		[DataMember]
		public IList<SittingDTO> Sittings { get; set; }
	}
}
