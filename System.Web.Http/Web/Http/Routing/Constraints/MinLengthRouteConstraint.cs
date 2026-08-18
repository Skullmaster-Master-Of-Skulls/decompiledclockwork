using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x02000096 RID: 150
	public class MinLengthRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x060003A1 RID: 929 RVA: 0x0000B51B File Offset: 0x0000971B
		public MinLengthRouteConstraint(int minLength)
		{
			if (minLength < 0)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("minLength", minLength, 0);
			}
			this.MinLength = minLength;
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000B545 File Offset: 0x00009745
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x0000B54D File Offset: 0x0000974D
		public int MinLength { get; private set; }

		// Token: 0x060003A4 RID: 932 RVA: 0x0000B558 File Offset: 0x00009758
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
				return text.Length >= this.MinLength;
			}
			return false;
		}
	}
}
