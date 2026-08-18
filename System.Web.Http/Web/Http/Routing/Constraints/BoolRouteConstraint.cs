using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x0200008A RID: 138
	public class BoolRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x0600037C RID: 892 RVA: 0x0000AEC0 File Offset: 0x000090C0
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
