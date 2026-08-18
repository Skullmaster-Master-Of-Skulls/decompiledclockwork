using System;
using System.IO;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x0200034B RID: 843
	public class CcmBlockCipher : IAeadBlockCipher
	{
		// Token: 0x06001E5C RID: 7772 RVA: 0x000B5AA8 File Offset: 0x000B4AA8
		public CcmBlockCipher(IBlockCipher cipher)
		{
			this.cipher = cipher;
			this.macBlock = new byte[CcmBlockCipher.BlockSize];
			if (cipher.GetBlockSize() != CcmBlockCipher.BlockSize)
			{
				throw new ArgumentException("cipher required with a block size of " + CcmBlockCipher.BlockSize + ".");
			}
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x000B5B09 File Offset: 0x000B4B09
		public virtual IBlockCipher GetUnderlyingCipher()
		{
			return this.cipher;
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x000B5B14 File Offset: 0x000B4B14
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.forEncryption = forEncryption;
			if (parameters is AeadParameters)
			{
				AeadParameters aeadParameters = (AeadParameters)parameters;
				this.nonce = aeadParameters.GetNonce();
				this.associatedText = aeadParameters.GetAssociatedText();
				this.macSize = aeadParameters.MacSize / 8;
				this.keyParam = aeadParameters.Key;
				return;
			}
			if (parameters is ParametersWithIV)
			{
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				this.nonce = parametersWithIV.GetIV();
				this.associatedText = null;
				this.macSize = this.macBlock.Length / 2;
				this.keyParam = parametersWithIV.Parameters;
				return;
			}
			throw new ArgumentException("invalid parameters passed to CCM");
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001E5F RID: 7775 RVA: 0x000B5BB3 File Offset: 0x000B4BB3
		public virtual string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/CCM";
			}
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x000B5BCA File Offset: 0x000B4BCA
		public virtual int GetBlockSize()
		{
			return this.cipher.GetBlockSize();
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x000B5BD7 File Offset: 0x000B4BD7
		public virtual int ProcessByte(byte input, byte[] outBytes, int outOff)
		{
			this.data.WriteByte(input);
			return 0;
		}

		// Token: 0x06001E62 RID: 7778 RVA: 0x000B5BE6 File Offset: 0x000B4BE6
		public virtual int ProcessBytes(byte[] inBytes, int inOff, int inLen, byte[] outBytes, int outOff)
		{
			this.data.Write(inBytes, inOff, inLen);
			return 0;
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x000B5BF8 File Offset: 0x000B4BF8
		public virtual int DoFinal(byte[] outBytes, int outOff)
		{
			byte[] array = this.data.ToArray();
			byte[] array2 = this.ProcessPacket(array, 0, array.Length);
			Array.Copy(array2, 0, outBytes, outOff, array2.Length);
			this.Reset();
			return array2.Length;
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x000B5C32 File Offset: 0x000B4C32
		public virtual void Reset()
		{
			this.cipher.Reset();
			this.data.SetLength(0L);
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x000B5C4C File Offset: 0x000B4C4C
		public virtual byte[] GetMac()
		{
			byte[] array = new byte[this.macSize];
			Array.Copy(this.macBlock, 0, array, 0, array.Length);
			return array;
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x000B5C77 File Offset: 0x000B4C77
		public virtual int GetUpdateOutputSize(int len)
		{
			return 0;
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x000B5C7A File Offset: 0x000B4C7A
		public int GetOutputSize(int len)
		{
			if (this.forEncryption)
			{
				return (int)this.data.Length + len + this.macSize;
			}
			return (int)this.data.Length + len - this.macSize;
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x000B5CB0 File Offset: 0x000B4CB0
		public byte[] ProcessPacket(byte[] input, int inOff, int inLen)
		{
			if (this.keyParam == null)
			{
				throw new InvalidOperationException("CCM cipher unitialized.");
			}
			IBlockCipher blockCipher = new SicBlockCipher(this.cipher);
			byte[] array = new byte[CcmBlockCipher.BlockSize];
			array[0] = (byte)(15 - this.nonce.Length - 1 & 7);
			Array.Copy(this.nonce, 0, array, 1, this.nonce.Length);
			blockCipher.Init(this.forEncryption, new ParametersWithIV(this.keyParam, array));
			byte[] array2;
			if (this.forEncryption)
			{
				int i = inOff;
				int num = 0;
				array2 = new byte[inLen + this.macSize];
				this.calculateMac(input, inOff, inLen, this.macBlock);
				blockCipher.ProcessBlock(this.macBlock, 0, this.macBlock, 0);
				while (i < inLen - CcmBlockCipher.BlockSize)
				{
					blockCipher.ProcessBlock(input, i, array2, num);
					num += CcmBlockCipher.BlockSize;
					i += CcmBlockCipher.BlockSize;
				}
				byte[] array3 = new byte[CcmBlockCipher.BlockSize];
				Array.Copy(input, i, array3, 0, inLen - i);
				blockCipher.ProcessBlock(array3, 0, array3, 0);
				Array.Copy(array3, 0, array2, num, inLen - i);
				num += inLen - i;
				Array.Copy(this.macBlock, 0, array2, num, array2.Length - num);
			}
			else
			{
				int num2 = inOff;
				int j = 0;
				array2 = new byte[inLen - this.macSize];
				Array.Copy(input, inOff + inLen - this.macSize, this.macBlock, 0, this.macSize);
				blockCipher.ProcessBlock(this.macBlock, 0, this.macBlock, 0);
				for (int num3 = this.macSize; num3 != this.macBlock.Length; num3++)
				{
					this.macBlock[num3] = 0;
				}
				while (j < array2.Length - CcmBlockCipher.BlockSize)
				{
					blockCipher.ProcessBlock(input, num2, array2, j);
					j += CcmBlockCipher.BlockSize;
					num2 += CcmBlockCipher.BlockSize;
				}
				byte[] array4 = new byte[CcmBlockCipher.BlockSize];
				Array.Copy(input, num2, array4, 0, array2.Length - j);
				blockCipher.ProcessBlock(array4, 0, array4, 0);
				Array.Copy(array4, 0, array2, j, array2.Length - j);
				byte[] b = new byte[CcmBlockCipher.BlockSize];
				this.calculateMac(array2, 0, array2.Length, b);
				if (!Arrays.ConstantTimeAreEqual(this.macBlock, b))
				{
					throw new InvalidCipherTextException("mac check in CCM failed");
				}
			}
			return array2;
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x000B5EF8 File Offset: 0x000B4EF8
		private int calculateMac(byte[] data, int dataOff, int dataLen, byte[] macBlock)
		{
			IMac mac = new CbcBlockCipherMac(this.cipher, this.macSize * 8);
			mac.Init(this.keyParam);
			byte[] array = new byte[16];
			if (this.hasAssociatedText())
			{
				byte[] array2 = array;
				int num = 0;
				array2[num] |= 64;
			}
			byte[] array3 = array;
			int num2 = 0;
			array3[num2] |= (byte)(((mac.GetMacSize() - 2) / 2 & 7) << 3);
			byte[] array4 = array;
			int num3 = 0;
			array4[num3] |= (byte)(15 - this.nonce.Length - 1 & 7);
			Array.Copy(this.nonce, 0, array, 1, this.nonce.Length);
			int i = dataLen;
			int num4 = 1;
			while (i > 0)
			{
				array[array.Length - num4] = (byte)(i & 255);
				i >>= 8;
				num4++;
			}
			mac.BlockUpdate(array, 0, array.Length);
			if (this.hasAssociatedText())
			{
				int num5;
				if (this.associatedText.Length < 65280)
				{
					mac.Update((byte)(this.associatedText.Length >> 8));
					mac.Update((byte)this.associatedText.Length);
					num5 = 2;
				}
				else
				{
					mac.Update(byte.MaxValue);
					mac.Update(254);
					mac.Update((byte)(this.associatedText.Length >> 24));
					mac.Update((byte)(this.associatedText.Length >> 16));
					mac.Update((byte)(this.associatedText.Length >> 8));
					mac.Update((byte)this.associatedText.Length);
					num5 = 6;
				}
				mac.BlockUpdate(this.associatedText, 0, this.associatedText.Length);
				num5 = (num5 + this.associatedText.Length) % 16;
				if (num5 != 0)
				{
					for (int num6 = 0; num6 != 16 - num5; num6++)
					{
						mac.Update(0);
					}
				}
			}
			mac.BlockUpdate(data, dataOff, dataLen);
			return mac.DoFinal(macBlock, 0);
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x000B60C6 File Offset: 0x000B50C6
		private bool hasAssociatedText()
		{
			return this.associatedText != null && this.associatedText.Length != 0;
		}

		// Token: 0x0400150A RID: 5386
		private static readonly int BlockSize = 16;

		// Token: 0x0400150B RID: 5387
		private readonly IBlockCipher cipher;

		// Token: 0x0400150C RID: 5388
		private readonly byte[] macBlock;

		// Token: 0x0400150D RID: 5389
		private bool forEncryption;

		// Token: 0x0400150E RID: 5390
		private byte[] nonce;

		// Token: 0x0400150F RID: 5391
		private byte[] associatedText;

		// Token: 0x04001510 RID: 5392
		private int macSize;

		// Token: 0x04001511 RID: 5393
		private ICipherParameters keyParam;

		// Token: 0x04001512 RID: 5394
		private readonly MemoryStream data = new MemoryStream();
	}
}
