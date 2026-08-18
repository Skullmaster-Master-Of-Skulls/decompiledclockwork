using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.DataMigration
{
	// Token: 0x02000401 RID: 1025
	public class MigrationStudentWithCourses : IMigrationDataItems
	{
		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x06001F6C RID: 8044 RVA: 0x00023650 File Offset: 0x00021850
		// (set) Token: 0x06001F6D RID: 8045 RVA: 0x00023658 File Offset: 0x00021858
		public MigrationStudent Student { get; set; }

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x06001F6E RID: 8046 RVA: 0x00023661 File Offset: 0x00021861
		// (set) Token: 0x06001F6F RID: 8047 RVA: 0x00023669 File Offset: 0x00021869
		public IList<MigrationExternalCourse> ExternalCourses { get; set; }
	}
}
