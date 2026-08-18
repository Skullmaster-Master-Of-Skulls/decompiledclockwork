using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MailBee;
using MailBee.Pop3Mail;

namespace a.a
{
	// Token: 0x020003EA RID: 1002
	internal class d : g
	{
		// Token: 0x060023A1 RID: 9121 RVA: 0x0009557B File Offset: 0x0009457B
		public d()
		{
			this.e = null;
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x0009558A File Offset: 0x0009458A
		protected override void ce()
		{
			this.e = new byte[this.a];
			this.i = new byte[this.a * 8];
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x000955B0 File Offset: 0x000945B0
		protected override int ch(int A_0, out int A_1)
		{
			A_1 = 0;
			int num = 0;
			int num2 = 0;
			int num3 = A_0;
			global::a.a.a a = (global::a.a.a)this.l.c(this.m.Count);
			while ((num2 = global::a.a.i.a(this.i, num, this.k, this.j - this.k, a.a)) > 0)
			{
				byte[] array = new byte[num2 - num];
				Buffer.BlockCopy(this.i, num, array, 0, num2 - num);
				string text = null;
				string a_ = null;
				af af;
				try
				{
					text = global::a.v.a(array, this.d);
					a_ = global::a.a.i.a(text);
					af = global::a.a.i.a(text, this.d);
				}
				catch (l l)
				{
					j a_2 = new j(array, this.d, text, af.f, a_, a.a, a.b);
					if (this.p != null)
					{
						this.p(a_2, this.f.c());
					}
					throw new MailBeeInvalidTextResponseException(l.ErrorCode, l, this.hs(), l.a(), this.d);
				}
				bool a_3 = af != af.c && a.a;
				if (this.e != null)
				{
					int num4 = num3 - (this.j - num2);
					this.e(this.i, num, num2 - num4, num4, this.f.c());
					num3 -= num4;
				}
				j a_4 = new j(array, this.d, text, af, a_, a_3, a.b);
				this.m.a(a_4);
				if (this.p != null)
				{
					this.p(a_4, this.f.c());
				}
				num = num2;
				this.k = num2;
				if (this.m.Count == this.l.Count)
				{
					break;
				}
				a = (global::a.a.a)this.l.c(this.m.Count);
			}
			if (num2 <= 0)
			{
				if (this.e != null)
				{
					this.e(this.i, num, this.j - num3, num3, this.f.c());
				}
				this.k = -num2;
			}
			return num;
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x000957E4 File Offset: 0x000947E4
		public override void ci()
		{
			this.l.a(new global::a.a.a(true));
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x000957F7 File Offset: 0x000947F7
		protected override int cj()
		{
			this.m.a(new j(null, this.d, null, af.d, null, false, false));
			return this.j;
		}

		// Token: 0x060023A6 RID: 9126 RVA: 0x0009581B File Offset: 0x0009481B
		protected override MailBeeEmailProtocolNegativeResponseException ck(int A_0, ai A_1, at A_2)
		{
			return new MailBeePop3NegativeResponseException(A_0, A_1, A_2);
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x00095828 File Offset: 0x00094828
		public new ao g(j A_0)
		{
			ao result;
			try
			{
				result = global::a.a.i.a(new ao(A_0.q()), this.d);
			}
			catch (l l)
			{
				throw new MailBeeInvalidTextResponseException(l.ErrorCode, l, this.hs(), l.a(), this.d);
			}
			return result;
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x00095880 File Offset: 0x00094880
		public new ao g(int A_0)
		{
			ao result;
			try
			{
				result = global::a.a.i.a(new ao(this.m.a(A_0).q()), this.d);
			}
			catch (l l)
			{
				throw new MailBeeInvalidTextResponseException(l.ErrorCode, l, this.hs(), l.a(), this.d);
			}
			return result;
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x000958E4 File Offset: 0x000948E4
		public new string i(int A_0)
		{
			return this.g(A_0).a(this.d);
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x000958F8 File Offset: 0x000948F8
		public new string[] h(int A_0)
		{
			return global::a.bb.e(this.i(A_0));
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x00095906 File Offset: 0x00094906
		public new k g()
		{
			return this.e;
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x0009590E File Offset: 0x0009490E
		public new void g(k A_0)
		{
			this.e = A_0;
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x00095918 File Offset: 0x00094918
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

		// Token: 0x04001788 RID: 6024
		private new k e;
	}
}
