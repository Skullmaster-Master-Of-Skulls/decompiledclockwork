using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x0200008D RID: 141
	public class DecimalRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x06000384 RID: 900 RVA: 0x0000B014 File Offset: 0x00009214
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
