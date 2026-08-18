using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.MailMerging;
using TechnoPro.Common.Core.Mappers.TPMailMan;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.MailMerging
{
	// Token: 0x02000030 RID: 48
	public class MailMergingEmailRestClientManager : BearerTokenRestProxy<IMailMergingEmailClientManager>, IMailMergingEmailClientManager, IWebService
	{
		// Token: 0x060001BB RID: 443 RVA: 0x00006490 File Offset: 0x00004690
		public MailMergingEmailRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000649A File Offset: 0x0000469A
		public MailMergingEmailRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000064A8 File Offset: 0x000046A8
		public TPMailMessageDTO MailMergeFromTemplateXml(MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary, string TemplateXml)
		{
			MailMergeEmailFromTemplateXmlReq mailMergeEmailFromTemplateXmlReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeEmailFromTemplateXmlReq>();
			mailMergeEmailFromTemplateXmlReq.ContextWithCustomDictionary = ContextWithCustomDictionary;
			mailMergeEmailFromTemplateXmlReq.TemplateXml = TemplateXml;
			BaseReportMessageReq baseReportMessageReq = mailMergeEmailFromTemplateXmlReq;
			ApplicationContext applicationContext = mailMergeEmailFromTemplateXmlReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return base.Post<MailMergeEmailFromTemplateXmlReq, TPMailMessageDTO>(mailMergeEmailFromTemplateXmlReq, "mailmergingemail/fromtemplatexml");
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000064F4 File Offset: 0x000046F4
		public TPMailMessageDTO MailMergeFromTemplate(MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary, int TemplateId)
		{
			MailMergeEmailFromTemplateReq mailMergeEmailFromTemplateReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeEmailFromTemplateReq>();
			mailMergeEmailFromTemplateReq.ContextWithCustomDictionary = ContextWithCustomDictionary;
			mailMergeEmailFromTemplateReq.TemplateId = TemplateId;
			BaseReportMessageReq baseReportMessageReq = mailMergeEmailFromTemplateReq;
			ApplicationContext applicationContext = mailMergeEmailFromTemplateReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return base.Post<MailMergeEmailFromTemplateReq, TPMailMessageDTO>(mailMergeEmailFromTemplateReq, "mailmergingemail/fromtemplate");
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00006540 File Offset: 0x00004740
		public TPMailMessageDTO MailMergeFromTemplateInWebSettings(MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary, Setting WebSettingEmailXmlTemplate)
		{
			MailMergeEmailFromTemplateInWebSettingsReq mailMergeEmailFromTemplateInWebSettingsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeEmailFromTemplateInWebSettingsReq>();
			mailMergeEmailFromTemplateInWebSettingsReq.ContextWithCustomDictionary = ContextWithCustomDictionary;
			mailMergeEmailFromTemplateInWebSettingsReq.WebSetting = (int)WebSettingEmailXmlTemplate;
			mailMergeEmailFromTemplateInWebSettingsReq.BinPath = ((mailMergeEmailFromTemplateInWebSettingsReq.ApplicationContext != null) ? mailMergeEmailFromTemplateInWebSettingsReq.ApplicationContext.ExecutingPath : null);
			return base.Post<MailMergeEmailFromTemplateInWebSettingsReq, TPMailMessageDTO>(mailMergeEmailFromTemplateInWebSettingsReq, "mailmergingemail/fromtemplateinwebsettings");
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00006590 File Offset: 0x00004790
		public IDictionary<int, TPMailMessageDTO> MailMergeAccommodationLetterCoursesEmail(IList<int> LuCourseIds, MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary, int TemplateId)
		{
			MailMergeAccommodationLetterCoursesEmailReq mailMergeAccommodationLetterCoursesEmailReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeAccommodationLetterCoursesEmailReq>();
			mailMergeAccommodationLetterCoursesEmailReq.ContextWithCustomDictionary = ContextWithCustomDictionary;
			mailMergeAccommodationLetterCoursesEmailReq.LuCourseIds = LuCourseIds;
			mailMergeAccommodationLetterCoursesEmailReq.TemplateId = TemplateId;
			mailMergeAccommodationLetterCoursesEmailReq.BinPath = ((mailMergeAccommodationLetterCoursesEmailReq.ApplicationContext != null) ? mailMergeAccommodationLetterCoursesEmailReq.ApplicationContext.ExecutingPath : null);
			return base.Post<MailMergeAccommodationLetterCoursesEmailReq, MailMergeAccommodationLetterCoursesEmailResp>(mailMergeAccommodationLetterCoursesEmailReq, "mailmergingemail/accommodationsingleletteremail").EmailsWithLucids;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000065EC File Offset: 0x000047EC
		public TPMailMessageDTO MailMergeAccommodationSingleLetterEmail(IList<int> LuCourseIds, MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary, int TemplateId)
		{
			MailMergeAccommodationSingleLetterEmailReq mailMergeAccommodationSingleLetterEmailReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeAccommodationSingleLetterEmailReq>();
			mailMergeAccommodationSingleLetterEmailReq.ContextWithCustomDictionary = ContextWithCustomDictionary;
			mailMergeAccommodationSingleLetterEmailReq.LuCourseIds = LuCourseIds;
			mailMergeAccommodationSingleLetterEmailReq.TemplateId = TemplateId;
			mailMergeAccommodationSingleLetterEmailReq.BinPath = ((mailMergeAccommodationSingleLetterEmailReq.ApplicationContext != null) ? mailMergeAccommodationSingleLetterEmailReq.ApplicationContext.ExecutingPath : null);
			return base.Post<MailMergeAccommodationSingleLetterEmailReq, TPMailMessageDTO>(mailMergeAccommodationSingleLetterEmailReq, "mailmergingemail/accommodationsingleletteremail");
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00006644 File Offset: 0x00004844
		public IDictionary<MailMergeContextDTO, TPMailMessageDTO> MailMergeMultipleEmailsFromTemplateXml(IList<MailMergeContextWithCustomDictionaryDTO> ContextsWithCustomDictionaries, string TemplateXml)
		{
			MailMergeMultipleEmailsFromTemplateXmlReq mailMergeMultipleEmailsFromTemplateXmlReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeMultipleEmailsFromTemplateXmlReq>();
			mailMergeMultipleEmailsFromTemplateXmlReq.ContextsWithCustomDictionaries = ContextsWithCustomDictionaries;
			mailMergeMultipleEmailsFromTemplateXmlReq.TemplateXml = TemplateXml;
			mailMergeMultipleEmailsFromTemplateXmlReq.BinPath = ((mailMergeMultipleEmailsFromTemplateXmlReq.ApplicationContext != null) ? mailMergeMultipleEmailsFromTemplateXmlReq.ApplicationContext.ExecutingPath : null);
			return base.Post<MailMergeMultipleEmailsFromTemplateXmlReq, MailMergeMultipleEmailsFromTemplateXmlResp>(mailMergeMultipleEmailsFromTemplateXmlReq, "mailmergingemail/multipleemailsfromtemplatexml").MailMessages;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00006698 File Offset: 0x00004898
		public IDictionary<MailMergeContextDTO, TPMailMessageDTO> MailMergeMultipleEmailsFromTemplateId(IList<MailMergeContextWithCustomDictionaryDTO> ContextsWithCustomDictionaries, int TemplateId)
		{
			MailMergeMultipleEmailsFromTemplateIdReq mailMergeMultipleEmailsFromTemplateIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeMultipleEmailsFromTemplateIdReq>();
			mailMergeMultipleEmailsFromTemplateIdReq.ContextsWithCustomDictionaries = ContextsWithCustomDictionaries;
			mailMergeMultipleEmailsFromTemplateIdReq.TemplateId = TemplateId;
			mailMergeMultipleEmailsFromTemplateIdReq.BinPath = ((mailMergeMultipleEmailsFromTemplateIdReq.ApplicationContext != null) ? mailMergeMultipleEmailsFromTemplateIdReq.ApplicationContext.ExecutingPath : null);
			return base.Post<MailMergeMultipleEmailsFromTemplateIdReq, MailMergeMultipleEmailsFromTemplateIdResp>(mailMergeMultipleEmailsFromTemplateIdReq, "mailmergingemail/multipleemailsfromtemplate").MailMessages;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000066EC File Offset: 0x000048EC
		public IDictionary<MailMergeContextDTO, TPMailMessageDTO> MailMergeMultipleEmailsFromTemplateInWebSettings(IList<MailMergeContextWithCustomDictionaryDTO> ContextsWithCustomDictionaries, Setting WebSettingExmailXmlTemplate)
		{
			MailMergeMultipleEmailsFromTemplateInWebSettingsReq mailMergeMultipleEmailsFromTemplateInWebSettingsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeMultipleEmailsFromTemplateInWebSettingsReq>();
			mailMergeMultipleEmailsFromTemplateInWebSettingsReq.ContextsWithCustomDictionaries = ContextsWithCustomDictionaries;
			mailMergeMultipleEmailsFromTemplateInWebSettingsReq.WebSettingId = (int)WebSettingExmailXmlTemplate;
			mailMergeMultipleEmailsFromTemplateInWebSettingsReq.BinPath = ((mailMergeMultipleEmailsFromTemplateInWebSettingsReq.ApplicationContext != null) ? mailMergeMultipleEmailsFromTemplateInWebSettingsReq.ApplicationContext.ExecutingPath : null);
			return base.Post<MailMergeMultipleEmailsFromTemplateInWebSettingsReq, MailMergeMultipleEmailsFromTemplateInWebSettingsResp>(mailMergeMultipleEmailsFromTemplateInWebSettingsReq, "mailmergingemail/multipleemailsfromtemplateinwebsettings").MailMessages;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00006740 File Offset: 0x00004940
		public IDictionary<MailMergeContextDTO, TPMailMessageDTO> MailMergeMultipleEmailsFromEmailTemplate(IList<MailMergeContextWithCustomDictionaryDTO> ContextsWithCustomDictionaries, TPMailMessageDTO EmailTemplate)
		{
			if (EmailTemplate == null)
			{
				EmailTemplate = new TPMailMessageDTO();
			}
			string templateXml = EmailTemplate.ToDomainObject().ToEmailXml();
			return this.MailMergeMultipleEmailsFromTemplateXml(ContextsWithCustomDictionaries, templateXml);
		}
	}
}
