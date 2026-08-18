using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x0200049B RID: 1179
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeTextResp
	{
		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x0600193E RID: 6462 RVA: 0x0000BAAD File Offset: 0x00009CAD
		// (set) Token: 0x0600193F RID: 6463 RVA: 0x0000BAB5 File Offset: 0x00009CB5
		[DataMember]
		public IList<string> MergedTexts { get; set; }
	}
}
