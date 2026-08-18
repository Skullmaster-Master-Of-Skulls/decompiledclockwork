using System;
using System.Globalization;
using System.Web.Routing;

namespace System.Web.Mvc.Routing.Constraints
{
	// Token: 0x02000017 RID: 23
	public class BoolRouteConstraint : IRouteConstraint
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00003C48 File Offset: 0x00001E48
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
			if (obj is bool)
			{
				return true;
			}
			string value = Convert.ToString(obj, CultureInfo.InvariantCulture);
			bool flag;
			return bool.TryParse(value, out flag);
		}
	}
}
