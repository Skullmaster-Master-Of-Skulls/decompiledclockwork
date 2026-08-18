using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Http.Controllers;
using System.Web.Http.Internal;
using System.Web.Http.ValueProviders;
using System.Web.Http.ValueProviders.Providers;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x0200013B RID: 315
	public class CollectionModelBinder<TElement> : IModelBinder
	{
		// Token: 0x060007D9 RID: 2009 RVA: 0x0001A190 File Offset: 0x00018390
		private static List<TElement> BindComplexCollection(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			string key = ModelBindingHelper.CreatePropertyModelName(bindingContext.ModelName, "index");
			ValueProviderResult value = bindingContext.ValueProvider.GetValue(key);
			IEnumerable<string> indexNamesFromValueProviderResult = CollectionModelBinderUtil.GetIndexNamesFromValueProviderResult(value);
			return CollectionModelBinder<TElement>.BindComplexCollectionFromIndexes(actionContext, bindingContext, indexNamesFromValueProviderResult);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0001A1CC File Offset: 0x000183CC
		internal static List<TElement> BindComplexCollectionFromIndexes(HttpActionContext actionContext, ModelBindingContext bindingContext, IEnumerable<string> indexNames)
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
				string modelName = ModelBindingHelper.CreateIndexModelName(bindingContext.ModelName, index);
				ModelBindingContext modelBindingContext = new ModelBindingContext(bindingContext)
				{
					ModelMetadata = actionContext.GetMetadataProvider().GetMetadataForType(null, typeof(TElement)),
					ModelName = modelName
				};
				bool flag2 = false;
				object model = null;
				if (actionContext.Bind(modelBindingContext))
				{
					flag2 = true;
					model = modelBindingContext.Model;
					bindingContext.ValidationNode.ChildNodes.Add(modelBindingContext.ValidationNode);
				}
				if (!flag2 && !flag)
				{
					break;
				}
				list.Add(ModelBindingHelper.CastOrDefault<TElement>(model));
			}
			return list;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0001A2B4 File Offset: 0x000184B4
		public virtual bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			ModelBindingHelper.ValidateBindingContext(bindingContext);
			if (!bindingContext.ValueProvider.ContainsPrefix(bindingContext.ModelName))
			{
				return false;
			}
			ValueProviderResult value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
			List<TElement> newCollection = (value != null) ? CollectionModelBinder<TElement>.BindSimpleCollection(actionContext, bindingContext, value.RawValue, value.Culture) : CollectionModelBinder<TElement>.BindComplexCollection(actionContext, bindingContext);
			return this.CreateOrReplaceCollection(actionContext, bindingContext, newCollection);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0001A31C File Offset: 0x0001851C
		internal static List<TElement> BindSimpleCollection(HttpActionContext actionContext, ModelBindingContext bindingContext, object rawValue, CultureInfo culture)
		{
			if (rawValue == null)
			{
				return null;
			}
			List<TElement> list = new List<TElement>();
			object[] array = ModelBindingHelper.RawValueToObjectArray(rawValue);
			foreach (object rawValue2 in array)
			{
				ModelBindingContext modelBindingContext = new ModelBindingContext(bindingContext)
				{
					ModelMetadata = actionContext.GetMetadataProvider().GetMetadataForType(null, typeof(TElement)),
					ModelName = bindingContext.ModelName,
					ValueProvider = new CompositeValueProvider
					{
						new ElementalValueProvider(bindingContext.ModelName, rawValue2, culture),
						bindingContext.ValueProvider
					}
				};
				object model = null;
				if (actionContext.Bind(modelBindingContext))
				{
					model = modelBindingContext.Model;
					bindingContext.ValidationNode.ChildNodes.Add(modelBindingContext.ValidationNode);
				}
				list.Add(ModelBindingHelper.CastOrDefault<TElement>(model));
			}
			return list;
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0001A402 File Offset: 0x00018602
		protected virtual bool CreateOrReplaceCollection(HttpActionContext actionContext, ModelBindingContext bindingContext, IList<TElement> newCollection)
		{
			CollectionModelBinderUtil.CreateOrReplaceCollection<TElement>(bindingContext, newCollection, () => new List<TElement>());
			return true;
		}
	}
}
