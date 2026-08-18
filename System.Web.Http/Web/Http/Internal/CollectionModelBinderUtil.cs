using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace System.Web.Http.Internal
{
	// Token: 0x0200012F RID: 303
	internal static class CollectionModelBinderUtil
	{
		// Token: 0x06000776 RID: 1910 RVA: 0x00018FEC File Offset: 0x000171EC
		internal static void CreateOrReplaceCollection<TElement>(ModelBindingContext bindingContext, IEnumerable<TElement> incomingElements, Func<ICollection<TElement>> creator)
		{
			ICollection<TElement> collection = bindingContext.Model as ICollection<TElement>;
			if (collection == null || collection.IsReadOnly)
			{
				collection = creator();
				bindingContext.Model = collection;
			}
			collection.Clear();
			foreach (TElement item in incomingElements)
			{
				collection.Add(item);
			}
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00019060 File Offset: 0x00017260
		internal static void CreateOrReplaceDictionary<TKey, TValue>(ModelBindingContext bindingContext, IEnumerable<KeyValuePair<TKey, TValue>> incomingElements, Func<IDictionary<TKey, TValue>> creator)
		{
			IDictionary<TKey, TValue> dictionary = bindingContext.Model as IDictionary<TKey, TValue>;
			if (dictionary == null || dictionary.IsReadOnly)
			{
				dictionary = creator();
				bindingContext.Model = dictionary;
			}
			dictionary.Clear();
			foreach (KeyValuePair<TKey, TValue> keyValuePair in incomingElements)
			{
				if (keyValuePair.Key != null)
				{
					dictionary[keyValuePair.Key] = keyValuePair.Value;
				}
			}
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x000190F0 File Offset: 0x000172F0
		internal static IModelBinder GetGenericBinder(Type supportedInterfaceType, Type newInstanceType, Type openBinderType, Type modelType)
		{
			Type[] genericBinderTypeArgs = CollectionModelBinderUtil.GetGenericBinderTypeArgs(supportedInterfaceType, modelType);
			if (genericBinderTypeArgs == null)
			{
				return null;
			}
			Type c = newInstanceType.MakeGenericType(genericBinderTypeArgs);
			if (!modelType.IsAssignableFrom(c))
			{
				return null;
			}
			Type type = openBinderType.MakeGenericType(genericBinderTypeArgs);
			return (IModelBinder)Activator.CreateInstance(type);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00019134 File Offset: 0x00017334
		internal static Type[] GetGenericBinderTypeArgs(Type supportedInterfaceType, Type modelType)
		{
			if (!modelType.IsGenericType || modelType.IsGenericTypeDefinition)
			{
				return null;
			}
			Type[] genericArguments = modelType.GetGenericArguments();
			if (genericArguments.Length != supportedInterfaceType.GetGenericArguments().Length)
			{
				return null;
			}
			return genericArguments;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001916C File Offset: 0x0001736C
		internal static IEnumerable<string> GetIndexNamesFromValueProviderResult(ValueProviderResult valueProviderResultIndex)
		{
			IEnumerable<string> result = null;
			if (valueProviderResultIndex != null)
			{
				string[] array = (string[])valueProviderResultIndex.ConvertTo(typeof(string[]));
				if (array != null && array.Length > 0)
				{
					result = array;
				}
			}
			return result;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0001927C File Offset: 0x0001747C
		internal static IEnumerable<string> GetZeroBasedIndexes()
		{
			int i = 0;
			for (;;)
			{
				yield return i.ToString(CultureInfo.InvariantCulture);
				i++;
			}
			yield break;
		}
	}
}
