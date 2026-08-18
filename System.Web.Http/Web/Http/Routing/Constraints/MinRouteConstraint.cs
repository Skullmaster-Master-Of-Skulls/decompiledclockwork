using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x02000097 RID: 151
	public class MinRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x060003A5 RID: 933 RVA: 0x0000B5B0 File Offset: 0x000097B0
		public MinRouteConstraint(long min)
		{
			this.Min = min;
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000B5BF File Offset: 0x000097BF
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x0000B5C7 File Offset: 0x000097C7
		public long Min { get; private set; }

		// Token: 0x060003A8 RID: 936 RVA: 0x0000B5D0 File Offset: 0x000097D0
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
					return num >= this.Min;
				}
				string s = Convert.ToString(obj, CultureInfo.InvariantCulture);
				if (long.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
				{
					return num >= this.Min;
				}
			}
			return false;
		}
	}
}
