using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.Routing;

namespace System.Web.Mvc.Routing.Constraints
{
	// Token: 0x02000015 RID: 21
	public class RegexRouteConstraint : IRouteConstraint
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00003BB4 File Offset: 0x00001DB4
		public RegexRouteConstraint(string pattern)
		{
			this.Pattern = pattern;
			this._regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003BD4 File Offset: 0x00001DD4
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00003BDC File Offset: 0x00001DDC
		public string Pattern { get; private set; }

		// Token: 0x06000083 RID: 131 RVA: 0x00003BE8 File Offset: 0x00001DE8
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
				string input = Convert.ToString(obj, CultureInfo.InvariantCulture);
				return this._regex.IsMatch(input);
			}
			return false;
		}

		// Token: 0x04000021 RID: 33
		private readonly Regex _regex;
	}
}
