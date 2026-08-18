using System;

namespace System.Data.Entity.Core.Common.QueryCache
{
	// Token: 0x020002F6 RID: 758
	internal abstract class QueryCacheKey
	{
		// Token: 0x06001ABA RID: 6842 RVA: 0x00085755 File Offset: 0x00083955
		protected QueryCacheKey()
		{
			this._hitCount = 1U;
		}

		// Token: 0x06001ABB RID: 6843
		public abstract override bool Equals(object obj);

		// Token: 0x06001ABC RID: 6844
		public abstract override int GetHashCode();

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001ABD RID: 6845 RVA: 0x00085764 File Offset: 0x00083964
		// (set) Token: 0x06001ABE RID: 6846 RVA: 0x0008576C File Offset: 0x0008396C
		internal uint HitCount
		{
			get
			{
				return this._hitCount;
			}
			set
			{
				this._hitCount = value;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06001ABF RID: 6847 RVA: 0x00085775 File Offset: 0x00083975
		// (set) Token: 0x06001AC0 RID: 6848 RVA: 0x0008577D File Offset: 0x0008397D
		internal int AgingIndex { get; set; }

		// Token: 0x06001AC1 RID: 6849 RVA: 0x00085786 File Offset: 0x00083986
		internal void UpdateHit()
		{
			if (4294967295U != this._hitCount)
			{
				this._hitCount += 1U;
			}
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x0008579F File Offset: 0x0008399F
		protected virtual bool Equals(string s, string t)
		{
			return string.Equals(s, t, QueryCacheKey._stringComparison);
		}

		// Token: 0x04000944 RID: 2372
		protected const int EstimatedParameterStringSize = 20;

		// Token: 0x04000945 RID: 2373
		private uint _hitCount;

		// Token: 0x04000946 RID: 2374
		protected static StringComparison _stringComparison = StringComparison.Ordinal;
	}
}
