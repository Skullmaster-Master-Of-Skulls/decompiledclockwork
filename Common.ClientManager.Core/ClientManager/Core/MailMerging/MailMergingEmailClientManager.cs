using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.MailMerging;
using TechnoPro.Common.Core.Mappers.TPMailMan;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.MailMerging
{
	// Token: 0x0200003C RID: 60
	public class MailMergingEmailClientManager : IMailMergingEmailClientManager, IWebService
	{
		// Token: 0x06000224 RID: 548 RVA: 0x0000A434 File Offset: 0x00008634
		public TPMailMessageDTO MailMergeFromTemplateXml(MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary, string TemplateXml)
		{
			MailMergeEmailFromTemplateXmlReq mailMergeEmailFromTemplateXmlReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeEmailFromTemplateXmlReq>();
			mailMergeEmailFromTemplateXmlReq.ContextWithCustomDictionary = ContextWithCustomDictionary;
			mailMergeEmailFromTemplateXmlReq.TemplateXml = TemplateXml;
			mailMergeEmailFromTemplateXmlReq.BinPath = ((mailMergeEmailFromTemplateXmlReq.ApplicationContext != null) ? mailMergeEmailFromTemplateXmlReq.ApplicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IMailMergingEmail>().MailMergeFromTemplateXml(mailMergeEmailFromTemplateXmlReq).MailMessage;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000A490 File Offset: 0x00008690
		public TPMailMessageDTO MailMergeFromTemplate(MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary, int TemplateId)
		{
			MailMergeEmailFromTemplateReq mailMergeEmailFromTemplateReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeEmailFromTemplateReq>();
			mailMergeEmailFromTemplateReq.ContextWithCustomDictionary = ContextWithCustomDictionary;
			mailMergeEmailFromTemplateReq.TemplateId = TemplateId;
			mailMergeEmailFromTemplateReq.BinPath = ((mailMergeEmailFromTemplateReq.ApplicationContext != null) ? mailMergeEmailFromTemplateReq.ApplicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IMailMergingEmail>().MailMergeFromTemplate(mailMergeEmailFromTemplateReq).MailMessage;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000A4EC File Offset: 0x000086EC
		public TPMailMessageDTO MailMergeFromTemplateInWebSettings(MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary, Setting WebSettingEmailXmlTemplate)
		{
			MailMergeEmailFromTemplateInWebSettingsReq mailMergeEmailFromTemplateInWebSettingsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeEmailFromTemplateInWebSettingsReq>();
			mailMergeEmailFromTemplateInWebSettingsReq.ContextWithCustomDictionary = ContextWithCustomDictionary;
			mailMergeEmailFromTemplateInWebSettingsReq.WebSetting = (int)WebSettingEmailXmlTemplate;
			mailMergeEmailFromTemplateInWebSettingsReq.BinPath = ((mailMergeEmailFromTemplateInWebSettingsReq.ApplicationContext != null) ? mailMergeEmailFromTemplateInWebSettingsReq.ApplicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IMailMergingEmail>().MailMergeFromTemplateInWebSettings(mailMergeEmailFromTemplateInWebSettingsReq).MailMessage;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000A548 File Offset: 0x00008748
		public IDictionary<int, TPMailMessageDTO> MailMergeAccommodationLetterCoursesEmail(IList<int> LuCourseIds, MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary, int TemplateId)
		{
			MailMergeAccommodationLetterCoursesEmailReq mailMergeAccommodationLetterCoursesEmailReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeAccommodationLetterCoursesEmailReq>();
			mailMergeAccommodationLetterCoursesEmailReq.ContextWithCustomDictionary = ContextWithCustomDictionary;
			mailMergeAccommodationLetterCoursesEmailReq.LuCourseIds = LuCourseIds;
			mailMergeAccommodationLetterCoursesEmailReq.TemplateId = TemplateId;
			mailMergeAccommodationLetterCoursesEmailReq.BinPath = ((mailMergeAccommodationLetterCoursesEmailReq.ApplicationContext != null) ? mailMergeAccommodationLetterCoursesEmailReq.ApplicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IMailMergingEmail>().MailMergeAccommodationLetterCoursesEmail(mailMergeAccommodationLetterCoursesEmailReq).EmailsWithLucids;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000A5AC File Offset: 0x000087AC
		public TPMailMessageDTO MailMergeAccommodationSingleLetterEmail(IList<int> LuCourseIds, MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary, int TemplateId)
		{
			MailMergeAccommodationSingleLetterEmailReq mailMergeAccommodationSingleLetterEmailReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeAccommodationSingleLetterEmailReq>();
			mailMergeAccommodationSingleLetterEmailReq.ContextWithCustomDictionary = ContextWithCustomDictionary;
			mailMergeAccommodationSingleLetterEmailReq.LuCourseIds = LuCourseIds;
			mailMergeAccommodationSingleLetterEmailReq.TemplateId = TemplateId;
			mailMergeAccommodationSingleLetterEmailReq.BinPath = ((mailMergeAccommodationSingleLetterEmailReq.ApplicationContext != null) ? mailMergeAccommodationSingleLetterEmailReq.ApplicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IMailMergingEmail>().MailMergeAccommodationSingleLetterEmail(mailMergeAccommodationSingleLetterEmailReq).Email;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000A610 File Offset: 0x00008810
		public IDictionary<MailMergeContextDTO, TPMailMessageDTO> MailMergeMultipleEmailsFromTemplateXml(IList<MailMergeContextWithCustomDictionaryDTO> ContextsWithCustomDictionaries, string TemplateXml)
		{
			MailMergeMultipleEmailsFromTemplateXmlReq mailMergeMultipleEmailsFromTemplateXmlReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeMultipleEmailsFromTemplateXmlReq>();
			mailMergeMultipleEmailsFromTemplateXmlReq.ContextsWithCustomDictionaries = ContextsWithCustomDictionaries;
			mailMergeMultipleEmailsFromTemplateXmlReq.TemplateXml = TemplateXml;
			mailMergeMultipleEmailsFromTemplateXmlReq.BinPath = ((mailMergeMultipleEmailsFromTemplateXmlReq.ApplicationContext != null) ? mailMergeMultipleEmailsFromTemplateXmlReq.ApplicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IMailMergingEmail>().MailMergeMultipleEmailsFromTemplateXml(mailMergeMultipleEmailsFromTemplateXmlReq).MailMessages;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000A66C File Offset: 0x0000886C
		public IDictionary<MailMergeContextDTO, TPMailMessageDTO> MailMergeMultipleEmailsFromEmailTemplate(IList<MailMergeContextWithCustomDictionaryDTO> ContextsWithCustomDictionaries, TPMailMessageDTO EmailTemplate)
		{
			bool flag = EmailTemplate == null;
			if (flag)
			{
				EmailTemplate = new TPMailMessageDTO();
			}
			string templateXml = EmailTemplate.ToDomainObject().ToEmailXml();
			MailMergeMultipleEmailsFromTemplateXmlReq mailMergeMultipleEmailsFromTemplateXmlReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeMultipleEmailsFromTemplateXmlReq>();
			mailMergeMultipleEmailsFromTemplateXmlReq.ContextsWithCustomDictionaries = ContextsWithCustomDictionaries;
			mailMergeMultipleEmailsFromTemplateXmlReq.TemplateXml = templateXml;
			mailMergeMultipleEmailsFromTemplateXmlReq.BinPath = ((mailMergeMultipleEmailsFromTemplateXmlReq.ApplicationContext != null) ? mailMergeMultipleEmailsFromTemplateXmlReq.ApplicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IMailMergingEmail>().MailMergeMultipleEmailsFromTemplateXml(mailMergeMultipleEmailsFromTemplateXmlReq).MailMessages;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000A6E4 File Offset: 0x000088E4
		public IDictionary<MailMergeContextDTO, TPMailMessageDTO> MailMergeMultipleEmailsFromTemplateId(IList<MailMergeContextWithCustomDictionaryDTO> ContextsWithCustomDictionaries, int TemplateId)
		{
			MailMergeMultipleEmailsFromTemplateIdReq mailMergeMultipleEmailsFromTemplateIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeMultipleEmailsFromTemplateIdReq>();
			mailMergeMultipleEmailsFromTemplateIdReq.ContextsWithCustomDictionaries = ContextsWithCustomDictionaries;
			mailMergeMultipleEmailsFromTemplateIdReq.TemplateId = TemplateId;
			mailMergeMultipleEmailsFromTemplateIdReq.BinPath = ((mailMergeMultipleEmailsFromTemplateIdReq.ApplicationContext != null) ? mailMergeMultipleEmailsFromTemplateIdReq.ApplicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IMailMergingEmail>().MailMergeMultipleEmailsFromTemplateId(mailMergeMultipleEmailsFromTemplateIdReq).MailMessages;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000A740 File Offset: 0x00008940
		public IDictionary<MailMergeContextDTO, TPMailMessageDTO> MailMergeMultipleEmailsFromTemplateInWebSettings(IList<MailMergeContextWithCustomDictionaryDTO> ContextsWithCustomDictionaries, Setting WebSettingExmailXmlTemplate)
		{
			MailMergeMultipleEmailsFromTemplateInWebSettingsReq mailMergeMultipleEmailsFromTemplateInWebSettingsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeMultipleEmailsFromTemplateInWebSettingsReq>();
			mailMergeMultipleEmailsFromTemplateInWebSettingsReq.ContextsWithCustomDictionaries = ContextsWithCustomDictionaries;
			mailMergeMultipleEmailsFromTemplateInWebSettingsReq.WebSettingId = (int)WebSettingExmailXmlTemplate;
			mailMergeMultipleEmailsFromTemplateInWebSettingsReq.BinPath = ((mailMergeMultipleEmailsFromTemplateInWebSettingsReq.ApplicationContext != null) ? mailMergeMultipleEmailsFromTemplateInWebSettingsReq.ApplicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IMailMergingEmail>().MailMergeMultipleEmailsFromTemplateInWebSettings(mailMergeMultipleEmailsFromTemplateInWebSettingsReq).MailMessages;
		}
	}
}
