using System;
using System.Collections;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x0200039E RID: 926
	internal sealed class b5 : at
	{
		// Token: 0x06002167 RID: 8551 RVA: 0x000898CE File Offset: 0x000888CE
		public f a()
		{
			return this.c;
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x000898D6 File Offset: 0x000888D6
		protected override void ey()
		{
			this.a.Clear();
			this.b = null;
			this.c = null;
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x000898F4 File Offset: 0x000888F4
		protected override void ez()
		{
			gy a_ = new gy();
			if (this.b != null)
			{
				this.a.Push(this.b);
				this.b.a().a(a_);
			}
			this.b = a_;
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x00089938 File Offset: 0x00088938
		protected override void e0(c9 A_0)
		{
			if (this.b == null)
			{
				throw new RtfStructureException(bv.n());
			}
			this.b.a().a(A_0);
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x0008995E File Offset: 0x0008895E
		protected override void e1(bp A_0)
		{
			if (this.b == null)
			{
				throw new RtfStructureException(bv.m());
			}
			this.b.a().a(A_0);
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x00089984 File Offset: 0x00088984
		protected override void e2()
		{
			if (this.a.Count > 0)
			{
				this.b = (gy)this.a.Pop();
				return;
			}
			if (this.c != null)
			{
				throw new RtfStructureException(bv.l());
			}
			this.c = this.b;
			this.b = null;
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x000899DC File Offset: 0x000889DC
		protected override void e3()
		{
			if (this.a.Count > 0)
			{
				throw new RtfBraceNestingException(bv.k());
			}
		}

		// Token: 0x040014E3 RID: 5347
		private readonly Stack a = new Stack();

		// Token: 0x040014E4 RID: 5348
		private gy b;

		// Token: 0x040014E5 RID: 5349
		private gy c;
	}
}
