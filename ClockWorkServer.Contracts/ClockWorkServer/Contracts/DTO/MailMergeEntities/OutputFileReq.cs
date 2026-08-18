using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x020004A9 RID: 1193
	[DataContract(Namespace = "http://tpro.ca")]
	public class OutputFileReq : BaseReportMessageReq
	{
		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06001974 RID: 6516 RVA: 0x0000BC01 File Offset: 0x00009E01
		// (set) Token: 0x06001975 RID: 6517 RVA: 0x0000BC09 File Offset: 0x00009E09
		[DataMember]
		public BinaryFileDTO Template { get; set; }

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06001976 RID: 6518 RVA: 0x0000BC12 File Offset: 0x00009E12
		// (set) Token: 0x06001977 RID: 6519 RVA: 0x0000BC1A File Offset: 0x00009E1A
		[DataMember]
		public List<MailMergeCodeDTO> Codes { get; set; }

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06001978 RID: 6520 RVA: 0x0000BC23 File Offset: 0x00009E23
		// (set) Token: 0x06001979 RID: 6521 RVA: 0x0000BC2B File Offset: 0x00009E2B
		[DataMember]
		public eFileFormatDTO FileFormat { get; set; }
	}
}
