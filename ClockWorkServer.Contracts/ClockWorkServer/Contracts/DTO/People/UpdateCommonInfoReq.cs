using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003B5 RID: 949
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateCommonInfoReq : BaseMessageReq
	{
		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x00009E51 File Offset: 0x00008051
		// (set) Token: 0x0600151B RID: 5403 RVA: 0x00009E59 File Offset: 0x00008059
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x0600151C RID: 5404 RVA: 0x00009E62 File Offset: 0x00008062
		// (set) Token: 0x0600151D RID: 5405 RVA: 0x00009E6A File Offset: 0x0000806A
		[DataMember]
		public StaffCommonInfoDTO CommonInfo { get; set; }

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x0600151E RID: 5406 RVA: 0x00009E73 File Offset: 0x00008073
		// (set) Token: 0x0600151F RID: 5407 RVA: 0x00009E7B File Offset: 0x0000807B
		[DataMember]
		public bool JustUpdateEmailAndPhone { get; set; }
	}
}
