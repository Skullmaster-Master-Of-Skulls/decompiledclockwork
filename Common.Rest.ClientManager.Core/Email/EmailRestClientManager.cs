using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.MailMerging;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Email
{
	// Token: 0x02000050 RID: 80
	public class EmailRestClientManager : BearerTokenRestProxy<IEmailClientManager>, IEmailClientManager, IWebService
	{
		// Token: 0x06000300 RID: 768 RVA: 0x0000909E File Offset: 0x0000729E
		public EmailRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000301 RID: 769 RVA: 0x000090A8 File Offset: 0x000072A8
		public EmailRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000302 RID: 770 RVA: 0x000090B3 File Offset: 0x000072B3
		public SendEmailsResp SendEmail(TPMailMessageDTO MailMessage, string ContextForLogging = "")
		{
			return this.SendEmails(new List<TPMailMessageDTO>
			{
				MailMessage
			}, ContextForLogging);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x000090C8 File Offset: 0x000072C8
		public SendEmailsResp SendEmails(IList<TPMailMessageDTO> MailMessages, string ContextForLogging = "")
		{
			return this.SendEmails(MailMessages, null, ContextForLogging);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x000090D4 File Offset: 0x000072D4
		public SendEmailsResp SendEmails(IList<TPMailMessageDTO> MailMessages, string emailTestModeAddress, string ContextForLogging = "")
		{
			SendEmailsReturnResultReq sendEmailsReturnResultReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SendEmailsReturnResultReq>();
			sendEmailsReturnResultReq.MailMessages = MailMessages;
			sendEmailsReturnResultReq.ContextForLogging = ContextForLogging;
			sendEmailsReturnResultReq.EmailTestModeAddress = emailTestModeAddress;
			return base.Post<SendEmailsReturnResultReq, SendEmailsResp>(sendEmailsReturnResultReq, "mailing/sendemails");
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00009110 File Offset: 0x00007310
		public SendEmailsResp SendEmail(MailMergeContextDTO Context, Setting EmailTemplateSetting, Group Module, Dictionary<string, string> Args = null)
		{
			MailMergeContextWithCustomDictionaryDTO mailMergeContextWithCustomDictionaryDTO = new MailMergeContextWithCustomDictionaryDTO
			{
				Context = Context,
				CustomDictionary = new MailMergeCustomDictionaryDTO
				{
					Args = (Args ?? new Dictionary<string, string>())
				}
			};
			GroupDataAttribute attribute = GroupDataAttribute.GetAttribute(Module);
			if (attribute != null)
			{
				IWebSettingsClientManager webSettingsClientManager = ObjectFactory.Resolve<IWebSettingsClientManager>();
				string value = (attribute.DefaultSignatureSetting > 0 && Enum.IsDefined(typeof(Setting), attribute.DefaultSignatureSetting)) ? webSettingsClientManager.GetSettingValue<string>((Setting)attribute.DefaultSignatureSetting) : "";
				if (!string.IsNullOrEmpty(value))
				{
					mailMergeContextWithCustomDictionaryDTO.CustomDictionary.Args.Add("signature", value);
				}
				string value2 = (attribute.DefaultFromSetting > 0 && Enum.IsDefined(typeof(Setting), attribute.DefaultFromSetting)) ? webSettingsClientManager.GetSettingValue<string>((Setting)attribute.DefaultFromSetting) : "";
				if (!string.IsNullOrEmpty(value2) && !mailMergeContextWithCustomDictionaryDTO.CustomDictionary.Args.ContainsKey("from"))
				{
					mailMergeContextWithCustomDictionaryDTO.CustomDictionary.Args.Add("from", value2);
				}
			}
			return this.SendEmail(EmailTemplateSetting, mailMergeContextWithCustomDictionaryDTO, string.IsNullOrEmpty((attribute != null) ? attribute.Name : null) ? Module.ToString() : attribute.Name);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00009251 File Offset: 0x00007451
		public SendEmailsResp SendEmail(int PersonId, Setting EmailTemplateSetting, Group Module, Dictionary<string, string> Args = null)
		{
			return this.SendEmail(new MailMergeContextDTO
			{
				PersonId = PersonId
			}, EmailTemplateSetting, Module, Args);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000926C File Offset: 0x0000746C
		public SendEmailsResp SendEmail(Setting EmailTemplateSetting, MailMergeContextWithCustomDictionaryDTO MailMergeContextWithCustomDictionary, string ContextForLogging = "")
		{
			SendEmailsResp result;
			try
			{
				if (MailMergeContextWithCustomDictionary.CustomDictionary == null)
				{
					MailMergeContextWithCustomDictionary.CustomDictionary = new MailMergeCustomDictionaryDTO();
				}
				if (MailMergeContextWithCustomDictionary.CustomDictionary.Args == null)
				{
					MailMergeContextWithCustomDictionary.CustomDictionary.Args = new Dictionary<string, string>();
				}
				Dictionary<string, string> args = MailMergeContextWithCustomDictionary.CustomDictionary.Args;
				foreach (KeyValuePair<string, string> keyValuePair in this.GetBaseApplicationMailMergeValues())
				{
					if (!string.IsNullOrEmpty(keyValuePair.Value) && !args.ContainsKey(keyValuePair.Key))
					{
						args.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
				TPMailMessageDTO tpmailMessageDTO = ObjectFactory.Resolve<IMailMergingEmailClientManager>().MailMergeFromTemplateInWebSettings(MailMergeContextWithCustomDictionary, EmailTemplateSetting);
				if (!tpmailMessageDTO.IsActive)
				{
					result = new SendEmailsResp
					{
						MailMessages = new List<TPMailMessageDTO>(),
						SendEmailResult = new TPMailResultDTO
						{
							Status = eTPMailResultStatusDTO.NotSentBecauseTemplateIsDisabled
						}
					};
				}
				else
				{
					result = this.SendEmails(new List<TPMailMessageDTO>
					{
						tpmailMessageDTO
					}, "");
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("EmailClientManager:SendEmail:Context={0}:Error={1}", ContextForLogging ?? "NULL", ex.ToString());
				result = new SendEmailsResp
				{
					SendEmailResult = new TPMailResultDTO
					{
						Status = eTPMailResultStatusDTO.Failed,
						ErrorMessage = ex.ToString()
					}
				};
			}
			return result;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x000093D4 File Offset: 0x000075D4
		public SendEmailsResp SendEmail(Setting EmailTemplateSetting, MailMergeContextDTO MailMergeContext, Group Module, Func<Dictionary<string, string>> GetArgs)
		{
			TPMailMessageDTO tpmailMessageDTO = ObjectFactory.Resolve<IWebSettingsClientManager>().GetSettingValue<string>(EmailTemplateSetting).EmailFromXml();
			if (tpmailMessageDTO == null || !tpmailMessageDTO.IsActive)
			{
				return new SendEmailsResp
				{
					SendEmailResult = new TPMailResultDTO
					{
						Status = eTPMailResultStatusDTO.NotSentBecauseTemplateIsDisabled
					}
				};
			}
			Dictionary<string, string> args = GetArgs();
			return this.SendEmail(MailMergeContext, EmailTemplateSetting, Module, args);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00009429 File Offset: 0x00007629
		public string GetDefaultFromAddress()
		{
			return base.Get<string>("mailing/defaultfromaddress", true);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00009438 File Offset: 0x00007638
		public SendEmailsResp SendEmail(Setting EmailTemplateSetting, MailMergeContextDTO MailMergeContext, StringDictionary Args, string ContextForLogging = "")
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (object obj in (Args ?? new StringDictionary()))
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = dictionaryEntry.Key.ToString();
				if (!string.IsNullOrEmpty(text) && !dictionary.ContainsKey(text))
				{
					Dictionary<string, string> dictionary2 = dictionary;
					string key = text;
					object value = dictionaryEntry.Value;
					dictionary2.Add(key, ((value != null) ? value.ToString() : null) ?? "");
				}
			}
			return this.SendEmail(EmailTemplateSetting, new MailMergeContextWithCustomDictionaryDTO
			{
				Context = MailMergeContext,
				CustomDictionary = new MailMergeCustomDictionaryDTO
				{
					Args = dictionary
				}
			}, ContextForLogging);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00009500 File Offset: 0x00007700
		private Dictionary<string, string> GetBaseApplicationMailMergeValues()
		{
			IWebSettingsClientManager webSettingsClientManager = ObjectFactory.Resolve<IWebSettingsClientManager>();
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			Dictionary<string, string> dictionary = (Dictionary<string, string>)clientCache["webBaseApplicationMailMergeValues"];
			if (dictionary != null)
			{
				return dictionary;
			}
			dictionary = new Dictionary<string, string>
			{
				{
					"from",
					this.GetDefaultFromAddress() ?? ""
				},
				{
					"signature",
					webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_DefaultSignature) ?? ""
				}
			};
			clientCache.Insert("webBaseApplicationMailMergeValues", dictionary, TimeSpan.FromMinutes(30.0));
			return dictionary;
		}
	}
}
