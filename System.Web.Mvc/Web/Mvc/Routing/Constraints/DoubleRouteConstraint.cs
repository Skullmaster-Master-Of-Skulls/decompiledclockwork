using System;
using System.Globalization;
using System.Web.Routing;

namespace System.Web.Mvc.Routing.Constraints
{
	// Token: 0x0200001B RID: 27
	public class DoubleRouteConstraint : IRouteConstraint
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00003E04 File Offset: 0x00002004
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
			if (obj is double)
			{
				return true;
			}
			string s = Convert.ToString(obj, CultureInfo.InvariantCulture);
			double num;
			return double.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, CultureInfo.InvariantCulture, out num);
		}
	}
}
