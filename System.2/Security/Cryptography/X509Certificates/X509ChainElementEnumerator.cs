using System;
using System.Collections;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000470 RID: 1136
	public sealed class X509ChainElementEnumerator : IEnumerator
	{
		// Token: 0x06002A4D RID: 10829 RVA: 0x000C1366 File Offset: 0x000BF566
		private X509ChainElementEnumerator()
		{
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x000C136E File Offset: 0x000BF56E
		internal X509ChainElementEnumerator(X509ChainElementCollection chainElements)
		{
			this.m_chainElements = chainElements;
			this.m_current = -1;
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06002A4F RID: 10831 RVA: 0x000C1384 File Offset: 0x000BF584
		public X509ChainElement Current
		{
			get
			{
				return this.m_chainElements[this.m_current];
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06002A50 RID: 10832 RVA: 0x000C1397 File Offset: 0x000BF597
		object IEnumerator.Current
		{
			get
			{
				return this.m_chainElements[this.m_current];
			}
		}

		// Token: 0x06002A51 RID: 10833 RVA: 0x000C13AA File Offset: 0x000BF5AA
		public bool MoveNext()
		{
			if (this.m_current == this.m_chainElements.Count - 1)
			{
				return false;
			}
			this.m_current++;
			return true;
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x000C13D2 File Offset: 0x000BF5D2
		public void Reset()
		{
			this.m_current = -1;
		}

		// Token: 0x0400260C RID: 9740
		private X509ChainElementCollection m_chainElements;

		// Token: 0x0400260D RID: 9741
		private int m_current;
	}
}
