using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x020004A1 RID: 1185
	[DataContract(Namespace = "http://tpro.ca")]
	public class LookupCodeValuesResp
	{
		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06001958 RID: 6488 RVA: 0x0000BB57 File Offset: 0x00009D57
		// (set) Token: 0x06001959 RID: 6489 RVA: 0x0000BB5F File Offset: 0x00009D5F
		[DataMember]
		public IList<MailMergeCodeDTO> Codes { get; set; }
	}
}
