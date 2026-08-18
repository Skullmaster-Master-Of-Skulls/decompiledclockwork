using System;
using System.Globalization;
using System.Web.Routing;

namespace System.Web.Mvc.Routing.Constraints
{
	// Token: 0x02000021 RID: 33
	public class MaxLengthRouteConstraint : IRouteConstraint
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x0000414F File Offset: 0x0000234F
		public MaxLengthRouteConstraint(int maxLength)
		{
			if (maxLength < 0)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("maxLength", maxLength, 0);
			}
			this.MaxLength = maxLength;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000416F File Offset: 0x0000236F
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00004177 File Offset: 0x00002377
		public int MaxLength { get; private set; }

		// Token: 0x060000A5 RID: 165 RVA: 0x00004180 File Offset: 0x00002380
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
				string text = Convert.ToString(obj, CultureInfo.InvariantCulture);
				return text.Length <= this.MaxLength;
			}
			return false;
		}
	}
}
