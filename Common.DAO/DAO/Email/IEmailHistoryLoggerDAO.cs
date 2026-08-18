using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Emailing;

namespace TechnoPro.Common.DAO.Email
{
	// Token: 0x0200007D RID: 125
	public interface IEmailHistoryLoggerDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000319 RID: 793
		void LogItem(EmailHistoryLoggerItem item);
	}
}
