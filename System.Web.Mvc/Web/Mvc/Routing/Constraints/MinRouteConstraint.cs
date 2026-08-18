using System;
using System.Globalization;
using System.Web.Routing;

namespace System.Web.Mvc.Routing.Constraints
{
	// Token: 0x02000024 RID: 36
	public class MinRouteConstraint : IRouteConstraint
	{
		// Token: 0x060000AE RID: 174 RVA: 0x00004300 File Offset: 0x00002500
		public MinRouteConstraint(long min)
		{
			this.Min = min;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000430F File Offset: 0x0000250F
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00004317 File Offset: 0x00002517
		public long Min { get; private set; }

		// Token: 0x060000B1 RID: 177 RVA: 0x00004320 File Offset: 0x00002520
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
