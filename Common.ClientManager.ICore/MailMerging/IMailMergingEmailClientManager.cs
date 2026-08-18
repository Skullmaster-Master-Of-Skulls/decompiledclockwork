using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.ClientManager.ICore.MailMerging
{
	// Token: 0x02000038 RID: 56
	public interface IMailMergingEmailClientManager : IWebService
	{
		// Token: 0x0600018C RID: 396
		TPMailMessageDTO MailMergeFromTemplateXml(MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary, string TemplateXml);

		// Token: 0x0600018D RID: 397
		TPMailMessageDTO MailMergeFromTemplate(MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary, int TemplateId);

		// Token: 0x0600018E RID: 398
		TPMailMessageDTO MailMergeFromTemplateInWebSettings(MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary, Setting WebSettingEmailXmlTemplate);

		// Token: 0x0600018F RID: 399
		IDictionary<int, TPMailMessageDTO> MailMergeAccommodationLetterCoursesEmail(IList<int> LuCourseIds, MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary, int TemplateId);

		// Token: 0x06000190 RID: 400
		TPMailMessageDTO MailMergeAccommodationSingleLetterEmail(IList<int> LuCourseIds, MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary, int TemplateId);

		// Token: 0x06000191 RID: 401
		IDictionary<MailMergeContextDTO, TPMailMessageDTO> MailMergeMultipleEmailsFromTemplateXml(IList<MailMergeContextWithCustomDictionaryDTO> ContextsWithCustomDictionaries, string TemplateXml);

		// Token: 0x06000192 RID: 402
		IDictionary<MailMergeContextDTO, TPMailMessageDTO> MailMergeMultipleEmailsFromTemplateId(IList<MailMergeContextWithCustomDictionaryDTO> ContextsWithCustomDictionaries, int TemplateId);

		// Token: 0x06000193 RID: 403
		IDictionary<MailMergeContextDTO, TPMailMessageDTO> MailMergeMultipleEmailsFromTemplateInWebSettings(IList<MailMergeContextWithCustomDictionaryDTO> ContextsWithCustomDictionaries, Setting WebSettingExmailXmlTemplate);

		// Token: 0x06000194 RID: 404
		IDictionary<MailMergeContextDTO, TPMailMessageDTO> MailMergeMultipleEmailsFromEmailTemplate(IList<MailMergeContextWithCustomDictionaryDTO> ContextsWithCustomDictionaries, TPMailMessageDTO EmailTemplate);
	}
}
