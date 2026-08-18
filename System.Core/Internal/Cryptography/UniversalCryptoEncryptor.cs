using System;
using System.Security.Cryptography;

namespace Internal.Cryptography
{
	// Token: 0x0200000D RID: 13
	internal sealed class UniversalCryptoEncryptor : UniversalCryptoTransform
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002720 File Offset: 0x00000920
		public UniversalCryptoEncryptor(PaddingMode paddingMode, BasicSymmetricCipher basicSymmetricCipher) : base(paddingMode, basicSymmetricCipher)
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000272A File Offset: 0x0000092A
		protected sealed override int UncheckedTransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			return base.BasicSymmetricCipher.Transform(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002740 File Offset: 0x00000940
		protected sealed override byte[] UncheckedTransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			byte[] array = this.PadBlock(inputBuffer, inputOffset, inputCount);
			return base.BasicSymmetricCipher.TransformFinal(array, 0, array.Length);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000276C File Offset: 0x0000096C
		private byte[] PadBlock(byte[] block, int offset, int count)
		{
			int num = base.InputBlockSize - count % base.InputBlockSize;
			byte[] array;
			switch (base.PaddingMode)
			{
			case PaddingMode.None:
				if (count % base.InputBlockSize != 0)
				{
					throw new CryptographicException(SR.GetString("Cryptography_PartialBlock"));
				}
				array = new byte[count];
				Buffer.BlockCopy(block, offset, array, 0, array.Length);
				break;
			case PaddingMode.PKCS7:
				array = new byte[count + num];
				Buffer.BlockCopy(block, offset, array, 0, count);
				for (int i = count; i < array.Length; i++)
				{
					array[i] = (byte)num;
				}
				break;
			case PaddingMode.Zeros:
				if (num == base.InputBlockSize)
				{
					num = 0;
				}
				array = new byte[count + num];
				Buffer.BlockCopy(block, offset, array, 0, count);
				break;
			case PaddingMode.ANSIX923:
				array = new byte[count + num];
				Buffer.BlockCopy(block, offset, array, 0, count);
				for (int j = count; j < array.Length - 1; j++)
				{
					array[j] = 0;
				}
				array[array.Length - 1] = (byte)num;
				break;
			case PaddingMode.ISO10126:
				array = new byte[count + num];
				Buffer.BlockCopy(block, offset, array, 0, count);
				if (num > 1)
				{
					if (UniversalCryptoEncryptor.s_rng == null)
					{
						UniversalCryptoEncryptor.s_rng = new RNGCryptoServiceProvider();
					}
					UniversalCryptoEncryptor.s_rng.GetBytes(array, count, num - 1);
				}
				array[array.Length - 1] = (byte)num;
				break;
			default:
				throw new CryptographicException(SR.GetString("Cryptography_UnknownPaddingMode"));
			}
			return array;
		}

		// Token: 0x04000065 RID: 101
		private static volatile RNGCryptoServiceProvider s_rng;
	}
}
