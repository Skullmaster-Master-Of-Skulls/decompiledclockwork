using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.MailMergeEntities.DocumentForPrint;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.DocumentForPrint
{
	// Token: 0x020004BC RID: 1212
	[DataContract(Namespace = "http://tpro.ca")]
	public class DocumentPrintItemDTO
	{
		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x060019C8 RID: 6600 RVA: 0x0000BEFF File Offset: 0x0000A0FF
		// (set) Token: 0x060019C9 RID: 6601 RVA: 0x0000BF07 File Offset: 0x0000A107
		[DataMember]
		public eDocumentPrintItemType ItemType { get; set; }

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x060019CA RID: 6602 RVA: 0x0000BF10 File Offset: 0x0000A110
		// (set) Token: 0x060019CB RID: 6603 RVA: 0x0000BF18 File Offset: 0x0000A118
		[DataMember]
		public string[] ColumnText { get; set; }
	}
}
