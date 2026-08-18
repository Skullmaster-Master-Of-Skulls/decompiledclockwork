using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	// Token: 0x02000011 RID: 17
	[__DynamicallyInvokable]
	public class HttpMessageInvoker : IDisposable
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x0000479E File Offset: 0x0000299E
		[__DynamicallyInvokable]
		public HttpMessageInvoker(HttpMessageHandler handler) : this(handler, true)
		{
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000047A8 File Offset: 0x000029A8
		[__DynamicallyInvokable]
		public HttpMessageInvoker(HttpMessageHandler handler, bool disposeHandler)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Http, this, ".ctor", handler);
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (Logging.On)
			{
				Logging.Associate(Logging.Http, this, handler);
			}
			this.handler = handler;
			this.disposeHandler = disposeHandler;
			if (Logging.On)
			{
				Logging.Exit(Logging.Http, this, ".ctor", null);
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000481C File Offset: 0x00002A1C
		[__DynamicallyInvokable]
		public virtual Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			this.CheckDisposed();
			if (Logging.On)
			{
				Logging.Enter(Logging.Http, this, "SendAsync", Logging.GetObjectLogHash(request) + ": " + ((request != null) ? request.ToString() : null));
			}
			Task<HttpResponseMessage> task = this.handler.SendAsync(request, cancellationToken);
			if (Logging.On)
			{
				Logging.Exit(Logging.Http, this, "SendAsync", task);
			}
			return task;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004898 File Offset: 0x00002A98
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000048A7 File Offset: 0x00002AA7
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this.disposed)
			{
				this.disposed = true;
				if (this.disposeHandler)
				{
					this.handler.Dispose();
				}
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000048D2 File Offset: 0x00002AD2
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x0400008C RID: 140
		private volatile bool disposed;

		// Token: 0x0400008D RID: 141
		private bool disposeHandler;

		// Token: 0x0400008E RID: 142
		private HttpMessageHandler handler;
	}
}
