using System;
using System.Collections.Generic;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x02000148 RID: 328
	public sealed class KeyValuePairModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x06000820 RID: 2080 RVA: 0x0001AA68 File Offset: 0x00018C68
		public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
		{
			return ModelBindingHelper.GetPossibleBinderInstance(modelType, typeof(KeyValuePair<, >), typeof(KeyValuePairModelBinder<, >));
		}
	}
}
