using System;
using System.Data.Entity.Core.EntityClient;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x0200016E RID: 366
	internal class CancelableEntityConnectionDispatcher
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x00039B4C File Offset: 0x00037D4C
		public InternalDispatcher<ICancelableEntityConnectionInterceptor> InternalDispatcher
		{
			get
			{
				return this._internalDispatcher;
			}
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x00039B78 File Offset: 0x00037D78
		public virtual bool Opening(EntityConnection entityConnection, DbInterceptionContext interceptionContext)
		{
			return this._internalDispatcher.Dispatch<bool>(true, (bool b, ICancelableEntityConnectionInterceptor i) => i.ConnectionOpening(entityConnection, interceptionContext) && b);
		}

		// Token: 0x0400033F RID: 831
		private readonly InternalDispatcher<ICancelableEntityConnectionInterceptor> _internalDispatcher = new InternalDispatcher<ICancelableEntityConnectionInterceptor>();
	}
}
