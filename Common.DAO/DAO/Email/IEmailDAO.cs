using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.DAO.Email
{
	// Token: 0x0200007C RID: 124
	public interface IEmailDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000313 RID: 787
		TPMailResult SendEmail(TPSmtpClient smtpSettings, string defaultEmailAddress, string to, string from, string subject, string bodytext, string bodyhtml = null, string cc = null, string bcc = null, string attachments = null);

		// Token: 0x06000314 RID: 788
		Task<TPMailResult> SendEmailAsync(TPSmtpClient smtpSettings, string defaultEmailAddress, string to, string from, string subject, string bodytext, string bodyhtml = null, string cc = null, string bcc = null, string attachments = null);

		// Token: 0x06000315 RID: 789
		IList<TPMailMessage> SendEmails(TPSmtpClient smtpSettings, string defaultEmailAddress, params TPMailMessage[] mailMessages);

		// Token: 0x06000316 RID: 790
		Task<IList<TPMailMessage>> SendEmailsAsync(TPSmtpClient smtpSettings, string defaultEmailAddress, params TPMailMessage[] mailMessages);

		// Token: 0x06000317 RID: 791
		TPSmtpClient ParseSettings(string xml);

		// Token: 0x06000318 RID: 792
		string GetXmlFromSettings(TPSmtpClient smtpSettings);
	}
}
