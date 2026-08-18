using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x020004AE RID: 1198
	[DataContract(Namespace = "http://tpro.ca")]
	public class TestAllMailMergeCodesResp
	{
		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x0600198F RID: 6543 RVA: 0x0000BCBC File Offset: 0x00009EBC
		// (set) Token: 0x06001990 RID: 6544 RVA: 0x0000BCC4 File Offset: 0x00009EC4
		[DataMember]
		public IList<string> Text { get; set; }
	}
}
