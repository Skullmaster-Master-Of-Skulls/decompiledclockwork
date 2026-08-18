using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x02000113 RID: 275
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class SHA256CryptoServiceProvider : SHA256
	{
		// Token: 0x060008E5 RID: 2277 RVA: 0x0001EF65 File Offset: 0x0001D165
		public SHA256CryptoServiceProvider()
		{
			this.m_hashAlgorithm = new CapiHashAlgorithm("Microsoft Enhanced RSA and AES Cryptographic Provider", CapiNative.ProviderType.RsaAes, CapiNative.AlgorithmId.Sha256);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0001EF84 File Offset: 0x0001D184
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

		// Token: 0x060008E7 RID: 2279 RVA: 0x0001EFBC File Offset: 0x0001D1BC
		public override void Initialize()
		{
			this.m_hashAlgorithm.Initialize();
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0001EFC9 File Offset: 0x0001D1C9
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.m_hashAlgorithm.HashCore(array, ibStart, cbSize);
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0001EFD9 File Offset: 0x0001D1D9
		protected override byte[] HashFinal()
		{
			return this.m_hashAlgorithm.HashFinal();
		}

		// Token: 0x040006BE RID: 1726
		private CapiHashAlgorithm m_hashAlgorithm;
	}
}
