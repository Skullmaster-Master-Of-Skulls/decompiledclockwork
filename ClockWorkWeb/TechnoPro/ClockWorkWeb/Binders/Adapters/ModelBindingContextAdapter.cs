using System;
using System.Web.Mvc;

namespace TechnoPro.ClockWorkWeb.Binders.Adapters
{
	// Token: 0x0200015E RID: 350
	public static class ModelBindingContextAdapter
	{
		// Token: 0x06000A9C RID: 2716 RVA: 0x00048C54 File Offset: 0x00046E54
		internal static string GetValue(this ModelBindingContext bindingContext, string key)
		{
			ValueProviderResult value = bindingContext.ValueProvider.GetValue(key);
			return (value == null) ? null : value.AttemptedValue;
		}
	}
}
