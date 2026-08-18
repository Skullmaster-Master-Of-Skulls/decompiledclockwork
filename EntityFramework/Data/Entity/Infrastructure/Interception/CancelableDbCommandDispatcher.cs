using System;
using System.Data.Common;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x0200016D RID: 365
	internal class CancelableDbCommandDispatcher
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x00039AD6 File Offset: 0x00037CD6
		public InternalDispatcher<ICancelableDbCommandInterceptor> InternalDispatcher
		{
			get
			{
				return this._internalDispatcher;
			}
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x00039B00 File Offset: 0x00037D00
		public virtual bool Executing(DbCommand command, DbInterceptionContext interceptionContext)
		{
			return this._internalDispatcher.Dispatch<bool>(true, (bool b, ICancelableDbCommandInterceptor i) => i.CommandExecuting(command, interceptionContext) && b);
		}

		// Token: 0x0400033E RID: 830
		private readonly InternalDispatcher<ICancelableDbCommandInterceptor> _internalDispatcher = new InternalDispatcher<ICancelableDbCommandInterceptor>();
	}
}
