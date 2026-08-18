using System;
using System.IO;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x0200031C RID: 796
	internal class p : eh
	{
		// Token: 0x06001C77 RID: 7287 RVA: 0x0007C9F9 File Offset: 0x0007B9F9
		private p()
		{
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x0007CA04 File Offset: 0x0007BA04
		public static short k(byte[] A_0, int A_1)
		{
			int num = (int)(A_0[A_1] & byte.MaxValue);
			return (short)(((int)(A_0[A_1 + 1] & byte.MaxValue) << 8) + num);
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x0007CA2C File Offset: 0x0007BA2C
		public static int j(byte[] A_0, int A_1)
		{
			int num = (int)(A_0[A_1] & byte.MaxValue);
			return ((int)(A_0[A_1 + 1] & byte.MaxValue) << 8) + num;
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x0007CA52 File Offset: 0x0007BA52
		public static short h(byte[] A_0)
		{
			return p.k(A_0, 0);
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x0007CA5B File Offset: 0x0007BA5B
		public new static int g(byte[] A_0)
		{
			return p.j(A_0, 0);
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x0007CA64 File Offset: 0x0007BA64
		public static int i(byte[] A_0, int A_1)
		{
			int num = A_1 + 1;
			int num2 = (int)(A_0[A_1] & byte.MaxValue);
			int num3 = (int)(A_0[num++] & byte.MaxValue);
			int num4 = (int)(A_0[num++] & byte.MaxValue);
			return ((int)(A_0[num++] & byte.MaxValue) << 24) + (num4 << 16) + (num3 << 8) + num2;
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x0007CAB8 File Offset: 0x0007BAB8
		public new static int f(byte[] A_0)
		{
			return p.i(A_0, 0);
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x0007CAC1 File Offset: 0x0007BAC1
		public static long h(byte[] A_0, int A_1)
		{
			return (long)p.i(A_0, A_1) & (long)((ulong)-1);
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x0007CACE File Offset: 0x0007BACE
		public new static long e(byte[] A_0)
		{
			return p.h(A_0, 0);
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x0007CAD8 File Offset: 0x0007BAD8
		public new static long g(byte[] A_0, int A_1)
		{
			long num = 0L;
			for (int i = A_1 + 8 - 1; i >= A_1; i--)
			{
				num <<= 8;
				num |= (long)(255UL & (ulong)A_0[i]);
			}
			return num;
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x0007CB0B File Offset: 0x0007BB0B
		public new static double f(byte[] A_0, int A_1)
		{
			return BitConverter.Int64BitsToDouble(p.g(A_0, A_1));
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x0007CB1C File Offset: 0x0007BB1C
		public new static void a(byte[] A_0, int A_1, short A_2)
		{
			int num = A_1 + 1;
			A_0[A_1] = (byte)(A_2 & 255);
			A_0[num++] = (byte)(A_2 >> 8 & 255);
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x0007CB4B File Offset: 0x0007BB4B
		public new static void e(byte[] A_0, int A_1, int A_2)
		{
			A_0[A_1] = (byte)A_2;
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x0007CB54 File Offset: 0x0007BB54
		public new static void d(byte[] A_0, int A_1, int A_2)
		{
			int num = A_1 + 1;
			A_0[A_1] = (byte)(A_2 & 255);
			A_0[num++] = (byte)(A_2 >> 8 & 255);
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x0007CB83 File Offset: 0x0007BB83
		[Obsolete]
		public new static void a(byte[] A_0, short A_1)
		{
			p.a(A_0, 0, A_1);
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x0007CB8D File Offset: 0x0007BB8D
		public new static void a(Stream A_0, short A_1)
		{
			A_0.WriteByte((byte)(A_1 & 255));
			A_0.WriteByte((byte)(A_1 >> 8 & 255));
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x0007CBB0 File Offset: 0x0007BBB0
		public new static void c(byte[] A_0, int A_1, int A_2)
		{
			int num = A_1 + 1;
			A_0[A_1] = (byte)(A_2 & 255);
			A_0[num++] = (byte)(A_2 >> 8 & 255);
			A_0[num++] = (byte)(A_2 >> 16 & 255);
			A_0[num++] = (byte)(A_2 >> 24 & 255);
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x0007CC03 File Offset: 0x0007BC03
		[Obsolete]
		public new static void e(byte[] A_0, int A_1)
		{
			p.c(A_0, 0, A_1);
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x0007CC10 File Offset: 0x0007BC10
		public new static void b(int A_0, Stream A_1)
		{
			A_1.WriteByte((byte)(A_0 & 255));
			A_1.WriteByte((byte)(A_0 >> 8 & 255));
			A_1.WriteByte((byte)(A_0 >> 16 & 255));
			A_1.WriteByte((byte)(A_0 >> 24 & 255));
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x0007CC60 File Offset: 0x0007BC60
		public new static void a(byte[] A_0, int A_1, long A_2)
		{
			int num = 8 + A_1;
			long num2 = A_2;
			for (int i = A_1; i < num; i++)
			{
				A_0[i] = (byte)(num2 & 255L);
				num2 >>= 8;
			}
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x0007CC90 File Offset: 0x0007BC90
		public new static void a(byte[] A_0, int A_1, double A_2)
		{
			long a_;
			if (double.IsNaN(A_2))
			{
				a_ = -276939487313920L;
			}
			else
			{
				a_ = BitConverter.DoubleToInt64Bits(A_2);
			}
			p.a(A_0, A_1, a_);
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x0007CCC3 File Offset: 0x0007BCC3
		public new static short e(Stream A_0)
		{
			return (short)p.d(A_0);
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x0007CCCC File Offset: 0x0007BCCC
		public new static int d(Stream A_0)
		{
			int num = A_0.ReadByte();
			int num2 = A_0.ReadByte();
			if ((num | num2) < 0)
			{
				throw new BufferUnderrunException();
			}
			return (num2 << 8) + num;
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x0007CCF8 File Offset: 0x0007BCF8
		public new static int c(Stream A_0)
		{
			int num = A_0.ReadByte();
			int num2 = A_0.ReadByte();
			int num3 = A_0.ReadByte();
			int num4 = A_0.ReadByte();
			if ((num | num2 | num3 | num4) < 0)
			{
				throw new BufferUnderrunException();
			}
			return (num4 << 24) + (num3 << 16) + (num2 << 8) + num;
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x0007CD40 File Offset: 0x0007BD40
		public new static long b(Stream A_0)
		{
			int num = A_0.ReadByte();
			int num2 = A_0.ReadByte();
			int num3 = A_0.ReadByte();
			int num4 = A_0.ReadByte();
			int num5 = A_0.ReadByte();
			int num6 = A_0.ReadByte();
			int num7 = A_0.ReadByte();
			int num8 = A_0.ReadByte();
			if ((num | num2 | num3 | num4 | num5 | num6 | num7 | num8) < 0)
			{
				throw new BufferUnderrunException();
			}
			return ((long)num8 << 56) + ((long)num7 << 48) + ((long)num6 << 40) + ((long)num5 << 32) + ((long)num4 << 24) + (long)((long)num3 << 16) + (long)((long)num2 << 8) + (long)num;
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x0007CDD4 File Offset: 0x0007BDD4
		public new static int a(byte A_0)
		{
			if ((A_0 & 128) == 0)
			{
				return (int)A_0;
			}
			return (int)((A_0 & 127) + 128);
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x0007CDEB File Offset: 0x0007BDEB
		[Obsolete]
		public new static int d(byte[] A_0, int A_1)
		{
			return (int)(A_0[A_1] & byte.MaxValue);
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x0007CDF8 File Offset: 0x0007BDF8
		public new static byte[] b(byte[] A_0, int A_1, int A_2)
		{
			byte[] array = new byte[A_2];
			Array.Copy(A_0, A_1, array, 0, A_2);
			return array;
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x0007CE17 File Offset: 0x0007BE17
		[Obsolete]
		public new static double d(byte[] A_0)
		{
			return p.f(A_0, 0);
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x0007CE20 File Offset: 0x0007BE20
		[Obsolete]
		public new static long c(byte[] A_0)
		{
			return p.g(A_0, 0);
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x0007CE29 File Offset: 0x0007BE29
		[Obsolete]
		public new static ulong b(byte[] A_0)
		{
			return p.c(A_0, 0);
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x0007CE32 File Offset: 0x0007BE32
		[Obsolete]
		public new static ulong c(byte[] A_0, int A_1)
		{
			return BitConverter.ToUInt64(A_0, A_1);
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x0007CE3C File Offset: 0x0007BE3C
		private new static long a(byte[] A_0, int A_1, int A_2)
		{
			long num = 0L;
			for (int i = A_1 + A_2 - 1; i >= A_1; i--)
			{
				num <<= 8;
				num |= (long)(255UL & (ulong)A_0[i]);
			}
			return num;
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x0007CE6F File Offset: 0x0007BE6F
		public new static short a(byte[] A_0)
		{
			return (short)(A_0[0] & byte.MaxValue);
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x0007CE7B File Offset: 0x0007BE7B
		public new static short b(byte[] A_0, int A_1)
		{
			return (short)(A_0[A_1] & byte.MaxValue);
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x0007CE87 File Offset: 0x0007BE87
		[Obsolete]
		public new static void a(byte[] A_0, double A_1)
		{
			p.a(A_0, 0, A_1);
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x0007CE91 File Offset: 0x0007BE91
		public new static void a(double A_0, Stream A_1)
		{
			p.a(BitConverter.DoubleToInt64Bits(A_0), A_1);
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x0007CE9F File Offset: 0x0007BE9F
		[Obsolete]
		public new static void a(byte[] A_0, uint A_1)
		{
			p.a(A_0, 0, A_1);
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x0007CEAC File Offset: 0x0007BEAC
		public new static void b(long A_0, Stream A_1)
		{
			A_1.WriteByte((byte)(A_0 & 255L));
			A_1.WriteByte((byte)(A_0 >> 8 & 255L));
			A_1.WriteByte((byte)(A_0 >> 16 & 255L));
			A_1.WriteByte((byte)(A_0 >> 24 & 255L));
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x0007CEFD File Offset: 0x0007BEFD
		[Obsolete]
		public new static void a(byte[] A_0, int A_1, uint A_2)
		{
			p.a(A_0, A_1, Convert.ToInt64(A_2), 4);
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x0007CF0D File Offset: 0x0007BF0D
		[Obsolete]
		public new static void a(byte[] A_0, long A_1)
		{
			p.a(A_0, 0, A_1);
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x0007CF18 File Offset: 0x0007BF18
		public new static void a(long A_0, Stream A_1)
		{
			A_1.WriteByte((byte)(A_0 & 255L));
			A_1.WriteByte((byte)(A_0 >> 8 & 255L));
			A_1.WriteByte((byte)(A_0 >> 16 & 255L));
			A_1.WriteByte((byte)(A_0 >> 24 & 255L));
			A_1.WriteByte((byte)(A_0 >> 32 & 255L));
			A_1.WriteByte((byte)(A_0 >> 40 & 255L));
			A_1.WriteByte((byte)(A_0 >> 48 & 255L));
			A_1.WriteByte((byte)(A_0 >> 56 & 255L));
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x0007CFB1 File Offset: 0x0007BFB1
		[Obsolete]
		public new static void a(byte[] A_0, ulong A_1)
		{
			p.a(A_0, 0, A_1);
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x0007CFBB File Offset: 0x0007BFBB
		[Obsolete]
		public new static void a(byte[] A_0, int A_1, ulong A_2)
		{
			p.a(A_0, A_1, A_2, 8);
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x0007CFC8 File Offset: 0x0007BFC8
		private new static void a(byte[] A_0, int A_1, long A_2, int A_3)
		{
			int num = A_3 + A_1;
			long num2 = A_2;
			for (int i = A_1; i < num; i++)
			{
				A_0[i] = (byte)(num2 & 255L);
				num2 >>= 8;
			}
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x0007CFF8 File Offset: 0x0007BFF8
		private new static void a(byte[] A_0, int A_1, ulong A_2, int A_3)
		{
			int num = A_3 + A_1;
			ulong num2 = A_2;
			for (int i = A_1; i < num; i++)
			{
				A_0[i] = (byte)(num2 & 255UL);
				num2 >>= 8;
			}
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x0007D028 File Offset: 0x0007C028
		[Obsolete]
		public new static void a(byte[] A_0, int A_1, short[] A_2)
		{
			p.a(A_0, A_1, Convert.ToInt64(A_2.Length), 2);
			for (int i = 0; i < A_2.Length; i++)
			{
				p.a(A_0, A_1 + 2 + i * 2, Convert.ToInt64(A_2[i]), 2);
			}
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x0007D069 File Offset: 0x0007C069
		[Obsolete]
		public new static void a(byte[] A_0, int A_1)
		{
			p.a(A_0, 0, Convert.ToInt64(A_1), 2);
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x0007D079 File Offset: 0x0007C079
		public new static void a(int A_0, Stream A_1)
		{
			A_1.WriteByte((byte)(A_0 & 255));
			A_1.WriteByte((byte)(A_0 >> 8 & 255));
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x0007D09C File Offset: 0x0007C09C
		[Obsolete]
		public new static byte[] a(Stream A_0, int A_1)
		{
			byte[] array = new byte[A_1];
			int num = A_0.Read(array, 0, array.Length);
			if (num == 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = 0;
				}
				return array;
			}
			if (num != A_1)
			{
				throw new BufferUnderrunException();
			}
			return array;
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x0007D0DE File Offset: 0x0007C0DE
		[Obsolete]
		public new static ulong a(Stream A_0)
		{
			return BitConverter.ToUInt64(p.a(A_0, 8), 0);
		}
	}
}
