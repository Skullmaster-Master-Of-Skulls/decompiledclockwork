using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MailBee;
using MailBee.ImapMail;

namespace a.f
{
	// Token: 0x020000EA RID: 234
	internal class d : g
	{
		// Token: 0x06000793 RID: 1939 RVA: 0x00022E20 File Offset: 0x00021E20
		public d()
		{
			this.e = null;
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00022E2F File Offset: 0x00021E2F
		protected override void ce()
		{
			this.e = new byte[this.a * 8];
			this.i = new byte[this.a * 8];
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00022E57 File Offset: 0x00021E57
		public override void cf()
		{
			base.cf();
			this.f = -1;
			this.g = null;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00022E70 File Offset: 0x00021E70
		private new int g(m A_0)
		{
			for (int i = this.m.Count - 1; i > -1; i--)
			{
				if (((global::a.f.a)this.m.a(i)).m() == A_0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00022EB1 File Offset: 0x00021EB1
		private new int h()
		{
			return this.g(global::a.f.m.b);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00022EBC File Offset: 0x00021EBC
		private new global::a.f.a g()
		{
			int num = this.h();
			if (num < 0)
			{
				return null;
			}
			return (global::a.f.a)this.m.a(num);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00022EE8 File Offset: 0x00021EE8
		protected override bool cg()
		{
			if (this.j > 0)
			{
				return true;
			}
			if (this.l.Count > 0 && ((v)this.l.b(0)).f == null)
			{
				return this.m.Count < this.l.a();
			}
			if (this.m.Count == 0)
			{
				return true;
			}
			if (this.g(global::a.f.m.d) > -1)
			{
				return false;
			}
			string text = null;
			for (int i = this.l.Count - 1; i > -1; i--)
			{
				v v = (v)this.l.b(i);
				if (v.i && v.f != null)
				{
					text = v.f;
					break;
				}
			}
			if (text == null)
			{
				throw new InvalidOperationException();
			}
			global::a.f.a a = this.g();
			return a == null || a.g() != text;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00022FC0 File Offset: 0x00021FC0
		private new global::a.f.a g(byte[] A_0, int A_1, int A_2)
		{
			string text = null;
			string a_ = null;
			string text2 = null;
			int num = 0;
			int a_2 = 0;
			af a_3 = af.c;
			m m = global::a.f.m.a;
			bool a_4 = false;
			try
			{
				text = this.d.GetString(A_0, A_1, A_2);
				m = global::a.f.r.a(A_0, A_1, A_2, this.d);
			}
			catch (l l)
			{
				throw new MailBeeInvalidTextResponseException(l.ErrorCode, l, this.hs(), l.a(), this.d);
			}
			if (m == global::a.f.m.b || m == global::a.f.m.c)
			{
				try
				{
					if (m == global::a.f.m.b)
					{
						a_ = global::a.f.r.a(text);
					}
					text2 = global::a.f.r.a(A_0, A_1, A_2, this.d, out a_2, out num);
					if (text2 == "NO" || text2 == "BAD")
					{
						a_3 = af.c;
					}
					else
					{
						a_3 = af.a;
					}
				}
				catch (l l2)
				{
					throw new MailBeeInvalidTextResponseException(l2.ErrorCode, l2, this.hs(), l2.a(), this.d);
				}
				if (text2 == "OK" || text2 == "NO" || text2 == "BAD" || text2 == "BYE" || text2 == "PREAUTH")
				{
					a_4 = true;
				}
			}
			else
			{
				a_3 = af.b;
			}
			return new global::a.f.a(null, this.d, text, a_3, null, m, a_, a_2, text2, a_4, null, null, null, null, null, true);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00023120 File Offset: 0x00022120
		protected override int ch(int A_0, out int A_1)
		{
			A_1 = 0;
			int num = 0;
			int num2 = 0;
			int num3 = A_0;
			if (this.k > this.j)
			{
				if (this.e != null)
				{
					this.e(this.g, this.i, num, this.j - num3, num3, this.f.c());
				}
				return -1;
			}
			while ((num2 = global::a.f.r.a(this.i, num, this.k, this.j - this.k, ref this.f)) > 0)
			{
				byte[] array = new byte[num2 - num];
				ao a_ = new ao(array);
				Buffer.BlockCopy(this.i, num, array, 0, num2 - num);
				string text = null;
				string text2 = null;
				string a_2 = null;
				string text3 = null;
				string a_3 = null;
				int num4 = 0;
				int a_4 = 0;
				af a_5 = af.c;
				m m = global::a.f.m.a;
				ArrayList a_6 = null;
				ArrayList arrayList = null;
				ArrayList arrayList2 = null;
				Hashtable a_7 = null;
				bool a_8 = true;
				int num5 = this.f - 2;
				bool a_9 = false;
				try
				{
					text = this.d.GetString(array, 0, num5);
					m = global::a.f.r.a(array, 0, num5, this.d);
				}
				catch (l l)
				{
					throw new MailBeeInvalidTextResponseException(l.ErrorCode, l, this.hs(), l.a(), this.d);
				}
				if (m == global::a.f.m.b || m == global::a.f.m.c)
				{
					try
					{
						if (m == global::a.f.m.b)
						{
							a_2 = global::a.f.r.a(text);
						}
						text3 = global::a.f.r.a(array, 0, num5, this.d, out a_4, out num4);
						if (text3 == "NO" || text3 == "BAD")
						{
							a_5 = af.c;
						}
						else
						{
							a_5 = af.a;
						}
					}
					catch (l l2)
					{
						throw new MailBeeInvalidTextResponseException(l2.ErrorCode, l2, this.hs(), l2.a(), this.d);
					}
					try
					{
						if (text3 == "OK" || text3 == "NO" || text3 == "BAD" || text3 == "BYE" || text3 == "PREAUTH")
						{
							a_9 = true;
							if (num4 < num5 - 1 && array[num4] == 91)
							{
								int num6 = num4 + 1;
								arrayList = global::a.f.r.a(a_, num4, num5 - num4, this.d, null, 1, true, out num4);
								if (arrayList != null && num4 > num6)
								{
									a_3 = this.d.GetString(array, num6, num4 - num6 - 1);
								}
							}
							text2 = global::a.f.r.a(array, 0, num5, this.d, num4);
							a_7 = global::a.f.s.a(arrayList, text3, text2, this.d);
						}
						else
						{
							if (text3 == "FLAGS")
							{
								text2 = global::a.f.r.a(array, 0, num5, this.d, num4);
							}
							arrayList2 = new ArrayList();
							bool a_10 = text3 != "LIST" && text3 != "LSUB";
							a_6 = global::a.f.r.a(a_, num4, array.Length - num4 - 2, this.d, arrayList2, -1, a_10, out num4);
						}
						goto IL_2E6;
					}
					catch (l l3)
					{
						a_8 = false;
						this.f.c().c(new MailBeeInvalidTextResponseException(l3.ErrorCode, l3, this.hs(), l3.a(), this.d));
						goto IL_2E6;
					}
					goto IL_2D1;
				}
				goto IL_2D1;
				IL_2E6:
				global::a.f.a a_11 = new global::a.f.a(array, this.d, text, a_5, text2, m, a_2, a_4, text3, a_9, a_3, a_6, arrayList, arrayList2, a_7, a_8);
				this.m.a(a_11);
				if (this.e != null)
				{
					int num7 = num3 - (this.j - num2);
					this.e(a_11, this.i, num, num2 - num7, num7, this.f.c());
					num3 -= num7;
				}
				if (this.p != null)
				{
					this.p(a_11, this.f.c());
				}
				num = num2;
				this.k = num2;
				this.f = -1;
				this.g = null;
				continue;
				IL_2D1:
				a_5 = af.b;
				text2 = global::a.f.r.a(array, 0, num5, this.d, 2);
				goto IL_2E6;
			}
			if (num2 <= 0)
			{
				A_1 = -num2 + this.a;
				if (this.e != null)
				{
					if (this.g == null)
					{
						if (this.f > -1)
						{
							this.g = this.g(this.i, num, this.f - 2);
							this.e(this.g, this.i, num, num, this.j - num, this.f.c());
						}
					}
					else
					{
						this.e(this.g, this.i, num, this.j - num3, num3, this.f.c());
					}
				}
				this.k = -num2;
			}
			return num;
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x000235E4 File Offset: 0x000225E4
		public override void ci()
		{
			this.l.a(new v(true));
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x000235F8 File Offset: 0x000225F8
		protected override int cj()
		{
			this.m.a(new global::a.f.a(null, this.d, null, af.d, null, global::a.f.m.a, null, 0, null, false, null, null, null, null, null, true));
			return this.j;
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00023630 File Offset: 0x00022630
		protected override MailBeeEmailProtocolNegativeResponseException ck(int A_0, ai A_1, at A_2)
		{
			return new MailBeeImapNegativeResponseException(A_0, A_1, A_2);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0002363A File Offset: 0x0002263A
		public new e i()
		{
			return this.e;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00023642 File Offset: 0x00022642
		public new void g(e A_0)
		{
			this.e = A_0;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0002364C File Offset: 0x0002264C
		protected override Task<int> cl(int A_0, aq<int> A_1)
		{
			d.a a;
			a.e = this;
			a.d = A_0;
			a.c = A_1;
			a.b = AsyncTaskMethodBuilder<int>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<int> b = a.b;
			b.Start<d.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x0400051B RID: 1307
		private new e e;

		// Token: 0x0400051C RID: 1308
		private new int f;

		// Token: 0x0400051D RID: 1309
		private new global::a.f.a g;
	}
}
