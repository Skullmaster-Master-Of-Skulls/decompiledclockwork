using System;
using System.Text;

namespace a.b
{
	// Token: 0x02000350 RID: 848
	internal sealed class hd : iy
	{
		// Token: 0x06001ED1 RID: 7889 RVA: 0x00083186 File Offset: 0x00082186
		public hd() : base(ie.a)
		{
			this.i();
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x000831A0 File Offset: 0x000821A0
		public string g()
		{
			return this.a;
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x000831A8 File Offset: 0x000821A8
		public int d()
		{
			return this.b;
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x000831B0 File Offset: 0x000821B0
		public int h()
		{
			return this.c;
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x000831B8 File Offset: 0x000821B8
		public int f()
		{
			return this.d;
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x000831C0 File Offset: 0x000821C0
		public f4 c()
		{
			return this.e;
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x000831C8 File Offset: 0x000821C8
		public i6 e()
		{
			return this.f;
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x000831D0 File Offset: 0x000821D0
		public string b()
		{
			string text = null;
			int length = this.g.Length;
			if (length > 0 && this.g[length - 1] == ';')
			{
				text = this.g.ToString().Substring(0, length - 1).Trim();
				if (text.Length == 0)
				{
					text = null;
				}
			}
			return text;
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x00083226 File Offset: 0x00082226
		public fe a()
		{
			return new b6(this.a, this.e, this.f, this.c, this.d, this.b());
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x00083251 File Offset: 0x00082251
		public void i()
		{
			this.b = 0;
			this.c = 0;
			this.d = 0;
			this.e = f4.a;
			this.f = i6.a;
			this.g.Remove(0, this.g.Length);
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x00083290 File Offset: 0x00082290
		protected override void da(f A_0)
		{
			string a_ = A_0.nu();
			uint num = global::b.a(a_);
			if (num <= 875660080U)
			{
				if (num <= 644779004U)
				{
					if (num != 596946891U)
					{
						if (num != 644779004U)
						{
							return;
						}
						if (!(a_ == "fdbmajor"))
						{
							return;
						}
					}
					else if (!(a_ == "fhimajor"))
					{
						return;
					}
				}
				else if (num != 747407905U)
				{
					if (num != 875660080U)
					{
						return;
					}
					if (!(a_ == "fdbminor"))
					{
						return;
					}
				}
				else if (!(a_ == "flominor"))
				{
					return;
				}
			}
			else if (num <= 2134103081U)
			{
				if (num != 1835979141U)
				{
					if (num != 2134103081U)
					{
						return;
					}
					if (!(a_ == "fbiminor"))
					{
						return;
					}
				}
				else if (!(a_ == "flomajor"))
				{
					return;
				}
			}
			else if (num != 2466964733U)
			{
				if (num != 3672565719U)
				{
					if (num != 3809224601U)
					{
						return;
					}
					if (!(a_ == "f"))
					{
						return;
					}
				}
				else if (!(a_ == "fhiminor"))
				{
					return;
				}
			}
			else if (!(a_ == "fbimajor"))
			{
				return;
			}
			base.c(A_0);
		}

		// Token: 0x06001EDC RID: 7900 RVA: 0x000833A0 File Offset: 0x000823A0
		protected override void dz(c9 A_0)
		{
			string a_ = A_0.jz();
			uint num = global::b.a(a_);
			if (num <= 2134103081U)
			{
				if (num <= 878424022U)
				{
					if (num <= 644779004U)
					{
						if (num == 596946891U)
						{
							a_ == "fhimajor";
							return;
						}
						if (num != 644779004U)
						{
							return;
						}
						a_ == "fdbmajor";
						return;
					}
					else
					{
						if (num == 747407905U)
						{
							a_ == "flominor";
							return;
						}
						if (num == 875660080U)
						{
							a_ == "fdbminor";
							return;
						}
						if (num != 878424022U)
						{
							return;
						}
						if (!(a_ == "fscript"))
						{
							return;
						}
						this.e = f4.e;
						return;
					}
				}
				else if (num <= 1265622565U)
				{
					if (num != 1158515092U)
					{
						if (num != 1265622565U)
						{
							return;
						}
						if (!(a_ == "ftech"))
						{
							return;
						}
						this.e = f4.g;
						return;
					}
					else
					{
						if (!(a_ == "fswiss"))
						{
							return;
						}
						this.e = f4.c;
						return;
					}
				}
				else
				{
					if (num == 1835979141U)
					{
						a_ == "flomajor";
						return;
					}
					if (num != 1990774951U)
					{
						if (num != 2134103081U)
						{
							return;
						}
						a_ == "fbiminor";
						return;
					}
					else
					{
						if (!(a_ == "fbidi"))
						{
							return;
						}
						this.e = f4.h;
						return;
					}
				}
			}
			else if (num <= 2963347582U)
			{
				if (num <= 2466964733U)
				{
					if (num != 2365460039U)
					{
						if (num != 2466964733U)
						{
							return;
						}
						a_ == "fbimajor";
						return;
					}
					else
					{
						if (!(a_ == "fcharset"))
						{
							return;
						}
						this.c = A_0.j2();
						return;
					}
				}
				else if (num != 2657895686U)
				{
					if (num != 2888623432U)
					{
						if (num != 2963347582U)
						{
							return;
						}
						if (!(a_ == "fprq"))
						{
							return;
						}
						switch (A_0.j2())
						{
						case 0:
							this.f = i6.a;
							return;
						case 1:
							this.f = i6.b;
							return;
						case 2:
							this.f = i6.c;
							return;
						default:
							return;
						}
					}
					else
					{
						if (!(a_ == "fnil"))
						{
							return;
						}
						this.e = f4.a;
						return;
					}
				}
				else
				{
					if (!(a_ == "fmodern"))
					{
						return;
					}
					this.e = f4.d;
					return;
				}
			}
			else if (num <= 3809224601U)
			{
				if (num == 3672565719U)
				{
					a_ == "fhiminor";
					return;
				}
				if (num != 3809224601U)
				{
					return;
				}
				if (!(a_ == "f"))
				{
					return;
				}
				this.a = A_0.jy();
				this.b = A_0.j2();
				return;
			}
			else if (num != 3880443187U)
			{
				if (num != 3985183974U)
				{
					if (num != 4000999766U)
					{
						return;
					}
					if (!(a_ == "froman"))
					{
						return;
					}
					this.e = f4.b;
					return;
				}
				else
				{
					if (!(a_ == "fdecor"))
					{
						return;
					}
					this.e = f4.f;
					return;
				}
			}
			else
			{
				if (!(a_ == "cpg"))
				{
					return;
				}
				this.d = A_0.j2();
				return;
			}
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x000836A3 File Offset: 0x000826A3
		protected override void ft(bp A_0)
		{
			this.g.Append(A_0.eu());
		}

		// Token: 0x04001408 RID: 5128
		private string a;

		// Token: 0x04001409 RID: 5129
		private int b;

		// Token: 0x0400140A RID: 5130
		private new int c;

		// Token: 0x0400140B RID: 5131
		private int d;

		// Token: 0x0400140C RID: 5132
		private f4 e;

		// Token: 0x0400140D RID: 5133
		private i6 f;

		// Token: 0x0400140E RID: 5134
		private readonly StringBuilder g = new StringBuilder();
	}
}
