using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B74 RID: 2932
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaContentByCategoryReq : BaseMessageReq
	{
		// Token: 0x170016DD RID: 5853
		// (get) Token: 0x06003E14 RID: 15892 RVA: 0x0001E741 File Offset: 0x0001C941
		// (set) Token: 0x06003E15 RID: 15893 RVA: 0x0001E749 File Offset: 0x0001C949
		[DataMember]
		public eMediaContentCategory MediaContentCategory { get; set; }
	}
}
