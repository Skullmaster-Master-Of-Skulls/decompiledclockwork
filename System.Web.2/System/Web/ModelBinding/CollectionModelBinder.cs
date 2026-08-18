using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Web.ModelBinding
{
	// Token: 0x02000634 RID: 1588
	public class CollectionModelBinder<TElement> : IModelBinder
	{
		// Token: 0x06004EEF RID: 20207 RVA: 0x001129D8 File Offset: 0x00110BD8
		private static List<TElement> BindComplexCollection(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			string key = ModelBinderUtil.CreatePropertyModelName(bindingContext.ModelName, "index");
			ValueProviderResult value = bindingContext.UnvalidatedValueProvider.GetValue(key);
			IEnumerable<string> indexNamesFromValueProviderResult = CollectionModelBinderUtil.GetIndexNamesFromValueProviderResult(value);
			return CollectionModelBinder<TElement>.BindComplexCollectionFromIndexes(modelBindingExecutionContext, bindingContext, indexNamesFromValueProviderResult);
		}

		// Token: 0x06004EF0 RID: 20208 RVA: 0x00112A14 File Offset: 0x00110C14
		internal static List<TElement> BindComplexCollectionFromIndexes(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext, IEnumerable<string> indexNames)
		{
			bool flag;
			if (indexNames != null)
			{
				flag = true;
			}
			else
			{
				flag = false;
				indexNames = CollectionModelBinderUtil.GetZeroBasedIndexes();
			}
			List<TElement> list = new List<TElement>();
			foreach (string index in indexNames)
			{
				string modelName = ModelBinderUtil.CreateIndexModelName(bindingContext.ModelName, index);
				ModelBindingContext modelBindingContext = new ModelBindingContext(bindingContext)
				{
					ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(TElement)),
					ModelName = modelName
				};
				object model = null;
				IModelBinder binder = bindingContext.ModelBinderProviders.GetBinder(modelBindingExecutionContext, modelBindingContext);
				if (binder != null)
				{
					if (binder.BindModel(modelBindingExecutionContext, modelBindingContext))
					{
						model = modelBindingContext.Model;
						bindingContext.ValidationNode.ChildNodes.Add(modelBindingContext.ValidationNode);
					}
				}
				else if (!flag)
				{
					break;
				}
				list.Add(ModelBinderUtil.CastOrDefault<TElement>(model));
			}
			return list;
		}

		// Token: 0x06004EF1 RID: 20209 RVA: 0x00112B08 File Offset: 0x00110D08
		public virtual bool BindModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			ModelBinderUtil.ValidateBindingContext(bindingContext);
			ValueProviderResult value = bindingContext.UnvalidatedValueProvider.GetValue(bindingContext.ModelName, !bindingContext.ValidateRequest);
			List<TElement> newCollection = (value != null) ? CollectionModelBinder<TElement>.BindSimpleCollection(modelBindingExecutionContext, bindingContext, value.RawValue, value.Culture) : CollectionModelBinder<TElement>.BindComplexCollection(modelBindingExecutionContext, bindingContext);
			return this.CreateOrReplaceCollection(modelBindingExecutionContext, bindingContext, newCollection);
		}

		// Token: 0x06004EF2 RID: 20210 RVA: 0x00112B64 File Offset: 0x00110D64
		internal static List<TElement> BindSimpleCollection(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext, object rawValue, CultureInfo culture)
		{
			if (rawValue == null)
			{
				return null;
			}
			List<TElement> list = new List<TElement>();
			object[] array = ModelBinderUtil.RawValueToObjectArray(rawValue);
			foreach (object rawValue2 in array)
			{
				ModelBindingContext modelBindingContext = new ModelBindingContext(bindingContext)
				{
					ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(TElement)),
					ModelName = bindingContext.ModelName,
					ValueProvider = new ValueProviderCollection
					{
						new ElementalValueProvider(bindingContext.ModelName, rawValue2, culture),
						bindingContext.ValueProvider
					}
				};
				object model = null;
				IModelBinder binder = bindingContext.ModelBinderProviders.GetBinder(modelBindingExecutionContext, modelBindingContext);
				if (binder != null && binder.BindModel(modelBindingExecutionContext, modelBindingContext))
				{
					model = modelBindingContext.Model;
					bindingContext.ValidationNode.ChildNodes.Add(modelBindingContext.ValidationNode);
				}
				list.Add(ModelBinderUtil.CastOrDefault<TElement>(model));
			}
			return list;
		}

		// Token: 0x06004EF3 RID: 20211 RVA: 0x00112C50 File Offset: 0x00110E50
		protected virtual bool CreateOrReplaceCollection(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext, IList<TElement> newCollection)
		{
			CollectionModelBinderUtil.CreateOrReplaceCollection<TElement>(bindingContext, newCollection, () => new List<TElement>());
			return true;
		}
	}
}
