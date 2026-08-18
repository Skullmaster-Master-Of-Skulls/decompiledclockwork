using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace System.Web.ModelBinding
{
	// Token: 0x02000679 RID: 1657
	public sealed class ModelBinderProviderCollection : Collection<ModelBinderProvider>
	{
		// Token: 0x06005092 RID: 20626 RVA: 0x00115F3D File Offset: 0x0011413D
		public ModelBinderProviderCollection()
		{
		}

		// Token: 0x06005093 RID: 20627 RVA: 0x00115F45 File Offset: 0x00114145
		public ModelBinderProviderCollection(IList<ModelBinderProvider> list) : base(list)
		{
		}

		// Token: 0x06005094 RID: 20628 RVA: 0x00115F50 File Offset: 0x00114150
		public IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			if (modelBindingExecutionContext == null)
			{
				throw new ArgumentNullException("modelBindingExecutionContext");
			}
			if (bindingContext == null)
			{
				throw new ArgumentNullException("bindingContext");
			}
			ModelBinderProvider modelBinderProvider;
			if (ModelBinderProviderCollection.TryGetProviderFromAttributes(bindingContext.ModelType, out modelBinderProvider))
			{
				return modelBinderProvider.GetBinder(modelBindingExecutionContext, bindingContext);
			}
			return (from provider in this
			let binder = provider.GetBinder(modelBindingExecutionContext, bindingContext)
			where binder != null
			select binder).FirstOrDefault<IModelBinder>();
		}

		// Token: 0x06005095 RID: 20629 RVA: 0x00116020 File Offset: 0x00114220
		internal IModelBinder GetRequiredBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			IModelBinder binder = this.GetBinder(modelBindingExecutionContext, bindingContext);
			if (binder == null)
			{
				throw Error.ModelBinderProviderCollection_BinderForTypeNotFound(bindingContext.ModelType);
			}
			return binder;
		}

		// Token: 0x06005096 RID: 20630 RVA: 0x00116046 File Offset: 0x00114246
		protected override void InsertItem(int index, ModelBinderProvider item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			base.InsertItem(index, item);
		}

		// Token: 0x06005097 RID: 20631 RVA: 0x00116060 File Offset: 0x00114260
		private void InsertSimpleProviderAtFront(ModelBinderProvider provider)
		{
			int num = 0;
			while (num < base.Count && ModelBinderProviderCollection.ShouldProviderGoFirst(base[num]))
			{
				num++;
			}
			base.InsertItem(num, provider);
		}

		// Token: 0x06005098 RID: 20632 RVA: 0x00116094 File Offset: 0x00114294
		public void RegisterBinderForGenericType(Type modelType, IModelBinder modelBinder)
		{
			this.InsertSimpleProviderAtFront(new GenericModelBinderProvider(modelType, modelBinder));
		}

		// Token: 0x06005099 RID: 20633 RVA: 0x001160A3 File Offset: 0x001142A3
		public void RegisterBinderForGenericType(Type modelType, Func<Type[], IModelBinder> modelBinderFactory)
		{
			this.InsertSimpleProviderAtFront(new GenericModelBinderProvider(modelType, modelBinderFactory));
		}

		// Token: 0x0600509A RID: 20634 RVA: 0x001160B2 File Offset: 0x001142B2
		public void RegisterBinderForGenericType(Type modelType, Type modelBinderType)
		{
			this.InsertSimpleProviderAtFront(new GenericModelBinderProvider(modelType, modelBinderType));
		}

		// Token: 0x0600509B RID: 20635 RVA: 0x001160C1 File Offset: 0x001142C1
		public void RegisterBinderForType(Type modelType, IModelBinder modelBinder)
		{
			this.RegisterBinderForType(modelType, modelBinder, false);
		}

		// Token: 0x0600509C RID: 20636 RVA: 0x001160CC File Offset: 0x001142CC
		internal void RegisterBinderForType(Type modelType, IModelBinder modelBinder, bool suppressPrefixCheck)
		{
			SimpleModelBinderProvider provider = new SimpleModelBinderProvider(modelType, modelBinder)
			{
				SuppressPrefixCheck = suppressPrefixCheck
			};
			this.InsertSimpleProviderAtFront(provider);
		}

		// Token: 0x0600509D RID: 20637 RVA: 0x001160EF File Offset: 0x001142EF
		public void RegisterBinderForType(Type modelType, Func<IModelBinder> modelBinderFactory)
		{
			this.InsertSimpleProviderAtFront(new SimpleModelBinderProvider(modelType, modelBinderFactory));
		}

		// Token: 0x0600509E RID: 20638 RVA: 0x001160FE File Offset: 0x001142FE
		protected override void SetItem(int index, ModelBinderProvider item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			base.SetItem(index, item);
		}

		// Token: 0x0600509F RID: 20639 RVA: 0x00116118 File Offset: 0x00114318
		private static bool ShouldProviderGoFirst(ModelBinderProvider provider)
		{
			ModelBinderProviderOptionsAttribute modelBinderProviderOptionsAttribute = provider.GetType().GetCustomAttributes(typeof(ModelBinderProviderOptionsAttribute), true).OfType<ModelBinderProviderOptionsAttribute>().FirstOrDefault<ModelBinderProviderOptionsAttribute>();
			return modelBinderProviderOptionsAttribute != null && modelBinderProviderOptionsAttribute.FrontOfList;
		}

		// Token: 0x060050A0 RID: 20640 RVA: 0x00116154 File Offset: 0x00114354
		private static bool TryGetProviderFromAttributes(Type modelType, out ModelBinderProvider provider)
		{
			ExtensibleModelBinderAttribute extensibleModelBinderAttribute = TypeDescriptorHelper.Get(modelType).GetAttributes().OfType<ExtensibleModelBinderAttribute>().FirstOrDefault<ExtensibleModelBinderAttribute>();
			if (extensibleModelBinderAttribute == null)
			{
				provider = null;
				return false;
			}
			if (typeof(ModelBinderProvider).IsAssignableFrom(extensibleModelBinderAttribute.BinderType))
			{
				provider = (ModelBinderProvider)SecurityUtils.SecureCreateInstance(extensibleModelBinderAttribute.BinderType);
			}
			else
			{
				if (!typeof(IModelBinder).IsAssignableFrom(extensibleModelBinderAttribute.BinderType))
				{
					string message = string.Format(CultureInfo.CurrentCulture, SR.GetString("ModelBinderProviderCollection_InvalidBinderType"), new object[]
					{
						extensibleModelBinderAttribute.BinderType,
						typeof(ModelBinderProvider),
						typeof(IModelBinder)
					});
					throw new InvalidOperationException(message);
				}
				Type type = extensibleModelBinderAttribute.BinderType.IsGenericTypeDefinition ? extensibleModelBinderAttribute.BinderType.MakeGenericType(modelType.GetGenericArguments()) : extensibleModelBinderAttribute.BinderType;
				IModelBinder modelBinder = (IModelBinder)SecurityUtils.SecureCreateInstance(type);
				provider = new SimpleModelBinderProvider(modelType, modelBinder)
				{
					SuppressPrefixCheck = extensibleModelBinderAttribute.SuppressPrefixCheck
				};
			}
			return true;
		}
	}
}
