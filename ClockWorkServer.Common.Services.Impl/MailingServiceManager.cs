using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.Core;
using TechnoPro.Common.Core.Mappers.TPMailMan;
using TechnoPro.Common.ICore;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Emailing;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000067 RID: 103
	public class MailingServiceManager : IMailing, IService
	{
		// Token: 0x060003CC RID: 972 RVA: 0x000118D4 File Offset: 0x0000FAD4
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x000118E8 File Offset: 0x0000FAE8
		public SendEmailsResp SendEmails(SendEmailsReq request)
		{
			IEmailManager emailManager = new EmailManager(request.GetOperationContext());
			EmailListSendCompletedInfo emailListSendCompletedInfo = emailManager.SendEmailsReturnResult((from g in request.MailMessages
			select g.ToDomainObject()).ToArray<TPMailMessage>());
			SendEmailsResp sendEmailsResp = new SendEmailsResp();
			List<TPMailMessageDTO> mailMessages;
			if (emailListSendCompletedInfo == null)
			{
				mailMessages = null;
			}
			else
			{
				List<TPMailMessage> mailMessages2 = emailListSendCompletedInfo.MailMessages;
				if (mailMessages2 == null)
				{
					mailMessages = null;
				}
				else
				{
					mailMessages = (from g in mailMessages2
					select g.ToDTO()).ToList<TPMailMessageDTO>();
				}
			}
			sendEmailsResp.MailMessages = mailMessages;
			TPMailResultDTO sendEmailResult;
			if (emailListSendCompletedInfo == null)
			{
				sendEmailResult = null;
			}
			else
			{
				TPMailResult sendEmailResult2 = emailListSendCompletedInfo.SendEmailResult;
				sendEmailResult = ((sendEmailResult2 != null) ? sendEmailResult2.ToDTO() : null);
			}
			sendEmailsResp.SendEmailResult = sendEmailResult;
			return sendEmailsResp;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x000119A4 File Offset: 0x0000FBA4
		public GetDefaultFromAddressResp GetDefaultFromAddress(GetDefaultFromAddressReq Request)
		{
			IEmailManager emailManager = new EmailManager(Request.GetOperationContext());
			return new GetDefaultFromAddressResp
			{
				EmailAddress = emailManager.GetDefaultFromAddress()
			};
		}

		// Token: 0x060003CF RID: 975 RVA: 0x000119D4 File Offset: 0x0000FBD4
		public SendEmailWithOverrideSettingsResp SendEmailWithOverrideSettings(SendEmailWithOverrideSettingsReq Request)
		{
			IEmailManager emailManager = new EmailManager(Request.GetOperationContext());
			TPMailResult tPMailResult = emailManager.SendEmail(Request.SmtpSettings.ToDomainObject(), Request.Message.ToDomainObject());
			return new SendEmailWithOverrideSettingsResp
			{
				SendMailResult = tPMailResult.ToDTO()
			};
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00011A24 File Offset: 0x0000FC24
		public SendEmailsReturnResultResp SendEmailsReturnResult(SendEmailsReturnResultReq Request)
		{
			IEmailManager emailManager = new EmailManager(Request.GetOperationContext());
			EmailListSendCompletedInfo emailListSendCompletedInfo = emailManager.SendEmailsReturnResult((from g in Request.MailMessages
			select g.ToDomainObject()).ToList<TPMailMessage>(), Request.EmailTestModeAddress, Request.ContextForLogging);
			SendEmailsReturnResultResp sendEmailsReturnResultResp = new SendEmailsReturnResultResp();
			TPMailResultDTO sendEmailResult;
			if (emailListSendCompletedInfo == null)
			{
				sendEmailResult = null;
			}
			else
			{
				TPMailResult sendEmailResult2 = emailListSendCompletedInfo.SendEmailResult;
				sendEmailResult = ((sendEmailResult2 != null) ? sendEmailResult2.ToDTO() : null);
			}
			sendEmailsReturnResultResp.SendEmailResult = sendEmailResult;
			List<TPMailMessageDTO> mailMessages;
			if (emailListSendCompletedInfo == null)
			{
				mailMessages = null;
			}
			else
			{
				List<TPMailMessage> mailMessages2 = emailListSendCompletedInfo.MailMessages;
				if (mailMessages2 == null)
				{
					mailMessages = null;
				}
				else
				{
					mailMessages = (from g in mailMessages2
					select g.ToDTO()).ToList<TPMailMessageDTO>();
				}
			}
			sendEmailsReturnResultResp.MailMessages = mailMessages;
			return sendEmailsReturnResultResp;
		}
	}
}
