using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.Utils;

namespace System.Data.Common.CommandTrees.ExpressionBuilder
{
	// Token: 0x02000429 RID: 1065
	public sealed class Row
	{
		// Token: 0x0600386E RID: 14446 RVA: 0x000D62DD File Offset: 0x000D44DD
		public Row(KeyValuePair<string, DbExpression> columnValue, params KeyValuePair<string, DbExpression>[] columnValues)
		{
			this.arguments = new ReadOnlyCollection<KeyValuePair<string, DbExpression>>(Helpers.Prepend<KeyValuePair<string, DbExpression>>(columnValues, columnValue));
		}

		// Token: 0x0600386F RID: 14447 RVA: 0x000D62F7 File Offset: 0x000D44F7
		public DbNewInstanceExpression ToExpression()
		{
			return DbExpressionBuilder.NewRow(this.arguments);
		}

		// Token: 0x06003870 RID: 14448 RVA: 0x000D6304 File Offset: 0x000D4504
		public static implicit operator DbExpression(Row row)
		{
			EntityUtil.CheckArgumentNull<Row>(row, "row");
			return row.ToExpression();
		}

		// Token: 0x04001850 RID: 6224
		private readonly ReadOnlyCollection<KeyValuePair<string, DbExpression>> arguments;
	}
}
