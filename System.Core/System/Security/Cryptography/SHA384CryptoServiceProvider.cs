using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x02000116 RID: 278
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class SHA384CryptoServiceProvider : SHA384
	{
		// Token: 0x060008F0 RID: 2288 RVA: 0x0001F06D File Offset: 0x0001D26D
		public SHA384CryptoServiceProvider()
		{
			this.m_hashAlgorithm = new CapiHashAlgorithm("Microsoft Enhanced RSA and AES Cryptographic Provider", CapiNative.ProviderType.RsaAes, CapiNative.AlgorithmId.Sha384);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0001F08C File Offset: 0x0001D28C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.m_hashAlgorithm.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0001F0C4 File Offset: 0x0001D2C4
		public override void Initialize()
		{
			this.m_hashAlgorithm.Initialize();
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0001F0D1 File Offset: 0x0001D2D1
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.m_hashAlgorithm.HashCore(array, ibStart, cbSize);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0001F0E1 File Offset: 0x0001D2E1
		protected override byte[] HashFinal()
		{
			return this.m_hashAlgorithm.HashFinal();
		}

		// Token: 0x040006C0 RID: 1728
		private CapiHashAlgorithm m_hashAlgorithm;
	}
}
