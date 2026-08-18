using System;

namespace Renci.SshNet.Security.Cryptography
{
	// Token: 0x02000082 RID: 130
	public abstract class StreamCipher : SymmetricCipher
	{
		// Token: 0x060006F2 RID: 1778 RVA: 0x000158FC File Offset: 0x00013AFC
		protected StreamCipher(byte[] key) : base(key)
		{
		}
	}
}
