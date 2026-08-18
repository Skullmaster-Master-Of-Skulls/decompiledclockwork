using System;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003D3 RID: 979
	public class OpenSslPbeParametersGenerator : PbeParametersGenerator
	{
		// Token: 0x0600220D RID: 8717 RVA: 0x000CE0C9 File Offset: 0x000CD0C9
		public override void Init(byte[] password, byte[] salt, int iterationCount)
		{
			base.Init(password, salt, 1);
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x000CE0D4 File Offset: 0x000CD0D4
		public virtual void Init(byte[] password, byte[] salt)
		{
			base.Init(password, salt, 1);
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x000CE0E0 File Offset: 0x000CD0E0
		private byte[] GenerateDerivedKey(int bytesNeeded)
		{
			byte[] array = new byte[this.digest.GetDigestSize()];
			byte[] array2 = new byte[bytesNeeded];
			int num = 0;
			for (;;)
			{
				this.digest.BlockUpdate(this.mPassword, 0, this.mPassword.Length);
				this.digest.BlockUpdate(this.mSalt, 0, this.mSalt.Length);
				this.digest.DoFinal(array, 0);
				int num2 = (bytesNeeded > array.Length) ? array.Length : bytesNeeded;
				Array.Copy(array, 0, array2, num, num2);
				num += num2;
				bytesNeeded -= num2;
				if (bytesNeeded == 0)
				{
					break;
				}
				this.digest.Reset();
				this.digest.BlockUpdate(array, 0, array.Length);
			}
			return array2;
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x000CE18D File Offset: 0x000CD18D
		[Obsolete("Use version with 'algorithm' parameter")]
		public override ICipherParameters GenerateDerivedParameters(int keySize)
		{
			return this.GenerateDerivedMacParameters(keySize);
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x000CE198 File Offset: 0x000CD198
		public override ICipherParameters GenerateDerivedParameters(string algorithm, int keySize)
		{
			keySize /= 8;
			byte[] keyBytes = this.GenerateDerivedKey(keySize);
			return ParameterUtilities.CreateKeyParameter(algorithm, keyBytes, 0, keySize);
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x000CE1BC File Offset: 0x000CD1BC
		[Obsolete("Use version with 'algorithm' parameter")]
		public override ICipherParameters GenerateDerivedParameters(int keySize, int ivSize)
		{
			keySize /= 8;
			ivSize /= 8;
			byte[] array = this.GenerateDerivedKey(keySize + ivSize);
			return new ParametersWithIV(new KeyParameter(array, 0, keySize), array, keySize, ivSize);
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x000CE1F0 File Offset: 0x000CD1F0
		public override ICipherParameters GenerateDerivedParameters(string algorithm, int keySize, int ivSize)
		{
			keySize /= 8;
			ivSize /= 8;
			byte[] array = this.GenerateDerivedKey(keySize + ivSize);
			KeyParameter parameters = ParameterUtilities.CreateKeyParameter(algorithm, array, 0, keySize);
			return new ParametersWithIV(parameters, array, keySize, ivSize);
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x000CE224 File Offset: 0x000CD224
		public override ICipherParameters GenerateDerivedMacParameters(int keySize)
		{
			keySize /= 8;
			byte[] key = this.GenerateDerivedKey(keySize);
			return new KeyParameter(key, 0, keySize);
		}

		// Token: 0x04001757 RID: 5975
		private readonly IDigest digest = new MD5Digest();
	}
}
