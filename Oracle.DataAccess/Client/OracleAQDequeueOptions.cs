using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000132 RID: 306
	public sealed class OracleAQDequeueOptions : ICloneable
	{
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x00079112 File Offset: 0x00078112
		// (set) Token: 0x06000C37 RID: 3127 RVA: 0x0007911A File Offset: 0x0007811A
		public OracleAQDequeueMode DequeueMode
		{
			get
			{
				return this.m_dequeueMode;
			}
			set
			{
				if (value != OracleAQDequeueMode.Browse && value != OracleAQDequeueMode.Locked && value != OracleAQDequeueMode.Remove && value != OracleAQDequeueMode.RemoveNoData)
				{
					throw new ArgumentOutOfRangeException("DequeueMode");
				}
				if (value != this.m_dequeueMode)
				{
					this.m_dequeueMode = value;
				}
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x00079147 File Offset: 0x00078147
		// (set) Token: 0x06000C39 RID: 3129 RVA: 0x0007914F File Offset: 0x0007814F
		public OracleAQNavigationMode NavigationMode
		{
			get
			{
				return this.m_navigationMode;
			}
			set
			{
				if (value != OracleAQNavigationMode.FirstMessage && value != OracleAQNavigationMode.NextMessage && value != OracleAQNavigationMode.NextTransaction && value != OracleAQNavigationMode.FirstMessageMultiGroup && value != OracleAQNavigationMode.NextMessageMultiGroup)
				{
					throw new ArgumentOutOfRangeException("NavigationMode");
				}
				if (value != this.m_navigationMode)
				{
					this.m_navigationMode = value;
				}
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x00079180 File Offset: 0x00078180
		// (set) Token: 0x06000C3B RID: 3131 RVA: 0x00079188 File Offset: 0x00078188
		public OracleAQMessageDeliveryMode DeliveryMode
		{
			get
			{
				return this.m_deliveryMode;
			}
			set
			{
				if (value != OracleAQMessageDeliveryMode.Buffered && value != OracleAQMessageDeliveryMode.Persistent && value != OracleAQMessageDeliveryMode.PersistentOrBuffered)
				{
					throw new ArgumentOutOfRangeException("DeliveryMode");
				}
				if (value != this.m_deliveryMode)
				{
					this.m_deliveryMode = value;
				}
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x000791B1 File Offset: 0x000781B1
		// (set) Token: 0x06000C3D RID: 3133 RVA: 0x000791B9 File Offset: 0x000781B9
		public OracleAQVisibilityMode Visibility
		{
			get
			{
				return this.m_visibility;
			}
			set
			{
				if (value != OracleAQVisibilityMode.Immediate && value != OracleAQVisibilityMode.OnCommit)
				{
					throw new ArgumentOutOfRangeException("Visibility");
				}
				if (value != this.m_visibility)
				{
					this.m_visibility = value;
				}
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x000791DE File Offset: 0x000781DE
		// (set) Token: 0x06000C3F RID: 3135 RVA: 0x000791E6 File Offset: 0x000781E6
		public int Wait
		{
			get
			{
				return this.m_wait;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("Wait");
				}
				if (value != this.m_wait)
				{
					this.m_wait = value;
				}
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x00079207 File Offset: 0x00078207
		// (set) Token: 0x06000C41 RID: 3137 RVA: 0x0007920F File Offset: 0x0007820F
		public byte[] MessageId
		{
			get
			{
				return this.m_messageId;
			}
			set
			{
				if (value != this.m_messageId)
				{
					this.m_messageId = value;
				}
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x00079221 File Offset: 0x00078221
		// (set) Token: 0x06000C43 RID: 3139 RVA: 0x00079229 File Offset: 0x00078229
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
					this.m_correlation = value;
				}
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x00079240 File Offset: 0x00078240
		// (set) Token: 0x06000C45 RID: 3141 RVA: 0x00079248 File Offset: 0x00078248
		public string ConsumerName
		{
			get
			{
				return this.m_consumerName;
			}
			set
			{
				if (value != this.m_consumerName)
				{
					this.m_consumerName = value;
				}
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x0007925F File Offset: 0x0007825F
		// (set) Token: 0x06000C47 RID: 3143 RVA: 0x00079267 File Offset: 0x00078267
		public bool ProviderSpecificType
		{
			get
			{
				return this.m_providerSpecificType;
			}
			set
			{
				this.m_providerSpecificType = value;
			}
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x00079270 File Offset: 0x00078270
		static OracleAQDequeueOptions()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x000792AC File Offset: 0x000782AC
		public object Clone()
		{
			return new OracleAQDequeueOptions
			{
				m_consumerName = this.m_consumerName,
				m_correlation = this.m_correlation,
				m_deliveryMode = this.m_deliveryMode,
				m_dequeueMode = this.m_dequeueMode,
				m_messageId = this.m_messageId,
				m_navigationMode = this.m_navigationMode,
				m_visibility = this.m_visibility,
				m_wait = this.m_wait
			};
		}

		// Token: 0x040009A1 RID: 2465
		internal OracleAQDequeueMode m_dequeueMode = OracleAQDequeueMode.Remove;

		// Token: 0x040009A2 RID: 2466
		internal OracleAQNavigationMode m_navigationMode = OracleAQNavigationMode.NextMessage;

		// Token: 0x040009A3 RID: 2467
		internal OracleAQVisibilityMode m_visibility = OracleAQVisibilityMode.OnCommit;

		// Token: 0x040009A4 RID: 2468
		internal int m_wait = -1;

		// Token: 0x040009A5 RID: 2469
		internal OracleAQMessageDeliveryMode m_deliveryMode = OracleAQMessageDeliveryMode.Persistent;

		// Token: 0x040009A6 RID: 2470
		internal string m_consumerName;

		// Token: 0x040009A7 RID: 2471
		internal byte[] m_messageId;

		// Token: 0x040009A8 RID: 2472
		internal string m_correlation;

		// Token: 0x040009A9 RID: 2473
		internal bool m_providerSpecificType;
	}
}
