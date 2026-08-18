using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;

namespace System.Data.Objects.Internal
{
	// Token: 0x02000164 RID: 356
	internal sealed class SpanIndex
	{
		// Token: 0x06001A99 RID: 6809 RVA: 0x00002050 File Offset: 0x00000250
		internal SpanIndex()
		{
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x0005B89A File Offset: 0x00059A9A
		internal void AddSpannedRowType(RowType spannedRowType, TypeUsage originalRowType)
		{
			if (this._rowMap == null)
			{
				this._rowMap = new Dictionary<RowType, TypeUsage>(SpanIndex.RowTypeEqualityComparer.Instance);
			}
			this._rowMap[spannedRowType] = originalRowType;
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x0005B8C4 File Offset: 0x00059AC4
		internal TypeUsage GetSpannedRowType(RowType spannedRowType)
		{
			TypeUsage result;
			if (this._rowMap != null && this._rowMap.TryGetValue(spannedRowType, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x0005B8EC File Offset: 0x00059AEC
		internal bool HasSpanMap(RowType spanRowType)
		{
			return this._spanMap != null && this._spanMap.ContainsKey(spanRowType);
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x0005B904 File Offset: 0x00059B04
		internal void AddSpanMap(RowType rowType, Dictionary<int, AssociationEndMember> columnMap)
		{
			if (this._spanMap == null)
			{
				this._spanMap = new Dictionary<RowType, Dictionary<int, AssociationEndMember>>(SpanIndex.RowTypeEqualityComparer.Instance);
			}
			this._spanMap[rowType] = columnMap;
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x0005B92C File Offset: 0x00059B2C
		internal Dictionary<int, AssociationEndMember> GetSpanMap(RowType rowType)
		{
			Dictionary<int, AssociationEndMember> result = null;
			if (this._spanMap != null && this._spanMap.TryGetValue(rowType, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x04000B27 RID: 2855
		private Dictionary<RowType, Dictionary<int, AssociationEndMember>> _spanMap;

		// Token: 0x04000B28 RID: 2856
		private Dictionary<RowType, TypeUsage> _rowMap;

		// Token: 0x020004B7 RID: 1207
		private sealed class RowTypeEqualityComparer : IEqualityComparer<RowType>
		{
			// Token: 0x06003C88 RID: 15496 RVA: 0x00002050 File Offset: 0x00000250
			private RowTypeEqualityComparer()
			{
			}

			// Token: 0x06003C89 RID: 15497 RVA: 0x000E3599 File Offset: 0x000E1799
			public bool Equals(RowType x, RowType y)
			{
				return x != null && y != null && x.EdmEquals(y);
			}

			// Token: 0x06003C8A RID: 15498 RVA: 0x000E35AA File Offset: 0x000E17AA
			public int GetHashCode(RowType obj)
			{
				return obj.Identity.GetHashCode();
			}

			// Token: 0x04001A77 RID: 6775
			internal static readonly SpanIndex.RowTypeEqualityComparer Instance = new SpanIndex.RowTypeEqualityComparer();
		}
	}
}
