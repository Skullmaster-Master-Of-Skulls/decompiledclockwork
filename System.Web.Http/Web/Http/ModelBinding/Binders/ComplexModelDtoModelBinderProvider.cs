using System;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x02000141 RID: 321
	public sealed class ComplexModelDtoModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x060007EE RID: 2030 RVA: 0x0001A5D8 File Offset: 0x000187D8
		public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
		{
			return ComplexModelDtoModelBinderProvider._underlyingProvider.GetBinder(configuration, modelType);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0001A5E8 File Offset: 0x000187E8
		private static SimpleModelBinderProvider GetUnderlyingProvider()
		{
			return new SimpleModelBinderProvider(typeof(ComplexModelDto), new ComplexModelDtoModelBinder())
			{
				SuppressPrefixCheck = true
			};
		}

		// Token: 0x04000250 RID: 592
		private static readonly SimpleModelBinderProvider _underlyingProvider = ComplexModelDtoModelBinderProvider.GetUnderlyingProvider();
	}
}
