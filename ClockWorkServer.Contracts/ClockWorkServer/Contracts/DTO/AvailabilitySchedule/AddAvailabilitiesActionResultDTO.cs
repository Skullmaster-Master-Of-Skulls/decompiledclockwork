using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008B7 RID: 2231
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddAvailabilitiesActionResultDTO
	{
		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x06002D21 RID: 11553 RVA: 0x000155A8 File Offset: 0x000137A8
		// (set) Token: 0x06002D22 RID: 11554 RVA: 0x000155B0 File Offset: 0x000137B0
		[DataMember]
		public bool AbortedEntireProcess { get; set; }

		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x06002D23 RID: 11555 RVA: 0x000155B9 File Offset: 0x000137B9
		// (set) Token: 0x06002D24 RID: 11556 RVA: 0x000155C1 File Offset: 0x000137C1
		[DataMember]
		public IList<AddAvailabilityActionResultDTO> Results { get; set; }
	}
}
