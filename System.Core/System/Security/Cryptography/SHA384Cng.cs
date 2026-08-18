using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x02000114 RID: 276
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class SHA384Cng : SHA384
	{
		// Token: 0x060008EA RID: 2282 RVA: 0x0001EFE6 File Offset: 0x0001D1E6
		public SHA384Cng()
		{
			this.m_hashAlgorithm = new BCryptHashAlgorithm(CngAlgorithm.Sha384, "Microsoft Primitive Provider");
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0001F004 File Offset: 0x0001D204
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

		// Token: 0x060008EC RID: 2284 RVA: 0x0001F03C File Offset: 0x0001D23C
		public override void Initialize()
		{
			this.m_hashAlgorithm.Initialize();
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0001F049 File Offset: 0x0001D249
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.m_hashAlgorithm.HashCore(array, ibStart, cbSize);
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0001F059 File Offset: 0x0001D259
		protected override byte[] HashFinal()
		{
			return this.m_hashAlgorithm.HashFinal();
		}

		// Token: 0x040006BF RID: 1727
		private BCryptHashAlgorithm m_hashAlgorithm;
	}
}
