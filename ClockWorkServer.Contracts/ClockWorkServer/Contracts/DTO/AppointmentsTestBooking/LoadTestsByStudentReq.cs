using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A39 RID: 2617
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestsByStudentReq : BaseMessageReq
	{
		// Token: 0x1700137B RID: 4987
		// (get) Token: 0x06003606 RID: 13830 RVA: 0x0001A2FE File Offset: 0x000184FE
		// (set) Token: 0x06003607 RID: 13831 RVA: 0x0001A306 File Offset: 0x00018506
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x1700137C RID: 4988
		// (get) Token: 0x06003608 RID: 13832 RVA: 0x0001A30F File Offset: 0x0001850F
		// (set) Token: 0x06003609 RID: 13833 RVA: 0x0001A317 File Offset: 0x00018517
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x1700137D RID: 4989
		// (get) Token: 0x0600360A RID: 13834 RVA: 0x0001A320 File Offset: 0x00018520
		// (set) Token: 0x0600360B RID: 13835 RVA: 0x0001A328 File Offset: 0x00018528
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x1700137E RID: 4990
		// (get) Token: 0x0600360C RID: 13836 RVA: 0x0001A331 File Offset: 0x00018531
		// (set) Token: 0x0600360D RID: 13837 RVA: 0x0001A339 File Offset: 0x00018539
		[DataMember]
		public bool HideCancelled { get; set; }
	}
}
