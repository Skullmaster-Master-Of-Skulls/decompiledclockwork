using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x0200040A RID: 1034
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadOnlineFormQueueItemFormDataItemsResp
	{
		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001681 RID: 5761 RVA: 0x0000A76A File Offset: 0x0000896A
		// (set) Token: 0x06001682 RID: 5762 RVA: 0x0000A772 File Offset: 0x00008972
		[DataMember]
		public IList<DynamicDataDTO> DataItems { get; set; }
	}
}
