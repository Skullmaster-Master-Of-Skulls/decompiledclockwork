using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200031E RID: 798
	[DataContract(Namespace = "http://tpro.ca")]
	public class TryToCompileCSharpReq : BaseReportMessageReq
	{
		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001227 RID: 4647 RVA: 0x000087A8 File Offset: 0x000069A8
		// (set) Token: 0x06001228 RID: 4648 RVA: 0x000087B0 File Offset: 0x000069B0
		[DataMember]
		public string Code { get; set; }

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x000087B9 File Offset: 0x000069B9
		// (set) Token: 0x0600122A RID: 4650 RVA: 0x000087C1 File Offset: 0x000069C1
		[DataMember]
		public IList<string> Imports { get; set; }
	}
}
