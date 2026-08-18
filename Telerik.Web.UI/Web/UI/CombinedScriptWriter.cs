using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Resources;
using System.Security;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x020019E0 RID: 6624
	public class CombinedScriptWriter
	{
		// Token: 0x06010054 RID: 65620 RVA: 0x00397AA7 File Offset: 0x00395CA7
		private CombinedScriptWriter(Page page, HttpContext context, ICryptoService service)
		{
			this._page = page;
			this._context = context;
			this._cryptoService = service;
		}

		// Token: 0x17004D5A RID: 19802
		// (get) Token: 0x06010055 RID: 65621 RVA: 0x00397AE1 File Offset: 0x00395CE1
		internal WhiteListLoader WhiteListLoader
		{
			get
			{
				if (CombinedScriptWriter.loader == null)
				{
					CombinedScriptWriter.loader = new WhiteListLoader();
				}
				return CombinedScriptWriter.loader;
			}
		}

		// Token: 0x17004D5B RID: 19803
		// (get) Token: 0x06010056 RID: 65622 RVA: 0x00397AF9 File Offset: 0x00395CF9
		internal ICryptoService EncryptionService
		{
			get
			{
				return this._cryptoService;
			}
		}

		// Token: 0x06010057 RID: 65623 RVA: 0x00397B01 File Offset: 0x00395D01
		public static void WriteCombinedScriptFile(Page page, HttpContext context)
		{
			new CombinedScriptWriter(page, context, CryptoService.GetService("")).WriteCombinedScriptFile();
		}

		// Token: 0x06010058 RID: 65624 RVA: 0x00397B1C File Offset: 0x00395D1C
		internal static void CompactInvalidScriptEntries(IList<ScriptEntry> scriptEntries)
		{
			int i = 0;
			int count = scriptEntries.Count;
			while (i < scriptEntries.Count)
			{
				if (scriptEntries[i] is InvalidScriptEntry)
				{
					scriptEntries.RemoveAt(i);
				}
				else
				{
					i++;
				}
			}
			if (count - scriptEntries.Count > 0)
			{
				scriptEntries.Add(new InvalidResourcesIndicatorScriptEntry(count - scriptEntries.Count));
			}
		}

		// Token: 0x06010059 RID: 65625 RVA: 0x00397B78 File Offset: 0x00395D78
		private static void RegisterNamespace(TextWriter builder, string typeName, bool isDebug)
		{
			int num = typeName.LastIndexOf('.');
			if (num != -1)
			{
				builder.Write("Type.registerNamespace('");
				builder.Write(typeName.Substring(0, num));
				builder.Write("');");
				if (isDebug)
				{
					builder.WriteLine();
				}
			}
		}

		// Token: 0x0601005A RID: 65626 RVA: 0x00397BC0 File Offset: 0x00395DC0
		private static string GetContentType(List<ScriptEntry> scriptEntries)
		{
			if (scriptEntries.Count == 0)
			{
				return "application/x-javascript";
			}
			ScriptEntry scriptEntry = scriptEntries[0];
			Assembly assembly = scriptEntries[0].LoadAssembly();
			if (assembly != null)
			{
				foreach (WebResourceAttribute webResourceAttribute in assembly.GetCustomAttributes(typeof(WebResourceAttribute), false))
				{
					string contentType;
					if (webResourceAttribute.WebResource == scriptEntry.Name && (contentType = webResourceAttribute.ContentType) != null)
					{
						string result;
						if (!(contentType == "text/css"))
						{
							if (!(contentType == "text/javascript") && !(contentType == "application/x-javascript"))
							{
								goto IL_AC;
							}
							result = "application/x-javascript";
						}
						else
						{
							result = "text/css";
						}
						return result;
					}
					IL_AC:;
				}
			}
			else
			{
				if (scriptEntry is ExternalScriptEntry)
				{
					return "application/x-javascript";
				}
				if (scriptEntry is ExternalStyleSheetEntry)
				{
					return "text/css";
				}
			}
			return string.Empty;
		}

		// Token: 0x17004D5C RID: 19804
		// (get) Token: 0x0601005B RID: 65627 RVA: 0x00397CAC File Offset: 0x00395EAC
		protected Regex WebResourceRegex
		{
			get
			{
				return this._webResourceRegex;
			}
		}

		// Token: 0x17004D5D RID: 19805
		// (get) Token: 0x0601005C RID: 65628 RVA: 0x00397CB4 File Offset: 0x00395EB4
		private Page Page
		{
			get
			{
				return this._page;
			}
		}

		// Token: 0x17004D5E RID: 19806
		// (get) Token: 0x0601005D RID: 65629 RVA: 0x00397CBC File Offset: 0x00395EBC
		private HttpContext Context
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x17004D5F RID: 19807
		// (get) Token: 0x0601005E RID: 65630 RVA: 0x00397CC4 File Offset: 0x00395EC4
		private HttpRequest Request
		{
			get
			{
				if (this._request == null)
				{
					this._request = this.Context.Request;
				}
				return this._request;
			}
		}

		// Token: 0x17004D60 RID: 19808
		// (get) Token: 0x0601005F RID: 65631 RVA: 0x00397CE5 File Offset: 0x00395EE5
		private HttpResponse Response
		{
			get
			{
				if (this._response == null)
				{
					this._response = this.Context.Response;
				}
				return this._response;
			}
		}

		// Token: 0x17004D61 RID: 19809
		// (get) Token: 0x06010060 RID: 65632 RVA: 0x00397D06 File Offset: 0x00395F06
		private WebResourceWriter WebResourceWriter
		{
			get
			{
				if (this._webResourceWriter == null)
				{
					this._webResourceWriter = new WebResourceWriter();
				}
				return this._webResourceWriter;
			}
		}

		// Token: 0x17004D62 RID: 19810
		// (get) Token: 0x06010061 RID: 65633 RVA: 0x00397D24 File Offset: 0x00395F24
		private bool UseStreams
		{
			get
			{
				string value = WebConfigurationManager.AppSettings["Telerik.WebResource.UseStreams"];
				return !string.IsNullOrEmpty(value) && bool.Parse(value);
			}
		}

		// Token: 0x06010062 RID: 65634 RVA: 0x00397D54 File Offset: 0x00395F54
		private void VerifyAssemblies(List<ScriptEntry> entries)
		{
			if (this.WhiteListLoader.WhiteListEnabled)
			{
				foreach (ScriptEntry entry in entries)
				{
					this.WhiteListLoader.VerifyEntry(entry);
				}
			}
		}

		// Token: 0x06010063 RID: 65635 RVA: 0x00397DB4 File Offset: 0x00395FB4
		private string SanitizeParams(string queryParams)
		{
			return queryParams.Replace(' ', '+').Trim(new char[]
			{
				';'
			});
		}

		// Token: 0x06010064 RID: 65636 RVA: 0x00397DE0 File Offset: 0x00395FE0
		private void WriteCombinedScriptFile()
		{
			string text = this.Request.Params["_TSM_HiddenField_"];
			string text2 = this.Request.Params["_TSM_CombinedScripts_"];
			if (string.IsNullOrEmpty(text2))
			{
				return;
			}
			if (ScriptManagerConfigurationSettings.GetConfiguration().EnableHandlerEncryption)
			{
				text2 = this.EncryptionService.DecryptWithMachineKey(this.SanitizeParams(text2));
			}
			List<ScriptEntry> list = ScriptEntry.Deserialize(HttpUtility.UrlDecode(text2));
			this.VerifyAssemblies(list);
			CombinedScriptWriter.CompactInvalidScriptEntries(list);
			this.Response.ContentType = CombinedScriptWriter.GetContentType(list);
			HttpCachePolicy cache = this.Response.Cache;
			cache.SetCacheability(HttpCacheability.Public);
			cache.VaryByParams["_TSM_HiddenField_"] = true;
			cache.VaryByParams["_TSM_CombinedScripts_"] = true;
			cache.VaryByParams["compress"] = true;
			cache.VaryByHeaders["User-Agent"] = true;
			cache.SetOmitVaryStar(true);
			cache.SetExpires(DateTime.Now.AddDays(365.0));
			cache.SetMaxAge(TimeSpan.FromDays(365.0));
			cache.SetLastModified(this.GetLastModifiedDate());
			cache.SetValidUntilExpires(true);
			Stream stream = this.Response.OutputStream;
			OutputCompression outputCompression = OutputCompression.AutoDetect;
			if (this.Request.Params["compress"] != null)
			{
				outputCompression = (OutputCompression)int.Parse(this.Request.Params["compress"]);
			}
			bool flag = outputCompression == OutputCompression.Forced || (outputCompression == OutputCompression.AutoDetect && this.IsGzipSupportingBrowser());
			if (flag)
			{
				foreach (string b in (this.Request.Headers["Accept-Encoding"] ?? "").ToUpperInvariant().Split(new char[]
				{
					','
				}))
				{
					if ("GZIP" == b)
					{
						this.Response.AddHeader("Content-encoding", "gzip");
						stream = new GZipStream(stream, CompressionMode.Compress);
						break;
					}
					if ("DEFLATE" == b)
					{
						this.Response.AddHeader("Content-encoding", "deflate");
						stream = new DeflateStream(stream, CompressionMode.Compress);
						break;
					}
				}
			}
			string text3 = string.Empty;
			string text4 = this.Request.Params["pk"];
			bool flag2 = text4 != null && !string.IsNullOrEmpty(text);
			string resourceUid = text2;
			bool flag3 = flag2 && ScriptCacheProviderManager.Provider.Exists(resourceUid);
			if (!this.UseStreams && flag2)
			{
				if (flag3)
				{
					text3 = ScriptCacheProviderManager.Provider.Get(resourceUid);
				}
				else
				{
					using (StringWriter stringWriter = new StringWriter())
					{
						this.WriteResponse(stringWriter, list, text, text2);
						text3 = stringWriter.ToString();
					}
					ScriptCacheProviderManager.Provider.Store(resourceUid, text3);
					using (StreamWriter streamWriter = new StreamWriter(stream))
					{
						streamWriter.Write(text3);
					}
				}
				if (!ScriptCacheProviderManager.Provider.AreAssociated(text4, resourceUid))
				{
					ScriptCacheProviderManager.Provider.Associate(text4, resourceUid);
					return;
				}
			}
			else
			{
				using (StreamWriter streamWriter2 = new StreamWriter(stream))
				{
					this.WriteResponse(streamWriter2, list, text, text2);
				}
			}
		}

		// Token: 0x06010065 RID: 65637 RVA: 0x00398150 File Offset: 0x00396350
		private void WriteResponse(TextWriter responseWriter, List<ScriptEntry> scriptEntries, string hiddenFieldName, string combinedScripts)
		{
			this.WriteScripts(scriptEntries, responseWriter);
			if (!string.IsNullOrEmpty(hiddenFieldName))
			{
				responseWriter.WriteLine("if(typeof(Sys)!=='undefined')Sys.Application.notifyScriptLoaded();");
				if (IdentifierValidator.IsValid(hiddenFieldName) && (scriptEntries.Count > 0 || combinedScripts == ";"))
				{
					string text = this.FilterValidScripts(scriptEntries);
					string value = string.Format(CultureInfo.InvariantCulture, "(function() {{\r\n    function loadHandler() {{\r\n        var hf = window.__TsmHiddenField;\r\n        if (!hf) return;\r\n        if (!hf._RSM_init) {{ hf._RSM_init = true; hf.value = ''; }}\r\n        hf.value += '{0}';\r\n        Sys.Application.remove_load(loadHandler);\r\n    }};\r\n    Sys.Application.add_load(loadHandler);\r\n}})();", new object[]
					{
						text
					});
					responseWriter.WriteLine(value);
				}
			}
			responseWriter.Flush();
		}

		// Token: 0x06010066 RID: 65638 RVA: 0x003981C8 File Offset: 0x003963C8
		private DateTime GetLastModifiedDate()
		{
			DateTime dateTime = DateTime.MinValue;
			foreach (CombinedScriptWriter.ScriptAssemblyInfo scriptAssemblyInfo in this._loadedAssemblies.Values)
			{
				if (scriptAssemblyInfo.ModifiedTime > dateTime)
				{
					dateTime = scriptAssemblyInfo.ModifiedTime;
				}
			}
			if (dateTime == DateTime.MinValue)
			{
				dateTime = this.GetTelerikAssemblyReleaseDate();
			}
			if (dateTime > DateTime.Now)
			{
				dateTime = DateTime.Now;
			}
			return dateTime;
		}

		// Token: 0x06010067 RID: 65639 RVA: 0x0039825C File Offset: 0x0039645C
		private DateTime GetTelerikAssemblyReleaseDate()
		{
			System.Version version = new AssemblyName(base.GetType().Assembly.FullName).Version;
			int build = version.Build;
			int num = version.Major;
			int num2 = Convert.ToInt32(Math.Floor(build / 100m));
			if (num2 > 12)
			{
				num2 %= 12;
				num++;
			}
			int day = build % 100;
			return DateTime.SpecifyKind(new DateTime(num, num2, day), DateTimeKind.Utc);
		}

		// Token: 0x06010068 RID: 65640 RVA: 0x003982D4 File Offset: 0x003964D4
		private string FilterValidScripts(List<ScriptEntry> scriptEntries)
		{
			if (scriptEntries.Count == 0)
			{
				return ";";
			}
			ScriptEntryUrlBuilder scriptEntryUrlBuilder = new ScriptEntryUrlBuilder("%3b", "");
			foreach (ScriptEntry scriptEntry in scriptEntries)
			{
				if (!(scriptEntry is InvalidScriptEntry))
				{
					scriptEntryUrlBuilder.RegisterScriptEntry(scriptEntry);
				}
			}
			List<string> urls = scriptEntryUrlBuilder.GetUrls();
			if (urls.Count > 0)
			{
				return HttpContext.Current.Server.UrlDecode(urls[0]);
			}
			return string.Empty;
		}

		// Token: 0x06010069 RID: 65641 RVA: 0x00398374 File Offset: 0x00396574
		private bool IsGzipSupportingBrowser()
		{
			return !this.Request.Browser.IsBrowser("IE") || this.Request.Browser.MajorVersion > 6;
		}

		// Token: 0x0601006A RID: 65642 RVA: 0x003983F0 File Offset: 0x003965F0
		private void WriteScripts(List<ScriptEntry> scriptEntries, TextWriter responseWriter)
		{
			foreach (ScriptEntry scriptEntry in scriptEntries)
			{
				string value = scriptEntry.Name;
				if (scriptEntry is ExternalScriptEntry)
				{
					value = "External Script: " + scriptEntry.Path.Substring(scriptEntry.Path.IndexOf("|") + 1);
				}
				responseWriter.Write("/* START ");
				responseWriter.Write(value);
				responseWriter.WriteLine(" */");
				if (this.UseStreams)
				{
					this.WriteScriptEntryContentViaStreams(scriptEntry, responseWriter);
				}
				else
				{
					string text = scriptEntry.GetScript();
					CombinedScriptWriter.ScriptAssemblyInfo assemblyInfo = this.GetLoadedScriptEntryAssembly(scriptEntry);
					if (assemblyInfo != null)
					{
						text = this.WebResourceRegex.Replace(text, delegate(Match match)
						{
							Type assemblyType = assemblyInfo.AssemblyType;
							return this.Page.ClientScript.GetWebResourceUrl(assemblyType, match.Groups["resourceName"].Value);
						});
					}
					responseWriter.WriteLine(text);
				}
				CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
				try
				{
					try
					{
						Thread.CurrentThread.CurrentUICulture = new CultureInfo(scriptEntry.Culture);
					}
					catch (ArgumentException)
					{
					}
					this.WriteAssemblyScriptResources(responseWriter, scriptEntry);
				}
				finally
				{
					Thread.CurrentThread.CurrentUICulture = currentUICulture;
				}
				responseWriter.Write("/* END ");
				responseWriter.Write(scriptEntry.Name);
				responseWriter.WriteLine(" */");
			}
		}

		// Token: 0x0601006B RID: 65643 RVA: 0x003985DC File Offset: 0x003967DC
		private void WriteScriptEntryContentViaStreams(ScriptEntry scriptEntry, TextWriter responseWriter)
		{
			CombinedScriptWriter.ScriptAssemblyInfo assemblyInfo = this.GetLoadedScriptEntryAssembly(scriptEntry);
			WebResourceNameEvaluator nameEvaluator = delegate(string webResourceName)
			{
				if (assemblyInfo != null)
				{
					Type assemblyType = assemblyInfo.AssemblyType;
					return this.Page.ClientScript.GetWebResourceUrl(assemblyType, webResourceName);
				}
				return string.Empty;
			};
			using (Stream resourceStream = scriptEntry.GetResourceStream())
			{
				if (resourceStream != null)
				{
					using (StreamReader streamReader = new StreamReader(resourceStream))
					{
						this.WebResourceWriter.WriteResource(streamReader, responseWriter, nameEvaluator);
					}
				}
			}
			responseWriter.WriteLine();
		}

		// Token: 0x0601006C RID: 65644 RVA: 0x0039866C File Offset: 0x0039686C
		private CombinedScriptWriter.ScriptAssemblyInfo GetLoadedScriptEntryAssembly(ScriptEntry scriptEntry)
		{
			if (!this._loadedAssemblies.ContainsKey(scriptEntry.Assembly))
			{
				Assembly assembly = scriptEntry.LoadAssembly();
				if (!(assembly != null))
				{
					return null;
				}
				this._loadedAssemblies[scriptEntry.Assembly] = new CombinedScriptWriter.ScriptAssemblyInfo(assembly, scriptEntry.Name);
			}
			return this._loadedAssemblies[scriptEntry.Assembly];
		}

		// Token: 0x0601006D RID: 65645 RVA: 0x003986D0 File Offset: 0x003968D0
		private void WriteAssemblyScriptResources(TextWriter outputWriter, ScriptEntry scriptEntry)
		{
			CombinedScriptWriter.ScriptAssemblyInfo loadedScriptEntryAssembly = this.GetLoadedScriptEntryAssembly(scriptEntry);
			if (loadedScriptEntryAssembly == null)
			{
				return;
			}
			Assembly assembly = loadedScriptEntryAssembly.Assembly;
			foreach (ScriptResourceAttribute scriptResourceAttribute in assembly.GetCustomAttributes(typeof(ScriptResourceAttribute), false))
			{
				if (scriptResourceAttribute.ScriptName == scriptEntry.Name)
				{
					string stringResourceClientTypeName = scriptResourceAttribute.StringResourceClientTypeName;
					string text = scriptResourceAttribute.StringResourceName;
					CombinedScriptWriter.RegisterNamespace(outputWriter, stringResourceClientTypeName, false);
					outputWriter.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0}={{", new object[]
					{
						stringResourceClientTypeName
					}));
					if (text.EndsWith(".resources", StringComparison.OrdinalIgnoreCase))
					{
						text = text.Substring(0, text.Length - 10);
					}
					ResourceManager resourceManager = new ResourceManager(text, assembly);
					using (ResourceSet resourceSet = resourceManager.GetResourceSet(CultureInfo.InvariantCulture, true, true))
					{
						bool flag = true;
						foreach (object obj in resourceSet)
						{
							DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
							if (!flag)
							{
								outputWriter.Write(",");
							}
							string text2 = (string)dictionaryEntry.Key;
							string @string = resourceManager.GetString(text2);
							outputWriter.Write(string.Format(CultureInfo.InvariantCulture, "\"{0}\":\"{1}\"", new object[]
							{
								JavaScriptString.QuoteString(text2),
								JavaScriptString.QuoteString(@string)
							}));
							flag = false;
						}
					}
					outputWriter.WriteLine("};");
				}
			}
		}

		// Token: 0x0400488C RID: 18572
		private const int DaysToCache = 365;

		// Token: 0x0400488D RID: 18573
		internal const string CompressParameterName = "compress";

		// Token: 0x0400488E RID: 18574
		internal const string PageKeyParameterName = "pk";

		// Token: 0x0400488F RID: 18575
		private readonly ICryptoService _cryptoService;

		// Token: 0x04004890 RID: 18576
		private static WhiteListLoader loader;

		// Token: 0x04004891 RID: 18577
		private readonly Regex _webResourceRegex = new Regex("<%\\s*=\\s*WebResource\\(\"(?<resourceName>[^\"]*)\"\\)\\s*%>", RegexOptions.Multiline | RegexOptions.Singleline);

		// Token: 0x04004892 RID: 18578
		private Page _page;

		// Token: 0x04004893 RID: 18579
		private HttpContext _context;

		// Token: 0x04004894 RID: 18580
		private HttpRequest _request;

		// Token: 0x04004895 RID: 18581
		private HttpResponse _response;

		// Token: 0x04004896 RID: 18582
		private WebResourceWriter _webResourceWriter;

		// Token: 0x04004897 RID: 18583
		private readonly Dictionary<string, CombinedScriptWriter.ScriptAssemblyInfo> _loadedAssemblies = new Dictionary<string, CombinedScriptWriter.ScriptAssemblyInfo>();

		// Token: 0x020019E1 RID: 6625
		private class ScriptAssemblyInfo
		{
			// Token: 0x0601006E RID: 65646 RVA: 0x00398888 File Offset: 0x00396A88
			public ScriptAssemblyInfo(Assembly assembly, string resourceName)
			{
				this._assembly = assembly;
			}

			// Token: 0x17004D63 RID: 19811
			// (get) Token: 0x0601006F RID: 65647 RVA: 0x003988A2 File Offset: 0x00396AA2
			public Assembly Assembly
			{
				get
				{
					return this._assembly;
				}
			}

			// Token: 0x17004D64 RID: 19812
			// (get) Token: 0x06010070 RID: 65648 RVA: 0x003988AC File Offset: 0x00396AAC
			public Type AssemblyType
			{
				get
				{
					if (this._assemblyType == null)
					{
						Type[] array = null;
						try
						{
							array = this.Assembly.GetTypes();
						}
						catch (ReflectionTypeLoadException ex)
						{
							array = ex.Types;
						}
						if (array.Length > 0)
						{
							this._assemblyType = array[0];
						}
					}
					return this._assemblyType;
				}
			}

			// Token: 0x17004D65 RID: 19813
			// (get) Token: 0x06010071 RID: 65649 RVA: 0x00398908 File Offset: 0x00396B08
			[SuppressMessage("Microsoft.Security", "CA2106:SecureAsserts")]
			public DateTime ModifiedTime
			{
				get
				{
					if (!this._modifiedTimeSet)
					{
						this._modifiedTimeSet = true;
						try
						{
							Uri uri = new Uri(this.Assembly.CodeBase);
							if (!uri.IsFile)
							{
								return DateTime.MinValue;
							}
							string localPath = uri.LocalPath;
							new FileIOPermission(FileIOPermissionAccess.Read, localPath).Assert();
							this._modifiedTime = File.GetLastWriteTime(localPath);
						}
						catch (SecurityException)
						{
						}
					}
					return this._modifiedTime;
				}
			}

			// Token: 0x04004898 RID: 18584
			private readonly Assembly _assembly;

			// Token: 0x04004899 RID: 18585
			private Type _assemblyType;

			// Token: 0x0400489A RID: 18586
			private DateTime _modifiedTime = DateTime.MinValue;

			// Token: 0x0400489B RID: 18587
			private bool _modifiedTimeSet;
		}
	}
}
