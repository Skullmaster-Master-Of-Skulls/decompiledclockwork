using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x02000095 RID: 149
	public class MaxRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x0600039D RID: 925 RVA: 0x0000B47C File Offset: 0x0000967C
		public MaxRouteConstraint(long max)
		{
			this.Max = max;
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0000B48B File Offset: 0x0000968B
		// (set) Token: 0x0600039F RID: 927 RVA: 0x0000B493 File Offset: 0x00009693
		public long Max { get; private set; }

		// Token: 0x060003A0 RID: 928 RVA: 0x0000B49C File Offset: 0x0000969C
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
				long num;
				if (obj is long)
				{
					num = (long)obj;
					return num <= this.Max;
				}
				string s = Convert.ToString(obj, CultureInfo.InvariantCulture);
				if (long.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
				{
					return num <= this.Max;
				}
			}
			return false;
		}
	}
}
