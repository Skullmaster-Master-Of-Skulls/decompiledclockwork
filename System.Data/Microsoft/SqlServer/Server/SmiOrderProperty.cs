using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000046 RID: 70
	internal class SmiOrderProperty : SmiMetaDataProperty
	{
		// Token: 0x06000269 RID: 617 RVA: 0x001DF0C8 File Offset: 0x001DE4C8
		internal SmiOrderProperty(IList<SmiOrderProperty.SmiColumnOrder> columnOrders)
		{
			this._columns = new List<SmiOrderProperty.SmiColumnOrder>(columnOrders).AsReadOnly();
		}

		// Token: 0x17000045 RID: 69
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

		// Token: 0x0600026B RID: 619 RVA: 0x001DF148 File Offset: 0x001DE548
		[Conditional("DEBUG")]
		internal void CheckCount(int countToMatch)
		{
		}

		// Token: 0x0600026C RID: 620 RVA: 0x001DF158 File Offset: 0x001DE558
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

		// Token: 0x040005FA RID: 1530
		private IList<SmiOrderProperty.SmiColumnOrder> _columns;

		// Token: 0x02000047 RID: 71
		internal struct SmiColumnOrder
		{
			// Token: 0x0600026D RID: 621 RVA: 0x001DF1F8 File Offset: 0x001DE5F8
			internal string TraceString()
			{
				return string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
				{
					this.SortOrdinal,
					this.Order
				});
			}

			// Token: 0x040005FB RID: 1531
			internal int SortOrdinal;

			// Token: 0x040005FC RID: 1532
			internal SortOrder Order;
		}
	}
}
