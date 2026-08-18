using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Reports
{
	// Token: 0x02000219 RID: 537
	public class ReportOptions
	{
		// Token: 0x0600105B RID: 4187 RVA: 0x0001770D File Offset: 0x0001590D
		public ReportOptions()
		{
			this.ColumnFormattingRules = new List<ColumnFormattingRule>();
			this.TableSortingRule = new List<ColumnSortingRule>();
			this.ColumnsToHide = new List<string>();
			this.RowFormattings = new List<RowFormatting>();
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x00017747 File Offset: 0x00015947
		// (set) Token: 0x0600105D RID: 4189 RVA: 0x0001774F File Offset: 0x0001594F
		public IList<ColumnFormattingRule> ColumnFormattingRules { get; set; }

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x00017758 File Offset: 0x00015958
		// (set) Token: 0x0600105F RID: 4191 RVA: 0x00017760 File Offset: 0x00015960
		public IList<ColumnSortingRule> TableSortingRule { get; set; }

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x00017769 File Offset: 0x00015969
		// (set) Token: 0x06001061 RID: 4193 RVA: 0x00017771 File Offset: 0x00015971
		public IList<string> ColumnsToHide { get; set; }

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x0001777A File Offset: 0x0001597A
		// (set) Token: 0x06001063 RID: 4195 RVA: 0x00017782 File Offset: 0x00015982
		public IList<RowFormatting> RowFormattings { get; set; }

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x0001778B File Offset: 0x0001598B
		// (set) Token: 0x06001065 RID: 4197 RVA: 0x00017793 File Offset: 0x00015993
		public IList<string> GroupingColumns { get; set; }

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x0001779C File Offset: 0x0001599C
		// (set) Token: 0x06001067 RID: 4199 RVA: 0x000177A4 File Offset: 0x000159A4
		public bool DontShowReportRunResults { get; set; }

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001068 RID: 4200 RVA: 0x000177AD File Offset: 0x000159AD
		// (set) Token: 0x06001069 RID: 4201 RVA: 0x000177B5 File Offset: 0x000159B5
		public string NoteToUser { get; set; }
	}
}
