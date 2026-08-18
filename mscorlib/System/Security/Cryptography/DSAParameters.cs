using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200087B RID: 2171
	[ComVisible(true)]
	[Serializable]
	public struct DSAParameters
	{
		// Token: 0x040028E0 RID: 10464
		public byte[] P;

		// Token: 0x040028E1 RID: 10465
		public byte[] Q;

		// Token: 0x040028E2 RID: 10466
		public byte[] G;

		// Token: 0x040028E3 RID: 10467
		public byte[] Y;

		// Token: 0x040028E4 RID: 10468
		public byte[] J;

		// Token: 0x040028E5 RID: 10469
		[NonSerialized]
		public byte[] X;

		// Token: 0x040028E6 RID: 10470
		public byte[] Seed;

		// Token: 0x040028E7 RID: 10471
		public int Counter;
	}
}
