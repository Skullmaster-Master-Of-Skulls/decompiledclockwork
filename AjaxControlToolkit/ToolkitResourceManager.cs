using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit.Bundling;

namespace AjaxControlToolkit
{
	// Token: 0x0200019C RID: 412
	public static class ToolkitResourceManager
	{
		// Token: 0x06000BDA RID: 3034 RVA: 0x0001EC20 File Offset: 0x0001CE20
		public static void RegisterControl(Type type)
		{
			ControlDependencyMap.Maps[type.FullName] = ControlDependencyMap.BuildDependencyMap(type);
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0001EC38 File Offset: 0x0001CE38
		// (set) Token: 0x06000BDC RID: 3036 RVA: 0x0001EC49 File Offset: 0x0001CE49
		public static bool RenderStyleLinks
		{
			get
			{
				return ToolkitResourceManager.GetContextFlag("3ca56b9cc998439ca4894b076783cfc9", ToolkitConfig.RenderStyleLinks);
			}
			set
			{
				ToolkitResourceManager.SetContextFlag("3ca56b9cc998439ca4894b076783cfc9", ToolkitConfig.RenderStyleLinks, value);
			}
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0001EC72 File Offset: 0x0001CE72
		public static string[] GetScriptPaths(params string[] toolkitBundles)
		{
			return (from script in ToolkitResourceManager.GetEmbeddedScripts(toolkitBundles)
			select ToolkitResourceManager.FormatScriptReleaseVirtualPath(script.Name)).ToArray<string>();
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0001ECDC File Offset: 0x0001CEDC
		internal static IEnumerable<ScriptReference> GetControlScriptReferences(Type type)
		{
			return new Localization().GetLocalizationScriptReferences().Concat(from entry in ToolkitResourceManager.GetScriptEntries(type)
			select new ScriptReference
			{
				Assembly = entry.AssemblyName,
				Name = entry.ResourceName + ".js"
			});
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0001ED20 File Offset: 0x0001CF20
		private static IEnumerable<EmbeddedScript> GetEmbeddedScripts(params string[] toolkitBundles)
		{
			List<EmbeddedScript> list = new List<EmbeddedScript>();
			Assembly assembly = typeof(ToolkitResourceManager).Assembly;
			list.AddRange(new Localization().GetAllLocalizationEmbeddedScripts());
			HashSet<string> hashSet = new HashSet<string>();
			BundleResolver bundleResolver = new BundleResolver(new DefaultCache());
			foreach (Type type in bundleResolver.GetControlTypesInBundles(toolkitBundles, ToolkitResourceManager.GetConfigPath()))
			{
				foreach (string text in from entry in ToolkitResourceManager.GetScriptEntries(type)
				select entry.ResourceName)
				{
					if (!hashSet.Contains(text))
					{
						list.Add(new EmbeddedScript(text, type.Assembly));
						hashSet.Add(text);
					}
				}
			}
			return list;
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0001EE34 File Offset: 0x0001D034
		private static string GetConfigPath()
		{
			if (string.IsNullOrEmpty(HttpRuntime.AppDomainAppVirtualPath))
			{
				return null;
			}
			return Path.Combine(HttpRuntime.AppDomainAppPath, "AjaxControlToolkit.config");
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0001EE54 File Offset: 0x0001D054
		public static void RegisterScriptMappings(string bundleName = null)
		{
			IEnumerable<EmbeddedScript> embeddedScripts;
			if (!string.IsNullOrWhiteSpace(bundleName))
			{
				embeddedScripts = ToolkitResourceManager.GetEmbeddedScripts(new string[]
				{
					bundleName
				});
			}
			else
			{
				embeddedScripts = ToolkitResourceManager.GetEmbeddedScripts(new string[0]);
			}
			foreach (EmbeddedScript embeddedScript in embeddedScripts)
			{
				ScriptManager.ScriptResourceMapping.AddDefinition(embeddedScript.Name + ".js", embeddedScript.SourceAssembly, new ScriptResourceDefinition
				{
					Path = ToolkitResourceManager.FormatScriptReleaseVirtualPath(embeddedScript.Name),
					DebugPath = ToolkitResourceManager.FormatScriptDebugVirtualPath(embeddedScript.Name),
					CdnPath = "//ajax.aspnetcdn.com/ajax/act/16_1_1/Scripts/AjaxControlToolkit/Release/" + embeddedScript.Name + ".js",
					CdnDebugPath = "//ajax.aspnetcdn.com/ajax/act/16_1_1/Scripts/AjaxControlToolkit/Debug/" + embeddedScript.Name + ".debug.js",
					CdnSupportsSecureConnection = true
				});
			}
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0001EF50 File Offset: 0x0001D150
		public static void RemoveScriptMappingsRegistration()
		{
			Assembly assembly = typeof(ToolkitResourceManager).Assembly;
			foreach (EmbeddedScript embeddedScript in ToolkitResourceManager.GetEmbeddedScripts(new string[0]))
			{
				ScriptManager.ScriptResourceMapping.RemoveDefinition(embeddedScript.Name + ".js", embeddedScript.SourceAssembly);
			}
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0001EFCC File Offset: 0x0001D1CC
		private static string FormatScriptDebugVirtualPath(string scriptName)
		{
			return "~/Scripts/AjaxControlToolkit/Debug/" + scriptName + ".debug.js";
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0001EFDE File Offset: 0x0001D1DE
		private static string FormatScriptReleaseVirtualPath(string scriptName)
		{
			return "~/Scripts/AjaxControlToolkit/Release/" + scriptName + ".js";
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0001F000 File Offset: 0x0001D200
		public static string[] GetStylePaths(params string[] toolkitBundles)
		{
			List<Type> controlTypesInBundles = new BundleResolver(new DefaultCache()).GetControlTypesInBundles(toolkitBundles, ToolkitResourceManager.GetConfigPath());
			return (from entry in ToolkitResourceManager.GetStyleEntries(controlTypesInBundles.ToArray()).Distinct<ToolkitResourceManager.ResourceEntry>()
			select ToolkitResourceManager.FormatStyleVirtualPath(entry.ResourceName, false)).ToArray<string>();
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0001F08C File Offset: 0x0001D28C
		internal static IEnumerable<string> GetStyleHrefs(System.Web.UI.Control control)
		{
			return from name in ToolkitResourceManager.GetStyleEntries(new Type[]
			{
				control.GetType()
			})
			select ToolkitResourceManager.GetStyleHref(name, control, new Func<Type, string, string>(control.Page.ClientScript.GetWebResourceUrl));
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0001F0D2 File Offset: 0x0001D2D2
		internal static string GetStyleHref(string entryName, System.Web.UI.Control control)
		{
			return ToolkitResourceManager.GetStyleHref(new ToolkitResourceManager.ResourceEntry(entryName, control.GetType(), 0), control);
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0001F0E7 File Offset: 0x0001D2E7
		internal static string GetStyleHref(ToolkitResourceManager.ResourceEntry entry, System.Web.UI.Control control)
		{
			return ToolkitResourceManager.GetStyleHref(entry, control, new Func<Type, string, string>(control.Page.ClientScript.GetWebResourceUrl));
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0001F108 File Offset: 0x0001D308
		public static string GetStyleHref(ToolkitResourceManager.ResourceEntry entry, System.Web.UI.Control control, Func<Type, string, string> getWebResourceUrlFunc)
		{
			bool minified = !ToolkitResourceManager.IsDebuggingEnabled();
			Type type = control.GetType();
			if (type.Assembly != entry.Assembly)
			{
				type = type.BaseType;
			}
			if (ToolkitResourceManager.IsCdnEnabled())
			{
				string newValue = (ToolkitResourceManager.IsSecureConnection() ? "https:" : "http:") + "//ajax.aspnetcdn.com/ajax/act/16_1_1/";
				return ToolkitResourceManager.FormatStyleVirtualPath(entry.ResourceName, minified).Replace("~/", newValue);
			}
			if (!ToolkitConfig.UseStaticResources)
			{
				return getWebResourceUrlFunc(type, ToolkitResourceManager.GetStyleResourceName(entry, minified));
			}
			return ToolkitResourceManager.FormatStyleVirtualPath(entry.ResourceName, minified);
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0001F1A4 File Offset: 0x0001D3A4
		private static string GetStyleResourceName(ToolkitResourceManager.ResourceEntry entry, bool minified)
		{
			bool useActPrefix = entry.Assembly == typeof(ToolkitResourceManager).Assembly;
			return ToolkitResourceManager.FormatStyleResourceName(entry.ResourceName, minified, useActPrefix);
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0001F440 File Offset: 0x0001D640
		internal static IEnumerable<ToolkitResourceManager.ResourceEntry> GetStyleEntries(params Type[] controlTypes)
		{
			foreach (Type type in controlTypes)
			{
				foreach (ToolkitResourceManager.ResourceEntry entry in ToolkitResourceManager.GetStyleEntries(type))
				{
					yield return entry;
				}
			}
			yield return new ToolkitResourceManager.ResourceEntry("Backgrounds", typeof(ExtenderControlBase), 0);
			yield break;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0001F45D File Offset: 0x0001D65D
		private static string FormatStyleVirtualPath(string name, bool minified)
		{
			return "~/Content/AjaxControlToolkit/Styles/" + name + (minified ? ".min.css" : ".css");
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0001F479 File Offset: 0x0001D679
		private static string FormatStyleResourceName(string name, bool minified, bool useActPrefix)
		{
			return (useActPrefix ? "AjaxControlToolkit.Styles." : "") + name + (minified ? ".min.css" : ".css");
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0001F4A0 File Offset: 0x0001D6A0
		public static void RegisterCssReferences(System.Web.UI.Control control)
		{
			if (!ToolkitResourceManager.RenderStyleLinks)
			{
				return;
			}
			HtmlHead header = control.Page.Header;
			foreach (string text in ToolkitResourceManager.GetStyleHrefs(control))
			{
				if (header == null)
				{
					throw new NotSupportedException("This page is missing a HtmlHead control which is required for the CSS stylesheet link that is being added. Please add <head runat=\"server\" />.");
				}
				bool flag = false;
				foreach (object obj in header.Controls)
				{
					HtmlLink htmlLink = obj as HtmlLink;
					if (htmlLink != null && text.Equals(htmlLink.Href, StringComparison.OrdinalIgnoreCase))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					HtmlLink htmlLink2 = new HtmlLink();
					htmlLink2.Href = text;
					htmlLink2.Attributes.Add("type", "text/css");
					htmlLink2.Attributes.Add("rel", "stylesheet");
					header.Controls.Add(htmlLink2);
					ScriptManager current = ScriptManager.GetCurrent(control.Page);
					if (current == null)
					{
						throw new InvalidOperationException("A ScriptManager is required on the page to use ASP.NET AJAX Script Components.");
					}
					if (current.IsInAsyncPostBack)
					{
						string text2 = htmlLink2.ResolveClientUrl(text);
						ScriptManager.RegisterClientScriptBlock(control, control.GetType(), "RegisterCssReferences", string.Concat(new string[]
						{
							"if (window.__ExtendedControlCssLoaded == null || typeof window.__ExtendedControlCssLoaded == 'undefined') {    window.__ExtendedControlCssLoaded = new Array();}var controlCssLoaded = window.__ExtendedControlCssLoaded; var head = document.getElementsByTagName('HEAD')[0];if (head && !Array.contains(controlCssLoaded,'",
							text2,
							"')) {var linkElement = document.createElement('link');linkElement.type = 'text/css';linkElement.rel = 'stylesheet';linkElement.href = '",
							text2,
							"';head.appendChild(linkElement);controlCssLoaded.push('",
							text2,
							"');}"
						}), true);
					}
				}
			}
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0001F664 File Offset: 0x0001D864
		internal static string GetImageHref(string imageName, System.Web.UI.Control control, bool resolveClientUrlForStatic = true)
		{
			if (ToolkitResourceManager.IsCdnEnabled())
			{
				return ("~/Content/AjaxControlToolkit/Images/" + imageName).Replace("~/", "//ajax.aspnetcdn.com/ajax/act/16_1_1/");
			}
			if (!ToolkitConfig.UseStaticResources)
			{
				return control.Page.ClientScript.GetWebResourceUrl(control.GetType(), "AjaxControlToolkit.Images." + imageName);
			}
			if (resolveClientUrlForStatic)
			{
				return control.Page.ResolveClientUrl("~/Content/AjaxControlToolkit/Images/" + imageName);
			}
			return "~/Content/AjaxControlToolkit/Images/" + imageName;
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0001F6E4 File Offset: 0x0001D8E4
		internal static void RegisterImagePaths(string[] imageNames, System.Web.UI.Control control)
		{
			if (imageNames.Length < 1)
			{
				return;
			}
			control.Page.ClientScript.RegisterStartupScript(control.Page.GetType(), "bb9d9f1593ff41a198714a472d603c55", "Type.registerNamespace('Sys.Extended.UI.Images');", true);
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in imageNames)
			{
				stringBuilder.AppendLine(string.Concat(new string[]
				{
					"Sys.Extended.UI.Images[",
					javaScriptSerializer.Serialize(text),
					"] = ",
					javaScriptSerializer.Serialize(ToolkitResourceManager.GetImageHref(text, control, true)),
					";"
				}));
			}
			control.Page.ClientScript.RegisterStartupScript(control.GetType(), "086a0778a11d433386793f72ea881602", stringBuilder.ToString(), true);
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0001F7B3 File Offset: 0x0001D9B3
		private static IEnumerable<ToolkitResourceManager.ResourceEntry> GetScriptEntries(Type type)
		{
			return ToolkitResourceManager.GetResourceEntries<ClientScriptResourceAttribute>(type, new HashSet<Type>(), ToolkitResourceManager._scriptsCache);
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0001F7C5 File Offset: 0x0001D9C5
		private static IEnumerable<ToolkitResourceManager.ResourceEntry> GetStyleEntries(Type type)
		{
			return ToolkitResourceManager.GetResourceEntries<ClientCssResourceAttribute>(type, new HashSet<Type>(), ToolkitResourceManager._cssCache);
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0001F850 File Offset: 0x0001DA50
		private static IEnumerable<ToolkitResourceManager.ResourceEntry> GetResourceEntries<AttributeType>(Type type, ICollection<Type> typeTrace, IDictionary<Type, List<ToolkitResourceManager.ResourceEntry>> cache) where AttributeType : ClientResourceAttribute
		{
			Func<RequiredScriptAttribute, bool> func = null;
			Func<RequiredScriptAttribute, int> func2 = null;
			Func<ToolkitResourceManager.ResourceEntry, int> func3 = null;
			if (typeTrace.Contains(type))
			{
				throw new InvalidOperationException("Circular reference detected.");
			}
			if (cache.ContainsKey(type))
			{
				return cache[type];
			}
			typeTrace.Add(type);
			IEnumerable<ToolkitResourceManager.ResourceEntry> result;
			try
			{
				bool flag = false;
				try
				{
					object sync;
					Monitor.Enter(sync = ToolkitResourceManager._sync, ref flag);
					if (cache.ContainsKey(type))
					{
						result = cache[type];
					}
					else
					{
						IEnumerable<RequiredScriptAttribute> source = type.GetCustomAttributes(typeof(RequiredScriptAttribute), true).Cast<RequiredScriptAttribute>();
						if (func == null)
						{
							func = ((RequiredScriptAttribute a) => a.ExtenderType != null);
						}
						IEnumerable<RequiredScriptAttribute> source2 = source.Where(func);
						if (func2 == null)
						{
							func2 = ((RequiredScriptAttribute a) => a.LoadOrder);
						}
						IEnumerable<ToolkitResourceManager.ResourceEntry> enumerable = source2.OrderBy(func2).SelectMany((RequiredScriptAttribute a) => ToolkitResourceManager.GetResourceEntries<AttributeType>(a.ExtenderType, typeTrace, cache));
						IEnumerable<ToolkitResourceManager.ResourceEntry> enumerable2 = Enumerable.Empty<ToolkitResourceManager.ResourceEntry>();
						Type current = type;
						int orderOffset = 0;
						while (current != typeof(object))
						{
							object[] customAttributes = current.GetCustomAttributes(typeof(AttributeType), false);
							orderOffset -= customAttributes.Length;
							enumerable2 = enumerable2.Concat((from AttributeType a in customAttributes
							select new ToolkitResourceManager.ResourceEntry(a.ResourcePath, current, orderOffset + a.LoadOrder)).ToList<ToolkitResourceManager.ResourceEntry>());
							current = current.BaseType;
						}
						IEnumerable<ToolkitResourceManager.ResourceEntry> first = enumerable;
						IEnumerable<ToolkitResourceManager.ResourceEntry> source3 = enumerable2.Distinct<ToolkitResourceManager.ResourceEntry>();
						if (func3 == null)
						{
							func3 = ((ToolkitResourceManager.ResourceEntry e) => e.Order);
						}
						List<ToolkitResourceManager.ResourceEntry> list = first.Concat(source3.OrderBy(func3)).ToList<ToolkitResourceManager.ResourceEntry>();
						cache.Add(type, list);
						result = list;
					}
				}
				finally
				{
					if (flag)
					{
						object sync;
						Monitor.Exit(sync);
					}
				}
			}
			finally
			{
				typeTrace.Remove(type);
			}
			return result;
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0001FA98 File Offset: 0x0001DC98
		private static bool GetContextFlag(string key, bool defaultValue)
		{
			HttpContext httpContext = HttpContext.Current;
			if (httpContext == null || !httpContext.Items.Contains(key))
			{
				return defaultValue;
			}
			return (bool)httpContext.Items[key];
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0001FAD0 File Offset: 0x0001DCD0
		private static void SetContextFlag(string key, object defaultValue, object value)
		{
			HttpContext httpContext = HttpContext.Current;
			if (httpContext == null)
			{
				return;
			}
			if (defaultValue == value)
			{
				httpContext.Items.Remove(key);
				return;
			}
			httpContext.Items[key] = value;
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0001FB08 File Offset: 0x0001DD08
		private static bool IsDebuggingEnabled()
		{
			ScriptManager currentScriptManager = ToolkitResourceManager.GetCurrentScriptManager();
			if (currentScriptManager != null)
			{
				return currentScriptManager.IsDebuggingEnabled;
			}
			HttpContext httpContext = HttpContext.Current;
			return httpContext != null && httpContext.IsDebuggingEnabled;
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0001FB38 File Offset: 0x0001DD38
		private static bool IsCdnEnabled()
		{
			ScriptManager currentScriptManager = ToolkitResourceManager.GetCurrentScriptManager();
			return currentScriptManager != null && currentScriptManager.EnableCdn;
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0001FB58 File Offset: 0x0001DD58
		private static bool IsSecureConnection()
		{
			HttpContext httpContext = HttpContext.Current;
			return httpContext != null && httpContext.Request != null && httpContext.Request.IsSecureConnection;
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0001FB84 File Offset: 0x0001DD84
		private static ScriptManager GetCurrentScriptManager()
		{
			HttpContext httpContext = HttpContext.Current;
			if (httpContext == null)
			{
				return null;
			}
			Page page = httpContext.Handler as Page;
			if (page == null)
			{
				return null;
			}
			return ScriptManager.GetCurrent(page);
		}

		// Token: 0x0400045D RID: 1117
		private const string ContextKey_UseEmbeddedStyles = "3ca56b9cc998439ca4894b076783cfc9";

		// Token: 0x0400045E RID: 1118
		private static readonly object _sync = new object();

		// Token: 0x0400045F RID: 1119
		private static readonly Dictionary<Type, List<ToolkitResourceManager.ResourceEntry>> _scriptsCache = new Dictionary<Type, List<ToolkitResourceManager.ResourceEntry>>();

		// Token: 0x04000460 RID: 1120
		private static readonly Dictionary<Type, List<ToolkitResourceManager.ResourceEntry>> _cssCache = new Dictionary<Type, List<ToolkitResourceManager.ResourceEntry>>();

		// Token: 0x0200019D RID: 413
		public struct ResourceEntry : IEquatable<ToolkitResourceManager.ResourceEntry>
		{
			// Token: 0x06000C01 RID: 3073 RVA: 0x0001FBB3 File Offset: 0x0001DDB3
			public ResourceEntry(string name, Type componentType, int order)
			{
				if (string.IsNullOrEmpty(name))
				{
					throw new ArgumentException();
				}
				this.ResourceName = name;
				this.ComponentType = componentType;
				this.Order = order;
			}

			// Token: 0x17000476 RID: 1142
			// (get) Token: 0x06000C02 RID: 3074 RVA: 0x0001FBD8 File Offset: 0x0001DDD8
			public string AssemblyName
			{
				get
				{
					if (!(this.ComponentType == null))
					{
						return this.ComponentType.Assembly.FullName;
					}
					return "";
				}
			}

			// Token: 0x17000477 RID: 1143
			// (get) Token: 0x06000C03 RID: 3075 RVA: 0x0001FBFE File Offset: 0x0001DDFE
			public Assembly Assembly
			{
				get
				{
					if (!(this.ComponentType == null))
					{
						return this.ComponentType.Assembly;
					}
					return null;
				}
			}

			// Token: 0x06000C04 RID: 3076 RVA: 0x0001FC1B File Offset: 0x0001DE1B
			public override int GetHashCode()
			{
				return this.ResourceName.ToLower().GetHashCode() ^ this.ComponentType.GetHashCode();
			}

			// Token: 0x06000C05 RID: 3077 RVA: 0x0001FC39 File Offset: 0x0001DE39
			public override bool Equals(object obj)
			{
				return obj is ToolkitResourceManager.ResourceEntry && this.Equals((ToolkitResourceManager.ResourceEntry)obj);
			}

			// Token: 0x06000C06 RID: 3078 RVA: 0x0001FC51 File Offset: 0x0001DE51
			public bool Equals(ToolkitResourceManager.ResourceEntry other)
			{
				return this.ResourceName.Equals(other.ResourceName, StringComparison.OrdinalIgnoreCase) && this.ComponentType == other.ComponentType;
			}

			// Token: 0x04000465 RID: 1125
			public readonly string ResourceName;

			// Token: 0x04000466 RID: 1126
			public readonly Type ComponentType;

			// Token: 0x04000467 RID: 1127
			public readonly int Order;
		}
	}
}
