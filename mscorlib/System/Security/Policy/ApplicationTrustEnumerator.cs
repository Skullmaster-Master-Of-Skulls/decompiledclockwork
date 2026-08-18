using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x0200049C RID: 1180
	[ComVisible(true)]
	public sealed class ApplicationTrustEnumerator : IEnumerator
	{
		// Token: 0x06002ECD RID: 11981 RVA: 0x0009E1D9 File Offset: 0x0009D1D9
		private ApplicationTrustEnumerator()
		{
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x0009E1E1 File Offset: 0x0009D1E1
		internal ApplicationTrustEnumerator(ApplicationTrustCollection trusts)
		{
			this.m_trusts = trusts;
			this.m_current = -1;
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06002ECF RID: 11983 RVA: 0x0009E1F7 File Offset: 0x0009D1F7
		public ApplicationTrust Current
		{
			get
			{
				return this.m_trusts[this.m_current];
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06002ED0 RID: 11984 RVA: 0x0009E20A File Offset: 0x0009D20A
		object IEnumerator.Current
		{
			get
			{
				return this.m_trusts[this.m_current];
			}
		}

		// Token: 0x06002ED1 RID: 11985 RVA: 0x0009E21D File Offset: 0x0009D21D
		public bool MoveNext()
		{
			if (this.m_current == this.m_trusts.Count - 1)
			{
				return false;
			}
			this.m_current++;
			return true;
		}

		// Token: 0x06002ED2 RID: 11986 RVA: 0x0009E245 File Offset: 0x0009D245
		public void Reset()
		{
			this.m_current = -1;
		}

		// Token: 0x040017E9 RID: 6121
		private ApplicationTrustCollection m_trusts;

		// Token: 0x040017EA RID: 6122
		private int m_current;
	}
}
