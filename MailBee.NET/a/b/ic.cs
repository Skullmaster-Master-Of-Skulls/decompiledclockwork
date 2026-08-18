using System;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x02000380 RID: 896
	internal sealed class ic : hs, w
	{
		// Token: 0x0600209E RID: 8350 RVA: 0x000877B0 File Offset: 0x000867B0
		public ic(RtfVisualSpecialCharKind A_0) : base(hu.c)
		{
			this.a = A_0;
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x000877C0 File Offset: 0x000867C0
		protected override void dw(gq A_0)
		{
			A_0.@is(this);
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x000877C9 File Offset: 0x000867C9
		public RtfVisualSpecialCharKind o9()
		{
			return this.a;
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x000877D4 File Offset: 0x000867D4
		protected override bool dx(object A_0)
		{
			ic ic = A_0 as ic;
			return ic != null && base.dx(ic) && this.a == ic.a;
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x00087804 File Offset: 0x00086804
		protected override int dy()
		{
			return f3.a(base.dy(), this.a);
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x0008781C File Offset: 0x0008681C
		public override string ToString()
		{
			return this.a.ToString();
		}

		// Token: 0x04001494 RID: 5268
		private readonly RtfVisualSpecialCharKind a;
	}
}
