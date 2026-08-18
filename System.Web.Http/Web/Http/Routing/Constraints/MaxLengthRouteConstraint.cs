using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x02000094 RID: 148
	public class MaxLengthRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x06000399 RID: 921 RVA: 0x0000B3E7 File Offset: 0x000095E7
		public MaxLengthRouteConstraint(int maxLength)
		{
			if (maxLength < 0)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("maxLength", maxLength, 0);
			}
			this.MaxLength = maxLength;
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000B411 File Offset: 0x00009611
		// (set) Token: 0x0600039B RID: 923 RVA: 0x0000B419 File Offset: 0x00009619
		public int MaxLength { get; private set; }

		// Token: 0x0600039C RID: 924 RVA: 0x0000B424 File Offset: 0x00009624
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
			if (values.TryGetValue(parameterName, out obj) && obj != null)
			{
				string text = Convert.ToString(obj, CultureInfo.InvariantCulture);
				return text.Length <= this.MaxLength;
			}
			return false;
		}
	}
}
