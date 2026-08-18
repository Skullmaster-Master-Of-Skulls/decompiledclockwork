using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B8F RID: 2959
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllMediaContentDetailByContentIdResp
	{
		// Token: 0x170016F5 RID: 5877
		// (get) Token: 0x06003E5F RID: 15967 RVA: 0x0001E8D9 File Offset: 0x0001CAD9
		// (set) Token: 0x06003E60 RID: 15968 RVA: 0x0001E8E1 File Offset: 0x0001CAE1
		[DataMember]
		public IList<MediaContentDetailDTO> MediaContentDetails { get; set; }
	}
}
