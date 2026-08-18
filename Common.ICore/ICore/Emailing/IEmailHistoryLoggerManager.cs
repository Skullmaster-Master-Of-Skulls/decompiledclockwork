using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Emailing;

namespace TechnoPro.Common.ICore.Emailing
{
	// Token: 0x02000092 RID: 146
	public interface IEmailHistoryLoggerManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600041A RID: 1050
		void LogItem(EmailHistoryLoggerItem item);
	}
}
