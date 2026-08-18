using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008CE RID: 2254
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteAvailabilityDatesAndTimesByContextResp
	{
		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x06002D9C RID: 11676 RVA: 0x000158FA File Offset: 0x00013AFA
		// (set) Token: 0x06002D9D RID: 11677 RVA: 0x00015902 File Offset: 0x00013B02
		[DataMember]
		public IList<DeleteAvailabilityActionResultDTO> Result { get; set; }
	}
}
