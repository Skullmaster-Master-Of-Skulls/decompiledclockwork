using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MailBee;
using MailBee.SmtpMail;

namespace a.d
{
	// Token: 0x02000479 RID: 1145
	internal class l : g
	{
		// Token: 0x0600278B RID: 10123 RVA: 0x000B79C2 File Offset: 0x000B69C2
		protected override void ce()
		{
			this.e = new byte[this.a * 8];
			this.i = new byte[this.a];
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x000B79E8 File Offset: 0x000B69E8
		protected override int ch(int A_0, out int A_1)
		{
			A_1 = 0;
			int num = 0;
			int num2 = 0;
			while ((num2 = global::a.d.r.a(this.i, num, this.j - num)) > 0)
			{
				byte[] array = new byte[num2 - num];
				Buffer.BlockCopy(this.i, num, array, 0, num2 - num);
				string a_ = null;
				int num3;
				try
				{
					string a_2 = global::a.v.a(array, this.d);
					a_ = global::a.d.r.a(a_2);
					num3 = global::a.d.r.b(a_2);
				}
				catch (l l)
				{
					throw new MailBeeInvalidTextResponseException(l.ErrorCode, l, this.hs(), l.a(), this.d);
				}
				af a_3;
				if (num3 < 400)
				{
					if (num3 < 300)
					{
						a_3 = af.a;
					}
					else
					{
						a_3 = af.b;
					}
				}
				else
				{
					a_3 = af.c;
				}
				j a_4 = new j(array, this.d, this.d.GetString(array, 0, array.Length), a_3, a_, num3);
				this.m.a(a_4);
				if (this.p != null)
				{
					this.p(a_4, this.f.c());
				}
				num = num2;
			}
			if (num <= 0)
			{
				this.k = 0;
			}
			else
			{
				this.k = num;
			}
			return num;
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x000B7B10 File Offset: 0x000B6B10
		public override void ci()
		{
			this.l.a(new m(true, false, false, false));
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x000B7B26 File Offset: 0x000B6B26
		protected override int cj()
		{
			this.m.a(new j(null, this.d, null, af.d, null, 0));
			return this.j;
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x000B7B49 File Offset: 0x000B6B49
		protected override MailBeeEmailProtocolNegativeResponseException ck(int A_0, ai A_1, at A_2)
		{
			return new MailBeeSmtpNegativeResponseException(A_0, A_1, A_2);
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x000B7B54 File Offset: 0x000B6B54
		protected override Task<int> cl(int A_0, aq<int> A_1)
		{
			l.a a;
			a.d = this;
			a.c = A_1;
			a.b = AsyncTaskMethodBuilder<int>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<int> b = a.b;
			b.Start<l.a>(ref a);
			return a.b.Task;
		}
	}
}
