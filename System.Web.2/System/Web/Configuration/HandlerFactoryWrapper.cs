using System;

namespace System.Web.Configuration
{
	// Token: 0x020006EB RID: 1771
	internal class HandlerFactoryWrapper : IHttpHandlerFactory
	{
		// Token: 0x06005520 RID: 21792 RVA: 0x00129A2A File Offset: 0x00127C2A
		internal HandlerFactoryWrapper(IHttpHandler handler, Type handlerType)
		{
			this._handler = handler;
			this._handlerType = handlerType;
		}

		// Token: 0x06005521 RID: 21793 RVA: 0x00129A40 File Offset: 0x00127C40
		public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
		{
			if (this._handler == null)
			{
				this._handler = (IHttpHandler)HttpRuntime.CreateNonPublicInstanceByWebObjectActivator(this._handlerType);
			}
			return this._handler;
		}

		// Token: 0x06005522 RID: 21794 RVA: 0x00129A66 File Offset: 0x00127C66
		public void ReleaseHandler(IHttpHandler handler)
		{
			if (this._handler != null && !this._handler.IsReusable)
			{
				this._handler = null;
			}
		}

		// Token: 0x04002C96 RID: 11414
		private IHttpHandler _handler;

		// Token: 0x04002C97 RID: 11415
		private Type _handlerType;
	}
}
