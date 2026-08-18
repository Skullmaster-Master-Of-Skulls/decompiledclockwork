using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ClockWorkLogger;
using Databases;
using MailBee;
using MailBee.Mime;
using MailBee.Security;
using MailBee.SmtpMail;
using TechnoPro.Common.DAO.Email;
using TechnoPro.Common.DAO.Impl.Email;
using TechnoPro.Common.DAO.MailBeeEmail.Adapters;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.TPMailMan;
using TechnoPro.Common.TextFormat.Adapters;

namespace TechnoPro.Common.DAO.MailBeeEmail
{
	// Token: 0x02000002 RID: 2
	public class EmailDAO : IEmailDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public EmailDAO()
		{
			Global.LicenseKey = "MN110-F830CF1F3121300630C4CF8A298D-D445";
			Global.FipsMode = true;
			this.OpContext = new OperationContext
			{
				WhoAmI = 1
			};
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000207A File Offset: 0x0000027A
		public EmailDAO(OperationContext opContext)
		{
			Global.LicenseKey = "MN110-F830CF1F3121300630C4CF8A298D-D445";
			Global.FipsMode = true;
			this.OpContext = opContext;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002099 File Offset: 0x00000299
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020A1 File Offset: 0x000002A1
		public OperationContext OpContext { get; set; }

		// Token: 0x06000005 RID: 5 RVA: 0x000020AC File Offset: 0x000002AC
		private AuthenticationMethods? ParseAuthenticationMethods(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return null;
			}
			string[] array = s.Split(new char[]
			{
				','
			});
			AuthenticationMethods? result = null;
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i].Trim();
				if (text.Length > 0 && Enum.IsDefined(typeof(AuthenticationMethods), s))
				{
					AuthenticationMethods authenticationMethods = (AuthenticationMethods)Enum.Parse(typeof(AuthenticationMethods), text);
					if (result == null)
					{
						result = new AuthenticationMethods?(authenticationMethods);
					}
					else
					{
						result = new AuthenticationMethods?(result.Value | authenticationMethods);
					}
				}
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002158 File Offset: 0x00000358
		private AuthenticationOptions? ParseAuthenticationOptions(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return null;
			}
			string[] array = s.Split(new char[]
			{
				','
			});
			AuthenticationOptions? result = null;
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i].Trim();
				if (text.Length > 0 && Enum.IsDefined(typeof(AuthenticationOptions), s))
				{
					AuthenticationOptions authenticationOptions = (AuthenticationOptions)Enum.Parse(typeof(AuthenticationOptions), text);
					if (result == null)
					{
						result = new AuthenticationOptions?(authenticationOptions);
					}
					else
					{
						result = new AuthenticationOptions?(result.Value | authenticationOptions);
					}
				}
			}
			return result;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002204 File Offset: 0x00000404
		private Smtp GetMailer(TPSmtpClient smtpSettings)
		{
			string username = smtpSettings.Username;
			string password = smtpSettings.Password;
			if (smtpSettings != null && smtpSettings.EnableNonFipsAlgorithms)
			{
				Global.FipsMode = false;
			}
			AuthenticationMethods? authenticationMethods = this.ParseAuthenticationMethods(smtpSettings.AuthenticationMethods);
			if (authenticationMethods == null)
			{
				authenticationMethods = new AuthenticationMethods?((username == null || username.Trim().Length < 1) ? AuthenticationMethods.None : AuthenticationMethods.Auto);
			}
			AuthenticationOptions? authenticationOptions = this.ParseAuthenticationOptions(smtpSettings.AuthenticationOptions);
			if (authenticationOptions == null)
			{
				authenticationOptions = new AuthenticationOptions?(AuthenticationOptions.TryUnsupportedMethods);
			}
			string sslStartupMode = smtpSettings.SslStartupMode;
			SslStartupMode sslMode;
			if (!string.IsNullOrEmpty(sslStartupMode) && Enum.IsDefined(typeof(SslStartupMode), sslStartupMode))
			{
				sslMode = (SslStartupMode)Enum.Parse(typeof(SslStartupMode), sslStartupMode);
			}
			else
			{
				sslMode = ((smtpSettings.SslProtocol == eSslProtocol.None) ? SslStartupMode.Manual : SslStartupMode.OnConnect);
			}
			Smtp smtp = new Smtp();
			SmtpServer smtpServer = new SmtpServer(smtpSettings.Server, username, password, authenticationMethods.Value)
			{
				Port = smtpSettings.Port,
				SslMode = sslMode,
				AuthOptions = authenticationOptions.Value
			};
			switch (smtpSettings.SslProtocol)
			{
			case eSslProtocol.Auto:
				smtpServer.SslProtocol = SecurityProtocol.Auto;
				break;
			case eSslProtocol.Ssl2:
				smtpServer.SslProtocol = SecurityProtocol.Ssl2;
				break;
			case eSslProtocol.Ssl3:
				smtpServer.SslProtocol = SecurityProtocol.Ssl3;
				break;
			case eSslProtocol.Tls:
			case eSslProtocol.Tls1:
				smtpServer.SslProtocol = SecurityProtocol.Tls1;
				break;
			case eSslProtocol.Tls11:
				smtpServer.SslProtocol = SecurityProtocol.Tls11;
				break;
			case eSslProtocol.Tls12:
				smtpServer.SslProtocol = SecurityProtocol.Tls12;
				break;
			case eSslProtocol.TlsAuto:
				smtpServer.SslProtocol = SecurityProtocol.TlsAuto;
				break;
			}
			if (smtpSettings.ServerTimeoutSeconds > 0)
			{
				smtpServer.Timeout = smtpSettings.ServerTimeoutSeconds * 1000;
			}
			if ((smtpSettings.ExtendedSmtpOptions & eExtendedSmtpOptions.NoChunking) > eExtendedSmtpOptions.Unknown)
			{
				smtpServer.SmtpOptions |= ExtendedSmtpOptions.NoChunking;
			}
			string text = (smtpSettings.HelloDomain ?? "").Trim();
			if (text.Equals("{ComputerName}", StringComparison.OrdinalIgnoreCase))
			{
				text = Environment.MachineName;
			}
			if (text.Length > 0)
			{
				smtpServer.HelloDomain = text;
			}
			smtp.SmtpServers.Add(smtpServer);
			return smtp;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000241C File Offset: 0x0000061C
		private IList<TPMailAddress> ExtractDistinctEmails(IList<TPMailAddress> emailAddresses)
		{
			List<TPMailAddress> list = new List<TPMailAddress>();
			if (emailAddresses == null)
			{
				return list;
			}
			foreach (TPMailAddress address in emailAddresses)
			{
				IList<TPMailAddress> list2 = this.ExtractEmails(address);
				if (list2 != null && list2.Count >= 1)
				{
					list.AddRange(list2);
				}
			}
			return list;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002484 File Offset: 0x00000684
		private IList<TPMailAddress> ExtractEmails(TPMailAddress address)
		{
			TPMailAddress address2 = address;
			if (((address2 != null) ? address2.EmailAddress : null) == null || address.EmailAddress.Trim().Length < 1)
			{
				return new List<TPMailAddress>();
			}
			address.EmailAddress = address.EmailAddress.UnEscapeXml();
			if (!address.EmailAddress.Contains(',') && !address.EmailAddress.Contains(';'))
			{
				return new List<TPMailAddress>
				{
					address
				};
			}
			return (from g in address.EmailAddress.Replace(';', ',').Split(new char[]
			{
				','
			})
			select g.Trim() into h
			where h.Length > 0
			select h into m
			select new TPMailAddress
			{
				EmailAddress = m,
				Id = address.Id
			}).ToList<TPMailAddress>();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000025A8 File Offset: 0x000007A8
		private static void SetFromAddress(ref Smtp mailer, TPMailAddress fromAddressSource, string defaultEmailAddress)
		{
			string displayName = string.IsNullOrEmpty((fromAddressSource != null) ? fromAddressSource.EmailAddress : null) ? "" : (fromAddressSource.Id ?? "").Trim();
			string emailAddress = string.IsNullOrEmpty((fromAddressSource != null) ? fromAddressSource.EmailAddress : null) ? (defaultEmailAddress ?? "").Trim() : fromAddressSource.EmailAddress;
			EmailDAO.SetFromAddress(ref mailer, displayName, emailAddress, defaultEmailAddress);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002618 File Offset: 0x00000818
		private static void SetFromAddress(ref Smtp mailer, string displayName, string emailAddress, string defaultEmailAddress)
		{
			string text = (displayName ?? "").Trim();
			string text2 = (emailAddress ?? "").Trim();
			if (text2.Length < 1)
			{
				text2 = (defaultEmailAddress ?? "").Trim();
			}
			if (text.Length > 0)
			{
				mailer.From.DisplayName = text;
				mailer.From.Email = text2;
				return;
			}
			EmailDAO.EmailAddressWithDisplayName emailAddressWithDisplayName = new EmailDAO.EmailAddressWithDisplayName(text2);
			mailer.From.DisplayName = (emailAddressWithDisplayName.DisplayName ?? "");
			mailer.From.Email = (emailAddressWithDisplayName.EmailAddress ?? "");
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000026C0 File Offset: 0x000008C0
		private void SetupMessage(Smtp mailer, TPMailMessage message, string defaultEmailAddress)
		{
			mailer.Message.From.DisplayName = "";
			mailer.Message.From.Email = "";
			mailer.Message.To.Clear();
			mailer.Message.Cc.Clear();
			mailer.Message.Bcc.Clear();
			mailer.Message.Subject = "";
			mailer.Message.Attachments.Clear();
			mailer.Message.BodyPlainText = "";
			mailer.Message.BodyHtmlText = "";
			EmailDAO.SetFromAddress(ref mailer, message.From, defaultEmailAddress);
			foreach (TPMailAddress tpmailAddress in this.ExtractDistinctEmails(message.To))
			{
				mailer.Message.To.Add(tpmailAddress.EmailAddress, tpmailAddress.Id);
			}
			foreach (TPMailAddress tpmailAddress2 in this.ExtractDistinctEmails(message.Cc))
			{
				mailer.Cc.Add(tpmailAddress2.EmailAddress, tpmailAddress2.Id);
			}
			foreach (TPMailAddress tpmailAddress3 in this.ExtractDistinctEmails(message.Bcc))
			{
				mailer.Bcc.Add(tpmailAddress3.EmailAddress, tpmailAddress3.Id);
			}
			mailer.Message.Subject = message.Subject;
			eEmailBodyType bodyType = message.BodyType;
			string text;
			string text2;
			if (bodyType != eEmailBodyType.PlainText)
			{
				if (bodyType != eEmailBodyType.Html)
				{
					text = (message.GetPlainTextBody() ?? "");
					text2 = (message.GetHtmlBody() ?? "");
					bool flag = !string.IsNullOrEmpty(text);
					bool flag2 = !string.IsNullOrEmpty(text2);
					if (!flag || !flag2)
					{
						if (flag)
						{
							text2 = text.Replace(Environment.NewLine, "<br />");
						}
						else if (flag2)
						{
							text = text2.ConvertHtmlToPlainText();
						}
					}
				}
				else
				{
					text2 = (message.GetHtmlBody() ?? "");
					if (string.IsNullOrEmpty(text2))
					{
						text2 = (message.Body ?? "");
					}
					text = text2.ConvertHtmlToPlainText();
				}
			}
			else
			{
				text = (message.GetPlainTextBody() ?? "");
				if (string.IsNullOrEmpty(text))
				{
					text = (message.BodyHtml ?? "");
				}
				text2 = text.Replace(Environment.NewLine, "<br />");
			}
			mailer.Message.BodyPlainText = text;
			if (message.DeliveryMethod == eTPMessageDeliveryMethod.Html || message.DeliveryMethod == eTPMessageDeliveryMethod.HtmlAndPlainText)
			{
				mailer.Message.BodyHtmlText = text2;
			}
			mailer.Message.Priority = message.Priority.ConvertToMailPriority();
			IList<TPMailAttachment> list = this.LookupAttachedFiles(message.Attachments);
			if (list != null && list.Count > 0)
			{
				foreach (TPMailAttachment tpmailAttachment in list)
				{
					mailer.Message.Attachments.Add(tpmailAttachment.FileBytes, tpmailAttachment.FileNameForDisplay, string.Empty, null, null, NewAttachmentOptions.ReplaceIfExists, MailTransferEncoding.None);
				}
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002A28 File Offset: 0x00000C28
		private void SetupMessage(Smtp mailer, string defaultEmailAddress, string to, string from, string subject, string bodytext, string bodyhtml = null, string cc = null, string bcc = null, string attachments = null)
		{
			EmailDAO.SetFromAddress(ref mailer, null, from, defaultEmailAddress);
			mailer.Message.To.AsString = to;
			if (!string.IsNullOrEmpty(cc))
			{
				mailer.Cc.AsString = cc;
			}
			if (!string.IsNullOrEmpty(bcc))
			{
				mailer.Bcc.AsString = bcc;
			}
			mailer.Message.Subject = subject;
			mailer.Message.BodyPlainText = bodytext;
			if (!string.IsNullOrEmpty(bodyhtml))
			{
				mailer.Message.BodyHtmlText = bodyhtml;
			}
			mailer.Message.Priority = MailPriority.Normal;
			foreach (TPMailAttachment tpmailAttachment in this.LookupAttachedFiles((from g in (attachments ?? "").Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries)
			select (g ?? "").Trim().GetAttachmentFromXml() into h
			where h != null && !string.IsNullOrEmpty(h.FileNameForDisplay)
			select h).ToList<TPMailAttachment>()))
			{
				mailer.Message.Attachments.Add(tpmailAttachment.FileBytes, Path.GetFileName(tpmailAttachment.FileNameForDisplay ?? ""), string.Empty, null, null, NewAttachmentOptions.ReplaceIfExists, MailTransferEncoding.None);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002B90 File Offset: 0x00000D90
		public TPMailResult SendEmail(TPSmtpClient smtpSettings, string defaultEmailAddress, string to, string from, string subject, string bodytext, string bodyhtml = null, string cc = null, string bcc = null, string attachments = null)
		{
			Smtp mailer = this.GetMailer(smtpSettings);
			TPMailResult result;
			try
			{
				this.SetupMessage(mailer, defaultEmailAddress, to, from, subject, bodytext, bodyhtml, cc, bcc, attachments);
				TPMailResult tpmailResult2;
				if (!mailer.Send())
				{
					TPMailResult tpmailResult = new TPMailResult();
					tpmailResult.Status = eTPMailResultStatus.Failed;
					tpmailResult.ErrorMessage = mailer.GetErrorDescription();
					tpmailResult2 = tpmailResult;
					tpmailResult.ErrorMessageHtml = string.Empty;
				}
				else
				{
					TPMailResult tpmailResult3 = new TPMailResult();
					tpmailResult3.Status = eTPMailResultStatus.CompletedSuccess;
					tpmailResult3.ErrorMessage = string.Empty;
					tpmailResult2 = tpmailResult3;
					tpmailResult3.ErrorMessageHtml = string.Empty;
				}
				result = tpmailResult2;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException("MailBee::EmailDAO:SendMail:smtpSettings=" + (((smtpSettings != null) ? smtpSettings.ToString() : null) ?? "NULL") + ":err=" + ex.ToString(), ex);
				result = new TPMailResult
				{
					Status = eTPMailResultStatus.Failed,
					ErrorMessage = ex.ToString(),
					ErrorMessageHtml = string.Empty
				};
			}
			return result;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002C7C File Offset: 0x00000E7C
		public Task<TPMailResult> SendEmailAsync(TPSmtpClient smtpSettings, string defaultEmailAddress, string to, string from, string subject, string bodytext, string bodyhtml = null, string cc = null, string bcc = null, string attachments = null)
		{
			Smtp mailer = this.GetMailer(smtpSettings);
			this.SetupMessage(mailer, defaultEmailAddress, to, from, subject, bodytext, bodyhtml, cc, bcc, attachments);
			return Task.Factory.FromAsync<string, EmailAddressCollection, TPMailResult>(new Func<string, EmailAddressCollection, AsyncCallback, object, IAsyncResult>(mailer.BeginSend), delegate(IAsyncResult result)
			{
				if (!mailer.EndSend())
				{
					return new TPMailResult
					{
						Status = eTPMailResultStatus.Failed,
						ErrorMessage = mailer.GetErrorDescription(),
						ErrorMessageHtml = string.Empty
					};
				}
				return new TPMailResult
				{
					Status = eTPMailResultStatus.CompletedSuccess,
					ErrorMessage = string.Empty,
					ErrorMessageHtml = string.Empty
				};
			}, mailer.Message.From.Email, mailer.Message.To, null).ContinueWith<TPMailResult>(delegate(Task<TPMailResult> task)
			{
				if (task.Status == TaskStatus.RanToCompletion)
				{
					if (task.Result.Status == eTPMailResultStatus.Failed)
					{
						CWLogger.Logger.Error("MailBee::EmailDAO:SendEmailAsync:smtpSettings={0}:err={1}", (smtpSettings == null) ? "NULL" : smtpSettings.ToString(), task.Result.ErrorMessage);
					}
					return task.Result;
				}
				if (task.Exception != null)
				{
					CWLogger logger = CWLogger.Logger;
					string format = "MailBee::EmailDAO:SendEmailAsync:smtpSettings={0}:err={1}";
					TPSmtpClient smtpSettings2 = smtpSettings;
					logger.ErrorException(string.Format(format, ((smtpSettings2 != null) ? smtpSettings2.ToString() : null) ?? "NULL", task.Exception.ToString()), task.Exception);
					return new TPMailResult
					{
						Status = eTPMailResultStatus.Failed,
						ErrorMessage = task.Exception.ToString(),
						ErrorMessageHtml = string.Empty
					};
				}
				return task.Result;
			});
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002D24 File Offset: 0x00000F24
		private IList<TPMailAttachment> LookupAttachedFiles(IList<TPMailAttachment> attachments)
		{
			CWLogger.Logger.Trace("EmailDAO::LookupAttachedFiles:: Attachments = " + (((attachments != null) ? attachments.Count.ToString() : null) ?? "NULL"));
			if (attachments == null || attachments.Count < 1)
			{
				return new List<TPMailAttachment>();
			}
			IEmailAttachmentDAO emailAttachmentDAO = new EmailAttachmentDAO(this.OpContext);
			List<TPMailAttachment> list = new List<TPMailAttachment>();
			foreach (TPMailAttachment tpmailAttachment in from a in attachments
			where a != null
			select a)
			{
				if ((tpmailAttachment.FileBytes == null || tpmailAttachment.FileBytes.Length < 1) && tpmailAttachment.FileAttachmentId > 0)
				{
					TPMailAttachment tpmailAttachment2 = emailAttachmentDAO.LoadAttachment(tpmailAttachment.FileAttachmentId);
					if (tpmailAttachment2 != null)
					{
						CWLogger logger = CWLogger.Logger;
						string format = "EmailDAO::LookupAttachedFiles:: After loading attachment: Id= {0}, FileBytes={1}, FileNameForDisplay={2}";
						object arg = tpmailAttachment2.FileAttachmentId;
						byte[] fileBytes = tpmailAttachment2.FileBytes;
						logger.Trace(string.Format(format, arg, ((fileBytes != null) ? fileBytes.Length.ToString() : null) ?? "NULL", tpmailAttachment2.FileNameForDisplay ?? "NULL"));
					}
					else
					{
						CWLogger.Logger.Trace(string.Format("EmailDAO::LookupAttachedFiles:: After loading attachment is NULL: Id= {0}", tpmailAttachment.FileAttachmentId));
					}
					if (tpmailAttachment2 != null)
					{
						list.Add(tpmailAttachment2);
					}
				}
				else
				{
					CWLogger logger2 = CWLogger.Logger;
					string format2 = "EmailDAO::LookupAttachedFiles:: Attachment: Id= {0}, FileBytes={1}, FileNameForDisplay={2}";
					object arg2 = tpmailAttachment.FileAttachmentId;
					byte[] fileBytes2 = tpmailAttachment.FileBytes;
					logger2.Trace(string.Format(format2, arg2, ((fileBytes2 != null) ? fileBytes2.Length.ToString() : null) ?? "NULL", tpmailAttachment.FileNameForDisplay ?? "NULL"));
					list.Add(tpmailAttachment);
				}
			}
			return list;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002F04 File Offset: 0x00001104
		public IList<TPMailMessage> SendEmails(TPSmtpClient smtpSettings, string defaultEmailAddress, params TPMailMessage[] mailMessages)
		{
			List<TPMailMessage> list = new List<TPMailMessage>();
			if (mailMessages == null)
			{
				return list;
			}
			Smtp mailer = this.GetMailer(smtpSettings);
			foreach (TPMailMessage tpmailMessage in mailMessages)
			{
				try
				{
					this.SetupMessage(mailer, tpmailMessage, defaultEmailAddress);
					if (mailer.Send())
					{
						tpmailMessage.WasSent = true;
						tpmailMessage.ErrorMessage = (tpmailMessage.ErrorMessageHtml = string.Empty);
					}
					else
					{
						tpmailMessage.WasSent = false;
						tpmailMessage.ErrorMessage = mailer.GetErrorDescription();
						tpmailMessage.ErrorMessageHtml = string.Empty;
					}
				}
				catch (Exception ex)
				{
					CWLogger.Logger.ErrorException(string.Format("MailBee::EmailDAO:SendMail2:smtpSettings={0}:err={1}", ((smtpSettings != null) ? smtpSettings.ToString() : null) ?? "NULL", ex.ToString()), ex);
					tpmailMessage.WasSent = false;
					tpmailMessage.ErrorMessage = ex.ToString();
					tpmailMessage.ErrorMessageHtml = string.Empty;
				}
				list.Add(tpmailMessage);
			}
			return list;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00003008 File Offset: 0x00001208
		public Task<IList<TPMailMessage>> SendEmailsAsync(TPSmtpClient smtpSettings, string defaultEmailAddress, params TPMailMessage[] mailMessages)
		{
			EmailDAO.<SendEmailsAsync>d__20 <SendEmailsAsync>d__;
			<SendEmailsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<TPMailMessage>>.Create();
			<SendEmailsAsync>d__.<>4__this = this;
			<SendEmailsAsync>d__.smtpSettings = smtpSettings;
			<SendEmailsAsync>d__.defaultEmailAddress = defaultEmailAddress;
			<SendEmailsAsync>d__.mailMessages = mailMessages;
			<SendEmailsAsync>d__.<>1__state = -1;
			<SendEmailsAsync>d__.<>t__builder.Start<EmailDAO.<SendEmailsAsync>d__20>(ref <SendEmailsAsync>d__);
			return <SendEmailsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00003063 File Offset: 0x00001263
		public TPSmtpClient ParseSettings(string xml)
		{
			string xml2 = xml ?? "";
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			return xml2.GetSmtpSettingsFromXml(DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null).Encryption);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00003091 File Offset: 0x00001291
		public string GetXmlFromSettings(TPSmtpClient smtpSettings)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			return smtpSettings.GetXmlFromSmtpSettings(DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null).Encryption);
		}

		// Token: 0x02000004 RID: 4
		internal class EmailAddressWithDisplayName
		{
			// Token: 0x06000016 RID: 22 RVA: 0x000030E8 File Offset: 0x000012E8
			public EmailAddressWithDisplayName(string emailAddressPossiblyWithDisplayName)
			{
				string text = (emailAddressPossiblyWithDisplayName ?? "").Trim();
				int num = text.IndexOf("<");
				int num2 = text.IndexOf(">");
				if (num < 0 || num2 < num)
				{
					this.EmailAddress = text;
					return;
				}
				this.DisplayName = ((num == 0) ? "" : text.Substring(0, num));
				this.EmailAddress = ((num2 == num + 1) ? "" : text.Substring(num + 1, num2 - num - 1));
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000017 RID: 23 RVA: 0x0000316C File Offset: 0x0000136C
			// (set) Token: 0x06000018 RID: 24 RVA: 0x00003174 File Offset: 0x00001374
			public string EmailAddress { get; set; }

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000019 RID: 25 RVA: 0x0000317D File Offset: 0x0000137D
			// (set) Token: 0x0600001A RID: 26 RVA: 0x00003185 File Offset: 0x00001385
			public string DisplayName { get; set; }
		}
	}
}
