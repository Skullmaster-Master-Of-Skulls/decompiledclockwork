using System;

namespace TechnoPro.Common.Public.Entities.DataMigration.Internal
{
	// Token: 0x02000418 RID: 1048
	public class MigrationFileInfo
	{
		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x06001FF2 RID: 8178 RVA: 0x0002456C File Offset: 0x0002276C
		// (set) Token: 0x06001FF3 RID: 8179 RVA: 0x00024574 File Offset: 0x00022774
		public string FileNameWithPath { get; set; }

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x06001FF4 RID: 8180 RVA: 0x0002457D File Offset: 0x0002277D
		// (set) Token: 0x06001FF5 RID: 8181 RVA: 0x00024585 File Offset: 0x00022785
		public string UniqueFilenameWithoutPath { get; set; }
	}
}
