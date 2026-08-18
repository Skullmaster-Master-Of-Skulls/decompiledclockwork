using System;
using System.Globalization;
using System.Web.Routing;

namespace System.Web.Mvc.Routing.Constraints
{
	// Token: 0x02000025 RID: 37
	public class RangeRouteConstraint : IRouteConstraint
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x0000439F File Offset: 0x0000259F
		public RangeRouteConstraint(long min, long max)
		{
			this.Min = min;
			this.Max = max;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000043B5 File Offset: 0x000025B5
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x000043BD File Offset: 0x000025BD
		public long Min { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000043C6 File Offset: 0x000025C6
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x000043CE File Offset: 0x000025CE
		public long Max { get; private set; }

		// Token: 0x060000B7 RID: 183 RVA: 0x000043D8 File Offset: 0x000025D8
		public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
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
