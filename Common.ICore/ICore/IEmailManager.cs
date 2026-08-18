using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Emailing;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.ICore
{
	// Token: 0x02000006 RID: 6
	public interface IEmailManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600002B RID: 43
		TPMailResult SendEmail(string to, string from, string subject, string bodytext, string bodyhtml = null, string cc = null, string bcc = null, string attachments = null);

		// Token: 0x0600002C RID: 44
		Task<TPMailResult> SendEmailAsync(string to, string from, string subject, string bodytext, string bodyhtml = null, string cc = null, string bcc = null, string attachments = null);

		// Token: 0x0600002D RID: 45
		TPMailResult SendEmail(TPMailMessage message);

		// Token: 0x0600002E RID: 46
		Task<TPMailResult> SendEmailAsync(TPMailMessage message);

		// Token: 0x0600002F RID: 47
		IList<TPMailMessage> SendEmail(params TPMailMessage[] messages);

		// Token: 0x06000030 RID: 48
		Task<IList<TPMailMessage>> SendEmailAsync(params TPMailMessage[] messages);

		// Token: 0x06000031 RID: 49
		string GetDefaultFromAddress();

		// Token: 0x06000032 RID: 50
		TPMailResult SendEmail(TPSmtpClient SmtpSettings, TPMailMessage Message);

		// Token: 0x06000033 RID: 51
		Task<TPMailResult> SendEmailAsync(TPSmtpClient SmtpSettings, TPMailMessage Message);

		// Token: 0x06000034 RID: 52
		IList<TPMailResult> SendEmails(IDictionary<MailMergeContext, TPMailMessage> messages, BatchEmailSendParameters parameters);

		// Token: 0x06000035 RID: 53
		EmailListSendCompletedInfo SendEmailsReturnResult(params TPMailMessage[] messages);

		// Token: 0x06000036 RID: 54
		EmailListSendCompletedInfo SendEmailsReturnResult(IList<TPMailMessage> MailMessages, string emailTestModeAddress, string ContextForLogging = "");
	}
}
