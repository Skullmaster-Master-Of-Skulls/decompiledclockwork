using System;

namespace log4net.Util
{
	// Token: 0x020000F9 RID: 249
	public class FormattingInfo
	{
		// Token: 0x0600070A RID: 1802 RVA: 0x0001627B File Offset: 0x0001447B
		public FormattingInfo()
		{
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00016295 File Offset: 0x00014495
		public FormattingInfo(int min, int max, bool leftAlign)
		{
			this.m_min = min;
			this.m_max = max;
			this.m_leftAlign = leftAlign;
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x000162C4 File Offset: 0x000144C4
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x000162CC File Offset: 0x000144CC
		public int Min
		{
			get
			{
				return this.m_min;
			}
			set
			{
				this.m_min = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x000162D5 File Offset: 0x000144D5
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x000162DD File Offset: 0x000144DD
		public int Max
		{
			get
			{
				return this.m_max;
			}
			set
			{
				this.m_max = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x000162E6 File Offset: 0x000144E6
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x000162EE File Offset: 0x000144EE
		public bool LeftAlign
		{
			get
			{
				return this.m_leftAlign;
			}
			set
			{
				this.m_leftAlign = value;
			}
		}

		// Token: 0x040002AD RID: 685
		private int m_min = -1;

		// Token: 0x040002AE RID: 686
		private int m_max = int.MaxValue;

		// Token: 0x040002AF RID: 687
		private bool m_leftAlign;
	}
}
