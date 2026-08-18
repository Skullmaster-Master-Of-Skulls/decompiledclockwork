using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x020002D3 RID: 723
	internal class h0 : e9, gj
	{
		// Token: 0x06001933 RID: 6451 RVA: 0x000705A7 File Offset: 0x0006F5A7
		public static Stream a(Stream A_0)
		{
			return new gm(A_0);
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x000705AF File Offset: 0x0006F5AF
		public cl h()
		{
			return this.h;
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x000705B7 File Offset: 0x0006F5B7
		public void a(cl A_0)
		{
			this.h = A_0;
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x000705C0 File Offset: 0x0006F5C0
		private h0(bool A_0)
		{
			this.f = new c3(this.i);
			this.c = new k(this.f);
			this.b = new dp(this, this.c.b(), new List<gx>(), this.f);
			this.d = new List<gx>();
			this.e = new List<gx>();
			this.g = null;
			if (A_0)
			{
				this.h = new gp(new byte[this.i.f() * 3]);
			}
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x00070660 File Offset: 0x0006F660
		public h0() : this(true)
		{
			this.f.d(1);
			this.f.a(new int[1]);
			this.e.Add(gx.a(this.i, false));
			this.ii(0, -3);
			this.c.jm(1);
			this.ii(1, -2);
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x000706C6 File Offset: 0x0006F6C6
		public h0(FileStream A_0) : this(A_0, true)
		{
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x000706D0 File Offset: 0x0006F6D0
		private h0(FileStream A_0, bool A_1) : this(false)
		{
			try
			{
				byte[] array = new byte[512];
				g9.a(A_0, array);
				this.f = new c3(array);
				this.h = new hc(A_0);
				this.b();
				A_0.Close();
			}
			catch (IOException ex)
			{
				if (A_1)
				{
					A_0.Close();
				}
				throw ex;
			}
			catch (Exception ex2)
			{
				if (A_1)
				{
					A_0.Close();
				}
				throw ex2;
			}
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x00070750 File Offset: 0x0006F750
		public h0(Stream A_0) : this(false)
		{
			Stream stream = null;
			bool a_ = false;
			try
			{
				stream = A_0;
				he he = he.a(512);
				g9.a(stream, he.a());
				this.f = new c3(he);
				e7.a(this.f.f());
				he he2 = he.a(gx.a(this.f));
				he.b(0);
				he2.b(he.a());
				he2.b(he.f());
				he he3 = he2;
				he3.b(he3.g() + g9.a(stream, he2.a(), he2.g(), (int)stream.Length));
				a_ = true;
				this.h = new gp(he2.a(), he2.g());
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
				this.a(A_0, a_);
			}
			this.b();
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00070838 File Offset: 0x0006F838
		private void a(Stream A_0, bool A_1)
		{
			try
			{
				A_0.Close();
			}
			catch (IOException ex)
			{
				if (A_1)
				{
					throw new Exception(ex.Message);
				}
			}
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00070870 File Offset: 0x0006F870
		private void b()
		{
			this.i = this.f.b();
			d7 d = this.ik();
			foreach (int a_ in this.f.d())
			{
				this.a(a_, d);
			}
			int num = this.f.f() - this.f.d().Length;
			int a_2 = this.f.h();
			for (int j = 0; j < this.f.a(); j++)
			{
				d.a(a_2);
				he a_3 = this.ie(a_2);
				gx gx = gx.a(this.i, a_3);
				gx.d(a_2);
				a_2 = gx.e(this.i.b());
				this.d.Add(gx);
				int num2 = Math.Min(num, this.i.b());
				for (int k = 0; k < num2; k++)
				{
					int num3 = gx.e(k);
					if (num3 == -1 || num3 == -2)
					{
						break;
					}
					this.a(num3, d);
				}
				num -= num2;
			}
			this.c = new k(this.f, this);
			List<gx> list = new List<gx>();
			this.b = new dp(this, this.c.b(), list, this.f);
			a_2 = this.f.e();
			for (int l = 0; l < this.f.c(); l++)
			{
				d.a(a_2);
				he a_4 = this.ie(a_2);
				gx gx2 = gx.a(this.i, a_4);
				gx2.d(a_2);
				list.Add(gx2);
				a_2 = this.ih(a_2);
			}
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x00070A2C File Offset: 0x0006FA2C
		private void a(int A_0, d7 A_1)
		{
			A_1.a(A_0);
			he a_ = this.ie(A_0);
			gx gx = gx.a(this.i, a_);
			gx.d(A_0);
			this.e.Add(gx);
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x00070A68 File Offset: 0x0006FA68
		private gx a(int A_0, bool A_1)
		{
			gx gx = gx.a(this.i, !A_1);
			gx.d(A_0);
			he a_ = he.a(this.i.f());
			int num = (1 + A_0) * this.i.f();
			this.h.m4(a_, (long)num);
			return gx;
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x00070ABC File Offset: 0x0006FABC
		public override he ie(int A_0)
		{
			long a_ = (long)((A_0 + 1) * this.i.f());
			return this.h.m3(this.i.f(), a_);
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x00070AF4 File Offset: 0x0006FAF4
		public override he @if(int A_0)
		{
			he result;
			try
			{
				result = this.ie(A_0);
			}
			catch (IndexOutOfRangeException)
			{
				long a_ = (long)((A_0 + 1) * this.i.f());
				he a_2 = he.a(this.l());
				this.h.m4(a_2, a_);
				result = this.ie(A_0);
			}
			return result;
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x00070B54 File Offset: 0x0006FB54
		public override ct ig(int A_0)
		{
			return gx.b(A_0, this.f, this.e);
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x00070B68 File Offset: 0x0006FB68
		public override int ih(int A_0)
		{
			ct ct = this.ig(A_0);
			return ct.a().e(ct.b());
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x00070B90 File Offset: 0x0006FB90
		public override void ii(int A_0, int A_1)
		{
			ct ct = this.ig(A_0);
			ct.a().a(ct.b(), A_1);
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x00070BB8 File Offset: 0x0006FBB8
		public override int ij()
		{
			int num = 0;
			for (int i = 0; i < this.e.Count; i++)
			{
				int num2 = this.i.e();
				gx gx = this.e[i];
				if (gx.g())
				{
					for (int j = 0; j < num2; j++)
					{
						if (gx.e(j) == -1)
						{
							return num + j;
						}
					}
				}
				num += num2;
			}
			gx gx2 = this.a(num, true);
			gx2.a(0, -3);
			this.e.Add(gx2);
			if (this.f.f() >= 109)
			{
				gx gx3 = null;
				foreach (gx gx4 in this.d)
				{
					if (gx4.g())
					{
						gx3 = gx4;
						break;
					}
				}
				if (gx3 == null)
				{
					gx3 = this.a(num + 1, false);
					gx3.a(0, num);
					gx2.a(1, -4);
					num++;
					if (this.d.Count == 0)
					{
						this.f.b(num);
					}
					else
					{
						this.d[this.d.Count - 1].a(this.i.b(), num);
					}
					this.d.Add(gx3);
					this.f.a(this.d.Count);
				}
				for (int k = 0; k < this.i.b(); k++)
				{
					if (gx3.e(k) == -1)
					{
						gx3.a(k, num);
					}
				}
			}
			else
			{
				int[] array = new int[this.f.f() + 1];
				Array.Copy(this.f.d(), 0, array, 0, array.Length - 1);
				array[array.Length - 1] = num;
				this.f.a(array);
			}
			this.f.d(this.e.Count);
			return num + 1;
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x00070DC8 File Offset: 0x0006FDC8
		public override d7 ik()
		{
			return new d7(this.h.m6(), this);
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x00070DDB File Offset: 0x0006FDDB
		public k d()
		{
			return this.c;
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x00070DE3 File Offset: 0x0006FDE3
		public dp g()
		{
			return this.b;
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x00070DEB File Offset: 0x0006FDEB
		public void a(hw A_0)
		{
			this.c.b(A_0.a());
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x00070DFE File Offset: 0x0006FDFE
		public void a(g8 A_0)
		{
			this.c.b(A_0);
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x00070E0C File Offset: 0x0006FE0C
		public h4 a(Stream A_0, string A_1)
		{
			return this.m().em(A_1, A_0);
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x00070E1B File Offset: 0x0006FE1B
		public h4 a(string A_0, int A_1, dn A_2)
		{
			return this.m().en(A_0, A_1, A_2);
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x00070E2B File Offset: 0x0006FE2B
		public ig a(string A_0)
		{
			return this.m().eo(A_0);
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x00070E39 File Offset: 0x0006FE39
		public void e()
		{
			if (!(this.h is hc))
			{
				throw new ArgumentException("POIFS opened from an inputstream, so WriteFilesystem() may not be called. Use WriteFilesystem(OutputStream) instead");
			}
			this.a();
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x00070E59 File Offset: 0x0006FE59
		public void b(Stream A_0)
		{
			this.a();
			this.h.m5(A_0);
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x00070E70 File Offset: 0x0006FE70
		private void a()
		{
			new ik(this.f).a(this.ie(-1));
			foreach (gx gx in this.e)
			{
				he a_ = this.ie(gx.f());
				ib.a(gx, a_);
			}
			this.b.a();
			this.c.a(new ga(this, this.f.g()));
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x00070F10 File Offset: 0x0006FF10
		public void k()
		{
			this.h.m7();
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x00070F1D File Offset: 0x0006FF1D
		public DirectoryNode m()
		{
			if (this.g == null)
			{
				this.g = new DirectoryNode(this.c.b(), this, null);
			}
			return this.g;
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x00070F45 File Offset: 0x0006FF45
		public az b(string A_0)
		{
			return this.m().a(A_0);
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x00070F53 File Offset: 0x0006FF53
		public void a(EntryNode A_0)
		{
			this.c.a(A_0.Property);
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x00070F68 File Offset: 0x0006FF68
		protected object[] i()
		{
			if (this.jk())
			{
				Array array = ((gj)this.m()).ji();
				object[] array2 = new object[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array2[i] = array.GetValue(i);
				}
				return array2;
			}
			return new object[0];
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x00070FB8 File Offset: 0x0006FFB8
		protected IEnumerator j()
		{
			if (!this.jk())
			{
				return ((gj)this.m()).jj();
			}
			return null;
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x00070FCF File Offset: 0x0006FFCF
		protected string c()
		{
			return "POIFS FileSystem";
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x00070FD6 File Offset: 0x0006FFD6
		public int l()
		{
			return this.i.f();
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x00070FE3 File Offset: 0x0006FFE3
		public y f()
		{
			return this.i;
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x00070FEB File Offset: 0x0006FFEB
		public override int il()
		{
			return this.l();
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x00070FF3 File Offset: 0x0006FFF3
		public bool jk()
		{
			return ((gj)this.m()).jk();
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x00071000 File Offset: 0x00070000
		public string jl()
		{
			return this.c();
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x00071008 File Offset: 0x00070008
		public Array ji()
		{
			return this.i();
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x00071010 File Offset: 0x00070010
		public IEnumerator jj()
		{
			return this.j();
		}

		// Token: 0x0400125F RID: 4703
		private static dm a = gn.a(typeof(h0));

		// Token: 0x04001260 RID: 4704
		private dp b;

		// Token: 0x04001261 RID: 4705
		private k c;

		// Token: 0x04001262 RID: 4706
		private List<gx> d;

		// Token: 0x04001263 RID: 4707
		private List<gx> e;

		// Token: 0x04001264 RID: 4708
		private c3 f;

		// Token: 0x04001265 RID: 4709
		private DirectoryNode g;

		// Token: 0x04001266 RID: 4710
		private cl h;

		// Token: 0x04001267 RID: 4711
		private y i = c5.b;
	}
}
