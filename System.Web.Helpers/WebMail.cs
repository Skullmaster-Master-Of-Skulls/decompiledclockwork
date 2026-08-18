using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Helpers.Resources;
using System.Web.WebPages.Scope;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Helpers
{
	// Token: 0x0200002B RID: 43
	public static class WebMail
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000A29C File Offset: 0x0000849C
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0000A2A8 File Offset: 0x000084A8
		public static string SmtpServer
		{
			get
			{
				return WebMail.ReadValue<string>(WebMail.SmtpServerKey);
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "SmtpServer");
				}
				ScopeStorage.CurrentScope[WebMail.SmtpServerKey] = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000A2D2 File Offset: 0x000084D2
		// (set) Token: 0x06000215 RID: 533 RVA: 0x0000A2DE File Offset: 0x000084DE
		public static int SmtpPort
		{
			get
			{
				return WebMail.ReadValue<int>(WebMail.SmtpPortKey);
			}
			set
			{
				ScopeStorage.CurrentScope[WebMail.SmtpPortKey] = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000A2F5 File Offset: 0x000084F5
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000A301 File Offset: 0x00008501
		public static string From
		{
			get
			{
				return WebMail.ReadValue<string>(WebMail.FromKey);
			}
			set
			{
				ScopeStorage.CurrentScope[WebMail.FromKey] = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000A313 File Offset: 0x00008513
		// (set) Token: 0x06000219 RID: 537 RVA: 0x0000A31F File Offset: 0x0000851F
		public static bool SmtpUseDefaultCredentials
		{
			get
			{
				return WebMail.ReadValue<bool>(WebMail.SmtpUseDefaultCredentialsKey);
			}
			set
			{
				ScopeStorage.CurrentScope[WebMail.SmtpUseDefaultCredentialsKey] = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000A336 File Offset: 0x00008536
		// (set) Token: 0x0600021B RID: 539 RVA: 0x0000A342 File Offset: 0x00008542
		public static bool EnableSsl
		{
			get
			{
				return WebMail.ReadValue<bool>(WebMail.EnableSslKey);
			}
			set
			{
				ScopeStorage.CurrentScope[WebMail.EnableSslKey] = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000A359 File Offset: 0x00008559
		// (set) Token: 0x0600021D RID: 541 RVA: 0x0000A365 File Offset: 0x00008565
		public static string UserName
		{
			get
			{
				return WebMail.ReadValue<string>(WebMail.UserNameKey);
			}
			set
			{
				ScopeStorage.CurrentScope[WebMail.UserNameKey] = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000A377 File Offset: 0x00008577
		// (set) Token: 0x0600021F RID: 543 RVA: 0x0000A383 File Offset: 0x00008583
		public static string Password
		{
			get
			{
				return WebMail.ReadValue<string>(WebMail.PasswordKey);
			}
			set
			{
				ScopeStorage.CurrentScope[WebMail.PasswordKey] = value;
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000A398 File Offset: 0x00008598
		public static void Send(string to, string subject, string body, string from = null, string cc = null, IEnumerable<string> filesToAttach = null, bool isBodyHtml = true, IEnumerable<string> additionalHeaders = null, string bcc = null, string contentEncoding = null, string headerEncoding = null, string priority = null, string replyTo = null)
		{
			if (filesToAttach != null)
			{
				foreach (string value in filesToAttach)
				{
					if (string.IsNullOrEmpty(value))
					{
						throw new ArgumentException(HelpersResources.WebMail_ItemInCollectionIsNull, "filesToAttach");
					}
				}
			}
			if (additionalHeaders != null)
			{
				foreach (string value2 in additionalHeaders)
				{
					if (string.IsNullOrEmpty(value2))
					{
						throw new ArgumentException(HelpersResources.WebMail_ItemInCollectionIsNull, "additionalHeaders");
					}
				}
			}
			MailPriority priority2 = MailPriority.Normal;
			if (!string.IsNullOrEmpty(priority) && !ConversionUtil.TryFromStringToEnum<MailPriority>(priority, out priority2))
			{
				throw new ArgumentException(HelpersResources.WebMail_InvalidPriority, "priority");
			}
			if (string.IsNullOrEmpty(WebMail.SmtpServer))
			{
				throw new InvalidOperationException(HelpersResources.WebMail_SmtpServerNotSpecified);
			}
			using (MailMessage mailMessage = new MailMessage())
			{
				WebMail.SetPropertiesOnMessage(mailMessage, to, subject, body, from, cc, bcc, replyTo, contentEncoding, headerEncoding, priority2, filesToAttach, isBodyHtml, additionalHeaders);
				using (SmtpClient smtpClient = new SmtpClient())
				{
					WebMail.SetPropertiesOnClient(smtpClient);
					smtpClient.Send(mailMessage);
				}
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000A4F4 File Offset: 0x000086F4
		private static TValue ReadValue<TValue>(object key)
		{
			return (TValue)((object)(ScopeStorage.CurrentScope[key] ?? WebMail.SmtpDefaults.Value[key]));
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000A51C File Offset: 0x0000871C
		private static IDictionary<object, object> ReadSmtpDefaults()
		{
			Dictionary<object, object> dictionary = new Dictionary<object, object>();
			try
			{
				using (SmtpClient smtpClient = new SmtpClient())
				{
					dictionary[WebMail.SmtpServerKey] = smtpClient.Host;
					dictionary[WebMail.SmtpPortKey] = smtpClient.Port;
					dictionary[WebMail.EnableSslKey] = smtpClient.EnableSsl;
					dictionary[WebMail.SmtpUseDefaultCredentialsKey] = smtpClient.UseDefaultCredentials;
					NetworkCredential networkCredential = smtpClient.Credentials as NetworkCredential;
					if (networkCredential != null)
					{
						dictionary[WebMail.UserNameKey] = networkCredential.UserName;
						dictionary[WebMail.PasswordKey] = networkCredential.Password;
					}
					else
					{
						dictionary[WebMail.UserNameKey] = null;
						dictionary[WebMail.PasswordKey] = null;
					}
					using (MailMessage mailMessage = new MailMessage())
					{
						dictionary[WebMail.FromKey] = ((mailMessage.From != null) ? mailMessage.From.Address : null);
					}
				}
			}
			catch (InvalidOperationException)
			{
			}
			return dictionary;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000A640 File Offset: 0x00008840
		internal static void SetPropertiesOnClient(SmtpClient client)
		{
			if (WebMail.SmtpServer != null)
			{
				client.Host = WebMail.SmtpServer;
			}
			client.Port = WebMail.SmtpPort;
			client.UseDefaultCredentials = WebMail.SmtpUseDefaultCredentials;
			client.EnableSsl = WebMail.EnableSsl;
			if (!string.IsNullOrEmpty(WebMail.UserName))
			{
				client.Credentials = new NetworkCredential(WebMail.UserName, WebMail.Password);
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000A6A4 File Offset: 0x000088A4
		internal static void SetPropertiesOnMessage(MailMessage message, string to, string subject, string body, string from, string cc, string bcc, string replyTo, string contentEncoding, string headerEncoding, MailPriority priority, IEnumerable<string> filesToAttach, bool isBodyHtml, IEnumerable<string> additionalHeaders)
		{
			message.Subject = subject;
			message.Body = body;
			message.IsBodyHtml = isBodyHtml;
			if (additionalHeaders != null)
			{
				WebMail.AssignHeaderValues(message, additionalHeaders);
			}
			if (to != null)
			{
				message.To.Add(to);
			}
			if (!string.IsNullOrEmpty(cc))
			{
				message.CC.Add(cc);
			}
			if (!string.IsNullOrEmpty(bcc))
			{
				message.Bcc.Add(bcc);
			}
			if (!string.IsNullOrEmpty(replyTo))
			{
				message.ReplyToList.Add(replyTo);
			}
			if (!string.IsNullOrEmpty(contentEncoding))
			{
				message.BodyEncoding = Encoding.GetEncoding(contentEncoding);
			}
			if (!string.IsNullOrEmpty(headerEncoding))
			{
				message.HeadersEncoding = Encoding.GetEncoding(headerEncoding);
			}
			message.Priority = priority;
			if (from != null)
			{
				message.From = new MailAddress(from);
			}
			else if (!string.IsNullOrEmpty(WebMail.From))
			{
				message.From = new MailAddress(WebMail.From);
			}
			else if (message.From == null || string.IsNullOrEmpty(message.From.Address))
			{
				HttpContext httpContext = HttpContext.Current;
				if (httpContext == null)
				{
					throw new InvalidOperationException(HelpersResources.WebMail_UnableToDetermineFrom);
				}
				message.From = new MailAddress("DoNotReply@" + httpContext.Request.Url.Host);
			}
			if (filesToAttach != null)
			{
				foreach (string text in filesToAttach)
				{
					if (!Path.IsPathRooted(text) && HttpRuntime.AppDomainAppPath != null)
					{
						message.Attachments.Add(new Attachment(Path.Combine(HttpRuntime.AppDomainAppPath, text)));
					}
					else
					{
						message.Attachments.Add(new Attachment(text));
					}
				}
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000A854 File Offset: 0x00008A54
		internal static void AssignHeaderValues(MailMessage message, IEnumerable<string> headerValues)
		{
			foreach (string header in headerValues)
			{
				string text;
				string text2;
				if (WebMail.TryParseHeader(header, out text, out text2))
				{
					Action<MailMessage, string> action;
					if (WebMail._actionableHeaders.TryGetValue(text, out action))
					{
						try
						{
							action(message, text2);
						}
						catch (FormatException)
						{
						}
					}
					message.Headers.Add(text, text2);
				}
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000A8DC File Offset: 0x00008ADC
		internal static bool TryParseHeader(string header, out string key, out string value)
		{
			int num = header.IndexOf(':');
			if (num > 0)
			{
				key = header.Substring(0, num).TrimEnd(new char[0]);
				value = header.Substring(num + 1).TrimStart(new char[0]);
				return key.Length > 0 && value.Length > 0;
			}
			key = null;
			value = null;
			return false;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000A940 File Offset: 0x00008B40
		private static void SetPriority(MailMessage message, string priority)
		{
			MailPriority priority2;
			if (!string.IsNullOrEmpty(priority) && ConversionUtil.TryFromStringToEnum<MailPriority>(priority, out priority2))
			{
				message.Priority = priority2;
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000A9BC File Offset: 0x00008BBC
		// Note: this type is marked as 'beforefieldinit'.
		static WebMail()
		{
			Dictionary<string, Action<MailMessage, string>> dictionary = new Dictionary<string, Action<MailMessage, string>>(StringComparer.OrdinalIgnoreCase);
			dictionary.Add("Bcc", delegate(MailMessage message, string value)
			{
				message.Bcc.Add(value);
			});
			dictionary.Add("Cc", delegate(MailMessage message, string value)
			{
				message.CC.Add(value);
			});
			dictionary.Add("From", delegate(MailMessage mailMessage, string value)
			{
				mailMessage.From = new MailAddress(value);
			});
			dictionary.Add("Priority", new Action<MailMessage, string>(WebMail.SetPriority));
			dictionary.Add("Reply-To", delegate(MailMessage mailMessage, string value)
			{
				mailMessage.ReplyToList.Add(value);
			});
			dictionary.Add("Sender", delegate(MailMessage mailMessage, string value)
			{
				mailMessage.Sender = new MailAddress(value);
			});
			dictionary.Add("To", delegate(MailMessage mailMessage, string value)
			{
				mailMessage.To.Add(value);
			});
			WebMail._actionableHeaders = dictionary;
		}

		// Token: 0x040000B1 RID: 177
		internal static readonly object SmtpServerKey = new object();

		// Token: 0x040000B2 RID: 178
		internal static readonly object SmtpPortKey = new object();

		// Token: 0x040000B3 RID: 179
		internal static readonly object SmtpUseDefaultCredentialsKey = new object();

		// Token: 0x040000B4 RID: 180
		internal static readonly object EnableSslKey = new object();

		// Token: 0x040000B5 RID: 181
		internal static readonly object PasswordKey = new object();

		// Token: 0x040000B6 RID: 182
		internal static readonly object UserNameKey = new object();

		// Token: 0x040000B7 RID: 183
		internal static readonly object FromKey = new object();

		// Token: 0x040000B8 RID: 184
		internal static readonly Lazy<IDictionary<object, object>> SmtpDefaults = new Lazy<IDictionary<object, object>>(new Func<IDictionary<object, object>>(WebMail.ReadSmtpDefaults));

		// Token: 0x040000B9 RID: 185
		private static readonly Dictionary<string, Action<MailMessage, string>> _actionableHeaders;
	}
}
