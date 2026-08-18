using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x02000105 RID: 261
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class MD5Cng : MD5
	{
		// Token: 0x0600088A RID: 2186 RVA: 0x0001CF50 File Offset: 0x0001B150
		public MD5Cng()
		{
			if (CryptoConfig.AllowOnlyFipsAlgorithms && LocalAppContextSwitches.UseLegacyFipsThrow)
			{
				throw new InvalidOperationException(SR.GetString("Cryptography_NonCompliantFIPSAlgorithm"));
			}
			this.m_hashAlgorithm = new BCryptHashAlgorithm(CngAlgorithm.MD5, "Microsoft Primitive Provider");
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0001CF8C File Offset: 0x0001B18C
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

		// Token: 0x0600088C RID: 2188 RVA: 0x0001CFC4 File Offset: 0x0001B1C4
		public override void Initialize()
		{
			this.m_hashAlgorithm.Initialize();
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0001CFD1 File Offset: 0x0001B1D1
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.m_hashAlgorithm.HashCore(array, ibStart, cbSize);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0001CFE1 File Offset: 0x0001B1E1
		protected override byte[] HashFinal()
		{
			return this.m_hashAlgorithm.HashFinal();
		}

		// Token: 0x04000682 RID: 1666
		private BCryptHashAlgorithm m_hashAlgorithm;
	}
}
