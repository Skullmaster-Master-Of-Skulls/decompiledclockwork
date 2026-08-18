using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Net.Mail;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.Management
{
	// Token: 0x02000171 RID: 369
	public abstract class MailWebEventProvider : BufferedWebEventProvider
	{
		// Token: 0x06001478 RID: 5240 RVA: 0x0003CF46 File Offset: 0x0003B146
		internal MailWebEventProvider()
		{
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x0003CF60 File Offset: 0x0003B160
		public override void Initialize(string name, NameValueCollection config)
		{
			ProviderUtil.GetAndRemoveRequiredNonEmptyStringAttribute(config, "from", name, ref this._from);
			ProviderUtil.GetAndRemoveStringAttribute(config, "to", name, ref this._to);
			ProviderUtil.GetAndRemoveStringAttribute(config, "cc", name, ref this._cc);
			ProviderUtil.GetAndRemoveStringAttribute(config, "bcc", name, ref this._bcc);
			if (string.IsNullOrEmpty(this._to) && string.IsNullOrEmpty(this._cc) && string.IsNullOrEmpty(this._bcc))
			{
				throw new ConfigurationErrorsException(SR.GetString("MailWebEventProvider_no_recipient_error", new object[]
				{
					base.GetType().ToString(),
					name
				}));
			}
			ProviderUtil.GetAndRemoveStringAttribute(config, "subjectPrefix", name, ref this._subjectPrefix);
			ProviderUtil.GetAndRemoveNonZeroPositiveOrInfiniteAttribute(config, "maxMessagesPerNotification", name, ref this._maxMessagesPerNotification);
			ProviderUtil.GetAndRemoveNonZeroPositiveOrInfiniteAttribute(config, "maxEventsPerMessage", name, ref this._maxEventsPerMessage);
			this._smtpClient = MailWebEventProvider.CreateSmtpClientWithAssert();
			base.Initialize(name, config);
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x0003D04D File Offset: 0x0003B24D
		[SmtpPermission(SecurityAction.Assert, Access = "Connect")]
		[EnvironmentPermission(SecurityAction.Assert, Read = "USERNAME")]
		internal static SmtpClient CreateSmtpClientWithAssert()
		{
			return new SmtpClient();
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x0600147B RID: 5243 RVA: 0x0003D054 File Offset: 0x0003B254
		internal string SubjectPrefix
		{
			get
			{
				return this._subjectPrefix;
			}
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x0003D05C File Offset: 0x0003B25C
		internal string GenerateSubject(int notificationSequence, int messageSequence, WebBaseEventCollection events, int count)
		{
			WebBaseEvent webBaseEvent = events[0];
			object[] args;
			if (count == 1)
			{
				string name = "WebEvent_event_email_subject";
				args = new string[]
				{
					notificationSequence.ToString(CultureInfo.InstalledUICulture),
					messageSequence.ToString(CultureInfo.InstalledUICulture),
					this._subjectPrefix,
					webBaseEvent.GetType().ToString(),
					WebBaseEvent.ApplicationInformation.ApplicationVirtualPath
				};
				return HttpUtility.HtmlEncode(SR.GetString(name, args));
			}
			string name2 = "WebEvent_event_group_email_subject";
			args = new string[]
			{
				notificationSequence.ToString(CultureInfo.InstalledUICulture),
				messageSequence.ToString(CultureInfo.InstalledUICulture),
				this._subjectPrefix,
				count.ToString(CultureInfo.InstalledUICulture),
				WebBaseEvent.ApplicationInformation.ApplicationVirtualPath
			};
			return HttpUtility.HtmlEncode(SR.GetString(name2, args));
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x0003D12C File Offset: 0x0003B32C
		internal MailMessage GetMessage()
		{
			MailMessage mailMessage = new MailMessage(this._from, this._to);
			if (!string.IsNullOrEmpty(this._cc))
			{
				mailMessage.CC.Add(new MailAddress(this._cc));
			}
			if (!string.IsNullOrEmpty(this._bcc))
			{
				mailMessage.Bcc.Add(new MailAddress(this._bcc));
			}
			return mailMessage;
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x0003D194 File Offset: 0x0003B394
		[SmtpPermission(SecurityAction.Assert, Access = "Connect")]
		internal void SendMail(MailMessage msg)
		{
			try
			{
				this._smtpClient.Send(msg);
			}
			catch (Exception innerException)
			{
				throw new HttpException(SR.GetString("MailWebEventProvider_cannot_send_mail"), innerException);
			}
		}

		// Token: 0x0600147F RID: 5247
		internal abstract void SendMessage(WebBaseEvent eventRaised);

		// Token: 0x06001480 RID: 5248 RVA: 0x0003D1D4 File Offset: 0x0003B3D4
		public override void ProcessEvent(WebBaseEvent eventRaised)
		{
			if (base.UseBuffering)
			{
				base.ProcessEvent(eventRaised);
				return;
			}
			this.SendMessage(eventRaised);
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x0003D1ED File Offset: 0x0003B3ED
		public override void Shutdown()
		{
			this.Flush();
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x0003D1F8 File Offset: 0x0003B3F8
		public override void ProcessEventFlush(WebEventBufferFlushInfo flushInfo)
		{
			int num = flushInfo.Events.Count;
			bool flag = false;
			int num2 = 1;
			int num3 = 0;
			bool flag2 = false;
			if (num == 0)
			{
				return;
			}
			WebBaseEvent[] array = null;
			int num4;
			if (num > this.MaxEventsPerMessage)
			{
				flag = true;
				num4 = num / this.MaxEventsPerMessage;
				if (num > num4 * this.MaxEventsPerMessage)
				{
					num4++;
				}
				if (num4 > this.MaxMessagesPerNotification)
				{
					num3 = num - this.MaxMessagesPerNotification * this.MaxEventsPerMessage;
					num4 = this.MaxMessagesPerNotification;
					num -= num3;
				}
			}
			else
			{
				num4 = 1;
			}
			int i = 0;
			while (i < num)
			{
				WebBaseEventCollection webBaseEventCollection;
				if (flag)
				{
					int num5 = Math.Min(this.MaxEventsPerMessage, num - i);
					if (array == null || array.Length != num5)
					{
						array = new WebBaseEvent[num5];
					}
					for (int j = 0; j < num5; j++)
					{
						array[j] = flushInfo.Events[j + i];
					}
					webBaseEventCollection = new WebBaseEventCollection(array);
				}
				else
				{
					webBaseEventCollection = flushInfo.Events;
				}
				this.SendMessage(webBaseEventCollection, flushInfo, num, num - (i + webBaseEventCollection.Count), num4, num3, num2, i, out flag2);
				if (flag2)
				{
					break;
				}
				i += webBaseEventCollection.Count;
				num2++;
			}
		}

		// Token: 0x06001483 RID: 5251
		internal abstract void SendMessage(WebBaseEventCollection events, WebEventBufferFlushInfo flushInfo, int eventsInNotification, int eventsRemaining, int messagesInNotification, int eventsLostDueToMessageLimit, int messageSequence, int eventsSent, out bool fatalError);

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001484 RID: 5252 RVA: 0x0003D31D File Offset: 0x0003B51D
		internal int MaxMessagesPerNotification
		{
			get
			{
				return this._maxMessagesPerNotification;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x0003D325 File Offset: 0x0003B525
		internal int MaxEventsPerMessage
		{
			get
			{
				return this._maxEventsPerMessage;
			}
		}

		// Token: 0x0400153B RID: 5435
		internal const int DefaultMaxMessagesPerNotification = 10;

		// Token: 0x0400153C RID: 5436
		internal const int DefaultMaxEventsPerMessage = 50;

		// Token: 0x0400153D RID: 5437
		internal const int MessageSequenceBase = 1;

		// Token: 0x0400153E RID: 5438
		private string _from;

		// Token: 0x0400153F RID: 5439
		private string _to;

		// Token: 0x04001540 RID: 5440
		private string _cc;

		// Token: 0x04001541 RID: 5441
		private string _bcc;

		// Token: 0x04001542 RID: 5442
		private string _subjectPrefix;

		// Token: 0x04001543 RID: 5443
		private SmtpClient _smtpClient;

		// Token: 0x04001544 RID: 5444
		private int _maxMessagesPerNotification = 10;

		// Token: 0x04001545 RID: 5445
		private int _maxEventsPerMessage = 50;
	}
}
