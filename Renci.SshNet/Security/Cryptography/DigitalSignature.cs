using System;

namespace Renci.SshNet.Security.Cryptography
{
	// Token: 0x0200007F RID: 127
	public abstract class DigitalSignature
	{
		// Token: 0x060006E4 RID: 1764
		public abstract bool Verify(byte[] input, byte[] signature);

		// Token: 0x060006E5 RID: 1765
		public abstract byte[] Sign(byte[] input);
	}
}
