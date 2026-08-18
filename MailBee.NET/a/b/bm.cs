using System;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x0200037D RID: 893
	internal sealed class bm : hs, ap
	{
		// Token: 0x06002082 RID: 8322 RVA: 0x0008729E File Offset: 0x0008629E
		public bm(RtfVisualBreakKind A_0) : base(hu.b)
		{
			this.a = A_0;
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x000872AE File Offset: 0x000862AE
		public RtfVisualBreakKind dv()
		{
			return this.a;
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x000872B8 File Offset: 0x000862B8
		public override string ToString()
		{
			return this.a.ToString();
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x000872D9 File Offset: 0x000862D9
		protected override void dw(gq A_0)
		{
			A_0.ir(this);
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x000872E4 File Offset: 0x000862E4
		protected override bool dx(object A_0)
		{
			bm bm = A_0 as bm;
			return bm != null && base.dx(bm) && this.a == bm.a;
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x00087314 File Offset: 0x00086314
		protected override int dy()
		{
			return f3.a(base.dy(), this.a);
		}

		// Token: 0x04001489 RID: 5257
		private readonly RtfVisualBreakKind a;
	}
}
