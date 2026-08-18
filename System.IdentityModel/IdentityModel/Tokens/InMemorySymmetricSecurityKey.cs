using System;
using System.Security.Cryptography;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000183 RID: 387
	public class InMemorySymmetricSecurityKey : SymmetricSecurityKey
	{
		// Token: 0x06000CA2 RID: 3234 RVA: 0x0003B536 File Offset: 0x00039736
		public InMemorySymmetricSecurityKey(byte[] symmetricKey) : this(symmetricKey, true)
		{
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x0003B540 File Offset: 0x00039740
		public InMemorySymmetricSecurityKey(byte[] symmetricKey, bool cloneBuffer)
		{
			if (symmetricKey == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("symmetricKey"));
			}
			if (symmetricKey.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SymmetricKeyLengthTooShort", new object[]
				{
					symmetricKey.Length
				})));
			}
			this.keySize = symmetricKey.Length * 8;
			if (cloneBuffer)
			{
				this.symmetricKey = new byte[symmetricKey.Length];
				Buffer.BlockCopy(symmetricKey, 0, this.symmetricKey, 0, symmetricKey.Length);
				return;
			}
			this.symmetricKey = symmetricKey;
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x0003B5CF File Offset: 0x000397CF
		public override int KeySize
		{
			get
			{
				return this.keySize;
			}
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x0003B5D7 File Offset: 0x000397D7
		public override byte[] DecryptKey(string algorithm, byte[] keyData)
		{
			return CryptoHelper.UnwrapKey(this.symmetricKey, keyData, algorithm);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0003B5E6 File Offset: 0x000397E6
		public override byte[] EncryptKey(string algorithm, byte[] keyData)
		{
			return CryptoHelper.WrapKey(this.symmetricKey, keyData, algorithm);
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0003B5F5 File Offset: 0x000397F5
		public override byte[] GenerateDerivedKey(string algorithm, byte[] label, byte[] nonce, int derivedKeyLength, int offset)
		{
			return CryptoHelper.GenerateDerivedKey(this.symmetricKey, algorithm, label, nonce, derivedKeyLength, offset);
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0003B609 File Offset: 0x00039809
		public override ICryptoTransform GetDecryptionTransform(string algorithm, byte[] iv)
		{
			return CryptoHelper.CreateDecryptor(this.symmetricKey, iv, algorithm);
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0003B618 File Offset: 0x00039818
		public override ICryptoTransform GetEncryptionTransform(string algorithm, byte[] iv)
		{
			return CryptoHelper.CreateEncryptor(this.symmetricKey, iv, algorithm);
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0003B627 File Offset: 0x00039827
		public override int GetIVSize(string algorithm)
		{
			return CryptoHelper.GetIVSize(algorithm);
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0003B62F File Offset: 0x0003982F
		public override KeyedHashAlgorithm GetKeyedHashAlgorithm(string algorithm)
		{
			return CryptoHelper.CreateKeyedHashAlgorithm(this.symmetricKey, algorithm);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0003B63D File Offset: 0x0003983D
		public override SymmetricAlgorithm GetSymmetricAlgorithm(string algorithm)
		{
			return CryptoHelper.GetSymmetricAlgorithm(this.symmetricKey, algorithm);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0003B64C File Offset: 0x0003984C
		public override byte[] GetSymmetricKey()
		{
			byte[] array = new byte[this.symmetricKey.Length];
			Buffer.BlockCopy(this.symmetricKey, 0, array, 0, this.symmetricKey.Length);
			return array;
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0003B67E File Offset: 0x0003987E
		public override bool IsAsymmetricAlgorithm(string algorithm)
		{
			return CryptoHelper.IsAsymmetricAlgorithm(algorithm);
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0003B686 File Offset: 0x00039886
		public override bool IsSupportedAlgorithm(string algorithm)
		{
			return CryptoHelper.IsSymmetricSupportedAlgorithm(algorithm, this.KeySize);
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00023A60 File Offset: 0x00021C60
		public override bool IsSymmetricAlgorithm(string algorithm)
		{
			return CryptoHelper.IsSymmetricAlgorithm(algorithm);
		}

		// Token: 0x04000C8F RID: 3215
		private int keySize;

		// Token: 0x04000C90 RID: 3216
		private byte[] symmetricKey;
	}
}
