using System;

namespace a.b
{
	// Token: 0x0200035F RID: 863
	internal sealed class ef : iy
	{
		// Token: 0x06001F6D RID: 8045 RVA: 0x00085C34 File Offset: 0x00084C34
		public ef(bc A_0) : base(ie.a)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("collectedProperties");
			}
			this.a = A_0;
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x00085C5D File Offset: 0x00084C5D
		public s a()
		{
			return new cs(this.c, this.d, this.e, this.f);
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x00085C7C File Offset: 0x00084C7C
		public void b()
		{
			this.c = 0;
			this.d = null;
			this.e = null;
			this.f = null;
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x00085C9C File Offset: 0x00084C9C
		protected override void da(f A_0)
		{
			string text = A_0.nu();
			if (text == "userprops")
			{
				base.c(A_0);
				return;
			}
			if (text == null)
			{
				this.b();
				base.c(A_0);
				this.a.a(this.a());
				return;
			}
			if (text == "propname")
			{
				this.b.b();
				this.b.ps(A_0);
				this.d = this.b.a();
				return;
			}
			if (text == "staticval")
			{
				this.b.b();
				this.b.ps(A_0);
				this.e = this.b.a();
				return;
			}
			if (!(text == "linkval"))
			{
				return;
			}
			this.b.b();
			this.b.ps(A_0);
			this.f = this.b.a();
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x00085D8C File Offset: 0x00084D8C
		protected override void dz(c9 A_0)
		{
			string text = A_0.jz();
			if (text == "proptype")
			{
				this.c = A_0.j2();
			}
		}

		// Token: 0x04001445 RID: 5189
		private readonly bc a;

		// Token: 0x04001446 RID: 5190
		private readonly c7 b = new c7();

		// Token: 0x04001447 RID: 5191
		private new int c;

		// Token: 0x04001448 RID: 5192
		private string d;

		// Token: 0x04001449 RID: 5193
		private string e;

		// Token: 0x0400144A RID: 5194
		private string f;
	}
}
