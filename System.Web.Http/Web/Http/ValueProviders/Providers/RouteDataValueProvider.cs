using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace System.Web.Http.ValueProviders.Providers
{
	// Token: 0x020001A1 RID: 417
	public class RouteDataValueProvider : NameValuePairsValueProvider
	{
		// Token: 0x06000A91 RID: 2705 RVA: 0x0002365C File Offset: 0x0002185C
		public RouteDataValueProvider(HttpActionContext actionContext, CultureInfo culture) : base(RouteDataValueProvider.GetRouteValues(actionContext.ControllerContext.RouteData), culture)
		{
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00023848 File Offset: 0x00021A48
		internal static IEnumerable<KeyValuePair<string, string>> GetRouteValues(IHttpRouteData routeData)
		{
			foreach (KeyValuePair<string, object> pair in routeData.Values)
			{
				KeyValuePair<string, object> keyValuePair = pair;
				string text;
				if (keyValuePair.Value != null)
				{
					KeyValuePair<string, object> keyValuePair2 = pair;
					text = keyValuePair2.Value.ToString();
				}
				else
				{
					text = null;
				}
				string value = text;
				KeyValuePair<string, object> keyValuePair3 = pair;
				yield return new KeyValuePair<string, string>(keyValuePair3.Key, value);
			}
			yield break;
		}
	}
}
