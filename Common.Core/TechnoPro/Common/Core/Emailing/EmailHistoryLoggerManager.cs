using System;
using ClockWorkLogger;
using TechnoPro.Common.DAO.Email;
using TechnoPro.Common.DAO.Impl.Email;
using TechnoPro.Common.ICore.Emailing;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Emailing;

namespace TechnoPro.Common.Core.Emailing
{
	// Token: 0x020000F6 RID: 246
	public class EmailHistoryLoggerManager : IEmailHistoryLoggerManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000999 RID: 2457 RVA: 0x0003CB8A File Offset: 0x0003AD8A
		public EmailHistoryLoggerManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new EmailHistoryLoggerDAO(this.OpContext);
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x0003CBAD File Offset: 0x0003ADAD
		// (set) Token: 0x0600099B RID: 2459 RVA: 0x0003CBB5 File Offset: 0x0003ADB5
		public OperationContext OpContext { get; set; }

		// Token: 0x0600099C RID: 2460 RVA: 0x0003CBC0 File Offset: 0x0003ADC0
		public void LogItem(EmailHistoryLoggerItem item)
		{
			try
			{
				this.dao.LogItem(item);
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("Common.Core.Emailing.EmailHistoryLoggerManager:LogItem:historyCode={0}:err={1}", (item == null) ? "null" : (item.HistoryCode ?? "NULL"), ex.ToString());
			}
		}

		// Token: 0x040001B0 RID: 432
		private IEmailHistoryLoggerDAO dao;
	}
}
