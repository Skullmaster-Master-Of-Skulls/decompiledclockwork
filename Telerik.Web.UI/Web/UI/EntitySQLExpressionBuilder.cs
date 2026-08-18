using System;
using System.Collections.Generic;
using Telerik.Web.UI.Functions;

namespace Telerik.Web.UI
{
	// Token: 0x0200196E RID: 6510
	internal class EntitySQLExpressionBuilder : ListViewFilterExpressionBuilder
	{
		// Token: 0x0600FC2C RID: 64556 RVA: 0x0038D04F File Offset: 0x0038B24F
		public EntitySQLExpressionBuilder(IEnumerable<RadListViewFilterExpression> expressions) : base(expressions)
		{
		}

		// Token: 0x0600FC2D RID: 64557 RVA: 0x0038D058 File Offset: 0x0038B258
		public EntitySQLExpressionBuilder(IEnumerable<RadListViewFilterExpression> expressions, RadListViewGroupFilterOperator groupOperator) : base(expressions, groupOperator)
		{
		}

		// Token: 0x17004C2C RID: 19500
		// (get) Token: 0x0600FC2E RID: 64558 RVA: 0x0038D06A File Offset: 0x0038B26A
		protected override TFunc<RadListViewFilterExpression, string> Convertor
		{
			get
			{
				return (RadListViewFilterExpression item) => item.ToEntitySQL();
			}
		}
	}
}
