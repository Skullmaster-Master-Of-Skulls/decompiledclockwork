using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x020000F4 RID: 244
	public sealed class OracleAQEnqueueOptions : ICloneable
	{
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x00058781 File Offset: 0x00057781
		// (set) Token: 0x060008EF RID: 2287 RVA: 0x00058789 File Offset: 0x00057789
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

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x000587AE File Offset: 0x000577AE
		// (set) Token: 0x060008F1 RID: 2289 RVA: 0x000587B6 File Offset: 0x000577B6
		public OracleAQMessageDeliveryMode DeliveryMode
		{
			get
			{
				return this.m_deliveryMode;
			}
			set
			{
				if (value != OracleAQMessageDeliveryMode.Buffered && value != OracleAQMessageDeliveryMode.Persistent)
				{
					throw new ArgumentOutOfRangeException("DeliveryMode");
				}
				if (value != this.m_deliveryMode)
				{
					this.m_deliveryMode = value;
				}
			}
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x000587DB File Offset: 0x000577DB
		static OracleAQEnqueueOptions()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00058800 File Offset: 0x00057800
		public object Clone()
		{
			return new OracleAQEnqueueOptions
			{
				m_deliveryMode = this.m_deliveryMode,
				m_visibility = this.m_visibility
			};
		}

		// Token: 0x040007A6 RID: 1958
		internal OracleAQVisibilityMode m_visibility = OracleAQVisibilityMode.OnCommit;

		// Token: 0x040007A7 RID: 1959
		internal OracleAQMessageDeliveryMode m_deliveryMode = OracleAQMessageDeliveryMode.Persistent;
	}
}
