using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x02000090 RID: 144
	public class GuidRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x0600038A RID: 906 RVA: 0x0000B154 File Offset: 0x00009354
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
			if (obj is Guid)
			{
				return true;
			}
			string input = Convert.ToString(obj, CultureInfo.InvariantCulture);
			Guid guid;
			return Guid.TryParse(input, out guid);
		}
	}
}
