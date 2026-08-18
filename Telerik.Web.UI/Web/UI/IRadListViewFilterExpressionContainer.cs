using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001971 RID: 6513
	public interface IRadListViewFilterExpressionContainer
	{
		// Token: 0x0600FC3A RID: 64570
		RadListViewFilterExpression FindByFieldName(string fieldName);

		// Token: 0x17004C2E RID: 19502
		// (get) Token: 0x0600FC3B RID: 64571
		IList<RadListViewFilterExpression> Expressions { get; }
	}
}
