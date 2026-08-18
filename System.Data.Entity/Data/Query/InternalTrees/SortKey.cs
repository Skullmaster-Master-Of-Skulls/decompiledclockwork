using System;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000D4 RID: 212
	internal class SortKey
	{
		// Token: 0x06000C5F RID: 3167 RVA: 0x0003C13C File Offset: 0x0003A33C
		internal SortKey(Var v, bool asc, string collation)
		{
			this.m_var = v;
			this.m_asc = asc;
			this.m_collation = collation;
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x0003C159 File Offset: 0x0003A359
		// (set) Token: 0x06000C61 RID: 3169 RVA: 0x0003C161 File Offset: 0x0003A361
		internal Var Var
		{
			get
			{
				return this.m_var;
			}
			set
			{
				this.m_var = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x0003C16A File Offset: 0x0003A36A
		internal bool AscendingSort
		{
			get
			{
				return this.m_asc;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x0003C172 File Offset: 0x0003A372
		internal string Collation
		{
			get
			{
				return this.m_collation;
			}
		}

		// Token: 0x04000975 RID: 2421
		private Var m_var;

		// Token: 0x04000976 RID: 2422
		private bool m_asc;

		// Token: 0x04000977 RID: 2423
		private string m_collation;
	}
}
