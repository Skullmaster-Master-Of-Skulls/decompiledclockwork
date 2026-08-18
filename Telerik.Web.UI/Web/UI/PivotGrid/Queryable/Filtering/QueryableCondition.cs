using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Filtering;

namespace Telerik.Web.UI.PivotGrid.Queryable.Filtering
{
	// Token: 0x02000731 RID: 1841
	[DataContract]
	public abstract class QueryableCondition : Condition
	{
		// Token: 0x0600417F RID: 16767 RVA: 0x000CDBA3 File Offset: 0x000CBDA3
		internal QueryableCondition()
		{
		}

		// Token: 0x06004180 RID: 16768
		protected internal abstract Expression GetExpression(Expression valueExpression);

		// Token: 0x06004181 RID: 16769 RVA: 0x000CDBAB File Offset: 0x000CBDAB
		internal bool IsValidExpression(Expression valueExpression)
		{
			return this.IsActive && valueExpression != null && valueExpression.ToString() != "null";
		}
	}
}
