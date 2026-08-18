using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Xml;

namespace Telerik.Web
{
	// Token: 0x02001022 RID: 4130
	internal class LocalizationProvider
	{
		// Token: 0x1700337D RID: 13181
		// (get) Token: 0x0600A30F RID: 41743 RVA: 0x00244ACE File Offset: 0x00242CCE
		public string ClassKey
		{
			get
			{
				return this._classKey;
			}
		}

		// Token: 0x0600A310 RID: 41744 RVA: 0x00244AD8 File Offset: 0x00242CD8
		public LocalizationProvider(string classKey, ILocalizableControl control, string resFileLocation)
		{
			if (!string.IsNullOrEmpty(resFileLocation))
			{
				this._locator = new LocalizationProvider.XmlResourceLocator(classKey, control, resFileLocation);
				this._classKey = classKey;
			}
			else if (LocalizationProvider.GlobalResourceFileExists(classKey, control.Culture))
			{
				this._locator = new LocalizationProvider.GlobalResourceLocator(classKey, control);
			}
			else
			{
				this._locator = new LocalizationProvider.EmbeddedResourceLocator(classKey, control);
			}
			this._classKey = classKey;
		}

		// Token: 0x0600A311 RID: 41745 RVA: 0x00244B3B File Offset: 0x00242D3B
		public LocalizationProvider(string classKey, ILocalizableControl control) : this(classKey, control, null)
		{
		}

		// Token: 0x0600A312 RID: 41746 RVA: 0x00244B46 File Offset: 0x00242D46
		public string GetString(string resourceKey)
		{
			return this._locator.GetString(resourceKey);
		}

		// Token: 0x0600A313 RID: 41747 RVA: 0x00244B54 File Offset: 0x00242D54
		private static bool GlobalResourceFileExists(string classKey, CultureInfo culture)
		{
			string str = (culture == null) ? CultureInfo.CurrentUICulture.Name : culture.Name;
			string text = LocalizationProvider._verifiedGlobalResources[classKey + str] as string;
			string a;
			if ((a = text) != null)
			{
				if (a == "yes")
				{
					return true;
				}
				if (a == "no")
				{
					return false;
				}
			}
			bool flag = false;
			try
			{
				flag = (LocalizationProvider.GlobalResourceLocator.GetString(classKey, "ReservedResource", culture) != null);
			}
			catch
			{
			}
			LocalizationProvider._verifiedGlobalResources[classKey + str] = (flag ? "yes" : "no");
			return flag;
		}

		// Token: 0x04002D50 RID: 11600
		private static readonly Hashtable _verifiedGlobalResources = new Hashtable();

		// Token: 0x04002D51 RID: 11601
		private readonly LocalizationProvider.IResourceLocator _locator;

		// Token: 0x04002D52 RID: 11602
		private readonly string _classKey;

		// Token: 0x02001023 RID: 4131
		private interface IResourceLocator
		{
			// Token: 0x0600A315 RID: 41749
			string GetString(string resourceKey);

			// Token: 0x0600A316 RID: 41750
			string GetString(string resourceKey, CultureInfo culture);
		}

		// Token: 0x02001024 RID: 4132
		private class EmbeddedResourceLocator : LocalizationProvider.IResourceLocator
		{
			// Token: 0x1700337E RID: 13182
			// (get) Token: 0x0600A317 RID: 41751 RVA: 0x00244C0C File Offset: 0x00242E0C
			private static IDictionary<string, ResourceManager> ResourceManagerCache
			{
				get
				{
					if (LocalizationProvider.EmbeddedResourceLocator._cache == null)
					{
						LocalizationProvider.EmbeddedResourceLocator._cache = new Dictionary<string, ResourceManager>();
					}
					return LocalizationProvider.EmbeddedResourceLocator._cache;
				}
			}

			// Token: 0x0600A318 RID: 41752 RVA: 0x00244C24 File Offset: 0x00242E24
			public EmbeddedResourceLocator(string classKey, ILocalizableControl control)
			{
				string text = "Telerik.Web.UI.Resources." + classKey;
				if (!LocalizationProvider.EmbeddedResourceLocator.ResourceManagerCache.ContainsKey(text))
				{
					this._rm = new ResourceManager(text, Assembly.GetExecutingAssembly());
					this._rm.IgnoreCase = true;
					LocalizationProvider.EmbeddedResourceLocator.ResourceManagerCache.Add(text, this._rm);
				}
				else
				{
					this._rm = LocalizationProvider.EmbeddedResourceLocator.ResourceManagerCache[text];
				}
				this._control = control;
			}

			// Token: 0x0600A319 RID: 41753 RVA: 0x00244C98 File Offset: 0x00242E98
			public string GetString(string resourceKey)
			{
				return this.GetString(resourceKey, this._control.Culture);
			}

			// Token: 0x0600A31A RID: 41754 RVA: 0x00244CAC File Offset: 0x00242EAC
			public string GetString(string resourceKey, CultureInfo culture)
			{
				return this._rm.GetString(resourceKey, culture);
			}

			// Token: 0x04002D53 RID: 11603
			[ThreadStatic]
			private static IDictionary<string, ResourceManager> _cache;

