using System;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x02000185 RID: 389
	public sealed class TypeMatchModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x06000A16 RID: 2582 RVA: 0x00021710 File Offset: 0x0001F910
		public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
		{
			return TypeMatchModelBinderProvider._binder;
		}

		// Token: 0x040002FD RID: 765
		private static readonly TypeMatchModelBinder _binder = new TypeMatchModelBinder();
	}
}
