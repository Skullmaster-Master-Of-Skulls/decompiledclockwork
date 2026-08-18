using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

namespace AjaxControlToolkit
{
	// Token: 0x020001A8 RID: 424
	public class Localization
	{
		// Token: 0x06000C4C RID: 3148 RVA: 0x000200BA File Offset: 0x0001E2BA
		static Localization()
		{
			Localization.PopulateKnownLocales();
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x000200DF File Offset: 0x0001E2DF
		public virtual ICollection<string> BuiltinLocales
		{
			get
			{
				return Localization._builtinLocales;
			}
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x000200E8 File Offset: 0x0001E2E8
		public static void AddLocale(string localeKey, string scriptName, Assembly scriptAssembly)
		{
			lock (Localization._locker)
			{
				if (Localization._customLocales == null)
				{
					Localization._customLocales = new Dictionary<string, Localization.LocaleScriptInfo>();
				}
				Localization._customLocales[localeKey] = new Localization.LocaleScriptInfo(localeKey, scriptName, scriptAssembly);
			}
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00020148 File Offset: 0x0001E348
		public static void AddExternalLocale(string localeKey, Func<string, ScriptReference> scriptReferenceProvider)
		{
			lock (Localization._locker)
			{
				if (Localization._externalLocales == null)
				{
					Localization._externalLocales = new Dictionary<string, Func<string, ScriptReference>>();
				}
				Localization._externalLocales[localeKey] = scriptReferenceProvider;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x000201A0 File Offset: 0x0001E3A0
		private static Assembly ToolkitAssembly
		{
			get
			{
				return typeof(Localization).Assembly;
			}
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x000201B4 File Offset: 0x0001E3B4
		private static void PopulateKnownLocales()
		{
			lock (Localization._locker)
			{
				if (Localization._builtinLocales == null)
				{
					Localization._builtinLocales = new HashSet<string>();
					foreach (string input in Localization.ToolkitAssembly.GetManifestResourceNames())
					{
						string pattern = "^" + Regex.Escape("Localization.Resources") + "\\.(?<key>[\\w-]+)\\.debug\\.js";
						Match match = Regex.Match(input, pattern);
						if (match.Success)
						{
							Localization._builtinLocales.Add(match.Groups["key"].Value);
						}
					}
				}
			}
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x000205B0 File Offset: 0x0001E7B0
		public IEnumerable<ScriptReference> GetLocalizationScriptReferences()
		{
			string localeKey = this.GetLocaleKey();
			IEnumerable<Localization.LocaleScriptInfo> localeScriptInfos = from i in this.GetAllLocaleScriptInfo()
			where i.LocaleKey == "" || i.LocaleKey == localeKey
			select i;
			IEnumerable<ScriptReference> scriptReferences = null;
			if (Localization._externalLocales.ContainsKey(localeKey))
			{
				ScriptReference item = Localization._externalLocales[localeKey](localeKey);
				scriptReferences = (from i in localeScriptInfos
				where i.LocaleKey == ""
				select this.CreateScriptReference(i.LocaleKey, i.ScriptAsssembly)).Concat(new List<ScriptReference>
				{
					item
				});
			}
			else
			{
				scriptReferences = from i in localeScriptInfos
				select this.CreateScriptReference(i.LocaleKey, i.ScriptAsssembly);
			}
			foreach (ScriptReference reference in scriptReferences)
			{
				yield return reference;
			}
			yield break;
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x000207A4 File Offset: 0x0001E9A4
		public IEnumerable<EmbeddedScript> GetAllLocalizationEmbeddedScripts()
		{
			IEnumerable<EmbeddedScript> scriptInfos = from i in this.GetAllLocaleScriptInfo()
			select new EmbeddedScript(i.ScriptName, i.ScriptAsssembly);
			foreach (EmbeddedScript info in scriptInfos)
			{
				yield return info;
			}
			yield break;
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00020AB4 File Offset: 0x0001ECB4
		private IEnumerable<Localization.LocaleScriptInfo> GetAllLocaleScriptInfo()
		{
			yield return new Localization.LocaleScriptInfo("", "Localization.Resources", Localization.ToolkitAssembly);
			HashSet<string> returnedLocales = new HashSet<string>();
			foreach (string localeKey in Localization._customLocales.Keys)
			{
				returnedLocales.Add(localeKey);
				yield return new Localization.LocaleScriptInfo(localeKey, Localization.GetCustomScriptName(localeKey), Localization._customLocales[localeKey].ScriptAsssembly);
			}
			foreach (string localeKey2 in this.BuiltinLocales)
			{
				if (!returnedLocales.Contains(localeKey2))
				{
					yield return new Localization.LocaleScriptInfo(localeKey2, Localization.FormatScriptName(localeKey2), Localization.ToolkitAssembly);
				}
			}
			yield break;
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00020AD1 File Offset: 0x0001ECD1
		public string GetLocaleKey()
		{
			if (!this.IsLocalizationEnabled())
			{
				return "";
			}
			return this.DetermineLocale();
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00020AE8 File Offset: 0x0001ECE8
		public virtual bool IsLocalizationEnabled()
		{
			Page page = HttpContext.Current.Handler as Page;
			if (page == null)
			{
				return true;
			}
			ScriptManager current = ScriptManager.GetCurrent(page);
			return current == null || current.EnableScriptLocalization;
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x00020B1C File Offset: 0x0001ED1C
		private ScriptReference CreateScriptReference(string localeKey, Assembly scriptAssembly)
		{
			if (Localization.ToolkitAssembly == scriptAssembly)
			{
				return new ScriptReference(Localization.FormatScriptName(localeKey) + ".js", Localization.ToolkitAssembly.FullName);
			}
			return new ScriptReference(Localization.GetCustomScriptName(localeKey) + this.GetScriptSuffix(), scriptAssembly.FullName);
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x00020B72 File Offset: 0x0001ED72
		private string GetScriptSuffix()
		{
			if (this.IsDebuggingEnabled())
			{
				return ".js";
			}
			return ".min.js";
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x00020B87 File Offset: 0x0001ED87
		public virtual bool IsDebuggingEnabled()
		{
			return HttpContext.Current.IsDebuggingEnabled;
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00020B93 File Offset: 0x0001ED93
		private static string GetCustomScriptName(string localeKey)
		{
			return Localization._customLocales[localeKey].ScriptName;
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x00020BA5 File Offset: 0x0001EDA5
		private static string FormatScriptName(string localeKey)
		{
			if (string.IsNullOrEmpty(localeKey))
			{
				return "Localization.Resources";
			}
			return "Localization.Resources." + localeKey;
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x00020BC0 File Offset: 0x0001EDC0
		private string DetermineLocale()
		{
			string name = CultureInfo.CurrentUICulture.Name;
			string result;
			if ((result = this.GetLocale(name)) == null)
			{
				result = (this.GetLocale(this.GetLanguage(name)) ?? string.Empty);
			}
			return result;
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00020BF9 File Offset: 0x0001EDF9
		private string GetLocale(string culture)
		{
			if (!this.BuiltinLocales.Concat(Localization._customLocales.Keys).Concat(Localization._externalLocales.Keys).Contains(culture))
			{
				return null;
			}
			return culture;
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x00020C2C File Offset: 0x0001EE2C
		private string GetLanguage(string cultureName)
		{
			return cultureName.Split(new char[]
			{
				'-'
			})[0];
		}

		// Token: 0x04000482 RID: 1154
		private static readonly object _locker = new object();

		// Token: 0x04000483 RID: 1155
		private static ICollection<string> _builtinLocales;

		// Token: 0x04000484 RID: 1156
		private static IDictionary<string, Localization.LocaleScriptInfo> _customLocales = new Dictionary<string, Localization.LocaleScriptInfo>();

		// Token: 0x04000485 RID: 1157
		private static IDictionary<string, Func<string, ScriptReference>> _externalLocales = new Dictionary<string, Func<string, ScriptReference>>();

		// Token: 0x020001A9 RID: 425
		private class LocaleScriptInfo
		{
			// Token: 0x17000495 RID: 1173
			// (get) Token: 0x06000C64 RID: 3172 RVA: 0x00020C56 File Offset: 0x0001EE56
			// (set) Token: 0x06000C65 RID: 3173 RVA: 0x00020C5E File Offset: 0x0001EE5E
			public string LocaleKey { get; private set; }

			// Token: 0x17000496 RID: 1174
			// (get) Token: 0x06000C66 RID: 3174 RVA: 0x00020C67 File Offset: 0x0001EE67
			// (set) Token: 0x06000C67 RID: 3175 RVA: 0x00020C6F File Offset: 0x0001EE6F
			public string ScriptName { get; private set; }

			// Token: 0x17000497 RID: 1175
			// (get) Token: 0x06000C68 RID: 3176 RVA: 0x00020C78 File Offset: 0x0001EE78
			// (set) Token: 0x06000C69 RID: 3177 RVA: 0x00020C80 File Offset: 0x0001EE80
			public Assembly ScriptAsssembly { get; private set; }

			// Token: 0x06000C6A RID: 3178 RVA: 0x00020C89 File Offset: 0x0001EE89
			public LocaleScriptInfo(string localeKey, string scriptName, Assembly scriptAssembly)
			{
				this.LocaleKey = localeKey;
				this.ScriptName = scriptName;
				this.ScriptAsssembly = scriptAssembly;
			}
		}
	}
}
