using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Web.Data.Extensions;

namespace Telerik.Web.Data.Expressions
{
	// Token: 0x02001BB3 RID: 7091
	internal abstract class GroupDescriptorExpressionBuilderBase : ExpressionBuilderBase
	{
		// Token: 0x06011247 RID: 70215 RVA: 0x003C7DA8 File Offset: 0x003C5FA8
		protected GroupDescriptorExpressionBuilderBase(IQueryable queryable) : base(queryable.ElementType)
		{
			this.Queryable = queryable;
		}

		// Token: 0x1700539E RID: 21406
		// (get) Token: 0x06011248 RID: 70216 RVA: 0x003C7DBD File Offset: 0x003C5FBD
		// (set) Token: 0x06011249 RID: 70217 RVA: 0x003C7DC5 File Offset: 0x003C5FC5
		public IQueryable Queryable { get; protected set; }

		// Token: 0x0601124A RID: 70218 RVA: 0x003C7DCE File Offset: 0x003C5FCE
		public IQueryable CreateQuery()
		{
			return this.Queryable.GroupBy(this.GroupKeySelectorExpression).OrderBy(this.GroupSortKeySelectorExpression, this.SortDirection).Select(this.ResultSelectorExpression);
		}

		// Token: 0x1700539F RID: 21407
		// (get) Token: 0x0601124B RID: 70219
		protected abstract ListSortDirection? SortDirection { get; }

		// Token: 0x170053A0 RID: 21408
		// (get) Token: 0x0601124C RID: 70220
		protected abstract LambdaExpression GroupKeySelectorExpression { get; }

		// Token: 0x170053A1 RID: 21409
		// (get) Token: 0x0601124D RID: 70221
		protected abstract LambdaExpression GroupSortKeySelectorExpression { get; }

		// Token: 0x170053A2 RID: 21410
		// (get) Token: 0x0601124E RID: 70222
		protected abstract LambdaExpression ResultSelectorExpression { get; }
	}
}
