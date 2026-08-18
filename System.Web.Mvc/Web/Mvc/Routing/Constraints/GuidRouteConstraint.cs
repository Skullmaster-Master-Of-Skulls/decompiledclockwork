using System;
using System.Globalization;
using System.Web.Routing;

namespace System.Web.Mvc.Routing.Constraints
{
	// Token: 0x0200001D RID: 29
	public class GuidRouteConstraint : IRouteConstraint
	{
		// Token: 0x06000093 RID: 147 RVA: 0x00003EDC File Offset: 0x000020DC
		public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			if (parameterName == null)
			{
				throw Error.ArgumentNull("parameterName");
			}
			if (values == null)
			{
				throw Error.ArgumentNull("values");
			}
			object obj;
			if (!values.TryGetValue(parameterName, out obj) || obj == null)
			{
				return false;
			}
			if (obj is Guid)
			{
				return true;
			}
			string input = Convert.ToString(obj, CultureInfo.InvariantCulture);
			Guid guid;
			return Guid.TryParse(input, out guid);
		}
	}
}
