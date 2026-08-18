using System;
using System.Web.Hosting;

namespace System.Web
{
	// Token: 0x02000090 RID: 144
	internal class DisposableHttpContextWrapper : IDisposable
	{
		// Token: 0x06000980 RID: 2432 RVA: 0x00015C0D File Offset: 0x00013E0D
		internal static HttpContext SwitchContext(HttpContext context)
		{
			return ContextBase.SwitchContext(context) as HttpContext;
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00015C1A File Offset: 0x00013E1A
		internal DisposableHttpContextWrapper(HttpContext context)
		{
			if (context != null)
			{
				this._savedContext = DisposableHttpContextWrapper.SwitchContext(context);
				this._needToUndo = (this._savedContext != context);
			}
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00015C43 File Offset: 0x00013E43
		void IDisposable.Dispose()
		{
			if (this._needToUndo)
			{
				DisposableHttpContextWrapper.SwitchContext(this._savedContext);
				this._savedContext = null;
				this._needToUndo = false;
			}
		}

		// Token: 0x04000383 RID: 899
		private bool _needToUndo;

		// Token: 0x04000384 RID: 900
		private HttpContext _savedContext;
	}
}
