using System;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x020000FB RID: 251
	public abstract class ModelBinderProvider
	{
		// Token: 0x0600061E RID: 1566
		public abstract IModelBinder GetBinder(HttpConfiguration configuration, Type modelType);
	}
}
