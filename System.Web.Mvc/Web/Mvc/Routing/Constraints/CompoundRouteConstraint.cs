using System;
using System.Collections.Generic;
using System.Web.Routing;

namespace System.Web.Mvc.Routing.Constraints
{
	// Token: 0x02000018 RID: 24
	public class CompoundRouteConstraint : IRouteConstraint
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00003CA9 File Offset: 0x00001EA9
		public CompoundRouteConstraint(IList<IRouteConstraint> constraints)
		{
			if (constraints == null)
			{
				throw Error.ArgumentNull("constraints");
			}
			this.Constraints = constraints;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003CC6 File Offset: 0x00001EC6
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00003CCE File Offset: 0x00001ECE
		public IEnumerable<IRouteConstraint> Constraints { get; private set; }

		// Token: 0x0600008A RID: 138 RVA: 0x00003CD8 File Offset: 0x00001ED8
		public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			foreach (IRouteConstraint routeConstraint in this.Constraints)
			{
				if (!routeConstraint.Match(httpContext, route, parameterName, values, routeDirection))
				{
					return false;
				}
			}
			return true;
		}
	}
}
