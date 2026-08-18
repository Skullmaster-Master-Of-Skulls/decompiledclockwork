using System;

namespace TechnoPro.Common.Public.Entities.DataMigration
{
	// Token: 0x02000403 RID: 1027
	public class MigrationStudentWithPerDateData : MigrationStudentWithData
	{
		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06001F78 RID: 8056 RVA: 0x0002383D File Offset: 0x00021A3D
		// (set) Token: 0x06001F79 RID: 8057 RVA: 0x00023845 File Offset: 0x00021A45
		public DateTime DateKey { get; set; }

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06001F7A RID: 8058 RVA: 0x0002384E File Offset: 0x00021A4E
		// (set) Token: 0x06001F7B RID: 8059 RVA: 0x00023856 File Offset: 0x00021A56
		public string WhoEnteredStudent_no { get; set; }

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06001F7C RID: 8060 RVA: 0x0002385F File Offset: 0x00021A5F
		// (set) Token: 0x06001F7D RID: 8061 RVA: 0x00023867 File Offset: 0x00021A67
		public int WhoEnterePersonId { get; set; }
	}
}
