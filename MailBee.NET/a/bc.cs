using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MailBee;

namespace a
{
	// Token: 0x02000219 RID: 537
	internal abstract class bc
	{
		// Token: 0x060011B8 RID: 4536 RVA: 0x0004FCD4 File Offset: 0x0004ECD4
		public bc(bo A_0, bc A_1, Logger A_2, int A_3)
		{
			this.a = false;
			this.b = A_0;
			this.c = A_1;
			this.f = A_3;
			this.d = A_2;
			this.e = null;
			this.n = null;
			this.o = null;
			this.g = null;
			this.bc();
			this.ff();
			this.hc(Global.DefaultEncoding);
			this.hd(Global.DefaultEncoding);
			this.pd(Global.DefaultTimeout);
			if (this.b != null)
			{
				this.n = (bc.d)Delegate.Combine(this.n, new bc.d(this.a));
				this.o = (bc.b)Delegate.Combine(this.o, new bc.b(this.a));
			}
			this.pa();
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0004FDA8 File Offset: 0x0004EDA8
		public void bc()
		{
			if (this.b != null && !this.b.bb())
			{
				this.h = global::a.bj.c;
				return;
			}
			if (this.c == null)
			{
				this.h = global::a.bj.b;
				return;
			}
			if (this.c.ba() == global::a.bj.a)
			{
				this.h = global::a.bj.a;
				return;
			}
			this.h = global::a.bj.c;
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0004FDFE File Offset: 0x0004EDFE
		public virtual void ha()
		{
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0004FE00 File Offset: 0x0004EE00
		public virtual void he()
		{
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0004FE02 File Offset: 0x0004EE02
		protected virtual void ff()
		{
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0004FE04 File Offset: 0x0004EE04
		public virtual void pa()
		{
			this.i = 0;
			this.j = string.Empty;
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0004FE18 File Offset: 0x0004EE18
		protected virtual void pb(int A_0)
		{
			this.i = A_0;
			this.j = a5.a(A_0);
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0004FE2D File Offset: 0x0004EE2D
		protected virtual void pc(MailBeeException A_0)
		{
			this.i = A_0.ErrorCode;
			this.j = A_0.Message;
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0004FE47 File Offset: 0x0004EE47
		public void d(MailBeeException A_0)
		{
			this.pc(A_0);
			this.b(A_0, true);
			this.fw(A_0);
		}

		// Token: 0x060011C1 RID: 4545
		protected abstract void fw(MailBeeException A_0);

		// Token: 0x060011C2 RID: 4546 RVA: 0x0004FE5F File Offset: 0x0004EE5F
		public virtual void fx()
		{
			bc bc = this.c;
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x0004FE68 File Offset: 0x0004EE68
		public bool be()
		{
			return this.a;
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x0004FE70 File Offset: 0x0004EE70
		public void k(bool A_0)
		{
			if (A_0 && this.a)
			{
				throw new MailBeeInvalidStateException(3);
			}
			this.a = A_0;
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x0004FE8B File Offset: 0x0004EE8B
		public Logger a8()
		{
			return this.d;
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0004FE93 File Offset: 0x0004EE93
		public void a(Logger A_0)
		{
			this.d = A_0;
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x0004FE9C File Offset: 0x0004EE9C
		public object bi()
		{
			return this.e;
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x0004FEA4 File Offset: 0x0004EEA4
		public void b(object A_0)
		{
			this.e = A_0;
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x0004FEAD File Offset: 0x0004EEAD
		public int bb()
		{
			return this.f;
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x0004FEB5 File Offset: 0x0004EEB5
		public void i(int A_0)
		{
			this.f = A_0;
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x0004FEBE File Offset: 0x0004EEBE
		public bc bj()
		{
			return this.c;
		}

		// Token: 0x060011CC RID: 4556
		public abstract string er();

		// Token: 0x060011CD RID: 4557 RVA: 0x0004FEC6 File Offset: 0x0004EEC6
		public string bh()
		{
			return this.j;
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0004FECE File Offset: 0x0004EECE
		public int bf()
		{
			return this.i;
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0004FED6 File Offset: 0x0004EED6
		public virtual Encoding bd()
		{
			return this.k;
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0004FEDE File Offset: 0x0004EEDE
		public virtual void hc(Encoding A_0)
		{
			this.k = A_0;
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0004FEE7 File Offset: 0x0004EEE7
		public virtual Encoding bg()
		{
			return this.l;
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0004FEEF File Offset: 0x0004EEEF
		public virtual void hd(Encoding A_0)
		{
			this.l = A_0;
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0004FEF8 File Offset: 0x0004EEF8
		public virtual int a9()
		{
			return this.m;
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0004FF00 File Offset: 0x0004EF00
		public virtual void pd(int A_0)
		{
			this.m = A_0;
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0004FF09 File Offset: 0x0004EF09
		public bj ba()
		{
			return this.h;
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0004FF11 File Offset: 0x0004EF11
		public void a(bj A_0)
		{
			this.h = A_0;
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0004FF1C File Offset: 0x0004EF1C
		public void b(MailBeeException A_0, bool A_1)
		{
			this.a8().b(string.Format(A_1 ? Resources.Instance.Log_Error0 : Resources.Instance.Log_Warning0, A_0.Message), null, LogMessageType.Info, this);
			if (this.n != null)
			{
				try
				{
					this.a(this.n, new object[]
					{
						A_0,
						A_1,
						this
					});
				}
				catch (MailBeeExternalException)
				{
				}
			}
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0004FF9C File Offset: 0x0004EF9C
		public void c(MailBeeException A_0)
		{
			this.b(A_0, false);
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x0004FFA8 File Offset: 0x0004EFA8
		public void a(MailBeeException A_0, bool A_1, bc A_2)
		{
			if (this.b.bq() && this.b.j() && !this.b.bf())
			{
				ErrorEventArgs a_ = new ErrorEventArgs(A_0, A_1, A_2);
				this.b.k(a_);
			}
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0004FFF1 File Offset: 0x0004EFF1
		public void a(LogEntry A_0)
		{
			if (this.o != null)
			{
				this.a(this.o, new object[]
				{
					A_0,
					this
				});
			}
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x00050018 File Offset: 0x0004F018
		public void a(LogEntry A_0, bc A_1)
		{
			if (this.b.bq() && this.b.l() && !this.b.bf())
			{
				LogNewEntryEventArgs a_ = new LogNewEntryEventArgs(A_0, A_1);
				this.b.m(a_);
			}
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x00050060 File Offset: 0x0004F060
		protected int a(int A_0, int A_1, ae A_2)
		{
			int num = 0;
			if (A_0 < 0 || A_0 > 60)
			{
				A_0 = 60;
			}
			if (A_0 > 1 || A_2.a > 1)
			{
				lock (A_2)
				{
					if (A_1 > 0)
					{
						if (A_2.a + A_1 > A_0)
						{
							num = A_0 - A_2.a;
						}
						else
						{
							num = A_1;
						}
						if (num > 0)
						{
							A_2.a += num;
						}
						else
						{
							num = 0;
						}
					}
					else if (A_1 < 0)
					{
						A_2.a += A_1;
					}
				}
			}
			return num;
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x000500F8 File Offset: 0x0004F0F8
		protected void a(int A_0, int A_1, ae A_2, out int A_3, out WaitHandle[] A_4, out WaitHandle[] A_5)
		{
			if (A_0 > 1)
			{
				A_3 = this.a(A_1, A_0, A_2);
			}
			else
			{
				A_3 = 0;
			}
			if (A_3 <= 0)
			{
				A_4 = null;
				A_5 = null;
				return;
			}
			A_4 = new WaitHandle[A_3];
			if (this.ba() == global::a.bj.b && this.b != null)
			{
				A_5 = new WaitHandle[2];
				A_5[0] = new ManualResetEvent(false);
				A_5[1] = this.b.bp().a();
				return;
			}
			A_5 = new WaitHandle[1];
			A_5[0] = new ManualResetEvent(false);
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x00050184 File Offset: 0x0004F184
		protected void a(aw A_0, Thread[] A_1, WaitHandle[] A_2, WaitHandle[] A_3, int A_4)
		{
			A_3[0] = A_2[A_4];
			for (;;)
			{
				int num;
				if (A_1 != null)
				{
					do
					{
						num = WaitHandle.WaitAny(A_3, 10000, false);
					}
					while (num == 258 && A_1[A_4].IsAlive);
					if (num == 258 && !A_1[A_4].IsAlive)
					{
						break;
					}
				}
				else
				{
					num = WaitHandle.WaitAny(A_3);
				}
				if (num == 258)
				{
					goto Block_4;
				}
				if (num >= 128)
				{
					num -= 128;
				}
				if (num > 0)
				{
					this.b.bp().c();
				}
				if (num == 0)
				{
					return;
				}
			}
			return;
			Block_4:
			throw new MailBeeInternalException(6);
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x00050214 File Offset: 0x0004F214
		protected object a(Delegate A_0, object[] A_1)
		{
			object result;
			try
			{
				result = A_0.DynamicInvoke(A_1);
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
			return result;
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x00050244 File Offset: 0x0004F244
		public virtual Task hf()
		{
			return Task.FromResult<int>(0);
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0005024C File Offset: 0x0004F24C
		public Task a(MailBeeException A_0, bool A_1)
		{
			bc.c c;
			c.c = this;
			c.e = A_0;
			c.d = A_1;
			c.b = AsyncTaskMethodBuilder.Create();
			c.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = c.b;
			asyncTaskMethodBuilder.Start<bc.c>(ref c);
			return c.b.Task;
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x000502A1 File Offset: 0x0004F2A1
		public Task b(MailBeeException A_0)
		{
			return this.a(A_0, false);
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x000502AC File Offset: 0x0004F2AC
		public Task e(MailBeeException A_0)
		{
			bc.a a;
			a.c = this;
			a.d = A_0;
			a.b = AsyncTaskMethodBuilder.Create();
			a.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = a.b;
			asyncTaskMethodBuilder.Start<bc.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x060011E4 RID: 4580
		protected abstract Task f1(MailBeeException A_0);

		// Token: 0x04000F13 RID: 3859
		protected bool a;

		// Token: 0x04000F14 RID: 3860
		protected bo b;

		// Token: 0x04000F15 RID: 3861
		protected bc c;

		// Token: 0x04000F16 RID: 3862
		protected Logger d;

		// Token: 0x04000F17 RID: 3863
		protected object e;

		// Token: 0x04000F18 RID: 3864
		protected int f;

		// Token: 0x04000F19 RID: 3865
		protected o g;

		// Token: 0x04000F1A RID: 3866
		private bj h;

		// Token: 0x04000F1B RID: 3867
		protected int i;

		// Token: 0x04000F1C RID: 3868
		protected string j;

		// Token: 0x04000F1D RID: 3869
		protected Encoding k;

		// Token: 0x04000F1E RID: 3870
		protected Encoding l;

		// Token: 0x04000F1F RID: 3871
		protected int m;

		// Token: 0x04000F20 RID: 3872
		private bc.d n;

		// Token: 0x04000F21 RID: 3873
		private bc.b o;

		// Token: 0x02000496 RID: 1174
		// (Invoke) Token: 0x06002842 RID: 10306
		protected delegate void d(MailBeeException A_0, bool A_1, bc A_2);

		// Token: 0x02000497 RID: 1175
		// (Invoke) Token: 0x06002846 RID: 10310
		protected delegate void b(LogEntry A_0, bc A_1);
	}
}
