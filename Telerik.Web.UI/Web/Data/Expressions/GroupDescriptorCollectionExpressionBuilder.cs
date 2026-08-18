using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Web.Data.Expressions
{
	// Token: 0x02001BB2 RID: 7090
	internal class GroupDescriptorCollectionExpressionBuilder : ExpressionBuilderBase
	{
		// Token: 0x06011245 RID: 70213 RVA: 0x003C7D1B File Offset: 0x003C5F1B
		public GroupDescriptorCollectionExpressionBuilder(IQueryable queryable, IEnumerable<IGroupDescriptor> groupDescriptors) : base(queryable.ElementType)
		{
			this.queryable = queryable;
			this.groupDescriptors = groupDescriptors;
		}

		// Token: 0x06011246 RID: 70214 RVA: 0x003C7D38 File Offset: 0x003C5F38
		public IQueryable CreateQuery()
		{
			GroupDescriptorExpressionBuilder groupDescriptorExpressionBuilder = null;
			foreach (IGroupDescriptor groupDescriptor in this.groupDescriptors.Reverse<IGroupDescriptor>())
			{
				GroupDescriptorExpressionBuilder groupDescriptorExpressionBuilder2 = new GroupDescriptorExpressionBuilder(this.queryable, groupDescriptor, groupDescriptorExpressionBuilder);
				groupDescriptorExpressionBuilder = groupDescriptorExpressionBuilder2;
			}
			if (groupDescriptorExpressionBuilder != null)
			{
				return groupDescriptorExpressionBuilder.CreateQuery();
			}
			return this.queryable;
		}

		// Token: 0x04004CBB RID: 19643
		private readonly IQueryable queryable;

		// Token: 0x04004CBC RID: 19644
		private readonly IEnumerable<IGroupDescriptor> groupDescriptors;
	}
}
