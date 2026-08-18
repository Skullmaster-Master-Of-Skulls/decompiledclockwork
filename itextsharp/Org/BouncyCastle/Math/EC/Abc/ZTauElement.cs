using System;

namespace Org.BouncyCastle.Math.EC.Abc
{
	// Token: 0x02000118 RID: 280
	internal class ZTauElement
	{
		// Token: 0x06000A7C RID: 2684 RVA: 0x0003791B File Offset: 0x0003691B
		public ZTauElement(BigInteger u, BigInteger v)
		{
			this.u = u;
			this.v = v;
		}

		// Token: 0x0400086F RID: 2159
		public readonly BigInteger u;

		// Token: 0x04000870 RID: 2160
		public readonly BigInteger v;
	}
}
