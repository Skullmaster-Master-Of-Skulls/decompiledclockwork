using System;

namespace Org.BouncyCastle.Crypto.Modes.Gcm
{
	// Token: 0x02000125 RID: 293
	public interface IGcmMultiplier
	{
		// Token: 0x06000AC3 RID: 2755
		void Init(byte[] H);

		// Token: 0x06000AC4 RID: 2756
		void MultiplyH(byte[] x);
	}
}