			// Token: 0x04002D54 RID: 11604
			private readonly ResourceManager _rm;

			// Token: 0x04002D55 RID: 11605
			private readonly ILocalizableControl _control;
		}

		// Token: 0x02001025 RID: 4133
		private class GlobalResourceLocator : LocalizationProvider.IResourceLocator
		{
			// Token: 0x0600A31B RID: 41755 RVA: 0x00244CBB File Offset: 0x00242EBB
			public GlobalResourceLocator(string classKey, ILocalizableControl control)
			{
				this._classKey = classKey;
				this._control = control;
			}

			// Token: 0x0600A31C RID: 41756 RVA: 0x00244CD1 File Offset: 0x00242ED1
			public string GetString(string resourceKey)
			{
				return this.GetString(resourceKey, this._control.Culture);
			}

			// Token: 0x0600A31D RID: 41757 RVA: 0x00244CE5 File Offset: 0x00242EE5
			public string GetString(string resourceKey, CultureInfo culture)
			{
				return LocalizationProvider.GlobalResourceLocator.GetString(this._classKey, resourceKey, culture);
			}

			// Token: 0x0600A31E RID: 41758 RVA: 0x00244CF4 File Offset: 0x00242EF4
			public static string GetString(string classKey, string resourceKey, CultureInfo culture)
			{
				return (string)HttpContext.GetGlobalResourceObject(classKey, resourceKey, culture);
			}

			// Token: 0x04002D56 RID: 11606
			private readonly string _classKey;

			// Token: 0x04002D57 RID: 11607
			private readonly ILocalizableControl _control;
		}

		// Token: 0x02001026 RID: 4134
		private class XmlResourceLocator : LocalizationProvider.IResourceLocator
		{
			// Token: 0x0600A31F RID: 41759 RVA: 0x00244D04 File Offset: 0x00242F04
			private void useXmlReader(string fileName, string cultureName)
			{
				using (XmlReader xmlReader = new XmlTextReader(fileName))
				{
					while (xmlReader.Read())
					{
						if (xmlReader.Name == "data" && xmlReader.HasAttributes)
						{
							string str = xmlReader["name"];
							xmlReader.Read();
							xmlReader.Read();
							if (!xmlReader.IsEmptyElement)
							{
								xmlReader.Read();
								this._ht.Add(str + cultureName, xmlReader.Value);
							}
							else
							{
								this._ht.Add(str + cultureName, string.Empty);
							}
							xmlReader.Read();
							xmlReader.Read();
							xmlReader.Read();
						}
					}
				}
			}

			// Token: 0x0600A320 RID: 41760 RVA: 0x00244DD0 File Offset: 0x00242FD0
			private void LoadResources(CultureInfo culture)
			{
				string text = (culture == null) ? string.Empty : ("." + culture.ToString());
				if (this._ht.Contains("ReservedResource" + text))
				{
					return;
				}
				string text2 = HttpContext.Current.Server.MapPath(this._resxBasePath + text + ".resx");
				if (!File.Exists(text2) && culture != null)
				{
					CultureInfo cultureInfo = culture;
					while (cultureInfo != null && cultureInfo != CultureInfo.InvariantCulture)
					{
						cultureInfo = cultureInfo.Parent;
						string str = (cultureInfo != null && !string.IsNullOrEmpty(cultureInfo.Name)) ? ("." + cultureInfo.Name) : string.Empty;
						text2 = HttpContext.Current.Server.MapPath(this._resxBasePath + str + ".resx");
						if (File.Exists(text2))
						{
							break;
						}
					}
				}
				if (!File.Exists(text2))
				{
					return;
				}
				this.useXmlReader(text2, text);
			}

			// Token: 0x0600A321 RID: 41761 RVA: 0x00244EB8 File Offset: 0x002430B8
			public XmlResourceLocator(string classKey, ILocalizableControl control, string xmlFileLocation)
			{
				this._resxBasePath = VirtualPathUtility.AppendTrailingSlash(xmlFileLocation) + classKey;
				this._culture = control.Culture;
				if (this._culture != null && (this._culture.Equals(new CultureInfo("en-US")) || this._culture.Equals(CultureInfo.InvariantCulture)))
				{
					this._culture = null;
				}
				this.LoadResources(this._culture);
			}

			// Token: 0x0600A322 RID: 41762 RVA: 0x00244F38 File Offset: 0x00243138
			public string GetString(string resourceKey)
			{
				return this.GetString(resourceKey, this._culture);
			}

			// Token: 0x0600A323 RID: 41763 RVA: 0x00244F48 File Offset: 0x00243148
			public string GetString(string resourceKey, CultureInfo culture)
			{
				string str = (culture == null) ? string.Empty : ("." + culture.ToString());
				object obj = this._ht[resourceKey + str];
				if (obj == null)
				{
					this.LoadResources(culture);
					obj = this._ht[resourceKey + str];
				}
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}

			// Token: 0x04002D58 RID: 11608
			private Hashtable _ht = new Hashtable();

			// Token: 0x04002D59 RID: 11609
			private string _resxBasePath;

			// Token: 0x04002D5A RID: 11610
			private CultureInfo _culture;
		}
	}
}
