using System;
using System.Globalization;
using System.Web.Routing;

namespace System.Web.Mvc.Routing.Constraints
{
	// Token: 0x02000020 RID: 32
	public class LongRouteConstraint : IRouteConstraint
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x000040E8 File Offset: 0x000022E8
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
			if (obj is long)
			{
				return true;
			}
			string s = Convert.ToString(obj, CultureInfo.InvariantCulture);
			long num;
			return long.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num);
		}
	}
}
