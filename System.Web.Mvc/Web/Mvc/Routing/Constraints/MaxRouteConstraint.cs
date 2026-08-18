using System;
using System.Globalization;
using System.Web.Routing;

namespace System.Web.Mvc.Routing.Constraints
{
	// Token: 0x02000022 RID: 34
	public class MaxRouteConstraint : IRouteConstraint
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x000041D8 File Offset: 0x000023D8
		public MaxRouteConstraint(long max)
		{
			this.Max = max;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000041E7 File Offset: 0x000023E7
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x000041EF File Offset: 0x000023EF
		public long Max { get; private set; }

		// Token: 0x060000A9 RID: 169 RVA: 0x000041F8 File Offset: 0x000023F8
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
