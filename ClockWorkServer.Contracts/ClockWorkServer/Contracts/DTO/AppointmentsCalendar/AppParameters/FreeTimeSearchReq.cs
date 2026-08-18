using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B26 RID: 2854
	[DataContract(Namespace = "http://tpro.ca")]
	public class FreeTimeSearchReq : BaseMessageReq
	{
		// Token: 0x17001613 RID: 5651
		// (get) Token: 0x06003C22 RID: 15394 RVA: 0x0001D342 File Offset: 0x0001B542
		// (set) Token: 0x06003C23 RID: 15395 RVA: 0x0001D34A File Offset: 0x0001B54A
		[DataMember]
		public FreeTimeSearchContextDTO FreeTimeSearchContext { get; set; }
	}
}
