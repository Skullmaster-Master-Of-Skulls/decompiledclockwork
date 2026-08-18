using System;

namespace Renci.SshNet.Security.Cryptography
{
	// Token: 0x02000083 RID: 131
	public abstract class SymmetricCipher : Cipher
	{
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x00015905 File Offset: 0x00013B05
		// (set) Token: 0x060006F4 RID: 1780 RVA: 0x0001590D File Offset: 0x00013B0D
		private protected byte[] Key { protected get; private set; }

		// Token: 0x060006F5 RID: 1781 RVA: 0x00015916 File Offset: 0x00013B16
		protected SymmetricCipher(byte[] key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.Key = key;
		}

		// Token: 0x060006F6 RID: 1782
		public abstract int EncryptBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset);

		// Token: 0x060006F7 RID: 1783
		public abstract int DecryptBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset);
	}
}
