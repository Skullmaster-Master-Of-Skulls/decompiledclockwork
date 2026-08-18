using System;
using System.Web.Http.Internal;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x02000183 RID: 387
	public sealed class TypeConverterModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x06000A11 RID: 2577 RVA: 0x00021586 File Offset: 0x0001F786
		public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
		{
			if (modelType == null)
			{
				throw Error.ArgumentNull("modelType");
			}
			if (!TypeHelper.HasStringConverter(modelType))
			{
				return null;
			}
			return new TypeConverterModelBinder();
		}
	}
}
