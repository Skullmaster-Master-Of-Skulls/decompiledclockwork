using System;
using System.Web.Routing;

namespace System.Web.Mvc.Routing.Constraints
{
	// Token: 0x02000026 RID: 38
	public class OptionalRouteConstraint : IRouteConstraint
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x0000446D File Offset: 0x0000266D
		public OptionalRouteConstraint(IRouteConstraint innerConstraint)
		{
			if (innerConstraint == null)
			{
				throw Error.ArgumentNull("innerConstraint");
			}
			this.InnerConstraint = innerConstraint;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000448A File Offset: 0x0000268A
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00004492 File Offset: 0x00002692
		public IRouteConstraint InnerConstraint { get; private set; }

		// Token: 0x060000BB RID: 187 RVA: 0x0000449C File Offset: 0x0000269C
		public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			if (route == null)
			{
				throw Error.ArgumentNull("route");
			}
			if (parameterName == null)
			{
				throw Error.ArgumentNull("parameterName");
			}
			if (values == null)
			{
				throw Error.ArgumentNull("values");
			}
			UrlParameter optional = UrlParameter.Optional;
			object obj;
			object obj2;
			return (route.Defaults.TryGetValue(parameterName, out obj) && obj == optional && values.TryGetValue(parameterName, out obj2) && obj2 == optional) || this.InnerConstraint.Match(httpContext, route, parameterName, values, routeDirection);
		}
	}
}
