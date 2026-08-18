using System;

namespace System.Web.Mvc
{
	// Token: 0x020000BA RID: 186
	public interface IModelBinderProvider
	{
		// Token: 0x060004F8 RID: 1272
		IModelBinder GetBinder(Type modelType);
	}
}
