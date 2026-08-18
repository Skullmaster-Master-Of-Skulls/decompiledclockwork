using System;

namespace System.Data.Entity.Infrastructure.Design
{
	// Token: 0x0200018F RID: 399
	internal class WrappedHandler : IResultHandler
	{
		// Token: 0x06000D84 RID: 3460 RVA: 0x0003D004 File Offset: 0x0003B204
		public WrappedHandler(object handler)
		{
			HandlerBase handlerBase = (handler as HandlerBase) ?? new ForwardingProxy<HandlerBase>(handler).GetTransparentProxy();
			this._resultHandler = ((handler as IResultHandler) ?? (handlerBase.ImplementsContract(typeof(IResultHandler).FullName) ? new ForwardingProxy<IResultHandler>(handler).GetTransparentProxy() : null));
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0003D062 File Offset: 0x0003B262
		public void SetResult(object value)
		{
			if (this._resultHandler != null)
			{
				this._resultHandler.SetResult(value);
			}
		}

		// Token: 0x040003AD RID: 941
		private readonly IResultHandler _resultHandler;
	}
}
