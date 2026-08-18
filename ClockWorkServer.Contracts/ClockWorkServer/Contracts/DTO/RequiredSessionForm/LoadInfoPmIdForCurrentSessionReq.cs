using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.RequiredSessionForm
{
	// Token: 0x020002F8 RID: 760
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadInfoPmIdForCurrentSessionReq : BaseMessageReq
	{
		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001181 RID: 4481 RVA: 0x00008368 File Offset: 0x00006568
		// (set) Token: 0x06001182 RID: 4482 RVA: 0x00008370 File Offset: 0x00006570
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x00008379 File Offset: 0x00006579
		// (set) Token: 0x06001184 RID: 4484 RVA: 0x00008381 File Offset: 0x00006581
		[DataMember]
		public int ScreenNum { get; set; }
	}
}
