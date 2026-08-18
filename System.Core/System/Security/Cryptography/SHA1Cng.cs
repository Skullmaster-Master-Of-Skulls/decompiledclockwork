using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x02000110 RID: 272
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class SHA1Cng : SHA1
	{
		// Token: 0x060008DA RID: 2266 RVA: 0x0001EE5D File Offset: 0x0001D05D
		public SHA1Cng()
		{
			this.m_hashAlgorithm = new BCryptHashAlgorithm(CngAlgorithm.Sha1, "Microsoft Primitive Provider");
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0001EE7C File Offset: 0x0001D07C
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

		// Token: 0x060008DC RID: 2268 RVA: 0x0001EEB4 File Offset: 0x0001D0B4
		public override void Initialize()
		{
			this.m_hashAlgorithm.Initialize();
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0001EEC1 File Offset: 0x0001D0C1
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.m_hashAlgorithm.HashCore(array, ibStart, cbSize);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0001EED1 File Offset: 0x0001D0D1
		protected override byte[] HashFinal()
		{
			return this.m_hashAlgorithm.HashFinal();
		}

		// Token: 0x040006BC RID: 1724
		private BCryptHashAlgorithm m_hashAlgorithm;
	}
}
