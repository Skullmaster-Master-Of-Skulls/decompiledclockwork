using System;
using System.Drawing;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x02000371 RID: 881
	internal sealed class es : gb
	{
		// Token: 0x06001FD7 RID: 8151 RVA: 0x00085DBC File Offset: 0x00084DBC
		public es(int A_0, int A_1, int A_2)
		{
			if (A_0 < 0 || A_0 > 255)
			{
				throw new RtfColorException(fa.j(A_0));
			}
			if (A_1 < 0 || A_1 > 255)
			{
				throw new RtfColorException(fa.j(A_1));
			}
			if (A_2 < 0 || A_2 > 255)
			{
				throw new RtfColorException(fa.j(A_2));
			}
			this.c = A_0;
			this.d = A_1;
			this.e = A_2;
			this.f = Color.FromArgb(A_0, A_1, A_2);
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x00085E3A File Offset: 0x00084E3A
		public int j3()
		{
			return this.c;
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x00085E42 File Offset: 0x00084E42
		public int j4()
		{
			return this.d;
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x00085E4A File Offset: 0x00084E4A
		public int j5()
		{
			return this.e;
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x00085E52 File Offset: 0x00084E52
		public Color j6()
		{
			return this.f;
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x00085E5A File Offset: 0x00084E5A
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && !(base.GetType() != obj.GetType()) && this.a(obj));
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x00085E81 File Offset: 0x00084E81
		public override int GetHashCode()
		{
			return f3.a(base.GetType().GetHashCode(), this.a());
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x00085E9C File Offset: 0x00084E9C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"Color{",
				this.c,
				",",
				this.d,
				",",
				this.e,
				"}"
			});
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x00085F00 File Offset: 0x00084F00
		private bool a(object A_0)
		{
			es es = A_0 as es;
			return es != null && this.c == es.c && this.d == es.d && this.e == es.e;
		}

		// Token: 0x06001FE0 RID: 8160 RVA: 0x00085F43 File Offset: 0x00084F43
		private int a()
		{
			return f3.a(f3.a(this.c, this.d), this.e);
		}

		// Token: 0x0400144B RID: 5195
		public static readonly gb a = new es(0, 0, 0);

		// Token: 0x0400144C RID: 5196
		public static readonly gb b = new es(255, 255, 255);

		// Token: 0x0400144D RID: 5197
		private readonly int c;

		// Token: 0x0400144E RID: 5198
		private readonly int d;

		// Token: 0x0400144F RID: 5199
		private readonly int e;

		// Token: 0x04001450 RID: 5200
		private readonly Color f;
	}
}
