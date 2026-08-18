using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.MailMergeEntities.Output;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x020004A0 RID: 1184
	[DataContract(Namespace = "http://tpro.ca")]
	public class OutputTextReq : BaseReportMessageReq
	{
		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06001951 RID: 6481 RVA: 0x0000BB24 File Offset: 0x00009D24
		// (set) Token: 0x06001952 RID: 6482 RVA: 0x0000BB2C File Offset: 0x00009D2C
		[DataMember]
		public IList<MailMergeCodeDTO> Codes { get; set; }

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x0000BB35 File Offset: 0x00009D35
		// (set) Token: 0x06001954 RID: 6484 RVA: 0x0000BB3D File Offset: 0x00009D3D
		[DataMember]
		public string Template { get; set; }

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06001955 RID: 6485 RVA: 0x0000BB46 File Offset: 0x00009D46
		// (set) Token: 0x06001956 RID: 6486 RVA: 0x0000BB4E File Offset: 0x00009D4E
		[DataMember]
		public eMailMergeDocumentOutputFormat OutputFormat { get; set; }
	}
}
