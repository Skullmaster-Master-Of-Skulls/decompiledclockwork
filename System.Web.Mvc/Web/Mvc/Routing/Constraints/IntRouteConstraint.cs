using System;
using System.Globalization;
using System.Web.Routing;

namespace System.Web.Mvc.Routing.Constraints
{
	// Token: 0x0200001E RID: 30
	public class IntRouteConstraint : IRouteConstraint
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00003F40 File Offset: 0x00002140
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
			if (obj is int)
			{
				return true;
			}
			string s = Convert.ToString(obj, CultureInfo.InvariantCulture);
			int num;
			return int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num);
		}
	}
}
