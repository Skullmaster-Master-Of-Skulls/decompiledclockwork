using System;
using System.Configuration.Provider;
using System.Threading;

namespace System.Web.Management
{
	// Token: 0x02000188 RID: 392
	public abstract class WebEventProvider : ProviderBase
	{
		// Token: 0x0600151E RID: 5406
		public abstract void ProcessEvent(WebBaseEvent raisedEvent);

		// Token: 0x0600151F RID: 5407
		public abstract void Shutdown();

		// Token: 0x06001520 RID: 5408
		public abstract void Flush();

		// Token: 0x06001521 RID: 5409 RVA: 0x00040BE2 File Offset: 0x0003EDE2
		internal void LogException(Exception e)
		{
			if (Interlocked.CompareExchange(ref this._exceptionLogged, 1, 0) == 0)
			{
				UnsafeNativeMethods.LogWebeventProviderFailure(HttpRuntime.AppDomainAppVirtualPath, this.Name, e.ToString());
			}
		}

		// Token: 0x04001629 RID: 5673
		private int _exceptionLogged;
	}
}
