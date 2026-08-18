using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace a.b
{
	// Token: 0x02000316 RID: 790
	internal class f5
	{
		// Token: 0x06001C22 RID: 7202 RVA: 0x0007B708 File Offset: 0x0007A708
		private f5()
		{
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x0007B710 File Offset: 0x0007A710
		private static string b(byte A_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Length = 0;
			for (int i = 0; i < 2; i++)
			{
				stringBuilder.Append(f5.a[A_0 >> f5.b[i + 6] & 15]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x0007B75C File Offset: 0x0007A75C
		private static string c(long A_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Length = 0;
			for (int i = 0; i < 8; i++)
			{
				stringBuilder.Append(f5.a[(int)(A_0 >> f5.b[i + f5.b.Length - 8]) & 15]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x0007B7B0 File Offset: 0x0007A7B0
		public static string a(byte[] A_0, long A_1, int A_2)
		{
			if (A_2 < 0 || A_2 >= A_0.Length)
			{
				throw new IndexOutOfRangeException(string.Format(CultureInfo.InvariantCulture, "illegal index: {0} into array of length {1}", new object[]
				{
					A_2,
					A_0.Length
				}));
			}
			long num = A_1 + (long)A_2;
			StringBuilder stringBuilder = new StringBuilder(74);
			for (int i = A_2; i < A_0.Length; i += 16)
			{
				int num2 = A_0.Length - i;
				if (num2 > 16)
				{
					num2 = 16;
				}
				stringBuilder.Append(f5.c(num)).Append(' ');
				for (int j = 0; j < 16; j++)
				{
					if (j < num2)
					{
						stringBuilder.Append(f5.b(A_0[j + i]));
					}
					else
					{
						stringBuilder.Append("  ");
					}
					stringBuilder.Append(' ');
				}
				for (int k = 0; k < num2; k++)
				{
					if (A_0[k + i] >= 32 && A_0[k + i] < 127)
					{
						stringBuilder.Append((char)A_0[k + i]);
					}
					else
					{
						stringBuilder.Append('.');
					}
				}
				stringBuilder.Append(f5.c);
				num += (long)num2;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x0007B8CC File Offset: 0x0007A8CC
		public static void a(byte[] A_0, long A_1, Stream A_2, int A_3)
		{
			f5.a(A_0, A_1, A_2, A_3, A_0.Length - A_3);
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x0007B8DC File Offset: 0x0007A8DC
		public static void a(Stream A_0, int A_1, int A_2)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				if (A_2 == -1)
				{
					for (int num = A_0.ReadByte(); num != -1; num = A_0.ReadByte())
					{
						memoryStream.WriteByte((byte)num);
					}
				}
				else
				{
					int num2 = A_2;
					while (num2-- > 0)
					{
						int num3 = A_0.ReadByte();
						if (num3 == -1)
						{
							break;
						}
						memoryStream.WriteByte((byte)num3);
					}
				}
				byte[] array = memoryStream.ToArray();
				f5.a(array, 0L, null, A_1, array.Length);
			}
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x0007B964 File Offset: 0x0007A964
		public static void a(byte[] A_0, long A_1, Stream A_2, int A_3, int A_4)
		{
			if (A_0.Length == 0)
			{
				byte[] bytes = Encoding.UTF8.GetBytes(string.Format(CultureInfo.InvariantCulture, "No Data{0}", new object[]
				{
					Environment.NewLine
				}));
				if (A_2 != null)
				{
					A_2.Write(bytes, 0, bytes.Length);
					A_2.Flush();
				}
				return;
			}
			if (A_3 < 0 || A_3 >= A_0.Length)
			{
				throw new IndexOutOfRangeException(string.Format(CultureInfo.InvariantCulture, "illegal index: {0} into array of length {1}", new object[]
				{
					A_3,
					A_0.Length
				}));
			}
			if (A_0.Length != 0)
			{
				long num = A_1 + (long)A_3;
				StringBuilder stringBuilder = new StringBuilder(74);
				int num2 = Math.Min(A_0.Length, A_3 + A_4);
				for (int i = A_3; i < num2; i += 16)
				{
					int num3 = num2 - i;
					if (num3 > 16)
					{
						num3 = 16;
					}
					stringBuilder.Append(f5.c(num)).Append(' ');
					for (int j = 0; j < 16; j++)
					{
						if (j < num3)
						{
							stringBuilder.Append(f5.b(A_0[j + i]));
						}
						else
						{
							stringBuilder.Append("  ");
						}
						stringBuilder.Append(' ');
					}
					for (int k = 0; k < num3; k++)
					{
						if (A_0[k + i] >= 32 && A_0[k + i] < 127)
						{
							stringBuilder.Append((char)A_0[k + i]);
						}
						else
						{
							stringBuilder.Append('.');
						}
					}
					stringBuilder.Append(f5.c);
					byte[] bytes2 = Encoding.UTF8.GetBytes(stringBuilder.ToString());
					if (A_2 != null)
					{
						A_2.Write(bytes2, 0, bytes2.Length);
						A_2.Flush();
					}
					stringBuilder.Length = 0;
					num += (long)num3;
				}
			}
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x0007BB09 File Offset: 0x0007AB09
		public static char[] d(int A_0)
		{
			return f5.b((long)A_0, 2);
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x0007BB13 File Offset: 0x0007AB13
		public static char[] c(int A_0)
		{
			return f5.b((long)A_0, 1);
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x0007BB1D File Offset: 0x0007AB1D
		public static char[] b(int A_0)
		{
			return f5.b((long)A_0, 4);
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x0007BB27 File Offset: 0x0007AB27
		public static char[] b(long A_0)
		{
			return f5.b(A_0, 8);
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x0007BB30 File Offset: 0x0007AB30
		private static char[] b(long A_0, int A_1)
		{
			int num = 2 + A_1 * 2;
			char[] array = new char[num];
			long num2 = A_0;
			do
			{
				array[--num] = f5.a[(int)(num2 & 15L)];
				num2 >>= 4;
			}
			while (num > 1);
			array[0] = '0';
			array[1] = 'x';
			return array;
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x0007BB72 File Offset: 0x0007AB72
		public static string a(byte A_0)
		{
			return f5.a((long)((ulong)A_0), 2);
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x0007BB7C File Offset: 0x0007AB7C
		public static string a(short A_0)
		{
			return f5.a((long)A_0, 4);
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x0007BB86 File Offset: 0x0007AB86
		public static string a(int A_0)
		{
			return f5.a((long)A_0, 8);
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x0007BB90 File Offset: 0x0007AB90
		public static string a(long A_0)
		{
			return f5.a(A_0, 16);
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x0007BB9C File Offset: 0x0007AB9C
		public static string a(byte[] A_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			for (int i = 0; i < A_0.Length; i++)
			{
				stringBuilder.Append(f5.a(A_0[i]));
				stringBuilder.Append(", ");
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x0007BBF0 File Offset: 0x0007ABF0
		public static string a(short[] A_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			for (int i = 0; i < A_0.Length; i++)
			{
				stringBuilder.Append(f5.a(A_0[i]));
				stringBuilder.Append(", ");
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x0007BC44 File Offset: 0x0007AC44
		private static string a(long A_0, int A_1)
		{
			StringBuilder stringBuilder = new StringBuilder(A_1);
			for (int i = 0; i < A_1; i++)
			{
				stringBuilder.Append(f5.a[(int)(A_0 >> f5.b[i + (16 - A_1)] & 15L)]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x0007BC90 File Offset: 0x0007AC90
		public static string a(byte[] A_0, int A_1)
		{
			int num = (int)Math.Round(Math.Log((double)A_0.Length) / Math.Log(10.0) + 0.5);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < num; i++)
			{
				stringBuilder.Append('0');
			}
			stringBuilder.Append(": ");
			StringBuilder stringBuilder2 = new StringBuilder();
			stringBuilder2.Append(0.0.ToString(stringBuilder.ToString(), CultureInfo.InvariantCulture));
			int num2 = -1;
			for (int j = 0; j < A_0.Length; j++)
			{
				if (++num2 == A_1)
				{
					stringBuilder2.Append('\n');
					stringBuilder2.Append(((double)j).ToString(stringBuilder.ToString(), CultureInfo.InvariantCulture));
					num2 = 0;
				}
				stringBuilder2.Append(f5.a(A_0[j]));
				stringBuilder2.Append(", ");
			}
			return stringBuilder2.ToString();
		}

		// Token: 0x04001352 RID: 4946
		private static readonly char[] a = new char[]
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
			'9',
			'A',
			'B',
			'C',
			'D',
			'E',
			'F',
			'\0',
			'\0'
		};

		// Token: 0x04001353 RID: 4947
		private static readonly int[] b = new int[]
		{
			60,
			56,
			52,
			48,
			44,
			40,
			36,
			32,
			28,
			24,
			20,
			16,
			12,
			8,
			4,
			0
		};

		// Token: 0x04001354 RID: 4948
		public static readonly string c = Environment.NewLine;
	}
}
