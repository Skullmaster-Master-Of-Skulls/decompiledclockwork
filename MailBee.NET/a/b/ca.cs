using System;

namespace a.b
{
	// Token: 0x02000352 RID: 850
	internal sealed class ca : iy
	{
		// Token: 0x06001EE3 RID: 7907 RVA: 0x000838F0 File Offset: 0x000828F0
		public ca() : base(ie.b)
		{
			this.i();
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x000838FF File Offset: 0x000828FF
		public void i()
		{
			this.a = de.e;
			this.b = 0;
			this.c = 0;
			this.d = 0;
			this.e = 0;
			this.f = 100;
			this.g = 100;
			this.h = null;
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x0008393B File Offset: 0x0008293B
		public de e()
		{
			return this.a;
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x00083943 File Offset: 0x00082943
		public int f()
		{
			return this.b;
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x0008394B File Offset: 0x0008294B
		public int h()
		{
			return this.c;
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x00083953 File Offset: 0x00082953
		public int c()
		{
			return this.d;
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x0008395B File Offset: 0x0008295B
		public int g()
		{
			return this.e;
		}

		// Token: 0x06001EEA RID: 7914 RVA: 0x00083963 File Offset: 0x00082963
		public int b()
		{
			return this.f;
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x0008396B File Offset: 0x0008296B
		public int d()
		{
			return this.g;
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x00083973 File Offset: 0x00082973
		public string a()
		{
			return this.h;
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x0008397C File Offset: 0x0008297C
		protected override void da(f A_0)
		{
			string text = A_0.nu();
			if (text == "pict")
			{
				this.i();
				base.c(A_0);
			}
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x000839AC File Offset: 0x000829AC
		protected override void dz(c9 A_0)
		{
			string a_ = A_0.jz();
			uint num = global::b.a(a_);
			if (num > 1666178777U)
			{
				if (num <= 2404747860U)
				{
					if (num != 1799583997U)
					{
						if (num != 2338465226U)
						{
							if (num != 2404747860U)
							{
								return;
							}
							if (!(a_ == "emfblip"))
							{
								return;
							}
							this.a = de.a;
							return;
						}
						else
						{
							if (!(a_ == "pichgoal"))
							{
								return;
							}
							this.e = A_0.j2();
							if (this.c == 0)
							{
								this.c = this.e;
								return;
							}
							return;
						}
					}
					else if (!(a_ == "wbitmap"))
					{
						return;
					}
				}
				else if (num != 2438597557U)
				{
					if (num != 3872902567U)
					{
						if (num != 4248957617U)
						{
							return;
						}
						if (!(a_ == "pich"))
						{
							return;
						}
						this.c = A_0.j2();
						this.e = this.c;
						return;
					}
					else
					{
						if (!(a_ == "picwgoal"))
						{
							return;
						}
						this.d = A_0.j2();
						if (this.b == 0)
						{
							this.b = this.d;
							return;
						}
						return;
					}
				}
				else if (!(a_ == "dibitmap"))
				{
					return;
				}
				this.a = de.e;
				return;
			}
			if (num <= 1098216500U)
			{
				if (num != 104988892U)
				{
					if (num != 171713509U)
					{
						if (num != 1098216500U)
						{
							return;
						}
						if (!(a_ == "jpegblip"))
						{
							return;
						}
						this.a = de.c;
						return;
					}
					else
					{
						if (!(a_ == "wmetafile"))
						{
							return;
						}
						this.a = de.d;
						return;
					}
				}
				else
				{
					if (!(a_ == "picw"))
					{
						return;
					}
					this.b = A_0.j2();
					this.d = this.b;
					return;
				}
			}
			else if (num != 1489552147U)
			{
				if (num != 1649401158U)
				{
					if (num != 1666178777U)
					{
						return;
					}
					if (!(a_ == "picscalex"))
					{
						return;
					}
					this.f = A_0.j2();
					return;
				}
				else
				{
					if (!(a_ == "picscaley"))
					{
						return;
					}
					this.g = A_0.j2();
				}
			}
			else
			{
				if (!(a_ == "pngblip"))
				{
					return;
				}
				this.a = de.b;
				return;
			}
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x00083BD9 File Offset: 0x00082BD9
		protected override void ft(bp A_0)
		{
			this.h = A_0.eu();
		}

		// Token: 0x04001411 RID: 5137
		private de a;

		// Token: 0x04001412 RID: 5138
		private int b;

		// Token: 0x04001413 RID: 5139
		private new int c;

		// Token: 0x04001414 RID: 5140
		private int d;

		// Token: 0x04001415 RID: 5141
		private int e;

		// Token: 0x04001416 RID: 5142
		private int f;

		// Token: 0x04001417 RID: 5143
		private int g;

		// Token: 0x04001418 RID: 5144
		private string h;
	}
}
