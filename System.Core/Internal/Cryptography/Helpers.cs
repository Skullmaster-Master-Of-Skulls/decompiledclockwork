using System;
using System.Security.Cryptography;

namespace Internal.Cryptography
{
	// Token: 0x02000006 RID: 6
	internal static class Helpers
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static byte[] CloneByteArray(this byte[] src)
		{
			if (src != null)
			{
				return (byte[])src.Clone();
			}
			return null;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002062 File Offset: 0x00000262
		public static bool UsesIv(this CipherMode cipherMode)
		{
			return cipherMode != CipherMode.ECB;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000206B File Offset: 0x0000026B
		public static byte[] GetCipherIv(this CipherMode cipherMode, byte[] iv)
		{
			if (!cipherMode.UsesIv())
			{
				return null;
			}
			if (iv == null)
			{
				throw new CryptographicException("Cryptography_MissingIV");
			}
			return iv;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002086 File Offset: 0x00000286
		public static CryptographicException ToCryptographicException(this Interop.NCrypt.ErrorCode errorCode)
		{
			return ((int)errorCode).ToCryptographicException();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002090 File Offset: 0x00000290
		public static bool IsLegalSize(this int size, KeySizes[] legalSizes)
		{
			for (int i = 0; i < legalSizes.Length; i++)
			{
				if (legalSizes[i].SkipSize == 0)
				{
					if (legalSizes[i].MinSize == size)
					{
						return true;
					}
				}
				else
				{
					for (int j = legalSizes[i].MinSize; j <= legalSizes[i].MaxSize; j += legalSizes[i].SkipSize)
					{
						if (j == size)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020EA File Offset: 0x000002EA
		public static int BitSizeToByteSize(this int bits)
		{
			return (bits + 7) / 8;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020F4 File Offset: 0x000002F4
		public static byte[] GenerateRandom(int count)
		{
			byte[] array = new byte[count];
			using (RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create())
			{
				randomNumberGenerator.GetBytes(array);
			}
			return array;
		}
	}
}
