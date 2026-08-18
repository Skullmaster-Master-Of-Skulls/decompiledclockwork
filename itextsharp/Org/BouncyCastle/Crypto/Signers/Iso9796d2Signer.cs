using System;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x0200018B RID: 395
	public class Iso9796d2Signer : ISignerWithRecovery, ISigner
	{
		// Token: 0x06000F51 RID: 3921 RVA: 0x000584B4 File Offset: 0x000574B4
		public byte[] GetRecoveredMessage()
		{
			return this.recoveredMessage;
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x000584BC File Offset: 0x000574BC
		public Iso9796d2Signer(IAsymmetricBlockCipher cipher, IDigest digest, bool isImplicit)
		{
			this.cipher = cipher;
			this.digest = digest;
			if (isImplicit)
			{
				this.trailer = 188;
				return;
			}
			if (digest is Sha1Digest)
			{
				this.trailer = 13260;
				return;
			}
			if (digest is RipeMD160Digest)
			{
				this.trailer = 12748;
				return;
			}
			if (digest is RipeMD128Digest)
			{
				this.trailer = 13004;
				return;
			}
			throw new ArgumentException("no valid trailer for digest");
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x00058532 File Offset: 0x00057532
		public Iso9796d2Signer(IAsymmetricBlockCipher cipher, IDigest digest) : this(cipher, digest, false)
		{
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x0005853D File Offset: 0x0005753D
		public string AlgorithmName
		{
			get
			{
				return this.digest.AlgorithmName + "withISO9796-2S1";
			}
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00058554 File Offset: 0x00057554
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
			RsaKeyParameters rsaKeyParameters = (RsaKeyParameters)parameters;
			this.cipher.Init(forSigning, rsaKeyParameters);
			this.keyBits = rsaKeyParameters.Modulus.BitLength;
			this.block = new byte[(this.keyBits + 7) / 8];
			if (this.trailer == 188)
			{
				this.mBuf = new byte[this.block.Length - this.digest.GetDigestSize() - 2];
			}
			else
			{
				this.mBuf = new byte[this.block.Length - this.digest.GetDigestSize() - 3];
			}
			this.Reset();
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x000585F4 File Offset: 0x000575F4
		private bool IsSameAs(byte[] a, byte[] b)
		{
			if (this.messageLength > this.mBuf.Length)
			{
				if (this.mBuf.Length > b.Length)
				{
					return false;
				}
				for (int num = 0; num != this.mBuf.Length; num++)
				{
					if (a[num] != b[num])
					{
						return false;
					}
				}
			}
			else
			{
				if (this.messageLength != b.Length)
				{
					return false;
				}
				for (int num2 = 0; num2 != b.Length; num2++)
				{
					if (a[num2] != b[num2])
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x00058665 File Offset: 0x00057665
		private void ClearBlock(byte[] block)
		{
			Array.Clear(block, 0, block.Length);
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x00058671 File Offset: 0x00057671
		public void Update(byte input)
		{
			this.digest.Update(input);
			if (this.messageLength < this.mBuf.Length)
			{
				this.mBuf[this.messageLength] = input;
			}
			this.messageLength++;
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x000586AC File Offset: 0x000576AC
		public void BlockUpdate(byte[] input, int inOff, int length)
		{
			this.digest.BlockUpdate(input, inOff, length);
			if (this.messageLength < this.mBuf.Length)
			{
				int num = 0;
				while (num < length && num + this.messageLength < this.mBuf.Length)
				{
					this.mBuf[this.messageLength + num] = input[inOff + num];
					num++;
				}
			}
			this.messageLength += length;
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x00058718 File Offset: 0x00057718
		public virtual void Reset()
		{
			this.digest.Reset();
			this.messageLength = 0;
			this.ClearBlock(this.mBuf);
			if (this.recoveredMessage != null)
			{
				this.ClearBlock(this.recoveredMessage);
			}
			this.recoveredMessage = null;
			this.fullMessage = false;
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x00058768 File Offset: 0x00057768
		public virtual byte[] GenerateSignature()
		{
			int digestSize = this.digest.GetDigestSize();
			int num;
			int num2;
			if (this.trailer == 188)
			{
				num = 8;
				num2 = this.block.Length - digestSize - 1;
				this.digest.DoFinal(this.block, num2);
				this.block[this.block.Length - 1] = 188;
			}
			else
			{
				num = 16;
				num2 = this.block.Length - digestSize - 2;
				this.digest.DoFinal(this.block, num2);
				this.block[this.block.Length - 2] = (byte)((uint)this.trailer >> 8);
				this.block[this.block.Length - 1] = (byte)this.trailer;
			}
			int num3 = (digestSize + this.messageLength) * 8 + num + 4 - this.keyBits;
			byte b;
			if (num3 > 0)
			{
				int num4 = this.messageLength - (num3 + 7) / 8;
				b = 96;
				num2 -= num4;
				Array.Copy(this.mBuf, 0, this.block, num2, num4);
			}
			else
			{
				b = 64;
				num2 -= this.messageLength;
				Array.Copy(this.mBuf, 0, this.block, num2, this.messageLength);
			}
			if (num2 - 1 > 0)
			{
				for (int num5 = num2 - 1; num5 != 0; num5--)
				{
					this.block[num5] = 187;
				}
				byte[] array = this.block;
				int num6 = num2 - 1;
				array[num6] ^= 1;
				this.block[0] = 11;
				byte[] array2 = this.block;
				int num7 = 0;
				array2[num7] |= b;
			}
			else
			{
				this.block[0] = 10;
				byte[] array3 = this.block;
				int num8 = 0;
				array3[num8] |= b;
			}
			byte[] result = this.cipher.ProcessBlock(this.block, 0, this.block.Length);
			this.ClearBlock(this.mBuf);
			this.ClearBlock(this.block);
			return result;
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x00058954 File Offset: 0x00057954
		public virtual bool VerifySignature(byte[] signature)
		{
			byte[] array = this.cipher.ProcessBlock(signature, 0, signature.Length);
			if (((array[0] & 192) ^ 64) != 0)
			{
				this.ClearBlock(this.mBuf);
				this.ClearBlock(array);
				return false;
			}
			if (((array[array.Length - 1] & 15) ^ 12) != 0)
			{
				this.ClearBlock(this.mBuf);
				this.ClearBlock(array);
				return false;
			}
			int num;
			if (((array[array.Length - 1] & 255) ^ 188) == 0)
			{
				num = 1;
			}
			else
			{
				int num2 = (int)(array[array.Length - 2] & byte.MaxValue) << 8 | (int)(array[array.Length - 1] & byte.MaxValue);
				int num3 = num2;
				if (num3 != 12748)
				{
					if (num3 != 13004)
					{
						if (num3 != 13260)
						{
							throw new ArgumentException("unrecognised hash in signature");
						}
						if (!(this.digest is Sha1Digest))
						{
							throw new ArgumentException("signer should be initialised with SHA1");
						}
					}
					else if (!(this.digest is RipeMD128Digest))
					{
						throw new ArgumentException("signer should be initialised with RipeMD128");
					}
				}
				else if (!(this.digest is RipeMD160Digest))
				{
					throw new ArgumentException("signer should be initialised with RipeMD160");
				}
				num = 2;
			}
			int num4 = 0;
			while (num4 != array.Length && ((array[num4] & 15) ^ 10) != 0)
			{
				num4++;
			}
			num4++;
			byte[] array2 = new byte[this.digest.GetDigestSize()];
			int num5 = array.Length - num - array2.Length;
			if (num5 - num4 <= 0)
			{
				this.ClearBlock(this.mBuf);
				this.ClearBlock(array);
				return false;
			}
			if ((array[0] & 32) == 0)
			{
				this.fullMessage = true;
				this.digest.Reset();
				this.digest.BlockUpdate(array, num4, num5 - num4);
				this.digest.DoFinal(array2, 0);
				for (int num6 = 0; num6 != array2.Length; num6++)
				{
					byte[] array3 = array;
					int num7 = num5 + num6;
					array3[num7] ^= array2[num6];
					if (array[num5 + num6] != 0)
					{
						this.ClearBlock(this.mBuf);
						this.ClearBlock(array);
						return false;
					}
				}
				this.recoveredMessage = new byte[num5 - num4];
				Array.Copy(array, num4, this.recoveredMessage, 0, this.recoveredMessage.Length);
			}
			else
			{
				this.fullMessage = false;
				this.digest.DoFinal(array2, 0);
				for (int num8 = 0; num8 != array2.Length; num8++)
				{
					byte[] array4 = array;
					int num9 = num5 + num8;
					array4[num9] ^= array2[num8];
					if (array[num5 + num8] != 0)
					{
						this.ClearBlock(this.mBuf);
						this.ClearBlock(array);
						return false;
					}
				}
				this.recoveredMessage = new byte[num5 - num4];
				Array.Copy(array, num4, this.recoveredMessage, 0, this.recoveredMessage.Length);
			}
			if (this.messageLength != 0 && !this.IsSameAs(this.mBuf, this.recoveredMessage))
			{
				this.ClearBlock(this.mBuf);
				this.ClearBlock(array);
				this.ClearBlock(this.recoveredMessage);
				return false;
			}
			this.ClearBlock(this.mBuf);
			this.ClearBlock(array);
			return true;
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x00058C50 File Offset: 0x00057C50
		public virtual bool HasFullMessage()
		{
			return this.fullMessage;
		}

		// Token: 0x04000B1A RID: 2842
		public const int TrailerImplicit = 188;

		// Token: 0x04000B1B RID: 2843
		public const int TrailerRipeMD160 = 12748;

		// Token: 0x04000B1C RID: 2844
		public const int TrailerRipeMD128 = 13004;

		// Token: 0x04000B1D RID: 2845
		public const int TrailerSha1 = 13260;

		// Token: 0x04000B1E RID: 2846
		private IDigest digest;

		// Token: 0x04000B1F RID: 2847
		private IAsymmetricBlockCipher cipher;

		// Token: 0x04000B20 RID: 2848
		private int trailer;

		// Token: 0x04000B21 RID: 2849
		private int keyBits;

		// Token: 0x04000B22 RID: 2850
		private byte[] block;

		// Token: 0x04000B23 RID: 2851
		private byte[] mBuf;

		// Token: 0x04000B24 RID: 2852
		private int messageLength;

		// Token: 0x04000B25 RID: 2853
		private bool fullMessage;

		// Token: 0x04000B26 RID: 2854
		private byte[] recoveredMessage;
	}
}
