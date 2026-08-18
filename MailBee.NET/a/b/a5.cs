using System;

namespace a.b
{
	// Token: 0x02000351 RID: 849
	internal sealed class a5 : iy
	{
		// Token: 0x06001EDE RID: 7902 RVA: 0x000836B7 File Offset: 0x000826B7
		public a5(ce A_0) : base(ie.a)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("fontTable");
			}
			this.b = A_0;
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x000836E0 File Offset: 0x000826E0
		public void b()
		{
			this.b.a();
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x000836F0 File Offset: 0x000826F0
		protected override void da(f A_0)
		{
			string a_ = A_0.nu();
			uint num = global::b.a(a_);
			if (num <= 1835979141U)
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
						if (num != 1835979141U)
						{
							return;
						}
						if (!(a_ == "flomajor"))
						{
							return;
						}
					}
					else if (!(a_ == "fdbminor"))
					{
						return;
					}
				}
				else if (!(a_ == "flominor"))
				{
					return;
				}
			}
			else
			{
				if (num > 2466964733U)
				{
					if (num != 3003421458U)
					{
						if (num != 3672565719U)
						{
							if (num == 3809224601U)
							{
								if (!(a_ == "f"))
								{
									return;
								}
								goto IL_11A;
							}
						}
						else
						{
							if (!(a_ == "fhiminor"))
							{
								return;
							}
							goto IL_11A;
						}
					}
					else
					{
						if (!(a_ == "fonttbl"))
						{
							return;
						}
						if (A_0.nt().get_Count() > 1)
						{
							if (A_0.nt().kb(1).p() == gl.b)
							{
								base.c(A_0);
								return;
							}
							int count = A_0.nt().get_Count();
							this.a.i();
							for (int i = 1; i < count; i++)
							{
								A_0.nt().kb(i).q(this.a);
								if (this.a.b() != null)
								{
									this.a();
									this.a.i();
								}
							}
						}
					}
					return;
				}
				if (num != 2134103081U)
				{
					if (num != 2466964733U)
					{
						return;
					}
					if (!(a_ == "fbimajor"))
					{
						return;
					}
				}
				else if (!(a_ == "fbiminor"))
				{
					return;
				}
			}
			IL_11A:
			this.a(A_0);
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x000838A1 File Offset: 0x000828A1
		private void a(f A_0)
		{
			this.a.i();
			this.a.ps(A_0);
			this.a();
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x000838C0 File Offset: 0x000828C0
		private void a()
		{
			if (!this.b.ga(this.a.g()))
			{
				this.b.a(this.a.a());
			}
		}

		// Token: 0x0400140F RID: 5135
		private readonly hd a = new hd();

		// Token: 0x04001410 RID: 5136
		private readonly ce b;
	}
}
