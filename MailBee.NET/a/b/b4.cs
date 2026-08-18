using System;

namespace a.b
{
	// Token: 0x02000399 RID: 921
	internal sealed class b4 : @in, bp
	{
		// Token: 0x0600211C RID: 8476 RVA: 0x000882F6 File Offset: 0x000872F6
		public b4(string A_0) : base(gl.c)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("text");
			}
			this.a = A_0;
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x00088314 File Offset: 0x00087314
		public string eu()
		{
			return this.a;
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x0008831C File Offset: 0x0008731C
		public override string ToString()
		{
			return this.a;
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x00088324 File Offset: 0x00087324
		protected override void ev(cy A_0)
		{
			A_0.pt(this);
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x00088330 File Offset: 0x00087330
		protected override bool ew(object A_0)
		{
			b4 b = A_0 as b4;
			return b != null && base.ew(A_0) && this.a.Equals(b.a);
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x00088363 File Offset: 0x00087363
		protected override int ex()
		{
			return f3.a(base.ex(), this.a);
		}

		// Token: 0x040014CB RID: 5323
		private readonly string a;
	}
}
