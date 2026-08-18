using System;
using System.Data;

namespace Telerik.Web.UI
{
	// Token: 0x020010E6 RID: 4326
	internal class GridDataColumn : DataColumn
	{
		// Token: 0x0600B131 RID: 45361 RVA: 0x00265D0F File Offset: 0x00263F0F
		public GridDataColumn()
		{
		}

		// Token: 0x0600B132 RID: 45362 RVA: 0x00265D17 File Offset: 0x00263F17
		public GridDataColumn(string columnName) : base(columnName)
		{
		}

		// Token: 0x0600B133 RID: 45363 RVA: 0x00265D20 File Offset: 0x00263F20
		public GridDataColumn(string columnName, Type dataType) : base(columnName, dataType)
		{
		}

		// Token: 0x0600B134 RID: 45364 RVA: 0x00265D2A File Offset: 0x00263F2A
		public GridDataColumn(string columnName, Type dataType, string expr) : base(columnName, dataType, expr)
		{
		}

		// Token: 0x1700396C RID: 14700
		// (get) Token: 0x0600B135 RID: 45365 RVA: 0x00265D35 File Offset: 0x00263F35
		// (set) Token: 0x0600B136 RID: 45366 RVA: 0x00265D3D File Offset: 0x00263F3D
		public bool IsPrimitive
		{
			get
			{
				return this._isPrimitive;
			}
			set
			{
				this._isPrimitive = value;
			}
		}

		// Token: 0x04002E7E RID: 11902
		private bool _isPrimitive;
	}
}
