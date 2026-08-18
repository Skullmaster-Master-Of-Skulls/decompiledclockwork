using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000488 RID: 1160
	public class MailMergeAndStoreSignatureButtonFileInDocumentsResp
	{
		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x060018F1 RID: 6385 RVA: 0x0000B8C0 File Offset: 0x00009AC0
		// (set) Token: 0x060018F2 RID: 6386 RVA: 0x0000B8C8 File Offset: 0x00009AC8
		[DataMember]
		public int FileId { get; set; }

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x060018F3 RID: 6387 RVA: 0x0000B8D1 File Offset: 0x00009AD1
		// (set) Token: 0x060018F4 RID: 6388 RVA: 0x0000B8D9 File Offset: 0x00009AD9
		[DataMember]
		public int[] FileListCidsFileWasStoredIn { get; set; }
	}
}
