using System;
using System.IO;
using System.Text;
using MailBee;
using MailBee.Tnef;

namespace a.h
{
	// Token: 0x02000204 RID: 516
	internal class f
	{
		// Token: 0x060010CF RID: 4303 RVA: 0x00046E13 File Offset: 0x00045E13
		public static byte e(byte[] A_0, int A_1)
		{
			return A_0[A_1] & byte.MaxValue;
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x00046E1F File Offset: 0x00045E1F
		public static ushort a(byte A_0, byte A_1)
		{
			return (ushort)(((int)(A_0 & byte.MaxValue) | (int)(A_1 & byte.MaxValue) << 8) & 65535);
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x00046E39 File Offset: 0x00045E39
		public static ushort d(byte[] A_0, int A_1)
		{
			return (ushort)(((int)(A_0[A_1] & byte.MaxValue) | (int)(A_0[A_1 + 1] & byte.MaxValue) << 8) & 65535);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x00046E59 File Offset: 0x00045E59
		public static uint a(byte A_0, byte A_1, byte A_2, byte A_3)
		{
			return (uint)((long)((int)(A_0 & byte.MaxValue) | (int)(A_1 & byte.MaxValue) << 8 | (int)(A_2 & byte.MaxValue) << 16 | (int)(A_3 & byte.MaxValue) << 24) & (long)((ulong)-1));
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x00046E87 File Offset: 0x00045E87
		public static uint c(byte[] A_0, int A_1)
		{
			return (uint)((long)((int)(A_0[A_1] & byte.MaxValue) | (int)(A_0[A_1 + 1] & byte.MaxValue) << 8 | (int)(A_0[A_1 + 2] & byte.MaxValue) << 16 | (int)(A_0[A_1 + 3] & byte.MaxValue) << 24) & (long)((ulong)-1));
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x00046EC3 File Offset: 0x00045EC3
		public static ulong b(byte[] A_0, int A_1)
		{
			return ((ulong)f.c(A_0, A_1 + 4) & (ulong)-1) << 32 | (ulong)(f.c(A_0, A_1) & uint.MaxValue);
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00046EE0 File Offset: 0x00045EE0
		public static int b(int A_0, int A_1)
		{
			return A_0 << 16 | A_1;
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x00046EE8 File Offset: 0x00045EE8
		public static int b(int A_0)
		{
			return A_0 & 65535;
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x00046EF1 File Offset: 0x00045EF1
		public static int a(int A_0)
		{
			return A_0 >> 16 & 65535;
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00046EFD File Offset: 0x00045EFD
		public static int c(byte[] A_0)
		{
			return f.d(A_0, 0, A_0.Length);
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00046F0C File Offset: 0x00045F0C
		public static int d(byte[] A_0, int A_1, int A_2)
		{
			long num = 0L;
			A_2 += A_1;
			for (int i = A_1; i < A_2; i++)
			{
				num += (long)(A_0[i] & byte.MaxValue);
			}
			return (int)(num % 65536L);
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x00046F44 File Offset: 0x00045F44
		public static int a(n A_0)
		{
			int num = 0;
			byte[] array = new byte[4096];
			n n = new n(A_0);
			try
			{
				int a_;
				while ((a_ = n.Read(array, 0, array.Length)) != 0)
				{
					num = (num + f.d(array, 0, a_)) % 65536;
				}
			}
			finally
			{
				n.Close();
			}
			return num;
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x00046FA4 File Offset: 0x00045FA4
		public static string c(string A_0)
		{
			if (A_0 == null)
			{
				return null;
			}
			int num = A_0.Length;
			while (num > 0 && A_0[num - 1] == '\0')
			{
				num--;
			}
			if (num != A_0.Length)
			{
				return A_0.Substring(0, num);
			}
			return A_0;
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x00046FE8 File Offset: 0x00045FE8
		public static string a(string A_0, string A_1, string A_2)
		{
			if (A_0 == null || A_1 == null || A_1.Length == 0)
			{
				return A_0;
			}
			if (A_2 == null)
			{
				A_2 = string.Empty;
			}
			int length = A_0.Length;
			int length2 = A_1.Length;
			int length3 = A_2.Length;
			int num = 0;
			while (num < length && (num = A_0.IndexOf(A_1, num)) > -1)
			{
				A_0 = A_0.Substring(0, num) + A_2 + A_0.Substring(num + length2);
				num += length3;
			}
			return A_0;
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x00047058 File Offset: 0x00046058
		public static string c(byte[] A_0, int A_1, int A_2)
		{
			try
			{
				return f.c(Global.DefaultEncoding.GetString(A_0, A_1, A_2));
			}
			catch (IOException)
			{
			}
			return string.Empty;
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x00047094 File Offset: 0x00046094
		public static string b(byte[] A_0, int A_1, int A_2)
		{
			try
			{
				return f.c(Encoding.GetEncoding("UTF-16LE").GetString(A_0, A_1, A_2));
			}
			catch (IOException)
			{
			}
			return string.Empty;
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x000470D8 File Offset: 0x000460D8
		public static string a(long A_0)
		{
			return "0x" + Convert.ToString(A_0, 16);
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x000470EC File Offset: 0x000460EC
		public static string b(byte[] A_0)
		{
			return f.a(A_0, 0, (A_0 != null) ? A_0.Length : 0, -1);
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x000470FF File Offset: 0x000460FF
		public static string a(byte[] A_0, int A_1)
		{
			return f.a(A_0, 0, (A_0 != null) ? A_0.Length : 0, A_1);
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x00047114 File Offset: 0x00046114
		public static string a(byte[] A_0, int A_1, int A_2, int A_3)
		{
			int num = (A_3 > -1) ? Math.Min(A_3, A_2) : A_2;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			if (A_0 == null)
			{
				stringBuilder.Append(A_0);
			}
			else
			{
				for (int i = 0; i < num; i++)
				{
					string text = Convert.ToString((int)(A_0[A_1 + i] & byte.MaxValue), 16).ToUpper();
					if (text.Length == 1)
					{
						stringBuilder.Append('0');
					}
					stringBuilder.Append(text);
				}
				if (num < A_2)
				{
					stringBuilder.Append("... (" + A_2 + " bytes)");
				}
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x000471BC File Offset: 0x000461BC
		public static bool a(byte[] A_0, int A_1, byte[] A_2, int A_3, int A_4)
		{
			bool flag = true;
			int num = 0;
			while (flag && num < A_4)
			{
				flag = (A_0[A_1 + num] == A_2[A_3 + num]);
				num++;
			}
			return flag;
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x000471E9 File Offset: 0x000461E9
		public static bool b(string A_0)
		{
			return A_0 != null && ((A_0 = A_0.ToLower()).StartsWith("application/ms-tnef") || A_0.StartsWith("application/vnd.ms-tnef"));
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x00047214 File Offset: 0x00046214
		public static byte[] a(string A_0)
		{
			return new Guid(A_0).ToByteArray();
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x00047230 File Offset: 0x00046230
		public static int a(byte[] A_0, int A_1, int A_2)
		{
			int num = 0;
			int num2 = A_1 + A_2;
			for (int i = A_1; i < num2; i++)
			{
				num = (f.a[(num ^ (int)A_0[i]) & 255] ^ f.a(num, 8));
			}
			return num;
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x0004726C File Offset: 0x0004626C
		public static byte[] a(byte[] A_0)
		{
			int num = 0;
			int i = 0;
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (A_0.Length < 16)
			{
				throw new MailBeeTnefParsingException(Resources.Instance.ErrorDesc_TnefCompressedRtfHeaderInvalid, 1007);
			}
			int num2 = (int)f.c(A_0, num);
			num += 4;
			int num3 = (int)f.c(A_0, num);
			num += 4;
			int num4 = (int)f.c(A_0, num);
			num += 4;
			int num5 = (int)f.c(A_0, num);
			num += 4;
			if (num2 != A_0.Length - 4)
			{
				throw new MailBeeTnefParsingException(Resources.Instance.ErrorDesc_TnefCompressedRtfDataSizeMismatch, 1008);
			}
			if (num5 != f.a(A_0, 16, A_0.Length - 16))
			{
				throw new MailBeeTnefParsingException(Resources.Instance.ErrorDesc_TnefCompressedRtfCrc32Failed, 1009);
			}
			byte[] array;
			if (num4 == 1095517517)
			{
				array = new byte[num3];
				Array.Copy(A_0, num, array, i, num3);
			}
			else
			{
				if (num4 != 1967544908)
				{
					throw new MailBeeTnefParsingException(string.Format(Resources.Instance.ErrorDesc_TnefUnknownRtfCompressionType0, num4), 1010);
				}
				array = new byte[f.b.Length + num3];
				Array.Copy(f.b, 0, array, 0, f.b.Length);
				i = f.b.Length;
				int num6 = 0;
				int num7 = 0;
				while (i < array.Length)
				{
					num7 = ((num6++ % 8 == 0) ? ((int)f.e(A_0, num++)) : (num7 >> 1));
					if ((num7 & 1) == 1)
					{
						int j = (int)f.e(A_0, num++);
						int num8 = (int)f.e(A_0, num++);
						j = (j << 4 | f.a(num8, 4));
						num8 = (num8 & 15) + 2;
						j = i / 4096 * 4096 + j;
						if (j >= i)
						{
							j -= 4096;
						}
						int num9 = j + num8;
						while (j < num9)
						{
							array[i++] = array[j++];
						}
					}
					else
					{
						array[i++] = A_0[num++];
					}
				}
				A_0 = array;
				array = new byte[num3];
				Array.Copy(A_0, f.b.Length, array, 0, num3);
			}
			return array;
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0004746C File Offset: 0x0004646C
		public static byte[] a(byte[] A_0, bool A_1)
		{
			int value = A_0.Length + 12;
			int num = A_0.Length;
			if (!A_1)
			{
				int value2 = 1095517517;
				int value3 = f.a(A_0, 0, A_0.Length);
				byte[] array = new byte[num + 16];
				Array.Copy(BitConverter.GetBytes(value), array, 4);
				Array.Copy(BitConverter.GetBytes(num), 0, array, 4, 4);
				Array.Copy(BitConverter.GetBytes(value2), 0, array, 8, 4);
				Array.Copy(BitConverter.GetBytes(value3), 0, array, 12, 4);
				Array.Copy(A_0, 0, array, 16, A_0.Length);
				return array;
			}
			throw new NotImplementedException();
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x000474F7 File Offset: 0x000464F7
		public static int a(int A_0, int A_1)
		{
			if (A_0 >= 0)
			{
				return A_0 >> A_1;
			}
			return (A_0 >> A_1) + (2 << ~A_1);
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x00047512 File Offset: 0x00046512
		public static int a(int A_0, long A_1)
		{
			return f.a(A_0, (int)A_1);
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0004751C File Offset: 0x0004651C
		public static long a(long A_0, int A_1)
		{
			if (A_0 >= 0L)
			{
				return A_0 >> A_1;
			}
			return (A_0 >> A_1) + (2L << ~A_1);
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x00047539 File Offset: 0x00046539
		public static long a(long A_0, long A_1)
		{
			return f.a(A_0, (int)A_1);
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x00047544 File Offset: 0x00046544
		static f()
		{
			for (int i = 0; i < 256; i++)
			{
				int num = i;
				for (int j = 0; j < 8; j++)
				{
					num = (((num & 1) == 1) ? ((int)((ulong)-306674912 ^ (ulong)((long)f.a(num, 1)))) : f.a(num, 1));
				}
				f.a[i] = num;
			}
			string s = "{\\rtf1\\ansi\\mac\\deff0\\deftab720{\\fonttbl;}{\\f0\\fnil \\froman \\fswiss \\fmodern \\fscript \\fdecor MS Sans SerifSymbolArialTimes New RomanCourier{\\colortbl\\red0\\green0\\blue0\n\r\\par \\pard\\plain\\f0\\fs20\\b\\i\\u\\tab\\tx";
			f.b = Encoding.GetEncoding("US-ASCII").GetBytes(s);
		}

		// Token: 0x04000E52 RID: 3666
		internal static int[] a = new int[256];

		// Token: 0x04000E53 RID: 3667
		internal static byte[] b;
	}
}
