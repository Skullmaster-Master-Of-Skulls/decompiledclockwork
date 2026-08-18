using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x02000088 RID: 136
	public class RegexRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x06000377 RID: 887 RVA: 0x0000AE2F File Offset: 0x0000902F
		public RegexRouteConstraint(string pattern)
		{
			this.Pattern = pattern;
			this._regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000AE4F File Offset: 0x0000904F
		// (set) Token: 0x06000379 RID: 889 RVA: 0x0000AE57 File Offset: 0x00009057
		public string Pattern { get; private set; }

		// Token: 0x0600037A RID: 890 RVA: 0x0000AE60 File Offset: 0x00009060
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
				string input = Convert.ToString(obj, CultureInfo.InvariantCulture);
				return this._regex.IsMatch(input);
			}
			return false;
		}

		// Token: 0x04000103 RID: 259
		private readonly Regex _regex;
	}
}
