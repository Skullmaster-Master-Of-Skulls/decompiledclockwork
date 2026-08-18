using System;
using System.Diagnostics;
using System.ServiceModel.Diagnostics.Application;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200076C RID: 1900
	internal class RequestContextMessageProperty : IDisposable
	{
		// Token: 0x06004896 RID: 18582 RVA: 0x0010C248 File Offset: 0x0010A448
		public RequestContextMessageProperty(RequestContext context)
		{
			this.context = context;
		}

		// Token: 0x1700123B RID: 4667
		// (get) Token: 0x06004897 RID: 18583 RVA: 0x0010C262 File Offset: 0x0010A462
		public static string Name
		{
			get
			{
				return "requestContext";
			}
		}

		// Token: 0x06004898 RID: 18584 RVA: 0x0010C26C File Offset: 0x0010A46C
		void IDisposable.Dispose()
		{
			bool flag = false;
			object obj = this.thisLock;
			RequestContext requestContext;
			lock (obj)
			{
				if (this.context == null)
				{
					return;
				}
				requestContext = this.context;
				this.context = null;
			}
			try
			{
				requestContext.Close();
				flag = true;
			}
			catch (CommunicationException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
			}
			catch (TimeoutException ex)
			{
				if (TD.CloseTimeoutIsEnabled())
				{
					TD.CloseTimeout(ex.Message);
				}
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
			}
			finally
			{
				if (!flag)
				{
					requestContext.Abort();
				}
			}
		}

		// Token: 0x04002DF3 RID: 11763
		private RequestContext context;

		// Token: 0x04002DF4 RID: 11764
		private object thisLock = new object();
	}
}
