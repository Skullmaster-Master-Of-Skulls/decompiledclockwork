using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.Core.MailMerging;
using TechnoPro.Common.Core.Mappers.MailMergeEntities;
using TechnoPro.Common.Core.Mappers.TPMailMan;
using TechnoPro.Common.ICore.MailMerging;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000069 RID: 105
	public class MailMergingEmailServiceManager : IMailMergingEmail, IService
	{
		// Token: 0x060003DF RID: 991 RVA: 0x00012400 File Offset: 0x00010600
		public MailMergeEmailFromTemplateXmlResp MailMergeFromTemplateXml(MailMergeEmailFromTemplateXmlReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			operationContext.WhoAmI = (Request.ContextWithCustomDictionary.Context.WhoAmId = Math.Max(Request.WhoAmI, Request.ContextWithCustomDictionary.Context.WhoAmId));
			IMailMergingEmailManager mailMergingEmailManager = new MailMergingEmailManager(operationContext);
			TPMailMessage tPMailMessage = mailMergingEmailManager.MailMerge(Request.ContextWithCustomDictionary.ToDomainObject(), Request.TemplateXml);
			return new MailMergeEmailFromTemplateXmlResp
			{
				MailMessage = tPMailMessage.ToDTO()
			};
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00012484 File Offset: 0x00010684
		public MailMergeEmailFromTemplateResp MailMergeFromTemplate(MailMergeEmailFromTemplateReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			operationContext.WhoAmI = (Request.ContextWithCustomDictionary.Context.WhoAmId = Math.Max(Request.WhoAmI, Request.ContextWithCustomDictionary.Context.WhoAmId));
			IMailMergingEmailManager mailMergingEmailManager = new MailMergingEmailManager(operationContext);
			TPMailMessage tPMailMessage = mailMergingEmailManager.MailMerge(Request.ContextWithCustomDictionary.ToDomainObject(), Request.TemplateId);
			return new MailMergeEmailFromTemplateResp
			{
				MailMessage = tPMailMessage.ToDTO()
			};
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00012508 File Offset: 0x00010708
		public MailMergeEmailFromTemplateInWebSettingsResp MailMergeFromTemplateInWebSettings(MailMergeEmailFromTemplateInWebSettingsReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			operationContext.WhoAmI = (Request.ContextWithCustomDictionary.Context.WhoAmId = Math.Max(Request.WhoAmI, Request.ContextWithCustomDictionary.Context.WhoAmId));
			IMailMergingEmailManager mailMergingEmailManager = new MailMergingEmailManager(operationContext);
			TPMailMessage tPMailMessage = mailMergingEmailManager.MailMerge(Request.ContextWithCustomDictionary.ToDomainObject(), (Setting)Request.WebSetting);
			return new MailMergeEmailFromTemplateInWebSettingsResp
			{
				MailMessage = tPMailMessage.ToDTO()
			};
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0001258C File Offset: 0x0001078C
		public MailMergeAccommodationLetterCoursesEmailResp MailMergeAccommodationLetterCoursesEmail(MailMergeAccommodationLetterCoursesEmailReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			operationContext.WhoAmI = (Request.ContextWithCustomDictionary.Context.WhoAmId = Math.Max(Request.WhoAmI, Request.ContextWithCustomDictionary.Context.WhoAmId));
			IMailMergingEmailManager mailMergingEmailManager = new MailMergingEmailManager(operationContext);
			IDictionary<int, TPMailMessage> dictionary = mailMergingEmailManager.MailMergeAccommodationLetterCoursesEmail(Request.LuCourseIds, Request.ContextWithCustomDictionary.ToDomainObject(), Request.TemplateId);
			Dictionary<int, TPMailMessageDTO> dictionary2 = new Dictionary<int, TPMailMessageDTO>();
			foreach (KeyValuePair<int, TPMailMessage> keyValuePair in dictionary)
			{
				dictionary2.Add(keyValuePair.Key, keyValuePair.Value.ToDTO());
			}
			return new MailMergeAccommodationLetterCoursesEmailResp
			{
				EmailsWithLucids = dictionary2
			};
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00012670 File Offset: 0x00010870
		public MailMergeAccommodationSingleLetterEmailResp MailMergeAccommodationSingleLetterEmail(MailMergeAccommodationSingleLetterEmailReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			operationContext.WhoAmI = (Request.ContextWithCustomDictionary.Context.WhoAmId = Math.Max(Request.WhoAmI, Request.ContextWithCustomDictionary.Context.WhoAmId));
			IMailMergingEmailManager mailMergingEmailManager = new MailMergingEmailManager(operationContext);
			TPMailMessage tPMailMessage = mailMergingEmailManager.MailMergeAccommodationSingleLetterEmail(Request.LuCourseIds, Request.ContextWithCustomDictionary.ToDomainObject(), Request.TemplateId);
			return new MailMergeAccommodationSingleLetterEmailResp
			{
				Email = tPMailMessage.ToDTO()
			};
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x000126F8 File Offset: 0x000108F8
		public MailMergeMultipleEmailsFromTemplateXmlResp MailMergeMultipleEmailsFromTemplateXml(MailMergeMultipleEmailsFromTemplateXmlReq Request)
		{
			IMailMergingEmailManager mailMergingEmailManager = new MailMergingEmailManager(Request.GetOperationContext());
			List<MailMergeContextWithCustomDictionary> list = Request.ContextsWithCustomDictionaries.ToList<MailMergeContextWithCustomDictionaryDTO>().ConvertAll<MailMergeContextWithCustomDictionary>((MailMergeContextWithCustomDictionaryDTO g) => g.ToDomainObject());
			foreach (MailMergeContextWithCustomDictionary mailMergeContextWithCustomDictionary in list)
			{
				mailMergeContextWithCustomDictionary.Context.WhoAmId = Request.WhoAmI;
			}
			IDictionary<MailMergeContext, TPMailMessage> dictionary = mailMergingEmailManager.MailMerge(list, Request.TemplateXml);
			Dictionary<MailMergeContextDTO, TPMailMessageDTO> dictionary2 = new Dictionary<MailMergeContextDTO, TPMailMessageDTO>();
			foreach (KeyValuePair<MailMergeContext, TPMailMessage> keyValuePair in dictionary)
			{
				dictionary2.Add(keyValuePair.Key.ToDTO(), keyValuePair.Value.ToDTO());
			}
			return new MailMergeMultipleEmailsFromTemplateXmlResp
			{
				MailMessages = dictionary2
			};
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00012814 File Offset: 0x00010A14
		public MailMergeMultipleEmailsFromTemplateIdResp MailMergeMultipleEmailsFromTemplateId(MailMergeMultipleEmailsFromTemplateIdReq Request)
		{
			IMailMergingEmailManager mailMergingEmailManager = new MailMergingEmailManager(Request.GetOperationContext());
			List<MailMergeContextWithCustomDictionary> list = Request.ContextsWithCustomDictionaries.ToList<MailMergeContextWithCustomDictionaryDTO>().ConvertAll<MailMergeContextWithCustomDictionary>((MailMergeContextWithCustomDictionaryDTO g) => g.ToDomainObject());
			foreach (MailMergeContextWithCustomDictionary mailMergeContextWithCustomDictionary in list)
			{
				mailMergeContextWithCustomDictionary.Context.WhoAmId = Request.WhoAmI;
			}
			IDictionary<MailMergeContext, TPMailMessage> dictionary = mailMergingEmailManager.MailMerge(list, Request.TemplateId);
			Dictionary<MailMergeContextDTO, TPMailMessageDTO> dictionary2 = new Dictionary<MailMergeContextDTO, TPMailMessageDTO>();
			foreach (KeyValuePair<MailMergeContext, TPMailMessage> keyValuePair in dictionary)
			{
				dictionary2.Add(keyValuePair.Key.ToDTO(), keyValuePair.Value.ToDTO());
			}
			return new MailMergeMultipleEmailsFromTemplateIdResp
			{
				MailMessages = dictionary2
			};
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00012930 File Offset: 0x00010B30
		public MailMergeMultipleEmailsFromTemplateInWebSettingsResp MailMergeMultipleEmailsFromTemplateInWebSettings(MailMergeMultipleEmailsFromTemplateInWebSettingsReq Request)
		{
			IMailMergingEmailManager mailMergingEmailManager = new MailMergingEmailManager(Request.GetOperationContext());
			List<MailMergeContextWithCustomDictionary> list = Request.ContextsWithCustomDictionaries.ToList<MailMergeContextWithCustomDictionaryDTO>().ConvertAll<MailMergeContextWithCustomDictionary>((MailMergeContextWithCustomDictionaryDTO g) => g.ToDomainObject());
			foreach (MailMergeContextWithCustomDictionary mailMergeContextWithCustomDictionary in list)
			{
				mailMergeContextWithCustomDictionary.Context.WhoAmId = Request.WhoAmI;
			}
			IDictionary<MailMergeContext, TPMailMessage> dictionary = mailMergingEmailManager.MailMerge(list, (Setting)Request.WebSettingId);
			Dictionary<MailMergeContextDTO, TPMailMessageDTO> dictionary2 = new Dictionary<MailMergeContextDTO, TPMailMessageDTO>();
			foreach (KeyValuePair<MailMergeContext, TPMailMessage> keyValuePair in dictionary)
			{
				dictionary2.Add(keyValuePair.Key.ToDTO(), keyValuePair.Value.ToDTO());
			}
			return new MailMergeMultipleEmailsFromTemplateInWebSettingsResp
			{
				MailMessages = dictionary2
			};
		}
	}
}
