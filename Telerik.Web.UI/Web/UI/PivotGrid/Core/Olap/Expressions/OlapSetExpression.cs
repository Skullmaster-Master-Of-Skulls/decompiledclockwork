using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Olap.Expressions
{
	// Token: 0x02000707 RID: 1799
	internal class OlapSetExpression : OlapWrapperExpression
	{
		// Token: 0x06003FDD RID: 16349 RVA: 0x000C9E6C File Offset: 0x000C806C
		internal OlapSetExpression(IEnumerable<OlapExpression> memberExpressions) : base(memberExpressions, OlapWrapperExpressionType.Set)
		{
		}
	}
}
