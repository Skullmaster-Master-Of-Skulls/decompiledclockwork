using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.ClientManager.ICore.Email
{
	// Token: 0x02000059 RID: 89
	public interface IEmailClientManager : IWebService
	{
		// Token: 0x0600029E RID: 670
		SendEmailsResp SendEmail(TPMailMessageDTO MailMessage, string ContextForLogging = "");

		// Token: 0x0600029F RID: 671
		SendEmailsResp SendEmails(IList<TPMailMessageDTO> MailMessages, string ContextForLogging = "");

		// Token: 0x060002A0 RID: 672
		SendEmailsResp SendEmails(IList<TPMailMessageDTO> MailMessages, string emailTestModeAddress, string ContextForLogging = "");

		// Token: 0x060002A1 RID: 673
		SendEmailsResp SendEmail(MailMergeContextDTO Context, Setting EmailTemplateSetting, Group Module, Dictionary<string, string> Args = null);

		// Token: 0x060002A2 RID: 674
		SendEmailsResp SendEmail(int PersonId, Setting EmailTemplateSetting, Group Module, Dictionary<string, string> Args = null);

		// Token: 0x060002A3 RID: 675
		SendEmailsResp SendEmail(Setting EmailTemplateSetting, MailMergeContextWithCustomDictionaryDTO MailMergeContextWithCustomDictionary, string ContextForLogging = "");

		// Token: 0x060002A4 RID: 676
		SendEmailsResp SendEmail(Setting EmailTemplateSetting, MailMergeContextDTO MailMergeContext, StringDictionary Args, string ContextForLogging = "");

		// Token: 0x060002A5 RID: 677
		SendEmailsResp SendEmail(Setting EmailTemplateSetting, MailMergeContextDTO MailMergeContext, Group Module, Func<Dictionary<string, string>> GetArgs);

		// Token: 0x060002A6 RID: 678
		string GetDefaultFromAddress();

		// Token: 0x060002A7 RID: 679
		SendEmailsResp SendEmail(string EmailTemplateSettingXml, MailMergeContextWithCustomDictionaryDTO MailMergeContextWithCustomDictionary, string ContextForLogging = "");

		// Token: 0x060002A8 RID: 680
		SendEmailsResp SendEmail(int templateId, MailMergeContextDTO MailMergeContext, StringDictionary Args, string ContextForLogging = "");

		// Token: 0x060002A9 RID: 681
		SendEmailsResp SendEmail(int templateId, MailMergeContextWithCustomDictionaryDTO MailMergeContextWithCustomDictionary, string ContextForLogging = "");
	}
}
