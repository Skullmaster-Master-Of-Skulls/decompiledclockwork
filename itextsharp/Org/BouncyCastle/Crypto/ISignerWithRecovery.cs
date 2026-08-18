using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x0200018A RID: 394
	public interface ISignerWithRecovery : ISigner
	{
		// Token: 0x06000F4F RID: 3919
		bool HasFullMessage();

		// Token: 0x06000F50 RID: 3920
		byte[] GetRecoveredMessage();
	}
}
