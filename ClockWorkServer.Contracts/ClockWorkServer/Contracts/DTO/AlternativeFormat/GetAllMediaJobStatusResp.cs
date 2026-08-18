using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BE9 RID: 3049
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetAllMediaJobStatusResp
	{
		// Token: 0x170017B6 RID: 6070
		// (get) Token: 0x06004047 RID: 16455 RVA: 0x0001F900 File Offset: 0x0001DB00
		// (set) Token: 0x06004048 RID: 16456 RVA: 0x0001F908 File Offset: 0x0001DB08
		[DataMember]
		public IList<MediaJobStatusDTO> MediaJobStatusList { get; set; }
	}
}
