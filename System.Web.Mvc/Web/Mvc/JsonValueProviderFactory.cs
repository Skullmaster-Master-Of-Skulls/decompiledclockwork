using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Web.Mvc.Properties;
using System.Web.Script.Serialization;

namespace System.Web.Mvc
{
	// Token: 0x02000107 RID: 263
	public sealed class JsonValueProviderFactory : ValueProviderFactory
	{
		// Token: 0x06000721 RID: 1825 RVA: 0x0001344C File Offset: 0x0001164C
		private static void AddToBackingStore(JsonValueProviderFactory.EntryLimitedDictionary backingStore, string prefix, object value)
		{
			IDictionary<string, object> dictionary = value as IDictionary<string, object>;
			if (dictionary != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in dictionary)
				{
					JsonValueProviderFactory.AddToBackingStore(backingStore, JsonValueProviderFactory.MakePropertyKey(prefix, keyValuePair.Key), keyValuePair.Value);
				}
				return;
			}
			IList list = value as IList;
			if (list != null)
			{
				for (int i = 0; i < list.Count; i++)
				{
					JsonValueProviderFactory.AddToBackingStore(backingStore, JsonValueProviderFactory.MakeArrayKey(prefix, i), list[i]);
				}
				return;
			}
			backingStore.Add(prefix, value);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x000134F0 File Offset: 0x000116F0
		private static object GetDeserializedObject(ControllerContext controllerContext)
		{
			if (!controllerContext.HttpContext.Request.ContentType.StartsWith("application/json", StringComparison.OrdinalIgnoreCase))
			{
				return null;
			}
			StreamReader streamReader = new StreamReader(controllerContext.HttpContext.Request.InputStream);
			string text = streamReader.ReadToEnd();
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			return javaScriptSerializer.DeserializeObject(text);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00013554 File Offset: 0x00011754
		public override IValueProvider GetValueProvider(ControllerContext controllerContext)
		{
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}
			object deserializedObject = JsonValueProviderFactory.GetDeserializedObject(controllerContext);
			if (deserializedObject == null)
			{
				return null;
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			JsonValueProviderFactory.EntryLimitedDictionary backingStore = new JsonValueProviderFactory.EntryLimitedDictionary(dictionary);
			JsonValueProviderFactory.AddToBackingStore(backingStore, string.Empty, deserializedObject);
			return new DictionaryValueProvider<object>(dictionary, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x000135A4 File Offset: 0x000117A4
		private static string MakeArrayKey(string prefix, int index)
		{
			return prefix + "[" + index.ToString(CultureInfo.InvariantCulture) + "]";
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x000135C2 File Offset: 0x000117C2
		private static string MakePropertyKey(string prefix, string propertyName)
		{
			if (!string.IsNullOrEmpty(prefix))
			{
				return prefix + "." + propertyName;
			}
			return propertyName;
		}

		// Token: 0x02000108 RID: 264
		private class EntryLimitedDictionary
		{
			// Token: 0x06000727 RID: 1831 RVA: 0x000135E2 File Offset: 0x000117E2
			public EntryLimitedDictionary(IDictionary<string, object> innerDictionary)
			{
				this._innerDictionary = innerDictionary;
			}

			// Token: 0x06000728 RID: 1832 RVA: 0x000135F4 File Offset: 0x000117F4
			public void Add(string key, object value)
			{
				if (++this._itemCount > JsonValueProviderFactory.EntryLimitedDictionary._maximumDepth)
				{
					throw new InvalidOperationException(MvcResources.JsonValueProviderFactory_RequestTooLarge);
				}
				this._innerDictionary.Add(key, value);
			}

			// Token: 0x06000729 RID: 1833 RVA: 0x00013634 File Offset: 0x00011834
			private static int GetMaximumDepth()
			{
				NameValueCollection appSettings = ConfigurationManager.AppSettings;
				if (appSettings != null)
				{
					string[] values = appSettings.GetValues("aspnet:MaxJsonDeserializerMembers");
					int result;
					if (values != null && values.Length > 0 && int.TryParse(values[0], out result))
					{
						return result;
					}
				}
				return 1000;
			}

			// Token: 0x040001F8 RID: 504
			private static int _maximumDepth = JsonValueProviderFactory.EntryLimitedDictionary.GetMaximumDepth();

			// Token: 0x040001F9 RID: 505
			private readonly IDictionary<string, object> _innerDictionary;

			// Token: 0x040001FA RID: 506
			private int _itemCount;
		}
	}
}
