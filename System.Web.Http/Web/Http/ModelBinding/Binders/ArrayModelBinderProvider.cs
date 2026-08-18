using System;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x0200013A RID: 314
	public sealed class ArrayModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x060007D7 RID: 2007 RVA: 0x0001A130 File Offset: 0x00018330
		public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
		{
			if (modelType == null)
			{
				throw Error.ArgumentNull("modelType");
			}
			if (!modelType.IsArray)
			{
				return null;
			}
			Type elementType = modelType.GetElementType();
			return (IModelBinder)Activator.CreateInstance(typeof(ArrayModelBinder<>).MakeGenericType(new Type[]
			{
				elementType
			}));
		}
	}
}
