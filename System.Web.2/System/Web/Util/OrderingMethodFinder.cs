using System;
using System.Linq.Expressions;

namespace System.Web.Util
{
	// Token: 0x020001D8 RID: 472
	internal sealed class OrderingMethodFinder : ExpressionVisitor
	{
		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001793 RID: 6035 RVA: 0x00049FBB File Offset: 0x000481BB
		// (set) Token: 0x06001794 RID: 6036 RVA: 0x00049FC3 File Offset: 0x000481C3
		private bool OrderingMethodFound { get; set; }

		// Token: 0x06001795 RID: 6037 RVA: 0x00049FCC File Offset: 0x000481CC
		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			if (this.isTopLevelMethodCall && QueryableUtility.IsOrderingMethod(node))
			{
				this.OrderingMethodFound = true;
			}
			this.isTopLevelMethodCall = false;
			Expression result = base.VisitMethodCall(node);
			this.isTopLevelMethodCall = true;
			return result;
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x0004A008 File Offset: 0x00048208
		internal static bool OrderMethodExists(Expression expression)
		{
			OrderingMethodFinder orderingMethodFinder = new OrderingMethodFinder();
			orderingMethodFinder.OrderingMethodFound = false;
			orderingMethodFinder.Visit(expression);
			return orderingMethodFinder.OrderingMethodFound;
		}

		// Token: 0x0400171B RID: 5915
		private bool isTopLevelMethodCall = true;
	}
}
