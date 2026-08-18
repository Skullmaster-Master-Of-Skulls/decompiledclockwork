using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D20 RID: 3360
	internal class DimensionSchemaElement : UniqueSchemaElement
	{
		// Token: 0x06007D29 RID: 32041 RVA: 0x001CB50F File Offset: 0x001C970F
		public DimensionSchemaElement()
		{
			this.hierarchies = new List<HierarchySchemaElement>();
		}

		// Token: 0x170027E7 RID: 10215
		// (get) Token: 0x06007D2A RID: 32042 RVA: 0x001CB522 File Offset: 0x001C9722
		public IList<HierarchySchemaElement> Hierarchies
		{
			get
			{
				return this.hierarchies;
			}
		}

		// Token: 0x0400225A RID: 8794
		private List<HierarchySchemaElement> hierarchies;
	}
}
