using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Http.Controllers;

namespace System.Web.Http.ValueProviders.Providers
{
	// Token: 0x020001A4 RID: 420
	public class QueryStringValueProviderFactory : ValueProviderFactory, IUriValueProviderFactory
	{
		// Token: 0x06000A96 RID: 2710 RVA: 0x000238D8 File Offset: 0x00021AD8
		public override IValueProvider GetValueProvider(HttpActionContext actionContext)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			IDictionary<string, object> properties = actionContext.Request.Properties;
			QueryStringValueProvider queryStringValueProvider;
			if (!properties.TryGetValue("{8572540D-3BD9-46DA-B112-A1E6C9086003}", out queryStringValueProvider))
			{
				queryStringValueProvider = new QueryStringValueProvider(actionContext, CultureInfo.InvariantCulture);
				properties["{8572540D-3BD9-46DA-B112-A1E6C9086003}"] = queryStringValueProvider;
			}
			return queryStringValueProvider;
		}

		// Token: 0x04000319 RID: 793
		private const string RequestLocalStorageKey = "{8572540D-3BD9-46DA-B112-A1E6C9086003}";
	}
}
