using System;
using System.Collections;
using System.IO;
using System.Text;

namespace a.b
{
	// Token: 0x020003A5 RID: 933
	internal class fc : TextWriter
	{
		// Token: 0x060021AF RID: 8623 RVA: 0x0008A0F0 File Offset: 0x000890F0
		static fc()
		{
			fc.a = new Hashtable(fc.ab.Length, StringComparer.OrdinalIgnoreCase);
			fc.b = new Hashtable(fc.ac.Length, StringComparer.OrdinalIgnoreCase);
			fc.c = new Hashtable(fc.ad.Length, StringComparer.OrdinalIgnoreCase);
			foreach (fc.g g in fc.ab)
			{
				fc.a.Add(g.b, g);
			}
			foreach (fc.c c in fc.ac)
			{
				fc.b.Add(c.b, c);
			}
			foreach (fc.b b in fc.ad)
			{
				fc.c.Add(b.b, b);
			}
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x0008AE32 File Offset: 0x00089E32
		public fc(TextWriter A_0) : this(A_0, "\t")
		{
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x0008AE40 File Offset: 0x00089E40
		public fc(TextWriter A_0, string A_1)
		{
			this.e = A_0;
			this.f = A_1;
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x0008AE6B File Offset: 0x00089E6B
		internal static string a(dj A_0)
		{
			if (A_0 < (dj)fc.ad.Length)
			{
				return fc.ad[(int)A_0].b;
			}
			return null;
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x0008AE85 File Offset: 0x00089E85
		protected static void a(string A_0, ag A_1)
		{
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x0008AE87 File Offset: 0x00089E87
		protected static void a(string A_0, dj A_1)
		{
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x0008AE89 File Offset: 0x00089E89
		protected static void a(string A_0, eb A_1)
		{
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x0008AE8B File Offset: 0x00089E8B
		public virtual void a(ag A_0, string A_1, bool A_2)
		{
			if (A_2)
			{
				A_1 = au.j(A_1);
			}
			this.a(this.a(A_0), A_1, A_0);
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x0008AEA7 File Offset: 0x00089EA7
		public virtual void a(ag A_0, string A_1)
		{
			if (A_0 != ag.u && A_0 != ag.r)
			{
				A_1 = au.j(A_1);
			}
			this.a(this.a(A_0), A_1, A_0);
		}

		// Token: 0x060021B8 RID: 8632 RVA: 0x0008AECA File Offset: 0x00089ECA
		public virtual void c(string A_0, string A_1, bool A_2)
		{
			if (A_2)
			{
				A_1 = au.j(A_1);
			}
			this.a(A_0, A_1, this.k(A_0));
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x0008AEE8 File Offset: 0x00089EE8
		public virtual void a(string A_0, string A_1)
		{
			ag ag = this.k(A_0);
			if (ag != ag.u && ag != ag.r)
			{
				A_1 = au.j(A_1);
			}
			this.a(A_0, A_1, ag);
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x0008AF18 File Offset: 0x00089F18
		protected virtual void a(string A_0, string A_1, ag A_2)
		{
			this.b();
			this.i[this.l].a = A_0;
			this.i[this.l].c = A_1;
			this.i[this.l].b = A_2;
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x0008AF70 File Offset: 0x00089F70
		protected virtual void a(string A_0, string A_1, dj A_2)
		{
			this.c();
			this.h[this.k].a = A_0;
			this.h[this.k].c = A_1;
			this.h[this.k].b = A_2;
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x0008AFC8 File Offset: 0x00089FC8
		public virtual void c(string A_0, string A_1)
		{
			this.a(A_0, A_1, this.f(A_0));
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x0008AFD9 File Offset: 0x00089FD9
		public virtual void a(dj A_0, string A_1)
		{
			this.a(this.b(A_0), A_1, A_0);
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x0008AFEA File Offset: 0x00089FEA
		public override void Close()
		{
			this.e.Close();
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x0008AFF7 File Offset: 0x00089FF7
		protected virtual string b(ag A_0, string A_1)
		{
			return au.j(A_1);
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x0008AFFF File Offset: 0x00089FFF
		protected string a(string A_0, bool A_1)
		{
			if (A_1)
			{
				return au.j(A_0);
			}
			return A_0;
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x0008B00C File Offset: 0x0008A00C
		protected string m(string A_0)
		{
			return au.a(A_0);
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x0008B014 File Offset: 0x0008A014
		protected virtual void q()
		{
			fc.f f = default(fc.f);
			for (int i = 0; i <= this.l; i++)
			{
				fc.f f2 = this.i[i];
				if (this.b(f2.a, f2.c, f2.b))
				{
					if (f2.b == ag.af)
					{
						f = f2;
					}
					else
					{
						this.b(f2.a, f2.c, false);
					}
				}
			}
			if (this.k != -1 || f.c != null)
			{
				this.Write(' ');
				this.Write("style");
				this.Write("=\"");
				for (int j = 0; j <= this.k; j++)
				{
					fc.d d = this.h[j];
					if (this.b(d.a, d.c, d.b))
					{
						this.a(d.a, d.c, false);
					}
				}
				this.Write(f.c);
				this.Write('"');
			}
			this.k = (this.l = -1);
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x0008B12B File Offset: 0x0008A12B
		public override void Flush()
		{
			this.e.Flush();
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x0008B138 File Offset: 0x0008A138
		protected ag k(string A_0)
		{
			object obj = fc.b[A_0];
			if (obj == null)
			{
				return (ag)(-1);
			}
			return ((fc.c)obj).a;
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x0008B161 File Offset: 0x0008A161
		protected string a(ag A_0)
		{
			if (A_0 < (ag)fc.ac.Length)
			{
				return fc.ac[(int)A_0].b;
			}
			return null;
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x0008B17C File Offset: 0x0008A17C
		protected dj f(string A_0)
		{
			object obj = fc.c[A_0];
			if (obj == null)
			{
				return (dj)(-1);
			}
			return ((fc.b)obj).a;
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x0008B1A5 File Offset: 0x0008A1A5
		protected string b(dj A_0)
		{
			return fc.a(A_0);
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x0008B1B0 File Offset: 0x0008A1B0
		protected virtual eb b(string A_0)
		{
			object obj = fc.a[A_0];
			if (obj == null)
			{
				return eb.a;
			}
			return ((fc.g)obj).a;
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x0008B1D9 File Offset: 0x0008A1D9
		internal static string b(eb A_0)
		{
			if (A_0 < (eb)fc.ab.Length)
			{
				return fc.ab[(int)A_0].b;
			}
			return null;
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x0008B1F3 File Offset: 0x0008A1F3
		protected virtual string e(eb A_0)
		{
			if (A_0 < (eb)fc.ab.Length)
			{
				return fc.ab[(int)A_0].b;
			}
			return null;
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x0008B210 File Offset: 0x0008A210
		protected bool b(ag A_0)
		{
			string text;
			return this.a(A_0, out text);
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x0008B228 File Offset: 0x0008A228
		protected bool a(ag A_0, out string A_1)
		{
			for (int i = 0; i <= this.l; i++)
			{
				if (this.i[i].b == A_0)
				{
					A_1 = this.i[i].c;
					return true;
				}
			}
			A_1 = null;
			return false;
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x0008B274 File Offset: 0x0008A274
		protected bool c(dj A_0)
		{
			string text;
			return this.a(A_0, out text);
		}

		// Token: 0x060021CE RID: 8654 RVA: 0x0008B28C File Offset: 0x0008A28C
		protected bool a(dj A_0, out string A_1)
		{
			for (int i = 0; i <= this.k; i++)
			{
				if (this.h[i].b == A_0)
				{
					A_1 = this.h[i].c;
					return true;
				}
			}
			A_1 = null;
			return false;
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x0008B2D7 File Offset: 0x0008A2D7
		protected virtual bool b(string A_0, string A_1, ag A_2)
		{
			return true;
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x0008B2DA File Offset: 0x0008A2DA
		protected virtual bool b(string A_0, string A_1, dj A_2)
		{
			return true;
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x0008B2DD File Offset: 0x0008A2DD
		protected virtual bool b(string A_0, eb A_1)
		{
			return true;
		}

		// Token: 0x060021D2 RID: 8658 RVA: 0x0008B2E0 File Offset: 0x0008A2E0
		protected virtual void p()
		{
			if (!this.g)
			{
				return;
			}
			this.g = false;
			for (int i = 0; i < this.f(); i++)
			{
				this.e.Write(this.f);
			}
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x0008B31F File Offset: 0x0008A31F
		protected string e()
		{
			if (this.m == -1)
			{
				throw new InvalidOperationException();
			}
			string result = this.o();
			this.m--;
			return result;
		}

		// Token: 0x060021D4 RID: 8660 RVA: 0x0008B344 File Offset: 0x0008A344
		protected void l(string A_0)
		{
			this.a();
			this.i(A_0);
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x0008B353 File Offset: 0x0008A353
		private void a(eb A_0)
		{
			this.a();
			this.c(A_0);
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x0008B362 File Offset: 0x0008A362
		protected virtual string g()
		{
			return null;
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x0008B365 File Offset: 0x0008A365
		protected virtual string j()
		{
			return null;
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x0008B368 File Offset: 0x0008A368
		protected virtual string n()
		{
			return null;
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x0008B36B File Offset: 0x0008A36B
		protected virtual string r()
		{
			return null;
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x0008B36E File Offset: 0x0008A36E
		public virtual void p(string A_0)
		{
			if (!this.b(A_0, this.b(A_0)))
			{
				return;
			}
			this.l(A_0);
			this.d();
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x0008B38E File Offset: 0x0008A38E
		public virtual void d(eb A_0)
		{
			if (!this.b(this.e(A_0), A_0))
			{
				return;
			}
			this.a(A_0);
			this.d();
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x0008B3AE File Offset: 0x0008A3AE
		private void a(string A_0)
		{
			if (A_0 != null)
			{
				this.Write(A_0);
			}
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x0008B3BC File Offset: 0x0008A3BC
		private void d()
		{
			this.a(this.r());
			this.o(this.o());
			this.q();
			eb eb = (this.k() < (eb)fc.ab.Length) ? this.k() : eb.a;
			switch (fc.ab[(int)eb].c)
			{
			case fc.a.a:
			{
				this.Write('>');
				this.WriteLine();
				int num = this.f();
				this.a(num + 1);
				break;
			}
			case fc.a.b:
				this.Write('>');
				break;
			case fc.a.c:
				this.Write(" />");
				break;
			}
			this.a(this.n());
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x0008B464 File Offset: 0x0008A464
		public virtual void h()
		{
			this.a(this.g());
			eb eb = (this.k() < (eb)fc.ab.Length) ? this.k() : eb.a;
			switch (fc.ab[(int)eb].c)
			{
			case fc.a.a:
			{
				int num = this.f();
				this.a(num - 1);
				this.j("");
				this.c(this.o());
				break;
			}
			case fc.a.b:
				this.c(this.o());
				break;
			}
			this.a(this.j());
			this.e();
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x0008B4FF File Offset: 0x0008A4FF
		public virtual void b(string A_0, string A_1, bool A_2)
		{
			this.Write(' ');
			this.Write(A_0);
			if (A_1 != null)
			{
				this.Write("=\"");
				A_1 = this.a(A_1, A_2);
				this.Write(A_1);
				this.Write('"');
			}
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x0008B537 File Offset: 0x0008A537
		public virtual void o(string A_0)
		{
			this.Write('<');
			this.Write(A_0);
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x0008B548 File Offset: 0x0008A548
		public virtual void c(string A_0)
		{
			this.Write("</");
			this.Write(A_0);
			this.Write('>');
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x0008B564 File Offset: 0x0008A564
		public virtual void g(string A_0)
		{
			this.Write('<');
			this.Write(A_0);
			this.Write('>');
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x0008B57D File Offset: 0x0008A57D
		public virtual void d(string A_0, string A_1)
		{
			this.a(A_0, A_1, false);
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x0008B588 File Offset: 0x0008A588
		public virtual void a(string A_0, string A_1, bool A_2)
		{
			this.Write(A_0);
			this.Write(':');
			this.Write(this.a(A_1, A_2));
			this.Write(';');
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x0008B5AF File Offset: 0x0008A5AF
		public override void Write(char[] buffer, int index, int count)
		{
			this.p();
			this.e.Write(buffer, index, count);
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x0008B5C5 File Offset: 0x0008A5C5
		public override void Write(double value)
		{
			this.p();
			this.e.Write(value);
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x0008B5D9 File Offset: 0x0008A5D9
		public override void Write(char value)
		{
			this.p();
			this.e.Write(value);
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x0008B5ED File Offset: 0x0008A5ED
		public override void Write(char[] buffer)
		{
			this.p();
			this.e.Write(buffer);
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x0008B601 File Offset: 0x0008A601
		public override void Write(int value)
		{
			this.p();
			this.e.Write(value);
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x0008B615 File Offset: 0x0008A615
		public override void Write(string format, object arg0)
		{
			this.p();
			this.e.Write(format, arg0);
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x0008B62A File Offset: 0x0008A62A
		public override void Write(string format, object arg0, object arg1)
		{
			this.p();
			this.e.Write(format, arg0, arg1);
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x0008B640 File Offset: 0x0008A640
		public override void Write(string format, params object[] args)
		{
			this.p();
			this.e.Write(format, args);
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x0008B655 File Offset: 0x0008A655
		public override void Write(string s)
		{
			this.p();
			this.e.Write(s);
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x0008B669 File Offset: 0x0008A669
		public override void Write(long value)
		{
			this.p();
			this.e.Write(value);
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x0008B67D File Offset: 0x0008A67D
		public override void Write(object value)
		{
			this.p();
			this.e.Write(value);
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x0008B691 File Offset: 0x0008A691
		public override void Write(float value)
		{
			this.p();
			this.e.Write(value);
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x0008B6A5 File Offset: 0x0008A6A5
		public override void Write(bool value)
		{
			this.p();
			this.e.Write(value);
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x0008B6B9 File Offset: 0x0008A6B9
		public virtual void b(string A_0, string A_1)
		{
			this.b(A_0, A_1, false);
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x0008B6C4 File Offset: 0x0008A6C4
		public override void WriteLine(char value)
		{
			this.p();
			this.e.WriteLine(value);
			this.g = true;
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x0008B6DF File Offset: 0x0008A6DF
		public override void WriteLine(long value)
		{
			this.p();
			this.e.WriteLine(value);
			this.g = true;
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x0008B6FA File Offset: 0x0008A6FA
		public override void WriteLine(object value)
		{
			this.p();
			this.e.WriteLine(value);
			this.g = true;
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x0008B715 File Offset: 0x0008A715
		public override void WriteLine(double value)
		{
			this.p();
			this.e.WriteLine(value);
			this.g = true;
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x0008B730 File Offset: 0x0008A730
		public override void WriteLine(char[] buffer, int index, int count)
		{
			this.p();
			this.e.WriteLine(buffer, index, count);
			this.g = true;
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x0008B74D File Offset: 0x0008A74D
		public override void WriteLine(char[] buffer)
		{
			this.p();
			this.e.WriteLine(buffer);
			this.g = true;
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x0008B768 File Offset: 0x0008A768
		public override void WriteLine(bool value)
		{
			this.p();
			this.e.WriteLine(value);
			this.g = true;
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x0008B783 File Offset: 0x0008A783
		public override void WriteLine()
		{
			this.p();
			this.e.WriteLine();
			this.g = true;
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x0008B79D File Offset: 0x0008A79D
		public override void WriteLine(int value)
		{
			this.p();
			this.e.WriteLine(value);
			this.g = true;
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x0008B7B8 File Offset: 0x0008A7B8
		public override void WriteLine(string format, object arg0, object arg1)
		{
			this.p();
			this.e.WriteLine(format, arg0, arg1);
			this.g = true;
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x0008B7D5 File Offset: 0x0008A7D5
		public override void WriteLine(string format, object arg0)
		{
			this.p();
			this.e.WriteLine(format, arg0);
			this.g = true;
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x0008B7F1 File Offset: 0x0008A7F1
		public override void WriteLine(string format, params object[] args)
		{
			this.p();
			this.e.WriteLine(format, args);
			this.g = true;
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x0008B80D File Offset: 0x0008A80D
		public override void WriteLine(uint value)
		{
			this.p();
			this.e.WriteLine(value);
			this.g = true;
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x0008B828 File Offset: 0x0008A828
		public override void WriteLine(string s)
		{
			this.p();
			this.e.WriteLine(s);
			this.g = true;
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x0008B843 File Offset: 0x0008A843
		public override void WriteLine(float value)
		{
			this.p();
			this.e.WriteLine(value);
			this.g = true;
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x0008B85E File Offset: 0x0008A85E
		public void j(string A_0)
		{
			this.e.WriteLine(A_0);
			this.g = true;
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x0008B873 File Offset: 0x0008A873
		public override Encoding get_Encoding()
		{
			return this.e.Encoding;
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x0008B880 File Offset: 0x0008A880
		public int f()
		{
			return this.d;
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x0008B888 File Offset: 0x0008A888
		public void a(int A_0)
		{
			this.d = A_0;
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x0008B891 File Offset: 0x0008A891
		public TextWriter m()
		{
			return this.e;
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x0008B899 File Offset: 0x0008A899
		public void a(TextWriter A_0)
		{
			this.e = A_0;
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x0008B8A2 File Offset: 0x0008A8A2
		public override string get_NewLine()
		{
			return this.e.NewLine;
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x0008B8AF File Offset: 0x0008A8AF
		public override void set_NewLine(string value)
		{
			this.e.NewLine = value;
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x0008B8BD File Offset: 0x0008A8BD
		protected eb k()
		{
			if (this.m == -1)
			{
				throw new InvalidOperationException();
			}
			return this.j[this.m].b;
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x0008B8E4 File Offset: 0x0008A8E4
		protected void c(eb A_0)
		{
			this.j[this.m].b = A_0;
			this.j[this.m].a = this.e(A_0);
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x0008B91A File Offset: 0x0008A91A
		protected string o()
		{
			if (this.m == -1)
			{
				throw new InvalidOperationException();
			}
			return this.j[this.m].a;
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x0008B944 File Offset: 0x0008A944
		protected void i(string A_0)
		{
			this.j[this.m].a = A_0;
			this.j[this.m].b = this.b(A_0);
			if (this.j[this.m].b != eb.a)
			{
				this.j[this.m].a = this.e(this.j[this.m].b);
			}
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x0008B9D0 File Offset: 0x0008A9D0
		private void c()
		{
			if (this.h == null)
			{
				this.h = new fc.d[16];
			}
			int num = this.k + 1;
			this.k = num;
			if (num < this.h.Length)
			{
				return;
			}
			fc.d[] destinationArray = new fc.d[this.h.Length * 2];
			Array.Copy(this.h, destinationArray, this.h.Length);
			this.h = destinationArray;
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x0008BA3C File Offset: 0x0008AA3C
		private void b()
		{
			if (this.i == null)
			{
				this.i = new fc.f[16];
			}
			int num = this.l + 1;
			this.l = num;
			if (num < this.i.Length)
			{
				return;
			}
			fc.f[] destinationArray = new fc.f[this.i.Length * 2];
			Array.Copy(this.i, destinationArray, this.i.Length);
			this.i = destinationArray;
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x0008BAA8 File Offset: 0x0008AAA8
		private void a()
		{
			if (this.j == null)
			{
				this.j = new fc.e[16];
			}
			int num = this.m + 1;
			this.m = num;
			if (num < this.j.Length)
			{
				return;
			}
			fc.e[] destinationArray = new fc.e[this.j.Length * 2];
			Array.Copy(this.j, destinationArray, this.j.Length);
			this.j = destinationArray;
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x0008BB11 File Offset: 0x0008AB11
		public virtual bool e(string A_0)
		{
			return true;
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x0008BB14 File Offset: 0x0008AB14
		public virtual void i()
		{
			string a_ = this.e(eb.n);
			this.o(a_);
			this.Write(" />");
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x0008BB3C File Offset: 0x0008AB3C
		public virtual void d(string A_0)
		{
			this.Write(au.b(A_0));
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x0008BB4A File Offset: 0x0008AB4A
		public virtual void h(string A_0)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x0008BB51 File Offset: 0x0008AB51
		public virtual void n(string A_0)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x0008BB58 File Offset: 0x0008AB58
		protected void b(string A_0, bool A_1)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x0008BB5F File Offset: 0x0008AB5F
		public virtual void l()
		{
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x0008BB61 File Offset: 0x0008AB61
		public virtual void s()
		{
		}

		// Token: 0x04001593 RID: 5523
		private static readonly Hashtable a;

		// Token: 0x04001594 RID: 5524
		private static readonly Hashtable b;

		// Token: 0x04001595 RID: 5525
		private static readonly Hashtable c;

		// Token: 0x04001596 RID: 5526
		private int d;

		// Token: 0x04001597 RID: 5527
		private TextWriter e;

		// Token: 0x04001598 RID: 5528
		private string f;

		// Token: 0x04001599 RID: 5529
		private bool g;

		// Token: 0x0400159A RID: 5530
		private fc.d[] h;

		// Token: 0x0400159B RID: 5531
		private fc.f[] i;

		// Token: 0x0400159C RID: 5532
		private fc.e[] j;

		// Token: 0x0400159D RID: 5533
		private int k = -1;

		// Token: 0x0400159E RID: 5534
		private int l = -1;

		// Token: 0x0400159F RID: 5535
		private int m = -1;

		// Token: 0x040015A0 RID: 5536
		public const string n = "\t";

		// Token: 0x040015A1 RID: 5537
		public const char o = '"';

		// Token: 0x040015A2 RID: 5538
		public const string p = "</";

		// Token: 0x040015A3 RID: 5539
		public const char q = '=';

		// Token: 0x040015A4 RID: 5540
		public const string r = "=\"";

		// Token: 0x040015A5 RID: 5541
		public const string s = " /";

		// Token: 0x040015A6 RID: 5542
		public const string t = " />";

		// Token: 0x040015A7 RID: 5543
		public const char u = ';';

		// Token: 0x040015A8 RID: 5544
		public const char v = '\'';

		// Token: 0x040015A9 RID: 5545
		public const char w = '/';

		// Token: 0x040015AA RID: 5546
		public const char x = ' ';

		// Token: 0x040015AB RID: 5547
		public const char y = ':';

		// Token: 0x040015AC RID: 5548
		public const char z = '<';

		// Token: 0x040015AD RID: 5549
		public const char aa = '>';

		// Token: 0x040015AE RID: 5550
		private static fc.g[] ab = new fc.g[]
		{
			new fc.g(eb.a, "", fc.a.a),
			new fc.g(eb.b, "a", fc.a.b),
			new fc.g(eb.c, "acronym", fc.a.b),
			new fc.g(eb.d, "address", fc.a.a),
			new fc.g(eb.e, "area", fc.a.a),
			new fc.g(eb.f, "b", fc.a.b),
			new fc.g(eb.g, "base", fc.a.c),
			new fc.g(eb.h, "basefont", fc.a.c),
			new fc.g(eb.i, "bdo", fc.a.b),
			new fc.g(eb.j, "bgsound", fc.a.c),
			new fc.g(eb.k, "big", fc.a.b),
			new fc.g(eb.l, "blockquote", fc.a.a),
			new fc.g(eb.m, "body", fc.a.a),
			new fc.g(eb.n, "br", fc.a.a),
			new fc.g(eb.o, "button", fc.a.b),
			new fc.g(eb.p, "caption", fc.a.a),
			new fc.g(eb.q, "center", fc.a.a),
			new fc.g(eb.r, "cite", fc.a.b),
			new fc.g(eb.s, "code", fc.a.b),
			new fc.g(eb.t, "col", fc.a.c),
			new fc.g(eb.u, "colgroup", fc.a.a),
			new fc.g(eb.v, "dd", fc.a.b),
			new fc.g(eb.w, "del", fc.a.b),
			new fc.g(eb.x, "dfn", fc.a.b),
			new fc.g(eb.y, "dir", fc.a.a),
			new fc.g(eb.z, "div", fc.a.a),
			new fc.g(eb.aa, "dl", fc.a.a),
			new fc.g(eb.ab, "dt", fc.a.b),
			new fc.g(eb.ac, "em", fc.a.b),
			new fc.g(eb.ad, "embed", fc.a.c),
			new fc.g(eb.ae, "fieldset", fc.a.a),
			new fc.g(eb.af, "font", fc.a.b),
			new fc.g(eb.ag, "form", fc.a.a),
			new fc.g(eb.ah, "frame", fc.a.c),
			new fc.g(eb.ai, "frameset", fc.a.a),
			new fc.g(eb.aj, "h1", fc.a.a),
			new fc.g(eb.ak, "h2", fc.a.a),
			new fc.g(eb.al, "h3", fc.a.a),
			new fc.g(eb.am, "h4", fc.a.a),
			new fc.g(eb.an, "h5", fc.a.a),
			new fc.g(eb.ao, "h6", fc.a.a),
			new fc.g(eb.ap, "head", fc.a.a),
			new fc.g(eb.aq, "hr", fc.a.c),
			new fc.g(eb.ar, "html", fc.a.a),
			new fc.g(eb.@as, "i", fc.a.b),
			new fc.g(eb.at, "iframe", fc.a.a),
			new fc.g(eb.au, "img", fc.a.c),
			new fc.g(eb.av, "input", fc.a.c),
			new fc.g(eb.aw, "ins", fc.a.b),
			new fc.g(eb.ax, "isindex", fc.a.c),
			new fc.g(eb.ay, "kbd", fc.a.b),
			new fc.g(eb.az, "label", fc.a.b),
			new fc.g(eb.a0, "legend", fc.a.a),
			new fc.g(eb.a1, "li", fc.a.b),
			new fc.g(eb.a2, "link", fc.a.c),
			new fc.g(eb.a3, "map", fc.a.a),
			new fc.g(eb.a4, "marquee", fc.a.a),
			new fc.g(eb.a5, "menu", fc.a.a),
			new fc.g(eb.a6, "meta", fc.a.c),
			new fc.g(eb.a7, "nobr", fc.a.b),
			new fc.g(eb.a8, "noframes", fc.a.a),
			new fc.g(eb.a9, "noscript", fc.a.a),
			new fc.g(eb.ba, "object", fc.a.a),
			new fc.g(eb.bb, "ol", fc.a.a),
			new fc.g(eb.bc, "option", fc.a.a),
			new fc.g(eb.bd, "p", fc.a.b),
			new fc.g(eb.be, "param", fc.a.a),
			new fc.g(eb.bf, "pre", fc.a.a),
			new fc.g(eb.bg, "q", fc.a.b),
			new fc.g(eb.bh, "rt", fc.a.a),
			new fc.g(eb.bi, "ruby", fc.a.a),
			new fc.g(eb.bj, "s", fc.a.b),
			new fc.g(eb.bk, "samp", fc.a.b),
			new fc.g(eb.bl, "script", fc.a.a),
			new fc.g(eb.bm, "select", fc.a.a),
			new fc.g(eb.bn, "small", fc.a.a),
			new fc.g(eb.bo, "span", fc.a.b),
			new fc.g(eb.bp, "strike", fc.a.b),
			new fc.g(eb.bq, "strong", fc.a.b),
			new fc.g(eb.br, "style", fc.a.a),
			new fc.g(eb.bs, "sub", fc.a.b),
			new fc.g(eb.bt, "sup", fc.a.b),
			new fc.g(eb.bu, "table", fc.a.a),
			new fc.g(eb.bv, "tbody", fc.a.a),
			new fc.g(eb.bw, "td", fc.a.b),
			new fc.g(eb.bx, "textarea", fc.a.b),
			new fc.g(eb.by, "tfoot", fc.a.a),
			new fc.g(eb.bz, "th", fc.a.b),
			new fc.g(eb.b0, "thead", fc.a.a),
			new fc.g(eb.b1, "title", fc.a.a),
			new fc.g(eb.b2, "tr", fc.a.a),
			new fc.g(eb.b3, "tt", fc.a.b),
			new fc.g(eb.b4, "u", fc.a.b),
			new fc.g(eb.b5, "ul", fc.a.a),
			new fc.g(eb.b6, "var", fc.a.b),
			new fc.g(eb.b7, "wbr", fc.a.c),
			new fc.g(eb.b8, "xml", fc.a.a)
		};

		// Token: 0x040015AF RID: 5551
		private static fc.c[] ac = new fc.c[]
		{
			new fc.c(ag.a, "accesskey"),
			new fc.c(ag.b, "align"),
			new fc.c(ag.c, "alt"),
			new fc.c(ag.d, "background"),
			new fc.c(ag.e, "bgcolor"),
			new fc.c(ag.f, "border"),
			new fc.c(ag.g, "bordercolor"),
			new fc.c(ag.h, "cellpadding"),
			new fc.c(ag.i, "cellspacing"),
			new fc.c(ag.j, "checked"),
			new fc.c(ag.k, "class"),
			new fc.c(ag.l, "cols"),
			new fc.c(ag.m, "colspan"),
			new fc.c(ag.n, "disabled"),
			new fc.c(ag.o, "for"),
			new fc.c(ag.p, "height"),
			new fc.c(ag.q, "href"),
			new fc.c(ag.r, "id"),
			new fc.c(ag.s, "maxlength"),
			new fc.c(ag.t, "multiple"),
			new fc.c(ag.u, "name"),
			new fc.c(ag.v, "nowrap"),
			new fc.c(ag.w, "onchange"),
			new fc.c(ag.x, "onclick"),
			new fc.c(ag.y, "readonly"),
			new fc.c(ag.z, "rows"),
			new fc.c(ag.aa, "rowspan"),
			new fc.c(ag.ab, "rules"),
			new fc.c(ag.ac, "selected"),
			new fc.c(ag.ad, "size"),
			new fc.c(ag.ae, "src"),
			new fc.c(ag.af, "style"),
			new fc.c(ag.ag, "tabindex"),
			new fc.c(ag.ah, "target"),
			new fc.c(ag.ai, "title"),
			new fc.c(ag.aj, "type"),
			new fc.c(ag.ak, "valign"),
			new fc.c(ag.al, "value"),
			new fc.c(ag.am, "width"),
			new fc.c(ag.an, "wrap"),
			new fc.c(ag.ao, "abbr"),
			new fc.c(ag.ap, "autocomplete"),
			new fc.c(ag.aq, "axis"),
			new fc.c(ag.ar, "content"),
			new fc.c(ag.@as, "coords"),
			new fc.c(ag.at, "_designerregion"),
			new fc.c(ag.au, "dir"),
			new fc.c(ag.av, "headers"),
			new fc.c(ag.aw, "longdesc"),
			new fc.c(ag.ax, "rel"),
			new fc.c(ag.ay, "scope"),
			new fc.c(ag.az, "shape"),
			new fc.c(ag.a0, "usemap"),
			new fc.c(ag.a1, "vcard_name")
		};

		// Token: 0x040015B0 RID: 5552
		private static fc.b[] ad = new fc.b[]
		{
			new fc.b(dj.a, "background-color"),
			new fc.b(dj.b, "background-image"),
			new fc.b(dj.c, "border-collapse"),
			new fc.b(dj.d, "border-color"),
			new fc.b(dj.e, "border-style"),
			new fc.b(dj.f, "border-width"),
			new fc.b(dj.g, "color"),
			new fc.b(dj.h, "font-family"),
			new fc.b(dj.i, "font-size"),
			new fc.b(dj.j, "font-style"),
			new fc.b(dj.k, "font-weight"),
			new fc.b(dj.l, "height"),
			new fc.b(dj.m, "text-decoration"),
			new fc.b(dj.n, "width"),
			new fc.b(dj.o, "list-style-image"),
			new fc.b(dj.p, "list-style-type"),
			new fc.b(dj.q, "cursor"),
			new fc.b(dj.r, "direction"),
			new fc.b(dj.s, "display"),
			new fc.b(dj.t, "filter"),
			new fc.b(dj.u, "font-variant"),
			new fc.b(dj.v, "left"),
			new fc.b(dj.w, "margin"),
			new fc.b(dj.x, "margin-bottom"),
			new fc.b(dj.y, "margin-left"),
			new fc.b(dj.z, "margin-right"),
			new fc.b(dj.aa, "margin-top"),
			new fc.b(dj.ab, "overflow"),
			new fc.b(dj.ac, "overflow-x"),
			new fc.b(dj.ad, "overflow-y"),
			new fc.b(dj.ae, "padding"),
			new fc.b(dj.af, "padding-bottom"),
			new fc.b(dj.ag, "padding-left"),
			new fc.b(dj.ah, "padding-right"),
			new fc.b(dj.ai, "padding-top"),
			new fc.b(dj.aj, "position"),
			new fc.b(dj.ak, "text-align"),
			new fc.b(dj.al, "vertical-align"),
			new fc.b(dj.am, "text-overflow"),
			new fc.b(dj.an, "top"),
			new fc.b(dj.ao, "visibility"),
			new fc.b(dj.ap, "white-space"),
			new fc.b(dj.aq, "z-index")
		};

		// Token: 0x020003A6 RID: 934
		private struct e
		{
			// Token: 0x040015B1 RID: 5553
			public string a;

			// Token: 0x040015B2 RID: 5554
			public eb b;
		}

		// Token: 0x020003A7 RID: 935
		private struct d
		{
			// Token: 0x040015B3 RID: 5555
			public string a;

			// Token: 0x040015B4 RID: 5556
			public dj b;

			// Token: 0x040015B5 RID: 5557
			public string c;
		}

		// Token: 0x020003A8 RID: 936
		private struct f
		{
			// Token: 0x040015B6 RID: 5558
			public string a;

			// Token: 0x040015B7 RID: 5559
			public ag b;

			// Token: 0x040015B8 RID: 5560
			public string c;
		}

		// Token: 0x020003A9 RID: 937
		private enum a
		{
			// Token: 0x040015BA RID: 5562
			a,
			// Token: 0x040015BB RID: 5563
			b,
			// Token: 0x040015BC RID: 5564
			c
		}

		// Token: 0x020003AA RID: 938
		private sealed class g
		{
			// Token: 0x06002219 RID: 8729 RVA: 0x0008BB63 File Offset: 0x0008AB63
			public g(eb A_0, string A_1, fc.a A_2)
			{
				this.a = A_0;
				this.b = A_1;
				this.c = A_2;
			}

			// Token: 0x040015BD RID: 5565
			public readonly eb a;

			// Token: 0x040015BE RID: 5566
			public readonly string b;

			// Token: 0x040015BF RID: 5567
			public readonly fc.a c;
		}

		// Token: 0x020003AB RID: 939
		private sealed class b
		{
			// Token: 0x0600221A RID: 8730 RVA: 0x0008BB80 File Offset: 0x0008AB80
			public b(dj A_0, string A_1)
			{
				this.a = A_0;
				this.b = A_1;
			}

			// Token: 0x040015C0 RID: 5568
			public readonly dj a;

			// Token: 0x040015C1 RID: 5569
			public readonly string b;
		}

		// Token: 0x020003AC RID: 940
		private sealed class c
		{
			// Token: 0x0600221B RID: 8731 RVA: 0x0008BB96 File Offset: 0x0008AB96
			public c(ag A_0, string A_1)
			{
				this.a = A_0;
				this.b = A_1;
			}

			// Token: 0x040015C2 RID: 5570
			public readonly ag a;

			// Token: 0x040015C3 RID: 5571
			public readonly string b;
		}
	}
}
