using System;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.DataMigration.TestsAndExams
{
	// Token: 0x02000408 RID: 1032
	public class MigrationTestExamInternal : MigrationTestExam
	{
		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06001F9A RID: 8090 RVA: 0x00023D37 File Offset: 0x00021F37
		// (set) Token: 0x06001F9B RID: 8091 RVA: 0x00023D3F File Offset: 0x00021F3F
		public PersonBase ClockWorkStudent { get; set; }

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06001F9C RID: 8092 RVA: 0x00023D48 File Offset: 0x00021F48
		// (set) Token: 0x06001F9D RID: 8093 RVA: 0x00023D50 File Offset: 0x00021F50
		public PersonBase ClockWorkStaff { get; set; }

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06001F9E RID: 8094 RVA: 0x00023D59 File Offset: 0x00021F59
		// (set) Token: 0x06001F9F RID: 8095 RVA: 0x00023D61 File Offset: 0x00021F61
		public AppType AppType { get; set; }

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06001FA0 RID: 8096 RVA: 0x00023D6A File Offset: 0x00021F6A
		// (set) Token: 0x06001FA1 RID: 8097 RVA: 0x00023D72 File Offset: 0x00021F72
		public int LuCourseId { get; set; }
	}
}
