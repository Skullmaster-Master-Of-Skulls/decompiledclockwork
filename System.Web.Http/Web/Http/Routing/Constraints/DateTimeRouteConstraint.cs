using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x0200008C RID: 140
	public class DateTimeRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x06000382 RID: 898 RVA: 0x0000AFAC File Offset: 0x000091AC
		public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
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
