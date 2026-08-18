using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000487 RID: 1159
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeAndStoreSignatureButtonFileInDocumentsReq : BaseReportMessageReq
	{
		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x060018E0 RID: 6368 RVA: 0x0000B838 File Offset: 0x00009A38
		// (set) Token: 0x060018E1 RID: 6369 RVA: 0x0000B840 File Offset: 0x00009A40
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x060018E2 RID: 6370 RVA: 0x0000B849 File Offset: 0x00009A49
		// (set) Token: 0x060018E3 RID: 6371 RVA: 0x0000B851 File Offset: 0x00009A51
		[DataMember]
		public MailMergeCustomDictionaryDTO CustomArgs { get; set; }

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x060018E4 RID: 6372 RVA: 0x0000B85A File Offset: 0x00009A5A
		// (set) Token: 0x060018E5 RID: 6373 RVA: 0x0000B862 File Offset: 0x00009A62
		[DataMember]
		public int TemplateId { get; set; }

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x060018E6 RID: 6374 RVA: 0x0000B86B File Offset: 0x00009A6B
		// (set) Token: 0x060018E7 RID: 6375 RVA: 0x0000B873 File Offset: 0x00009A73
		[DataMember]
		public eFileFormatDTO OutputFormat { get; set; }

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x060018E8 RID: 6376 RVA: 0x0000B87C File Offset: 0x00009A7C
		// (set) Token: 0x060018E9 RID: 6377 RVA: 0x0000B884 File Offset: 0x00009A84
		[DataMember]
		public int OverrideFileListCid { get; set; }

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x060018EA RID: 6378 RVA: 0x0000B88D File Offset: 0x00009A8D
		// (set) Token: 0x060018EB RID: 6379 RVA: 0x0000B895 File Offset: 0x00009A95
		[DataMember]
		public string Title { get; set; }

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x060018EC RID: 6380 RVA: 0x0000B89E File Offset: 0x00009A9E
		// (set) Token: 0x060018ED RID: 6381 RVA: 0x0000B8A6 File Offset: 0x00009AA6
		[DataMember]
		public IDictionary<int, string> ModifiedPerStudentFileLists { get; set; }

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x060018EE RID: 6382 RVA: 0x0000B8AF File Offset: 0x00009AAF
		// (set) Token: 0x060018EF RID: 6383 RVA: 0x0000B8B7 File Offset: 0x00009AB7
		[DataMember]
		public int[] FileListCidsOnLocalForm { get; set; }
	}
}
