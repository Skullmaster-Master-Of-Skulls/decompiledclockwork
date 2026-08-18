using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Veteran
{
	// Token: 0x02000122 RID: 290
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadChangeInBenefitRequestsReq : BaseMessageReq
	{
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x000033E2 File Offset: 0x000015E2
		// (set) Token: 0x06000768 RID: 1896 RVA: 0x000033EA File Offset: 0x000015EA
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x000033F3 File Offset: 0x000015F3
		// (set) Token: 0x0600076A RID: 1898 RVA: 0x000033FB File Offset: 0x000015FB
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x00003404 File Offset: 0x00001604
		// (set) Token: 0x0600076C RID: 1900 RVA: 0x0000340C File Offset: 0x0000160C
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}
