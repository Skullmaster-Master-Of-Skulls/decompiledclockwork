using System;
using System.Globalization;

namespace a.b
{
	// Token: 0x02000398 RID: 920
	internal sealed class er : @in, c9
	{
		// Token: 0x06002111 RID: 8465 RVA: 0x000881CA File Offset: 0x000871CA
		public er(string A_0) : base(gl.a)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("name");
			}
			this.a = A_0;
			this.b = A_0;
			this.c = null;
			this.d = -1;
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x00088200 File Offset: 0x00087200
		public er(string A_0, string A_1) : base(gl.a)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("name");
			}
			if (A_1 == null)
			{
				throw new ArgumentNullException("value");
			}
			this.a = A_0 + A_1;
			this.b = A_0;
			this.c = A_1;
			int num;
			if (int.TryParse(A_1, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
			{
				this.d = num;
				return;
			}
			this.d = -1;
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x0008826A File Offset: 0x0008726A
		public string jy()
		{
			return this.a;
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x00088272 File Offset: 0x00087272
		public string jz()
		{
			return this.b;
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x0008827A File Offset: 0x0008727A
		public bool j0()
		{
			return this.c != null;
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x00088285 File Offset: 0x00087285
		public string j1()
		{
			return this.c;
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x0008828D File Offset: 0x0008728D
		public int j2()
		{
			return this.d;
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x00088295 File Offset: 0x00087295
		public override string ToString()
		{
			return "\\" + this.a;
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x000882A7 File Offset: 0x000872A7
		protected override void ev(cy A_0)
		{
			A_0.pr(this);
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x000882B0 File Offset: 0x000872B0
		protected override bool ew(object A_0)
		{
			er er = A_0 as er;
			return er != null && base.ew(A_0) && this.a.Equals(er.a);
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x000882E3 File Offset: 0x000872E3
		protected override int ex()
		{
			return f3.a(base.ex(), this.a);
		}

		// Token: 0x040014C7 RID: 5319
		private readonly string a;

		// Token: 0x040014C8 RID: 5320
		private readonly string b;

		// Token: 0x040014C9 RID: 5321
		private readonly string c;

		// Token: 0x040014CA RID: 5322
		private readonly int d;
	}
}
