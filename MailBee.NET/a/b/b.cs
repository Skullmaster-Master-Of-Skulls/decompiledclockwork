using System;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x0200031E RID: 798
	internal class b : gc
	{
		// Token: 0x06001CAB RID: 7339 RVA: 0x0007D0F5 File Offset: 0x0007C0F5
		public b(byte[] A_0, int A_1, int A_2)
		{
			this.a = A_0;
			this.c = A_1;
			this.b = A_1 + A_2;
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x0007D114 File Offset: 0x0007C114
		public b(byte[] A_0, int A_1) : this(A_0, A_1, A_0.Length - A_1)
		{
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x0007D123 File Offset: 0x0007C123
		public b(byte[] A_0) : this(A_0, 0, A_0.Length)
		{
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x0007D130 File Offset: 0x0007C130
		public int aq()
		{
			return this.b - this.c;
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x0007D13F File Offset: 0x0007C13F
		private void a(int A_0)
		{
			if (A_0 > this.b - this.c)
			{
				throw new RuntimeException("Buffer overrun");
			}
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x0007D15C File Offset: 0x0007C15C
		public int a()
		{
			return this.c;
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x0007D164 File Offset: 0x0007C164
		public int ReadByte()
		{
			this.a(1);
			byte[] array = this.a;
			int num = this.c;
			this.c = num + 1;
			return array[num];
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x0007D190 File Offset: 0x0007C190
		public int a0()
		{
			this.a(4);
			int num = this.c;
			int num2 = (int)(this.a[num++] & byte.MaxValue);
			int num3 = (int)(this.a[num++] & byte.MaxValue);
			int num4 = (int)(this.a[num++] & byte.MaxValue);
			int num5 = (int)(this.a[num++] & byte.MaxValue);
			this.c = num;
			return (num5 << 24) + (num4 << 16) + (num3 << 8) + num2;
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x0007D20C File Offset: 0x0007C20C
		public long ax()
		{
			this.a(8);
			int num = this.c;
			int num2 = (int)(this.a[num++] & byte.MaxValue);
			int num3 = (int)(this.a[num++] & byte.MaxValue);
			int num4 = (int)(this.a[num++] & byte.MaxValue);
			int num5 = (int)(this.a[num++] & byte.MaxValue);
			int num6 = (int)(this.a[num++] & byte.MaxValue);
			int num7 = (int)(this.a[num++] & byte.MaxValue);
			int num8 = (int)(this.a[num++] & byte.MaxValue);
			long num9 = (long)(this.a[num++] & byte.MaxValue);
			this.c = num;
			return (num9 << 56) + ((long)num8 << 48) + ((long)num7 << 40) + ((long)num6 << 32) + ((long)num5 << 24) + (long)((long)num4 << 16) + (long)((long)num3 << 8) + (long)num2;
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x0007D2F7 File Offset: 0x0007C2F7
		public short az()
		{
			return (short)this.a1();
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x0007D300 File Offset: 0x0007C300
		public int a2()
		{
			this.a(1);
			byte[] array = this.a;
			int num = this.c;
			this.c = num + 1;
			return array[num] & 255;
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x0007D334 File Offset: 0x0007C334
		public int a1()
		{
			this.a(2);
			int num = this.c;
			int num2 = (int)(this.a[num++] & byte.MaxValue);
			int num3 = (int)(this.a[num++] & byte.MaxValue);
			this.c = num;
			return (num3 << 8) + num2;
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x0007D37F File Offset: 0x0007C37F
		public void av(byte[] A_0, int A_1, int A_2)
		{
			this.a(A_2);
			Array.Copy(this.a, this.c, A_0, A_1, A_2);
			this.c += A_2;
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x0007D3AA File Offset: 0x0007C3AA
		public void ay(byte[] A_0)
		{
			this.av(A_0, 0, A_0.Length);
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x0007D3B7 File Offset: 0x0007C3B7
		public double aw()
		{
			return BitConverter.Int64BitsToDouble(this.ax());
		}

		// Token: 0x04001365 RID: 4965
		private byte[] a;

		// Token: 0x04001366 RID: 4966
		private int b;

		// Token: 0x04001367 RID: 4967
		private int c;
	}
}
