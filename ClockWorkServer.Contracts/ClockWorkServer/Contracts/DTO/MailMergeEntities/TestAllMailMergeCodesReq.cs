using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x020004AD RID: 1197
	[DataContract(Namespace = "http://tpro.ca")]
	public class TestAllMailMergeCodesReq : BaseReportMessageReq
	{
		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06001986 RID: 6534 RVA: 0x0000BC78 File Offset: 0x00009E78
		// (set) Token: 0x06001987 RID: 6535 RVA: 0x0000BC80 File Offset: 0x00009E80
		[DataMember]
		public string StartingContextString { get; set; }

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06001988 RID: 6536 RVA: 0x0000BC89 File Offset: 0x00009E89
		// (set) Token: 0x06001989 RID: 6537 RVA: 0x0000BC91 File Offset: 0x00009E91
		[DataMember]
		public MailMergeContextDTO StartingContext { get; set; }

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x0600198A RID: 6538 RVA: 0x0000BC9A File Offset: 0x00009E9A
		// (set) Token: 0x0600198B RID: 6539 RVA: 0x0000BCA2 File Offset: 0x00009EA2
		[DataMember]
		public string TemplateHeaderText { get; set; }

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x0600198C RID: 6540 RVA: 0x0000BCAB File Offset: 0x00009EAB
		// (set) Token: 0x0600198D RID: 6541 RVA: 0x0000BCB3 File Offset: 0x00009EB3
		[DataMember]
		public IList<string> CustomMailMergeCodes { get; set; }
	}
}
