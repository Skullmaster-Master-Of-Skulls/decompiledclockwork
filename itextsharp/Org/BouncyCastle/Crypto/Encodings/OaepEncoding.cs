using System;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Encodings
{
	// Token: 0x02000194 RID: 404
	public class OaepEncoding : IAsymmetricBlockCipher
	{
		// Token: 0x06000FBB RID: 4027 RVA: 0x0005B50F File Offset: 0x0005A50F
		public OaepEncoding(IAsymmetricBlockCipher cipher) : this(cipher, new Sha1Digest(), null)
		{
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0005B51E File Offset: 0x0005A51E
		public OaepEncoding(IAsymmetricBlockCipher cipher, IDigest hash) : this(cipher, hash, null)
		{
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x0005B529 File Offset: 0x0005A529
		public OaepEncoding(IAsymmetricBlockCipher cipher, IDigest hash, byte[] encodingParams) : this(cipher, hash, hash, encodingParams)
		{
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x0005B538 File Offset: 0x0005A538
		public OaepEncoding(IAsymmetricBlockCipher cipher, IDigest hash, IDigest mgf1Hash, byte[] encodingParams)
		{
			this.engine = cipher;
			this.hash = hash;
			this.mgf1Hash = mgf1Hash;
			this.defHash = new byte[hash.GetDigestSize()];
			if (encodingParams != null)
			{
				hash.BlockUpdate(encodingParams, 0, encodingParams.Length);
			}
			hash.DoFinal(this.defHash, 0);
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x0005B590 File Offset: 0x0005A590
		public IAsymmetricBlockCipher GetUnderlyingCipher()
		{
			return this.engine;
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x0005B598 File Offset: 0x0005A598
		public string AlgorithmName
		{
			get
			{
				return this.engine.AlgorithmName + "/OAEPPadding";
			}
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0005B5B0 File Offset: 0x0005A5B0
		public void Init(bool forEncryption, ICipherParameters param)
		{
			if (param is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)param;
				this.random = parametersWithRandom.Random;
			}
			else
			{
				this.random = new SecureRandom();
			}
			this.engine.Init(forEncryption, param);
			this.forEncryption = forEncryption;
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0005B5FC File Offset: 0x0005A5FC
		public int GetInputBlockSize()
		{
			int inputBlockSize = this.engine.GetInputBlockSize();
			if (this.forEncryption)
			{
				return inputBlockSize - 1 - 2 * this.defHash.Length;
			}
			return inputBlockSize;
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0005B630 File Offset: 0x0005A630
		public int GetOutputBlockSize()
		{
			int outputBlockSize = this.engine.GetOutputBlockSize();
			if (this.forEncryption)
			{
				return outputBlockSize;
			}
			return outputBlockSize - 1 - 2 * this.defHash.Length;
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0005B661 File Offset: 0x0005A661
		public byte[] ProcessBlock(byte[] inBytes, int inOff, int inLen)
		{
			if (this.forEncryption)
			{
				return this.encodeBlock(inBytes, inOff, inLen);
			}
			return this.decodeBlock(inBytes, inOff, inLen);
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x0005B680 File Offset: 0x0005A680
		private byte[] encodeBlock(byte[] inBytes, int inOff, int inLen)
		{
			byte[] array = new byte[this.GetInputBlockSize() + 1 + 2 * this.defHash.Length];
			Array.Copy(inBytes, inOff, array, array.Length - inLen, inLen);
			array[array.Length - inLen - 1] = 1;
			Array.Copy(this.defHash, 0, array, this.defHash.Length, this.defHash.Length);
			byte[] array2 = this.random.GenerateSeed(this.defHash.Length);
			byte[] array3 = this.maskGeneratorFunction1(array2, 0, array2.Length, array.Length - this.defHash.Length);
			for (int num = this.defHash.Length; num != array.Length; num++)
			{
				byte[] array4 = array;
				int num2 = num;
				array4[num2] ^= array3[num - this.defHash.Length];
			}
			Array.Copy(array2, 0, array, 0, this.defHash.Length);
			array3 = this.maskGeneratorFunction1(array, this.defHash.Length, array.Length - this.defHash.Length, this.defHash.Length);
			for (int num3 = 0; num3 != this.defHash.Length; num3++)
			{
				byte[] array5 = array;
				int num4 = num3;
				array5[num4] ^= array3[num3];
			}
			return this.engine.ProcessBlock(array, 0, array.Length);
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0005B7B4 File Offset: 0x0005A7B4
		private byte[] decodeBlock(byte[] inBytes, int inOff, int inLen)
		{
			byte[] array = this.engine.ProcessBlock(inBytes, inOff, inLen);
			byte[] array2;
			if (array.Length < this.engine.GetOutputBlockSize())
			{
				array2 = new byte[this.engine.GetOutputBlockSize()];
				Array.Copy(array, 0, array2, array2.Length - array.Length, array.Length);
			}
			else
			{
				array2 = array;
			}
			if (array2.Length < 2 * this.defHash.Length + 1)
			{
				throw new InvalidCipherTextException("data too short");
			}
			byte[] array3 = this.maskGeneratorFunction1(array2, this.defHash.Length, array2.Length - this.defHash.Length, this.defHash.Length);
			for (int num = 0; num != this.defHash.Length; num++)
			{
				byte[] array4 = array2;
				int num2 = num;
				array4[num2] ^= array3[num];
			}
			array3 = this.maskGeneratorFunction1(array2, 0, this.defHash.Length, array2.Length - this.defHash.Length);
			for (int num3 = this.defHash.Length; num3 != array2.Length; num3++)
			{
				byte[] array5 = array2;
				int num4 = num3;
				array5[num4] ^= array3[num3 - this.defHash.Length];
			}
			for (int num5 = 0; num5 != this.defHash.Length; num5++)
			{
				if (this.defHash[num5] != array2[this.defHash.Length + num5])
				{
					throw new InvalidCipherTextException("data hash wrong");
				}
			}
			int num6 = 2 * this.defHash.Length;
			while (num6 != array2.Length && array2[num6] == 0)
			{
				num6++;
			}
			if (num6 >= array2.Length - 1 || array2[num6] != 1)
			{
				throw new InvalidCipherTextException("data start wrong " + num6);
			}
			num6++;
			byte[] array6 = new byte[array2.Length - num6];
			Array.Copy(array2, num6, array6, 0, array6.Length);
			return array6;
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0005B96F File Offset: 0x0005A96F
		private void ItoOSP(int i, byte[] sp)
		{
			sp[0] = (byte)((uint)i >> 24);
			sp[1] = (byte)((uint)i >> 16);
			sp[2] = (byte)((uint)i >> 8);
			sp[3] = (byte)i;
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0005B990 File Offset: 0x0005A990
		private byte[] maskGeneratorFunction1(byte[] Z, int zOff, int zLen, int length)
		{
			byte[] array = new byte[length];
			byte[] array2 = new byte[this.mgf1Hash.GetDigestSize()];
			byte[] array3 = new byte[4];
			int num = 0;
			this.hash.Reset();
			do
			{
				this.ItoOSP(num, array3);
				this.mgf1Hash.BlockUpdate(Z, zOff, zLen);
				this.mgf1Hash.BlockUpdate(array3, 0, array3.Length);
				this.mgf1Hash.DoFinal(array2, 0);
				Array.Copy(array2, 0, array, num * array2.Length, array2.Length);
			}
			while (++num < length / array2.Length);
			if (num * array2.Length < length)
			{
				this.ItoOSP(num, array3);
				this.mgf1Hash.BlockUpdate(Z, zOff, zLen);
				this.mgf1Hash.BlockUpdate(array3, 0, array3.Length);
				this.mgf1Hash.DoFinal(array2, 0);
				Array.Copy(array2, 0, array, num * array2.Length, array.Length - num * array2.Length);
			}
			return array;
		}

		// Token: 0x04000B5C RID: 2908
		private byte[] defHash;

		// Token: 0x04000B5D RID: 2909
		private IDigest hash;

		// Token: 0x04000B5E RID: 2910
		private IDigest mgf1Hash;

		// Token: 0x04000B5F RID: 2911
		private IAsymmetricBlockCipher engine;

		// Token: 0x04000B60 RID: 2912
		private SecureRandom random;

		// Token: 0x04000B61 RID: 2913
		private bool forEncryption;
	}
}
