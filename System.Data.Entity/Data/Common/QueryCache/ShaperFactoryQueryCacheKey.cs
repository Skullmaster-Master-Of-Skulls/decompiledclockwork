using System;
using System.Data.Objects;

namespace System.Data.Common.QueryCache
{
	// Token: 0x020003DD RID: 989
	internal class ShaperFactoryQueryCacheKey<T> : QueryCacheKey
	{
		// Token: 0x0600352E RID: 13614 RVA: 0x000CF1F6 File Offset: 0x000CD3F6
		internal ShaperFactoryQueryCacheKey(string columnMapKey, MergeOption mergeOption, bool isValueLayer)
		{
			this._columnMapKey = columnMapKey;
			this._mergeOption = mergeOption;
			this._isValueLayer = isValueLayer;
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x000CF214 File Offset: 0x000CD414
		public override bool Equals(object obj)
		{
			ShaperFactoryQueryCacheKey<T> shaperFactoryQueryCacheKey = obj as ShaperFactoryQueryCacheKey<T>;
			return shaperFactoryQueryCacheKey != null && (this._columnMapKey.Equals(shaperFactoryQueryCacheKey._columnMapKey, QueryCacheKey._stringComparison) && this._mergeOption == shaperFactoryQueryCacheKey._mergeOption) && this._isValueLayer == shaperFactoryQueryCacheKey._isValueLayer;
		}

		// Token: 0x06003530 RID: 13616 RVA: 0x000CF263 File Offset: 0x000CD463
		public override int GetHashCode()
		{
			return this._columnMapKey.GetHashCode();
		}

		// Token: 0x04001786 RID: 6022
		private readonly string _columnMapKey;

		// Token: 0x04001787 RID: 6023
		private readonly MergeOption _mergeOption;

		// Token: 0x04001788 RID: 6024
		private readonly bool _isValueLayer;
	}
}
