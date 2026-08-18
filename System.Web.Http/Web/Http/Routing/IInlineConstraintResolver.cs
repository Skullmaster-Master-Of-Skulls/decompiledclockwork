using System;

namespace System.Web.Http.Routing
{
	// Token: 0x02000011 RID: 17
	public interface IInlineConstraintResolver
	{
		// Token: 0x0600007D RID: 125
		IHttpRouteConstraint ResolveConstraint(string inlineConstraint);
	}
}
