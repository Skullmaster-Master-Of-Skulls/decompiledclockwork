using System;

namespace TechnoPro.Common.Public.Entities.People
{
	// Token: 0x02000262 RID: 610
	public class StaffWithCommonInfo
	{
		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x00018A24 File Offset: 0x00016C24
		// (set) Token: 0x0600125D RID: 4701 RVA: 0x00018A2C File Offset: 0x00016C2C
		public PersonBase Staff { get; set; }

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x00018A35 File Offset: 0x00016C35
		// (set) Token: 0x0600125F RID: 4703 RVA: 0x00018A3D File Offset: 0x00016C3D
		public StaffCommonInfo StaffCommonInfo { get; set; }
	}
}
