using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace a.b
{
	// Token: 0x020002B6 RID: 694
	internal class ey
	{
		// Token: 0x06001833 RID: 6195 RVA: 0x0006E794 File Offset: 0x0006D794
		[CompilerGenerated]
		public void b(bq A_0)
		{
			bq bq = this.a;
			bq bq2;
			do
			{
				bq2 = bq;
				bq value = (bq)Delegate.Combine(bq2, A_0);
				bq = Interlocked.CompareExchange<bq>(ref this.a, value, bq2);
			}
			while (bq != bq2);
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x0006E7CC File Offset: 0x0006D7CC
		[CompilerGenerated]
		public void a(bq A_0)
		{
			bq bq = this.a;
			bq bq2;
			do
			{
				bq2 = bq;
				bq value = (bq)Delegate.Remove(bq2, A_0);
				bq = Interlocked.CompareExchange<bq>(ref this.a, value, bq2);
			}
			while (bq != bq2);
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x0006E801 File Offset: 0x0006D801
		protected virtual void a(c1 A_0)
		{
			if (this.a != null)
			{
				this.a(this, A_0);
			}
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x0006E818 File Offset: 0x0006D818
		public ey()
		{
			this.b = new fn();
			this.c = false;
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x0006E834 File Offset: 0x0006D834
		public List<ge> a(Stream A_0)
		{
			this.c = true;
			c3 c = new c3(A_0);
			d3 d = new d3(A_0, c.b());
			new e7(c.b(), c.f(), c.d(), c.a(), c.h(), d);
			gz gz = new gz(c, d);
			return this.a(fx.a(c.b(), d, gz.b(), c.e()), d, gz.b().om(), new db());
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x0006E8B8 File Offset: 0x0006D8B8
		public void a(gr A_0)
		{
			if (A_0 == null)
			{
				throw new NullReferenceException();
			}
			if (this.c)
			{
				throw new InvalidOperationException();
			}
			this.b.b(A_0);
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x0006E8DD File Offset: 0x0006D8DD
		public void a(gr A_0, string A_1)
		{
			this.a(A_0, null, A_1);
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x0006E8E8 File Offset: 0x0006D8E8
		public void a(gr A_0, db A_1, string A_2)
		{
			if (A_0 == null || A_2 == null || A_2.Length == 0)
			{
				throw new NullReferenceException();
			}
			if (this.c)
			{
				throw new InvalidOperationException();
			}
			this.b.a(A_0, (A_1 == null) ? new db() : A_1, A_2);
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x0006E924 File Offset: 0x0006D924
		private List<ge> a(dc A_0, dc A_1, IEnumerator A_2, db A_3)
		{
			List<ge> result = new List<ge>();
			while (A_2.MoveNext())
			{
				object obj = A_2.Current;
				ed ed = (ed)obj;
				string text = ed.f();
				if (ed.lj())
				{
					db a_ = new db(A_3, new string[]
					{
						text
					});
					this.a(A_0, A_1, ((g8)ed).om(), a_);
				}
				else
				{
					int a_2 = ed.i();
					IEnumerator enumerator = this.b.a(A_3, text);
					if (enumerator.MoveNext())
					{
						enumerator.Reset();
						int a_3 = ed.h();
						eg a_4;
						if (ed.g())
						{
							a_4 = new eg(text, A_0.fc(a_2, -1), a_3);
						}
						else
						{
							a_4 = new eg(text, A_1.fc(a_2, -1), a_3);
						}
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							((gr)obj2).a(new dw(new az(a_4), A_3, text));
						}
					}
					else if (ed.g())
					{
						A_0.fc(a_2, -1);
					}
					else
					{
						A_1.fc(a_2, -1);
					}
				}
			}
			return result;
		}

		// Token: 0x04001221 RID: 4641
		[CompilerGenerated]
		private bq a;

		// Token: 0x04001222 RID: 4642
		private fn b;

		// Token: 0x04001223 RID: 4643
		private bool c;
	}
}
