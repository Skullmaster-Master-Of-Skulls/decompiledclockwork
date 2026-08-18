using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x0200040E RID: 1038
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllStudentOnlineFormsResp
	{
		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x0000A7AE File Offset: 0x000089AE
		// (set) Token: 0x0600168E RID: 5774 RVA: 0x0000A7B6 File Offset: 0x000089B6
		[DataMember]
		public IList<OnlineFormQueueItemDTO> Items { get; set; }
	}
}
