using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x020004AC RID: 1196
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeResp
	{
		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06001983 RID: 6531 RVA: 0x0000BC67 File Offset: 0x00009E67
		// (set) Token: 0x06001984 RID: 6532 RVA: 0x0000BC6F File Offset: 0x00009E6F
		[DataMember]
		public IList<MailMergeCodeDTO> Codes { get; set; }
	}
}
