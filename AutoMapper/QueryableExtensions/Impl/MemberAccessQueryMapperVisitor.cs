using System;
using System.Linq.Expressions;

namespace AutoMapper.QueryableExtensions.Impl
{
	// Token: 0x02000061 RID: 97
	public class MemberAccessQueryMapperVisitor : ExpressionVisitor
	{
		// Token: 0x06000381 RID: 897 RVA: 0x00008CDA File Offset: 0x00006EDA
		public MemberAccessQueryMapperVisitor(ExpressionVisitor rootVisitor, IConfigurationProvider config)
		{
			this._rootVisitor = rootVisitor;
			this._config = config;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00008CF0 File Offset: 0x00006EF0
		protected override Expression VisitMember(MemberExpression node)
		{
			Expression expression = this._rootVisitor.Visit(node.Expression);
			if (expression != null)
			{
				PropertyMap propertyMap = this._config.GetPropertyMap(node.Member, expression.Type);
				return Expression.MakeMemberAccess(expression, propertyMap.DestinationProperty.MemberInfo);
			}
			return node;
		}

		// Token: 0x040000AF RID: 175
		private readonly ExpressionVisitor _rootVisitor;

		// Token: 0x040000B0 RID: 176
		private readonly IConfigurationProvider _config;
	}
}
