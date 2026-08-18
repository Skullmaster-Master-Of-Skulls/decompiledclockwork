using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Web.Util;

namespace System.Web.Management
{
	// Token: 0x0200017B RID: 379
	public sealed class TemplatedMailWebEventProvider : MailWebEventProvider, IInternalWebEventProvider
	{
		// Token: 0x060014DD RID: 5341 RVA: 0x0003FA5C File Offset: 0x0003DC5C
		internal TemplatedMailWebEventProvider()
		{
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x0003FA64 File Offset: 0x0003DC64
		public override void Initialize(string name, NameValueCollection config)
		{
			ProviderUtil.GetAndRemoveStringAttribute(config, "template", name, ref this._templateUrl);
			if (this._templateUrl == null)
			{
				throw new ConfigurationErrorsException(SR.GetString("Provider_missing_attribute", new object[]
				{
					"template",
					name
				}));
			}
			this._templateUrl = this._templateUrl.Trim();
			if (this._templateUrl.Length == 0)
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_provider_attribute", new object[]
				{
					"template",
					name,
					this._templateUrl
				}));
			}
			if (!UrlPath.IsRelativeUrl(this._templateUrl))
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_mail_template_provider_attribute", new object[]
				{
					"template",
					name,
					this._templateUrl
				}));
			}
			this._templateUrl = UrlPath.Combine(HttpRuntime.AppDomainAppVirtualPathString, this._templateUrl);
			if (!HttpRuntime.IsPathWithinAppRoot(this._templateUrl))
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_mail_template_provider_attribute", new object[]
				{
					"template",
					name,
					this._templateUrl
				}));
			}
			ProviderUtil.GetAndRemoveBooleanAttribute(config, "detailedTemplateErrors", name, ref this._detailedTemplateErrors);
			base.Initialize(name, config);
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x0003FB98 File Offset: 0x0003DD98
		private void GenerateMessageBody(MailMessage msg, WebBaseEventCollection events, DateTime lastNotificationUtc, int discardedSinceLastNotification, int eventsInBuffer, int notificationSequence, EventNotificationType notificationType, int eventsInNotification, int eventsRemaining, int messagesInNotification, int eventsLostDueToMessageLimit, int messageSequence, out bool fatalError)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InstalledUICulture);
			MailEventNotificationInfo data = new MailEventNotificationInfo(msg, events, lastNotificationUtc, discardedSinceLastNotification, eventsInBuffer, notificationSequence, notificationType, eventsInNotification, eventsRemaining, messagesInNotification, eventsLostDueToMessageLimit, messageSequence);
			CallContext.SetData("_TWCurEvt", data);
			try
			{
				TemplatedMailWebEventProvider.TemplatedMailErrorFormatterGenerator templatedMailErrorFormatterGenerator = new TemplatedMailWebEventProvider.TemplatedMailErrorFormatterGenerator(events.Count + eventsRemaining, this._detailedTemplateErrors);
				HttpServerUtility.ExecuteLocalRequestAndCaptureResponse(this._templateUrl, stringWriter, templatedMailErrorFormatterGenerator);
				fatalError = templatedMailErrorFormatterGenerator.ErrorFormatterCalled;
				if (fatalError)
				{
					msg.Subject = HttpUtility.HtmlEncode(SR.GetString("WebEvent_event_email_subject_template_error", new object[]
					{
						notificationSequence.ToString(CultureInfo.InstalledUICulture),
						messageSequence.ToString(CultureInfo.InstalledUICulture),
						base.SubjectPrefix
					}));
				}
				msg.Body = stringWriter.ToString();
				msg.IsBodyHtml = true;
			}
			finally
			{
				CallContext.FreeNamedDataSlot("_TWCurEvt");
			}
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x0003FC78 File Offset: 0x0003DE78
		internal override void SendMessage(WebBaseEvent eventRaised)
		{
			WebBaseEventCollection events = new WebBaseEventCollection(eventRaised);
			bool flag;
			this.SendMessageInternal(events, DateTime.MinValue, 0, 0, Interlocked.Increment(ref this._nonBufferNotificationSequence), EventNotificationType.Unbuffered, 1, 0, 1, 0, 1, out flag);
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x0003FCB0 File Offset: 0x0003DEB0
		internal override void SendMessage(WebBaseEventCollection events, WebEventBufferFlushInfo flushInfo, int eventsInNotification, int eventsRemaining, int messagesInNotification, int eventsLostDueToMessageLimit, int messageSequence, int eventsSent, out bool fatalError)
		{
			this.SendMessageInternal(events, flushInfo.LastNotificationUtc, flushInfo.EventsDiscardedSinceLastNotification, flushInfo.EventsInBuffer, flushInfo.NotificationSequence, flushInfo.NotificationType, eventsInNotification, eventsRemaining, messagesInNotification, eventsLostDueToMessageLimit, messageSequence, out fatalError);
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x0003FCF0 File Offset: 0x0003DEF0
		private void SendMessageInternal(WebBaseEventCollection events, DateTime lastNotificationUtc, int discardedSinceLastNotification, int eventsInBuffer, int notificationSequence, EventNotificationType notificationType, int eventsInNotification, int eventsRemaining, int messagesInNotification, int eventsLostDueToMessageLimit, int messageSequence, out bool fatalError)
		{
			using (MailMessage message = base.GetMessage())
			{
				message.Subject = base.GenerateSubject(notificationSequence, messageSequence, events, events.Count);
				this.GenerateMessageBody(message, events, lastNotificationUtc, discardedSinceLastNotification, eventsInBuffer, notificationSequence, notificationType, eventsInNotification, eventsRemaining, messagesInNotification, eventsLostDueToMessageLimit, messageSequence, out fatalError);
				base.SendMail(message);
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x060014E3 RID: 5347 RVA: 0x0003FD5C File Offset: 0x0003DF5C
		public static MailEventNotificationInfo CurrentNotification
		{
			get
			{
				return (MailEventNotificationInfo)CallContext.GetData("_TWCurEvt");
			}
		}

		// Token: 0x04001591 RID: 5521
		private int _nonBufferNotificationSequence;

		// Token: 0x04001592 RID: 5522
		private string _templateUrl;

		// Token: 0x04001593 RID: 5523
		private bool _detailedTemplateErrors;

		// Token: 0x04001594 RID: 5524
		internal const string CurrentEventsName = "_TWCurEvt";

		// Token: 0x0200090F RID: 2319
		private class TemplatedMailErrorFormatterGenerator : ErrorFormatterGenerator
		{
			// Token: 0x060068F9 RID: 26873 RVA: 0x00175F35 File Offset: 0x00174135
			internal TemplatedMailErrorFormatterGenerator(int eventsRemaining, bool showDetails)
			{
				this._eventsRemaining = eventsRemaining;
				this._showDetails = showDetails;
			}

			// Token: 0x17001D21 RID: 7457
			// (get) Token: 0x060068FA RID: 26874 RVA: 0x00175F4B File Offset: 0x0017414B
			internal bool ErrorFormatterCalled
			{
				get
				{
					return this._errorFormatterCalled;
				}
			}

			// Token: 0x060068FB RID: 26875 RVA: 0x00175F54 File Offset: 0x00174154
			internal override ErrorFormatter GetErrorFormatter(Exception e)
			{
				Exception innerException = e.InnerException;
				this._errorFormatterCalled = true;
				while (innerException != null)
				{
					if (innerException is HttpCompileException)
					{
						return new TemplatedMailCompileErrorFormatter((HttpCompileException)innerException, this._eventsRemaining, this._showDetails);
					}
					innerException = innerException.InnerException;
				}
				return new TemplatedMailRuntimeErrorFormatter(e, this._eventsRemaining, this._showDetails);
			}

			// Token: 0x0400371F RID: 14111
			private int _eventsRemaining;

			// Token: 0x04003720 RID: 14112
			private bool _showDetails;

			// Token: 0x04003721 RID: 14113
			private bool _errorFormatterCalled;
		}
	}
}
