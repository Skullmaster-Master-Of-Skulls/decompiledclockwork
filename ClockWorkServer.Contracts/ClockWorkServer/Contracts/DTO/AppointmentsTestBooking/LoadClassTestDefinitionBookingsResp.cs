using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A22 RID: 2594
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadClassTestDefinitionBookingsResp
	{
		// Token: 0x17001354 RID: 4948
		// (get) Token: 0x060035A1 RID: 13729 RVA: 0x0001A067 File Offset: 0x00018267
		// (set) Token: 0x060035A2 RID: 13730 RVA: 0x0001A06F File Offset: 0x0001826F
		[DataMember]
		public List<TestDTO> Tests { get; set; }
	}
}
