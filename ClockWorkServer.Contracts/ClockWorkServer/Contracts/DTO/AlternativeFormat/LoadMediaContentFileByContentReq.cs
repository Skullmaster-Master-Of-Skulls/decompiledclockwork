using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B55 RID: 2901
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaContentFileByContentReq : BaseMessageReq
	{
		// Token: 0x170016B3 RID: 5811
		// (get) Token: 0x06003DA1 RID: 15777 RVA: 0x0001E477 File Offset: 0x0001C677
		// (set) Token: 0x06003DA2 RID: 15778 RVA: 0x0001E47F File Offset: 0x0001C67F
		[DataMember]
		public Guid MediaContentID { get; set; }

		// Token: 0x170016B4 RID: 5812
		// (get) Token: 0x06003DA3 RID: 15779 RVA: 0x0001E488 File Offset: 0x0001C688
		// (set) Token: 0x06003DA4 RID: 15780 RVA: 0x0001E490 File Offset: 0x0001C690
		[DataMember]
		public int StudentId { get; set; }
	}
}
