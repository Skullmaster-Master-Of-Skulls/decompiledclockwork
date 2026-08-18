using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AD3 RID: 2771
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadOverlappingAvailabilitiesReq : BaseMessageReq
	{
		// Token: 0x1700157F RID: 5503
		// (get) Token: 0x06003AA7 RID: 15015 RVA: 0x0001C958 File Offset: 0x0001AB58
		// (set) Token: 0x06003AA8 RID: 15016 RVA: 0x0001C960 File Offset: 0x0001AB60
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17001580 RID: 5504
		// (get) Token: 0x06003AA9 RID: 15017 RVA: 0x0001C969 File Offset: 0x0001AB69
		// (set) Token: 0x06003AAA RID: 15018 RVA: 0x0001C971 File Offset: 0x0001AB71
		[DataMember]
		public DateTime StartDateTime { get; set; }

		// Token: 0x17001581 RID: 5505
		// (get) Token: 0x06003AAB RID: 15019 RVA: 0x0001C97A File Offset: 0x0001AB7A
		// (set) Token: 0x06003AAC RID: 15020 RVA: 0x0001C982 File Offset: 0x0001AB82
		[DataMember]
		public DateTime EndDateTime { get; set; }
	}
}
