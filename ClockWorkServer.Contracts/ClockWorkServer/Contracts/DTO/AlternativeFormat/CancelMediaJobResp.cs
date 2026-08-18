using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BDB RID: 3035
	[DataContract(Namespace = "http://tpro.ca")]
	public class CancelMediaJobResp
	{
		// Token: 0x17001797 RID: 6039
		// (get) Token: 0x06003FFB RID: 16379 RVA: 0x0001F6F1 File Offset: 0x0001D8F1
		// (set) Token: 0x06003FFC RID: 16380 RVA: 0x0001F6F9 File Offset: 0x0001D8F9
		[DataMember]
		public IList<MediaContentRequestedInfoDTO> MediaContentRequestedInfoList { get; set; }
	}
}
