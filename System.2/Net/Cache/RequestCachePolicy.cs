using System;

namespace System.Net.Cache
{
	// Token: 0x02000312 RID: 786
	public class RequestCachePolicy
	{
		// Token: 0x06001C19 RID: 7193 RVA: 0x00085EA1 File Offset: 0x000840A1
		public RequestCachePolicy() : this(RequestCacheLevel.Default)
		{
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x00085EAA File Offset: 0x000840AA
		public RequestCachePolicy(RequestCacheLevel level)
		{
			if (level < RequestCacheLevel.Default || level > RequestCacheLevel.NoCacheNoStore)
			{
				throw new ArgumentOutOfRangeException("level");
			}
			this.m_Level = level;
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001C1B RID: 7195 RVA: 0x00085ECC File Offset: 0x000840CC
		public RequestCacheLevel Level
		{
			get
			{
				return this.m_Level;
			}
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x00085ED4 File Offset: 0x000840D4
		public override string ToString()
		{
			return "Level:" + this.m_Level.ToString();
		}

		// Token: 0x04001B5F RID: 7007
		private RequestCacheLevel m_Level;
	}
}
