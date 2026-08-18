using System;
using System.Collections;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x0200047D RID: 1149
	public sealed class X509ExtensionEnumerator : IEnumerator
	{
		// Token: 0x06002A9B RID: 10907 RVA: 0x000C23DA File Offset: 0x000C05DA
		private X509ExtensionEnumerator()
		{
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x000C23E2 File Offset: 0x000C05E2
		internal X509ExtensionEnumerator(X509ExtensionCollection extensions)
		{
			this.m_extensions = extensions;
			this.m_current = -1;
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06002A9D RID: 10909 RVA: 0x000C23F8 File Offset: 0x000C05F8
		public X509Extension Current
		{
			get
			{
				return this.m_extensions[this.m_current];
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06002A9E RID: 10910 RVA: 0x000C240B File Offset: 0x000C060B
		object IEnumerator.Current
		{
			get
			{
				return this.m_extensions[this.m_current];
			}
		}

		// Token: 0x06002A9F RID: 10911 RVA: 0x000C241E File Offset: 0x000C061E
		public bool MoveNext()
		{
			if (this.m_current == this.m_extensions.Count - 1)
			{
				return false;
			}
			this.m_current++;
			return true;
		}

		// Token: 0x06002AA0 RID: 10912 RVA: 0x000C2446 File Offset: 0x000C0646
		public void Reset()
		{
			this.m_current = -1;
		}

		// Token: 0x04002648 RID: 9800
		private X509ExtensionCollection m_extensions;

		// Token: 0x04002649 RID: 9801
		private int m_current;
	}
}
