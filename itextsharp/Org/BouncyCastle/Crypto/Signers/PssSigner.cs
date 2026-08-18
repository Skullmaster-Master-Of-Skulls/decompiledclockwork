using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x020005A6 RID: 1446
	public class PssSigner : ISigner
	{
		// Token: 0x060031ED RID: 12781 RVA: 0x00137028 File Offset: 0x00136028
		public PssSigner(IAsymmetricBlockCipher cipher, IDigest digest) : this(cipher, digest, digest.GetDigestSize())
		{
		}

		// Token: 0x060031EE RID: 12782 RVA: 0x00137038 File Offset: 0x00136038
		public PssSigner(IAsymmetricBlockCipher cipher, IDigest digest, int saltLen) : this(cipher, digest, saltLen, 188)
		{
		}

		// Token: 0x060031EF RID: 12783 RVA: 0x00137048 File Offset: 0x00136048
		public PssSigner(IAsymmetricBlockCipher cipher, IDigest digest, int saltLen, byte trailer) : this(cipher, digest, digest, saltLen, 188)
		{
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x0013705C File Offset: 0x0013605C
		public PssSigner(IAsymmetricBlockCipher cipher, IDigest contentDigest, IDigest mgfDigest, int saltLen, byte trailer)
		{
			this.cipher = cipher;
			this.contentDigest = contentDigest;
			this.mgfDigest = mgfDigest;
			this.hLen = mgfDigest.GetDigestSize();
			this.sLen = saltLen;
			this.salt = new byte[saltLen];
			this.mDash = new byte[8 + saltLen + this.hLen];
			this.trailer = trailer;
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x060031F1 RID: 12785 RVA: 0x001370C3 File Offset: 0x001360C3
		public string AlgorithmName
		{
			get
			{
				return this.mgfDigest.AlgorithmName + "withRSAandMGF1";
			}
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x001370DC File Offset: 0x001360DC
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
			if (parameters is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)parameters;
				parameters = parametersWithRandom.Parameters;
				this.random = parametersWithRandom.Random;
			}
			else if (forSigning)
			{
				this.random = new SecureRandom();
			}
			this.cipher.Init(forSigning, parameters);
			RsaKeyParameters rsaKeyParameters;
			if (parameters is RsaBlindingParameters)
			{
				rsaKeyParameters = ((RsaBlindingParameters)parameters).PublicKey;
			}
			else
			{
				rsaKeyParameters = (RsaKeyParameters)parameters;
			}
			this.emBits = rsaKeyParameters.Modulus.BitLength - 1;
			if (this.emBits < 8 * this.hLen + 8 * this.sLen + 9)
			{
				throw new ArgumentException("key too small for specified hash and salt lengths");
			}
			this.block = new byte[(this.emBits + 7) / 8];
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x00137195 File Offset: 0x00136195
		private void ClearBlock(byte[] block)
		{
			Array.Clear(block, 0, block.Length);
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x001371A1 File Offset: 0x001361A1
		public virtual void Update(byte input)
		{
			this.contentDigest.Update(input);
		}

		// Token: 0x060031F5 RID: 12789 RVA: 0x001371AF File Offset: 0x001361AF
		public virtual void BlockUpdate(byte[] input, int inOff, int length)
		{
			this.contentDigest.BlockUpdate(input, inOff, length);
		}

		// Token: 0x060031F6 RID: 12790 RVA: 0x001371BF File Offset: 0x001361BF
		public virtual void Reset()
		{
			this.contentDigest.Reset();
		}

		// Token: 0x060031F7 RID: 12791 RVA: 0x001371CC File Offset: 0x001361CC
		public virtual byte[] GenerateSignature()
		{
			this.contentDigest.DoFinal(this.mDash, this.mDash.Length - this.hLen - this.sLen);
			if (this.sLen != 0)
			{
				this.random.NextBytes(this.salt);
				this.salt.CopyTo(this.mDash, this.mDash.Length - this.sLen);
			}
			byte[] array = new byte[this.hLen];
			this.mgfDigest.BlockUpdate(this.mDash, 0, this.mDash.Length);
			this.mgfDigest.DoFinal(array, 0);
			this.block[this.block.Length - this.sLen - 1 - this.hLen - 1] = 1;
			this.salt.CopyTo(this.block, this.block.Length - this.sLen - this.hLen - 1);
			byte[] array2 = this.MaskGeneratorFunction1(array, 0, array.Length, this.block.Length - this.hLen - 1);
			for (int num = 0; num != array2.Length; num++)
			{
				byte[] array3 = this.block;
				int num2 = num;
				array3[num2] ^= array2[num];
			}
			byte[] array4 = this.block;
			int num3 = 0;
			array4[num3] &= (byte)(255 >> this.block.Length * 8 - this.emBits);
			array.CopyTo(this.block, this.block.Length - this.hLen - 1);
			this.block[this.block.Length - 1] = this.trailer;
			byte[] result = this.cipher.ProcessBlock(this.block, 0, this.block.Length);
			this.ClearBlock(this.block);
			return result;
		}

		// Token: 0x060031F8 RID: 12792 RVA: 0x00137390 File Offset: 0x00136390
		public virtual bool VerifySignature(byte[] signature)
		{
			this.contentDigest.DoFinal(this.mDash, this.mDash.Length - this.hLen - this.sLen);
			byte[] array = this.cipher.ProcessBlock(signature, 0, signature.Length);
			array.CopyTo(this.block, this.block.Length - array.Length);
			if (this.block[this.block.Length - 1] != this.trailer)
			{
				this.ClearBlock(this.block);
				return false;
			}
			byte[] array2 = this.MaskGeneratorFunction1(this.block, this.block.Length - this.hLen - 1, this.hLen, this.block.Length - this.hLen - 1);
			for (int num = 0; num != array2.Length; num++)
			{
				byte[] array3 = this.block;
				int num2 = num;
				array3[num2] ^= array2[num];
			}
			byte[] array4 = this.block;
			int num3 = 0;
			array4[num3] &= (byte)(255 >> this.block.Length * 8 - this.emBits);
			for (int num4 = 0; num4 != this.block.Length - this.hLen - this.sLen - 2; num4++)
			{
				if (this.block[num4] != 0)
				{
					this.ClearBlock(this.block);
					return false;
				}
			}
			if (this.block[this.block.Length - this.hLen - this.sLen - 2] != 1)
			{
				this.ClearBlock(this.block);
				return false;
			}
			Array.Copy(this.block, this.block.Length - this.sLen - this.hLen - 1, this.mDash, this.mDash.Length - this.sLen, this.sLen);
			this.mgfDigest.BlockUpdate(this.mDash, 0, this.mDash.Length);
			this.mgfDigest.DoFinal(this.mDash, this.mDash.Length - this.hLen);
			int num5 = this.block.Length - this.hLen - 1;
			for (int num6 = this.mDash.Length - this.hLen; num6 != this.mDash.Length; num6++)
			{
				if ((this.block[num5] ^ this.mDash[num6]) != 0)
				{
					this.ClearBlock(this.mDash);
					this.ClearBlock(this.block);
					return false;
				}
				num5++;
			}
			this.ClearBlock(this.mDash);
			this.ClearBlock(this.block);
			return true;
		}

		// Token: 0x060031F9 RID: 12793 RVA: 0x00137612 File Offset: 0x00136612
		private void ItoOSP(int i, byte[] sp)
		{
			sp[0] = (byte)((uint)i >> 24);
			sp[1] = (byte)((uint)i >> 16);
			sp[2] = (byte)((uint)i >> 8);
			sp[3] = (byte)i;
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x00137630 File Offset: 0x00136630
		private byte[] MaskGeneratorFunction1(byte[] Z, int zOff, int zLen, int length)
		{
			byte[] array = new byte[length];
			byte[] array2 = new byte[this.hLen];
			byte[] array3 = new byte[4];
			int i = 0;
			this.mgfDigest.Reset();
			while (i < length / this.hLen)
			{
				this.ItoOSP(i, array3);
				this.mgfDigest.BlockUpdate(Z, zOff, zLen);
				this.mgfDigest.BlockUpdate(array3, 0, array3.Length);
				this.mgfDigest.DoFinal(array2, 0);
				array2.CopyTo(array, i * this.hLen);
				i++;
			}
			if (i * this.hLen < length)
			{
				this.ItoOSP(i, array3);
				this.mgfDigest.BlockUpdate(Z, zOff, zLen);
				this.mgfDigest.BlockUpdate(array3, 0, array3.Length);
				this.mgfDigest.DoFinal(array2, 0);
				Array.Copy(array2, 0, array, i * this.hLen, array.Length - i * this.hLen);
			}
			return array;
		}

		// Token: 0x0400224A RID: 8778
		public const byte TrailerImplicit = 188;

		// Token: 0x0400224B RID: 8779
		private readonly IDigest contentDigest;

		// Token: 0x0400224C RID: 8780
		private readonly IDigest mgfDigest;

		// Token: 0x0400224D RID: 8781
		private readonly IAsymmetricBlockCipher cipher;

		// Token: 0x0400224E RID: 8782
		private SecureRandom random;

		// Token: 0x0400224F RID: 8783
		private int hLen;

		// Token: 0x04002250 RID: 8784
		private int sLen;

		// Token: 0x04002251 RID: 8785
		private int emBits;

		// Token: 0x04002252 RID: 8786
		private byte[] salt;

		// Token: 0x04002253 RID: 8787
		private byte[] mDash;

		// Token: 0x04002254 RID: 8788
		private byte[] block;

		// Token: 0x04002255 RID: 8789
		private byte trailer;
	}
}
