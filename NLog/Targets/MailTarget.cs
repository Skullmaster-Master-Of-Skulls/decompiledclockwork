using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using JetBrains.Annotations;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.Layouts;

namespace NLog.Targets
{
	// Token: 0x02000162 RID: 354
	[Target("Mail")]
	public class MailTarget : TargetWithLayoutHeaderAndFooter
	{
		// Token: 0x06000D52 RID: 3410 RVA: 0x00020150 File Offset: 0x0001E350
		public MailTarget()
		{
			this.Body = "${message}${newline}";
			this.Subject = "Message from NLog on ${machinename}";
			this.Encoding = Encoding.UTF8;
			this.SmtpPort = 25;
			this.SmtpAuthentication = SmtpAuthenticationMode.None;
			this.Timeout = 10000;
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x000201A8 File Offset: 0x0001E3A8
		// (set) Token: 0x06000D54 RID: 3412 RVA: 0x00020210 File Offset: 0x0001E410
		internal SmtpSection SmtpSection
		{
			get
			{
				if (this._currentailSettings == null)
				{
					try
					{
						this._currentailSettings = (System.Configuration.ConfigurationManager.GetSection("system.net/mailSettings/smtp") as SmtpSection);
					}
					catch (Exception ex)
					{
						InternalLogger.Warn(ex, "reading 'From' from .config failed.");
						if (ex.MustBeRethrown())
						{
							throw;
						}
						this._currentailSettings = new SmtpSection();
					}
				}
				return this._currentailSettings;
			}
			set
			{
				this._currentailSettings = value;
			}
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00020219 File Offset: 0x0001E419
		public MailTarget(string name) : this()
		{
			base.Name = name;
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x00020228 File Offset: 0x0001E428
		// (set) Token: 0x06000D57 RID: 3415 RVA: 0x00020263 File Offset: 0x0001E463
		public Layout From
		{
			get
			{
				if (!this.UseSystemNetMailSettings || this._from != null)
				{
					return this._from;
				}
				string from = this.SmtpSection.From;
				if (from == null)
				{
					return null;
				}
				return from;
			}
			set
			{
				this._from = value;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x0002026C File Offset: 0x0001E46C
		// (set) Token: 0x06000D59 RID: 3417 RVA: 0x00020274 File Offset: 0x0001E474
		[RequiredParameter]
		public Layout To { get; set; }

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x0002027D File Offset: 0x0001E47D
		// (set) Token: 0x06000D5B RID: 3419 RVA: 0x00020285 File Offset: 0x0001E485
		public Layout CC { get; set; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x0002028E File Offset: 0x0001E48E
		// (set) Token: 0x06000D5D RID: 3421 RVA: 0x00020296 File Offset: 0x0001E496
		public Layout Bcc { get; set; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x0002029F File Offset: 0x0001E49F
		// (set) Token: 0x06000D5F RID: 3423 RVA: 0x000202A7 File Offset: 0x0001E4A7
		public bool AddNewLines { get; set; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x000202B0 File Offset: 0x0001E4B0
		// (set) Token: 0x06000D61 RID: 3425 RVA: 0x000202B8 File Offset: 0x0001E4B8
		[RequiredParameter]
		[DefaultValue("Message from NLog on ${machinename}")]
		public Layout Subject { get; set; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x000202C1 File Offset: 0x0001E4C1
		// (set) Token: 0x06000D63 RID: 3427 RVA: 0x000202C9 File Offset: 0x0001E4C9
		[DefaultValue("${message}${newline}")]
		public Layout Body
		{
			get
			{
				return this.Layout;
			}
			set
			{
				this.Layout = value;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x000202D2 File Offset: 0x0001E4D2
		// (set) Token: 0x06000D65 RID: 3429 RVA: 0x000202DA File Offset: 0x0001E4DA
		[DefaultValue("UTF8")]
		public Encoding Encoding { get; set; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x000202E3 File Offset: 0x0001E4E3
		// (set) Token: 0x06000D67 RID: 3431 RVA: 0x000202EB File Offset: 0x0001E4EB
		[DefaultValue(false)]
		public bool Html { get; set; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x000202F4 File Offset: 0x0001E4F4
		// (set) Token: 0x06000D69 RID: 3433 RVA: 0x000202FC File Offset: 0x0001E4FC
		public Layout SmtpServer { get; set; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x00020305 File Offset: 0x0001E505
		// (set) Token: 0x06000D6B RID: 3435 RVA: 0x0002030D File Offset: 0x0001E50D
		[DefaultValue("None")]
		public SmtpAuthenticationMode SmtpAuthentication { get; set; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000D6C RID: 3436 RVA: 0x00020316 File Offset: 0x0001E516
		// (set) Token: 0x06000D6D RID: 3437 RVA: 0x0002031E File Offset: 0x0001E51E
		public Layout SmtpUserName { get; set; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x00020327 File Offset: 0x0001E527
		// (set) Token: 0x06000D6F RID: 3439 RVA: 0x0002032F File Offset: 0x0001E52F
		public Layout SmtpPassword { get; set; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x00020338 File Offset: 0x0001E538
		// (set) Token: 0x06000D71 RID: 3441 RVA: 0x00020340 File Offset: 0x0001E540
		[DefaultValue(false)]
		public bool EnableSsl { get; set; }

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x00020349 File Offset: 0x0001E549
		// (set) Token: 0x06000D73 RID: 3443 RVA: 0x00020351 File Offset: 0x0001E551
		[DefaultValue(25)]
		public int SmtpPort { get; set; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x0002035A File Offset: 0x0001E55A
		// (set) Token: 0x06000D75 RID: 3445 RVA: 0x00020362 File Offset: 0x0001E562
		[DefaultValue(false)]
		public bool UseSystemNetMailSettings { get; set; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x0002036B File Offset: 0x0001E56B
		// (set) Token: 0x06000D77 RID: 3447 RVA: 0x00020373 File Offset: 0x0001E573
		[DefaultValue(SmtpDeliveryMethod.Network)]
		public SmtpDeliveryMethod DeliveryMethod { get; set; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x0002037C File Offset: 0x0001E57C
		// (set) Token: 0x06000D79 RID: 3449 RVA: 0x00020384 File Offset: 0x0001E584
		[DefaultValue(null)]
		public string PickupDirectoryLocation { get; set; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x0002038D File Offset: 0x0001E58D
		// (set) Token: 0x06000D7B RID: 3451 RVA: 0x00020395 File Offset: 0x0001E595
		public Layout Priority { get; set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x0002039E File Offset: 0x0001E59E
		// (set) Token: 0x06000D7D RID: 3453 RVA: 0x000203A6 File Offset: 0x0001E5A6
		[DefaultValue(false)]
		public bool ReplaceNewlineWithBrTagInHtml { get; set; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x000203AF File Offset: 0x0001E5AF
		// (set) Token: 0x06000D7F RID: 3455 RVA: 0x000203B7 File Offset: 0x0001E5B7
		[DefaultValue(10000)]
		public int Timeout { get; set; }

		// Token: 0x06000D80 RID: 3456 RVA: 0x000203C0 File Offset: 0x0001E5C0
		internal virtual ISmtpClient CreateSmtpClient()
		{
			return new MySmtpClient();
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x000203C8 File Offset: 0x0001E5C8
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			this.Write(new AsyncLogEventInfo[]
			{
				logEvent
			});
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x00020400 File Offset: 0x0001E600
		protected override void Write(AsyncLogEventInfo[] logEvents)
		{
			foreach (KeyValuePair<string, List<AsyncLogEventInfo>> keyValuePair in logEvents.BucketSort((AsyncLogEventInfo c) => this.GetSmtpSettingsKey(c.LogEvent)))
			{
				List<AsyncLogEventInfo> value = keyValuePair.Value;
				this.ProcessSingleMailMessage(value);
			}
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00020468 File Offset: 0x0001E668
		protected override void InitializeTarget()
		{
			this.CheckRequiredParameters();
			base.InitializeTarget();
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00020478 File Offset: 0x0001E678
		private void ProcessSingleMailMessage([NotNull] List<AsyncLogEventInfo> events)
		{
			try
			{
				if (events.Count == 0)
				{
					throw new NLogRuntimeException("We need at least one event.");
				}
				LogEventInfo logEvent = events[0].LogEvent;
				LogEventInfo logEvent2 = events[events.Count - 1].LogEvent;
				StringBuilder stringBuilder = this.CreateBodyBuffer(events, logEvent, logEvent2);
				using (MailMessage mailMessage = this.CreateMailMessage(logEvent2, stringBuilder.ToString()))
				{
					using (ISmtpClient smtpClient = this.CreateSmtpClient())
					{
						if (!this.UseSystemNetMailSettings)
						{
							this.ConfigureMailClient(logEvent2, smtpClient);
						}
						InternalLogger.Debug("Sending mail to {0} using {1}:{2} (ssl={3})", new object[]
						{
							mailMessage.To,
							smtpClient.Host,
							smtpClient.Port,
							smtpClient.EnableSsl
						});
						InternalLogger.Trace("  Subject: '{0}'", new object[]
						{
							mailMessage.Subject
						});
						InternalLogger.Trace("  From: '{0}'", new object[]
						{
							mailMessage.From.ToString()
						});
						smtpClient.Send(mailMessage);
						foreach (AsyncLogEventInfo asyncLogEventInfo in events)
						{
							asyncLogEventInfo.Continuation(null);
						}
					}
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "Error sending mail.");
				if (ex.MustBeRethrown())
				{
					throw;
				}
				foreach (AsyncLogEventInfo asyncLogEventInfo2 in events)
				{
					asyncLogEventInfo2.Continuation(ex);
				}
			}
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x000206AC File Offset: 0x0001E8AC
		private StringBuilder CreateBodyBuffer(IEnumerable<AsyncLogEventInfo> events, LogEventInfo firstEvent, LogEventInfo lastEvent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (base.Header != null)
			{
				stringBuilder.Append(base.Header.Render(firstEvent));
				if (this.AddNewLines)
				{
					stringBuilder.Append("\n");
				}
			}
			foreach (AsyncLogEventInfo asyncLogEventInfo in events)
			{
				stringBuilder.Append(this.Layout.Render(asyncLogEventInfo.LogEvent));
				if (this.AddNewLines)
				{
					stringBuilder.Append("\n");
				}
			}
			if (base.Footer != null)
			{
				stringBuilder.Append(base.Footer.Render(lastEvent));
				if (this.AddNewLines)
				{
					stringBuilder.Append("\n");
				}
			}
			return stringBuilder;
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00020780 File Offset: 0x0001E980
		internal void ConfigureMailClient(LogEventInfo lastEvent, ISmtpClient client)
		{
			this.CheckRequiredParameters();
			if (this.SmtpServer == null && string.IsNullOrEmpty(this.PickupDirectoryLocation))
			{
				throw new NLogRuntimeException(string.Format("After the processing of the MailTarget's '{0}' property it appears to be empty. The email message will not be sent.", "SmtpServer/PickupDirectoryLocation"));
			}
			if (this.DeliveryMethod == SmtpDeliveryMethod.Network && this.SmtpServer == null)
			{
				throw new NLogRuntimeException(string.Format("After the processing of the MailTarget's '{0}' property it appears to be empty. The email message will not be sent.", "SmtpServer"));
			}
			if (this.DeliveryMethod == SmtpDeliveryMethod.SpecifiedPickupDirectory && string.IsNullOrEmpty(this.PickupDirectoryLocation))
			{
				throw new NLogRuntimeException(string.Format("After the processing of the MailTarget's '{0}' property it appears to be empty. The email message will not be sent.", "PickupDirectoryLocation"));
			}
			if (this.SmtpServer != null && this.DeliveryMethod == SmtpDeliveryMethod.Network)
			{
				string text = this.SmtpServer.Render(lastEvent);
				if (string.IsNullOrEmpty(text))
				{
					throw new NLogRuntimeException(string.Format("After the processing of the MailTarget's '{0}' property it appears to be empty. The email message will not be sent.", "SmtpServer"));
				}
				client.Host = text;
				client.Port = this.SmtpPort;
				client.EnableSsl = this.EnableSsl;
				if (this.SmtpAuthentication == SmtpAuthenticationMode.Ntlm)
				{
					InternalLogger.Trace("  Using NTLM authentication.");
					client.Credentials = CredentialCache.DefaultNetworkCredentials;
				}
				else if (this.SmtpAuthentication == SmtpAuthenticationMode.Basic)
				{
					string text2 = this.SmtpUserName.Render(lastEvent);
					string text3 = this.SmtpPassword.Render(lastEvent);
					InternalLogger.Trace("  Using basic authentication: Username='{0}' Password='{1}'", new object[]
					{
						text2,
						new string('*', text3.Length)
					});
					client.Credentials = new NetworkCredential(text2, text3);
				}
			}
			if (!string.IsNullOrEmpty(this.PickupDirectoryLocation) && this.DeliveryMethod == SmtpDeliveryMethod.SpecifiedPickupDirectory)
			{
				client.PickupDirectoryLocation = MailTarget.ConvertDirectoryLocation(this.PickupDirectoryLocation);
			}
			client.DeliveryMethod = this.DeliveryMethod;
			client.Timeout = this.Timeout;
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x00020924 File Offset: 0x0001EB24
		internal static string ConvertDirectoryLocation(string pickupDirectoryLocation)
		{
			if (!pickupDirectoryLocation.StartsWith("~/"))
			{
				return pickupDirectoryLocation;
			}
			string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			string path = pickupDirectoryLocation.Substring("~/".Length).Replace('/', Path.DirectorySeparatorChar);
			return Path.Combine(baseDirectory, path);
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00020974 File Offset: 0x0001EB74
		private void CheckRequiredParameters()
		{
			if (!this.UseSystemNetMailSettings && this.SmtpServer == null && this.DeliveryMethod == SmtpDeliveryMethod.Network)
			{
				throw new NLogConfigurationException("The MailTarget's '{0}' properties are not set - but needed because useSystemNetMailSettings=false and DeliveryMethod=Network. The email message will not be sent.", new object[]
				{
					"SmtpServer"
				});
			}
			if (!this.UseSystemNetMailSettings && string.IsNullOrEmpty(this.PickupDirectoryLocation) && this.DeliveryMethod == SmtpDeliveryMethod.SpecifiedPickupDirectory)
			{
				throw new NLogConfigurationException("The MailTarget's '{0}' properties are not set - but needed because useSystemNetMailSettings=false and DeliveryMethod=SpecifiedPickupDirectory. The email message will not be sent.", new object[]
				{
					"PickupDirectoryLocation"
				});
			}
			if (this.From == null)
			{
				throw new NLogConfigurationException("After the processing of the MailTarget's '{0}' property it appears to be empty. The email message will not be sent.", new object[]
				{
					"From"
				});
			}
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x00020A10 File Offset: 0x0001EC10
		private string GetSmtpSettingsKey(LogEventInfo logEvent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			MailTarget.AppendLayout(stringBuilder, logEvent, this.From);
			MailTarget.AppendLayout(stringBuilder, logEvent, this.To);
			MailTarget.AppendLayout(stringBuilder, logEvent, this.CC);
			MailTarget.AppendLayout(stringBuilder, logEvent, this.Bcc);
			MailTarget.AppendLayout(stringBuilder, logEvent, this.SmtpServer);
			MailTarget.AppendLayout(stringBuilder, logEvent, this.SmtpPassword);
			MailTarget.AppendLayout(stringBuilder, logEvent, this.SmtpUserName);
			return stringBuilder.ToString();
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00020A84 File Offset: 0x0001EC84
		private static void AppendLayout(StringBuilder sb, LogEventInfo logEvent, Layout layout)
		{
			sb.Append("|");
			if (layout != null)
			{
				sb.Append(layout.Render(logEvent));
			}
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00020AA4 File Offset: 0x0001ECA4
		private MailMessage CreateMailMessage(LogEventInfo lastEvent, string body)
		{
			MailMessage mailMessage = new MailMessage();
			string text = (this.From == null) ? null : this.From.Render(lastEvent);
			if (string.IsNullOrEmpty(text))
			{
				throw new NLogRuntimeException("After the processing of the MailTarget's '{0}' property it appears to be empty. The email message will not be sent.", new object[]
				{
					"From"
				});
			}
			mailMessage.From = new MailAddress(text);
			bool flag = MailTarget.AddAddresses(mailMessage.To, this.To, lastEvent);
			bool flag2 = MailTarget.AddAddresses(mailMessage.CC, this.CC, lastEvent);
			bool flag3 = MailTarget.AddAddresses(mailMessage.Bcc, this.Bcc, lastEvent);
			if (!flag && !flag2 && !flag3)
			{
				throw new NLogRuntimeException("After the processing of the MailTarget's '{0}' property it appears to be empty. The email message will not be sent.", new object[]
				{
					"To/Cc/Bcc"
				});
			}
			mailMessage.Subject = ((this.Subject == null) ? string.Empty : this.Subject.Render(lastEvent).Trim());
			mailMessage.BodyEncoding = this.Encoding;
			mailMessage.IsBodyHtml = this.Html;
			if (this.Priority != null)
			{
				string value = this.Priority.Render(lastEvent);
				try
				{
					mailMessage.Priority = (MailPriority)Enum.Parse(typeof(MailPriority), value, true);
				}
				catch
				{
					InternalLogger.Warn("Could not convert '{0}' to MailPriority, valid values are Low, Normal and High. Using normal priority as fallback.");
					mailMessage.Priority = MailPriority.Normal;
				}
			}
			mailMessage.Body = body;
			if (mailMessage.IsBodyHtml && this.ReplaceNewlineWithBrTagInHtml && mailMessage.Body != null)
			{
				mailMessage.Body = mailMessage.Body.Replace(EnvironmentHelper.NewLine, "<br/>");
			}
			return mailMessage;
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00020C38 File Offset: 0x0001EE38
		private static bool AddAddresses(MailAddressCollection mailAddressCollection, Layout layout, LogEventInfo logEvent)
		{
			bool result = false;
			if (layout != null)
			{
				foreach (string addresses in layout.Render(logEvent).Split(new char[]
				{
					';'
				}, StringSplitOptions.RemoveEmptyEntries))
				{
					mailAddressCollection.Add(addresses);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x04000391 RID: 913
		private const string RequiredPropertyIsEmptyFormat = "After the processing of the MailTarget's '{0}' property it appears to be empty. The email message will not be sent.";

		// Token: 0x04000392 RID: 914
		private Layout _from;

		// Token: 0x04000393 RID: 915
		private SmtpSection _currentailSettings;
	}
}
