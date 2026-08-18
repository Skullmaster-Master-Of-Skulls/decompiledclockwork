using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200096C RID: 2412
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetDoubleBookedAttendeesResp
	{
		// Token: 0x17001183 RID: 4483
		// (get) Token: 0x06003145 RID: 12613 RVA: 0x00018028 File Offset: 0x00016228
		// (set) Token: 0x06003146 RID: 12614 RVA: 0x00018030 File Offset: 0x00016230
		[DataMember]
		public IList<int> DoubleBookedPersonIds { get; set; }
	}
}
