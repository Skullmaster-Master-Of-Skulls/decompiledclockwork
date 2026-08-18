using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.ClientManager.Core.MailMerging;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.MailMerging;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Configuration;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Email
{
	// Token: 0x02000060 RID: 96
	public class EmailClientManager : IEmailClientManager, IWebService
	{
		// Token: 0x06000372 RID: 882 RVA: 0x0000F000 File Offset: 0x0000D200
		public SendEmailsResp SendEmail(TPMailMessageDTO MailMessage, string ContextForLogging = "")
		{
			return this.SendEmails(new List<TPMailMessageDTO>
			{
				MailMessage
			}, ContextForLogging);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000F028 File Offset: 0x0000D228
		public SendEmailsResp SendEmails(IList<TPMailMessageDTO> MailMessages, string ContextForLogging = "")
		{
			string appSettingsByNameUsingProtection = ClockWorkConfigurationManager.GetAppSettingsByNameUsingProtection("emailtestmodeaddress");
			return this.SendEmails(MailMessages, appSettingsByNameUsingProtection, ContextForLogging);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000F050 File Offset: 0x0000D250
		public SendEmailsResp SendEmails(IList<TPMailMessageDTO> MailMessages, string emailTestModeAddress, string ContextForLogging = "")
		{
			SendEmailsReturnResultReq sendEmailsReturnResultReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SendEmailsReturnResultReq>();
			sendEmailsReturnResultReq.MailMessages = MailMessages;
			sendEmailsReturnResultReq.ContextForLogging = ContextForLogging;
			sendEmailsReturnResultReq.EmailTestModeAddress = emailTestModeAddress;
			SendEmailsReturnResultResp sendEmailsReturnResultResp = ClientServiceFactory.GetClientInstance<IMailing>().SendEmailsReturnResult(sendEmailsReturnResultReq);
			SendEmailsResp result;
			if (sendEmailsReturnResultResp != null)
			{
				SendEmailsResp sendEmailsResp = new SendEmailsResp();
				sendEmailsResp.MailMessages = sendEmailsReturnResultResp.MailMessages;
				result = sendEmailsResp;
				sendEmailsResp.SendEmailResult = sendEmailsReturnResultResp.SendEmailResult;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000F0B8 File Offset: 0x0000D2B8
		public SendEmailsResp SendEmail(Setting EmailTemplateSetting, MailMergeContextDTO MailMergeContext, Group Module, Func<Dictionary<string, string>> GetArgs)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			TPMailMessageDTO tpmailMessageDTO = webSettingsClientManager.GetSettingValue<string>(EmailTemplateSetting).EmailFromXml();
			bool flag = tpmailMessageDTO == null || !tpmailMessageDTO.IsActive;
			SendEmailsResp result;
			if (flag)
			{
				result = new SendEmailsResp
				{
					SendEmailResult = new TPMailResultDTO
					{
						Status = eTPMailResultStatusDTO.NotSentBecauseTemplateIsDisabled
					}
				};
			}
			else
			{
				Dictionary<string, string> args = GetArgs();
				result = this.SendEmail(MailMergeContext, EmailTemplateSetting, Module, args);
			}
			return result;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000F124 File Offset: 0x0000D324
		public string GetDefaultFromAddress()
		{
			GetDefaultFromAddressReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetDefaultFromAddressReq>();
			return ClientServiceFactory.GetClientInstance<IMailing>().GetDefaultFromAddress(request).EmailAddress;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000F154 File Offset: 0x0000D354
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
			bool flag = attribute != null;
			if (flag)
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				string value = (attribute.DefaultSignatureSetting > 0 && Enum.IsDefined(typeof(Setting), attribute.DefaultSignatureSetting)) ? webSettingsClientManager.GetSettingValue<string>((Setting)attribute.DefaultSignatureSetting) : "";
				bool flag2 = !string.IsNullOrEmpty(value);
				if (flag2)
				{
					mailMergeContextWithCustomDictionaryDTO.CustomDictionary.Args.Add("signature", value);
				}
				string value2 = (attribute.DefaultFromSetting > 0 && Enum.IsDefined(typeof(Setting), attribute.DefaultFromSetting)) ? webSettingsClientManager.GetSettingValue<string>((Setting)attribute.DefaultFromSetting) : "";
				bool flag3 = !string.IsNullOrEmpty(value2) && !mailMergeContextWithCustomDictionaryDTO.CustomDictionary.Args.ContainsKey("from");
				if (flag3)
				{
					mailMergeContextWithCustomDictionaryDTO.CustomDictionary.Args.Add("from", value2);
				}
			}
			return this.SendEmail(EmailTemplateSetting, mailMergeContextWithCustomDictionaryDTO, string.IsNullOrEmpty((attribute != null) ? attribute.Name : null) ? Module.ToString() : attribute.Name);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000F2BC File Offset: 0x0000D4BC
		public SendEmailsResp SendEmail(int PersonId, Setting EmailTemplateSetting, Group Module, Dictionary<string, string> Args = null)
		{
			return this.SendEmail(new MailMergeContextDTO
			{
				PersonId = PersonId
			}, EmailTemplateSetting, Module, Args);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000F2E8 File Offset: 0x0000D4E8
		public SendEmailsResp SendEmail(Setting EmailTemplateSetting, MailMergeContextWithCustomDictionaryDTO MailMergeContextWithCustomDictionary, string ContextForLogging = "")
		{
			SendEmailsResp result;
			try
			{
				bool flag = MailMergeContextWithCustomDictionary.CustomDictionary == null;
				if (flag)
				{
					MailMergeContextWithCustomDictionary.CustomDictionary = new MailMergeCustomDictionaryDTO();
				}
				bool flag2 = MailMergeContextWithCustomDictionary.CustomDictionary.Args == null;
				if (flag2)
				{
					MailMergeContextWithCustomDictionary.CustomDictionary.Args = new Dictionary<string, string>();
				}
				Dictionary<string, string> args = MailMergeContextWithCustomDictionary.CustomDictionary.Args;
				Dictionary<string, string> baseApplicationMailMergeValues = this.GetBaseApplicationMailMergeValues();
				foreach (KeyValuePair<string, string> keyValuePair in baseApplicationMailMergeValues)
				{
					bool flag3 = !string.IsNullOrEmpty(keyValuePair.Value) && !args.ContainsKey(keyValuePair.Key);
					if (flag3)
					{
						args.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
				IMailMergingEmailClientManager mailMergingEmailClientManager = new MailMergingEmailClientManager();
				TPMailMessageDTO tpmailMessageDTO = mailMergingEmailClientManager.MailMergeFromTemplateInWebSettings(MailMergeContextWithCustomDictionary, EmailTemplateSetting);
				bool flag4 = !tpmailMessageDTO.IsActive;
				if (flag4)
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

		// Token: 0x0600037A RID: 890 RVA: 0x0000F49C File Offset: 0x0000D69C
		public SendEmailsResp SendEmail(int templateId, MailMergeContextWithCustomDictionaryDTO MailMergeContextWithCustomDictionary, string ContextForLogging = "")
		{
			SendEmailsResp result;
			try
			{
				bool flag = MailMergeContextWithCustomDictionary.CustomDictionary == null;
				if (flag)
				{
					MailMergeContextWithCustomDictionary.CustomDictionary = new MailMergeCustomDictionaryDTO();
				}
				bool flag2 = MailMergeContextWithCustomDictionary.CustomDictionary.Args == null;
				if (flag2)
				{
					MailMergeContextWithCustomDictionary.CustomDictionary.Args = new Dictionary<string, string>();
				}
				Dictionary<string, string> args = MailMergeContextWithCustomDictionary.CustomDictionary.Args;
				Dictionary<string, string> baseApplicationMailMergeValues = this.GetBaseApplicationMailMergeValues();
				foreach (KeyValuePair<string, string> keyValuePair in baseApplicationMailMergeValues)
				{
					bool flag3 = !string.IsNullOrEmpty(keyValuePair.Value) && !args.ContainsKey(keyValuePair.Key);
					if (flag3)
					{
						args.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
				IMailMergingEmailClientManager mailMergingEmailClientManager = new MailMergingEmailClientManager();
				TPMailMessageDTO tpmailMessageDTO = mailMergingEmailClientManager.MailMergeFromTemplate(MailMergeContextWithCustomDictionary, templateId);
				bool flag4 = !tpmailMessageDTO.IsActive;
				if (flag4)
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

		// Token: 0x0600037B RID: 891 RVA: 0x0000F650 File Offset: 0x0000D850
		public SendEmailsResp SendEmail(string EmailTemplateSettingXml, MailMergeContextWithCustomDictionaryDTO MailMergeContextWithCustomDictionary, string ContextForLogging = "")
		{
			SendEmailsResp result;
			try
			{
				bool flag = MailMergeContextWithCustomDictionary.CustomDictionary == null;
				if (flag)
				{
					MailMergeContextWithCustomDictionary.CustomDictionary = new MailMergeCustomDictionaryDTO();
				}
				bool flag2 = MailMergeContextWithCustomDictionary.CustomDictionary.Args == null;
				if (flag2)
				{
					MailMergeContextWithCustomDictionary.CustomDictionary.Args = new Dictionary<string, string>();
				}
				Dictionary<string, string> args = MailMergeContextWithCustomDictionary.CustomDictionary.Args;
				Dictionary<string, string> baseApplicationMailMergeValues = this.GetBaseApplicationMailMergeValues();
				foreach (KeyValuePair<string, string> keyValuePair in baseApplicationMailMergeValues)
				{
					bool flag3 = !string.IsNullOrEmpty(keyValuePair.Value) && !args.ContainsKey(keyValuePair.Key);
					if (flag3)
					{
						args.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
				IMailMergingEmailClientManager mailMergingEmailClientManager = new MailMergingEmailClientManager();
				TPMailMessageDTO tpmailMessageDTO = mailMergingEmailClientManager.MailMergeFromTemplateXml(MailMergeContextWithCustomDictionary, EmailTemplateSettingXml);
				bool flag4 = !tpmailMessageDTO.IsActive;
				if (flag4)
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

		// Token: 0x0600037C RID: 892 RVA: 0x0000F804 File Offset: 0x0000DA04
		public SendEmailsResp SendEmail(int templateId, MailMergeContextDTO MailMergeContext, StringDictionary Args, string ContextForLogging = "")
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (object obj in (Args ?? new StringDictionary()))
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = dictionaryEntry.Key.ToString();
				bool flag = !string.IsNullOrEmpty(text) && !dictionary.ContainsKey(text);
				if (flag)
				{
					Dictionary<string, string> dictionary2 = dictionary;
					string key = text;
					object value = dictionaryEntry.Value;
					dictionary2.Add(key, ((value != null) ? value.ToString() : null) ?? "");
				}
			}
			return this.SendEmail(templateId, new MailMergeContextWithCustomDictionaryDTO
			{
				Context = MailMergeContext,
				CustomDictionary = new MailMergeCustomDictionaryDTO
				{
					Args = dictionary
				}
			}, ContextForLogging);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000F8E4 File Offset: 0x0000DAE4
		public SendEmailsResp SendEmail(Setting EmailTemplateSetting, MailMergeContextDTO MailMergeContext, StringDictionary Args, string ContextForLogging = "")
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (object obj in (Args ?? new StringDictionary()))
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = dictionaryEntry.Key.ToString();
				bool flag = !string.IsNullOrEmpty(text) && !dictionary.ContainsKey(text);
				if (flag)
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

		// Token: 0x0600037E RID: 894 RVA: 0x0000F9C4 File Offset: 0x0000DBC4
		private Dictionary<string, string> GetBaseApplicationMailMergeValues()
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			Dictionary<string, string> dictionary = (Dictionary<string, string>)clientCache["webBaseApplicationMailMergeValues"];
			bool flag = dictionary != null;
			Dictionary<string, string> result;
			if (flag)
			{
				result = dictionary;
			}
			else
			{
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
				result = dictionary;
			}
			return result;
		}
	}
}
