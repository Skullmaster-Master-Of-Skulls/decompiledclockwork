using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B0A RID: 2826
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveChannelsForStudentResp
	{
		// Token: 0x170015EB RID: 5611
		// (get) Token: 0x06003BB6 RID: 15286 RVA: 0x0001D09A File Offset: 0x0001B29A
		// (set) Token: 0x06003BB7 RID: 15287 RVA: 0x0001D0A2 File Offset: 0x0001B2A2
		[DataMember]
		public IList<ChannelDTO> ActiveChannels { get; set; }
	}
}
