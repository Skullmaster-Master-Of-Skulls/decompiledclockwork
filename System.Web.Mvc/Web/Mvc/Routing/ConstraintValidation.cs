using System;
using System.Collections.Generic;
using System.Web.Mvc.Properties;
using System.Web.Routing;

namespace System.Web.Mvc.Routing
{
	// Token: 0x02000048 RID: 72
	internal static class ConstraintValidation
	{
		// Token: 0x060001F1 RID: 497 RVA: 0x00007340 File Offset: 0x00005540
		public static void Validate(Route route)
		{
			if (route.Constraints == null)
			{
				return;
			}
			foreach (KeyValuePair<string, object> keyValuePair in route.Constraints)
			{
				if (!(keyValuePair.Value is string) && !(keyValuePair.Value is IRouteConstraint))
				{
					throw Error.InvalidOperation(MvcResources.Route_InvalidConstraint, new object[]
					{
						keyValuePair.Key,
						route.Url,
						typeof(IRouteConstraint).FullName
					});
				}
			}
		}
	}
}
