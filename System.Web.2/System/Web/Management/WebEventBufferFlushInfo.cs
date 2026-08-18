using System;

namespace System.Web.Management
{
	// Token: 0x02000181 RID: 385
	public sealed class WebEventBufferFlushInfo
	{
		// Token: 0x060014FF RID: 5375 RVA: 0x0003FFB1 File Offset: 0x0003E1B1
		internal WebEventBufferFlushInfo(WebBaseEventCollection events, EventNotificationType notificationType, int notificationSequence, DateTime lastNotification, int eventsDiscardedSinceLastNotification, int eventsInBuffer)
		{
			this._events = events;
			this._notificationType = notificationType;
			this._notificationSequence = notificationSequence;
			this._lastNotification = lastNotification;
			this._eventsDiscardedSinceLastNotification = eventsDiscardedSinceLastNotification;
			this._eventsInBuffer = eventsInBuffer;
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x0003FFE6 File Offset: 0x0003E1E6
		public WebBaseEventCollection Events
		{
			get
			{
				return this._events;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001501 RID: 5377 RVA: 0x0003FFEE File Offset: 0x0003E1EE
		public DateTime LastNotificationUtc
		{
			get
			{
				return this._lastNotification;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x0003FFF6 File Offset: 0x0003E1F6
		public int EventsDiscardedSinceLastNotification
		{
			get
			{
				return this._eventsDiscardedSinceLastNotification;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x0003FFFE File Offset: 0x0003E1FE
		public int EventsInBuffer
		{
			get
			{
				return this._eventsInBuffer;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x00040006 File Offset: 0x0003E206
		public int NotificationSequence
		{
			get
			{
				return this._notificationSequence;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001505 RID: 5381 RVA: 0x0004000E File Offset: 0x0003E20E
		public EventNotificationType NotificationType
		{
			get
			{
				return this._notificationType;
			}
		}

		// Token: 0x040015AE RID: 5550
		private WebBaseEventCollection _events;

		// Token: 0x040015AF RID: 5551
		private DateTime _lastNotification;

		// Token: 0x040015B0 RID: 5552
		private int _eventsDiscardedSinceLastNotification;

		// Token: 0x040015B1 RID: 5553
		private int _eventsInBuffer;

		// Token: 0x040015B2 RID: 5554
		private int _notificationSequence;

		// Token: 0x040015B3 RID: 5555
		private EventNotificationType _notificationType;
	}
}
