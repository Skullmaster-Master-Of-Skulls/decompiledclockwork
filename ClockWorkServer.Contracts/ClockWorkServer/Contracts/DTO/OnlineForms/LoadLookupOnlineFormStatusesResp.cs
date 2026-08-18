using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x020003FC RID: 1020
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLookupOnlineFormStatusesResp
	{
		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x0000A605 File Offset: 0x00008805
		// (set) Token: 0x0600164A RID: 5706 RVA: 0x0000A60D File Offset: 0x0000880D
		[DataMember]
		public IList<OnlineFormStatusDTO> OnlineFormStatuses { get; set; }
	}
}
