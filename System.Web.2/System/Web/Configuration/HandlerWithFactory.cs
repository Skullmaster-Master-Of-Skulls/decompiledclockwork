using System;

namespace System.Web.Configuration
{
	// Token: 0x020006ED RID: 1773
	internal class HandlerWithFactory
	{
		// Token: 0x06005526 RID: 21798 RVA: 0x00129AC7 File Offset: 0x00127CC7
		internal HandlerWithFactory(IHttpHandler handler, IHttpHandlerFactory factory)
		{
			this._handler = handler;
			this._factory = factory;
		}

		// Token: 0x06005527 RID: 21799 RVA: 0x00129ADD File Offset: 0x00127CDD
		internal void Recycle()
		{
			this._factory.ReleaseHandler(this._handler);
		}

		// Token: 0x04002C9B RID: 11419
		private IHttpHandler _handler;

		// Token: 0x04002C9C RID: 11420
		private IHttpHandlerFactory _factory;
	}
}
