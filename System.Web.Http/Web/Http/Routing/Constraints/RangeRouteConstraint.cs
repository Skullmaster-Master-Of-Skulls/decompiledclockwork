using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x02000098 RID: 152
	public class RangeRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x060003A9 RID: 937 RVA: 0x0000B64F File Offset: 0x0000984F
		public RangeRouteConstraint(long min, long max)
		{
			this.Min = min;
			this.Max = max;
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000B665 File Offset: 0x00009865
		// (set) Token: 0x060003AB RID: 939 RVA: 0x0000B66D File Offset: 0x0000986D
		public long Min { get; private set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000B676 File Offset: 0x00009876
		// (set) Token: 0x060003AD RID: 941 RVA: 0x0000B67E File Offset: 0x0000987E
		public long Max { get; private set; }

		// Token: 0x060003AE RID: 942 RVA: 0x0000B688 File Offset: 0x00009888
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
					return num >= this.Min && num <= this.Max;
				}
				string s = Convert.ToString(obj, CultureInfo.InvariantCulture);
				if (long.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
				{
					return num >= this.Min && num <= this.Max;
				}
			}
			return false;
		}
	}
}
