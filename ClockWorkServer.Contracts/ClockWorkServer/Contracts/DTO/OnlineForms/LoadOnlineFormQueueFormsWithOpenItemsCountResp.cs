using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x02000410 RID: 1040
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadOnlineFormQueueFormsWithOpenItemsCountResp
	{
		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x0000A7F2 File Offset: 0x000089F2
		// (set) Token: 0x06001698 RID: 5784 RVA: 0x0000A7FA File Offset: 0x000089FA
		[DataMember]
		public IList<OnlineFormIdWithOpenItemsCountDTO> Items { get; set; }
	}
}
