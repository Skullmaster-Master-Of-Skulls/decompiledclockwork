using System;

namespace a.b
{
	// Token: 0x0200034F RID: 847
	internal sealed class cd : iy
	{
		// Token: 0x06001ECB RID: 7883 RVA: 0x00082C46 File Offset: 0x00081C46
		public cd(g3 A_0) : base(ie.a)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("info");
			}
			this.a = A_0;
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x00082C7A File Offset: 0x00081C7A
		public void a()
		{
			this.a.a();
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x00082C88 File Offset: 0x00081C88
		protected override void da(f A_0)
		{
			string a_ = A_0.nu();
			uint num = global::b.a(a_);
			if (num <= 1349246118U)
			{
				if (num <= 684675452U)
				{
					if (num <= 413797721U)
					{
						if (num != 263456517U)
						{
							if (num != 413797721U)
							{
								return;
							}
							if (!(a_ == "doccomm"))
							{
								return;
							}
							this.a.h(this.b(A_0));
							return;
						}
						else
						{
							if (!(a_ == "info"))
							{
								return;
							}
							base.c(A_0);
							return;
						}
					}
					else if (num != 437767012U)
					{
						if (num != 684675452U)
						{
							return;
						}
						if (!(a_ == "creatim"))
						{
							return;
						}
						this.a.b(new DateTime?(this.a(A_0)));
						return;
					}
					else
					{
						if (!(a_ == "revtim"))
						{
							return;
						}
						this.a.c(new DateTime?(this.a(A_0)));
						return;
					}
				}
				else if (num <= 1002009594U)
				{
					if (num != 991397836U)
					{
						if (num != 1002009594U)
						{
							return;
						}
						if (!(a_ == "printim"))
						{
							return;
						}
						this.a.d(new DateTime?(this.a(A_0)));
						return;
					}
					else
					{
						if (!(a_ == "manager"))
						{
							return;
						}
						this.a.b(this.b(A_0));
						return;
					}
				}
				else if (num != 1333443158U)
				{
					if (num != 1349246118U)
					{
						return;
					}
					if (!(a_ == "buptim"))
					{
						return;
					}
					this.a.a(new DateTime?(this.a(A_0)));
					return;
				}
				else
				{
					if (!(a_ == "author"))
					{
						return;
					}
					this.a.e(this.b(A_0));
					return;
				}
			}
			else if (num <= 2556802313U)
			{
				if (num <= 1832983798U)
				{
					if (num != 1738982494U)
					{
						if (num != 1832983798U)
						{
							return;
						}
						if (!(a_ == "hlinkbase"))
						{
							return;
						}
						this.a.j(this.b(A_0));
						return;
					}
					else
					{
						if (!(a_ == "comment"))
						{
							return;
						}
						this.a.d(this.b(A_0));
						return;
					}
				}
				else if (num != 2300378703U)
				{
					if (num != 2556802313U)
					{
						return;
					}
					if (!(a_ == "title"))
					{
						return;
					}
					this.a.c(this.b(A_0));
					return;
				}
				else
				{
					if (!(a_ == "subject"))
					{
						return;
					}
					this.a.i(this.b(A_0));
					return;
				}
			}
			else if (num <= 3475980913U)
			{
				if (num != 2858608828U)
				{
					if (num != 3475980913U)
					{
						return;
					}
					if (!(a_ == "category"))
					{
						return;
					}
					this.a.g(this.b(A_0));
					return;
				}
				else
				{
					if (!(a_ == "company"))
					{
						return;
					}
					this.a.f(this.b(A_0));
					return;
				}
			}
			else if (num != 4210524501U)
			{
				if (num != 4225036029U)
				{
					return;
				}
				if (!(a_ == "operator"))
				{
					return;
				}
				this.a.a(this.b(A_0));
				return;
			}
			else
			{
				if (!(a_ == "keywords"))
				{
					return;
				}
				this.a.k(this.b(A_0));
				return;
			}
		}

		// Token: 0x06001ECE RID: 7886 RVA: 0x00082FD0 File Offset: 0x00081FD0
		protected override void dz(c9 A_0)
		{
			string a_ = A_0.jz();
			uint num = global::b.a(a_);
			if (num <= 926444256U)
			{
				if (num != 466782324U)
				{
					if (num != 886831229U)
					{
						if (num != 926444256U)
						{
							return;
						}
						if (!(a_ == "id"))
						{
							return;
						}
						this.a.a(new int?(A_0.j2()));
						return;
					}
					else
					{
						if (!(a_ == "nofwords"))
						{
							return;
						}
						this.a.d(new int?(A_0.j2()));
						return;
					}
				}
				else
				{
					if (!(a_ == "nofpages"))
					{
						return;
					}
					this.a.f(new int?(A_0.j2()));
					return;
				}
			}
			else if (num <= 1798968713U)
			{
				if (num != 1181855383U)
				{
					if (num != 1798968713U)
					{
						return;
					}
					if (!(a_ == "edmins"))
					{
						return;
					}
					this.a.g(new int?(A_0.j2()));
					return;
				}
				else
				{
					if (!(a_ == "version"))
					{
						return;
					}
					this.a.c(new int?(A_0.j2()));
					return;
				}
			}
			else if (num != 2572619433U)
			{
				if (num != 3728026500U)
				{
					return;
				}
				if (!(a_ == "vern"))
				{
					return;
				}
				this.a.b(new int?(A_0.j2()));
				return;
			}
			else
			{
				if (!(a_ == "nofchars"))
				{
					return;
				}
				this.a.e(new int?(A_0.j2()));
				return;
			}
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x0008313E File Offset: 0x0008213E
		private string b(f A_0)
		{
			this.b.b();
			this.b.ps(A_0);
			return this.b.a();
		}

		// Token: 0x06001ED0 RID: 7888 RVA: 0x00083162 File Offset: 0x00082162
		private DateTime a(f A_0)
		{
			this.c.b();
			this.c.ps(A_0);
			return this.c.a();
		}

		// Token: 0x04001405 RID: 5125
		private readonly g3 a;

		// Token: 0x04001406 RID: 5126
		private readonly c7 b = new c7();

		// Token: 0x04001407 RID: 5127
		private new readonly br c = new br();
	}
}
