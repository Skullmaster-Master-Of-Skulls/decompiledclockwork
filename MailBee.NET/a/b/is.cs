using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x02000337 RID: 823
	internal class @is : d1
	{
		// Token: 0x06001DAB RID: 7595 RVA: 0x0008085E File Offset: 0x0007F85E
		public @is(ip A_0) : this(A_0, new du())
		{
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x0008086C File Offset: 0x0007F86C
		public @is(ip A_0, du A_1)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("rtfDocument");
			}
			if (A_1 == null)
			{
				throw new ArgumentNullException("settings");
			}
			this.d = A_0;
			this.e = A_1;
			this.g = new dr(A_1.g());
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x000808DB File Offset: 0x0007F8DB
		public ip at()
		{
			return this.d;
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x000808E3 File Offset: 0x0007F8E3
		public du aa()
		{
			return this.e;
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x000808EB File Offset: 0x0007F8EB
		public ix p()
		{
			return this.f;
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x000808F3 File Offset: 0x0007F8F3
		public void a(ix A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("value");
			}
			this.f = A_0;
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x0008090A File Offset: 0x0007F90A
		public dr al()
		{
			return this.g;
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x00080912 File Offset: 0x0007F912
		public go w()
		{
			return this.b;
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x0008091A File Offset: 0x0007F91A
		protected fc z()
		{
			return this.h;
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x00080922 File Offset: 0x0007F922
		protected i3 am()
		{
			return this.c;
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x0008092A File Offset: 0x0007F92A
		protected bool s()
		{
			return this.c(eb.bd);
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x00080934 File Offset: 0x0007F934
		protected bool k()
		{
			return this.c(eb.b5) || this.c(eb.bb);
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x0008094A File Offset: 0x0007F94A
		protected bool n()
		{
			return this.c(eb.a1);
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x00080954 File Offset: 0x0007F954
		protected virtual string ag()
		{
			return "MailBee.NET Objects";
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x0008095C File Offset: 0x0007F95C
		public string au()
		{
			this.b.a();
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				using (this.h = new fc(stringWriter))
				{
					this.e();
					this.d();
				}
				result = stringWriter.ToString();
			}
			this.c.a();
			return result;
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x000809E0 File Offset: 0x0007F9E0
		protected bool a(eb A_0)
		{
			return this.c.a(A_0);
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x000809EE File Offset: 0x0007F9EE
		protected bool c(eb A_0)
		{
			return this.c.b(A_0);
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x000809FC File Offset: 0x0007F9FC
		protected void b(eb A_0)
		{
			this.z().d(A_0);
			this.c.c(A_0);
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x00080A16 File Offset: 0x0007FA16
		protected void x()
		{
			this.a(false);
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x00080A1F File Offset: 0x0007FA1F
		protected virtual void a(bool A_0)
		{
			this.z().h();
			if (A_0)
			{
				this.z().WriteLine();
			}
			this.c.c();
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x00080A45 File Offset: 0x0007FA45
		protected virtual void @as()
		{
			this.b(eb.b1);
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x00080A4F File Offset: 0x0007FA4F
		protected virtual void ae()
		{
			this.b(eb.a6);
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x00080A59 File Offset: 0x0007FA59
		protected virtual void f()
		{
			this.b(eb.ar);
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x00080A63 File Offset: 0x0007FA63
		protected virtual void h()
		{
			this.b(eb.a2);
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x00080A6D File Offset: 0x0007FA6D
		protected virtual void af()
		{
			this.b(eb.ap);
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x00080A77 File Offset: 0x0007FA77
		protected virtual void u()
		{
			this.b(eb.m);
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x00080A81 File Offset: 0x0007FA81
		protected virtual void aj()
		{
			this.b(eb.n);
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x00080A8B File Offset: 0x0007FA8B
		protected virtual void ak()
		{
			this.b(eb.b);
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x00080A94 File Offset: 0x0007FA94
		protected virtual void m()
		{
			this.b(eb.bd);
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x00080A9E File Offset: 0x0007FA9E
		protected virtual void g()
		{
			this.b(eb.f);
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x00080AA7 File Offset: 0x0007FAA7
		protected virtual void ah()
		{
			this.b(eb.@as);
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x00080AB1 File Offset: 0x0007FAB1
		protected virtual void l()
		{
			this.b(eb.b4);
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x00080ABB File Offset: 0x0007FABB
		protected virtual void t()
		{
			this.b(eb.bj);
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x00080AC5 File Offset: 0x0007FAC5
		protected virtual void j()
		{
			this.b(eb.bo);
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x00080ACF File Offset: 0x0007FACF
		protected virtual void q()
		{
			this.b(eb.b5);
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x00080AD9 File Offset: 0x0007FAD9
		protected virtual void r()
		{
			this.b(eb.bb);
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x00080AE3 File Offset: 0x0007FAE3
		protected virtual void ar()
		{
			this.b(eb.a1);
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x00080AED File Offset: 0x0007FAED
		protected virtual void v()
		{
			this.b(eb.au);
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x00080AF7 File Offset: 0x0007FAF7
		protected virtual void ai()
		{
			this.b(eb.br);
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x00080B01 File Offset: 0x0007FB01
		protected virtual void ap()
		{
			if (string.IsNullOrEmpty(this.e.m()))
			{
				return;
			}
			this.z().WriteLine(this.e.m());
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x00080B2C File Offset: 0x0007FB2C
		protected virtual void ab()
		{
			this.z().a("http-equiv", "content-type");
			string text = "text/html";
			if (!string.IsNullOrEmpty(this.e.l()))
			{
				text = text + "; charset=" + this.e.l();
			}
			this.z().a(global::a.b.ag.ar, text);
			this.ae();
			this.x();
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x00080B98 File Offset: 0x0007FB98
		protected virtual void aq()
		{
			string text = this.ag();
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			this.z().WriteLine();
			this.z().a(global::a.b.ag.u, "generator");
			this.z().a(global::a.b.ag.ar, text);
			this.ae();
			this.x();
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x00080BEC File Offset: 0x0007FBEC
		protected virtual void i()
		{
			if (!this.e.a())
			{
				return;
			}
			foreach (string text in this.e.o())
			{
				if (!string.IsNullOrEmpty(text))
				{
					this.z().WriteLine();
					this.z().a(global::a.b.ag.q, text);
					this.z().a(global::a.b.ag.aj, "text/css");
					this.z().a(global::a.b.ag.ax, "stylesheet");
					this.h();
					this.x();
				}
			}
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x00080CA0 File Offset: 0x0007FCA0
		protected virtual void an()
		{
			this.ab();
			this.aq();
			this.i();
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x00080CB4 File Offset: 0x0007FCB4
		protected virtual void ac()
		{
			if (string.IsNullOrEmpty(this.e.b()))
			{
				return;
			}
			this.z().WriteLine();
			this.@as();
			this.z().Write(this.e.b());
			this.x();
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x00080D04 File Offset: 0x0007FD04
		protected virtual void o()
		{
			if (!this.e.e())
			{
				return;
			}
			this.z().WriteLine();
			this.ai();
			bool flag = true;
			foreach (object obj in this.e.h())
			{
				bf bf = (bf)obj;
				if (bf.bd().Count != 0)
				{
					if (!flag)
					{
						this.z().WriteLine();
					}
					this.z().WriteLine(bf.be());
					this.z().WriteLine("{");
					for (int i = 0; i < bf.bd().Count; i++)
					{
						this.z().WriteLine(string.Format(CultureInfo.InvariantCulture, "  {0}: {1};", new object[]
						{
							bf.bd().Keys[i],
							bf.bd()[i]
						}));
					}
					this.z().Write("}");
					flag = false;
				}
			}
			this.x();
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x00080E38 File Offset: 0x0007FE38
		protected virtual void y()
		{
			foreach (object obj in this.d.dt())
			{
				((i1)obj).o(this);
			}
			this.a();
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x00080E9C File Offset: 0x0007FE9C
		protected virtual void ao()
		{
			if (this.s())
			{
				return;
			}
			this.m();
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x00080EAD File Offset: 0x0007FEAD
		protected virtual void ad()
		{
			if (!this.s())
			{
				return;
			}
			this.a(true);
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x00080EBF File Offset: 0x0007FEBF
		protected virtual bool g(i1 A_0)
		{
			return true;
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x00080EC2 File Offset: 0x0007FEC2
		protected virtual void f(i1 A_0)
		{
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x00080EC4 File Offset: 0x0007FEC4
		protected virtual ja e(i1 A_0)
		{
			ja result = gd.a;
			if (A_0.n() == hu.a)
			{
				result = this.f.pi(A_0 as c8);
			}
			return result;
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x00080EF4 File Offset: 0x0007FEF4
		protected virtual string b(string A_0)
		{
			string text = global::a.au.b(A_0);
			if (this.e.n())
			{
				text = text.Replace(" ", "&nbsp;");
			}
			return text;
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x00080F28 File Offset: 0x0007FF28
		protected override void pe(c8 A_0)
		{
			if (!this.d(A_0))
			{
				return;
			}
			if (A_0.jh().g5() && !this.e.j())
			{
				return;
			}
			ej ej = A_0.jh();
			switch (ej.g8())
			{
			case ay.b:
				this.z().a(global::a.b.ag.b, "center", false);
				break;
			case ay.c:
				this.z().a(global::a.b.ag.b, "right", false);
				break;
			case ay.d:
				this.z().a(global::a.b.ag.b, "justify", false);
				break;
			}
			if (!this.n())
			{
				this.ao();
			}
			if (ej.g1())
			{
				this.g();
			}
			if (ej.g2())
			{
				this.ah();
			}
			if (ej.g3())
			{
				this.l();
			}
			if (ej.g4())
			{
				this.t();
			}
			ja ja = this.e(A_0);
			if (!ja.le())
			{
				if (!string.IsNullOrEmpty(ja.k6()))
				{
					this.z().a(dj.g, ja.k6());
				}
				if (!string.IsNullOrEmpty(ja.k8()))
				{
					this.z().a(dj.a, ja.k8());
				}
				if (!string.IsNullOrEmpty(ja.la()))
				{
					this.z().a(dj.h, ja.la());
				}
				if (!string.IsNullOrEmpty(ja.lc()))
				{
					this.z().a(dj.i, ja.lc());
				}
				this.j();
			}
			bool flag = false;
			if (this.e.f())
			{
				string text = this.a(A_0.jg());
				if (!string.IsNullOrEmpty(text))
				{
					flag = true;
					this.z().a(global::a.b.ag.q, text);
					this.ak();
				}
			}
			string value = this.b(A_0.jg());
			this.z().Write(value);
			if (flag)
			{
				this.x();
			}
			if (!ja.le())
			{
				this.x();
			}
			if (ej.g4())
			{
				this.x();
			}
			if (ej.g3())
			{
				this.x();
			}
			if (ej.g2())
			{
				this.x();
			}
			if (ej.g1())
			{
				this.x();
			}
			this.c(A_0);
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x00081144 File Offset: 0x00080144
		protected override void pf(im A_0)
		{
			if (!this.d(A_0))
			{
				return;
			}
			switch (A_0.m9())
			{
			case ay.b:
				this.z().a(global::a.b.ag.b, "center", false);
				break;
			case ay.c:
				this.z().a(global::a.b.ag.b, "right", false);
				break;
			case ay.d:
				this.z().a(global::a.b.ag.b, "justify", false);
				break;
			}
			this.ao();
			int a_ = this.b.Count + 1;
			string a_2 = this.e.a(a_, A_0.m8());
			int width = this.e.i().gk(A_0.m8(), A_0.na(), A_0.nc(), A_0.ne());
			int height = this.e.i().gl(A_0.m8(), A_0.nb(), A_0.nd(), A_0.nf());
			this.z().a(global::a.b.ag.am, width.ToString());
			this.z().a(global::a.b.ag.p, height.ToString());
			string text = global::a.au.b(a_2);
			this.z().a(global::a.b.ag.ae, text, false);
			this.v();
			this.x();
			this.b.a(new am(text, this.e.i().gf(), new Size(width, height)));
			this.c(A_0);
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x000812A8 File Offset: 0x000802A8
		protected override void pg(w A_0)
		{
			if (!this.d(A_0))
			{
				return;
			}
			RtfVisualSpecialCharKind rtfVisualSpecialCharKind = A_0.o9();
			if (rtfVisualSpecialCharKind != RtfVisualSpecialCharKind.ParagraphNumberBegin)
			{
				if (rtfVisualSpecialCharKind != RtfVisualSpecialCharKind.ParagraphNumberEnd)
				{
					if (this.al().ContainsKey(A_0.o9()))
					{
						this.z().Write(this.al()[A_0.o9()]);
					}
				}
				else
				{
					this.j = false;
				}
			}
			else
			{
				this.j = true;
			}
			this.c(A_0);
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x00081318 File Offset: 0x00080318
		protected override void ph(ap A_0)
		{
			if (!this.d(A_0))
			{
				return;
			}
			switch (A_0.dv())
			{
			case RtfVisualBreakKind.Line:
				this.aj();
				break;
			case RtfVisualBreakKind.Paragraph:
				if (this.s())
				{
					this.ad();
				}
				else if (this.n())
				{
					this.ad();
					this.a(true);
				}
				else
				{
					this.ao();
					this.z().Write("&nbsp;");
					this.ad();
				}
				break;
			}
			this.c(A_0);
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x000813A0 File Offset: 0x000803A0
		private string a(string A_0)
		{
			if (string.IsNullOrEmpty(A_0))
			{
				return null;
			}
			if (this.n == null)
			{
				if (string.IsNullOrEmpty(this.e.d()))
				{
					return null;
				}
				this.n = new Regex(this.e.d());
			}
			if (!this.n.IsMatch(A_0))
			{
				return null;
			}
			return A_0;
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x000813FA File Offset: 0x000803FA
		private void e()
		{
			if ((this.e.k() & global::a.b.n.b) != global::a.b.n.b)
			{
				return;
			}
			this.ap();
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x00081413 File Offset: 0x00080413
		private void d()
		{
			if ((this.e.k() & global::a.b.n.c) == global::a.b.n.c)
			{
				this.f();
			}
			this.c();
			this.b();
			if ((this.e.k() & global::a.b.n.c) == global::a.b.n.c)
			{
				this.a(true);
			}
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x00081452 File Offset: 0x00080452
		private void c()
		{
			if ((this.e.k() & global::a.b.n.d) != global::a.b.n.d)
			{
				return;
			}
			this.af();
			this.an();
			this.ac();
			this.o();
			this.a(true);
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x0008148C File Offset: 0x0008048C
		private void b()
		{
			if ((this.e.k() & global::a.b.n.e) == global::a.b.n.e)
			{
				this.u();
			}
			if ((this.e.k() & global::a.b.n.f) == global::a.b.n.f)
			{
				this.y();
			}
			if ((this.e.k() & global::a.b.n.e) == global::a.b.n.e)
			{
				this.x();
			}
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x000814F3 File Offset: 0x000804F3
		private bool d(i1 A_0)
		{
			if (this.b(A_0))
			{
				return false;
			}
			this.a(A_0);
			return this.g(A_0);
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x0008150E File Offset: 0x0008050E
		private void c(i1 A_0)
		{
			this.f(A_0);
			this.i = A_0;
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x00081520 File Offset: 0x00080520
		private bool b(i1 A_0)
		{
			c8 c = A_0 as c8;
			if (c == null || !this.j)
			{
				return false;
			}
			if (!this.k())
			{
				if ("·".Equals(c.jg()))
				{
					this.q();
				}
				else
				{
					this.r();
				}
			}
			this.ar();
			return true;
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x00081570 File Offset: 0x00080570
		private void a()
		{
			if (this.i == null)
			{
				return;
			}
			this.a(this.i);
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x00081588 File Offset: 0x00080588
		private void a(i1 A_0)
		{
			if (!this.k())
			{
				return;
			}
			ap ap = this.i as ap;
			if (ap == null || ap.dv() != RtfVisualBreakKind.Paragraph)
			{
				return;
			}
			w w = A_0 as w;
			if (w == null || w.o9() != RtfVisualSpecialCharKind.ParagraphNumberBegin)
			{
				this.a(true);
			}
		}

		// Token: 0x04001392 RID: 5010
		public const string a = ".html";

		// Token: 0x04001393 RID: 5011
		private readonly go b = new go();

		// Token: 0x04001394 RID: 5012
		private readonly i3 c = new i3();

		// Token: 0x04001395 RID: 5013
		private readonly ip d;

		// Token: 0x04001396 RID: 5014
		private readonly du e;

		// Token: 0x04001397 RID: 5015
		private ix f = new it();

		// Token: 0x04001398 RID: 5016
		private readonly dr g;

		// Token: 0x04001399 RID: 5017
		private fc h;

		// Token: 0x0400139A RID: 5018
		private i1 i;

		// Token: 0x0400139B RID: 5019
		private bool j;

		// Token: 0x0400139C RID: 5020
		private const string k = "MailBee.NET Objects";

		// Token: 0x0400139D RID: 5021
		private const string l = "&nbsp;";

		// Token: 0x0400139E RID: 5022
		private const string m = "·";

		// Token: 0x0400139F RID: 5023
		private Regex n;
	}
}
