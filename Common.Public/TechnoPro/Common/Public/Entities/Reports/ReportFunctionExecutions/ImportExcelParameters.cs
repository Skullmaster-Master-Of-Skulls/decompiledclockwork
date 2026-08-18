using System;

namespace TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions
{
	// Token: 0x0200024A RID: 586
	public class ImportExcelParameters
	{
		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x00018566 File Offset: 0x00016766
		// (set) Token: 0x060011CA RID: 4554 RVA: 0x0001856E File Offset: 0x0001676E
		public string ExcelFilenameWithPath { get; set; }

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x00018577 File Offset: 0x00016777
		// (set) Token: 0x060011CC RID: 4556 RVA: 0x0001857F File Offset: 0x0001677F
		public string WorksheetName { get; set; }

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x00018588 File Offset: 0x00016788
		// (set) Token: 0x060011CE RID: 4558 RVA: 0x00018590 File Offset: 0x00016790
		public int WorksheetIndex { get; set; }
	}
}
