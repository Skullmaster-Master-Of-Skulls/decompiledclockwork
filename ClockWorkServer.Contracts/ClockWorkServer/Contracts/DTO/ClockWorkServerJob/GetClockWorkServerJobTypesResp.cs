using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x0200086E RID: 2158
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetClockWorkServerJobTypesResp
	{
		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x06002BD6 RID: 11222 RVA: 0x00014C56 File Offset: 0x00012E56
		// (set) Token: 0x06002BD7 RID: 11223 RVA: 0x00014C5E File Offset: 0x00012E5E
		[DataMember]
		public IList<ClockWorkServerJobExecutingTypeInfoDTO> ClockWorkServerJobTypeList { get; set; }
	}
}
