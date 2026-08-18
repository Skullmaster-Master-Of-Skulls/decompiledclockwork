using System;
using System.Collections.Generic;
using Telerik.Web.UI.Functions;

namespace Telerik.Web.UI
{
	// Token: 0x0200196F RID: 6511
	internal class OqlExpressionBuilder : ListViewFilterExpressionBuilder
	{
		// Token: 0x0600FC30 RID: 64560 RVA: 0x0038D089 File Offset: 0x0038B289
		public OqlExpressionBuilder(IEnumerable<RadListViewFilterExpression> expressions) : base(expressions)
		{
		}

		// Token: 0x0600FC31 RID: 64561 RVA: 0x0038D092 File Offset: 0x0038B292
		public OqlExpressionBuilder(IEnumerable<RadListViewFilterExpression> expressions, RadListViewGroupFilterOperator groupOperator) : base(expressions, groupOperator)
		{
		}

		// Token: 0x17004C2D RID: 19501
		// (get) Token: 0x0600FC32 RID: 64562 RVA: 0x0038D0A4 File Offset: 0x0038B2A4
		protected override TFunc<RadListViewFilterExpression, string> Convertor
		{
			get
			{
				return (RadListViewFilterExpression item) => item.ToOql();
			}
		}
	}
}
