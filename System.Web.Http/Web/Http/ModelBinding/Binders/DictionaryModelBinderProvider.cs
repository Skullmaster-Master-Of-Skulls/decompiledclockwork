using System;
using System.Collections.Generic;
using System.Web.Http.Internal;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x02000143 RID: 323
	public sealed class DictionaryModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x060007F7 RID: 2039 RVA: 0x0001A66C File Offset: 0x0001886C
		public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
		{
			return CollectionModelBinderUtil.GetGenericBinder(typeof(IDictionary<, >), typeof(Dictionary<, >), typeof(DictionaryModelBinder<, >), modelType);
		}
	}
}
