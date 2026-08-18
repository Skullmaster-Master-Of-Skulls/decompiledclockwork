using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BDD RID: 3037
	[DataContract(Namespace = "http://tpro.ca")]
	public class MarkMediaJobAsCompletedResp
	{
		// Token: 0x1700179C RID: 6044
		// (get) Token: 0x06004007 RID: 16391 RVA: 0x0001F746 File Offset: 0x0001D946
		// (set) Token: 0x06004008 RID: 16392 RVA: 0x0001F74E File Offset: 0x0001D94E
		[DataMember]
		public IList<MediaContentRequestedInfoDTO> MediaContentRequestedInfoList { get; set; }
	}
}
