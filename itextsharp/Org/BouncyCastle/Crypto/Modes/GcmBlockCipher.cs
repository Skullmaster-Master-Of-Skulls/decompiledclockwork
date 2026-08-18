using System;
using Org.BouncyCastle.Crypto.Modes.Gcm;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x0200018E RID: 398
	public class GcmBlockCipher : IAeadBlockCipher
	{
		// Token: 0x06000F68 RID: 3944 RVA: 0x00058CFF File Offset: 0x00057CFF
		public GcmBlockCipher(IBlockCipher c) : this(c, null)
		{
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x00058D0C File Offset: 0x00057D0C
		public GcmBlockCipher(IBlockCipher c, IGcmMultiplier m)
		{
			if (c.GetBlockSize() != 16)
			{
				throw new ArgumentException("cipher required with a block size of " + 16 + ".");
			}
			if (m == null)
			{
				m = new Tables8kGcmMultiplier();
			}
			this.cipher = c;
			this.multiplier = m;
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x00058D5D File Offset: 0x00057D5D
		public virtual string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/GCM";
			}
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x00058D74 File Offset: 0x00057D74
		public virtual int GetBlockSize()
		{
			return 16;
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x00058D78 File Offset: 0x00057D78
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.forEncryption = forEncryption;
			this.macBlock = null;
			if (parameters is AeadParameters)
			{
				AeadParameters aeadParameters = (AeadParameters)parameters;
				this.nonce = aeadParameters.GetNonce();
				this.A = aeadParameters.GetAssociatedText();
				int num = aeadParameters.MacSize;
				if (num < 96 || num > 128 || num % 8 != 0)
				{
					throw new ArgumentException("Invalid value for MAC size: " + num);
				}
				this.macSize = num / 8;
				this.keyParam = aeadParameters.Key;
			}
			else
			{
				if (!(parameters is ParametersWithIV))
				{
					throw new ArgumentException("invalid parameters passed to GCM");
				}
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				this.nonce = parametersWithIV.GetIV();
				this.A = null;
				this.macSize = 16;
				this.keyParam = (KeyParameter)parametersWithIV.Parameters;
			}
			int num2 = forEncryption ? 16 : (16 + this.macSize);
			this.bufBlock = new byte[num2];
			if (this.nonce == null || this.nonce.Length < 1)
			{
				throw new ArgumentException("IV must be at least 1 byte");
			}
			if (this.A == null)
			{
				this.A = new byte[0];
			}
			this.cipher.Init(true, this.keyParam);
			this.H = new byte[16];
			this.cipher.ProcessBlock(this.H, 0, this.H, 0);
			this.multiplier.Init(this.H);
			this.initS = this.gHASH(this.A);
			if (this.nonce.Length == 12)
			{
				this.J0 = new byte[16];
				Array.Copy(this.nonce, 0, this.J0, 0, this.nonce.Length);
				this.J0[15] = 1;
			}
			else
			{
				this.J0 = this.gHASH(this.nonce);
				byte[] array = new byte[16];
				GcmBlockCipher.packLength((ulong)((long)this.nonce.Length * 8L), array, 8);
				GcmUtilities.Xor(this.J0, array);
				this.multiplier.MultiplyH(this.J0);
			}
			this.S = Arrays.Clone(this.initS);
			this.counter = Arrays.Clone(this.J0);
			this.bufOff = 0;
			this.totalLength = 0UL;
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x00058FB1 File Offset: 0x00057FB1
		public virtual byte[] GetMac()
		{
			return Arrays.Clone(this.macBlock);
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x00058FBE File Offset: 0x00057FBE
		public virtual int GetOutputSize(int len)
		{
			if (this.forEncryption)
			{
				return len + this.bufOff + this.macSize;
			}
			return len + this.bufOff - this.macSize;
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x00058FE7 File Offset: 0x00057FE7
		public virtual int GetUpdateOutputSize(int len)
		{
			return (len + this.bufOff) / 16 * 16;
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x00058FF7 File Offset: 0x00057FF7
		public virtual int ProcessByte(byte input, byte[] output, int outOff)
		{
			return this.Process(input, output, outOff);
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x00059004 File Offset: 0x00058004
		public virtual int ProcessBytes(byte[] input, int inOff, int len, byte[] output, int outOff)
		{
			int num = 0;
			for (int num2 = 0; num2 != len; num2++)
			{
				this.bufBlock[this.bufOff++] = input[inOff + num2];
				if (this.bufOff == this.bufBlock.Length)
				{
					this.gCTRBlock(this.bufBlock, 16, output, outOff + num);
					if (!this.forEncryption)
					{
						Array.Copy(this.bufBlock, 16, this.bufBlock, 0, this.macSize);
					}
					this.bufOff = this.bufBlock.Length - 16;
					num += 16;
				}
			}
			return num;
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x0005909C File Offset: 0x0005809C
		private int Process(byte input, byte[] output, int outOff)
		{
			this.bufBlock[this.bufOff++] = input;
			if (this.bufOff == this.bufBlock.Length)
			{
				this.gCTRBlock(this.bufBlock, 16, output, outOff);
				if (!this.forEncryption)
				{
					Array.Copy(this.bufBlock, 16, this.bufBlock, 0, this.macSize);
				}
				this.bufOff = this.bufBlock.Length - 16;
				return 16;
			}
			return 0;
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x0005911C File Offset: 0x0005811C
		public int DoFinal(byte[] output, int outOff)
		{
			int num = this.bufOff;
			if (!this.forEncryption)
			{
				if (num < this.macSize)
				{
					throw new InvalidCipherTextException("data too short");
				}
				num -= this.macSize;
			}
			if (num > 0)
			{
				byte[] array = new byte[16];
				Array.Copy(this.bufBlock, 0, array, 0, num);
				this.gCTRBlock(array, num, output, outOff);
			}
			byte[] array2 = new byte[16];
			GcmBlockCipher.packLength((ulong)((long)this.A.Length * 8L), array2, 0);
			GcmBlockCipher.packLength(this.totalLength * 8UL, array2, 8);
			GcmUtilities.Xor(this.S, array2);
			this.multiplier.MultiplyH(this.S);
			byte[] array3 = new byte[16];
			this.cipher.ProcessBlock(this.J0, 0, array3, 0);
			GcmUtilities.Xor(array3, this.S);
			int num2 = num;
			this.macBlock = new byte[this.macSize];
			Array.Copy(array3, 0, this.macBlock, 0, this.macSize);
			if (this.forEncryption)
			{
				Array.Copy(this.macBlock, 0, output, outOff + this.bufOff, this.macSize);
				num2 += this.macSize;
			}
			else
			{
				byte[] array4 = new byte[this.macSize];
				Array.Copy(this.bufBlock, num, array4, 0, this.macSize);
				if (!Arrays.ConstantTimeAreEqual(this.macBlock, array4))
				{
					throw new InvalidCipherTextException("mac check in GCM failed");
				}
			}
			this.Reset(false);
			return num2;
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x00059288 File Offset: 0x00058288
		public virtual void Reset()
		{
			this.Reset(true);
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x00059294 File Offset: 0x00058294
		private void Reset(bool clearMac)
		{
			this.S = Arrays.Clone(this.initS);
			this.counter = Arrays.Clone(this.J0);
			this.bufOff = 0;
			this.totalLength = 0UL;
			if (this.bufBlock != null)
			{
				Array.Clear(this.bufBlock, 0, this.bufBlock.Length);
			}
			if (clearMac)
			{
				this.macBlock = null;
			}
			this.cipher.Reset();
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x00059304 File Offset: 0x00058304
		private void gCTRBlock(byte[] buf, int bufCount, byte[] output, int outOff)
		{
			for (int i = 15; i >= 12; i--)
			{
				byte[] array = this.counter;
				int num = i;
				if ((array[num] += 1) != 0)
				{
					break;
				}
			}
			byte[] array2 = new byte[16];
			this.cipher.ProcessBlock(this.counter, 0, array2, 0);
			byte[] val;
			if (this.forEncryption)
			{
				Array.Copy(GcmBlockCipher.Zeroes, bufCount, array2, bufCount, 16 - bufCount);
				val = array2;
			}
			else
			{
				val = buf;
			}
			for (int j = bufCount - 1; j >= 0; j--)
			{
				byte[] array3 = array2;
				int num2 = j;
				array3[num2] ^= buf[j];
				output[outOff + j] = array2[j];
			}
			GcmUtilities.Xor(this.S, val);
			this.multiplier.MultiplyH(this.S);
			this.totalLength += (ulong)((long)bufCount);
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x000593D8 File Offset: 0x000583D8
		private byte[] gHASH(byte[] b)
		{
			byte[] array = new byte[16];
			for (int i = 0; i < b.Length; i += 16)
			{
				byte[] array2 = new byte[16];
				int length = Math.Min(b.Length - i, 16);
				Array.Copy(b, i, array2, 0, length);
				GcmUtilities.Xor(array, array2);
				this.multiplier.MultiplyH(array);
			}
			return array;
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0005942F File Offset: 0x0005842F
		private static void packLength(ulong len, byte[] bs, int off)
		{
			Pack.UInt32_To_BE((uint)(len >> 32), bs, off);
			Pack.UInt32_To_BE((uint)len, bs, off + 4);
		}

		// Token: 0x04000B2B RID: 2859
		private const int BlockSize = 16;

		// Token: 0x04000B2C RID: 2860
		private static readonly byte[] Zeroes = new byte[16];

		// Token: 0x04000B2D RID: 2861
		private readonly IBlockCipher cipher;

		// Token: 0x04000B2E RID: 2862
		private readonly IGcmMultiplier multiplier;

		// Token: 0x04000B2F RID: 2863
		private bool forEncryption;

		// Token: 0x04000B30 RID: 2864
		private int macSize;

		// Token: 0x04000B31 RID: 2865
		private byte[] nonce;

		// Token: 0x04000B32 RID: 2866
		private byte[] A;

		// Token: 0x04000B33 RID: 2867
		private KeyParameter keyParam;

		// Token: 0x04000B34 RID: 2868
		private byte[] H;

		// Token: 0x04000B35 RID: 2869
		private byte[] initS;

		// Token: 0x04000B36 RID: 2870
		private byte[] J0;

		// Token: 0x04000B37 RID: 2871
		private byte[] bufBlock;

		// Token: 0x04000B38 RID: 2872
		private byte[] macBlock;

		// Token: 0x04000B39 RID: 2873
		private byte[] S;

		// Token: 0x04000B3A RID: 2874
		private byte[] counter;

		// Token: 0x04000B3B RID: 2875
		private int bufOff;

		// Token: 0x04000B3C RID: 2876
		private ulong totalLength;
	}
}
