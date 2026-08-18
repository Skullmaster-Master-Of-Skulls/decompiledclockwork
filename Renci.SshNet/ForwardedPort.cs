using System;
using Renci.SshNet.Common;

namespace Renci.SshNet
{
	// Token: 0x02000024 RID: 36
	public abstract class ForwardedPort : IForwardedPort
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00006F23 File Offset: 0x00005123
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00006F2B File Offset: 0x0000512B
		internal ISession Session { get; set; }

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x060001CD RID: 461 RVA: 0x00006F34 File Offset: 0x00005134
		// (remove) Token: 0x060001CE RID: 462 RVA: 0x00006F6C File Offset: 0x0000516C
		internal event EventHandler Closing;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x060001CF RID: 463 RVA: 0x00006FA1 File Offset: 0x000051A1
		// (remove) Token: 0x060001D0 RID: 464 RVA: 0x00006FAA File Offset: 0x000051AA
		event EventHandler IForwardedPort.Closing
		{
			add
			{
				this.Closing += value;
			}
			remove
			{
				this.Closing -= value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001D1 RID: 465
		public abstract bool IsStarted { get; }

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x060001D2 RID: 466 RVA: 0x00006FB4 File Offset: 0x000051B4
		// (remove) Token: 0x060001D3 RID: 467 RVA: 0x00006FEC File Offset: 0x000051EC
		public event EventHandler<ExceptionEventArgs> Exception;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x060001D4 RID: 468 RVA: 0x00007024 File Offset: 0x00005224
		// (remove) Token: 0x060001D5 RID: 469 RVA: 0x0000705C File Offset: 0x0000525C
		public event EventHandler<PortForwardEventArgs> RequestReceived;

		// Token: 0x060001D6 RID: 470 RVA: 0x00007094 File Offset: 0x00005294
		public virtual void Start()
		{
			this.CheckDisposed();
			if (this.IsStarted)
			{
				throw new InvalidOperationException("Forwarded port is already started.");
			}
			if (this.Session == null)
			{
				throw new InvalidOperationException("Forwarded port is not added to a client.");
			}
			if (!this.Session.IsConnected)
			{
				throw new SshConnectionException("Client not connected.");
			}
			this.Session.ErrorOccured += this.Session_ErrorOccured;
			this.StartPort();
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00007102 File Offset: 0x00005302
		public virtual void Stop()
		{
			this.CheckDisposed();
			if (this.IsStarted)
			{
				this.StopPort(this.Session.ConnectionInfo.Timeout);
			}
		}

		// Token: 0x060001D8 RID: 472
		protected abstract void StartPort();

		// Token: 0x060001D9 RID: 473 RVA: 0x00007128 File Offset: 0x00005328
		protected virtual void StopPort(TimeSpan timeout)
		{
			this.RaiseClosing();
			ISession session = this.Session;
			if (session != null)
			{
				session.ErrorOccured -= this.Session_ErrorOccured;
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00007158 File Offset: 0x00005358
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				ISession session = this.Session;
				if (session != null)
				{
					this.StopPort(session.ConnectionInfo.Timeout);
					this.Session = null;
				}
			}
		}

		// Token: 0x060001DB RID: 475
		protected abstract void CheckDisposed();

		// Token: 0x060001DC RID: 476 RVA: 0x0000718C File Offset: 0x0000538C
		protected void RaiseExceptionEvent(Exception exception)
		{
			EventHandler<ExceptionEventArgs> exception2 = this.Exception;
			if (exception2 != null)
			{
				exception2(this, new ExceptionEventArgs(exception));
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000071B0 File Offset: 0x000053B0
		protected void RaiseRequestReceived(string host, uint port)
		{
			EventHandler<PortForwardEventArgs> requestReceived = this.RequestReceived;
			if (requestReceived != null)
			{
				requestReceived(this, new PortForwardEventArgs(host, port));
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000071D8 File Offset: 0x000053D8
		private void RaiseClosing()
		{
			EventHandler closing = this.Closing;
			if (closing != null)
			{
				closing(this, EventArgs.Empty);
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00004F48 File Offset: 0x00003148
		private void Session_ErrorOccured(object sender, ExceptionEventArgs e)
		{
			this.RaiseExceptionEvent(e.Exception);
		}
	}
}
