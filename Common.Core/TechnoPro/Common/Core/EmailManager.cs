using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ClockWorkLogger;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.Core.AppointmentsPointOfContact;
using TechnoPro.Common.Core.Emailing;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.Email;
using TechnoPro.Common.DAO.MailBeeEmail;
using TechnoPro.Common.ICore;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.AppointmentsPointOfContact;
using TechnoPro.Common.ICore.Emailing;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsPointOfContact;
using TechnoPro.Common.Public.Entities.Emailing;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.TPMailMan;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Core
{
	// Token: 0x0200001D RID: 29
	public class EmailManager : IEmailManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00004F6B File Offset: 0x0000316B
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00004F73 File Offset: 0x00003173
		public IEmailDAO dao { get; set; }

		// Token: 0x060000D8 RID: 216 RVA: 0x00004F7C File Offset: 0x0000317C
		public EmailManager()
		{
			this.dao = new EmailDAO();
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004F92 File Offset: 0x00003192
		public EmailManager(OperationContext opContext)
		{
			this.dao = new EmailDAO(opContext);
			this.OpContext = opContext;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004FB4 File Offset: 0x000031B4
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00004FCC File Offset: 0x000031CC
		public OperationContext OpContext
		{
			get
			{
				return this._opContext;
			}
			set
			{
				this._opContext = value;
				this.dao.OpContext = this._opContext;
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004FE8 File Offset: 0x000031E8
		private static bool IsEmailValid(string email)
		{
			bool flag = string.IsNullOrEmpty(email);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Regex regex = new Regex("(?<user>[^@]+)@(?<host>.+)");
				Match match = regex.Match(email);
				result = match.Success;
			}
			return result;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005024 File Offset: 0x00003224
		private TPSmtpClient GetSmtpSettings()
		{
			OldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			List<OldUserSetting> list = oldUserSettingManager.LoadAllUserSettings(this.OpContext.WhoAmI);
			OldUserSetting oldUserSetting = list.Find((OldUserSetting s) => s.SettingCode == eSettingCode.SETTING_SMTP_SETTINGS);
			bool flag = oldUserSetting != null;
			if (flag)
			{
				TPSmtpClient tpsmtpClient = this.dao.ParseSettings(oldUserSetting.StringVal ?? "");
				bool flag2 = tpsmtpClient != null && !string.IsNullOrEmpty(tpsmtpClient.Server);
				if (flag2)
				{
					return tpsmtpClient;
				}
			}
			CWLogger.Logger.Debug("Common.Core.EmailManager.GetSmtpSettings:Using legacy settings because can't find SETTING_SMTP_SETTINGS value");
			return this.GetSmtpSettingsLegacy();
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000050DC File Offset: 0x000032DC
		private TPSmtpClient GetSmtpSettingsLegacy()
		{
			TPSmtpClient result;
			try
			{
				OldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
				List<OldUserSetting> list = oldUserSettingManager.LoadAllUserSettings(this.OpContext.WhoAmI);
				OldUserSetting oldUserSetting = list.Find((OldUserSetting s) => s.SettingCode == eSettingCode.SETTING_EmailOutgoingSmtpServer);
				string server = (oldUserSetting != null) ? oldUserSetting.StringVal : string.Empty;
				oldUserSetting = list.Find((OldUserSetting s) => s.SettingCode == eSettingCode.SETTING_EmailOutgoingSmtpPort);
				int port = (oldUserSetting != null) ? oldUserSetting.IntVal : 25;
				oldUserSetting = list.Find((OldUserSetting s) => s.SettingCode == eSettingCode.SETTING_EmailUseSSL);
				bool flag = oldUserSetting != null;
				int num;
				if (flag)
				{
					bool flag2 = oldUserSetting.IntVal != 0;
					if (flag2)
					{
						num = oldUserSetting.IntVal;
					}
					else
					{
						string text = (oldUserSetting.StringVal ?? "").ToLower().Trim();
						bool flag3 = !int.TryParse(text, out num);
						if (flag3)
						{
							num = (("yes1true".IndexOf(text) >= 0) ? 1 : 0);
						}
					}
				}
				else
				{
					num = 0;
				}
				oldUserSetting = list.Find((OldUserSetting s) => s.SettingCode == eSettingCode.SETTING_EmailUserName);
				string username = (oldUserSetting != null) ? oldUserSetting.StringVal : string.Empty;
				oldUserSetting = list.Find((OldUserSetting s) => s.SettingCode == eSettingCode.SETTING_EmailUserPassword);
				string password = (oldUserSetting != null) ? oldUserSetting.StringVal : string.Empty;
				bool flag4 = num == 0;
				eSslProtocol sslProtocol;
				if (flag4)
				{
					sslProtocol = eSslProtocol.None;
				}
				else
				{
					sslProtocol = (eSslProtocol)(Enum.IsDefined(typeof(eSslProtocol), num) ? num : 1);
				}
				result = new TPSmtpClient
				{
					Server = server,
					Port = port,
					SslProtocol = sslProtocol,
					Username = username,
					Password = password
				};
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("EmailManager::GetSmtpSettings:: {0}", ex.ToString()), ex);
				result = null;
			}
			return result;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005324 File Offset: 0x00003524
		public TPMailResult SendEmail(string to, string from, string subject, string bodytext, string bodyhtml = null, string cc = null, string bcc = null, string attachments = null)
		{
			TPSmtpClient smtpSettings = this.GetSmtpSettings();
			return this.dao.SendEmail(smtpSettings, this.GetDefaultFromAddress(), to, from, subject, bodytext, bodyhtml, cc, bcc, attachments);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000535C File Offset: 0x0000355C
		[DebuggerStepThrough]
		public Task<TPMailResult> SendEmailAsync(string to, string from, string subject, string bodytext, string bodyhtml = null, string cc = null, string bcc = null, string attachments = null)
		{
			EmailManager.<SendEmailAsync>d__14 <SendEmailAsync>d__ = new EmailManager.<SendEmailAsync>d__14();
			<SendEmailAsync>d__.<>t__builder = AsyncTaskMethodBuilder<TPMailResult>.Create();
			<SendEmailAsync>d__.<>4__this = this;
			<SendEmailAsync>d__.to = to;
			<SendEmailAsync>d__.from = from;
			<SendEmailAsync>d__.subject = subject;
			<SendEmailAsync>d__.bodytext = bodytext;
			<SendEmailAsync>d__.bodyhtml = bodyhtml;
			<SendEmailAsync>d__.cc = cc;
			<SendEmailAsync>d__.bcc = bcc;
			<SendEmailAsync>d__.attachments = attachments;
			<SendEmailAsync>d__.<>1__state = -1;
			<SendEmailAsync>d__.<>t__builder.Start<EmailManager.<SendEmailAsync>d__14>(ref <SendEmailAsync>d__);
			return <SendEmailAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000053E0 File Offset: 0x000035E0
		[DebuggerStepThrough]
		public Task<TPMailResult> SendEmailAsync(TPSmtpClient SmtpSettings, TPMailMessage Message)
		{
			EmailManager.<SendEmailAsync>d__15 <SendEmailAsync>d__ = new EmailManager.<SendEmailAsync>d__15();
			<SendEmailAsync>d__.<>t__builder = AsyncTaskMethodBuilder<TPMailResult>.Create();
			<SendEmailAsync>d__.<>4__this = this;
			<SendEmailAsync>d__.SmtpSettings = SmtpSettings;
			<SendEmailAsync>d__.Message = Message;
			<SendEmailAsync>d__.<>1__state = -1;
			<SendEmailAsync>d__.<>t__builder.Start<EmailManager.<SendEmailAsync>d__15>(ref <SendEmailAsync>d__);
			return <SendEmailAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005434 File Offset: 0x00003634
		public IList<TPMailResult> SendEmails(IDictionary<MailMergeContext, TPMailMessage> messages, BatchEmailSendParameters parameters)
		{
			Dictionary<TPMailMessage, TPMailResult> dictionary = new Dictionary<TPMailMessage, TPMailResult>();
			IPointOfContactManager pointOfContactManager = new PointOfContactManager(this.OpContext);
			foreach (KeyValuePair<MailMergeContext, TPMailMessage> keyValuePair in messages)
			{
				TPMailMessage value = keyValuePair.Value;
				MailMergeContext key = keyValuePair.Key;
				bool testMode = parameters.TestMode;
				if (testMode)
				{
					TPMailMessage tpmailMessage = value;
					string str = "Sent in test mode - would have sent to: ";
					string str2;
					if (value.To == null)
					{
						str2 = "";
					}
					else
					{
						str2 = string.Join(", ", value.To.ConvertAll<string>((TPMailAddress g) => g.EmailAddress ?? "NULL").ToArray());
					}
					tpmailMessage.Body = str + str2 + value.Body;
					value.To = new List<TPMailAddress>
					{
						new TPMailAddress
						{
							EmailAddress = parameters.AdminEmail
						}
					};
					value.Subject = "TEST MODE: " + value.Subject;
				}
				TPMailResult tpmailResult = this.SendEmail(value);
				dictionary.Add(value, tpmailResult);
				bool flag = key.PersonId > 0;
				if (flag)
				{
					pointOfContactManager.SaveEmailAsPointOfContact(false, key.PersonId, this.OpContext.WhoAmI, value, ePointOfContactContext.AutomaticSystemCreated);
				}
				bool flag2 = !string.IsNullOrEmpty(parameters.EmailTypeCode);
				if (flag2)
				{
					IEmailHistoryLoggerManager emailHistoryLoggerManager = new EmailHistoryLoggerManager(this.OpContext);
					emailHistoryLoggerManager.LogItem(new EmailHistoryLoggerItem
					{
						HistoryCode = parameters.EmailTypeCode,
						PersonId = key.PersonId,
						LuCourseId = key.LuCourseId,
						SentByPersonId = this.OpContext.WhoAmI,
						TemplateId = parameters.EmailTemplateId,
						EmailMessage = value.ConvertToDisplayString(),
						WasSuccessfullySent = (tpmailResult.Status == eTPMailResultStatus.CompletedSuccess || tpmailResult.Status == eTPMailResultStatus.CompletedWithWarnings),
						Note = (tpmailResult.ErrorMessage ?? "")
					});
				}
				bool flag3 = parameters.AppIconEmailSent >= 0 && key.AppointmentId > 0;
				if (flag3)
				{
					IAppointmentIconManager appointmentIconManager = new AppointmentIconManager(this.OpContext);
					appointmentIconManager.InsertOrUpdateAppointmentIcon(false, key.AppointmentId, new AppointmentIcon
					{
						Icon = new IconInfo
						{
							IconNum = parameters.AppIconEmailSent
						}
					});
				}
				bool flag4 = parameters.EmailDelay > 0;
				if (flag4)
				{
					Thread.Sleep(parameters.EmailDelay);
				}
			}
			bool sendReport = parameters.SendReport;
			if (sendReport)
			{
				int num = dictionary.Count((KeyValuePair<TPMailMessage, TPMailResult> g) => g.Value.Status == eTPMailResultStatus.CompletedSuccess || g.Value.Status == eTPMailResultStatus.CompletedWithWarnings);
				int num2 = dictionary.Count - num;
				string text = string.Format("Batch email report for '{0}' ({1}) Fail count={2}\n", parameters.Title ?? "", parameters.EmailTypeCode ?? "", num2);
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(text);
				stringBuilder.Append(string.Format("Successfully sent {0}\n", num.ToString()));
				stringBuilder.Append("\n");
				foreach (KeyValuePair<TPMailMessage, TPMailResult> keyValuePair2 in dictionary)
				{
					stringBuilder.Append("===================\n");
					stringBuilder.Append(string.Format("Successful: {0}\n", keyValuePair2.Value.Status.ToString()));
					bool flag5 = !string.IsNullOrEmpty(keyValuePair2.Value.ErrorMessage);
					if (flag5)
					{
						stringBuilder.Append(string.Format("Error: {0}\n", keyValuePair2.Value.ErrorMessage ?? ""));
					}
					stringBuilder.Append(keyValuePair2.Key.ConvertToDisplayString());
					stringBuilder.Append("\n\n");
				}
				string text2 = parameters.AdminEmail;
				bool flag6 = !EmailManager.IsEmailValid(text2);
				if (flag6)
				{
					text2 = this.GetDefaultFromAddress();
				}
				this.SendEmail(text2, text2, text, stringBuilder.ToString(), null, null, null, null);
			}
			return dictionary.Values.ToList<TPMailResult>();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000058B8 File Offset: 0x00003AB8
		public EmailListSendCompletedInfo SendEmailsReturnResult(params TPMailMessage[] messages)
		{
			IList<TPMailMessage> list = this.SendEmail(messages.ToArray<TPMailMessage>());
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (TPMailMessage tpmailMessage in from email in list
			where !email.WasSent
			select email)
			{
				num++;
				stringBuilder.AppendLine(tpmailMessage.ErrorMessage);
			}
			TPMailResult sendEmailResult = new TPMailResult
			{
				Status = ((num > 0) ? ((num == list.Count) ? eTPMailResultStatus.Failed : eTPMailResultStatus.CompletedWithWarnings) : eTPMailResultStatus.CompletedSuccess),
				ErrorMessage = ((num > 0) ? stringBuilder.ToString() : null)
			};
			return new EmailListSendCompletedInfo
			{
				MailMessages = list.ToList<TPMailMessage>(),
				SendEmailResult = sendEmailResult
			};
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000059A4 File Offset: 0x00003BA4
		public TPMailResult SendEmail(TPMailMessage message)
		{
			IList<TPMailMessage> list = this.SendEmail(new TPMailMessage[]
			{
				message
			});
			bool flag = list == null || list.Count < 1;
			TPMailResult result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new TPMailResult
				{
					ErrorMessage = list[0].ErrorMessage,
					Status = (list[0].WasSent ? eTPMailResultStatus.CompletedSuccess : eTPMailResultStatus.Failed)
				};
			}
			return result;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005A10 File Offset: 0x00003C10
		[DebuggerStepThrough]
		public Task<TPMailResult> SendEmailAsync(TPMailMessage message)
		{
			EmailManager.<SendEmailAsync>d__19 <SendEmailAsync>d__ = new EmailManager.<SendEmailAsync>d__19();
			<SendEmailAsync>d__.<>t__builder = AsyncTaskMethodBuilder<TPMailResult>.Create();
			<SendEmailAsync>d__.<>4__this = this;
			<SendEmailAsync>d__.message = message;
			<SendEmailAsync>d__.<>1__state = -1;
			<SendEmailAsync>d__.<>t__builder.Start<EmailManager.<SendEmailAsync>d__19>(ref <SendEmailAsync>d__);
			return <SendEmailAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005A5C File Offset: 0x00003C5C
		[DebuggerStepThrough]
		public Task<IList<TPMailMessage>> SendEmailAsync(params TPMailMessage[] messages)
		{
			EmailManager.<SendEmailAsync>d__20 <SendEmailAsync>d__ = new EmailManager.<SendEmailAsync>d__20();
			<SendEmailAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<TPMailMessage>>.Create();
			<SendEmailAsync>d__.<>4__this = this;
			<SendEmailAsync>d__.messages = messages;
			<SendEmailAsync>d__.<>1__state = -1;
			<SendEmailAsync>d__.<>t__builder.Start<EmailManager.<SendEmailAsync>d__20>(ref <SendEmailAsync>d__);
			return <SendEmailAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005AA8 File Offset: 0x00003CA8
		public IList<TPMailMessage> SendEmail(params TPMailMessage[] messages)
		{
			TPSmtpClient smtpSettings = this.GetSmtpSettings();
			return this.dao.SendEmails(smtpSettings, this.GetDefaultFromAddress(), messages);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005AD4 File Offset: 0x00003CD4
		public string GetDefaultFromAddress()
		{
			OldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			string settingValue_String = oldUserSettingManager.GetSettingValue_String(this.OpContext.WhoAmI, eSettingCode.SETTING_EmailDefaultFromAddress);
			bool flag = !string.IsNullOrEmpty(settingValue_String);
			string result;
			if (flag)
			{
				result = settingValue_String;
			}
			else
			{
				SettingManager settingManager = new SettingManager(this.OpContext);
				Setting[] array = new Setting[12];
				RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.AC0819B50817CA6F98FFD7C584C677D4781F512205AAD7535053EB66B0577A5A).FieldHandle);
				Setting[] array2 = array;
				foreach (Setting setting in array2)
				{
					string settingValue = settingManager.GetSettingValue<string>(setting);
					bool flag2 = !string.IsNullOrEmpty(settingValue);
					if (flag2)
					{
						return settingValue;
					}
				}
				StaffCommonInfoManager staffCommonInfoManager = new StaffCommonInfoManager(this.OpContext);
				result = (staffCommonInfoManager.LoadStaffEmail(this.OpContext.WhoAmI) ?? "");
			}
			return result;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005BA8 File Offset: 0x00003DA8
		public TPMailResult SendEmail(TPSmtpClient SmtpSettings, TPMailMessage Message)
		{
			IList<TPMailMessage> list = this.dao.SendEmails(SmtpSettings, this.GetDefaultFromAddress(), new TPMailMessage[]
			{
				Message
			});
			bool flag = list == null || list.Count < 1;
			TPMailResult result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new TPMailResult
				{
					ErrorMessage = list[0].ErrorMessage,
					Status = (list[0].WasSent ? eTPMailResultStatus.CompletedSuccess : eTPMailResultStatus.Failed)
				};
			}
			return result;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005C20 File Offset: 0x00003E20
		public EmailListSendCompletedInfo SendEmailsReturnResult(IList<TPMailMessage> MailMessages, string emailTestModeAddress, string ContextForLogging = "")
		{
			List<TPMailMessage> list = new List<TPMailMessage>();
			foreach (TPMailMessage tpmailMessage in MailMessages)
			{
				bool flag = !tpmailMessage.IsActive;
				if (!flag)
				{
					bool flag2 = !string.IsNullOrEmpty(emailTestModeAddress);
					if (flag2)
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.Append("* Test Mode *\nThis email was redirected to your email address because of the setting in the 'ExternalAppSetings.config' file on the website.  The original intended recipients were:\n");
						stringBuilder.Append("To: " + tpmailMessage.To.GetEmailList() + "\n");
						List<TPMailAddress> cc = tpmailMessage.Cc;
						object obj;
						if (cc == null)
						{
							obj = null;
						}
						else
						{
							obj = cc.FirstOrDefault((TPMailAddress g) => !string.IsNullOrEmpty(g.EmailAddress));
						}
						bool flag3 = obj != null;
						if (flag3)
						{
							foreach (TPMailAddress tpmailAddress in tpmailMessage.Cc)
							{
								stringBuilder.Append("Cc: " + tpmailAddress.EmailAddress + "\n");
							}
						}
						List<TPMailAddress> bcc = tpmailMessage.Bcc;
						object obj2;
						if (bcc == null)
						{
							obj2 = null;
						}
						else
						{
							obj2 = bcc.FirstOrDefault((TPMailAddress g) => !string.IsNullOrEmpty(g.EmailAddress));
						}
						bool flag4 = obj2 != null;
						if (flag4)
						{
							foreach (TPMailAddress tpmailAddress2 in tpmailMessage.Bcc)
							{
								stringBuilder.Append("Bcc: " + tpmailAddress2.EmailAddress + "\n");
							}
						}
						stringBuilder.Append("\n");
						bool flag5 = !string.IsNullOrEmpty(tpmailMessage.Body);
						if (flag5)
						{
							tpmailMessage.Body = stringBuilder.ToString() + tpmailMessage.Body;
						}
						bool flag6 = !string.IsNullOrEmpty(tpmailMessage.BodyHtml);
						if (flag6)
						{
							tpmailMessage.BodyHtml = stringBuilder.ToString().Replace("\n", "\n<br />") + tpmailMessage.BodyHtml;
						}
						List<TPMailAddress> cc2 = tpmailMessage.Cc;
						if (cc2 != null)
						{
							cc2.Clear();
						}
						List<TPMailAddress> bcc2 = tpmailMessage.Bcc;
						if (bcc2 != null)
						{
							bcc2.Clear();
						}
						tpmailMessage.To = new List<TPMailAddress>
						{
							new TPMailAddress
							{
								EmailAddress = emailTestModeAddress
							}
						};
					}
					list.Add(tpmailMessage);
				}
			}
			bool flag7 = list.Count < 1;
			EmailListSendCompletedInfo result;
			if (flag7)
			{
				result = null;
			}
			else
			{
				EmailListSendCompletedInfo emailListSendCompletedInfo = this.SendEmailsReturnResult(list.ToArray());
				bool flag8 = list != null;
				if (flag8)
				{
					foreach (TPMailMessage tpmailMessage2 in list)
					{
						CWLogger logger = CWLogger.Logger;
						string message = "SendEmails:{0}:{1}:{2}";
						object arg = ContextForLogging ?? "";
						object obj3;
						if (emailListSendCompletedInfo == null)
						{
							obj3 = null;
						}
						else
						{
							TPMailResult sendEmailResult = emailListSendCompletedInfo.SendEmailResult;
							obj3 = ((sendEmailResult != null) ? sendEmailResult.Status.ToString() : null);
						}
						logger.Trace(message, arg, obj3 ?? "NULL", (tpmailMessage2 == null) ? "NULL" : tpmailMessage2.ToEmailXml());
					}
				}
				result = emailListSendCompletedInfo;
			}
			return result;
		}

		// Token: 0x0400003E RID: 62
		private OperationContext _opContext;
	}
}
