using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x02000088 RID: 136
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class SignerInfoEnumerator : IEnumerator
	{
		// Token: 0x06000537 RID: 1335 RVA: 0x000044A9 File Offset: 0x000026A9
		private SignerInfoEnumerator()
		{
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001B9CA File Offset: 0x00019BCA
		internal SignerInfoEnumerator(SignerInfoCollection signerInfos)
		{
			this.m_signerInfos = signerInfos;
			this.m_current = -1;
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x0001B9E0 File Offset: 0x00019BE0
		public SignerInfo Current
		{
			get
			{
				return this.m_signerInfos[this.m_current];
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0001B9E0 File Offset: 0x00019BE0
		object IEnumerator.Current
		{
			get
			{
				return this.m_signerInfos[this.m_current];
			}
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001B9F3 File Offset: 0x00019BF3
		public bool MoveNext()
		{
			if (this.m_current == this.m_signerInfos.Count - 1)
			{
				return false;
			}
			this.m_current++;
			return true;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001BA1B File Offset: 0x00019C1B
		public void Reset()
		{
			this.m_current = -1;
		}

		// Token: 0x04000521 RID: 1313
		private SignerInfoCollection m_signerInfos;

		// Token: 0x04000522 RID: 1314
		private int m_current;
	}
}
