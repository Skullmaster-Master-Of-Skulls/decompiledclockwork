using System;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x02000190 RID: 400
	public class Pkcs5S2ParametersGenerator : PbeParametersGenerator
	{
		// Token: 0x06000F8D RID: 3981 RVA: 0x00059564 File Offset: 0x00058564
		private void F(byte[] P, byte[] S, int c, byte[] iBuf, byte[] outBytes, int outOff)
		{
			byte[] array = new byte[this.hMac.GetMacSize()];
			ICipherParameters parameters = new KeyParameter(P);
			this.hMac.Init(parameters);
			if (S != null)
			{
				this.hMac.BlockUpdate(S, 0, S.Length);
			}
			this.hMac.BlockUpdate(iBuf, 0, iBuf.Length);
			this.hMac.DoFinal(array, 0);
			Array.Copy(array, 0, outBytes, outOff, array.Length);
			for (int num = 1; num != c; num++)
			{
				this.hMac.Init(parameters);
				this.hMac.BlockUpdate(array, 0, array.Length);
				this.hMac.DoFinal(array, 0);
				for (int num2 = 0; num2 != array.Length; num2++)
				{
					int num3 = outOff + num2;
					outBytes[num3] ^= array[num2];
				}
			}
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x00059635 File Offset: 0x00058635
		private void IntToOctet(byte[] Buffer, int i)
		{
			Buffer[0] = (byte)((uint)i >> 24);
			Buffer[1] = (byte)((uint)i >> 16);
			Buffer[2] = (byte)((uint)i >> 8);
			Buffer[3] = (byte)i;
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x00059654 File Offset: 0x00058654
		private byte[] GenerateDerivedKey(int dkLen)
		{
			int macSize = this.hMac.GetMacSize();
			int num = (dkLen + macSize - 1) / macSize;
			byte[] array = new byte[4];
			byte[] array2 = new byte[num * macSize];
			for (int i = 1; i <= num; i++)
			{
				this.IntToOctet(array, i);
				this.F(this.mPassword, this.mSalt, this.mIterationCount, array, array2, (i - 1) * macSize);
			}
			return array2;
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x000596BF File Offset: 0x000586BF
		[Obsolete("Use version with 'algorithm' parameter")]
		public override ICipherParameters GenerateDerivedParameters(int keySize)
		{
			return this.GenerateDerivedMacParameters(keySize);
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x000596C8 File Offset: 0x000586C8
		public override ICipherParameters GenerateDerivedParameters(string algorithm, int keySize)
		{
			keySize /= 8;
			byte[] keyBytes = this.GenerateDerivedKey(keySize);
			return ParameterUtilities.CreateKeyParameter(algorithm, keyBytes, 0, keySize);
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x000596EC File Offset: 0x000586EC
		[Obsolete("Use version with 'algorithm' parameter")]
		public override ICipherParameters GenerateDerivedParameters(int keySize, int ivSize)
		{
			keySize /= 8;
			ivSize /= 8;
			byte[] array = this.GenerateDerivedKey(keySize + ivSize);
			return new ParametersWithIV(new KeyParameter(array, 0, keySize), array, keySize, ivSize);
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x00059720 File Offset: 0x00058720
		public override ICipherParameters GenerateDerivedParameters(string algorithm, int keySize, int ivSize)
		{
			keySize /= 8;
			ivSize /= 8;
			byte[] array = this.GenerateDerivedKey(keySize + ivSize);
			KeyParameter parameters = ParameterUtilities.CreateKeyParameter(algorithm, array, 0, keySize);
			return new ParametersWithIV(parameters, array, keySize, ivSize);
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x00059754 File Offset: 0x00058754
		public override ICipherParameters GenerateDerivedMacParameters(int keySize)
		{
			keySize /= 8;
			byte[] key = this.GenerateDerivedKey(keySize);
			return new KeyParameter(key, 0, keySize);
		}

		// Token: 0x04000B40 RID: 2880
		private readonly IMac hMac = new HMac(new Sha1Digest());
	}
}
