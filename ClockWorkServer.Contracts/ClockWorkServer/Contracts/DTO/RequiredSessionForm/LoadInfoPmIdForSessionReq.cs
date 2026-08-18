using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.RequiredSessionForm
{
	// Token: 0x020002FA RID: 762
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadInfoPmIdForSessionReq : BaseMessageReq
	{
		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x0000839B File Offset: 0x0000659B
		// (set) Token: 0x0600118A RID: 4490 RVA: 0x000083A3 File Offset: 0x000065A3
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x000083AC File Offset: 0x000065AC
		// (set) Token: 0x0600118C RID: 4492 RVA: 0x000083B4 File Offset: 0x000065B4
		[DataMember]
		public int ScreenNum { get; set; }

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x000083BD File Offset: 0x000065BD
		// (set) Token: 0x0600118E RID: 4494 RVA: 0x000083C5 File Offset: 0x000065C5
		[DataMember]
		public DateTime DateInSession { get; set; }
	}
}
