using System;

namespace a.b
{
	// Token: 0x0200035E RID: 862
	internal sealed class br : iy
	{
		// Token: 0x06001F69 RID: 8041 RVA: 0x00085B19 File Offset: 0x00084B19
		public br() : base(ie.c)
		{
			this.b();
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x00085B28 File Offset: 0x00084B28
		public void b()
		{
			this.a = 1970;
			this.b = 1;
			this.c = 1;
			this.d = 0;
			this.e = 0;
			this.f = 0;
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x00085B58 File Offset: 0x00084B58
		public DateTime a()
		{
			return new DateTime(this.a, this.b, this.c, this.d, this.e, this.f);
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x00085B84 File Offset: 0x00084B84
		protected override void dz(c9 A_0)
		{
			string text = A_0.jz();
			if (text == "yr")
			{
				this.a = A_0.j2();
				return;
			}
			if (text == "mo")
			{
				this.b = A_0.j2();
				return;
			}
			if (text == "dy")
			{
				this.c = A_0.j2();
				return;
			}
			if (text == "hr")
			{
				this.d = A_0.j2();
				return;
			}
			if (text == "min")
			{
				this.e = A_0.j2();
				return;
			}
			if (!(text == "sec"))
			{
				return;
			}
			this.f = A_0.j2();
		}

		// Token: 0x0400143F RID: 5183
		private int a;

		// Token: 0x04001440 RID: 5184
		private int b;

		// Token: 0x04001441 RID: 5185
		private new int c;

		// Token: 0x04001442 RID: 5186
		private int d;

		// Token: 0x04001443 RID: 5187
		private int e;

		// Token: 0x04001444 RID: 5188
		private int f;
	}
}
