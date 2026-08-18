using System;
using System.Collections.Generic;
using System.Text;

namespace System.IdentityModel
{
	// Token: 0x02000022 RID: 34
	internal static class Asn1IntegerConverter
	{
		// Token: 0x060000FA RID: 250 RVA: 0x000050AC File Offset: 0x000032AC
		public static string Asn1IntegerToDecimalString(byte[] asn1)
		{
			if (asn1 == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("asn1");
			}
			if (asn1.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("asn1", SR.GetString("LengthOfArrayToConvertMustGreaterThanZero")));
			}
			List<byte> list = new List<byte>(asn1.Length * 8 / 3);
			int num = 0;
			byte b;
			for (int i = 0; i < asn1.Length - 1; i++)
			{
				b = asn1[i];
				for (int j = 0; j < 8; j++)
				{
					if ((b & 1) == 1)
					{
						Asn1IntegerConverter.AddSecondDecimalToFirst(list, Asn1IntegerConverter.TwoToThePowerOf(num));
					}
					num++;
					b = (byte)(b >> 1);
				}
			}
			b = asn1[asn1.Length - 1];
			for (int k = 0; k < 7; k++)
			{
				if ((b & 1) == 1)
				{
					Asn1IntegerConverter.AddSecondDecimalToFirst(list, Asn1IntegerConverter.TwoToThePowerOf(num));
				}
				num++;
				b = (byte)(b >> 1);
			}
			StringBuilder stringBuilder = new StringBuilder(list.Count + 1);
			List<byte> list2;
			if (b == 0)
			{
				list2 = list;
			}
			else
			{
				List<byte> list3 = new List<byte>(Asn1IntegerConverter.TwoToThePowerOf(num));
				Asn1IntegerConverter.SubtractSecondDecimalFromFirst(list3, list);
				list2 = list3;
				stringBuilder.Append('-');
			}
			int l = list2.Count - 1;
			while (l >= 0 && list2[l] == 0)
			{
				l--;
			}
			if (l < 0 && asn1.Length != 0)
			{
				stringBuilder.Append(Asn1IntegerConverter.digitMap[0]);
			}
			else
			{
				while (l >= 0)
				{
					stringBuilder.Append(Asn1IntegerConverter.digitMap[(int)list2[l--]]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005214 File Offset: 0x00003414
		private static byte[] TwoToThePowerOf(int n)
		{
			List<byte[]> obj = Asn1IntegerConverter.powersOfTwo;
			byte[] result;
			lock (obj)
			{
				if (n >= Asn1IntegerConverter.powersOfTwo.Count)
				{
					for (int i = Asn1IntegerConverter.powersOfTwo.Count; i <= n; i++)
					{
						List<byte> list = new List<byte>(Asn1IntegerConverter.powersOfTwo[i - 1]);
						byte b = 0;
						for (int j = 0; j < list.Count; j++)
						{
							byte b2 = (byte)(((int)list[j] << 1) + (int)b);
							list[j] = b2 % 10;
							b = b2 / 10;
						}
						if (b > 0)
						{
							list.Add(b);
						}
						Asn1IntegerConverter.powersOfTwo.Add(list.ToArray());
					}
				}
				result = Asn1IntegerConverter.powersOfTwo[n];
			}
			return result;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000052F4 File Offset: 0x000034F4
		private static void AddSecondDecimalToFirst(List<byte> first, byte[] second)
		{
			byte b = 0;
			int num = 0;
			while (num < second.Length || num < first.Count)
			{
				if (num >= first.Count)
				{
					first.Add(0);
				}
				byte b2;
				if (num < second.Length)
				{
					b2 = first[num] + second[num] + b;
				}
				else
				{
					b2 = first[num] + b;
				}
				first[num] = b2 % 10;
				b = b2 / 10;
				num++;
			}
			if (b > 0)
			{
				first.Add(b);
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000536C File Offset: 0x0000356C
		private static void SubtractSecondDecimalFromFirst(List<byte> first, List<byte> second)
		{
			byte b = 0;
			for (int i = 0; i < second.Count; i++)
			{
				int num = (int)(first[i] - second[i] - b);
				if (num < 0)
				{
					b = 1;
					first[i] = (byte)(num + 10);
				}
				else
				{
					b = 0;
					first[i] = (byte)num;
				}
			}
			if (b > 0)
			{
				for (int j = second.Count; j < first.Count; j++)
				{
					int num2 = (int)(first[j] - b);
					if (num2 >= 0)
					{
						first[j] = (byte)num2;
						return;
					}
					b = 1;
					first[j] = (byte)(num2 + 10);
				}
			}
		}

		// Token: 0x040000D5 RID: 213
		private static List<byte[]> powersOfTwo = new List<byte[]>(new byte[][]
		{
			new byte[]
			{
				1
			}
		});

		// Token: 0x040000D6 RID: 214
		private static readonly char[] digitMap = new char[]
		{
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9'
		};
	}
}
