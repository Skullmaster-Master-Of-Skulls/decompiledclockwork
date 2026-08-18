using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x02000119 RID: 281
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class SHA512CryptoServiceProvider : SHA512
	{
		// Token: 0x060008FB RID: 2299 RVA: 0x0001F175 File Offset: 0x0001D375
		public SHA512CryptoServiceProvider()
		{
			this.m_hashAlgorithm = new CapiHashAlgorithm("Microsoft Enhanced RSA and AES Cryptographic Provider", CapiNative.ProviderType.RsaAes, CapiNative.AlgorithmId.Sha512);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0001F194 File Offset: 0x0001D394
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

		// Token: 0x060008FD RID: 2301 RVA: 0x0001F1CC File Offset: 0x0001D3CC
		public override void Initialize()
		{
			this.m_hashAlgorithm.Initialize();
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0001F1D9 File Offset: 0x0001D3D9
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.m_hashAlgorithm.HashCore(array, ibStart, cbSize);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0001F1E9 File Offset: 0x0001D3E9
		protected override byte[] HashFinal()
		{
			return this.m_hashAlgorithm.HashFinal();
		}

		// Token: 0x040006C2 RID: 1730
		private CapiHashAlgorithm m_hashAlgorithm;
	}
}
