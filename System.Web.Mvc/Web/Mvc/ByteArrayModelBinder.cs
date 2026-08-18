using System;

namespace System.Web.Mvc
{
	// Token: 0x02000128 RID: 296
	public class ByteArrayModelBinder : IModelBinder
	{
		// Token: 0x060007C5 RID: 1989 RVA: 0x00014F14 File Offset: 0x00013114
		public virtual object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			if (bindingContext == null)
			{
				throw new ArgumentNullException("bindingContext");
			}
			ValueProviderResult value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
			if (value == null)
			{
				return null;
			}
			string attemptedValue = value.AttemptedValue;
			if (string.IsNullOrEmpty(attemptedValue))
			{
				return null;
			}
			string s = attemptedValue.Replace("\"", string.Empty);
			return Convert.FromBase64String(s);
		}
	}
}
