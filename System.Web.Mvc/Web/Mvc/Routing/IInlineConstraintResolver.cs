using System;
using System.Web.Routing;

namespace System.Web.Mvc.Routing
{
	// Token: 0x0200000B RID: 11
	public interface IInlineConstraintResolver
	{
		// Token: 0x06000055 RID: 85
		IRouteConstraint ResolveConstraint(string inlineConstraint);
	}
}
