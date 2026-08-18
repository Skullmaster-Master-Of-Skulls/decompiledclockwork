using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace a.b
{
	// Token: 0x020002CF RID: 719
	internal class b2 : ig
	{
		// Token: 0x060018F1 RID: 6385 RVA: 0x0006F950 File Offset: 0x0006E950
		public b2(ig A_0, ICollection<string> A_1)
		{
			this.c = A_0;
			this.a = new List<string>();
			this.b = new Dictionary<string, List<string>>();
			foreach (string text in A_1)
			{
				int num = text.IndexOf('/');
				if (num == -1)
				{
					this.a.Add(text);
				}
				else
				{
					string key = text.Substring(0, num);
					string item = text.Substring(num + 1);
					if (!this.b.ContainsKey(key))
					{
						this.b.Add(key, new List<string>());
					}
					this.b[key].Add(item);
				}
			}
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x0006FA18 File Offset: 0x0006EA18
		public IEnumerator<e1> eh()
		{
			return this.a();
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x0006FA20 File Offset: 0x0006EA20
		public bool ei()
		{
			return this.ek() == 0;
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x0006FA2B File Offset: 0x0006EA2B
		public bool ej(string A_0)
		{
			return !this.a.Contains(A_0) && this.c.ej(A_0);
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x0006FA4C File Offset: 0x0006EA4C
		public int ek()
		{
			int num = this.c.ek();
			foreach (string a_ in this.a)
			{
				if (this.c.ej(a_))
				{
					num--;
				}
			}
			return num;
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x0006FAB8 File Offset: 0x0006EAB8
		public IEnumerator<e1> a()
		{
			return new b2.a(this);
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x0006FAC0 File Offset: 0x0006EAC0
		public e1 el(string A_0)
		{
			if (this.a.Contains(A_0))
			{
				throw new FileNotFoundException(A_0);
			}
			e1 a_ = this.c.el(A_0);
			return this.a(a_);
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x0006FAF8 File Offset: 0x0006EAF8
		private e1 a(e1 A_0)
		{
			string key = A_0.r();
			if (this.b.ContainsKey(key) && A_0 is ig)
			{
				return new b2((ig)A_0, this.b[key]);
			}
			return A_0;
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x0006FB3B File Offset: 0x0006EB3B
		public h4 em(string A_0, Stream A_1)
		{
			return this.c.em(A_0, A_1);
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x0006FB4A File Offset: 0x0006EB4A
		public h4 en(string A_0, int A_1, dn A_2)
		{
			return this.c.en(A_0, A_1, A_2);
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x0006FB5A File Offset: 0x0006EB5A
		public ig eo(string A_0)
		{
			return this.c.eo(A_0);
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x0006FB68 File Offset: 0x0006EB68
		public ar ep()
		{
			return this.c.ep();
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x0006FB75 File Offset: 0x0006EB75
		public void eq(ar A_0)
		{
			this.c.eq(A_0);
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x0006FB83 File Offset: 0x0006EB83
		public string r()
		{
			return this.c.r();
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x0006FB90 File Offset: 0x0006EB90
		public bool aa()
		{
			return true;
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x0006FB93 File Offset: 0x0006EB93
		public bool s()
		{
			return false;
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x0006FB96 File Offset: 0x0006EB96
		public ig t()
		{
			return this.c.t();
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x0006FBA3 File Offset: 0x0006EBA3
		public bool u()
		{
			return this.c.u();
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x0006FBB0 File Offset: 0x0006EBB0
		public bool v(string A_0)
		{
			return this.c.v(A_0);
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0006FBBE File Offset: 0x0006EBBE
		public IEnumerator<e1> GetEnumerator()
		{
			return new b2.a(this);
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x0006FBC6 File Offset: 0x0006EBC6
		IEnumerator IEnumerable.b()
		{
			return new b2.a(this);
		}

		// Token: 0x0400124B RID: 4683
		private List<string> a;

		// Token: 0x0400124C RID: 4684
		private Dictionary<string, List<string>> b;

		// Token: 0x0400124D RID: 4685
		private ig c;

		// Token: 0x020002D0 RID: 720
		private class a : IEnumerator<e1>
		{
			// Token: 0x06001906 RID: 6406 RVA: 0x0006FBCE File Offset: 0x0006EBCE
			public a(b2 A_0)
			{
				this.d = A_0;
				this.c = A_0.c;
				this.a = this.c.eh();
			}

			// Token: 0x06001907 RID: 6407 RVA: 0x0006FBFA File Offset: 0x0006EBFA
			public void b()
			{
				throw new InvalidOperationException("Remove not supported");
			}

			// Token: 0x06001908 RID: 6408 RVA: 0x0006FC06 File Offset: 0x0006EC06
			public e1 get_Current()
			{
				return this.b;
			}

			// Token: 0x06001909 RID: 6409 RVA: 0x0006FC0E File Offset: 0x0006EC0E
			public void Dispose()
			{
			}

			// Token: 0x0600190A RID: 6410 RVA: 0x0006FC10 File Offset: 0x0006EC10
			object IEnumerator.a()
			{
				return this.b;
			}

			// Token: 0x0600190B RID: 6411 RVA: 0x0006FC18 File Offset: 0x0006EC18
			public bool MoveNext()
			{
				this.b = null;
				while (this.a.MoveNext())
				{
					e1 e = this.a.Current;
					if (!this.d.a.Contains(e.r()))
					{
						this.b = this.d.a(e);
						break;
					}
				}
				return this.b != null;
			}

			// Token: 0x0600190C RID: 6412 RVA: 0x0006FC7C File Offset: 0x0006EC7C
			public void Reset()
			{
				throw new NotImplementedException();
			}

			// Token: 0x0400124E RID: 4686
			private IEnumerator<e1> a;

			// Token: 0x0400124F RID: 4687
			private e1 b;

			// Token: 0x04001250 RID: 4688
			private ig c;

			// Token: 0x04001251 RID: 4689
			private b2 d;
		}
	}
}
