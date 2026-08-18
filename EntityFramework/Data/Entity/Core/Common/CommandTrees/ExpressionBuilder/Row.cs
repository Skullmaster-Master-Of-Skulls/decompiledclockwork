using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder
{
	// Token: 0x02000120 RID: 288
	public sealed class Row
	{
		// Token: 0x060008C8 RID: 2248 RVA: 0x0002D744 File Offset: 0x0002B944
		public Row(KeyValuePair<string, DbExpression> columnValue, params KeyValuePair<string, DbExpression>[] columnValues)
		{
			this.arguments = new ReadOnlyCollection<KeyValuePair<string, DbExpression>>(Helpers.Prepend<KeyValuePair<string, DbExpression>>(columnValues, columnValue));
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0002D75E File Offset: 0x0002B95E
		public DbNewInstanceExpression ToExpression()
		{
			return DbExpressionBuilder.NewRow(this.arguments);
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0002D76B File Offset: 0x0002B96B
		public static implicit operator DbExpression(Row row)
		{
			Check.NotNull<Row>(row, "row");
			return row.ToExpression();
		}

		// Token: 0x0400028A RID: 650
		private readonly ReadOnlyCollection<KeyValuePair<string, DbExpression>> arguments;
	}
}
