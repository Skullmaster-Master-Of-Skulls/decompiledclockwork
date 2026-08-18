using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000430 RID: 1072
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadNotetakerBaseByIdResp
	{
		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x0600172E RID: 5934 RVA: 0x0000ABF7 File Offset: 0x00008DF7
		// (set) Token: 0x0600172F RID: 5935 RVA: 0x0000ABFF File Offset: 0x00008DFF
		[DataMember]
		public NotetakerBaseDTO NotetakerBase { get; set; }
	}
}
