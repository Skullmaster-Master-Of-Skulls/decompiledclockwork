using System;

namespace TechnoPro.Common.Public.Entities.DataMigration.Results
{
	// Token: 0x0200040F RID: 1039
	public class MigrationExternalCourseResult
	{
		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06001FC3 RID: 8131 RVA: 0x00024284 File Offset: 0x00022484
		// (set) Token: 0x06001FC4 RID: 8132 RVA: 0x0002428C File Offset: 0x0002248C
		public MigrationExternalCourse ExternalCourse { get; set; }

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x06001FC5 RID: 8133 RVA: 0x00024295 File Offset: 0x00022495
		// (set) Token: 0x06001FC6 RID: 8134 RVA: 0x0002429D File Offset: 0x0002249D
		public eMigrationExternalCourseStatus Status { get; set; }

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x06001FC7 RID: 8135 RVA: 0x000242A6 File Offset: 0x000224A6
		// (set) Token: 0x06001FC8 RID: 8136 RVA: 0x000242AE File Offset: 0x000224AE
		public string ErrorMessage { get; set; }
	}
}
