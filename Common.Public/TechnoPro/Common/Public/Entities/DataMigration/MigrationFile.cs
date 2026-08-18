using System;

namespace TechnoPro.Common.Public.Entities.DataMigration
{
	// Token: 0x020003FE RID: 1022
	public class MigrationFile : IMigrationDataItems
	{
		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x06001F63 RID: 8035 RVA: 0x00023614 File Offset: 0x00021814
		// (set) Token: 0x06001F64 RID: 8036 RVA: 0x0002361C File Offset: 0x0002181C
		public string StudentNumber { get; set; }

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06001F65 RID: 8037 RVA: 0x00023625 File Offset: 0x00021825
		// (set) Token: 0x06001F66 RID: 8038 RVA: 0x0002362D File Offset: 0x0002182D
		public string FilenameWithPath { get; set; }
	}
}
