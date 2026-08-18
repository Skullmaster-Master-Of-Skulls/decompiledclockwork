using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.MailMergeEntities.Output;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x0200049C RID: 1180
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeTextReq : BaseReportMessageReq
	{
		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06001941 RID: 6465 RVA: 0x0000BABE File Offset: 0x00009CBE
		// (set) Token: 0x06001942 RID: 6466 RVA: 0x0000BAC6 File Offset: 0x00009CC6
		[DataMember]
		public MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary { get; set; }

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06001943 RID: 6467 RVA: 0x0000BACF File Offset: 0x00009CCF
		// (set) Token: 0x06001944 RID: 6468 RVA: 0x0000BAD7 File Offset: 0x00009CD7
		[DataMember]
		public string Template { get; set; }

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06001945 RID: 6469 RVA: 0x0000BAE0 File Offset: 0x00009CE0
		// (set) Token: 0x06001946 RID: 6470 RVA: 0x0000BAE8 File Offset: 0x00009CE8
		[DataMember]
		public eMailMergeDocumentOutputFormat MailMergeDocumentOutputFormat { get; set; }
	}
}
