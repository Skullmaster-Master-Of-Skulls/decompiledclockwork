using System;
using System.Xml;
using Oracle.DataAccess.Types;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000112 RID: 274
	public sealed class OracleAQMessage
	{
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x0006F221 File Offset: 0x0006E221
		public int DequeueAttempts
		{
			get
			{
				return this.m_deqAttempts;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x0006F229 File Offset: 0x0006E229
		// (set) Token: 0x06000AB9 RID: 2745 RVA: 0x0006F231 File Offset: 0x0006E231
		public string Correlation
		{
			get
			{
				return this.m_correlation;
			}
			set
			{
				if (value != this.m_correlation)
				{
					this.m_msgPropsModified = true;
					this.m_correlation = value;
				}
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x0006F24F File Offset: 0x0006E24F
		public byte[] OriginalMessageId
		{
			get
			{
				return this.m_originalMessageId;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x0006F257 File Offset: 0x0006E257
		public OracleAQMessageDeliveryMode DeliveryMode
		{
			get
			{
				return this.m_deliveryMode;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x0006F25F File Offset: 0x0006E25F
		// (set) Token: 0x06000ABD RID: 2749 RVA: 0x0006F267 File Offset: 0x0006E267
		public int Delay
		{
			get
			{
				return this.m_delay;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("Delay");
				}
				if (value != this.m_delay)
				{
					this.m_msgPropsModified = true;
					this.m_delay = value;
				}
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x0006F28F File Offset: 0x0006E28F
		// (set) Token: 0x06000ABF RID: 2751 RVA: 0x0006F297 File Offset: 0x0006E297
		public string ExceptionQueue
		{
			get
			{
				return this.m_exceptionQueue;
			}
			set
			{
				if (value != this.m_exceptionQueue)
				{
					this.m_msgPropsModified = true;
					this.m_exceptionQueue = value;
				}
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x0006F2B5 File Offset: 0x0006E2B5
		// (set) Token: 0x06000AC1 RID: 2753 RVA: 0x0006F2BD File Offset: 0x0006E2BD
		public int Expiration
		{
			get
			{
				return this.m_expiration;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("Expiration");
				}
				if (value != this.m_expiration)
				{
					this.m_msgPropsModified = true;
					this.m_expiration = value;
				}
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x0006F2E5 File Offset: 0x0006E2E5
		// (set) Token: 0x06000AC3 RID: 2755 RVA: 0x0006F2ED File Offset: 0x0006E2ED
		public int Priority
		{
			get
			{
				return this.m_priority;
			}
			set
			{
				if (value != this.m_priority)
				{
					this.m_msgPropsModified = true;
					this.m_priority = value;
				}
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x0006F306 File Offset: 0x0006E306
		// (set) Token: 0x06000AC5 RID: 2757 RVA: 0x0006F30E File Offset: 0x0006E30E
		public OracleAQAgent[] Recipients
		{
			get
			{
				return this.m_recipients;
			}
			set
			{
				if (value != this.m_recipients)
				{
					this.m_msgPropsModified = true;
					this.m_recipients = value;
				}
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x0006F327 File Offset: 0x0006E327
		public string TransactionGroup
		{
			get
			{
				return this.m_transactionGroup;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0006F32F File Offset: 0x0006E32F
		public byte[] MessageId
		{
			get
			{
				return this.m_messageId;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x0006F337 File Offset: 0x0006E337
		public DateTime EnqueueTime
		{
			get
			{
				return this.m_enqueueTime;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x0006F33F File Offset: 0x0006E33F
		public OracleAQMessageState State
		{
			get
			{
				return this.m_state;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x0006F347 File Offset: 0x0006E347
		// (set) Token: 0x06000ACB RID: 2763 RVA: 0x0006F350 File Offset: 0x0006E350
		public object Payload
		{
			get
			{
				return this.m_payload;
			}
			set
			{
				if (value is OracleXmlType || value is XmlReader || value is string || value is byte[] || value is OracleBinary || value is IOracleCustomType || value == null)
				{
					this.m_payload = value;
					return;
				}
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					"Payload"
				}));
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x0006F3B7 File Offset: 0x0006E3B7
		// (set) Token: 0x06000ACD RID: 2765 RVA: 0x0006F3BF File Offset: 0x0006E3BF
		public OracleAQAgent SenderId
		{
			get
			{
				return this.m_senderId;
			}
			set
			{
				this.m_msgPropsModified = true;
				this.m_senderId = value;
			}
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0006F3CF File Offset: 0x0006E3CF
		static OracleAQMessage()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0006F3DD File Offset: 0x0006E3DD
		public OracleAQMessage()
		{
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0006F3F4 File Offset: 0x0006E3F4
		public OracleAQMessage(object payload) : this()
		{
			if (payload is OracleXmlType || payload is XmlReader || payload is string || payload is byte[] || payload is OracleBinary || payload is IOracleCustomType || payload == null)
			{
				this.m_payload = payload;
				return;
			}
			throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
			{
				"payload"
			}));
		}

		// Token: 0x040008E1 RID: 2273
		internal object m_payload;

		// Token: 0x040008E2 RID: 2274
		internal OracleAQMessageDeliveryMode m_deliveryMode = OracleAQMessageDeliveryMode.Persistent;

		// Token: 0x040008E3 RID: 2275
		internal int m_deqAttempts;

		// Token: 0x040008E4 RID: 2276
		internal string m_correlation;

		// Token: 0x040008E5 RID: 2277
		internal string m_exceptionQueue;

		// Token: 0x040008E6 RID: 2278
		internal int m_expiration = -1;

		// Token: 0x040008E7 RID: 2279
		internal int m_delay;

		// Token: 0x040008E8 RID: 2280
		internal OracleAQAgent[] m_recipients;

		// Token: 0x040008E9 RID: 2281
		internal OracleAQAgent m_senderId;

		// Token: 0x040008EA RID: 2282
		internal string m_transactionGroup;

		// Token: 0x040008EB RID: 2283
		internal byte[] m_messageId;

		// Token: 0x040008EC RID: 2284
		internal DateTime m_enqueueTime;

		// Token: 0x040008ED RID: 2285
		internal OracleAQMessageState m_state;

		// Token: 0x040008EE RID: 2286
		internal int m_priority;

		// Token: 0x040008EF RID: 2287
		internal byte[] m_originalMessageId;

		// Token: 0x040008F0 RID: 2288
		internal bool m_msgPropsModified;
	}
}
