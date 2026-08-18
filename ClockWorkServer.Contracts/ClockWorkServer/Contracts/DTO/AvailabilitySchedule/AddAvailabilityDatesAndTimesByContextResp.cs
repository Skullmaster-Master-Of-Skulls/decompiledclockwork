using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008CA RID: 2250
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddAvailabilityDatesAndTimesByContextResp
	{
		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x06002D8C RID: 11660 RVA: 0x00015894 File Offset: 0x00013A94
		// (set) Token: 0x06002D8D RID: 11661 RVA: 0x0001589C File Offset: 0x00013A9C
		[DataMember]
		public AddAvailabilitiesActionResultDTO Result { get; set; }
	}
}
