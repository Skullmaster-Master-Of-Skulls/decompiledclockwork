using System;
using System.Net.Mail;

namespace System.Web.Management
{
	// Token: 0x0200017C RID: 380
	public sealed class MailEventNotificationInfo
	{
		// Token: 0x060014E4 RID: 5348 RVA: 0x0003FD70 File Offset: 0x0003DF70
		internal MailEventNotificationInfo(MailMessage msg, WebBaseEventCollection events, DateTime lastNotificationUtc, int discardedSinceLastNotification, int eventsInBuffer, int notificationSequence, EventNotificationType notificationType, int eventsInNotification, int eventsRemaining, int messagesInNotification, int eventsLostDueToMessageLimit, int messageSequence)
		{
			this._events = events;
			this._lastNotificationUtc = lastNotificationUtc;
			this._discardedSinceLastNotification = discardedSinceLastNotification;
			this._eventsInBuffer = eventsInBuffer;
			this._notificationSequence = notificationSequence;
			this._notificationType = notificationType;
			this._eventsInNotification = eventsInNotification;
			this._eventsRemaining = eventsRemaining;
			this._messagesInNotification = messagesInNotification;
			this._eventsLostDueToMessageLimit = eventsLostDueToMessageLimit;
			this._messageSequence = messageSequence;
			this._msg = msg;
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x0003FDE0 File Offset: 0x0003DFE0
		public WebBaseEventCollection Events
		{
			get
			{
				return this._events;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x0003FDE8 File Offset: 0x0003DFE8
		public EventNotificationType NotificationType
		{
			get
			{
				return this._notificationType;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x060014E7 RID: 5351 RVA: 0x0003FDF0 File Offset: 0x0003DFF0
		public int EventsInNotification
		{
			get
			{
				return this._eventsInNotification;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x0003FDF8 File Offset: 0x0003DFF8
		public int EventsRemaining
		{
			get
			{
				return this._eventsRemaining;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x060014E9 RID: 5353 RVA: 0x0003FE00 File Offset: 0x0003E000
		public int MessagesInNotification
		{
			get
			{
				return this._messagesInNotification;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x0003FE08 File Offset: 0x0003E008
		public int EventsInBuffer
		{
			get
			{
				return this._eventsInBuffer;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x060014EB RID: 5355 RVA: 0x0003FE10 File Offset: 0x0003E010
		public int EventsDiscardedByBuffer
		{
			get
			{
				return this._discardedSinceLastNotification;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x060014EC RID: 5356 RVA: 0x0003FE18 File Offset: 0x0003E018
		public int EventsDiscardedDueToMessageLimit
		{
			get
			{
				return this._eventsLostDueToMessageLimit;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x060014ED RID: 5357 RVA: 0x0003FE20 File Offset: 0x0003E020
		public int NotificationSequence
		{
			get
			{
				return this._notificationSequence;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x0003FE28 File Offset: 0x0003E028
		public int MessageSequence
		{
			get
			{
				return this._messageSequence;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x060014EF RID: 5359 RVA: 0x0003FE30 File Offset: 0x0003E030
		public DateTime LastNotificationUtc
		{
			get
			{
				return this._lastNotificationUtc;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x0003FE38 File Offset: 0x0003E038
		public MailMessage Message
		{
			get
			{
				return this._msg;
			}
		}

		// Token: 0x04001595 RID: 5525
		private WebBaseEventCollection _events;

		// Token: 0x04001596 RID: 5526
		private DateTime _lastNotificationUtc;

		// Token: 0x04001597 RID: 5527
		private int _discardedSinceLastNotification;

		// Token: 0x04001598 RID: 5528
		private int _eventsInBuffer;

		// Token: 0x04001599 RID: 5529
		private int _notificationSequence;

		// Token: 0x0400159A RID: 5530
		private EventNotificationType _notificationType;

		// Token: 0x0400159B RID: 5531
		private int _eventsInNotification;

		// Token: 0x0400159C RID: 5532
		private int _eventsRemaining;

		// Token: 0x0400159D RID: 5533
		private int _messagesInNotification;

		// Token: 0x0400159E RID: 5534
		private int _eventsLostDueToMessageLimit;

		// Token: 0x0400159F RID: 5535
		private int _messageSequence;

		// Token: 0x040015A0 RID: 5536
		private MailMessage _msg;
	}
}
