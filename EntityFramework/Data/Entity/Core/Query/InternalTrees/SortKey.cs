using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200062A RID: 1578
	internal class SortKey
	{
		// Token: 0x06003D7A RID: 15738 RVA: 0x0011B36E File Offset: 0x0011956E
		internal SortKey(Var v, bool asc, string collation)
		{
			this.Var = v;
			this.m_asc = asc;
			this.m_collation = collation;
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06003D7B RID: 15739 RVA: 0x0011B38B File Offset: 0x0011958B
		// (set) Token: 0x06003D7C RID: 15740 RVA: 0x0011B393 File Offset: 0x00119593
		internal Var Var { get; set; }

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06003D7D RID: 15741 RVA: 0x0011B39C File Offset: 0x0011959C
		internal bool AscendingSort
		{
			get
			{
				return this.m_asc;
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06003D7E RID: 15742 RVA: 0x0011B3A4 File Offset: 0x001195A4
		internal string Collation
		{
			get
			{
				return this.m_collation;
			}
		}

		// Token: 0x04001737 RID: 5943
		private readonly bool m_asc;

		// Token: 0x04001738 RID: 5944
		private readonly string m_collation;
	}
}
