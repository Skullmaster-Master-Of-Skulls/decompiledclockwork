using System;
using System.Globalization;
using System.IO;

namespace a.b
{
	// Token: 0x02000318 RID: 792
	internal class id : aa
	{
		// Token: 0x06001C3E RID: 7230 RVA: 0x0007C0E7 File Offset: 0x0007B0E7
		public id(int A_0)
		{
			if (A_0 < 0)
			{
				throw new IndexOutOfRangeException("negative offset");
			}
			this.b = A_0;
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x0007C105 File Offset: 0x0007B105
		public id(int A_0, int A_1) : this(A_0)
		{
			this.a = A_1;
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x0007C115 File Offset: 0x0007B115
		public id(int A_0, byte[] A_1) : this(A_0)
		{
			this.@in(A_1);
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x0007C125 File Offset: 0x0007B125
		public id(int A_0, int A_1, byte[] A_2) : this(A_0)
		{
			this.a(A_1, A_2);
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x0007C136 File Offset: 0x0007B136
		public int a()
		{
			return this.a;
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x0007C13E File Offset: 0x0007B13E
		public void a(int A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x0007C147 File Offset: 0x0007B147
		public void a(int A_0, byte[] A_1)
		{
			this.a = A_0;
			this.ip(A_1);
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x0007C157 File Offset: 0x0007B157
		public void @in(byte[] A_0)
		{
			this.a = p.i(A_0, this.b);
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x0007C16B File Offset: 0x0007B16B
		public void io(Stream A_0)
		{
			this.a = p.c(A_0);
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x0007C179 File Offset: 0x0007B179
		public void ip(byte[] A_0)
		{
			p.c(A_0, this.b, this.a);
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x0007C18D File Offset: 0x0007B18D
		public static void a(int A_0, int A_1, byte[] A_2)
		{
			p.c(A_2, A_0, A_1);
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x0007C197 File Offset: 0x0007B197
		public override string ToString()
		{
			return Convert.ToString(this.a, CultureInfo.CurrentCulture);
		}

		// Token: 0x04001355 RID: 4949
		private int a;

		// Token: 0x04001356 RID: 4950
		private int b;
	}
}
