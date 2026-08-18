using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x02000111 RID: 273
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class SHA256Cng : SHA256
	{
		// Token: 0x060008DF RID: 2271 RVA: 0x0001EEDE File Offset: 0x0001D0DE
		public SHA256Cng()
		{
			this.m_hashAlgorithm = new BCryptHashAlgorithm(CngAlgorithm.Sha256, "Microsoft Primitive Provider");
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0001EEFC File Offset: 0x0001D0FC
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

		// Token: 0x060008E1 RID: 2273 RVA: 0x0001EF34 File Offset: 0x0001D134
		public override void Initialize()
		{
			this.m_hashAlgorithm.Initialize();
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0001EF41 File Offset: 0x0001D141
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.m_hashAlgorithm.HashCore(array, ibStart, cbSize);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0001EF51 File Offset: 0x0001D151
		protected override byte[] HashFinal()
		{
			return this.m_hashAlgorithm.HashFinal();
		}

		// Token: 0x040006BD RID: 1725
		private BCryptHashAlgorithm m_hashAlgorithm;
	}
}
