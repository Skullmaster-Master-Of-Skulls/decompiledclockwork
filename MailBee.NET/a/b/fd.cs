using System;

namespace a.b
{
	// Token: 0x020002F5 RID: 757
	internal class fd
	{
		// Token: 0x06001AB1 RID: 6833 RVA: 0x00075105 File Offset: 0x00074105
		internal fd(byte[] A_0, int A_1)
		{
			this.a = A_0;
			this.b = A_1;
			this.c = this.a.Length;
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x00075129 File Offset: 0x00074129
		public int c()
		{
			return this.c - this.b;
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x00075138 File Offset: 0x00074138
		public int a()
		{
			byte[] array = this.a;
			int num = this.b;
			this.b = num + 1;
			return array[num] & 255;
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x00075164 File Offset: 0x00074164
		public int e()
		{
			int num = this.b;
			int num2 = (int)(this.a[num++] & byte.MaxValue);
			int num3 = (int)(this.a[num++] & byte.MaxValue);
			this.b = num;
			return (num3 << 8) + num2;
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x000751A8 File Offset: 0x000741A8
		public int a(fd A_0)
		{
			int num = A_0.a.Length - 1;
			int num2 = (int)(A_0.a[num++] & byte.MaxValue);
			byte[] array = this.a;
			int num3 = this.b;
			this.b = num3 + 1;
			return ((array[num3] & 255) << 8) + num2;
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x000751F8 File Offset: 0x000741F8
		public int d()
		{
			int num = this.b;
			int num2 = (int)(this.a[num++] & byte.MaxValue);
			int num3 = (int)(this.a[num++] & byte.MaxValue);
			int num4 = (int)(this.a[num++] & byte.MaxValue);
			int num5 = (int)(this.a[num++] & byte.MaxValue);
			this.b = num;
			return (num5 << 24) + (num4 << 16) + (num3 << 8) + num2;
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x0007526C File Offset: 0x0007426C
		public int a(fd A_0, int A_1)
		{
			byte[] array = new byte[4];
			this.a(A_0, A_1, array);
			int num = (int)(array[0] & byte.MaxValue);
			int num2 = (int)(array[1] & byte.MaxValue);
			int num3 = (int)(array[2] & byte.MaxValue);
			return ((int)(array[3] & byte.MaxValue) << 24) + (num3 << 16) + (num2 << 8) + num;
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x000752C0 File Offset: 0x000742C0
		public long b()
		{
			int num = this.b;
			int num2 = (int)(this.a[num++] & byte.MaxValue);
			int num3 = (int)(this.a[num++] & byte.MaxValue);
			int num4 = (int)(this.a[num++] & byte.MaxValue);
			int num5 = (int)(this.a[num++] & byte.MaxValue);
			int num6 = (int)(this.a[num++] & byte.MaxValue);
			int num7 = (int)(this.a[num++] & byte.MaxValue);
			int num8 = (int)(this.a[num++] & byte.MaxValue);
			long num9 = (long)(this.a[num++] & byte.MaxValue);
			this.b = num;
			return (num9 << 56) + ((long)num8 << 48) + ((long)num7 << 40) + ((long)num6 << 32) + ((long)num5 << 24) + (long)((long)num4 << 16) + (long)((long)num3 << 8) + (long)num2;
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x000753A4 File Offset: 0x000743A4
		public long b(fd A_0, int A_1)
		{
			byte[] array = new byte[8];
			this.a(A_0, A_1, array);
			int num = (int)(array[0] & byte.MaxValue);
			int num2 = (int)(array[1] & byte.MaxValue);
			int num3 = (int)(array[2] & byte.MaxValue);
			int num4 = (int)(array[3] & byte.MaxValue);
			int num5 = (int)(array[4] & byte.MaxValue);
			int num6 = (int)(array[5] & byte.MaxValue);
			int num7 = (int)(array[6] & byte.MaxValue);
			return ((long)(array[7] & byte.MaxValue) << 56) + ((long)num7 << 48) + ((long)num6 << 40) + ((long)num5 << 32) + ((long)num4 << 24) + (long)((long)num3 << 16) + (long)((long)num2 << 8) + (long)num;
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x00075444 File Offset: 0x00074444
		private void a(fd A_0, int A_1, byte[] A_2)
		{
			Array.Copy(A_0.a, A_0.b, A_2, 0, A_1);
			int length = A_2.Length - A_1;
			Array.Copy(this.a, 0, A_2, A_1, length);
			this.b = length;
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x00075481 File Offset: 0x00074481
		public void a(byte[] A_0, int A_1, int A_2)
		{
			Array.Copy(this.a, this.b, A_0, A_1, A_2);
			this.b += A_2;
		}

		// Token: 0x040012F1 RID: 4849
		private byte[] a;

		// Token: 0x040012F2 RID: 4850
		private int b;

		// Token: 0x040012F3 RID: 4851
		private int c;
	}
}
