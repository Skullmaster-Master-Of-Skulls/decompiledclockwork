using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BE7 RID: 3047
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaJobStatusByGroupResp
	{
		// Token: 0x170017B5 RID: 6069
		// (get) Token: 0x06004043 RID: 16451 RVA: 0x0001F8EF File Offset: 0x0001DAEF
		// (set) Token: 0x06004044 RID: 16452 RVA: 0x0001F8F7 File Offset: 0x0001DAF7
		[DataMember]
		public IList<MediaJobStatusDTO> MediaJobStatusList { get; set; }
	}
}
