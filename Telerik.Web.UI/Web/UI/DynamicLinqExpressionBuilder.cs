using System;
using System.Collections.Generic;
using Telerik.Web.UI.Functions;

namespace Telerik.Web.UI
{
	// Token: 0x0200196D RID: 6509
	internal class DynamicLinqExpressionBuilder : ListViewFilterExpressionBuilder
	{
		// Token: 0x0600FC28 RID: 64552 RVA: 0x0038D015 File Offset: 0x0038B215
		public DynamicLinqExpressionBuilder(IEnumerable<RadListViewFilterExpression> expressions) : base(expressions)
		{
		}

		// Token: 0x0600FC29 RID: 64553 RVA: 0x0038D01E File Offset: 0x0038B21E
		public DynamicLinqExpressionBuilder(IEnumerable<RadListViewFilterExpression> expressions, RadListViewGroupFilterOperator groupOperator) : base(expressions, groupOperator)
		{
		}

		// Token: 0x17004C2B RID: 19499
		// (get) Token: 0x0600FC2A RID: 64554 RVA: 0x0038D030 File Offset: 0x0038B230
		protected override TFunc<RadListViewFilterExpression, string> Convertor
		{
			get
			{
				return (RadListViewFilterExpression item) => item.ToDynamicLinq();
			}
		}
	}
}
