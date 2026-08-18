using System;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x020001E9 RID: 489
	public class Iso9796d2PssSigner : ISignerWithRecovery, ISigner
	{
		// Token: 0x06001320 RID: 4896 RVA: 0x0006D998 File Offset: 0x0006C998
		public byte[] GetRecoveredMessage()
		{
			return this.recoveredMessage;
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x0006D9A0 File Offset: 0x0006C9A0
		public Iso9796d2PssSigner(IAsymmetricBlockCipher cipher, IDigest digest, int saltLength, bool isImplicit)
		{
			this.cipher = cipher;
			this.digest = digest;
			this.hLen = digest.GetDigestSize();
			this.saltLength = saltLength;
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

		// Token: 0x06001322 RID: 4898 RVA: 0x0006DA2A File Offset: 0x0006CA2A
		public Iso9796d2PssSigner(IAsymmetricBlockCipher cipher, IDigest digest, int saltLength) : this(cipher, digest, saltLength, false)
		{
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x0006DA36 File Offset: 0x0006CA36
		public string AlgorithmName
		{
			get
			{
				return this.digest.AlgorithmName + "withISO9796-2S2";
			}
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0006DA50 File Offset: 0x0006CA50
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
			RsaKeyParameters rsaKeyParameters;
			if (parameters is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)parameters;
				rsaKeyParameters = (RsaKeyParameters)parametersWithRandom.Parameters;
				if (forSigning)
				{
					this.random = parametersWithRandom.Random;
				}
			}
			else if (parameters is ParametersWithSalt)
			{
				if (!forSigning)
				{
					throw new ArgumentException("ParametersWithSalt only valid for signing", "parameters");
				}
				ParametersWithSalt parametersWithSalt = (ParametersWithSalt)parameters;
				rsaKeyParameters = (RsaKeyParameters)parametersWithSalt.Parameters;
				this.standardSalt = parametersWithSalt.GetSalt();
				if (this.standardSalt.Length != this.saltLength)
				{
					throw new ArgumentException("Fixed salt is of wrong length");
				}
			}
			else
			{
				rsaKeyParameters = (RsaKeyParameters)parameters;
				if (forSigning)
				{
					this.random = new SecureRandom();
				}
			}
			this.cipher.Init(forSigning, rsaKeyParameters);
			this.keyBits = rsaKeyParameters.Modulus.BitLength;
			this.block = new byte[(this.keyBits + 7) / 8];
			if (this.trailer == 188)
			{
				this.mBuf = new byte[this.block.Length - this.digest.GetDigestSize() - this.saltLength - 1 - 1];
			}
			else
			{
				this.mBuf = new byte[this.block.Length - this.digest.GetDigestSize() - this.saltLength - 1 - 2];
			}
			this.Reset();
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x0006DB90 File Offset: 0x0006CB90
		private bool IsSameAs(byte[] a, byte[] b)
		{
			if (this.messageLength != b.Length)
			{
				return false;
			}
			for (int num = 0; num != b.Length; num++)
			{
				if (a[num] != b[num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0006DBC3 File Offset: 0x0006CBC3
		private void ClearBlock(byte[] block)
		{
			Array.Clear(block, 0, block.Length);
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x0006DBD0 File Offset: 0x0006CBD0
		public virtual void Update(byte input)
		{
			if (this.messageLength < this.mBuf.Length)
			{
				this.mBuf[this.messageLength++] = input;
				return;
			}
			this.digest.Update(input);
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x0006DC13 File Offset: 0x0006CC13
		public virtual void BlockUpdate(byte[] input, int inOff, int length)
		{
			while (length > 0 && this.messageLength < this.mBuf.Length)
			{
				this.Update(input[inOff]);
				inOff++;
				length--;
			}
			if (length > 0)
			{
				this.digest.BlockUpdate(input, inOff, length);
			}
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0006DC50 File Offset: 0x0006CC50
		public virtual void Reset()
		{
			this.digest.Reset();
			this.messageLength = 0;
			if (this.mBuf != null)
			{
				this.ClearBlock(this.mBuf);
			}
			if (this.recoveredMessage != null)
			{
				this.ClearBlock(this.recoveredMessage);
				this.recoveredMessage = null;
			}
			this.fullMessage = false;
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0006DCA8 File Offset: 0x0006CCA8
		public byte[] GenerateSignature()
		{
			int digestSize = this.digest.GetDigestSize();
			byte[] array = new byte[digestSize];
			this.digest.DoFinal(array, 0);
			byte[] array2 = new byte[8];
			this.LtoOSP((long)(this.messageLength * 8), array2);
			this.digest.BlockUpdate(array2, 0, array2.Length);
			this.digest.BlockUpdate(this.mBuf, 0, this.messageLength);
			this.digest.BlockUpdate(array, 0, array.Length);
			byte[] array3;
			if (this.standardSalt != null)
			{
				array3 = this.standardSalt;
			}
			else
			{
				array3 = new byte[this.saltLength];
				this.random.NextBytes(array3);
			}
			this.digest.BlockUpdate(array3, 0, array3.Length);
			byte[] array4 = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array4, 0);
			int num = 2;
			if (this.trailer == 188)
			{
				num = 1;
			}
			int num2 = this.block.Length - this.messageLength - array3.Length - this.hLen - num - 1;
			this.block[num2] = 1;
			Array.Copy(this.mBuf, 0, this.block, num2 + 1, this.messageLength);
			Array.Copy(array3, 0, this.block, num2 + 1 + this.messageLength, array3.Length);
			byte[] array5 = this.MaskGeneratorFunction1(array4, 0, array4.Length, this.block.Length - this.hLen - num);
			for (int num3 = 0; num3 != array5.Length; num3++)
			{
				byte[] array6 = this.block;
				int num4 = num3;
				array6[num4] ^= array5[num3];
			}
			Array.Copy(array4, 0, this.block, this.block.Length - this.hLen - num, this.hLen);
			if (this.trailer == 188)
			{
				this.block[this.block.Length - 1] = 188;
			}
			else
			{
				this.block[this.block.Length - 2] = (byte)((uint)this.trailer >> 8);
				this.block[this.block.Length - 1] = (byte)this.trailer;
			}
			byte[] array7 = this.block;
			int num5 = 0;
			array7[num5] &= 127;
			byte[] result = this.cipher.ProcessBlock(this.block, 0, this.block.Length);
			this.ClearBlock(this.mBuf);
			this.ClearBlock(this.block);
			this.messageLength = 0;
			return result;
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x0006DF1C File Offset: 0x0006CF1C
		public virtual bool VerifySignature(byte[] signature)
		{
			byte[] array = this.cipher.ProcessBlock(signature, 0, signature.Length);
			int num = (this.keyBits + 7) / 8;
			if (array.Length < num)
			{
				byte[] array2 = new byte[num];
				array.CopyTo(array2, array2.Length - array.Length);
				this.ClearBlock(array);
				array = array2;
			}
			int num2;
			if (((array[array.Length - 1] & 255) ^ 188) == 0)
			{
				num2 = 1;
			}
			else
			{
				int num3 = (int)(array[array.Length - 2] & byte.MaxValue) << 8 | (int)(array[array.Length - 1] & byte.MaxValue);
				int num4 = num3;
				if (num4 != 12748)
				{
					if (num4 != 13004)
					{
						if (num4 != 13260)
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
				num2 = 2;
			}
			byte[] array3 = new byte[this.hLen];
			this.digest.DoFinal(array3, 0);
			byte[] array4 = this.MaskGeneratorFunction1(array, array.Length - this.hLen - num2, this.hLen, array.Length - this.hLen - num2);
			for (int num5 = 0; num5 != array4.Length; num5++)
			{
				byte[] array5 = array;
				int num6 = num5;
				array5[num6] ^= array4[num5];
			}
			byte[] array6 = array;
			int num7 = 0;
			array6[num7] &= 127;
			int num8 = 0;
			while (num8 < array.Length && array[num8++] != 1)
			{
			}
			if (num8 >= array.Length)
			{
				this.ClearBlock(array);
				return false;
			}
			this.fullMessage = (num8 > 1);
			this.recoveredMessage = new byte[array4.Length - num8 - this.saltLength];
			Array.Copy(array, num8, this.recoveredMessage, 0, this.recoveredMessage.Length);
			byte[] array7 = new byte[8];
			this.LtoOSP((long)(this.recoveredMessage.Length * 8), array7);
			this.digest.BlockUpdate(array7, 0, array7.Length);
			if (this.recoveredMessage.Length != 0)
			{
				this.digest.BlockUpdate(this.recoveredMessage, 0, this.recoveredMessage.Length);
			}
			this.digest.BlockUpdate(array3, 0, array3.Length);
			this.digest.BlockUpdate(array, num8 + this.recoveredMessage.Length, this.saltLength);
			byte[] array8 = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array8, 0);
			int num9 = array.Length - num2 - array8.Length;
			for (int num10 = 0; num10 != array8.Length; num10++)
			{
				if (array8[num10] != array[num9 + num10])
				{
					this.ClearBlock(array);
					this.ClearBlock(array8);
					this.ClearBlock(this.recoveredMessage);
					this.fullMessage = false;
					return false;
				}
			}
			this.ClearBlock(array);
			this.ClearBlock(array8);
			if (this.messageLength != 0)
			{
				if (!this.IsSameAs(this.mBuf, this.recoveredMessage))
				{
					this.ClearBlock(this.mBuf);
					return false;
				}
				this.messageLength = 0;
			}
			this.ClearBlock(this.mBuf);
			return true;
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x0006E242 File Offset: 0x0006D242
		public virtual bool HasFullMessage()
		{
			return this.fullMessage;
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0006E24A File Offset: 0x0006D24A
		private void ItoOSP(int i, byte[] sp)
		{
			sp[0] = (byte)((uint)i >> 24);
			sp[1] = (byte)((uint)i >> 16);
			sp[2] = (byte)((uint)i >> 8);
			sp[3] = (byte)i;
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0006E268 File Offset: 0x0006D268
		private void LtoOSP(long l, byte[] sp)
		{
			sp[0] = (byte)((ulong)l >> 56);
			sp[1] = (byte)((ulong)l >> 48);
			sp[2] = (byte)((ulong)l >> 40);
			sp[3] = (byte)((ulong)l >> 32);
			sp[4] = (byte)((ulong)l >> 24);
			sp[5] = (byte)((ulong)l >> 16);
			sp[6] = (byte)((ulong)l >> 8);
			sp[7] = (byte)l;
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0006E2A8 File Offset: 0x0006D2A8
		private byte[] MaskGeneratorFunction1(byte[] Z, int zOff, int zLen, int length)
		{
			byte[] array = new byte[length];
			byte[] array2 = new byte[this.hLen];
			byte[] array3 = new byte[4];
			int num = 0;
			this.digest.Reset();
			do
			{
				this.ItoOSP(num, array3);
				this.digest.BlockUpdate(Z, zOff, zLen);
				this.digest.BlockUpdate(array3, 0, array3.Length);
				this.digest.DoFinal(array2, 0);
				Array.Copy(array2, 0, array, num * this.hLen, this.hLen);
			}
			while (++num < length / this.hLen);
			if (num * this.hLen < length)
			{
				this.ItoOSP(num, array3);
				this.digest.BlockUpdate(Z, zOff, zLen);
				this.digest.BlockUpdate(array3, 0, array3.Length);
				this.digest.DoFinal(array2, 0);
				Array.Copy(array2, 0, array, num * this.hLen, array.Length - num * this.hLen);
			}
			return array;
		}

		// Token: 0x04000D68 RID: 3432
		public const int TrailerImplicit = 188;

		// Token: 0x04000D69 RID: 3433
		public const int TrailerRipeMD160 = 12748;

		// Token: 0x04000D6A RID: 3434
		public const int TrailerRipeMD128 = 13004;

		// Token: 0x04000D6B RID: 3435
		public const int TrailerSha1 = 13260;

		// Token: 0x04000D6C RID: 3436
		private IDigest digest;

		// Token: 0x04000D6D RID: 3437
		private IAsymmetricBlockCipher cipher;

		// Token: 0x04000D6E RID: 3438
		private SecureRandom random;

		// Token: 0x04000D6F RID: 3439
		private byte[] standardSalt;

		// Token: 0x04000D70 RID: 3440
		private int hLen;

		// Token: 0x04000D71 RID: 3441
		private int trailer;

		// Token: 0x04000D72 RID: 3442
		private int keyBits;

		// Token: 0x04000D73 RID: 3443
		private byte[] block;

		// Token: 0x04000D74 RID: 3444
		private byte[] mBuf;

		// Token: 0x04000D75 RID: 3445
		private int messageLength;

		// Token: 0x04000D76 RID: 3446
		private readonly int saltLength;

		// Token: 0x04000D77 RID: 3447
		private bool fullMessage;

		// Token: 0x04000D78 RID: 3448
		private byte[] recoveredMessage;
	}
}
