using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x02000093 RID: 147
	public class LongRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x06000397 RID: 919 RVA: 0x0000B380 File Offset: 0x00009580
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
