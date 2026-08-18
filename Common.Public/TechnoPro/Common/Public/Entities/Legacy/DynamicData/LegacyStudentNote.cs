using System;

namespace TechnoPro.Common.Public.Entities.Legacy.DynamicData
{
	// Token: 0x020002FC RID: 764
	public class LegacyStudentNote
	{
		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x0600174D RID: 5965 RVA: 0x0001C4E8 File Offset: 0x0001A6E8
		// (set) Token: 0x0600174E RID: 5966 RVA: 0x0001C4F0 File Offset: 0x0001A6F0
		public int PersonId { get; set; }

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x0600174F RID: 5967 RVA: 0x0001C4F9 File Offset: 0x0001A6F9
		// (set) Token: 0x06001750 RID: 5968 RVA: 0x0001C501 File Offset: 0x0001A701
		public int ControlId { get; set; }

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06001751 RID: 5969 RVA: 0x0001C50A File Offset: 0x0001A70A
		// (set) Token: 0x06001752 RID: 5970 RVA: 0x0001C512 File Offset: 0x0001A712
		public string ControlValue { get; set; }
	}
}
