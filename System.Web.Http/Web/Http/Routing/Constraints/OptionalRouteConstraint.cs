using System;
using System.Collections.Generic;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x02000099 RID: 153
	public class OptionalRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x060003AF RID: 943 RVA: 0x0000B71D File Offset: 0x0000991D
		public OptionalRouteConstraint(IHttpRouteConstraint innerConstraint)
		{
			if (innerConstraint == null)
			{
				throw Error.ArgumentNull("innerConstraint");
			}
			this.InnerConstraint = innerConstraint;
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000B73A File Offset: 0x0000993A
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x0000B742 File Offset: 0x00009942
		public IHttpRouteConstraint InnerConstraint { get; private set; }

		// Token: 0x060003B2 RID: 946 RVA: 0x0000B74C File Offset: 0x0000994C
		public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
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
			RouteParameter optional = RouteParameter.Optional;
			object obj;
			object obj2;
			return (route.Defaults.TryGetValue(parameterName, out obj) && obj == optional && values.TryGetValue(parameterName, out obj2) && obj2 == optional) || this.InnerConstraint.Match(request, route, parameterName, values, routeDirection);
		}
	}
}
