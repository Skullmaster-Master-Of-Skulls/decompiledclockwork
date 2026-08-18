using System;
using System.Globalization;
using System.Web.Routing;

namespace System.Web.ModelBinding
{
	// Token: 0x02000685 RID: 1669
	public sealed class RouteDataValueProvider : DictionaryValueProvider<object>
	{
		// Token: 0x060050F4 RID: 20724 RVA: 0x001171B7 File Offset: 0x001153B7
		public RouteDataValueProvider(ModelBindingExecutionContext modelBindingExecutionContext) : base(RouteDataValueProvider.GetRouteValues(modelBindingExecutionContext), CultureInfo.InvariantCulture)
		{
		}

		// Token: 0x060050F5 RID: 20725 RVA: 0x001171CC File Offset: 0x001153CC
		private static RouteValueDictionary GetRouteValues(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			RouteData service = modelBindingExecutionContext.GetService<RouteData>();
			if (service != null)
			{
				return service.Values;
			}
			return new RouteValueDictionary();
		}
	}
}
