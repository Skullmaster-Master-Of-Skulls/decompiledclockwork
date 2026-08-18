using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using MailBee;

namespace a
{
	// Token: 0x0200049D RID: 1181
	internal class x
	{
		// Token: 0x06002859 RID: 10329 RVA: 0x000BC0A0 File Offset: 0x000BB0A0
		public x()
		{
			this.a = true;
			this.b = false;
			this.c = null;
			this.e = null;
			this.f = null;
			this.d = null;
			this.g = new ManualResetEvent(true);
			this.h = new ManualResetEvent(false);
			this.i = new ManualResetEvent(false);
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x000BC101 File Offset: 0x000BB101
		public void b(bool A_0)
		{
			this.a = A_0;
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x000BC10A File Offset: 0x000BB10A
		public ISynchronizeInvoke d()
		{
			return this.c;
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x000BC112 File Offset: 0x000BB112
		public void a(ISynchronizeInvoke A_0)
		{
			this.c = A_0;
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x000BC11B File Offset: 0x000BB11B
		public bool b()
		{
			return this.b;
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x000BC123 File Offset: 0x000BB123
		public void a(bool A_0)
		{
			this.b = A_0;
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x000BC12C File Offset: 0x000BB12C
		public ManualResetEvent a()
		{
			return this.h;
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x000BC134 File Offset: 0x000BB134
		public void a(Delegate A_0, object A_1, CommonEventArgs A_2)
		{
			this.a(A_0, new object[]
			{
				A_1,
				A_2
			}, A_2.Context.ba() != bj.c);
		}

		// Token: 0x06002861 RID: 10337 RVA: 0x000BC15C File Offset: 0x000BB15C
		private void a(Delegate A_0, object[] A_1, bool A_2)
		{
			if (A_0 != null)
			{
				Delegate[] invocationList = A_0.GetInvocationList();
				int i = 0;
				while (i < invocationList.Length)
				{
					Delegate @delegate = invocationList[i];
					ISynchronizeInvoke synchronizeInvoke = this.c;
					if (synchronizeInvoke == null)
					{
						synchronizeInvoke = (@delegate.Target as ISynchronizeInvoke);
					}
					if (synchronizeInvoke == null || !synchronizeInvoke.InvokeRequired)
					{
						try
						{
							@delegate.DynamicInvoke(A_1);
							goto IL_169;
						}
						catch (TargetInvocationException ex)
						{
							throw new MailBeeExternalException(7, ex.InnerException);
						}
						goto IL_57;
					}
					goto IL_57;
					IL_169:
					i++;
					continue;
					IL_57:
					if (this.a && A_2)
					{
						try
						{
							synchronizeInvoke.Invoke(@delegate, A_1);
							goto IL_169;
						}
						catch (ObjectDisposedException a_)
						{
							Control control = synchronizeInvoke as Control;
							if (control == null || (!control.IsDisposed && !control.Disposing))
							{
								throw new MailBeeExternalException(7, a_);
							}
							goto IL_169;
						}
						catch (Exception a_2)
						{
							throw new MailBeeExternalException(7, a_2);
						}
					}
					if (this.b)
					{
						bool flag = false;
						do
						{
							this.g.WaitOne();
							lock (this)
							{
								if (this.e == null)
								{
									this.e = @delegate;
									this.f = A_1;
									flag = true;
								}
								this.g.Reset();
							}
						}
						while (!flag);
					}
					else
					{
						this.e = @delegate;
						this.f = A_1;
					}
					this.i.Reset();
					this.h.Set();
					this.i.WaitOne();
					Exception ex2 = this.d;
					this.e = null;
					this.f = null;
					this.d = null;
					this.g.Set();
					if (ex2 != null)
					{
						throw ex2;
					}
					goto IL_169;
				}
			}
		}

		// Token: 0x06002862 RID: 10338 RVA: 0x000BC314 File Offset: 0x000BB314
		public void c()
		{
			this.h.Reset();
			if (this.e == null)
			{
				throw new InvalidOperationException("m_delToExec == null");
			}
			ISynchronizeInvoke synchronizeInvoke = this.c;
			if (synchronizeInvoke == null)
			{
				synchronizeInvoke = (this.e.Target as ISynchronizeInvoke);
			}
			if (synchronizeInvoke == null || !synchronizeInvoke.InvokeRequired)
			{
				try
				{
					this.e.DynamicInvoke(this.f);
					this.d = null;
					goto IL_CF;
				}
				catch (TargetInvocationException ex)
				{
					this.d = new MailBeeExternalException(7, ex.InnerException);
					goto IL_CF;
				}
			}
			try
			{
				synchronizeInvoke.Invoke(this.e, this.f);
				this.d = null;
			}
			catch (ObjectDisposedException a_)
			{
				Control control = synchronizeInvoke as Control;
				if (control == null || (!control.IsDisposed && !control.Disposing))
				{
					this.d = new MailBeeExternalException(7, a_);
				}
			}
			catch (Exception a_2)
			{
				this.d = new MailBeeExternalException(7, a_2);
			}
			IL_CF:
			this.i.Set();
		}

		// Token: 0x04001B92 RID: 7058
		private bool a;

		// Token: 0x04001B93 RID: 7059
		private bool b;

		// Token: 0x04001B94 RID: 7060
		private ISynchronizeInvoke c;

		// Token: 0x04001B95 RID: 7061
		private Exception d;

		// Token: 0x04001B96 RID: 7062
		private Delegate e;

		// Token: 0x04001B97 RID: 7063
		private object[] f;

		// Token: 0x04001B98 RID: 7064
		private ManualResetEvent g;

		// Token: 0x04001B99 RID: 7065
		private ManualResetEvent h;

		// Token: 0x04001B9A RID: 7066
		private ManualResetEvent i;
	}
}
