using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Telerik.Web.UI.PivotGrid.Queryable
{
	// Token: 0x02000730 RID: 1840
	internal class FilterDescriptionInformation
	{
		// Token: 0x06004176 RID: 16758 RVA: 0x000CDB2C File Offset: 0x000CBD2C
		public FilterDescriptionInformation(QueryableFilterDescription filterDescription)
		{
			if (filterDescription == null)
			{
				throw new ArgumentNullException("filterDescription");
			}
			this.Description = filterDescription;
			this.CachedProjectionFilterExpressions = new List<Expression>();
			this.FilterPropertyNames = new List<string>();
		}

		// Token: 0x17001557 RID: 5463
		// (get) Token: 0x06004177 RID: 16759 RVA: 0x000CDB5F File Offset: 0x000CBD5F
		// (set) Token: 0x06004178 RID: 16760 RVA: 0x000CDB67 File Offset: 0x000CBD67
		public QueryableFilterDescription Description { get; private set; }

		// Token: 0x17001558 RID: 5464
		// (get) Token: 0x06004179 RID: 16761 RVA: 0x000CDB70 File Offset: 0x000CBD70
		// (set) Token: 0x0600417A RID: 16762 RVA: 0x000CDB78 File Offset: 0x000CBD78
		public Expression CachedFilterExpression { get; set; }

		// Token: 0x17001559 RID: 5465
		// (get) Token: 0x0600417B RID: 16763 RVA: 0x000CDB81 File Offset: 0x000CBD81
		// (set) Token: 0x0600417C RID: 16764 RVA: 0x000CDB89 File Offset: 0x000CBD89
		public List<Expression> CachedProjectionFilterExpressions { get; set; }

		// Token: 0x1700155A RID: 5466
		// (get) Token: 0x0600417D RID: 16765 RVA: 0x000CDB92 File Offset: 0x000CBD92
		// (set) Token: 0x0600417E RID: 16766 RVA: 0x000CDB9A File Offset: 0x000CBD9A
		public List<string> FilterPropertyNames { get; private set; }
	}
}
