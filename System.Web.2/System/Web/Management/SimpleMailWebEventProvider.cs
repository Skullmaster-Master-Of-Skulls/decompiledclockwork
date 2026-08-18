using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web.Util;

namespace System.Web.Management
{
	// Token: 0x02000175 RID: 373
	public sealed class SimpleMailWebEventProvider : MailWebEventProvider, IInternalWebEventProvider
	{
		// Token: 0x060014A0 RID: 5280 RVA: 0x0003DD80 File Offset: 0x0003BF80
		internal SimpleMailWebEventProvider()
		{
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x0003DDA0 File Offset: 0x0003BFA0
		public override void Initialize(string name, NameValueCollection config)
		{
			string text = null;
			ProviderUtil.GetAndRemoveStringAttribute(config, "bodyHeader", name, ref this._bodyHeader);
			if (this._bodyHeader != null)
			{
				this._bodyHeader += "\n";
			}
			ProviderUtil.GetAndRemoveStringAttribute(config, "bodyFooter", name, ref this._bodyFooter);
			if (this._bodyFooter != null)
			{
				this._bodyFooter += "\n";
			}
			ProviderUtil.GetAndRemoveStringAttribute(config, "separator", name, ref text);
			if (text != null)
			{
				this._separator = text + "\n";
			}
			ProviderUtil.GetAndRemovePositiveOrInfiniteAttribute(config, "maxEventLength", name, ref this._maxEventLength);
			base.Initialize(name, config);
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x0003DE4C File Offset: 0x0003C04C
		private void GenerateWarnings(StringBuilder sb, DateTime lastFlush, int discardedSinceLastFlush, int seq, int eventsToDrop)
		{
			if (!base.UseBuffering)
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			if (discardedSinceLastFlush != 0 && seq == 1)
			{
				sb.Append(SimpleMailWebEventProvider.s_header_warnings);
				sb.Append("\n");
				sb.Append(this._separator);
				flag = true;
				sb.Append(SR.GetString("MailWebEventProvider_discard_warning", new object[]
				{
					100.ToString(CultureInfo.InstalledUICulture),
					discardedSinceLastFlush.ToString(CultureInfo.InstalledUICulture),
					lastFlush.ToString("r", CultureInfo.InstalledUICulture)
				}));
				sb.Append("\n\n");
				flag2 = true;
			}
			if (eventsToDrop > 0)
			{
				if (!flag)
				{
					sb.Append(SimpleMailWebEventProvider.s_header_warnings);
					sb.Append("\n");
					sb.Append(this._separator);
				}
				sb.Append(SR.GetString("MailWebEventProvider_events_drop_warning", new object[]
				{
					101.ToString(CultureInfo.InstalledUICulture),
					eventsToDrop.ToString(CultureInfo.InstalledUICulture)
				}));
				sb.Append("\n\n");
				flag2 = true;
			}
			if (flag2)
			{
				sb.Append("\n");
			}
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x0003DF7C File Offset: 0x0003C17C
		private void GenerateApplicationInformation(StringBuilder sb)
		{
			sb.Append(SimpleMailWebEventProvider.s_header_app_info);
			sb.Append("\n");
			sb.Append(this._separator);
			sb.Append(WebBaseEvent.ApplicationInformation.ToString());
			sb.Append("\n\n");
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x0003DFCC File Offset: 0x0003C1CC
		private void GenerateSummary(StringBuilder sb, int firstEvent, int lastEvent, int eventsInNotif, int eventsInBuffer)
		{
			if (!base.UseBuffering)
			{
				return;
			}
			sb.Append(SimpleMailWebEventProvider.s_header_summary);
			sb.Append("\n");
			sb.Append(this._separator);
			firstEvent++;
			lastEvent++;
			sb.Append(SR.GetString("MailWebEventProvider_summary_body", new object[]
			{
				firstEvent.ToString(CultureInfo.InstalledUICulture),
				lastEvent.ToString(CultureInfo.InstalledUICulture),
				eventsInNotif.ToString(CultureInfo.InstalledUICulture),
				eventsInBuffer.ToString(CultureInfo.InstalledUICulture)
			}));
			sb.Append("\n\n");
			sb.Append("\n");
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x0003E07C File Offset: 0x0003C27C
		private string GenerateBody(WebBaseEventCollection events, int begin, DateTime lastFlush, int discardedSinceLastFlush, int eventsInBuffer, int messageSequence, int eventsInNotification, int eventsLostDueToMessageLimit)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int count = events.Count;
			if (this._bodyHeader != null)
			{
				stringBuilder.Append(this._bodyHeader);
			}
			this.GenerateWarnings(stringBuilder, lastFlush, discardedSinceLastFlush, messageSequence, eventsLostDueToMessageLimit);
			this.GenerateSummary(stringBuilder, begin, begin + count - 1, eventsInNotification, eventsInBuffer);
			this.GenerateApplicationInformation(stringBuilder);
			for (int i = 0; i < count; i++)
			{
				WebBaseEvent webBaseEvent = events[i];
				string text = webBaseEvent.ToString(false, true);
				if (this._maxEventLength != 2147483647 && text.Length > this._maxEventLength)
				{
					text = text.Substring(0, this._maxEventLength);
				}
				if (i == 0)
				{
					stringBuilder.Append(SimpleMailWebEventProvider.s_header_events);
					stringBuilder.Append("\n");
					stringBuilder.Append(this._separator);
				}
				stringBuilder.Append(text);
				stringBuilder.Append("\n");
				stringBuilder.Append(this._separator);
			}
			if (this._bodyFooter != null)
			{
				stringBuilder.Append(this._bodyFooter);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x0003E188 File Offset: 0x0003C388
		internal override void SendMessage(WebBaseEvent eventRaised)
		{
			WebBaseEventCollection events = new WebBaseEventCollection(eventRaised);
			this.SendMessageInternal(events, Interlocked.Increment(ref this._nonBufferNotificationSequence), 0, DateTime.MinValue, 0, 0, 1, 1, 1, 0);
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x0003E1BC File Offset: 0x0003C3BC
		internal override void SendMessage(WebBaseEventCollection events, WebEventBufferFlushInfo flushInfo, int eventsInNotification, int eventsRemaining, int messagesInNotification, int eventsLostDueToMessageLimit, int messageSequence, int eventsSent, out bool fatalError)
		{
			this.SendMessageInternal(events, flushInfo.NotificationSequence, eventsSent, flushInfo.LastNotificationUtc, flushInfo.EventsDiscardedSinceLastNotification, flushInfo.EventsInBuffer, messageSequence, messagesInNotification, eventsInNotification, eventsLostDueToMessageLimit);
			fatalError = false;
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x0003E1F8 File Offset: 0x0003C3F8
		private void SendMessageInternal(WebBaseEventCollection events, int notificationSequence, int begin, DateTime lastFlush, int discardedSinceLastFlush, int eventsInBuffer, int messageSequence, int messagesInNotification, int eventsInNotification, int eventsLostDueToMessageLimit)
		{
			using (MailMessage message = base.GetMessage())
			{
				if (messageSequence != messagesInNotification)
				{
					eventsLostDueToMessageLimit = 0;
				}
				message.Body = this.GenerateBody(events, begin, lastFlush, discardedSinceLastFlush, eventsInBuffer, messageSequence, eventsInNotification, eventsLostDueToMessageLimit);
				message.Subject = base.GenerateSubject(notificationSequence, messageSequence, events, events.Count);
				base.SendMail(message);
			}
		}

		// Token: 0x04001563 RID: 5475
		private const int DefaultMaxEventLength = 8192;

		// Token: 0x04001564 RID: 5476
		private const int MessageIdDiscard = 100;

		// Token: 0x04001565 RID: 5477
		private const int MessageIdEventsToDrop = 101;

		// Token: 0x04001566 RID: 5478
		private static string s_header_warnings = SR.GetString("MailWebEventProvider_Warnings");

		// Token: 0x04001567 RID: 5479
		private static string s_header_summary = SR.GetString("MailWebEventProvider_Summary");

		// Token: 0x04001568 RID: 5480
		private static string s_header_app_info = SR.GetString("MailWebEventProvider_Application_Info");

		// Token: 0x04001569 RID: 5481
		private static string s_header_events = SR.GetString("MailWebEventProvider_Events");

		// Token: 0x0400156A RID: 5482
		private string _separator = "---------------\n";

		// Token: 0x0400156B RID: 5483
		private string _bodyHeader;

		// Token: 0x0400156C RID: 5484
		private string _bodyFooter;

		// Token: 0x0400156D RID: 5485
		private int _maxEventLength = 8192;

		// Token: 0x0400156E RID: 5486
		private int _nonBufferNotificationSequence;
	}
}
