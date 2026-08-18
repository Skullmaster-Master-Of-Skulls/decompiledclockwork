using System;
using System.Globalization;
using System.Web.Routing;

namespace System.Web.Mvc.Routing.Constraints
{
	// Token: 0x0200001A RID: 26
	public class DecimalRouteConstraint : IRouteConstraint
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00003D9C File Offset: 0x00001F9C
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
			if (obj is decimal)
			{
				return true;
			}
			string s = Convert.ToString(obj, CultureInfo.InvariantCulture);
			decimal num;
			return decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out num);
		}
	}
}
