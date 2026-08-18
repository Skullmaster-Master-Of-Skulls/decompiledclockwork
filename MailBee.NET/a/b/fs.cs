using System;
using System.Collections;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x02000357 RID: 855
	internal sealed class fs : eq
	{
		// Token: 0x06001F0C RID: 7948 RVA: 0x00085279 File Offset: 0x00084279
		public gw kr()
		{
			return this.a;
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x00085281 File Offset: 0x00084281
		public void a(gw A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x0008528A File Offset: 0x0008428A
		public int ks()
		{
			return this.b;
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x00085292 File Offset: 0x00084292
		public void a(int A_0)
		{
			this.b = A_0;
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x0008529B File Offset: 0x0008429B
		public string kt()
		{
			return this.c;
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x000852A3 File Offset: 0x000842A3
		public void a(string A_0)
		{
			this.c = A_0;
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x000852AC File Offset: 0x000842AC
		public fe ku()
		{
			fe fe = this.d.gc(this.c);
			if (fe != null)
			{
				return fe;
			}
			throw new RtfUndefinedFontException(fa.a(this.c, this.d.ToString()));
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x000852EB File Offset: 0x000842EB
		public h6 kv()
		{
			return this.d;
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x000852F3 File Offset: 0x000842F3
		public ce f()
		{
			return this.d;
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x000852FB File Offset: 0x000842FB
		public j kw()
		{
			return this.e;
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x00085303 File Offset: 0x00084303
		public h9 b()
		{
			return this.e;
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x0008530B File Offset: 0x0008430B
		public string kx()
		{
			return this.f;
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x00085313 File Offset: 0x00084313
		public void b(string A_0)
		{
			this.f = A_0;
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x0008531C File Offset: 0x0008431C
		public gu ky()
		{
			return this.g;
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x00085324 File Offset: 0x00084324
		public ej kz()
		{
			return this.i;
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x0008532C File Offset: 0x0008432C
		public ej k0()
		{
			if (this.i == null)
			{
				return this.a();
			}
			return this.i;
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x00085343 File Offset: 0x00084343
		public cu a()
		{
			if (this.i == null)
			{
				this.a(new cu(this.ku(), 24));
			}
			return this.i;
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x00085368 File Offset: 0x00084368
		public void a(cu A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("value");
			}
			int num = this.g.df(A_0);
			if (num >= 0)
			{
				this.i = (cu)this.g.dd(num);
				return;
			}
			this.g.a(A_0);
			this.i = A_0;
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x000853BF File Offset: 0x000843BF
		public @do k1()
		{
			return this.j;
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x000853C7 File Offset: 0x000843C7
		public g3 c()
		{
			return this.j;
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x000853CF File Offset: 0x000843CF
		public ee k2()
		{
			return this.k;
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x000853D7 File Offset: 0x000843D7
		public bc g()
		{
			return this.k;
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x000853DF File Offset: 0x000843DF
		public void d()
		{
			this.h.Push(this.a());
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x000853F2 File Offset: 0x000843F2
		public void e()
		{
			if (this.h.Count == 0)
			{
				throw new RtfStructureException(fa.i());
			}
			this.i = (cu)this.h.Pop();
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x00085424 File Offset: 0x00084424
		public void h()
		{
			this.a = gw.a;
			this.b = 1;
			this.c = "f0";
			this.d.a();
			this.e.a();
			this.f = null;
			this.g.a();
			this.h.Clear();
			this.i = null;
			this.j.a();
			this.k.a();
		}

		// Token: 0x04001421 RID: 5153
		private gw a;

		// Token: 0x04001422 RID: 5154
		private int b;

		// Token: 0x04001423 RID: 5155
		private string c;

		// Token: 0x04001424 RID: 5156
		private readonly ce d = new ce();

		// Token: 0x04001425 RID: 5157
		private readonly h9 e = new h9();

		// Token: 0x04001426 RID: 5158
		private string f;

		// Token: 0x04001427 RID: 5159
		private readonly a9 g = new a9();

		// Token: 0x04001428 RID: 5160
		private readonly Stack h = new Stack();

		// Token: 0x04001429 RID: 5161
		private cu i;

		// Token: 0x0400142A RID: 5162
		private readonly g3 j = new g3();

		// Token: 0x0400142B RID: 5163
		private readonly bc k = new bc();
	}
}
