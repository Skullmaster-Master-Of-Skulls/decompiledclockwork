using System;

namespace System.Web.ModelBinding
{
	// Token: 0x0200067B RID: 1659
	public static class ModelBinderProviders
	{
		// Token: 0x17001734 RID: 5940
		// (get) Token: 0x060050A4 RID: 20644 RVA: 0x00116265 File Offset: 0x00114465
		public static ModelBinderProviderCollection Providers
		{
			get
			{
				return ModelBinderProviders._providers;
			}
		}

		// Token: 0x060050A5 RID: 20645 RVA: 0x0011626C File Offset: 0x0011446C
		private static ModelBinderProviderCollection CreateDefaultCollection()
		{
			return new ModelBinderProviderCollection
			{
				new TypeMatchModelBinderProvider(),
				new BinaryDataModelBinderProvider(),
				new KeyValuePairModelBinderProvider(),
				new ComplexModelBinderProvider(),
				new ArrayModelBinderProvider(),
				new DictionaryModelBinderProvider(),
				new CollectionModelBinderProvider(),
				new TypeConverterModelBinderProvider(),
				new MutableObjectModelBinderProvider()
			};
		}

		// Token: 0x04002ACB RID: 10955
		private static readonly ModelBinderProviderCollection _providers = ModelBinderProviders.CreateDefaultCollection();
	}
}
