using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Web.UI.PivotGrid.Core.Olap.Expressions
{
	// Token: 0x02000708 RID: 1800
	internal class OlapTupleExpression : OlapWrapperExpression
	{
		// Token: 0x06003FDE RID: 16350 RVA: 0x000C9E78 File Offset: 0x000C8078
		internal OlapTupleExpression(OlapExpression memberExpression) : base(new OlapExpression[]
		{
			memberExpression
		}, OlapWrapperExpressionType.Tuple)
		{
			if (memberExpression == null)
			{
				throw new ArgumentNullException("memberExpression");
			}
		}

		// Token: 0x06003FDF RID: 16351 RVA: 0x000C9EA8 File Offset: 0x000C80A8
		internal OlapTupleExpression(IEnumerable<OlapExpression> memberExpressions) : base(memberExpressions, OlapWrapperExpressionType.Tuple)
		{
			List<OlapExpression> list = memberExpressions.ToList<OlapExpression>();
			if (list.Count == 0)
			{
				throw new ArgumentException("At least one member required");
			}
		}
	}
}
