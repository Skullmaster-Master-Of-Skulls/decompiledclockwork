using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x0200049D RID: 1181
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExtractCodesResp
	{
		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06001948 RID: 6472 RVA: 0x0000BAF1 File Offset: 0x00009CF1
		// (set) Token: 0x06001949 RID: 6473 RVA: 0x0000BAF9 File Offset: 0x00009CF9
		[DataMember]
		public IList<MailMergeCodeDTO> Codes { get; set; }
	}
}
