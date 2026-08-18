using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020004C3 RID: 1219
	public class Pkcs5S1ParametersGenerator : PbeParametersGenerator
	{
		// Token: 0x06002990 RID: 10640 RVA: 0x000FCDED File Offset: 0x000FBDED
		public Pkcs5S1ParametersGenerator(IDigest digest)
		{
			this.digest = digest;
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x000FCDFC File Offset: 0x000FBDFC
		private byte[] GenerateDerivedKey()
		{
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.BlockUpdate(this.mPassword, 0, this.mPassword.Length);
			this.digest.BlockUpdate(this.mSalt, 0, this.mSalt.Length);
			this.digest.DoFinal(array, 0);
			for (int i = 1; i < this.mIterationCount; i++)
			{
				this.digest.BlockUpdate(array, 0, array.Length);
				this.digest.DoFinal(array, 0);
			}
			return array;
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x000FCE8C File Offset: 0x000FBE8C
		[Obsolete("Use version with 'algorithm' parameter")]
		public override ICipherParameters GenerateDerivedParameters(int keySize)
		{
			return this.GenerateDerivedMacParameters(keySize);
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x000FCE98 File Offset: 0x000FBE98
		public override ICipherParameters GenerateDerivedParameters(string algorithm, int keySize)
		{
			keySize /= 8;
			if (keySize > this.digest.GetDigestSize())
			{
				throw new ArgumentException("Can't Generate a derived key " + keySize + " bytes long.");
			}
			byte[] keyBytes = this.GenerateDerivedKey();
			return ParameterUtilities.CreateKeyParameter(algorithm, keyBytes, 0, keySize);
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x000FCEE4 File Offset: 0x000FBEE4
		[Obsolete("Use version with 'algorithm' parameter")]
		public override ICipherParameters GenerateDerivedParameters(int keySize, int ivSize)
		{
			keySize /= 8;
			ivSize /= 8;
			if (keySize + ivSize > this.digest.GetDigestSize())
			{
				throw new ArgumentException("Can't Generate a derived key " + (keySize + ivSize) + " bytes long.");
			}
			byte[] array = this.GenerateDerivedKey();
			return new ParametersWithIV(new KeyParameter(array, 0, keySize), array, keySize, ivSize);
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x000FCF40 File Offset: 0x000FBF40
		public override ICipherParameters GenerateDerivedParameters(string algorithm, int keySize, int ivSize)
		{
			keySize /= 8;
			ivSize /= 8;
			if (keySize + ivSize > this.digest.GetDigestSize())
			{
				throw new ArgumentException("Can't Generate a derived key " + (keySize + ivSize) + " bytes long.");
			}
			byte[] array = this.GenerateDerivedKey();
			KeyParameter parameters = ParameterUtilities.CreateKeyParameter(algorithm, array, 0, keySize);
			return new ParametersWithIV(parameters, array, keySize, ivSize);
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x000FCFA0 File Offset: 0x000FBFA0
		public override ICipherParameters GenerateDerivedMacParameters(int keySize)
		{
			keySize /= 8;
			if (keySize > this.digest.GetDigestSize())
			{
				throw new ArgumentException("Can't Generate a derived key " + keySize + " bytes long.");
			}
			byte[] key = this.GenerateDerivedKey();
			return new KeyParameter(key, 0, keySize);
		}

		// Token: 0x04001D02 RID: 7426
		private readonly IDigest digest;
	}
}
