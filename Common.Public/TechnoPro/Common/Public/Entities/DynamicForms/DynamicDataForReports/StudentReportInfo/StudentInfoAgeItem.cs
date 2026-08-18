using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataForReports.StudentReportInfo
{
	// Token: 0x020003A6 RID: 934
	public class StudentInfoAgeItem : StudentInfoItemBase
	{
		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06001C68 RID: 7272 RVA: 0x000209B4 File Offset: 0x0001EBB4
		// (set) Token: 0x06001C69 RID: 7273 RVA: 0x000209BC File Offset: 0x0001EBBC
		public DateTime? DateOfBirth { get; set; }

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x06001C6A RID: 7274 RVA: 0x000209C5 File Offset: 0x0001EBC5
		// (set) Token: 0x06001C6B RID: 7275 RVA: 0x000209CD File Offset: 0x0001EBCD
		public int Age { get; set; }
	}
}
