using System;
using System.Globalization;
using System.Web.Routing;

namespace System.Web.Mvc.Routing.Constraints
{
	// Token: 0x02000019 RID: 25
	public class DateTimeRouteConstraint : IRouteConstraint
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00003D34 File Offset: 0x00001F34
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
			if (obj is DateTime)
			{
				return true;
			}
			string s = Convert.ToString(obj, CultureInfo.InvariantCulture);
			DateTime dateTime;
			return DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
		}
	}
}
