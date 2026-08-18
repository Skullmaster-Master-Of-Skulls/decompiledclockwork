using System;

namespace TechnoPro.Common.Public.Entities.DataMigration.TestsAndExams
{
	// Token: 0x02000407 RID: 1031
	public class MigrationTestExam : MigrationAppointment
	{
		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06001F97 RID: 8087 RVA: 0x00023D1D File Offset: 0x00021F1D
		// (set) Token: 0x06001F98 RID: 8088 RVA: 0x00023D25 File Offset: 0x00021F25
		public MigrationCourse Course { get; set; }
	}
}
