using System;

namespace System.Data.Common.QueryCache
{
	// Token: 0x020003DF RID: 991
	internal abstract class QueryCacheKey
	{
		// Token: 0x0600353B RID: 13627 RVA: 0x000CF5E9 File Offset: 0x000CD7E9
		protected QueryCacheKey()
		{
			this._hitCount = 1U;
		}

		// Token: 0x0600353C RID: 13628
		public abstract override bool Equals(object obj);

		// Token: 0x0600353D RID: 13629
		public abstract override int GetHashCode();

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x0600353E RID: 13630 RVA: 0x000CF5F8 File Offset: 0x000CD7F8
		// (set) Token: 0x0600353F RID: 13631 RVA: 0x000CF600 File Offset: 0x000CD800
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

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06003540 RID: 13632 RVA: 0x000CF609 File Offset: 0x000CD809
		// (set) Token: 0x06003541 RID: 13633 RVA: 0x000CF611 File Offset: 0x000CD811
		internal int AgingIndex
		{
			get
			{
				return this._agingIndex;
			}
			set
			{
				this._agingIndex = value;
			}
		}

		// Token: 0x06003542 RID: 13634 RVA: 0x000CF61A File Offset: 0x000CD81A
		internal void UpdateHit()
		{
			if (4294967295U != this._hitCount)
			{
				this._hitCount += 1U;
			}
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x000CF633 File Offset: 0x000CD833
		protected virtual bool Equals(string s, string t)
		{
			return string.Equals(s, t, QueryCacheKey._stringComparison);
		}

		// Token: 0x04001792 RID: 6034
		protected const int EstimatedParameterStringSize = 20;

		// Token: 0x04001793 RID: 6035
		private uint _hitCount;

		// Token: 0x04001794 RID: 6036
		private int _agingIndex;

		// Token: 0x04001795 RID: 6037
		protected static StringComparison _stringComparison = StringComparison.Ordinal;
	}
}
