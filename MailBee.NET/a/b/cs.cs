using System;
using System.Text;

namespace a.b
{
	// Token: 0x02000376 RID: 886
	internal sealed class cs : s
	{
		// Token: 0x06002029 RID: 8233 RVA: 0x0008642D File Offset: 0x0008542D
		public cs(int A_0, string A_1, string A_2) : this(A_0, A_1, A_2, null)
		{
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x0008643C File Offset: 0x0008543C
		public cs(int A_0, string A_1, string A_2, string A_3)
		{
			if (A_1 == null)
			{
				throw new ArgumentNullException("name");
			}
			if (A_2 == null)
			{
				throw new ArgumentNullException("staticValue");
			}
			this.a = A_0;
			if (A_0 <= 5)
			{
				if (A_0 == 3)
				{
					this.b = ha.b;
					goto IL_7C;
				}
				if (A_0 == 5)
				{
					this.b = ha.c;
					goto IL_7C;
				}
			}
			else
			{
				if (A_0 == 11)
				{
					this.b = ha.e;
					goto IL_7C;
				}
				if (A_0 == 30)
				{
					this.b = ha.f;
					goto IL_7C;
				}
				if (A_0 == 64)
				{
					this.b = ha.d;
					goto IL_7C;
				}
			}
			this.b = ha.a;
			IL_7C:
			this.c = A_1;
			this.d = A_2;
			this.e = A_3;
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x000864DB File Offset: 0x000854DB
		public int gs()
		{
			return this.a;
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x000864E3 File Offset: 0x000854E3
		public ha gt()
		{
			return this.b;
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x000864EB File Offset: 0x000854EB
		public string gu()
		{
			return this.c;
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x000864F3 File Offset: 0x000854F3
		public string gv()
		{
			return this.d;
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x000864FB File Offset: 0x000854FB
		public string gw()
		{
			return this.e;
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x00086503 File Offset: 0x00085503
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && !(base.GetType() != obj.GetType()) && this.a(obj));
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x0008652C File Offset: 0x0008552C
		private bool a(object A_0)
		{
			cs cs = A_0 as cs;
			return cs != null && this.a == cs.a && this.b == cs.b && this.c.Equals(cs.c) && au.a(this.d, cs.d) && au.a(this.e, cs.e);
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x00086598 File Offset: 0x00085598
		public override int GetHashCode()
		{
			return f3.a(base.GetType().GetHashCode(), this.a());
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x000865B0 File Offset: 0x000855B0
		private int a()
		{
			return f3.a(f3.a(f3.a(f3.a(this.a, this.b), this.c), this.d), this.e);
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x000865EC File Offset: 0x000855EC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.c);
			if (this.d != null)
			{
				stringBuilder.Append("=");
				stringBuilder.Append(this.d);
			}
			if (this.e != null)
			{
				stringBuilder.Append("@");
				stringBuilder.Append(this.e);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001471 RID: 5233
		private readonly int a;

		// Token: 0x04001472 RID: 5234
		private readonly ha b;

		// Token: 0x04001473 RID: 5235
		private readonly string c;

		// Token: 0x04001474 RID: 5236
		private readonly string d;

		// Token: 0x04001475 RID: 5237
		private readonly string e;
	}
}
