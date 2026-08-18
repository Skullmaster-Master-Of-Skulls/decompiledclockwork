using System;

namespace System.Net.Cache
{
	// Token: 0x0200056C RID: 1388
	public class RequestCachePolicy
	{
		// Token: 0x06002A99 RID: 10905 RVA: 0x000B5031 File Offset: 0x000B4031
		public RequestCachePolicy() : this(RequestCacheLevel.Default)
		{
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x000B503A File Offset: 0x000B403A
		public RequestCachePolicy(RequestCacheLevel level)
		{
			if (level < RequestCacheLevel.Default || level > RequestCacheLevel.NoCacheNoStore)
			{
				throw new ArgumentOutOfRangeException("level");
			}
			this.m_Level = level;
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06002A9B RID: 10907 RVA: 0x000B505C File Offset: 0x000B405C
		public RequestCacheLevel Level
		{
			get
			{
				return this.m_Level;
			}
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x000B5064 File Offset: 0x000B4064
		public override string ToString()
		{
			return "Level:" + this.m_Level.ToString();
		}

		// Token: 0x04002920 RID: 10528
		private RequestCacheLevel m_Level;
	}
}
