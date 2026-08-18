using System;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x0200017F RID: 383
	public sealed class MutableObjectModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x06000A04 RID: 2564 RVA: 0x00021330 File Offset: 0x0001F530
		public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
		{
			if (!MutableObjectModelBinder.CanBindType(modelType))
			{
				return null;
			}
			return new MutableObjectModelBinder();
		}
	}
}
