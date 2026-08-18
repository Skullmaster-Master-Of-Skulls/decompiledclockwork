using System;
using System.Collections.Generic;
using System.Web.Http.Internal;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x0200013E RID: 318
	public sealed class CollectionModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x060007E3 RID: 2019 RVA: 0x0001A461 File Offset: 0x00018661
		public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
		{
			return CollectionModelBinderUtil.GetGenericBinder(typeof(ICollection<>), typeof(List<>), typeof(CollectionModelBinder<>), modelType);
		}
	}
}
