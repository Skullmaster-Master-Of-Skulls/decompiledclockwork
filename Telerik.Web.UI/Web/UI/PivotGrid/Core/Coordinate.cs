using System;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000D35 RID: 3381
	public struct Coordinate : IEquatable<Coordinate>
	{
		// Token: 0x06007DA9 RID: 32169 RVA: 0x001CBE91 File Offset: 0x001CA091
		public Coordinate(IGroup rowGroup, IGroup columnGroup)
		{
			this = default(Coordinate);
			this.RowGroup = rowGroup;
			this.ColumnGroup = columnGroup;
		}

		// Token: 0x17002814 RID: 10260
		// (get) Token: 0x06007DAA RID: 32170 RVA: 0x001CBEA8 File Offset: 0x001CA0A8
		// (set) Token: 0x06007DAB RID: 32171 RVA: 0x001CBEB0 File Offset: 0x001CA0B0
		public IGroup RowGroup { get; set; }

		// Token: 0x17002815 RID: 10261
		// (get) Token: 0x06007DAC RID: 32172 RVA: 0x001CBEB9 File Offset: 0x001CA0B9
		// (set) Token: 0x06007DAD RID: 32173 RVA: 0x001CBEC1 File Offset: 0x001CA0C1
		public IGroup ColumnGroup { get; set; }

		// Token: 0x06007DAE RID: 32174 RVA: 0x001CBECC File Offset: 0x001CA0CC
		public bool Equals(Coordinate other)
		{
			return (this.RowGroup == other.RowGroup || this.RowGroup.Equals(other.RowGroup)) && (this.ColumnGroup == other.ColumnGroup || this.ColumnGroup.Equals(other.ColumnGroup));
		}

		// Token: 0x06007DAF RID: 32175 RVA: 0x001CBF24 File Offset: 0x001CA124
		public override bool Equals(object obj)
		{
			if (obj is Coordinate)
			{
				Coordinate other = (Coordinate)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06007DB0 RID: 32176 RVA: 0x001CBF49 File Offset: 0x001CA149
		public override int GetHashCode()
		{
			return this.RowGroup.GetHashCode() * 8821 + this.ColumnGroup.GetHashCode() * 8741;
		}

		// Token: 0x06007DB1 RID: 32177 RVA: 0x001CBF6E File Offset: 0x001CA16E
		public static bool operator ==(Coordinate left, Coordinate right)
		{
			return left.Equals(right);
		}

		// Token: 0x06007DB2 RID: 32178 RVA: 0x001CBF78 File Offset: 0x001CA178
		public static bool operator !=(Coordinate left, Coordinate right)
		{
			return !left.Equals(right);
		}
	}
}
