using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.ClockWorkSnapshot
{
	// Token: 0x0200044A RID: 1098
	public class ClockWorkSnapshotTable : BusinessBase<string>
	{
		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06002141 RID: 8513 RVA: 0x00025530 File Offset: 0x00023730
		// (set) Token: 0x06002142 RID: 8514 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public string TableName
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x06002143 RID: 8515 RVA: 0x00025548 File Offset: 0x00023748
		// (set) Token: 0x06002144 RID: 8516 RVA: 0x00025550 File Offset: 0x00023750
		public List<string> EncryptedColumns { get; set; }

		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x06002145 RID: 8517 RVA: 0x00025559 File Offset: 0x00023759
		// (set) Token: 0x06002146 RID: 8518 RVA: 0x00025561 File Offset: 0x00023761
		public int OrderNum { get; set; }
	}
}
