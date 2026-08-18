using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000117 RID: 279
	public class OracleAQMessageAvailableEventArgs
	{
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x00070549 File Offset: 0x0006F549
		public string QueueName
		{
			get
			{
				return this.m_queueName;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x00070551 File Offset: 0x0006F551
		public string ConsumerName
		{
			get
			{
				return this.m_consumerName;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x00070559 File Offset: 0x0006F559
		public byte[][] MessageId
		{
			get
			{
				return this.m_messageId;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x00070561 File Offset: 0x0006F561
		public string Correlation
		{
			get
			{
				return this.m_correlation;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x00070569 File Offset: 0x0006F569
		public int Delay
		{
			get
			{
				return this.m_delay;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x00070571 File Offset: 0x0006F571
		public string ExceptionQueue
		{
			get
			{
				return this.m_exceptionQueue;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x00070579 File Offset: 0x0006F579
		public int Expiration
		{
			get
			{
				return this.m_expiration;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x00070581 File Offset: 0x0006F581
		public int Priority
		{
			get
			{
				return this.m_priority;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x00070589 File Offset: 0x0006F589
		public DateTime EnqueueTime
		{
			get
			{
				return this.m_enqueueTime;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x00070591 File Offset: 0x0006F591
		public OracleAQMessageState State
		{
			get
			{
				return this.m_state;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x00070599 File Offset: 0x0006F599
		public OracleAQMessageDeliveryMode DeliveryMode
		{
			get
			{
				return this.m_deliveryMode;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x000705A1 File Offset: 0x0006F5A1
		public OracleAQAgent SenderId
		{
			get
			{
				return this.m_senderId;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x000705A9 File Offset: 0x0006F5A9
		public byte[] OriginalMessageId
		{
			get
			{
				return this.m_originalMessageId;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x000705B1 File Offset: 0x0006F5B1
		public OracleAQNotificationType NotificationType
		{
			get
			{
				return this.m_notificationType;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x000705B9 File Offset: 0x0006F5B9
		public int AvailableMessages
		{
			get
			{
				return this.m_availableMessages;
			}
		}

		// Token: 0x0400090D RID: 2317
		internal string m_queueName;

		// Token: 0x0400090E RID: 2318
		internal string m_consumerName;

		// Token: 0x0400090F RID: 2319
		internal byte[][] m_messageId;

		// Token: 0x04000910 RID: 2320
		internal string m_correlation;

		// Token: 0x04000911 RID: 2321
		internal int m_delay;

		// Token: 0x04000912 RID: 2322
		internal string m_exceptionQueue;

		// Token: 0x04000913 RID: 2323
		internal int m_expiration;

		// Token: 0x04000914 RID: 2324
		internal int m_priority;

		// Token: 0x04000915 RID: 2325
		internal DateTime m_enqueueTime;

		// Token: 0x04000916 RID: 2326
		internal OracleAQMessageState m_state;

		// Token: 0x04000917 RID: 2327
		internal OracleAQMessageDeliveryMode m_deliveryMode;

		// Token: 0x04000918 RID: 2328
		internal OracleAQAgent m_senderId;

		// Token: 0x04000919 RID: 2329
		internal byte[] m_originalMessageId;

		// Token: 0x0400091A RID: 2330
		internal OracleAQNotificationType m_notificationType;

		// Token: 0x0400091B RID: 2331
		internal int m_availableMessages;
	}
}
