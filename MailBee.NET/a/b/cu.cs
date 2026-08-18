using System;
using System.Text;

namespace a.b
{
	// Token: 0x0200037A RID: 890
	internal sealed class cu : ej
	{
		// Token: 0x0600204F RID: 8271 RVA: 0x000869AC File Offset: 0x000859AC
		public cu(fe A_0, int A_1)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("font");
			}
			if (A_1 <= 0 || A_1 > 65535)
			{
				throw new ArgumentException(fa.g(A_1));
			}
			this.a = A_0;
			this.b = A_1;
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x00086A0C File Offset: 0x00085A0C
		public cu(ej A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("copy");
			}
			this.a = A_0.gy();
			this.b = A_0.gz();
			this.c = A_0.g0();
			this.d = A_0.g1();
			this.e = A_0.g2();
			this.f = A_0.g3();
			this.g = A_0.g4();
			this.h = A_0.g5();
			this.i = A_0.g6();
			this.j = A_0.g7();
			this.k = A_0.g8();
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x00086AC8 File Offset: 0x00085AC8
		public cu(cu A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("copy");
			}
			this.a = A_0.a;
			this.b = A_0.b;
			this.c = A_0.c;
			this.d = A_0.d;
			this.e = A_0.e;
			this.f = A_0.f;
			this.g = A_0.g;
			this.h = A_0.h;
			this.i = A_0.i;
			this.j = A_0.j;
			this.k = A_0.k;
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x00086B84 File Offset: 0x00085B84
		public string gx()
		{
			StringBuilder stringBuilder = new StringBuilder(this.a.e9());
			stringBuilder.Append(", ");
			stringBuilder.Append(this.b);
			stringBuilder.Append((this.c >= 0) ? "+" : "");
			stringBuilder.Append(this.c);
			stringBuilder.Append(", ");
			if (this.d || this.e || this.f || this.g)
			{
				bool flag = false;
				if (this.d)
				{
					stringBuilder.Append("bold");
					flag = true;
				}
				if (this.e)
				{
					stringBuilder.Append(flag ? "+italic" : "italic");
					flag = true;
				}
				if (this.f)
				{
					stringBuilder.Append(flag ? "+underline" : "underline");
					flag = true;
				}
				if (this.g)
				{
					stringBuilder.Append(flag ? "+strikethrough" : "strikethrough");
				}
			}
			else
			{
				stringBuilder.Append("plain");
			}
			if (this.h)
			{
				stringBuilder.Append(", hidden");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x00086CAE File Offset: 0x00085CAE
		public fe gy()
		{
			return this.a;
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x00086CB6 File Offset: 0x00085CB6
		public cu a(fe A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("rtfFont");
			}
			if (this.a.Equals(A_0))
			{
				return this;
			}
			return new cu(this)
			{
				a = A_0
			};
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x00086CE3 File Offset: 0x00085CE3
		public int gz()
		{
			return this.b;
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x00086CEB File Offset: 0x00085CEB
		public cu b(int A_0)
		{
			if (A_0 <= 0 || A_0 > 65535)
			{
				throw new ArgumentException(fa.g(A_0));
			}
			if (this.b == A_0)
			{
				return this;
			}
			return new cu(this)
			{
				b = A_0
			};
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x00086D1D File Offset: 0x00085D1D
		public int g0()
		{
			return this.c;
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x00086D25 File Offset: 0x00085D25
		public cu a(int A_0)
		{
			if (this.c == A_0)
			{
				return this;
			}
			return new cu(this)
			{
				c = A_0
			};
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x00086D3F File Offset: 0x00085D3F
		public cu c(bool A_0)
		{
			return new cu(this)
			{
				b = Math.Max(1, this.b * 2 / 3),
				c = (A_0 ? 1 : -1) * Math.Max(1, this.b / 2)
			};
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x00086D7C File Offset: 0x00085D7C
		public bool d()
		{
			return !this.d && !this.e && !this.f && !this.g && !this.h && this.b == 24 && this.c == 0 && es.a.Equals(this.j) && es.b.Equals(this.i);
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x00086DE7 File Offset: 0x00085DE7
		public cu c()
		{
			if (this.d())
			{
				return this;
			}
			return new cu(this.a, 24)
			{
				k = this.k
			};
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x00086E0C File Offset: 0x00085E0C
		public bool g1()
		{
			return this.d;
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x00086E14 File Offset: 0x00085E14
		public cu e(bool A_0)
		{
			if (this.d == A_0)
			{
				return this;
			}
			return new cu(this)
			{
				d = A_0
			};
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x00086E2E File Offset: 0x00085E2E
		public bool g2()
		{
			return this.e;
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x00086E36 File Offset: 0x00085E36
		public cu d(bool A_0)
		{
			if (this.e == A_0)
			{
				return this;
			}
			return new cu(this)
			{
				e = A_0
			};
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x00086E50 File Offset: 0x00085E50
		public bool g3()
		{
			return this.f;
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x00086E58 File Offset: 0x00085E58
		public cu b(bool A_0)
		{
			if (this.f == A_0)
			{
				return this;
			}
			return new cu(this)
			{
				f = A_0
			};
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x00086E72 File Offset: 0x00085E72
		public bool g4()
		{
			return this.g;
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x00086E7A File Offset: 0x00085E7A
		public cu f(bool A_0)
		{
			if (this.g == A_0)
			{
				return this;
			}
			return new cu(this)
			{
				g = A_0
			};
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x00086E94 File Offset: 0x00085E94
		public bool g5()
		{
			return this.h;
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x00086E9C File Offset: 0x00085E9C
		public cu a(bool A_0)
		{
			if (this.h == A_0)
			{
				return this;
			}
			return new cu(this)
			{
				h = A_0
			};
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x00086EB6 File Offset: 0x00085EB6
		public gb g6()
		{
			return this.i;
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x00086EBE File Offset: 0x00085EBE
		public cu a(gb A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("derivedBackgroundColor");
			}
			if (this.i.Equals(A_0))
			{
				return this;
			}
			return new cu(this)
			{
				i = A_0
			};
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x00086EEB File Offset: 0x00085EEB
		public gb g7()
		{
			return this.j;
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x00086EF3 File Offset: 0x00085EF3
		public cu b(gb A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("derivedForegroundColor");
			}
			if (this.j.Equals(A_0))
			{
				return this;
			}
			return new cu(this)
			{
				j = A_0
			};
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x00086F20 File Offset: 0x00085F20
		public ay g8()
		{
			return this.k;
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x00086F28 File Offset: 0x00085F28
		public cu a(ay A_0)
		{
			if (this.k == A_0)
			{
				return this;
			}
			return new cu(this)
			{
				k = A_0
			};
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x00086F42 File Offset: 0x00085F42
		ej ej.b()
		{
			return new cu(this);
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x00086F4A File Offset: 0x00085F4A
		public cu e()
		{
			return new cu(this);
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x00086F52 File Offset: 0x00085F52
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && !(base.GetType() != obj.GetType()) && this.a(obj));
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x00086F79 File Offset: 0x00085F79
		public override int GetHashCode()
		{
			return f3.a(base.GetType().GetHashCode(), this.a());
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x00086F94 File Offset: 0x00085F94
		private bool a(object A_0)
		{
			cu cu = A_0 as cu;
			return cu != null && this.a.Equals(cu.a) && this.b == cu.b && this.c == cu.c && this.d == cu.d && this.e == cu.e && this.f == cu.f && this.g == cu.g && this.h == cu.h && this.i.Equals(cu.i) && this.j.Equals(cu.j) && this.k == cu.k;
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x00087060 File Offset: 0x00086060
		private int a()
		{
			return f3.a(f3.a(f3.a(f3.a(f3.a(f3.a(f3.a(f3.a(f3.a(f3.a(this.a.GetHashCode(), this.b), this.c), this.d), this.e), this.f), this.g), this.h), this.i), this.j), this.k);
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x00087104 File Offset: 0x00086104
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("Font ");
			stringBuilder.Append(this.gx());
			stringBuilder.Append(", ");
			stringBuilder.Append(this.k);
			stringBuilder.Append(", ");
			stringBuilder.Append(this.j.ToString());
			stringBuilder.Append(" on ");
			stringBuilder.Append(this.i.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x0400147D RID: 5245
		private fe a;

		// Token: 0x0400147E RID: 5246
		private int b;

		// Token: 0x0400147F RID: 5247
		private int c;

		// Token: 0x04001480 RID: 5248
		private bool d;

		// Token: 0x04001481 RID: 5249
		private bool e;

		// Token: 0x04001482 RID: 5250
		private bool f;

		// Token: 0x04001483 RID: 5251
		private bool g;

		// Token: 0x04001484 RID: 5252
		private bool h;

		// Token: 0x04001485 RID: 5253
		private gb i = es.b;

		// Token: 0x04001486 RID: 5254
		private gb j = es.a;

		// Token: 0x04001487 RID: 5255
		private ay k;
	}
}
