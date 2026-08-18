using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	// Token: 0x0200000E RID: 14
	[__DynamicallyInvokable]
	public abstract class DelegatingHandler : HttpMessageHandler
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000045FB File Offset: 0x000027FB
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00004603 File Offset: 0x00002803
		[__DynamicallyInvokable]
		public HttpMessageHandler InnerHandler
		{
			[__DynamicallyInvokable]
			get
			{
				return this.innerHandler;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.CheckDisposedOrStarted();
				if (Logging.On)
				{
					Logging.Associate(Logging.Http, this, value);
				}
				this.innerHandler = value;
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004633 File Offset: 0x00002833
		[__DynamicallyInvokable]
		protected DelegatingHandler()
		{
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000463B File Offset: 0x0000283B
		[__DynamicallyInvokable]
		protected DelegatingHandler(HttpMessageHandler innerHandler)
		{
			this.InnerHandler = innerHandler;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000464A File Offset: 0x0000284A
		[__DynamicallyInvokable]
		protected internal override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request", SR.net_http_handler_norequest);
			}
			this.SetOperationStarted();
			return this.innerHandler.SendAsync(request, cancellationToken);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004672 File Offset: 0x00002872
		[__DynamicallyInvokable]
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this.disposed)
			{
				this.disposed = true;
				if (this.innerHandler != null)
				{
					this.innerHandler.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000046A4 File Offset: 0x000028A4
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000046C1 File Offset: 0x000028C1
		private void CheckDisposedOrStarted()
		{
			this.CheckDisposed();
			if (this.operationStarted)
			{
				throw new InvalidOperationException(SR.net_http_operation_started);
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000046DE File Offset: 0x000028DE
		private void SetOperationStarted()
		{
			this.CheckDisposed();
			if (this.innerHandler == null)
			{
				throw new InvalidOperationException(SR.net_http_handler_not_assigned);
			}
			if (!this.operationStarted)
			{
				this.operationStarted = true;
			}
		}

		// Token: 0x04000088 RID: 136
		private HttpMessageHandler innerHandler;

		// Token: 0x04000089 RID: 137
		private volatile bool operationStarted;

		// Token: 0x0400008A RID: 138
		private volatile bool disposed;
	}
}
