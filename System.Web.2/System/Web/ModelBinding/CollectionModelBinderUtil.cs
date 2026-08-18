using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Web.ModelBinding
{
	// Token: 0x02000633 RID: 1587
	internal static class CollectionModelBinderUtil
	{
		// Token: 0x06004EE9 RID: 20201 RVA: 0x001127B8 File Offset: 0x001109B8
		public static void CreateOrReplaceCollection<TElement>(ModelBindingContext bindingContext, IEnumerable<TElement> incomingElements, Func<ICollection<TElement>> creator)
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

		// Token: 0x06004EEA RID: 20202 RVA: 0x0011282C File Offset: 0x00110A2C
		public static void CreateOrReplaceDictionary<TKey, TValue>(ModelBindingContext bindingContext, IEnumerable<KeyValuePair<TKey, TValue>> incomingElements, Func<IDictionary<TKey, TValue>> creator)
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

		// Token: 0x06004EEB RID: 20203 RVA: 0x001128BC File Offset: 0x00110ABC
		public static IModelBinder GetGenericBinder(Type supportedInterfaceType, Type newInstanceType, Type openBinderType, ModelMetadata modelMetadata)
		{
			Type[] typeArgumentsForUpdatableGenericCollection = CollectionModelBinderUtil.GetTypeArgumentsForUpdatableGenericCollection(supportedInterfaceType, newInstanceType, modelMetadata);
			if (typeArgumentsForUpdatableGenericCollection == null)
			{
				return null;
			}
			return (IModelBinder)Activator.CreateInstance(openBinderType.MakeGenericType(typeArgumentsForUpdatableGenericCollection));
		}

		// Token: 0x06004EEC RID: 20204 RVA: 0x001128E8 File Offset: 0x00110AE8
		public static IEnumerable<string> GetIndexNamesFromValueProviderResult(ValueProviderResult vpResultIndex)
		{
			IEnumerable<string> result = null;
			if (vpResultIndex != null)
			{
				string[] array = (string[])vpResultIndex.ConvertTo(typeof(string[]));
				if (array != null && array.Length != 0)
				{
					result = array;
				}
			}
			return result;
		}

		// Token: 0x06004EED RID: 20205 RVA: 0x0011291A File Offset: 0x00110B1A
		public static IEnumerable<string> GetZeroBasedIndexes()
		{
			int i = 0;
			for (;;)
			{
				yield return i.ToString(CultureInfo.InvariantCulture);
				int num = i;
				i = num + 1;
			}
			yield break;
		}

		// Token: 0x06004EEE RID: 20206 RVA: 0x00112924 File Offset: 0x00110B24
		public static Type[] GetTypeArgumentsForUpdatableGenericCollection(Type supportedInterfaceType, Type newInstanceType, ModelMetadata modelMetadata)
		{
			if (!modelMetadata.ModelType.IsGenericType || modelMetadata.ModelType.IsGenericTypeDefinition)
			{
				return null;
			}
			Type[] genericArguments = modelMetadata.ModelType.GetGenericArguments();
			if (genericArguments.Length != supportedInterfaceType.GetGenericArguments().Length)
			{
				return null;
			}
			if (!modelMetadata.IsReadOnly)
			{
				Type c = newInstanceType.MakeGenericType(genericArguments);
				if (modelMetadata.ModelType.IsAssignableFrom(c))
				{
					return genericArguments;
				}
			}
			Type type = supportedInterfaceType.MakeGenericType(genericArguments);
			if (!type.IsInstanceOfType(modelMetadata.Model))
			{
				return null;
			}
			Type type2 = TypeHelpers.ExtractGenericInterface(type, typeof(ICollection<>));
			bool flag = (bool)type2.GetProperty("IsReadOnly").GetValue(modelMetadata.Model, null);
			if (flag)
			{
				return null;
			}
			return genericArguments;
		}
	}
}
