using System;
using System.Text;

namespace a.b
{
	// Token: 0x02000397 RID: 919
	internal sealed class gy : @in, f
	{
		// Token: 0x06002107 RID: 8455 RVA: 0x00087F2C File Offset: 0x00086F2C
		public gy() : base(gl.b)
		{
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x00087F40 File Offset: 0x00086F40
		public a1 nt()
		{
			return this.a;
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x00087F48 File Offset: 0x00086F48
		public e0 a()
		{
			return this.a;
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x00087F50 File Offset: 0x00086F50
		public string nu()
		{
			if (this.a.Count > 0)
			{
				f8 f = this.a.kb(0);
				if (f.p() == gl.a)
				{
					c9 c = (c9)f;
					if ("*".Equals(c.jz()) && this.a.Count > 1)
					{
						f8 f2 = this.a.kb(1);
						if (f2.p() == gl.a)
						{
							return ((c9)f2).jz();
						}
					}
					return c.jz();
				}
			}
			return null;
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x00087FD0 File Offset: 0x00086FD0
		public bool nv()
		{
			if (this.a.Count > 0)
			{
				f8 f = this.a.kb(0);
				if (f.p() == gl.a)
				{
					c9 c = (c9)f;
					if ("*".Equals(c.jz()))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x0008801C File Offset: 0x0008701C
		public f nw(string A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("destination");
			}
			foreach (object obj in this.a)
			{
				f8 f = (f8)obj;
				if (f.p() == gl.b)
				{
					f f2 = (f)f;
					if (A_0.Equals(f2.nu()))
					{
						return f2;
					}
				}
			}
			return null;
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x000880A4 File Offset: 0x000870A4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("{");
			int count = this.a.Count;
			stringBuilder.Append(count);
			stringBuilder.Append(" items");
			if (count > 0)
			{
				stringBuilder.Append(": [");
				stringBuilder.Append(this.a.kb(0));
				if (count > 1)
				{
					stringBuilder.Append(", ");
					stringBuilder.Append(this.a.kb(1));
					if (count > 2)
					{
						stringBuilder.Append(", ");
						if (count > 3)
						{
							stringBuilder.Append("..., ");
						}
						stringBuilder.Append(this.a.kb(count - 1));
					}
				}
				stringBuilder.Append("]");
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x00088178 File Offset: 0x00087178
		protected override void ev(cy A_0)
		{
			A_0.ps(this);
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x00088184 File Offset: 0x00087184
		protected override bool ew(object A_0)
		{
			gy gy = A_0 as gy;
			return gy != null && base.ew(A_0) && this.a.Equals(gy.a);
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x000881B7 File Offset: 0x000871B7
		protected override int ex()
		{
			return f3.a(base.ex(), this.a);
		}

		// Token: 0x040014C6 RID: 5318
		private readonly e0 a = new e0();
	}
}
