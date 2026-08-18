using System;
using System.Linq.Expressions;

namespace AutoMapper.QueryableExtensions.Impl
{
	// Token: 0x02000066 RID: 102
	public class ParameterReplacementVisitor : ExpressionVisitor
	{
		// Token: 0x06000395 RID: 917 RVA: 0x00008F5D File Offset: 0x0000715D
		public ParameterReplacementVisitor(Expression memberExpression)
		{
			this._memberExpression = memberExpression;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00008F6C File Offset: 0x0000716C
		protected override Expression VisitParameter(ParameterExpression node)
		{
			return this._memberExpression;
		}

		// Token: 0x040000B1 RID: 177
		private readonly Expression _memberExpression;
	}
}
