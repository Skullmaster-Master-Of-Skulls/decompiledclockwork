using System;
using System.Collections.Generic;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x0200008B RID: 139
	public class CompoundRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x0600037E RID: 894 RVA: 0x0000AF21 File Offset: 0x00009121
		public CompoundRouteConstraint(IList<IHttpRouteConstraint> constraints)
		{
			if (constraints == null)
			{
				throw Error.ArgumentNull("constraints");
			}
			this.Constraints = constraints;
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000AF3E File Offset: 0x0000913E
		// (set) Token: 0x06000380 RID: 896 RVA: 0x0000AF46 File Offset: 0x00009146
		public IEnumerable<IHttpRouteConstraint> Constraints { get; private set; }

		// Token: 0x06000381 RID: 897 RVA: 0x0000AF50 File Offset: 0x00009150
		public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
		{
			foreach (IHttpRouteConstraint httpRouteConstraint in this.Constraints)
			{
				if (!httpRouteConstraint.Match(request, route, parameterName, values, routeDirection))
				{
					return false;
				}
			}
			return true;
		}
	}
}
