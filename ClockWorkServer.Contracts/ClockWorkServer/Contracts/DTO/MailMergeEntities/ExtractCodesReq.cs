using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x0200049E RID: 1182
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExtractCodesReq : BaseReportMessageReq
	{
		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x0600194B RID: 6475 RVA: 0x0000BB02 File Offset: 0x00009D02
		// (set) Token: 0x0600194C RID: 6476 RVA: 0x0000BB0A File Offset: 0x00009D0A
		[DataMember]
		public string Template { get; set; }
	}
}
