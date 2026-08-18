using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Telerik.Web.Data
{
	// Token: 0x02001BA5 RID: 7077
	internal class QueryableAggregatesGroupDescriptor : GroupDescriptorBase, IAggregateFunctionsProvider
	{
		// Token: 0x060111EA RID: 70122 RVA: 0x003C67D7 File Offset: 0x003C49D7
		public QueryableAggregatesGroupDescriptor(IEnumerable<AggregateFunction> aggregateFunctions)
		{
			this.aggregateFunctions = aggregateFunctions;
		}

		// Token: 0x17005393 RID: 21395
		// (get) Token: 0x060111EB RID: 70123 RVA: 0x003C67E6 File Offset: 0x003C49E6
		public IEnumerable<AggregateFunction> AggregateFunctions
		{
			get
			{
				return this.aggregateFunctions;
			}
		}

		// Token: 0x060111EC RID: 70124 RVA: 0x003C67EE File Offset: 0x003C49EE
		protected override Expression CreateGroupKeyExpression(ParameterExpression parameterExpression)
		{
			return Expression.Constant(1);
		}

		// Token: 0x04004CAA RID: 19626
		private readonly IEnumerable<AggregateFunction> aggregateFunctions;
	}
}
