using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.MailMerging
{
	// Token: 0x02000011 RID: 17
	public interface IMailMergingEmailWebClientManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600002F RID: 47
		TPMailMessageDTO MailMergeFromTemplate(MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary, int TemplateId);

		// Token: 0x06000030 RID: 48
		TPMailMessageDTO MailMergeFromTemplateInWebSettings(MailMergeContextWithCustomDictionaryDTO ContextsWithCustomDictionaries, Setting WebSetting);

		// Token: 0x06000031 RID: 49
		TPMailMessageDTO MailMergeFromTemplateXml(MailMergeContextWithCustomDictionaryDTO ContextsWithCustomDictionaries, string TemplateXml);

		// Token: 0x06000032 RID: 50
		IDictionary<int, TPMailMessageDTO> MailMergeAccommodationLetterCoursesEmail(MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary, IList<int> LuCourseIds, int TemplateId);

		// Token: 0x06000033 RID: 51
		TPMailMessageDTO MailMergeAccommodationSingleLetterEmail(MailMergeContextWithCustomDictionaryDTO ContextsWithCustomDictionaries, IList<int> LuCourseIds, int TemplateId);

		// Token: 0x06000034 RID: 52
		IDictionary<MailMergeContextDTO, TPMailMessageDTO> MailMergeMultipleEmailsFromTemplateId(IList<MailMergeContextWithCustomDictionaryDTO> ContextsWithCustomDictionaries, int TemplateId);

		// Token: 0x06000035 RID: 53
		IDictionary<MailMergeContextDTO, TPMailMessageDTO> MailMergeMultipleEmailsFromTemplateInWebSettings(IList<MailMergeContextWithCustomDictionaryDTO> ContextsWithCustomDictionaries, Setting WebSetting);

		// Token: 0x06000036 RID: 54
		IDictionary<MailMergeContextDTO, TPMailMessageDTO> MailMergeMultipleEmailsFromTemplateXml(IList<MailMergeContextWithCustomDictionaryDTO> ContextsWithCustomDictionaries, string TemplateXml);
	}
}
