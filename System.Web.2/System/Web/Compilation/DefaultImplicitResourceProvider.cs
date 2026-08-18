using System;
using System.Collections;
using System.Globalization;
using System.Resources;

namespace System.Web.Compilation
{
	// Token: 0x02000845 RID: 2117
	internal class DefaultImplicitResourceProvider : IImplicitResourceProvider
	{
		// Token: 0x060064A5 RID: 25765 RVA: 0x001609A2 File Offset: 0x0015EBA2
		internal DefaultImplicitResourceProvider(IResourceProvider resourceProvider)
		{
			this._resourceProvider = resourceProvider;
		}

		// Token: 0x060064A6 RID: 25766 RVA: 0x001609B4 File Offset: 0x0015EBB4
		public virtual object GetObject(ImplicitResourceKey entry, CultureInfo culture)
		{
			string resourceKey = DefaultImplicitResourceProvider.ConstructFullKey(entry);
			return this._resourceProvider.GetObject(resourceKey, culture);
		}

		// Token: 0x060064A7 RID: 25767 RVA: 0x001609D5 File Offset: 0x0015EBD5
		public virtual ICollection GetImplicitResourceKeys(string keyPrefix)
		{
			this.EnsureGetPageResources();
			if (this._implicitResources == null)
			{
				return null;
			}
			return (ICollection)this._implicitResources[keyPrefix];
		}

		// Token: 0x060064A8 RID: 25768 RVA: 0x001609F8 File Offset: 0x0015EBF8
		internal void EnsureGetPageResources()
		{
			if (this._attemptedGetPageResources)
			{
				return;
			}
			this._attemptedGetPageResources = true;
			IResourceReader resourceReader = this._resourceProvider.ResourceReader;
			if (resourceReader == null)
			{
				return;
			}
			this._implicitResources = new Hashtable(StringComparer.OrdinalIgnoreCase);
			foreach (object obj in resourceReader)
			{
				ImplicitResourceKey implicitResourceKey = DefaultImplicitResourceProvider.ParseFullKey((string)((DictionaryEntry)obj).Key);
				if (implicitResourceKey != null)
				{
					ArrayList arrayList = (ArrayList)this._implicitResources[implicitResourceKey.KeyPrefix];
					if (arrayList == null)
					{
						arrayList = new ArrayList();
						this._implicitResources[implicitResourceKey.KeyPrefix] = arrayList;
					}
					arrayList.Add(implicitResourceKey);
				}
			}
		}

		// Token: 0x060064A9 RID: 25769 RVA: 0x00160ACC File Offset: 0x0015ECCC
		private static ImplicitResourceKey ParseFullKey(string key)
		{
			string filter = string.Empty;
			if (key.IndexOf(':') > 0)
			{
				string[] array = key.Split(new char[]
				{
					':'
				});
				if (array.Length > 2)
				{
					return null;
				}
				filter = array[0];
				key = array[1];
			}
			int num = key.IndexOf('.');
			if (num <= 0)
			{
				return null;
			}
			string keyPrefix = key.Substring(0, num);
			string property = key.Substring(num + 1);
			return new ImplicitResourceKey
			{
				Filter = filter,
				KeyPrefix = keyPrefix,
				Property = property
			};
		}

		// Token: 0x060064AA RID: 25770 RVA: 0x00160B54 File Offset: 0x0015ED54
		private static string ConstructFullKey(ImplicitResourceKey entry)
		{
			string text = entry.KeyPrefix + "." + entry.Property;
			if (entry.Filter.Length > 0)
			{
				text = entry.Filter + ":" + text;
			}
			return text;
		}

		// Token: 0x040033F2 RID: 13298
		private IResourceProvider _resourceProvider;

		// Token: 0x040033F3 RID: 13299
		private IDictionary _implicitResources;

		// Token: 0x040033F4 RID: 13300
		private bool _attemptedGetPageResources;
	}
}
