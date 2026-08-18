using System;
using System.Security.Cryptography;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000185 RID: 389
	public abstract class SymmetricSecurityKey : SecurityKey
	{
		// Token: 0x06000CBF RID: 3263
		public abstract byte[] GenerateDerivedKey(string algorithm, byte[] label, byte[] nonce, int derivedKeyLength, int offset);

		// Token: 0x06000CC0 RID: 3264
		public abstract ICryptoTransform GetDecryptionTransform(string algorithm, byte[] iv);

		// Token: 0x06000CC1 RID: 3265
		public abstract ICryptoTransform GetEncryptionTransform(string algorithm, byte[] iv);

		// Token: 0x06000CC2 RID: 3266
		public abstract int GetIVSize(string algorithm);

		// Token: 0x06000CC3 RID: 3267
		public abstract KeyedHashAlgorithm GetKeyedHashAlgorithm(string algorithm);

		// Token: 0x06000CC4 RID: 3268
		public abstract SymmetricAlgorithm GetSymmetricAlgorithm(string algorithm);

		// Token: 0x06000CC5 RID: 3269
		public abstract byte[] GetSymmetricKey();
	}
}
