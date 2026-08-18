using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x02000844 RID: 2116
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetControllerActiveJobListResp
	{
		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x06002B16 RID: 11030 RVA: 0x00014739 File Offset: 0x00012939
		// (set) Token: 0x06002B17 RID: 11031 RVA: 0x00014741 File Offset: 0x00012941
		[DataMember]
		public IList<ClockWorkServerJobInfoExDTO> ControllerActiveJobList { get; set; }
	}
}
