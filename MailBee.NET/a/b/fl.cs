using System;
using System.IO;
using MailBee;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x02000261 RID: 609
	internal class fl : ii
	{
		// Token: 0x06001540 RID: 5440 RVA: 0x000611A5 File Offset: 0x000601A5
		public new virtual int i()
		{
			return this.h(3616);
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x000611B2 File Offset: 0x000601B2
		public override DateTime ko()
		{
			return this.f(12295);
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x000611BF File Offset: 0x000601BF
		public new virtual DateTime s()
		{
			return this.f(12296);
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x000611CC File Offset: 0x000601CC
		public new virtual co q()
		{
			di di = null;
			if (this.h(14085) != 5)
			{
				return null;
			}
			e2 e = this.x.b(14081);
			if (e.f == 258)
			{
				if (!e.i)
				{
					di = new di(this.u, e.h);
				}
			}
			else if (e.f == 13)
			{
				int a_ = (int)ii.b(e.h, 0, 4);
				h1 h = this.y.b(a_);
				di = new di(this.u, h);
				this.y.a(this.u.d((long)h.c));
			}
			if (di == null)
			{
				return null;
			}
			try
			{
				c0 a_2 = new c0(di);
				return ii.a(this.u, this.w, a_2, this.y);
			}
			catch (MailBeePstException)
			{
			}
			return null;
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x000612BC File Offset: 0x000602BC
		public new virtual Stream k()
		{
			if (this.o() == 0)
			{
				throw new MailBeePstParsingException(Resources.Instance.ErrorDesc_OutlookPstAttachmentIsEmpty, 1210);
			}
			e2 e = this.x.b(14081);
			if (e.i)
			{
				h1 a_ = this.y.b(e.g);
				return new di(this.u, a_);
			}
			return new di(this.u, e.h, !e.b());
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x00061338 File Offset: 0x00060338
		public new virtual int o()
		{
			e2 e = this.x.b(14081);
			if (!e.i)
			{
				return e.h.Length;
			}
			h1 h = this.y.b(e.g);
			if (h == null)
			{
				throw new MailBeeOutlookMsgParsingException(string.Format(Resources.Instance.ErrorDesc_OutlookPstMissingAttachmentDescriptorItemFor0, e.g), 1210);
			}
			return h.c();
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x000613A5 File Offset: 0x000603A5
		public new virtual string a()
		{
			return this.d(14084);
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x000613B2 File Offset: 0x000603B2
		public new virtual int m()
		{
			return this.h(14085);
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x000613BF File Offset: 0x000603BF
		public new virtual int p()
		{
			return this.h(3616);
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x000613CC File Offset: 0x000603CC
		public new virtual int r()
		{
			return this.h(3617);
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x000613D9 File Offset: 0x000603D9
		public new virtual string d()
		{
			return this.d(14087);
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x000613E6 File Offset: 0x000603E6
		public new virtual string l()
		{
			return this.d(14088);
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x000613F3 File Offset: 0x000603F3
		public new virtual int h()
		{
			return this.h(14091);
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x00061400 File Offset: 0x00060400
		public new virtual string b()
		{
			return this.d(14093);
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x0006140D File Offset: 0x0006040D
		public new virtual string g()
		{
			return this.d(14094);
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x0006141A File Offset: 0x0006041A
		public new virtual int n()
		{
			return this.h(14096);
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x00061427 File Offset: 0x00060427
		internal fl(bs A_0, c0 A_1, fb A_2) : base(A_0, null, A_1, A_2)
		{
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x00061433 File Offset: 0x00060433
		public new string t()
		{
			return this.d(14098);
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x00061440 File Offset: 0x00060440
		public new bool e()
		{
			return (this.h(14100) & 1) > 0;
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x00061452 File Offset: 0x00060452
		public new bool c()
		{
			return (this.h(14100) & 2) > 0;
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x00061464 File Offset: 0x00060464
		public new bool j()
		{
			return (this.h(14100) & 4) > 0;
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x00061476 File Offset: 0x00060476
		public new string f()
		{
			return this.d(14102);
		}

		// Token: 0x04001051 RID: 4177
		public new const int a = 0;

		// Token: 0x04001052 RID: 4178
		public new const int b = 1;

		// Token: 0x04001053 RID: 4179
		public new const int c = 2;

		// Token: 0x04001054 RID: 4180
		public new const int d = 3;

		// Token: 0x04001055 RID: 4181
		public new const int e = 4;

		// Token: 0x04001056 RID: 4182
		public new const int f = 5;

		// Token: 0x04001057 RID: 4183
		public new const int g = 6;
	}
}
