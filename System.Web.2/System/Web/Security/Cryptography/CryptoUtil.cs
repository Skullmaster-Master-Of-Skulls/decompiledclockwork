using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Web.Util;

namespace System.Web.Security.Cryptography
{
	// Token: 0x0200060B RID: 1547
	internal static class CryptoUtil
	{
		// Token: 0x06004DB9 RID: 19897 RVA: 0x0010DD5C File Offset: 0x0010BF5C
		public static string BinaryToHex(byte[] data)
		{
			if (data == null)
			{
				return null;
			}
			char[] array = new char[checked(data.Length * 2)];
			for (int i = 0; i < data.Length; i++)
			{
				byte b = data[i];
				array[2 * i] = CryptoUtil.NibbleToHex((byte)(b >> 4));
				array[2 * i + 1] = CryptoUtil.NibbleToHex(b & 15);
			}
			return new string(array);
		}

		// Token: 0x06004DBA RID: 19898 RVA: 0x0010DDB0 File Offset: 0x0010BFB0
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		public static bool BuffersAreEqual(byte[] buffer1, int buffer1Offset, int buffer1Count, byte[] buffer2, int buffer2Offset, int buffer2Count)
		{
			if (buffer1Count != buffer2Count)
			{
				return false;
			}
			int num = 0;
			for (int i = 0; i < buffer1Count; i++)
			{
				num |= (int)(buffer1[buffer1Offset + i] - buffer2[buffer2Offset + i]);
			}
			return num == 0;
		}

		// Token: 0x06004DBB RID: 19899 RVA: 0x0010DDE5 File Offset: 0x0010BFE5
		public static byte[] ComputeSHA256Hash(byte[] input)
		{
			return CryptoUtil.ComputeSHA256Hash(input, 0, input.Length);
		}

		// Token: 0x06004DBC RID: 19900 RVA: 0x0010DDF4 File Offset: 0x0010BFF4
		public static byte[] ComputeSHA256Hash(byte[] buffer, int offset, int count)
		{
			byte[] result;
			using (SHA256 sha = CryptoAlgorithms.CreateSHA256())
			{
				result = sha.ComputeHash(buffer, offset, count);
			}
			return result;
		}

		// Token: 0x06004DBD RID: 19901 RVA: 0x0010DE30 File Offset: 0x0010C030
		public static byte[] CreatePredictableIV(byte[] buffer, int ivBitLength)
		{
			byte[] array = new byte[ivBitLength / 8];
			int num = 0;
			int i = array.Length;
			using (SHA256 sha = CryptoAlgorithms.CreateSHA256())
			{
				while (i > 0)
				{
					byte[] array2 = sha.ComputeHash(buffer);
					int num2 = Math.Min(i, array2.Length);
					Buffer.BlockCopy(array2, 0, array, num, num2);
					num += num2;
					i -= num2;
					buffer = array2;
				}
			}
			return array;
		}

		// Token: 0x06004DBE RID: 19902 RVA: 0x0010DEA4 File Offset: 0x0010C0A4
		public static byte[] HexToBinary(string data)
		{
			if (data == null || data.Length % 2 != 0)
			{
				return null;
			}
			byte[] array = new byte[data.Length / 2];
			for (int i = 0; i < array.Length; i++)
			{
				int num = HttpEncoderUtility.HexToInt(data[2 * i]);
				int num2 = HttpEncoderUtility.HexToInt(data[2 * i + 1]);
				if (num == -1 || num2 == -1)
				{
					return null;
				}
				array[i] = (byte)(num << 4 | num2);
			}
			return array;
		}

		// Token: 0x06004DBF RID: 19903 RVA: 0x0010DF10 File Offset: 0x0010C110
		private static char NibbleToHex(byte nibble)
		{
			return (char)((nibble < 10) ? (nibble + 48) : (nibble - 10 + 65));
		}

		// Token: 0x04002970 RID: 10608
		public static readonly UTF8Encoding SecureUTF8Encoding = new UTF8Encoding(false, true);
	}
}
