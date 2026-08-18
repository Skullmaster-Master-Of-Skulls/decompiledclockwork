using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x02000091 RID: 145
	public class IntRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x0600038C RID: 908 RVA: 0x0000B1B8 File Offset: 0x000093B8
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
