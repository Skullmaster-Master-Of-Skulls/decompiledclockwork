using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200007B RID: 123
	internal class SmiOrderProperty : SmiMetaDataProperty
	{
		// Token: 0x060005A2 RID: 1442 RVA: 0x000481D8 File Offset: 0x000475D8
		internal SmiOrderProperty(IList<SmiOrderProperty.SmiColumnOrder> columnOrders)
		{
			this._columns = new List<SmiOrderProperty.SmiColumnOrder>(columnOrders).AsReadOnly();
		}

		// Token: 0x170000BB RID: 187
		internal SmiOrderProperty.SmiColumnOrder this[int ordinal]
		{
			get
			{
				if (this._columns.Count <= ordinal)
				{
					return new SmiOrderProperty.SmiColumnOrder
					{
						Order = SortOrder.Unspecified,
						SortOrdinal = -1
					};
				}
				return this._columns[ordinal];
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00048240 File Offset: 0x00047640
		[Conditional("DEBUG")]
		internal void CheckCount(int countToMatch)
		{
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00048250 File Offset: 0x00047650
		internal override string TraceString()
		{
			string text = "SortOrder(";
			bool flag = false;
			foreach (SmiOrderProperty.SmiColumnOrder smiColumnOrder in this._columns)
			{
				if (flag)
				{
					text += ",";
				}
				else
				{
					flag = true;
				}
				if (SortOrder.Unspecified != smiColumnOrder.Order)
				{
					text += smiColumnOrder.TraceString();
				}
			}
			text += ")";
			return text;
		}

		// Token: 0x0400025E RID: 606
		private IList<SmiOrderProperty.SmiColumnOrder> _columns;

		// Token: 0x02000341 RID: 833
		internal struct SmiColumnOrder
		{
			// Token: 0x060033D9 RID: 13273 RVA: 0x0013F418 File Offset: 0x0013E818
			internal string TraceString()
			{
				return string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
				{
					this.SortOrdinal,
					this.Order
				});
			}

			// Token: 0x04001E8F RID: 7823
			internal int SortOrdinal;

			// Token: 0x04001E90 RID: 7824
			internal SortOrder Order;
		}
	}
}
