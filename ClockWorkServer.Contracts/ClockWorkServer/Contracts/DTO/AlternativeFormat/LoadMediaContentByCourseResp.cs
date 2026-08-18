using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B71 RID: 2929
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaContentByCourseResp
	{
		// Token: 0x170016DA RID: 5850
		// (get) Token: 0x06003E0B RID: 15883 RVA: 0x0001E70E File Offset: 0x0001C90E
		// (set) Token: 0x06003E0C RID: 15884 RVA: 0x0001E716 File Offset: 0x0001C916
		[DataMember]
		public IList<MediaContentDTO> MediaContents { get; set; }
	}
}
