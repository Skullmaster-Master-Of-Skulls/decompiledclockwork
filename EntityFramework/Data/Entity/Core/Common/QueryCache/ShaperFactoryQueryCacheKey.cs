using System;
using System.Data.Entity.Core.Objects;

namespace System.Data.Entity.Core.Common.QueryCache
{
	// Token: 0x020002FD RID: 765
	internal class ShaperFactoryQueryCacheKey<T> : QueryCacheKey
	{
		// Token: 0x06001AE5 RID: 6885 RVA: 0x00086464 File Offset: 0x00084664
		internal ShaperFactoryQueryCacheKey(string columnMapKey, MergeOption mergeOption, bool streaming, bool isValueLayer)
		{
			this._columnMapKey = columnMapKey;
			this._mergeOption = mergeOption;
			this._isValueLayer = isValueLayer;
			this._streaming = streaming;
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x0008648C File Offset: 0x0008468C
		public override bool Equals(object obj)
		{
			ShaperFactoryQueryCacheKey<T> shaperFactoryQueryCacheKey = obj as ShaperFactoryQueryCacheKey<T>;
			return shaperFactoryQueryCacheKey != null && (this._columnMapKey.Equals(shaperFactoryQueryCacheKey._columnMapKey, QueryCacheKey._stringComparison) && this._mergeOption == shaperFactoryQueryCacheKey._mergeOption && this._isValueLayer == shaperFactoryQueryCacheKey._isValueLayer) && this._streaming == shaperFactoryQueryCacheKey._streaming;
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x000864E9 File Offset: 0x000846E9
		public override int GetHashCode()
		{
			return this._columnMapKey.GetHashCode();
		}

		// Token: 0x0400096B RID: 2411
		private readonly string _columnMapKey;

		// Token: 0x0400096C RID: 2412
		private readonly MergeOption _mergeOption;

		// Token: 0x0400096D RID: 2413
		private readonly bool _isValueLayer;

		// Token: 0x0400096E RID: 2414
		private readonly bool _streaming;
	}
}
