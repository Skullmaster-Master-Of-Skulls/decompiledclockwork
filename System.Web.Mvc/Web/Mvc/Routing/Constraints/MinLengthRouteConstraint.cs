using System;
using System.Globalization;
using System.Web.Routing;

namespace System.Web.Mvc.Routing.Constraints
{
	// Token: 0x02000023 RID: 35
	public class MinLengthRouteConstraint : IRouteConstraint
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00004277 File Offset: 0x00002477
		public MinLengthRouteConstraint(int minLength)
		{
			if (minLength < 0)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("minLength", minLength, 0);
			}
			this.MinLength = minLength;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00004297 File Offset: 0x00002497
		// (set) Token: 0x060000AC RID: 172 RVA: 0x0000429F File Offset: 0x0000249F
		public int MinLength { get; private set; }

		// Token: 0x060000AD RID: 173 RVA: 0x000042A8 File Offset: 0x000024A8
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
				return text.Length >= this.MinLength;
			}
			return false;
		}
	}
}
