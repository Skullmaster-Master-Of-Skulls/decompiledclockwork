using System;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000D47 RID: 3399
	public sealed class CellAggregateValue : IEquatable<CellAggregateValue>
	{
		// Token: 0x06007E7C RID: 32380 RVA: 0x001CF99C File Offset: 0x001CDB9C
		internal CellAggregateValue()
		{
		}

		// Token: 0x17002850 RID: 10320
		// (get) Token: 0x06007E7D RID: 32381 RVA: 0x001CF9A4 File Offset: 0x001CDBA4
		// (set) Token: 0x06007E7E RID: 32382 RVA: 0x001CF9AC File Offset: 0x001CDBAC
		public IGroup RowGroup { get; internal set; }

		// Token: 0x17002851 RID: 10321
		// (get) Token: 0x06007E7F RID: 32383 RVA: 0x001CF9B5 File Offset: 0x001CDBB5
		// (set) Token: 0x06007E80 RID: 32384 RVA: 0x001CF9BD File Offset: 0x001CDBBD
		public IGroup ColumnGroup { get; internal set; }

		// Token: 0x17002852 RID: 10322
		// (get) Token: 0x06007E81 RID: 32385 RVA: 0x001CF9C6 File Offset: 0x001CDBC6
		// (set) Token: 0x06007E82 RID: 32386 RVA: 0x001CF9CE File Offset: 0x001CDBCE
		public IAggregateDescription Description { get; internal set; }

		// Token: 0x17002853 RID: 10323
		// (get) Token: 0x06007E83 RID: 32387 RVA: 0x001CF9D7 File Offset: 0x001CDBD7
		// (set) Token: 0x06007E84 RID: 32388 RVA: 0x001CF9DF File Offset: 0x001CDBDF
		public object Value { get; internal set; }

		// Token: 0x17002854 RID: 10324
		// (get) Token: 0x06007E85 RID: 32389 RVA: 0x001CF9E8 File Offset: 0x001CDBE8
		// (set) Token: 0x06007E86 RID: 32390 RVA: 0x001CF9F0 File Offset: 0x001CDBF0
		public string FormattedValue { get; internal set; }

		// Token: 0x17002855 RID: 10325
		// (get) Token: 0x06007E87 RID: 32391 RVA: 0x001CF9F9 File Offset: 0x001CDBF9
		internal bool IsVoidCell
		{
			get
			{
				return this.Description == null && this.Value == null;
			}
		}

		// Token: 0x17002856 RID: 10326
		// (get) Token: 0x06007E88 RID: 32392 RVA: 0x001CFA0E File Offset: 0x001CDC0E
		internal bool IsTextEmpty
		{
			get
			{
				return this.Value == null && string.IsNullOrEmpty(this.FormattedValue);
			}
		}

		// Token: 0x06007E89 RID: 32393 RVA: 0x001CFA25 File Offset: 0x001CDC25
		public override string ToString()
		{
			return this.FormattedValue;
		}

		// Token: 0x06007E8A RID: 32394 RVA: 0x001CFA30 File Offset: 0x001CDC30
		public override bool Equals(object obj)
		{
			CellAggregateValue cellAggregateValue = obj as CellAggregateValue;
			return cellAggregateValue != null && this.Equals(cellAggregateValue);
		}

		// Token: 0x06007E8B RID: 32395 RVA: 0x001CFA50 File Offset: 0x001CDC50
		public bool Equals(CellAggregateValue other)
		{
			return object.Equals(this.Value, other.Value) && this.RowGroup == other.RowGroup && this.ColumnGroup == other.ColumnGroup && this.FormattedValue == other.FormattedValue && this.Description == other.Description;
		}

		// Token: 0x06007E8C RID: 32396 RVA: 0x001CFAB0 File Offset: 0x001CDCB0
		public override int GetHashCode()
		{
			return ((this.Description != null) ? this.Description.GetHashCode() : 0) * 104743 + this.Value.GetHashCode() * 104759 + (string.IsNullOrWhiteSpace(this.FormattedValue) ? 0 : this.FormattedValue.GetHashCode());
		}
	}
}
