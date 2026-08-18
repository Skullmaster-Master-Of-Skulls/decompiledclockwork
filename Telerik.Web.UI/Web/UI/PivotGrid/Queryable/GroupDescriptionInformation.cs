using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Telerik.Web.UI.PivotGrid.Queryable
{
	// Token: 0x02000737 RID: 1847
	internal class GroupDescriptionInformation
	{
		// Token: 0x060041BD RID: 16829 RVA: 0x000CE81A File Offset: 0x000CCA1A
		public GroupDescriptionInformation(QueryableGroupDescription description)
		{
			if (description == null)
			{
				throw new ArgumentNullException("description");
			}
			this.ProjectionPropertyNames = new List<string>();
			this.CachedProjectionPropertyExpressions = new List<Expression>();
			this.Description = description;
		}

		// Token: 0x1700156F RID: 5487
		// (get) Token: 0x060041BE RID: 16830 RVA: 0x000CE84D File Offset: 0x000CCA4D
		// (set) Token: 0x060041BF RID: 16831 RVA: 0x000CE855 File Offset: 0x000CCA55
		public QueryableGroupDescription Description { get; private set; }

		// Token: 0x17001570 RID: 5488
		// (get) Token: 0x060041C0 RID: 16832 RVA: 0x000CE85E File Offset: 0x000CCA5E
		// (set) Token: 0x060041C1 RID: 16833 RVA: 0x000CE866 File Offset: 0x000CCA66
		public List<Expression> CachedProjectionPropertyExpressions { get; private set; }

		// Token: 0x17001571 RID: 5489
		// (get) Token: 0x060041C2 RID: 16834 RVA: 0x000CE86F File Offset: 0x000CCA6F
		// (set) Token: 0x060041C3 RID: 16835 RVA: 0x000CE877 File Offset: 0x000CCA77
		public Expression CachedGroupingExpression { get; set; }

		// Token: 0x17001572 RID: 5490
		// (get) Token: 0x060041C4 RID: 16836 RVA: 0x000CE880 File Offset: 0x000CCA80
		// (set) Token: 0x060041C5 RID: 16837 RVA: 0x000CE888 File Offset: 0x000CCA88
		public List<string> ProjectionPropertyNames { get; private set; }

		// Token: 0x17001573 RID: 5491
		// (get) Token: 0x060041C6 RID: 16838 RVA: 0x000CE891 File Offset: 0x000CCA91
		// (set) Token: 0x060041C7 RID: 16839 RVA: 0x000CE899 File Offset: 0x000CCA99
		public string GroupingTypePropertyName { get; set; }

		// Token: 0x17001574 RID: 5492
		// (get) Token: 0x060041C8 RID: 16840 RVA: 0x000CE8A2 File Offset: 0x000CCAA2
		// (set) Token: 0x060041C9 RID: 16841 RVA: 0x000CE8AA File Offset: 0x000CCAAA
		public PropertyInfo GroupingTypePropertyInfo { get; set; }

		// Token: 0x17001575 RID: 5493
		// (get) Token: 0x060041CA RID: 16842 RVA: 0x000CE8B3 File Offset: 0x000CCAB3
		// (set) Token: 0x060041CB RID: 16843 RVA: 0x000CE8BB File Offset: 0x000CCABB
		public Func<object, object> GroupingTypePropertyAccess { get; set; }
	}
}
