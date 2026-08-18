using System;
using System.Linq.Expressions;

namespace Telerik.Web.Data.Expressions
{
	// Token: 0x02001BBA RID: 7098
	internal class PropertyAccessExpressionBuilder : MemberAccessExpressionBuilderBase
	{
		// Token: 0x06011280 RID: 70272 RVA: 0x003C87F8 File Offset: 0x003C69F8
		public PropertyAccessExpressionBuilder(Type itemType, string memberName) : base(itemType, memberName)
		{
		}

		// Token: 0x06011281 RID: 70273 RVA: 0x003C8802 File Offset: 0x003C6A02
		protected override Expression CreateMemberAccessExpressionOverride()
		{
			return ExpressionFactory.MakeMemberAccess(base.ParameterExpression, base.MemberName, base.Options.LiftMemberAccessToNull);
		}
	}
}
