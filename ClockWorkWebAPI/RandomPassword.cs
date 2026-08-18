using System;
using System.Security.Cryptography;

namespace ClockWorkWebAPI
{
	// Token: 0x02000026 RID: 38
	public class RandomPassword
	{
		// Token: 0x06000208 RID: 520 RVA: 0x0000EF94 File Offset: 0x0000D194
		public static string Generate()
		{
			return RandomPassword.Generate(RandomPassword.DEFAULT_MIN_PASSWORD_LENGTH, RandomPassword.DEFAULT_MAX_PASSWORD_LENGTH);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000EFB8 File Offset: 0x0000D1B8
		public static string Generate(int length)
		{
			return RandomPassword.Generate(length, length);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000EFD4 File Offset: 0x0000D1D4
		public static string Generate(int minLength, int maxLength)
		{
			bool flag = minLength <= 0 || maxLength <= 0 || minLength > maxLength;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				char[][] array = new char[][]
				{
					RandomPassword.PASSWORD_CHARS_LCASE.ToCharArray(),
					RandomPassword.PASSWORD_CHARS_UCASE.ToCharArray(),
					RandomPassword.PASSWORD_CHARS_NUMERIC.ToCharArray(),
					RandomPassword.PASSWORD_CHARS_SPECIAL.ToCharArray()
				};
				int[] array2 = new int[array.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = array[i].Length;
				}
				int[] array3 = new int[array.Length];
				for (int j = 0; j < array3.Length; j++)
				{
					array3[j] = j;
				}
				byte[] array4 = new byte[4];
				RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider();
				rngcryptoServiceProvider.GetBytes(array4);
				int seed = (int)(array4[0] & 127) << 24 | (int)array4[1] << 16 | (int)array4[2] << 8 | (int)array4[3];
				Random random = new Random(seed);
				bool flag2 = minLength < maxLength;
				char[] array5;
				if (flag2)
				{
					array5 = new char[random.Next(minLength, maxLength + 1)];
				}
				else
				{
					array5 = new char[minLength];
				}
				int num = array3.Length - 1;
				for (int k = 0; k < array5.Length; k++)
				{
					bool flag3 = num == 0;
					int num2;
					if (flag3)
					{
						num2 = 0;
					}
					else
					{
						num2 = random.Next(0, num);
					}
					int num3 = array3[num2];
					int num4 = array2[num3] - 1;
					bool flag4 = num4 == 0;
					int num5;
					if (flag4)
					{
						num5 = 0;
					}
					else
					{
						num5 = random.Next(0, num4 + 1);
					}
					array5[k] = array[num3][num5];
					bool flag5 = num4 == 0;
					if (flag5)
					{
						array2[num3] = array[num3].Length;
					}
					else
					{
						bool flag6 = num4 != num5;
						if (flag6)
						{
							char c = array[num3][num4];
							array[num3][num4] = array[num3][num5];
							array[num3][num5] = c;
						}
						array2[num3]--;
					}
					bool flag7 = num == 0;
					if (flag7)
					{
						num = array3.Length - 1;
					}
					else
					{
						bool flag8 = num != num2;
						if (flag8)
						{
							int num6 = array3[num];
							array3[num] = array3[num2];
							array3[num2] = num6;
						}
						num--;
					}
				}
				result = new string(array5);
			}
			return result;
		}

		// Token: 0x0400012A RID: 298
		private static int DEFAULT_MIN_PASSWORD_LENGTH = 8;

		// Token: 0x0400012B RID: 299
		private static int DEFAULT_MAX_PASSWORD_LENGTH = 10;

		// Token: 0x0400012C RID: 300
		private static string PASSWORD_CHARS_LCASE = "abcdefgijkmnopqrstwxyz";

		// Token: 0x0400012D RID: 301
		private static string PASSWORD_CHARS_UCASE = "ABCDEFGHJKLMNPQRSTWXYZ";

		// Token: 0x0400012E RID: 302
		private static string PASSWORD_CHARS_NUMERIC = "23456789";

		// Token: 0x0400012F RID: 303
		private static string PASSWORD_CHARS_SPECIAL = "*$-+?_&=!%{}/";
	}
}
