using System;
using System.Globalization;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http.ValueProviders.Providers
{
	// Token: 0x020001A3 RID: 419
	public class QueryStringValueProvider : NameValuePairsValueProvider
	{
		// Token: 0x06000A95 RID: 2709 RVA: 0x000238BF File Offset: 0x00021ABF
		public QueryStringValueProvider(HttpActionContext actionContext, CultureInfo culture) : base(actionContext.ControllerContext.Request.GetQueryNameValuePairs(), culture)
		{
		}
	}
}
