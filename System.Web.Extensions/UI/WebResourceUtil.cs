using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Resources;

namespace System.Web.UI
{
	// Token: 0x0200008B RID: 139
	internal static class WebResourceUtil
	{
		// Token: 0x060005F5 RID: 1525 RVA: 0x0001ADF4 File Offset: 0x00018FF4
		public static bool AssemblyContainsWebResource(Assembly assembly, string resourceName)
		{
			if (assembly == AssemblyCache.SystemWebExtensions)
			{
				return WebResourceUtil._systemWebExtensionsCache.Contains(resourceName);
			}
			Tuple<string, Assembly> key = new Tuple<string, Assembly>(resourceName, assembly);
			object obj = WebResourceUtil._assemblyContainsWebResourceCache[key];
			if (obj == null)
			{
				obj = false;
				object[] customAttributes = assembly.GetCustomAttributes(typeof(WebResourceAttribute), false);
				object[] array = customAttributes;
				int i = 0;
				while (i < array.Length)
				{
					WebResourceAttribute webResourceAttribute = (WebResourceAttribute)array[i];
					if (string.Equals(webResourceAttribute.WebResource, resourceName, StringComparison.Ordinal))
					{
						if (assembly.GetManifestResourceStream(resourceName) != null)
						{
							obj = true;
							break;
						}
						throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, AtlasWeb.WebResourceUtil_AssemblyDoesNotContainEmbeddedResource, new object[]
						{
							assembly,
							resourceName
						}));
					}
					else
					{
						i++;
					}
				}
				WebResourceUtil._assemblyContainsWebResourceCache[key] = obj;
			}
			return (bool)obj;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001AEC4 File Offset: 0x000190C4
		private static WebResourceUtil.FastStringLookupTable CreateSystemWebExtensionsCache()
		{
			Assembly systemWebExtensions = AssemblyCache.SystemWebExtensions;
			object[] customAttributes = systemWebExtensions.GetCustomAttributes(typeof(WebResourceAttribute), false);
			IEnumerable<string> strings = from WebResourceAttribute attr in customAttributes
			select attr.WebResource;
			return new WebResourceUtil.FastStringLookupTable(strings);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0001AF1C File Offset: 0x0001911C
		public static void VerifyAssemblyContainsReleaseWebResource(Assembly assembly, string releaseResourceName, Assembly currentAjaxAssembly)
		{
			if (!WebResourceUtil.AssemblyContainsWebResource(assembly, releaseResourceName))
			{
				string message;
				if (assembly == AssemblyCache.SystemWebExtensions)
				{
					message = string.Format(CultureInfo.CurrentUICulture, AtlasWeb.WebResourceUtil_SystemWebExtensionsDoesNotContainReleaseWebResource, new object[]
					{
						currentAjaxAssembly ?? assembly,
						releaseResourceName
					});
				}
				else
				{
					message = string.Format(CultureInfo.CurrentUICulture, AtlasWeb.WebResourceUtil_AssemblyDoesNotContainReleaseWebResource, new object[]
					{
						assembly,
						releaseResourceName
					});
				}
				throw new InvalidOperationException(message);
			}
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0001AF89 File Offset: 0x00019189
		public static void VerifyAssemblyContainsDebugWebResource(Assembly assembly, string debugResourceName)
		{
			if (!WebResourceUtil.AssemblyContainsWebResource(assembly, debugResourceName))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, AtlasWeb.WebResourceUtil_AssemblyDoesNotContainDebugWebResource, new object[]
				{
					assembly,
					debugResourceName
				}));
			}
		}

		// Token: 0x04000227 RID: 551
		private static readonly Hashtable _assemblyContainsWebResourceCache = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04000228 RID: 552
		private static readonly WebResourceUtil.FastStringLookupTable _systemWebExtensionsCache = WebResourceUtil.CreateSystemWebExtensionsCache();

		// Token: 0x0200016B RID: 363
		private class FastStringLookupTable
		{
			// Token: 0x06001045 RID: 4165 RVA: 0x00037E10 File Offset: 0x00036010
			public FastStringLookupTable(IEnumerable<string> strings)
			{
				int num = (from s in strings
				orderby s.Length descending
				select s.Length).First<int>();
				this._table = new string[num + 1][];
				IEnumerable<IGrouping<int, string>> enumerable = from s in strings
				group s by s.Length into g
				select g;
				foreach (IGrouping<int, string> grouping in enumerable)
				{
					this._table[grouping.Key] = grouping.ToArray<string>();
				}
			}

			// Token: 0x06001046 RID: 4166 RVA: 0x00037F14 File Offset: 0x00036114
			public bool Contains(string s)
			{
				if (string.IsNullOrEmpty(s))
				{
					return false;
				}
				if (s.Length >= this._table.Length)
				{
					return false;
				}
				string[] array = this._table[s.Length];
				if (array == null)
				{
					return false;
				}
				for (int i = 0; i < array.Length; i++)
				{
					if (s == array[i])
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x040004F7 RID: 1271
			private readonly string[][] _table;
		}
	}
}
