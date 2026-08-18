using System;

namespace TechnoPro.Common.Public.Entities.MergeDuplicates
{
	// Token: 0x0200028D RID: 653
	public class TableAndColumn
	{
		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x060013D3 RID: 5075 RVA: 0x00019A24 File Offset: 0x00017C24
		// (set) Token: 0x060013D4 RID: 5076 RVA: 0x00019A2C File Offset: 0x00017C2C
		public eClockWorkTable Table { get; set; }

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x00019A35 File Offset: 0x00017C35
		// (set) Token: 0x060013D6 RID: 5078 RVA: 0x00019A3D File Offset: 0x00017C3D
		public eClockWorkColumn Column { get; set; }

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x00019A46 File Offset: 0x00017C46
		// (set) Token: 0x060013D8 RID: 5080 RVA: 0x00019A4E File Offset: 0x00017C4E
		public string OtherColumnName { get; set; }
	}
}
