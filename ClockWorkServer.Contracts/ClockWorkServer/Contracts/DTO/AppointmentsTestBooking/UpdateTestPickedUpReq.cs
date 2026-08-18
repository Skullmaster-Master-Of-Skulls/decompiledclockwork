using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A38 RID: 2616
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateTestPickedUpReq : BaseMessageReq
	{
		// Token: 0x17001378 RID: 4984
		// (get) Token: 0x060035FF RID: 13823 RVA: 0x0001A2CB File Offset: 0x000184CB
		// (set) Token: 0x06003600 RID: 13824 RVA: 0x0001A2D3 File Offset: 0x000184D3
		[DataMember]
		public int ExamId { get; set; }

		// Token: 0x17001379 RID: 4985
		// (get) Token: 0x06003601 RID: 13825 RVA: 0x0001A2DC File Offset: 0x000184DC
		// (set) Token: 0x06003602 RID: 13826 RVA: 0x0001A2E4 File Offset: 0x000184E4
		[DataMember]
		public DateTime? TestPickedUpDate { get; set; }

		// Token: 0x1700137A RID: 4986
		// (get) Token: 0x06003603 RID: 13827 RVA: 0x0001A2ED File Offset: 0x000184ED
		// (set) Token: 0x06003604 RID: 13828 RVA: 0x0001A2F5 File Offset: 0x000184F5
		[DataMember]
		public string TestPickedUpNote { get; set; }
	}
}
