using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000341 RID: 833
	[DataContract(Namespace = "http://tpro.ca")]
	public class ReportOptionsDTO
	{
		// Token: 0x0600130B RID: 4875 RVA: 0x00008E54 File Offset: 0x00007054
		public ReportOptionsDTO()
		{
			this.ColumnFormattingRules = new List<ColumnFormattingRuleDTO>();
			this.TableSortingRule = new List<ColumnSortingRuleDTO>();
			this.ColumnsToHide = new List<string>();
			this.RowFormattings = new List<RowFormattingDTO>();
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x00008E8E File Offset: 0x0000708E
		// (set) Token: 0x0600130D RID: 4877 RVA: 0x00008E96 File Offset: 0x00007096
		[DataMember]
		public IList<ColumnFormattingRuleDTO> ColumnFormattingRules { get; set; }

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x00008E9F File Offset: 0x0000709F
		// (set) Token: 0x0600130F RID: 4879 RVA: 0x00008EA7 File Offset: 0x000070A7
		[DataMember]
		public IList<ColumnSortingRuleDTO> TableSortingRule { get; set; }

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x00008EB0 File Offset: 0x000070B0
		// (set) Token: 0x06001311 RID: 4881 RVA: 0x00008EB8 File Offset: 0x000070B8
		[DataMember]
		public IList<string> ColumnsToHide { get; set; }

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x00008EC1 File Offset: 0x000070C1
		// (set) Token: 0x06001313 RID: 4883 RVA: 0x00008EC9 File Offset: 0x000070C9
		[DataMember]
		public bool DontShowReportRunResults { get; set; }

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x00008ED2 File Offset: 0x000070D2
		// (set) Token: 0x06001315 RID: 4885 RVA: 0x00008EDA File Offset: 0x000070DA
		[DataMember]
		public IList<RowFormattingDTO> RowFormattings { get; set; }

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x00008EE3 File Offset: 0x000070E3
		// (set) Token: 0x06001317 RID: 4887 RVA: 0x00008EEB File Offset: 0x000070EB
		[DataMember]
		public IList<string> GroupingColumns { get; set; }

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x00008EF4 File Offset: 0x000070F4
		// (set) Token: 0x06001319 RID: 4889 RVA: 0x00008EFC File Offset: 0x000070FC
		[DataMember]
		public string NoteToUser { get; set; }
	}
}
