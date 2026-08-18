using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B09 RID: 2825
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveChannelsForStudentReq : BaseMessageReq
	{
		// Token: 0x170015EA RID: 5610
		// (get) Token: 0x06003BB3 RID: 15283 RVA: 0x0001D089 File Offset: 0x0001B289
		// (set) Token: 0x06003BB4 RID: 15284 RVA: 0x0001D091 File Offset: 0x0001B291
		[DataMember]
		public int StudentPersonId { get; set; }
	}
}
