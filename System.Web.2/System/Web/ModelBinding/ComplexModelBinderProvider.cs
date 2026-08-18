using System;

namespace System.Web.ModelBinding
{
	// Token: 0x02000637 RID: 1591
	public sealed class ComplexModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x06004EFE RID: 20222 RVA: 0x00112DD8 File Offset: 0x00110FD8
		public override IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			return ComplexModelBinderProvider._underlyingProvider.GetBinder(modelBindingExecutionContext, bindingContext);
		}

		// Token: 0x06004EFF RID: 20223 RVA: 0x00112DE6 File Offset: 0x00110FE6
		private static SimpleModelBinderProvider GetUnderlyingProvider()
		{
			return new SimpleModelBinderProvider(typeof(ComplexModel), new ComplexModelBinder())
			{
				SuppressPrefixCheck = true
			};
		}

		// Token: 0x04002A5D RID: 10845
		private static readonly SimpleModelBinderProvider _underlyingProvider = ComplexModelBinderProvider.GetUnderlyingProvider();
	}
}
