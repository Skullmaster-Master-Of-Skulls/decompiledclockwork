using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B52 RID: 2898
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllMediaContentFileSortedByMediaContentResp
	{
		// Token: 0x170016B0 RID: 5808
		// (get) Token: 0x06003D98 RID: 15768 RVA: 0x0001E444 File Offset: 0x0001C644
		// (set) Token: 0x06003D99 RID: 15769 RVA: 0x0001E44C File Offset: 0x0001C64C
		[DataMember]
		public IList<MediaContentFileWithoutDataDTO> MediaContentFiles { get; set; }
	}
}
