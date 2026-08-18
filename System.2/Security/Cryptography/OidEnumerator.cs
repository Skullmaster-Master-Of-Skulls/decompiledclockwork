using System;
using System.Collections;

namespace System.Security.Cryptography
{
	// Token: 0x02000460 RID: 1120
	public sealed class OidEnumerator : IEnumerator
	{
		// Token: 0x060029A3 RID: 10659 RVA: 0x000BCDF2 File Offset: 0x000BAFF2
		private OidEnumerator()
		{
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x000BCDFA File Offset: 0x000BAFFA
		internal OidEnumerator(OidCollection oids)
		{
			this.m_oids = oids;
			this.m_current = -1;
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x060029A5 RID: 10661 RVA: 0x000BCE10 File Offset: 0x000BB010
		public Oid Current
		{
			get
			{
				return this.m_oids[this.m_current];
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x060029A6 RID: 10662 RVA: 0x000BCE23 File Offset: 0x000BB023
		object IEnumerator.Current
		{
			get
			{
				return this.m_oids[this.m_current];
			}
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x000BCE36 File Offset: 0x000BB036
		public bool MoveNext()
		{
			if (this.m_current == this.m_oids.Count - 1)
			{
				return false;
			}
			this.m_current++;
			return true;
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x000BCE5E File Offset: 0x000BB05E
		public void Reset()
		{
			this.m_current = -1;
		}

		// Token: 0x040025A2 RID: 9634
		private OidCollection m_oids;

		// Token: 0x040025A3 RID: 9635
		private int m_current;
	}
}
