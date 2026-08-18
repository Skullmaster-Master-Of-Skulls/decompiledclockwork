using System;
using System.Globalization;
using System.Web.Routing;

namespace System.Web.Mvc.Routing.Constraints
{
	// Token: 0x0200001C RID: 28
	public class FloatRouteConstraint : IRouteConstraint
	{
		// Token: 0x06000091 RID: 145 RVA: 0x00003E70 File Offset: 0x00002070
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
			if (obj is float)
			{
				return true;
			}
			string s = Convert.ToString(obj, CultureInfo.InvariantCulture);
			float num;
			return float.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, CultureInfo.InvariantCulture, out num);
		}
	}
}
