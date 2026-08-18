using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MailBee;

namespace a
{
	// Token: 0x02000224 RID: 548
	internal abstract class bo : a0
	{
		// Token: 0x06001263 RID: 4707 RVA: 0x00051D50 File Offset: 0x00050D50
		public bo()
		{
			this.a = new x();
			this.m = new Logger(this);
			this.e = true;
			this.g = true;
			this.f9();
			this.lt(Global.DefaultEncoding);
			this.lu(Global.DefaultEncoding);
			this.c = false;
			this.i = true;
			this.q = null;
			this.k = false;
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00051DC0 File Offset: 0x00050DC0
		public void bo()
		{
			this.e = false;
			this.c = true;
			if (this.p != null)
			{
				this.p.he();
			}
			this.m.a();
		}

		// Token: 0x06001265 RID: 4709
		public abstract bool j();

		// Token: 0x06001266 RID: 4710
		public abstract void k(ErrorEventArgs A_0);

		// Token: 0x06001267 RID: 4711
		public abstract bool l();

		// Token: 0x06001268 RID: 4712
		public abstract void m(LogNewEntryEventArgs A_0);

		// Token: 0x06001269 RID: 4713
		protected abstract void f9();

		// Token: 0x0600126A RID: 4714 RVA: 0x00051DF0 File Offset: 0x00050DF0
		protected void b(MailBeeException A_0)
		{
			if (this.p != null)
			{
				bool disableOnException = this.m.DisableOnException;
				try
				{
					this.m.DisableOnException = true;
					this.p.d(A_0);
				}
				finally
				{
					this.m.DisableOnException = disableOnException;
				}
			}
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00051E48 File Offset: 0x00050E48
		public bool bn()
		{
			return !this.c;
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00051E53 File Offset: 0x00050E53
		public bool bc()
		{
			return this.p != null && this.p.be();
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00051E6A File Offset: 0x00050E6A
		protected void bl()
		{
			this.p.a(this.g ? global::a.bj.a : global::a.bj.c);
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00051E83 File Offset: 0x00050E83
		protected void bj()
		{
			this.p.a(this.g ? global::a.bj.b : global::a.bj.c);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00051E9C File Offset: 0x00050E9C
		public void bd()
		{
			this.c = true;
			bc bc = this.p;
			if (bc != null)
			{
				bc.fx();
			}
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x00051EC0 File Offset: 0x00050EC0
		public virtual void cb()
		{
			if (this.p.be())
			{
				throw new MailBeeInvalidStateException(3);
			}
			this.c = false;
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x00051EDD File Offset: 0x00050EDD
		public void bh()
		{
			if (!this.g)
			{
				this.bg();
			}
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x00051EED File Offset: 0x00050EED
		public void bg()
		{
			this.g(-1);
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x00051EF8 File Offset: 0x00050EF8
		public bool g(int A_0)
		{
			if (A_0 < -1)
			{
				throw new MailBeeInvalidArgumentException(23);
			}
			if (this.p == null || !this.p.be())
			{
				return true;
			}
			if (this.g)
			{
				throw new MailBeeInvalidStateException(10);
			}
			WaitHandle[] array = new WaitHandle[(this.q == null) ? 1 : 2];
			array[0] = this.a.a();
			if (this.q != null)
			{
				array[1] = this.q.get_AsyncWaitHandle();
			}
			DateTime d = DateTime.MinValue;
			if (A_0 != -1 && A_0 != 0)
			{
				d = DateTime.Now.AddTicks((long)(A_0 * 10000));
			}
			for (;;)
			{
				int num;
				if (A_0 == -1)
				{
					num = WaitHandle.WaitAny(array);
				}
				else
				{
					TimeSpan timeSpan = TimeSpan.Zero;
					if (A_0 != 0)
					{
						timeSpan = d - DateTime.Now;
					}
					if (!(timeSpan >= TimeSpan.Zero))
					{
						break;
					}
					num = WaitHandle.WaitAny(array, timeSpan, false);
				}
				long num2 = (long)num;
				if (num2 != 0L)
				{
					if (num2 == 1L)
					{
						return true;
					}
					if (num2 == 258L)
					{
						return false;
					}
				}
				else
				{
					this.a.c();
				}
			}
			return false;
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x00051FFB File Offset: 0x00050FFB
		public bool bf()
		{
			return this.c;
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x00052003 File Offset: 0x00051003
		public virtual string l1()
		{
			return this.p.bh();
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00052010 File Offset: 0x00051010
		public virtual int l2()
		{
			return this.p.bf();
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x0005201D File Offset: 0x0005101D
		public Logger bi()
		{
			return this.m;
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x00052025 File Offset: 0x00051025
		public bool bq()
		{
			return this.e;
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x0005202D File Offset: 0x0005102D
		public void k(bool A_0)
		{
			if (this.e != A_0)
			{
				this.e = A_0;
			}
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x0005203F File Offset: 0x0005103F
		public bool bb()
		{
			return this.g;
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x00052048 File Offset: 0x00051048
		public void j(bool A_0)
		{
			if (this.p != null && this.p.be())
			{
				throw new MailBeeInvalidStateException(3);
			}
			if (this.g != A_0)
			{
				this.g = A_0;
				if (this.p != null)
				{
					this.p.bc();
				}
				this.a.b(A_0);
			}
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x000520A0 File Offset: 0x000510A0
		public virtual Encoding bk()
		{
			return this.n;
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x000520A8 File Offset: 0x000510A8
		public virtual void lt(Encoding A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			this.n = A_0;
			this.p.hc(A_0);
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x000520C8 File Offset: 0x000510C8
		public virtual Encoding bm()
		{
			return this.o;
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x000520D0 File Offset: 0x000510D0
		public virtual void lu(Encoding A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			this.o = A_0;
			this.p.hd(A_0);
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x000520F0 File Offset: 0x000510F0
		public virtual bool be()
		{
			return this.i;
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x000520F8 File Offset: 0x000510F8
		public virtual void ls(bool A_0)
		{
			this.i = A_0;
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x00052101 File Offset: 0x00051101
		public x bp()
		{
			return this.a;
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x0005210C File Offset: 0x0005110C
		protected Task c(MailBeeException A_0)
		{
			bo.a a;
			a.c = this;
			a.d = A_0;
			a.b = AsyncTaskMethodBuilder.Create();
			a.a = -1;
			AsyncTaskMethodBuilder b = a.b;
			b.Start<bo.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x04000F2F RID: 3887
		protected x a;

		// Token: 0x04000F30 RID: 3888
		protected bool c;

		// Token: 0x04000F31 RID: 3889
		protected bool e;

		// Token: 0x04000F32 RID: 3890
		private bool g;

		// Token: 0x04000F33 RID: 3891
		protected bool i;

		// Token: 0x04000F34 RID: 3892
		protected bool k;

		// Token: 0x04000F35 RID: 3893
		protected Logger m;

		// Token: 0x04000F36 RID: 3894
		protected Encoding n;

		// Token: 0x04000F37 RID: 3895
		protected Encoding o;

		// Token: 0x04000F38 RID: 3896
		protected bc p;

		// Token: 0x04000F39 RID: 3897
		protected o q;
	}
}
