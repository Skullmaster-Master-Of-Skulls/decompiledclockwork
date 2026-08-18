using System;
using System.Globalization;

namespace System.Web.Mvc
{
	// Token: 0x02000117 RID: 279
	public sealed class RouteDataValueProvider : DictionaryValueProvider<object>
	{
		// Token: 0x0600075E RID: 1886 RVA: 0x00013D1F File Offset: 0x00011F1F
		public RouteDataValueProvider(ControllerContext controllerContext) : base(controllerContext.RouteData.Values, CultureInfo.InvariantCulture)
		{
		}
	}
}
