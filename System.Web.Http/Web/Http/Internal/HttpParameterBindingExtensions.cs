using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace System.Web.Http.Internal
{
	// Token: 0x020000C4 RID: 196
	internal static class HttpParameterBindingExtensions
	{
		// Token: 0x06000482 RID: 1154 RVA: 0x0000E62C File Offset: 0x0000C82C
		public static bool WillReadUri(this HttpParameterBinding parameterBinding)
		{
			if (parameterBinding == null)
			{
				throw Error.ArgumentNull("parameterBinding");
			}
			IValueProviderParameterBinding valueProviderParameterBinding = parameterBinding as IValueProviderParameterBinding;
			if (valueProviderParameterBinding != null)
			{
				IEnumerable<ValueProviderFactory> valueProviderFactories = valueProviderParameterBinding.ValueProviderFactories;
				if (valueProviderFactories.Any<ValueProviderFactory>())
				{
					if (valueProviderFactories.All((ValueProviderFactory factory) => factory is IUriValueProviderFactory))
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
