using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A1C RID: 2588
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateTestDeliveredReq : BaseMessageReq
	{
		// Token: 0x1700134D RID: 4941
		// (get) Token: 0x0600358D RID: 13709 RVA: 0x00019FF0 File Offset: 0x000181F0
		// (set) Token: 0x0600358E RID: 13710 RVA: 0x00019FF8 File Offset: 0x000181F8
		[DataMember]
		public int ExamId { get; set; }

		// Token: 0x1700134E RID: 4942
		// (get) Token: 0x0600358F RID: 13711 RVA: 0x0001A001 File Offset: 0x00018201
		// (set) Token: 0x06003590 RID: 13712 RVA: 0x0001A009 File Offset: 0x00018209
		[DataMember]
		public string TestDeliveredMessage { get; set; }
	}
}
