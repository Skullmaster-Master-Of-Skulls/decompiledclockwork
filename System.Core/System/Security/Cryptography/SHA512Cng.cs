using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x02000117 RID: 279
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class SHA512Cng : SHA512
	{
		// Token: 0x060008F5 RID: 2293 RVA: 0x0001F0EE File Offset: 0x0001D2EE
		public SHA512Cng()
		{
			this.m_hashAlgorithm = new BCryptHashAlgorithm(CngAlgorithm.Sha512, "Microsoft Primitive Provider");
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0001F10C File Offset: 0x0001D30C
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

		// Token: 0x060008F7 RID: 2295 RVA: 0x0001F144 File Offset: 0x0001D344
		public override void Initialize()
		{
			this.m_hashAlgorithm.Initialize();
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0001F151 File Offset: 0x0001D351
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.m_hashAlgorithm.HashCore(array, ibStart, cbSize);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0001F161 File Offset: 0x0001D361
		protected override byte[] HashFinal()
		{
			return this.m_hashAlgorithm.HashFinal();
		}

		// Token: 0x040006C1 RID: 1729
		private BCryptHashAlgorithm m_hashAlgorithm;
	}
}
