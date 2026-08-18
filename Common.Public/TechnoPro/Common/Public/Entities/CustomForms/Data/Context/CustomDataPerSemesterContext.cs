using System;

namespace TechnoPro.Common.Public.Entities.CustomForms.Data.Context
{
	// Token: 0x02000433 RID: 1075
	public class CustomDataPerSemesterContext : CustomDataContext
	{
		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x06002081 RID: 8321 RVA: 0x00024B9B File Offset: 0x00022D9B
		// (set) Token: 0x06002082 RID: 8322 RVA: 0x00024BA3 File Offset: 0x00022DA3
		public int PersonId { get; set; }

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x06002083 RID: 8323 RVA: 0x00024BAC File Offset: 0x00022DAC
		// (set) Token: 0x06002084 RID: 8324 RVA: 0x00024BB4 File Offset: 0x00022DB4
		public int SemesterId { get; set; }
	}
}
