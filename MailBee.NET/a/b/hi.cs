using System;
using System.Text;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x0200035A RID: 858
	internal sealed class hi : en
	{
		// Token: 0x06001F38 RID: 7992 RVA: 0x000854B4 File Offset: 0x000844B4
		public bool c()
		{
			return this.a;
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x000854BC File Offset: 0x000844BC
		public void a(bool A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x000854C5 File Offset: 0x000844C5
		public ip b()
		{
			return this.b;
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x000854CD File Offset: 0x000844CD
		protected override void db(eq A_0)
		{
			this.b = null;
			this.c = new ao();
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x000854E4 File Offset: 0x000844E4
		protected override void jt(eq A_0, string A_1)
		{
			if (this.a)
			{
				ej ej = A_0.k0();
				if (!ej.Equals(this.d))
				{
					this.a();
				}
				this.d = ej;
				this.e.Append(A_1);
				return;
			}
			this.c.a(new ea(A_1, A_0.k0()));
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x00085540 File Offset: 0x00084540
		protected override void ju(eq A_0, RtfVisualSpecialCharKind A_1)
		{
			this.a();
			this.c.a(new ic(A_1));
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x00085559 File Offset: 0x00084559
		protected override void jv(eq A_0, RtfVisualBreakKind A_1)
		{
			this.a();
			this.c.a(new bm(A_1));
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x00085574 File Offset: 0x00084574
		protected override void dc(eq A_0, de A_1, int A_2, int A_3, int A_4, int A_5, int A_6, int A_7, string A_8)
		{
			this.a();
			this.c.a(new gt(A_1, A_0.k0().g8(), A_2, A_3, A_4, A_5, A_6, A_7, A_8));
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x000855B0 File Offset: 0x000845B0
		protected override void kq(eq A_0)
		{
			this.a();
			this.b = new be(A_0, this.c);
			this.c = null;
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x000855D4 File Offset: 0x000845D4
		private void a()
		{
			if (this.d != null)
			{
				this.c.a(new ea(this.e.ToString(), this.d));
				this.d = null;
				this.e.Remove(0, this.e.Length);
			}
		}

		// Token: 0x0400142C RID: 5164
		private bool a = true;

		// Token: 0x0400142D RID: 5165
		private be b;

		// Token: 0x0400142E RID: 5166
		private ao c;

		// Token: 0x0400142F RID: 5167
		private ej d;

		// Token: 0x04001430 RID: 5168
		private readonly StringBuilder e = new StringBuilder();
	}
}
