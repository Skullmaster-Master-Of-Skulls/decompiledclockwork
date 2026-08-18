using System;
using System.Text;

namespace a.b
{
	// Token: 0x020002C6 RID: 710
	internal class ge
	{
		// Token: 0x0600188A RID: 6282 RVA: 0x0006EDCC File Offset: 0x0006DDCC
		public ge(db A_0, string A_1)
		{
			if (A_0 == null)
			{
				throw new NullReferenceException("path must not be null");
			}
			if (A_1 == null)
			{
				throw new NullReferenceException("name must not be null");
			}
			if (A_1.Length == 0)
			{
				throw new ArgumentException("name cannot be empty");
			}
			this.a = A_0;
			this.b = A_1;
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x0006EE1C File Offset: 0x0006DE1C
		public string b()
		{
			return this.a.ToString();
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x0006EE29 File Offset: 0x0006DE29
		public string a()
		{
			return this.b;
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x0006EE34 File Offset: 0x0006DE34
		public override bool Equals(object o)
		{
			bool result = false;
			if (o != null && o.GetType() == base.GetType())
			{
				if (this == o)
				{
					result = true;
				}
				else
				{
					ge ge = (ge)o;
					result = (this.a.Equals(ge.a) && this.b.Equals(ge.b));
				}
			}
			return result;
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x0006EE91 File Offset: 0x0006DE91
		public override int GetHashCode()
		{
			if (this.c == 0)
			{
				this.c = (this.a.GetHashCode() ^ this.b.GetHashCode());
			}
			return this.c;
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x0006EEC0 File Offset: 0x0006DEC0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(40 * (this.a.b() + 1));
			for (int i = 0; i < this.a.b(); i++)
			{
				stringBuilder.Append(this.a.a(i)).Append("/");
			}
			stringBuilder.Append(this.b);
			return stringBuilder.ToString();
		}

		// Token: 0x04001238 RID: 4664
		private db a;

		// Token: 0x04001239 RID: 4665
		private string b;

		// Token: 0x0400123A RID: 4666
		private int c;
	}
}
