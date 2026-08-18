using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	// Token: 0x02000010 RID: 16
	[__DynamicallyInvokable]
	public abstract class HttpMessageHandler : IDisposable
	{
		// Token: 0x060000BC RID: 188 RVA: 0x00004755 File Offset: 0x00002955
		[__DynamicallyInvokable]
		protected HttpMessageHandler()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Http, this, ".ctor", null);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Http, this, ".ctor", null);
			}
		}

		// Token: 0x060000BD RID: 189
		[__DynamicallyInvokable]
		protected internal abstract Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);

		// Token: 0x060000BE RID: 190 RVA: 0x0000478D File Offset: 0x0000298D
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000478F File Offset: 0x0000298F
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
