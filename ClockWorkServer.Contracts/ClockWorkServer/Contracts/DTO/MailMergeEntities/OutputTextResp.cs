using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x0200049F RID: 1183
	[DataContract(Namespace = "http://tpro.ca")]
	public class OutputTextResp
	{
		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x0600194E RID: 6478 RVA: 0x0000BB13 File Offset: 0x00009D13
		// (set) Token: 0x0600194F RID: 6479 RVA: 0x0000BB1B File Offset: 0x00009D1B
		[DataMember]
		public IList<string> MergedTexts { get; set; }
	}
}
