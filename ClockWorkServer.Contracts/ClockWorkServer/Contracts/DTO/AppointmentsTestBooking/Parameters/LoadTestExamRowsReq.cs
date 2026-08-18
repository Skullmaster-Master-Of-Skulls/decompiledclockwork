using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A7F RID: 2687
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestExamRowsReq : BaseMessageReq
	{
		// Token: 0x1700147B RID: 5243
		// (get) Token: 0x06003846 RID: 14406 RVA: 0x0001B4F7 File Offset: 0x000196F7
		// (set) Token: 0x06003847 RID: 14407 RVA: 0x0001B4FF File Offset: 0x000196FF
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x1700147C RID: 5244
		// (get) Token: 0x06003848 RID: 14408 RVA: 0x0001B508 File Offset: 0x00019708
		// (set) Token: 0x06003849 RID: 14409 RVA: 0x0001B510 File Offset: 0x00019710
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x1700147D RID: 5245
		// (get) Token: 0x0600384A RID: 14410 RVA: 0x0001B519 File Offset: 0x00019719
		// (set) Token: 0x0600384B RID: 14411 RVA: 0x0001B521 File Offset: 0x00019721
		[DataMember]
		public bool HideCancelled { get; set; }
	}
}
