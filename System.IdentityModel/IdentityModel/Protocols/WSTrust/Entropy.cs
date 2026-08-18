using System;
using System.IdentityModel.Tokens;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x020001F3 RID: 499
	public class Entropy : ProtectedKey
	{
		// Token: 0x0600109F RID: 4255 RVA: 0x0004728E File Offset: 0x0004548E
		public Entropy(int entropySizeInBits) : this(CryptoHelper.GenerateRandomBytes(entropySizeInBits))
		{
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x0004729C File Offset: 0x0004549C
		public Entropy(byte[] secret) : base(secret)
		{
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x000472A5 File Offset: 0x000454A5
		public Entropy(byte[] secret, EncryptingCredentials wrappingCredentials) : base(secret, wrappingCredentials)
		{
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x000472AF File Offset: 0x000454AF
		public Entropy(ProtectedKey protectedKey) : base(Entropy.GetKeyBytesFromProtectedKey(protectedKey), Entropy.GetWrappingCredentialsFromProtectedKey(protectedKey))
		{
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x000472C3 File Offset: 0x000454C3
		private static byte[] GetKeyBytesFromProtectedKey(ProtectedKey protectedKey)
		{
			if (protectedKey == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("protectedKey");
			}
			return protectedKey.GetKeyBytes();
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x000472DE File Offset: 0x000454DE
		private static EncryptingCredentials GetWrappingCredentialsFromProtectedKey(ProtectedKey protectedKey)
		{
			if (protectedKey == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("protectedKey");
			}
			return protectedKey.WrappingCredentials;
		}
	}
}
