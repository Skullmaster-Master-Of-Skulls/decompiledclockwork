using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Updates
{
	// Token: 0x02000170 RID: 368
	[DataContract(Namespace = "http://tpro.ca")]
	public class CancelOnScheduleUpdatesReq : BaseMessageReq
	{
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x00003FAB File Offset: 0x000021AB
		// (set) Token: 0x060008E3 RID: 2275 RVA: 0x00003FB3 File Offset: 0x000021B3
		[DataMember]
		public IList<UpdateFileInfoDTO> Updates { get; set; }
	}
}
