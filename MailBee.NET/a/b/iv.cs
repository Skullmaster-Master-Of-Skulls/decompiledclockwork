using System;
using System.Globalization;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x0200031F RID: 799
	internal class iv : d2
	{
		// Token: 0x06001CBA RID: 7354 RVA: 0x0007D3C4 File Offset: 0x0007C3C4
		public iv(byte[] A_0, int A_1, int A_2)
		{
			if (A_1 < 0 || A_1 > A_0.Length)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"Specified startOffset (",
					A_1,
					") is out of allowable range (0..",
					A_0.Length,
					")"
				}));
			}
			this.a = A_0;
			this.c = A_1;
			this.b = A_1 + A_2;
			if (this.b < A_1 || this.b > A_0.Length)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"calculated end index (",
					this.b,
					") is out of allowable range (",
					this.c,
					"..",
					A_0.Length,
					")"
				}));
			}
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x0007D4A1 File Offset: 0x0007C4A1
		public iv(byte[] A_0, int A_1) : this(A_0, A_1, A_0.Length - A_1)
		{
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x0007D4B0 File Offset: 0x0007C4B0
		private void a(int A_0)
		{
			if (A_0 > this.b - this.c)
			{
				throw new RuntimeException(string.Format(CultureInfo.InvariantCulture, "Buffer overrun i={0};endIndex={1};writeIndex={2}", new object[]
				{
					A_0,
					this.b,
					this.c
				}));
			}
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x0007D510 File Offset: 0x0007C510
		public void pj(int A_0)
		{
			this.a(1);
			byte[] array = this.a;
			int num = this.c;
			this.c = num + 1;
			array[num] = (byte)A_0;
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x0007D53E File Offset: 0x0007C53E
		public void pk(double A_0)
		{
			this.pm(BitConverter.DoubleToInt64Bits(A_0));
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x0007D54C File Offset: 0x0007C54C
		public void pl(int A_0)
		{
			this.a(4);
			int num = this.c;
			this.a[num++] = (byte)(A_0 & 255);
			this.a[num++] = (byte)(A_0 >> 8 & 255);
			this.a[num++] = (byte)(A_0 >> 16 & 255);
			this.a[num++] = (byte)(A_0 >> 24 & 255);
			this.c = num;
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x0007D5C6 File Offset: 0x0007C5C6
		public void pm(long A_0)
		{
			this.pl((int)A_0);
			this.pl((int)(A_0 >> 32));
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x0007D5DC File Offset: 0x0007C5DC
		public void pn(int A_0)
		{
			this.a(2);
			int num = this.c;
			this.a[num++] = (byte)(A_0 & 255);
			this.a[num++] = (byte)(A_0 >> 8 & 255);
			this.c = num;
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x0007D628 File Offset: 0x0007C628
		public void po(byte[] A_0)
		{
			int num = A_0.Length;
			this.a(num);
			Array.Copy(A_0, 0, this.a, this.c, num);
			this.c += num;
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x0007D662 File Offset: 0x0007C662
		public void pp(byte[] A_0, int A_1, int A_2)
		{
			this.a(A_2);
			Array.Copy(A_0, A_1, this.a, this.c, A_2);
			this.c += A_2;
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x0007D68D File Offset: 0x0007C68D
		public int a()
		{
			return this.c;
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x0007D695 File Offset: 0x0007C695
		public c2 pq(int A_0)
		{
			this.a(A_0);
			c2 result = new iv(this.a, this.c, A_0);
			this.c += A_0;
			return result;
		}

		// Token: 0x04001368 RID: 4968
		private byte[] a;

		// Token: 0x04001369 RID: 4969
		private int b;

		// Token: 0x0400136A RID: 4970
		private int c;
	}
}
