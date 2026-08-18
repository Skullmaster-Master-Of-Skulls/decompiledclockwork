using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x020005BE RID: 1470
	internal sealed class SpanIndex
	{
		// Token: 0x06003AEB RID: 15083 RVA: 0x00117B2C File Offset: 0x00115D2C
		internal void AddSpannedRowType(RowType spannedRowType, TypeUsage originalRowType)
		{
			if (this._rowMap == null)
			{
				this._rowMap = new Dictionary<RowType, TypeUsage>(SpanIndex.RowTypeEqualityComparer.Instance);
			}
			this._rowMap[spannedRowType] = originalRowType;
		}

		// Token: 0x06003AEC RID: 15084 RVA: 0x00117B54 File Offset: 0x00115D54
		internal TypeUsage GetSpannedRowType(RowType spannedRowType)
		{
			TypeUsage result;
			if (this._rowMap != null && this._rowMap.TryGetValue(spannedRowType, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06003AED RID: 15085 RVA: 0x00117B7C File Offset: 0x00115D7C
		internal bool HasSpanMap(RowType spanRowType)
		{
			return this._spanMap != null && this._spanMap.ContainsKey(spanRowType);
		}

		// Token: 0x06003AEE RID: 15086 RVA: 0x00117B94 File Offset: 0x00115D94
		internal void AddSpanMap(RowType rowType, Dictionary<int, AssociationEndMember> columnMap)
		{
			if (this._spanMap == null)
			{
				this._spanMap = new Dictionary<RowType, Dictionary<int, AssociationEndMember>>(SpanIndex.RowTypeEqualityComparer.Instance);
			}
			this._spanMap[rowType] = columnMap;
		}

		// Token: 0x06003AEF RID: 15087 RVA: 0x00117BBC File Offset: 0x00115DBC
		internal Dictionary<int, AssociationEndMember> GetSpanMap(RowType rowType)
		{
			Dictionary<int, AssociationEndMember> result = null;
			if (this._spanMap != null && this._spanMap.TryGetValue(rowType, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x04001645 RID: 5701
		private Dictionary<RowType, Dictionary<int, AssociationEndMember>> _spanMap;

		// Token: 0x04001646 RID: 5702
		private Dictionary<RowType, TypeUsage> _rowMap;

		// Token: 0x020005BF RID: 1471
		private sealed class RowTypeEqualityComparer : IEqualityComparer<RowType>
		{
			// Token: 0x06003AF1 RID: 15089 RVA: 0x00117BEE File Offset: 0x00115DEE
			private RowTypeEqualityComparer()
			{
			}

			// Token: 0x06003AF2 RID: 15090 RVA: 0x00117BF6 File Offset: 0x00115DF6
			public bool Equals(RowType x, RowType y)
			{
				return x != null && y != null && x.EdmEquals(y);
			}

			// Token: 0x06003AF3 RID: 15091 RVA: 0x00117C07 File Offset: 0x00115E07
			public int GetHashCode(RowType obj)
			{
				return obj.Identity.GetHashCode();
			}

			// Token: 0x04001647 RID: 5703
			internal static readonly SpanIndex.RowTypeEqualityComparer Instance = new SpanIndex.RowTypeEqualityComparer();
		}
	}
}
