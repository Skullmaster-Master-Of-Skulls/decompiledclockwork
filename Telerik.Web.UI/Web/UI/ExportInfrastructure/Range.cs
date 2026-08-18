using System;
using System.Drawing;
using Telerik.Web.UI.Export;

namespace Telerik.Web.UI.ExportInfrastructure
{
	// Token: 0x02000A4F RID: 2639
	public class Range
	{
		// Token: 0x170021B7 RID: 8631
		// (get) Token: 0x06006645 RID: 26181 RVA: 0x0017E5BE File Offset: 0x0017C7BE
		// (set) Token: 0x06006646 RID: 26182 RVA: 0x0017E5C6 File Offset: 0x0017C7C6
		public Point Start
		{
			get
			{
				return this._start;
			}
			set
			{
				this._start = value;
			}
		}

		// Token: 0x170021B8 RID: 8632
		// (get) Token: 0x06006647 RID: 26183 RVA: 0x0017E5CF File Offset: 0x0017C7CF
		// (set) Token: 0x06006648 RID: 26184 RVA: 0x0017E5D7 File Offset: 0x0017C7D7
		public Point End
		{
			get
			{
				return this._end;
			}
			set
			{
				this._end = value;
			}
		}

		// Token: 0x06006649 RID: 26185 RVA: 0x0017E5E0 File Offset: 0x0017C7E0
		public Range(Point startCell, Point endCell)
		{
			this.Start = startCell;
			this.End = endCell;
		}

		// Token: 0x0600664A RID: 26186 RVA: 0x0017E5F6 File Offset: 0x0017C7F6
		public Range(string startCell, string endCell)
		{
			if (!Utils.IsValidExcelCellIndex(startCell) || !Utils.IsValidExcelCellIndex(endCell))
			{
				throw new ArgumentException("Range arguments must be valid Excel style indices or Point values!");
			}
			this.Start = Utils.ConvertExcelCellIndexToPoint(startCell);
			this.End = Utils.ConvertExcelCellIndexToPoint(endCell);
		}

		// Token: 0x0600664B RID: 26187 RVA: 0x0017E631 File Offset: 0x0017C831
		public static bool operator ==(Range range1, Range range2)
		{
			return object.ReferenceEquals(range1, range2) || (range1 != null && range2 != null && range1.Start == range2.Start && range1.End == range2.End);
		}

		// Token: 0x0600664C RID: 26188 RVA: 0x0017E66C File Offset: 0x0017C86C
		public static bool operator !=(Range range1, Range range2)
		{
			return !(range1 == range2);
		}

		// Token: 0x0600664D RID: 26189 RVA: 0x0017E678 File Offset: 0x0017C878
		public override bool Equals(object obj)
		{
			Range range = obj as Range;
			return !(range == null) && this.Start == range.Start && this.End == range.End;
		}

		// Token: 0x0600664E RID: 26190 RVA: 0x0017E6BD File Offset: 0x0017C8BD
		public bool Equals(Range other)
		{
			return other != null && this.Start == other.Start && this.End == other.End;
		}

		// Token: 0x0600664F RID: 26191 RVA: 0x0017E6EC File Offset: 0x0017C8EC
		public override int GetHashCode()
		{
			return this.Start.X ^ this.Start.Y ^ this.End.X ^ this.End.Y;
		}

		// Token: 0x06006650 RID: 26192 RVA: 0x0017E734 File Offset: 0x0017C934
		public override string ToString()
		{
			return string.Format("{0}:{1}", Utils.ConvertPointToExcelCellIndex(this.Start), Utils.ConvertPointToExcelCellIndex(this.End));
		}

		// Token: 0x040018C3 RID: 6339
		private Point _start;

		// Token: 0x040018C4 RID: 6340
		private Point _end;
	}
}
