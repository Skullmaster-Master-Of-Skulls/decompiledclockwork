using System;
using System.Linq.Expressions;

namespace Telerik.Web.UI.PivotGrid.Queryable
{
	// Token: 0x02000724 RID: 1828
	internal class AggregateDescriptionInformation
	{
		// Token: 0x060040D1 RID: 16593 RVA: 0x000CC452 File Offset: 0x000CA652
		public AggregateDescriptionInformation(QueryableAggregateDescription descriptor)
		{
			if (descriptor == null)
			{
				throw new ArgumentNullException("descriptor");
			}
			this.Descriptor = descriptor;
		}

		// Token: 0x1700152C RID: 5420
		// (get) Token: 0x060040D2 RID: 16594 RVA: 0x000CC46F File Offset: 0x000CA66F
		// (set) Token: 0x060040D3 RID: 16595 RVA: 0x000CC477 File Offset: 0x000CA677
		public QueryableAggregateDescription Descriptor { get; private set; }

		// Token: 0x1700152D RID: 5421
		// (get) Token: 0x060040D4 RID: 16596 RVA: 0x000CC480 File Offset: 0x000CA680
		// (set) Token: 0x060040D5 RID: 16597 RVA: 0x000CC488 File Offset: 0x000CA688
		public Expression CachedAggregateExpression { get; set; }

		// Token: 0x1700152E RID: 5422
		// (get) Token: 0x060040D6 RID: 16598 RVA: 0x000CC491 File Offset: 0x000CA691
		// (set) Token: 0x060040D7 RID: 16599 RVA: 0x000CC499 File Offset: 0x000CA699
		public Expression CachedAggregatedValueExpression { get; set; }

		// Token: 0x1700152F RID: 5423
		// (get) Token: 0x060040D8 RID: 16600 RVA: 0x000CC4A2 File Offset: 0x000CA6A2
		// (set) Token: 0x060040D9 RID: 16601 RVA: 0x000CC4AA File Offset: 0x000CA6AA
		public string AggregateValuePropertyName { get; set; }

		// Token: 0x17001530 RID: 5424
		// (get) Token: 0x060040DA RID: 16602 RVA: 0x000CC4B3 File Offset: 0x000CA6B3
		// (set) Token: 0x060040DB RID: 16603 RVA: 0x000CC4BB File Offset: 0x000CA6BB
		public string AggregateTypePropertyName { get; set; }

		// Token: 0x17001531 RID: 5425
		// (get) Token: 0x060040DC RID: 16604 RVA: 0x000CC4C4 File Offset: 0x000CA6C4
		// (set) Token: 0x060040DD RID: 16605 RVA: 0x000CC4CC File Offset: 0x000CA6CC
		public Func<object, object> AggregateTypePropertyAccess { get; set; }
	}
}
