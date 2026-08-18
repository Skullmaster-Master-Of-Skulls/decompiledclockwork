using System;
using System.Linq;

namespace System.Web.ModelBinding
{
	// Token: 0x0200062D RID: 1581
	public sealed class BinaryDataModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x06004EDD RID: 20189 RVA: 0x00112674 File Offset: 0x00110874
		public override IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			return (from provider in BinaryDataModelBinderProvider._providers
			let binder = provider.GetBinder(modelBindingExecutionContext, bindingContext)
			where binder != null
			select binder).FirstOrDefault<IModelBinder>();
		}

		// Token: 0x04002A53 RID: 10835
		private static readonly ModelBinderProvider[] _providers = new ModelBinderProvider[]
		{
			new SimpleModelBinderProvider(typeof(byte[]), new BinaryDataModelBinderProvider.ByteArrayExtensibleModelBinder())
		};

		// Token: 0x02000A17 RID: 2583
		private class ByteArrayExtensibleModelBinder : IModelBinder
		{
			// Token: 0x06006DF0 RID: 28144 RVA: 0x00188F58 File Offset: 0x00187158
			public bool BindModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
			{
				ModelBinderUtil.ValidateBindingContext(bindingContext);
				ValueProviderResult value = bindingContext.UnvalidatedValueProvider.GetValue(bindingContext.ModelName);
				if (value == null)
				{
					return false;
				}
				string text = (string)value.ConvertTo(typeof(string));
				if (string.IsNullOrEmpty(text))
				{
					return false;
				}
				string s = text.Replace("\"", string.Empty);
				bool result;
				try
				{
					bindingContext.Model = this.ConvertByteArray(Convert.FromBase64String(s));
					result = true;
				}
				catch
				{
					result = false;
				}
				return result;
			}

			// Token: 0x06006DF1 RID: 28145 RVA: 0x00036414 File Offset: 0x00034614
			protected virtual object ConvertByteArray(byte[] originalModel)
			{
				return originalModel;
			}
		}
	}
}
