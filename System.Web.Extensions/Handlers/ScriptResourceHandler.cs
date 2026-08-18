using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Resources;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Resources;
using System.Web.Security.Cryptography;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Handlers
{
	// Token: 0x020000DE RID: 222
	public class ScriptResourceHandler : IHttpHandler
	{
		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x0002A009 File Offset: 0x00028209
		private static string ScriptResourceAbsolutePath
		{
			get
			{
				if (ScriptResourceHandler._scriptResourceAbsolutePath == null)
				{
					ScriptResourceHandler._scriptResourceAbsolutePath = VirtualPathUtility.ToAbsolute("~/ScriptResource.axd");
				}
				return ScriptResourceHandler._scriptResourceAbsolutePath;
			}
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0002A026 File Offset: 0x00028226
		private static Exception Create404(Exception innerException)
		{
			return new HttpException(404, AtlasWeb.ScriptResourceHandler_InvalidRequest, innerException);
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0002A038 File Offset: 0x00028238
		internal static CultureInfo DetermineNearestAvailableCulture(Assembly assembly, string scriptResourceName, CultureInfo culture)
		{
			if (string.IsNullOrEmpty(scriptResourceName))
			{
				return CultureInfo.InvariantCulture;
			}
			Tuple<Assembly, string, CultureInfo> key = Tuple.Create<Assembly, string, CultureInfo>(assembly, scriptResourceName, culture);
			CultureInfo cultureInfo = (CultureInfo)ScriptResourceHandler._cultureCache[key];
			if (cultureInfo == null)
			{
				string text = scriptResourceName.EndsWith(".debug.js", StringComparison.OrdinalIgnoreCase) ? (scriptResourceName.Substring(0, scriptResourceName.Length - 9) + ".js") : null;
				ScriptResourceInfo instance = ScriptResourceInfo.GetInstance(assembly, scriptResourceName);
				ScriptResourceInfo scriptResourceInfo = (text != null) ? ScriptResourceInfo.GetInstance(assembly, text) : null;
				if (!string.IsNullOrEmpty(instance.ScriptResourceName) || (scriptResourceInfo != null && !string.IsNullOrEmpty(scriptResourceInfo.ScriptResourceName)))
				{
					ResourceManager resourceManager = ScriptResourceAttribute.GetResourceManager(instance.ScriptResourceName, assembly);
					ResourceManager resourceManager2 = (scriptResourceInfo != null) ? ScriptResourceAttribute.GetResourceManager(scriptResourceInfo.ScriptResourceName, assembly) : null;
					ResourceSet resourceSet = null;
					ResourceSet resourceSet2 = null;
					if (resourceManager != null)
					{
						resourceManager.GetResourceSet(CultureInfo.InvariantCulture, true, true);
						resourceSet = resourceManager.GetResourceSet(culture, true, false);
					}
					if (resourceManager2 != null)
					{
						resourceManager2.GetResourceSet(CultureInfo.InvariantCulture, true, true);
						resourceSet2 = resourceManager2.GetResourceSet(culture, true, false);
					}
					if (resourceManager == null)
					{
						if (resourceManager2 == null)
						{
							culture = CultureInfo.InvariantCulture;
							goto IL_156;
						}
					}
					while (resourceSet == null)
					{
						if (resourceSet2 != null)
						{
							break;
						}
						culture = culture.Parent;
						if (culture.Equals(CultureInfo.InvariantCulture))
						{
							break;
						}
						resourceSet = resourceManager.GetResourceSet(culture, true, false);
						resourceSet2 = ((resourceManager2 != null) ? resourceManager2.GetResourceSet(culture, true, false) : null);
					}
				}
				else
				{
					culture = CultureInfo.InvariantCulture;
				}
				IL_156:
				CultureInfo assemblyNeutralCulture = ScriptResourceHandler.GetAssemblyNeutralCulture(assembly);
				if (assemblyNeutralCulture != null && assemblyNeutralCulture.Equals(culture))
				{
					culture = CultureInfo.InvariantCulture;
				}
				cultureInfo = culture;
				ScriptResourceHandler._cultureCache[key] = cultureInfo;
			}
			return cultureInfo;
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0002A1C7 File Offset: 0x000283C7
		private static void EnsureScriptResourceRequest(string path)
		{
			if (!ScriptResourceHandler.IsScriptResourceRequest(path))
			{
				ScriptResourceHandler.Throw404();
			}
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0002A1D8 File Offset: 0x000283D8
		private static Assembly GetAssembly(string assemblyName)
		{
			string[] array = assemblyName.Split(new char[]
			{
				','
			});
			if (array.Length != 1 && array.Length != 4)
			{
				ScriptResourceHandler.Throw404();
			}
			AssemblyName assemblyName2 = new AssemblyName();
			assemblyName2.Name = array[0];
			if (array.Length == 4)
			{
				assemblyName2.Version = new Version(array[1]);
				string text = array[2];
				assemblyName2.CultureInfo = ((text.Length > 0) ? new CultureInfo(text) : CultureInfo.InvariantCulture);
				assemblyName2.SetPublicKeyToken(HexParser.Parse(array[3]));
			}
			Assembly result = null;
			try
			{
				result = Assembly.Load(assemblyName2);
			}
			catch (FileNotFoundException innerException)
			{
				ScriptResourceHandler.Throw404(innerException);
			}
			catch (FileLoadException innerException2)
			{
				ScriptResourceHandler.Throw404(innerException2);
			}
			catch (BadImageFormatException innerException3)
			{
				ScriptResourceHandler.Throw404(innerException3);
			}
			return result;
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0002A2B0 File Offset: 0x000284B0
		private static Tuple<AssemblyName, string> GetAssemblyInfo(Assembly assembly)
		{
			Tuple<AssemblyName, string> tuple = (Tuple<AssemblyName, string>)ScriptResourceHandler._assemblyInfoCache[assembly];
			if (tuple == null)
			{
				tuple = ScriptResourceHandler.GetAssemblyInfoInternal(assembly);
				ScriptResourceHandler._assemblyInfoCache[assembly] = tuple;
			}
			return tuple;
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0002A2E8 File Offset: 0x000284E8
		private static Tuple<AssemblyName, string> GetAssemblyInfoInternal(Assembly assembly)
		{
			AssemblyName item = new AssemblyName(assembly.FullName);
			string item2 = Convert.ToBase64String(assembly.ManifestModule.ModuleVersionId.ToByteArray());
			return new Tuple<AssemblyName, string>(item, item2);
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0002A324 File Offset: 0x00028524
		private static CultureInfo GetAssemblyNeutralCulture(Assembly assembly)
		{
			CultureInfo cultureInfo = (CultureInfo)ScriptResourceHandler._cultureCache[assembly];
			if (cultureInfo == null)
			{
				object[] customAttributes = assembly.GetCustomAttributes(typeof(NeutralResourcesLanguageAttribute), false);
				if (customAttributes != null && customAttributes.Length != 0)
				{
					cultureInfo = CultureInfo.GetCultureInfo(((NeutralResourcesLanguageAttribute)customAttributes[0]).CultureName);
					ScriptResourceHandler._cultureCache[assembly] = cultureInfo;
				}
			}
			return cultureInfo;
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0002A37E File Offset: 0x0002857E
		internal static string GetEmptyPageUrl(string title)
		{
			return ScriptResourceHandler.GetScriptResourceHandler().GetEmptyPageUrl(title);
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0002A38B File Offset: 0x0002858B
		private static IScriptResourceHandler GetScriptResourceHandler()
		{
			if (ScriptResourceHandler._scriptResourceHandler == null)
			{
				ScriptResourceHandler._scriptResourceHandler = new ScriptResourceHandler.RuntimeScriptResourceHandler();
			}
			return ScriptResourceHandler._scriptResourceHandler;
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x0002A3A3 File Offset: 0x000285A3
		internal static string GetScriptResourceUrl(Assembly assembly, string resourceName, CultureInfo culture, bool zip)
		{
			return ScriptResourceHandler.GetScriptResourceHandler().GetScriptResourceUrl(assembly, resourceName, culture, zip);
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x0002A3B3 File Offset: 0x000285B3
		internal static string GetScriptResourceUrl(List<Tuple<Assembly, List<Tuple<string, CultureInfo>>>> assemblyResourceLists, bool zip)
		{
			return ScriptResourceHandler.GetScriptResourceHandler().GetScriptResourceUrl(assemblyResourceLists, zip);
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x0001D1CA File Offset: 0x0001B3CA
		protected virtual bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x0002A3C1 File Offset: 0x000285C1
		private static bool IsCompressionEnabled(HttpContext context)
		{
			return ScriptingScriptResourceHandlerSection.ApplicationSettings.EnableCompression && (context == null || !context.Request.Browser.IsBrowser("IE") || context.Request.Browser.MajorVersion > 6);
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x0002A3FB File Offset: 0x000285FB
		internal static bool IsScriptResourceRequest(string path)
		{
			return !string.IsNullOrEmpty(path) && string.Equals(path, ScriptResourceHandler.ScriptResourceAbsolutePath, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x0002A413 File Offset: 0x00028613
		private static void OutputEmptyPage(HttpResponseBase response, string title)
		{
			ScriptResourceHandler.PrepareResponseCache(response);
			response.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><script type=\"text/javascript\">parent.Sys.Application._onIFrameLoad();</script><title>" + HttpUtility.HtmlEncode(title) + "</title></head><body></body></html>");
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0002A438 File Offset: 0x00028638
		private static void PrepareResponseCache(HttpResponseBase response)
		{
			HttpCachePolicyBase cache = response.Cache;
			DateTime now = DateTime.Now;
			cache.SetCacheability(HttpCacheability.Public);
			cache.VaryByParams["d"] = true;
			cache.SetOmitVaryStar(true);
			cache.SetExpires(now + TimeSpan.FromDays(365.0));
			cache.SetValidUntilExpires(true);
			cache.SetLastModified(now);
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x0002A49C File Offset: 0x0002869C
		private static void PrepareResponseNoCache(HttpResponseBase response)
		{
			HttpCachePolicyBase cache = response.Cache;
			DateTime now = DateTime.Now;
			cache.SetCacheability(HttpCacheability.Public);
			cache.SetExpires(now + TimeSpan.FromDays(365.0));
			cache.SetValidUntilExpires(true);
			cache.SetLastModified(now);
			cache.SetNoServerCaching();
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0002A4EB File Offset: 0x000286EB
		[SecuritySafeCritical]
		protected virtual void ProcessRequest(HttpContext context)
		{
			ScriptResourceHandler.ProcessRequest(new HttpContextWrapper(context), null, null, true);
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0002A4FC File Offset: 0x000286FC
		internal static void ProcessRequest(HttpContextBase context, ScriptResourceHandler.VirtualFileReader fileReader = null, Action<string, Exception> logAction = null, bool validatePath = true)
		{
			string text = null;
			bool flag = false;
			try
			{
				HttpResponseBase response = context.Response;
				response.Clear();
				if (validatePath)
				{
					ScriptResourceHandler.EnsureScriptResourceRequest(context.Request.Path);
				}
				string text2 = context.Request.QueryString["d"];
				if (string.IsNullOrEmpty(text2))
				{
					ScriptResourceHandler.Throw404();
				}
				flag = true;
				try
				{
					text = Page.DecryptString(text2, Purpose.ScriptResourceHandler_ScriptResourceUrl);
				}
				catch (CryptographicException innerException)
				{
					ScriptResourceHandler.Throw404(innerException);
				}
				ScriptResourceHandler.VirtualFileReader virtualFileReader;
				if ((virtualFileReader = fileReader) == null && (virtualFileReader = ScriptResourceHandler.<>c.<>9__30_0) == null)
				{
					virtualFileReader = (ScriptResourceHandler.<>c.<>9__30_0 = delegate(string virtualPath, out Encoding encoding)
					{
						VirtualPathProvider virtualPathProvider = HostingEnvironment.VirtualPathProvider;
						if (!virtualPathProvider.FileExists(virtualPath))
						{
							ScriptResourceHandler.Throw404();
						}
						VirtualFile file = virtualPathProvider.GetFile(virtualPath);
						if (!AppSettings.ScriptResourceAllowNonJsFiles && !file.Name.EndsWith(".js", StringComparison.OrdinalIgnoreCase))
						{
							ScriptResourceHandler.Throw404();
						}
						string result;
						using (Stream stream = file.Open())
						{
							using (StreamReader streamReader = new StreamReader(stream, true))
							{
								encoding = streamReader.CurrentEncoding;
								result = streamReader.ReadToEnd();
							}
						}
						return result;
					});
				}
				fileReader = virtualFileReader;
				ScriptResourceHandler.ProcessRequestInternal(response, text, fileReader);
			}
			catch (Exception arg)
			{
				if (flag)
				{
					logAction = (logAction ?? new Action<string, Exception>(AssemblyResourceLoader.LogWebResourceFailure));
					logAction(text, arg);
				}
				ScriptResourceHandler.Throw404();
			}
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0002A5E0 File Offset: 0x000287E0
		private static void ProcessRequestInternal(HttpResponseBase response, string decryptedString, ScriptResourceHandler.VirtualFileReader fileReader)
		{
			if (string.IsNullOrEmpty(decryptedString))
			{
				ScriptResourceHandler.Throw404();
			}
			char c = decryptedString[0];
			if (c <= 'Z')
			{
				switch (c)
				{
				case 'Q':
					goto IL_72;
				case 'R':
					goto IL_78;
				case 'S':
					goto IL_8C;
				case 'T':
					ScriptResourceHandler.OutputEmptyPage(response, decryptedString.Substring(1));
					return;
				case 'U':
					goto IL_6C;
				default:
					if (c != 'Z')
					{
						goto IL_8C;
					}
					break;
				}
			}
			else
			{
				switch (c)
				{
				case 'q':
					goto IL_72;
				case 'r':
					goto IL_78;
				case 's':
				case 't':
					goto IL_8C;
				case 'u':
					goto IL_6C;
				default:
					if (c != 'z')
					{
						goto IL_8C;
					}
					break;
				}
			}
			bool flag = true;
			bool flag2 = true;
			goto IL_92;
			IL_6C:
			flag = true;
			flag2 = false;
			goto IL_92;
			IL_72:
			flag = false;
			flag2 = true;
			goto IL_92;
			IL_78:
			flag = false;
			flag2 = false;
			goto IL_92;
			IL_8C:
			ScriptResourceHandler.Throw404();
			return;
			IL_92:
			decryptedString = decryptedString.Substring(1);
			if (string.IsNullOrEmpty(decryptedString))
			{
				ScriptResourceHandler.Throw404();
			}
			string[] array = decryptedString.Split(new char[]
			{
				'|'
			});
			if (flag)
			{
				if (array.Length != 3 && array.Length != 5)
				{
					ScriptResourceHandler.Throw404();
				}
			}
			else if (array.Length % 2 != 0)
			{
				ScriptResourceHandler.Throw404();
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text = null;
			if (flag)
			{
				string assemblyName = array[0];
				string resourceName = array[1];
				string text2 = array[2];
				Assembly assembly = ScriptResourceHandler.GetAssembly(assemblyName);
				if (assembly == null)
				{
					ScriptResourceHandler.Throw404();
				}
				stringBuilder.Append(ScriptResourceAttribute.GetScriptFromWebResourceInternal(assembly, resourceName, string.IsNullOrEmpty(text2) ? CultureInfo.InvariantCulture : new CultureInfo(text2), flag2, out text));
			}
			else
			{
				bool flag3 = false;
				for (int i = 0; i < array.Length; i += 2)
				{
					string text3 = array[i];
					bool flag4 = !string.IsNullOrEmpty(text3);
					if (!flag4 || text3[0] != '#')
					{
						string[] array2 = array[i + 1].Split(new char[]
						{
							','
						});
						if (array2.Length == 0)
						{
							ScriptResourceHandler.Throw404();
						}
						Assembly assembly2 = flag4 ? ScriptResourceHandler.GetAssembly(text3) : null;
						if (assembly2 == null)
						{
							if (text == null)
							{
								text = "text/javascript";
							}
							for (int j = 0; j < array2.Length; j++)
							{
								string virtualPath = ScriptResourceHandler._bypassVirtualPathResolution ? array2[j] : VirtualPathUtility.ToAbsolute(array2[j]);
								Encoding encoding;
								string value = fileReader(virtualPath, out encoding);
								if (flag3)
								{
									stringBuilder.Append('\n');
								}
								flag3 = true;
								stringBuilder.Append(value);
							}
						}
						else
						{
							for (int k = 0; k < array2.Length; k += 2)
							{
								try
								{
									string resourceName2 = array2[k];
									string text4 = array2[k + 1];
									if (flag3)
									{
										stringBuilder.Append('\n');
									}
									flag3 = true;
									string text5;
									stringBuilder.Append(ScriptResourceAttribute.GetScriptFromWebResourceInternal(assembly2, resourceName2, string.IsNullOrEmpty(text4) ? CultureInfo.InvariantCulture : new CultureInfo(text4), flag2, out text5));
									if (text == null)
									{
										text = text5;
									}
								}
								catch (MissingManifestResourceException innerException)
								{
									throw ScriptResourceHandler.Create404(innerException);
								}
								catch (HttpException innerException2)
								{
									throw ScriptResourceHandler.Create404(innerException2);
								}
							}
						}
					}
				}
			}
			if (ScriptingScriptResourceHandlerSection.ApplicationSettings.EnableCaching)
			{
				ScriptResourceHandler.PrepareResponseCache(response);
			}
			else
			{
				ScriptResourceHandler.PrepareResponseNoCache(response);
			}
			response.ContentType = text;
			if (flag2)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (Stream stream = new GZipStream(memoryStream, CompressionMode.Compress))
					{
						using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8))
						{
							streamWriter.Write(stringBuilder.ToString());
						}
					}
					byte[] array3 = memoryStream.ToArray();
					response.AddHeader("Content-encoding", "gzip");
					response.OutputStream.Write(array3, 0, array3.Length);
					return;
				}
			}
			response.Write(stringBuilder.ToString());
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0002A970 File Offset: 0x00028B70
		internal static void SetScriptResourceHandler(IScriptResourceHandler scriptResourceHandler)
		{
			ScriptResourceHandler._scriptResourceHandler = scriptResourceHandler;
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0002A978 File Offset: 0x00028B78
		private static void Throw404()
		{
			throw ScriptResourceHandler.Create404(null);
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0002A980 File Offset: 0x00028B80
		private static void Throw404(Exception innerException)
		{
			throw ScriptResourceHandler.Create404(innerException);
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0002A988 File Offset: 0x00028B88
		void IHttpHandler.ProcessRequest(HttpContext context)
		{
			this.ProcessRequest(context);
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x0002A991 File Offset: 0x00028B91
		bool IHttpHandler.IsReusable
		{
			get
			{
				return this.IsReusable;
			}
		}

		// Token: 0x0400036D RID: 877
		private const string _scriptResourceUrl = "~/ScriptResource.axd";

		// Token: 0x0400036E RID: 878
		private static readonly IDictionary _assemblyInfoCache = Hashtable.Synchronized(new Hashtable());

		// Token: 0x0400036F RID: 879
		private static readonly IDictionary _cultureCache = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04000370 RID: 880
		private static readonly object _getMethodLock = new object();

		// Token: 0x04000371 RID: 881
		private static IScriptResourceHandler _scriptResourceHandler = new ScriptResourceHandler.RuntimeScriptResourceHandler();

		// Token: 0x04000372 RID: 882
		private static string _scriptResourceAbsolutePath;

		// Token: 0x04000373 RID: 883
		private static bool _bypassVirtualPathResolution = false;

		// Token: 0x04000374 RID: 884
		private static int _maximumResourceUrlLength = 2048;

		// Token: 0x02000175 RID: 373
		// (Invoke) Token: 0x06001068 RID: 4200
		internal delegate string VirtualFileReader(string virtualPath, out Encoding encoding);

		// Token: 0x02000176 RID: 374
		private class RuntimeScriptResourceHandler : IScriptResourceHandler
		{
			// Token: 0x0600106B RID: 4203 RVA: 0x000382B6 File Offset: 0x000364B6
			string IScriptResourceHandler.GetScriptResourceUrl(Assembly assembly, string resourceName, CultureInfo culture, bool zip)
			{
				return ((IScriptResourceHandler)this).GetScriptResourceUrl(new List<Tuple<Assembly, List<Tuple<string, CultureInfo>>>>
				{
					new Tuple<Assembly, List<Tuple<string, CultureInfo>>>(assembly, new List<Tuple<string, CultureInfo>>
					{
						new Tuple<string, CultureInfo>(resourceName, culture)
					})
				}, zip);
			}

			// Token: 0x0600106C RID: 4204 RVA: 0x000382E4 File Offset: 0x000364E4
			string IScriptResourceHandler.GetScriptResourceUrl(List<Tuple<Assembly, List<Tuple<string, CultureInfo>>>> assemblyResourceLists, bool zip)
			{
				if (!ScriptResourceHandler.IsCompressionEnabled(HttpContext.Current))
				{
					zip = false;
				}
				bool flag = true;
				foreach (Tuple<Assembly, List<Tuple<string, CultureInfo>>> tuple in assemblyResourceLists)
				{
					if (tuple.Item1 == null)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					List<object> list = new List<object>();
					foreach (Tuple<Assembly, List<Tuple<string, CultureInfo>>> tuple2 in assemblyResourceLists)
					{
						list.Add(tuple2.Item1);
						foreach (Tuple<string, CultureInfo> tuple3 in tuple2.Item2)
						{
							list.Add(tuple3.Item1);
							list.Add(tuple3.Item2);
						}
					}
					list.Add(zip);
					string text = (string)ScriptResourceHandler.RuntimeScriptResourceHandler._urlCache[list];
					if (text == null)
					{
						text = ScriptResourceHandler.RuntimeScriptResourceHandler.GetScriptResourceUrlImpl(assemblyResourceLists, zip);
						ScriptResourceHandler.RuntimeScriptResourceHandler._urlCache[list] = text;
					}
					return text;
				}
				return ScriptResourceHandler.RuntimeScriptResourceHandler.GetScriptResourceUrlImpl(assemblyResourceLists, zip);
			}

			// Token: 0x0600106D RID: 4205 RVA: 0x0003843C File Offset: 0x0003663C
			[SecuritySafeCritical]
			private static string GetScriptResourceUrlImpl(List<Tuple<Assembly, List<Tuple<string, CultureInfo>>>> assemblyResourceLists, bool zip)
			{
				ScriptResourceHandler.RuntimeScriptResourceHandler.EnsureAbsoluteScriptResourceUrl();
				bool flag = false;
				if (assemblyResourceLists.Count == 1)
				{
					Tuple<Assembly, List<Tuple<string, CultureInfo>>> tuple = assemblyResourceLists[0];
					if (tuple.Item1 != null && tuple.Item2.Count == 1)
					{
						flag = true;
					}
				}
				string value;
				if (flag)
				{
					value = (zip ? "Z" : "U");
				}
				else
				{
					value = (zip ? "Q" : "R");
				}
				StringBuilder stringBuilder = new StringBuilder(value);
				HashCodeCombiner hashCodeCombiner = new HashCodeCombiner();
				bool flag2 = true;
				foreach (Tuple<Assembly, List<Tuple<string, CultureInfo>>> tuple2 in assemblyResourceLists)
				{
					if (!flag2)
					{
						stringBuilder.Append('|');
					}
					else
					{
						flag2 = false;
					}
					if (tuple2.Item1 != null)
					{
						Tuple<AssemblyName, string> assemblyInfo = ScriptResourceHandler.GetAssemblyInfo(tuple2.Item1);
						AssemblyName item = assemblyInfo.Item1;
						string item2 = assemblyInfo.Item2;
						hashCodeCombiner.AddObject(item2);
						if (tuple2.Item1.GlobalAssemblyCache)
						{
							stringBuilder.Append(item.Name);
							stringBuilder.Append(',');
							stringBuilder.Append(item.Version);
							stringBuilder.Append(',');
							if (item.CultureInfo != null)
							{
								stringBuilder.Append(item.CultureInfo);
							}
							stringBuilder.Append(',');
							stringBuilder.Append(HexParser.ToString(item.GetPublicKeyToken()));
						}
						else
						{
							stringBuilder.Append(item.Name);
						}
					}
					stringBuilder.Append('|');
					bool flag3 = true;
					foreach (Tuple<string, CultureInfo> tuple3 in tuple2.Item2)
					{
						if (!flag3)
						{
							stringBuilder.Append(',');
						}
						if (tuple2.Item1 != null)
						{
							stringBuilder.Append(tuple3.Item1);
							Tuple<Assembly, string, CultureInfo> key = Tuple.Create<Assembly, string, CultureInfo>(tuple2.Item1, tuple3.Item1, tuple3.Item2);
							string text = (string)ScriptResourceHandler.RuntimeScriptResourceHandler._cultureCache[key];
							if (text == null)
							{
								ScriptResourceInfo instance = ScriptResourceInfo.GetInstance(tuple2.Item1, tuple3.Item1);
								if (instance == ScriptResourceInfo.Empty)
								{
									ScriptResourceHandler.RuntimeScriptResourceHandler.ThrowUnknownResource(tuple3.Item1);
								}
								if (tuple2.Item1.GetManifestResourceStream(instance.ScriptName) == null)
								{
									ScriptResourceHandler.RuntimeScriptResourceHandler.ThrowUnknownResource(tuple3.Item1);
								}
								text = ScriptResourceHandler.DetermineNearestAvailableCulture(tuple2.Item1, tuple3.Item1, tuple3.Item2).Name;
								ScriptResourceHandler.RuntimeScriptResourceHandler._cultureCache[key] = text;
							}
							stringBuilder.Append(flag ? "|" : ",");
							stringBuilder.Append(text);
						}
						else
						{
							if (!ScriptResourceHandler._bypassVirtualPathResolution)
							{
								VirtualPathProvider virtualPathProvider = HostingEnvironment.VirtualPathProvider;
								if (!virtualPathProvider.FileExists(tuple3.Item1))
								{
									ScriptResourceHandler.RuntimeScriptResourceHandler.ThrowUnknownResource(tuple3.Item1);
								}
								string fileHash = virtualPathProvider.GetFileHash(tuple3.Item1, new string[]
								{
									tuple3.Item1
								});
								hashCodeCombiner.AddObject(fileHash);
							}
							stringBuilder.Append(tuple3.Item1);
						}
						flag3 = false;
					}
				}
				string text2;
				if (flag)
				{
					text2 = ScriptResourceHandler.RuntimeScriptResourceHandler._absoluteScriptResourceUrl + Page.EncryptString(stringBuilder.ToString(), Purpose.ScriptResourceHandler_ScriptResourceUrl) + "&t=" + hashCodeCombiner.CombinedHashString;
				}
				else
				{
					stringBuilder.Append("|#|");
					stringBuilder.Append(hashCodeCombiner.CombinedHashString);
					text2 = ScriptResourceHandler.RuntimeScriptResourceHandler._absoluteScriptResourceUrl + Page.EncryptString(stringBuilder.ToString(), Purpose.ScriptResourceHandler_ScriptResourceUrl);
				}
				if (text2.Length > ScriptResourceHandler._maximumResourceUrlLength)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ScriptResourceHandler_ResourceUrlTooLong, new object[]
					{
						ScriptResourceHandler._maximumResourceUrlLength
					}));
				}
				return text2;
			}

			// Token: 0x0600106E RID: 4206 RVA: 0x00038828 File Offset: 0x00036A28
			private static void EnsureAbsoluteScriptResourceUrl()
			{
				if (ScriptResourceHandler.RuntimeScriptResourceHandler._absoluteScriptResourceUrl == null)
				{
					ScriptResourceHandler.RuntimeScriptResourceHandler._absoluteScriptResourceUrl = (ScriptResourceHandler._bypassVirtualPathResolution ? "~/ScriptResource.axd?d=" : (VirtualPathUtility.ToAbsolute("~/ScriptResource.axd") + "?d="));
				}
			}

			// Token: 0x0600106F RID: 4207 RVA: 0x00038858 File Offset: 0x00036A58
			string IScriptResourceHandler.GetEmptyPageUrl(string title)
			{
				ScriptResourceHandler.RuntimeScriptResourceHandler.EnsureAbsoluteScriptResourceUrl();
				return ScriptResourceHandler.RuntimeScriptResourceHandler._absoluteScriptResourceUrl + Page.EncryptString("T" + title, Purpose.ScriptResourceHandler_ScriptResourceUrl);
			}

			// Token: 0x06001070 RID: 4208 RVA: 0x0003887E File Offset: 0x00036A7E
			private static void ThrowUnknownResource(string resourceName)
			{
				throw new HttpException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ScriptResourceHandler_UnknownResource, new object[]
				{
					resourceName
				}));
			}

			// Token: 0x04000511 RID: 1297
			private static readonly IDictionary _urlCache = Hashtable.Synchronized(new Hashtable(ListEqualityComparer.Instance));

			// Token: 0x04000512 RID: 1298
			private static readonly IDictionary _cultureCache = Hashtable.Synchronized(new Hashtable());

			// Token: 0x04000513 RID: 1299
			private static string _absoluteScriptResourceUrl;
		}
	}
}
