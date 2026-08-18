using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000339 RID: 825
	[DataContract(Namespace = "http://tpro.ca")]
	public class FormattedReportDTO
	{
		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x00008A1D File Offset: 0x00006C1D
		// (set) Token: 0x0600128D RID: 4749 RVA: 0x00008A25 File Offset: 0x00006C25
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x00008A2E File Offset: 0x00006C2E
		// (set) Token: 0x0600128F RID: 4751 RVA: 0x00008A36 File Offset: 0x00006C36
		[DataMember]
		public int ReportFileId { get; set; }

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x00008A3F File Offset: 0x00006C3F
		// (set) Token: 0x06001291 RID: 4753 RVA: 0x00008A47 File Offset: 0x00006C47
		[DataMember]
		public string Description { get; set; }

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001292 RID: 4754 RVA: 0x00008A50 File Offset: 0x00006C50
		// (set) Token: 0x06001293 RID: 4755 RVA: 0x00008A58 File Offset: 0x00006C58
		[DataMember]
		public byte[] FormattedReportTemplate { get; set; }

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x00008A61 File Offset: 0x00006C61
		// (set) Token: 0x06001295 RID: 4757 RVA: 0x00008A69 File Offset: 0x00006C69
		[DataMember]
		public string FileChecksum { get; set; }

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x00008A72 File Offset: 0x00006C72
		// (set) Token: 0x06001297 RID: 4759 RVA: 0x00008A7A File Offset: 0x00006C7A
		[DataMember]
		public int OrderNum { get; set; }
	}
}
