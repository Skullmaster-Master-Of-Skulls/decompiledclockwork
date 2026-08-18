using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.DataMigration.Internal
{
	// Token: 0x0200041A RID: 1050
	public class MigrationStudentInternal : MigrationStudent
	{
		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06001FFA RID: 8186 RVA: 0x000245A8 File Offset: 0x000227A8
		// (set) Token: 0x06001FFB RID: 8187 RVA: 0x000245B0 File Offset: 0x000227B0
		public PersonBase ClockWorkStudent { get; set; }
	}
}
