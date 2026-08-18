using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Management;
using System.Web.RegularExpressions;
using System.Web.Security.Cryptography;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Handlers
{
	// Token: 0x020001A4 RID: 420
	public sealed class AssemblyResourceLoader : IHttpHandler
	{
		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x00044210 File Offset: 0x00042410
		private static bool DebugMode
		{
			get
			{
				return HttpContext.Current.IsDebuggingEnabled;
			}
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x0004421C File Offset: 0x0004241C
		private static int CreateWebResourceUrlCacheKey(Assembly assembly, string resourceName, bool htmlEncoded, bool forSubstitution, bool enableCdn, bool debuggingEnabled, bool secureConnection)
		{
			int h = HashCodeCombiner.CombineHashCodes(assembly.GetHashCode(), resourceName.GetHashCode(), htmlEncoded.GetHashCode(), forSubstitution.GetHashCode(), enableCdn.GetHashCode());
			return HashCodeCombiner.CombineHashCodes(h, debuggingEnabled.GetHashCode(), secureConnection.GetHashCode());
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x00044264 File Offset: 0x00042464
		private static void EnsureHandlerExistenceChecked()
		{
			if (!AssemblyResourceLoader._handlerExistenceChecked)
			{
				HttpContext httpContext = HttpContext.Current;
				IIS7WorkerRequest iis7WorkerRequest = (httpContext != null) ? (httpContext.WorkerRequest as IIS7WorkerRequest) : null;
				string virtualPath = UrlPath.Combine(HttpRuntime.AppDomainAppVirtualPathString, "WebResource.axd");
				if (iis7WorkerRequest != null)
				{
					string text = iis7WorkerRequest.MapHandlerAndGetHandlerTypeString("GET", UrlPath.Combine(HttpRuntime.AppDomainAppVirtualPathString, "WebResource.axd"), false, true);
					if (!string.IsNullOrEmpty(text))
					{
						AssemblyResourceLoader._handlerExists = (typeof(AssemblyResourceLoader) == BuildManager.GetType(text, true, false));
					}
				}
				else
				{
					HttpHandlerAction httpHandlerAction = RuntimeConfig.GetConfig(VirtualPath.Create(virtualPath)).HttpHandlers.FindMapping("GET", VirtualPath.Create("WebResource.axd"));
					AssemblyResourceLoader._handlerExists = (httpHandlerAction != null && httpHandlerAction.TypeInternal == typeof(AssemblyResourceLoader));
				}
				AssemblyResourceLoader._handlerExistenceChecked = true;
			}
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x00044338 File Offset: 0x00042538
		private static string FormatWebResourceUrl(string assemblyName, string resourceName, long assemblyDate, bool htmlEncoded)
		{
			string text = Page.EncryptString(assemblyName + "|" + resourceName, Purpose.AssemblyResourceLoader_WebResourceUrl);
			if (htmlEncoded)
			{
				return string.Format(CultureInfo.InvariantCulture, "WebResource.axd?d={0}&amp;t={1}", new object[]
				{
					text,
					assemblyDate
				});
			}
			return string.Format(CultureInfo.InvariantCulture, "WebResource.axd?d={0}&t={1}", new object[]
			{
				text,
				assemblyDate
			});
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x000443A4 File Offset: 0x000425A4
		internal static Assembly GetAssemblyFromType(Type type)
		{
			Assembly assembly = (Assembly)AssemblyResourceLoader._typeAssemblyCache[type];
			if (assembly == null)
			{
				assembly = type.Assembly;
				AssemblyResourceLoader._typeAssemblyCache[type] = assembly;
			}
			return assembly;
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x000443E0 File Offset: 0x000425E0
		private static Pair GetAssemblyInfo(Assembly assembly)
		{
			Pair pair = AssemblyResourceLoader._assemblyInfoCache[assembly] as Pair;
			if (pair == null)
			{
				pair = AssemblyResourceLoader.GetAssemblyInfoWithAssertInternal(assembly);
				AssemblyResourceLoader._assemblyInfoCache[assembly] = pair;
			}
			return pair;
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x00044418 File Offset: 0x00042618
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private static Pair GetAssemblyInfoWithAssertInternal(Assembly assembly)
		{
			AssemblyName name = assembly.GetName();
			long ticks = File.GetLastWriteTime(new Uri(name.CodeBase).LocalPath).Ticks;
			return new Pair(name, ticks);
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x00044458 File Offset: 0x00042658
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private static VirtualPath GetDiskResourcePath(string resourceName)
		{
			VirtualPath scriptLocation = Util.GetScriptLocation();
			VirtualPath virtualPath = scriptLocation.SimpleCombine(resourceName);
			string path = virtualPath.MapPath();
			if (File.Exists(path))
			{
				return virtualPath;
			}
			return null;
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x00044485 File Offset: 0x00042685
		internal static string GetWebResourceUrl(Type type, string resourceName)
		{
			return AssemblyResourceLoader.GetWebResourceUrl(type, resourceName, false, null);
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x00044490 File Offset: 0x00042690
		internal static string GetWebResourceUrl(Type type, string resourceName, bool htmlEncoded)
		{
			return AssemblyResourceLoader.GetWebResourceUrl(type, resourceName, htmlEncoded, null);
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x0004449C File Offset: 0x0004269C
		internal static string GetWebResourceUrl(Type type, string resourceName, bool htmlEncoded, IScriptManager scriptManager)
		{
			bool enableCdn = scriptManager != null && scriptManager.EnableCdn;
			return AssemblyResourceLoader.GetWebResourceUrl(type, resourceName, htmlEncoded, scriptManager, enableCdn);
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x000444C0 File Offset: 0x000426C0
		internal static string GetWebResourceUrl(Type type, string resourceName, bool htmlEncoded, IScriptManager scriptManager, bool enableCdn)
		{
			Assembly assemblyFromType = AssemblyResourceLoader.GetAssemblyFromType(type);
			if (assemblyFromType == typeof(AssemblyResourceLoader).Assembly)
			{
				if (string.Equals(resourceName, "WebForms.js", StringComparison.Ordinal))
				{
					if (!AssemblyResourceLoader._webFormsScriptChecked)
					{
						AssemblyResourceLoader._webFormsScriptLocation = AssemblyResourceLoader.GetDiskResourcePath(resourceName);
						AssemblyResourceLoader._webFormsScriptChecked = true;
					}
					if (AssemblyResourceLoader._webFormsScriptLocation != null)
					{
						return AssemblyResourceLoader._webFormsScriptLocation.VirtualPathString;
					}
				}
				else if (string.Equals(resourceName, "WebUIValidation.js", StringComparison.Ordinal))
				{
					if (!AssemblyResourceLoader._webUIValidationScriptChecked)
					{
						AssemblyResourceLoader._webUIValidationScriptLocation = AssemblyResourceLoader.GetDiskResourcePath(resourceName);
						AssemblyResourceLoader._webUIValidationScriptChecked = true;
					}
					if (AssemblyResourceLoader._webUIValidationScriptLocation != null)
					{
						return AssemblyResourceLoader._webUIValidationScriptLocation.VirtualPathString;
					}
				}
				else if (string.Equals(resourceName, "SmartNav.htm", StringComparison.Ordinal))
				{
					if (!AssemblyResourceLoader._smartNavPageChecked)
					{
						AssemblyResourceLoader._smartNavPageLocation = AssemblyResourceLoader.GetDiskResourcePath(resourceName);
						AssemblyResourceLoader._smartNavPageChecked = true;
					}
					if (AssemblyResourceLoader._smartNavPageLocation != null)
					{
						return AssemblyResourceLoader._smartNavPageLocation.VirtualPathString;
					}
				}
				else if (string.Equals(resourceName, "SmartNav.js", StringComparison.Ordinal))
				{
					if (!AssemblyResourceLoader._smartNavScriptChecked)
					{
						AssemblyResourceLoader._smartNavScriptLocation = AssemblyResourceLoader.GetDiskResourcePath(resourceName);
						AssemblyResourceLoader._smartNavScriptChecked = true;
					}
					if (AssemblyResourceLoader._smartNavScriptLocation != null)
					{
						return AssemblyResourceLoader._smartNavScriptLocation.VirtualPathString;
					}
				}
			}
			return AssemblyResourceLoader.GetWebResourceUrlInternal(assemblyFromType, resourceName, htmlEncoded, false, scriptManager, enableCdn);
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x000445F8 File Offset: 0x000427F8
		private static WebResourceAttribute FindWebResourceAttribute(Assembly assembly, string resourceName)
		{
			object[] customAttributes = assembly.GetCustomAttributes(false);
			for (int i = 0; i < customAttributes.Length; i++)
			{
				WebResourceAttribute webResourceAttribute = customAttributes[i] as WebResourceAttribute;
				if (webResourceAttribute != null && string.Equals(webResourceAttribute.WebResource, resourceName, StringComparison.Ordinal))
				{
					return webResourceAttribute;
				}
			}
			return null;
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x0004463C File Offset: 0x0004283C
		internal static string FormatCdnUrl(Assembly assembly, string cdnPath)
		{
			AssemblyName assemblyName = new AssemblyName(assembly.FullName);
			return string.Format(CultureInfo.InvariantCulture, cdnPath, new object[]
			{
				HttpUtility.UrlEncode(assemblyName.Name),
				HttpUtility.UrlEncode(assemblyName.Version.ToString(4)),
				HttpUtility.UrlEncode(AssemblyUtil.GetAssemblyFileVersion(assembly))
			});
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x00044698 File Offset: 0x00042898
		private static string GetCdnPath(string resourceName, Assembly assembly, bool secureConnection)
		{
			string text = null;
			WebResourceAttribute webResourceAttribute = AssemblyResourceLoader.FindWebResourceAttribute(assembly, resourceName);
			if (webResourceAttribute != null)
			{
				text = (secureConnection ? webResourceAttribute.CdnPathSecureConnection : webResourceAttribute.CdnPath);
				if (!string.IsNullOrEmpty(text))
				{
					text = AssemblyResourceLoader.FormatCdnUrl(assembly, text);
				}
			}
			return text;
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x000446D8 File Offset: 0x000428D8
		internal static string GetWebResourceUrlInternal(Assembly assembly, string resourceName, bool htmlEncoded, bool forSubstitution, IScriptManager scriptManager)
		{
			bool enableCdn = scriptManager != null && scriptManager.EnableCdn;
			return AssemblyResourceLoader.GetWebResourceUrlInternal(assembly, resourceName, htmlEncoded, forSubstitution, scriptManager, enableCdn);
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x00044700 File Offset: 0x00042900
		internal static string GetWebResourceUrlInternal(Assembly assembly, string resourceName, bool htmlEncoded, bool forSubstitution, IScriptManager scriptManager, bool enableCdn)
		{
			AssemblyResourceLoader.EnsureHandlerExistenceChecked();
			if (!AssemblyResourceLoader._handlerExists)
			{
				throw new InvalidOperationException(SR.GetString("AssemblyResourceLoader_HandlerNotRegistered"));
			}
			Assembly assembly2 = assembly;
			string resourceName2 = resourceName;
			bool flag;
			bool flag2;
			if (scriptManager != null)
			{
				flag = scriptManager.IsDebuggingEnabled;
				flag2 = scriptManager.IsSecureConnection;
			}
			else
			{
				flag2 = (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.IsSecureConnection);
				flag = (HttpContext.Current != null && HttpContext.Current.IsDebuggingEnabled);
			}
			int num = AssemblyResourceLoader.CreateWebResourceUrlCacheKey(assembly, resourceName, htmlEncoded, forSubstitution, enableCdn, flag, flag2);
			string text = (string)AssemblyResourceLoader._urlCache[num];
			if (text == null)
			{
				IScriptResourceDefinition scriptResourceDefinition = null;
				if (ClientScriptManager._scriptResourceMapping != null)
				{
					scriptResourceDefinition = ClientScriptManager._scriptResourceMapping.GetDefinition(resourceName, assembly);
					if (scriptResourceDefinition != null)
					{
						if (!string.IsNullOrEmpty(scriptResourceDefinition.ResourceName))
						{
							resourceName2 = scriptResourceDefinition.ResourceName;
						}
						if (scriptResourceDefinition.ResourceAssembly != null)
						{
							assembly2 = scriptResourceDefinition.ResourceAssembly;
						}
					}
				}
				string text2 = null;
				if (scriptResourceDefinition != null)
				{
					if (enableCdn)
					{
						if (flag)
						{
							text2 = (flag2 ? scriptResourceDefinition.CdnDebugPathSecureConnection : scriptResourceDefinition.CdnDebugPath);
							if (string.IsNullOrEmpty(text2))
							{
								text2 = scriptResourceDefinition.DebugPath;
								if (string.IsNullOrEmpty(text2))
								{
									if (!flag2 || string.IsNullOrEmpty(scriptResourceDefinition.CdnDebugPath))
									{
										text2 = AssemblyResourceLoader.GetCdnPath(resourceName2, assembly2, flag2);
									}
									if (string.IsNullOrEmpty(text2))
									{
										text2 = scriptResourceDefinition.Path;
									}
								}
							}
						}
						else
						{
							text2 = (flag2 ? scriptResourceDefinition.CdnPathSecureConnection : scriptResourceDefinition.CdnPath);
							if (string.IsNullOrEmpty(text2))
							{
								if (!flag2 || string.IsNullOrEmpty(scriptResourceDefinition.CdnPath))
								{
									text2 = AssemblyResourceLoader.GetCdnPath(resourceName2, assembly2, flag2);
								}
								if (string.IsNullOrEmpty(text2))
								{
									text2 = scriptResourceDefinition.Path;
								}
							}
						}
					}
					else if (flag)
					{
						text2 = scriptResourceDefinition.DebugPath;
						if (string.IsNullOrEmpty(text2))
						{
							text2 = scriptResourceDefinition.Path;
						}
					}
					else
					{
						text2 = scriptResourceDefinition.Path;
					}
				}
				else if (enableCdn)
				{
					text2 = AssemblyResourceLoader.GetCdnPath(resourceName2, assembly2, flag2);
				}
				if (!string.IsNullOrEmpty(text2))
				{
					if (UrlPath.IsAppRelativePath(text2))
					{
						if (AssemblyResourceLoader._applicationRootPath == null)
						{
							text = VirtualPathUtility.ToAbsolute(text2);
						}
						else
						{
							text = VirtualPathUtility.ToAbsolute(text2, AssemblyResourceLoader._applicationRootPath);
						}
					}
					else
					{
						text = text2;
					}
					if (htmlEncoded)
					{
						text = HttpUtility.HtmlEncode(text);
					}
				}
				else
				{
					Pair assemblyInfo = AssemblyResourceLoader.GetAssemblyInfo(assembly2);
					AssemblyName assemblyName = (AssemblyName)assemblyInfo.First;
					long assemblyDate = (long)assemblyInfo.Second;
					string value = assemblyName.Version.ToString();
					string assemblyName2;
					if (assembly2.GlobalAssemblyCache)
					{
						if (assembly2 == HttpContext.SystemWebAssembly)
						{
							assemblyName2 = "s";
						}
						else
						{
							StringBuilder stringBuilder = new StringBuilder();
							stringBuilder.Append('f');
							stringBuilder.Append(assemblyName.Name);
							stringBuilder.Append(',');
							stringBuilder.Append(value);
							stringBuilder.Append(',');
							if (assemblyName.CultureInfo != null)
							{
								stringBuilder.Append(assemblyName.CultureInfo.ToString());
							}
							stringBuilder.Append(',');
							byte[] publicKeyToken = assemblyName.GetPublicKeyToken();
							for (int i = 0; i < publicKeyToken.Length; i++)
							{
								stringBuilder.Append(publicKeyToken[i].ToString("x2", CultureInfo.InvariantCulture));
							}
							assemblyName2 = stringBuilder.ToString();
						}
					}
					else
					{
						assemblyName2 = "p" + assemblyName.Name;
					}
					text = AssemblyResourceLoader.FormatWebResourceUrl(assemblyName2, resourceName2, assemblyDate, htmlEncoded);
					if (!forSubstitution && HttpRuntime.AppDomainAppVirtualPathString != null)
					{
						text = UrlPath.Combine(HttpRuntime.AppDomainAppVirtualPathString, text);
					}
				}
				AssemblyResourceLoader._urlCache[num] = text;
			}
			return text;
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x00044A98 File Offset: 0x00042C98
		internal static bool IsValidWebResourceRequest(HttpContext context)
		{
			AssemblyResourceLoader.EnsureHandlerExistenceChecked();
			if (!AssemblyResourceLoader._handlerExists)
			{
				return false;
			}
			string b = UrlPath.Combine(HttpRuntime.AppDomainAppVirtualPathString, "WebResource.axd");
			string path = context.Request.Path;
			return string.Equals(path, b, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x00044ADC File Offset: 0x00042CDC
		internal static void LogWebResourceFailure(string decryptedData, Exception exception)
		{
			string @string;
			if (decryptedData != null)
			{
				@string = SR.GetString("Webevent_msg_RuntimeErrorWebResourceFailure_ResourceMissing", new object[]
				{
					decryptedData
				});
			}
			else
			{
				@string = SR.GetString("Webevent_msg_RuntimeErrorWebResourceFailure_DecryptionError");
			}
			WebBaseEvent.RaiseSystemEvent(@string, null, 3012, 0, exception);
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x0600161B RID: 5659 RVA: 0x000097B7 File Offset: 0x000079B7
		bool IHttpHandler.IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x00044B20 File Offset: 0x00042D20
		void IHttpHandler.ProcessRequest(HttpContext context)
		{
			context.Response.Clear();
			Stream stream = null;
			string text = null;
			bool flag = false;
			Exception exception = null;
			try
			{
				NameValueCollection queryString = context.Request.QueryString;
				string text2 = queryString["d"];
				if (string.IsNullOrEmpty(text2))
				{
					throw new HttpException(404, SR.GetString("AssemblyResourceLoader_InvalidRequest"));
				}
				flag = true;
				text = Page.DecryptString(text2, Purpose.AssemblyResourceLoader_WebResourceUrl);
				int num = text.IndexOf('|');
				string text3 = text.Substring(0, num);
				if (string.IsNullOrEmpty(text3))
				{
					throw new HttpException(404, SR.GetString("AssemblyResourceLoader_AssemblyNotFound", new object[]
					{
						text3
					}));
				}
				string text4 = text.Substring(num + 1);
				if (string.IsNullOrEmpty(text4))
				{
					throw new HttpException(404, SR.GetString("AssemblyResourceLoader_ResourceNotFound", new object[]
					{
						text4
					}));
				}
				char c = text3[0];
				text3 = text3.Substring(1);
				Assembly assembly = null;
				if (c == 'f')
				{
					string[] array = text3.Split(new char[]
					{
						','
					});
					if (array.Length != 4)
					{
						throw new HttpException(404, SR.GetString("AssemblyResourceLoader_InvalidRequest"));
					}
					AssemblyName assemblyName = new AssemblyName();
					assemblyName.Name = array[0];
					assemblyName.Version = new Version(array[1]);
					string text5 = array[2];
					if (text5.Length > 0)
					{
						assemblyName.CultureInfo = new CultureInfo(text5);
					}
					else
					{
						assemblyName.CultureInfo = CultureInfo.InvariantCulture;
					}
					string text6 = array[3];
					byte[] array2 = new byte[text6.Length / 2];
					for (int i = 0; i < array2.Length; i++)
					{
						array2[i] = byte.Parse(text6.Substring(i * 2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
					}
					assemblyName.SetPublicKeyToken(array2);
					assembly = Assembly.Load(assemblyName);
				}
				else if (c == 's')
				{
					assembly = typeof(AssemblyResourceLoader).Assembly;
				}
				else
				{
					if (c != 'p')
					{
						throw new HttpException(404, SR.GetString("AssemblyResourceLoader_InvalidRequest"));
					}
					assembly = Assembly.Load(text3);
				}
				if (assembly == null)
				{
					throw new HttpException(404, SR.GetString("AssemblyResourceLoader_InvalidRequest"));
				}
				bool flag2 = false;
				bool flag3 = false;
				string text7 = string.Empty;
				int num2 = HashCodeCombiner.CombineHashCodes(assembly.GetHashCode(), text4.GetHashCode());
				Triplet triplet = (Triplet)AssemblyResourceLoader._webResourceCache[num2];
				if (triplet != null)
				{
					flag3 = (bool)triplet.First;
					text7 = (string)triplet.Second;
					flag2 = (bool)triplet.Third;
				}
				else
				{
					WebResourceAttribute webResourceAttribute = AssemblyResourceLoader.FindWebResourceAttribute(assembly, text4);
					if (webResourceAttribute != null)
					{
						text4 = webResourceAttribute.WebResource;
						flag3 = true;
						text7 = webResourceAttribute.ContentType;
						flag2 = webResourceAttribute.PerformSubstitution;
					}
					try
					{
						if (flag3)
						{
							flag3 = false;
							stream = assembly.GetManifestResourceStream(text4);
							flag3 = (stream != null);
						}
					}
					finally
					{
						Triplet triplet2 = new Triplet();
						triplet2.First = flag3;
						triplet2.Second = text7;
						triplet2.Third = flag2;
						AssemblyResourceLoader._webResourceCache[num2] = triplet2;
					}
				}
				if (flag3)
				{
					HttpCachePolicy cache = context.Response.Cache;
					cache.SetCacheability(HttpCacheability.Public);
					cache.VaryByParams["d"] = true;
					cache.SetOmitVaryStar(true);
					cache.SetExpires(DateTime.Now + TimeSpan.FromDays(365.0));
					cache.SetValidUntilExpires(true);
					Pair assemblyInfo = AssemblyResourceLoader.GetAssemblyInfo(assembly);
					cache.SetLastModified(new DateTime((long)assemblyInfo.Second));
					StreamReader streamReader = null;
					try
					{
						if (stream == null)
						{
							stream = assembly.GetManifestResourceStream(text4);
						}
						if (stream != null)
						{
							context.Response.ContentType = text7;
							if (flag2)
							{
								streamReader = new StreamReader(stream, true);
								string text8 = streamReader.ReadToEnd();
								MatchCollection matchCollection = AssemblyResourceLoader.webResourceRegex.Matches(text8);
								int num3 = 0;
								StringBuilder stringBuilder = new StringBuilder();
								foreach (object obj in matchCollection)
								{
									Match match = (Match)obj;
									stringBuilder.Append(text8.Substring(num3, match.Index - num3));
									Group group = match.Groups["resourceName"];
									if (group != null)
									{
										string text9 = group.ToString();
										if (text9.Length > 0)
										{
											if (string.Equals(text9, text4, StringComparison.Ordinal))
											{
												throw new HttpException(404, SR.GetString("AssemblyResourceLoader_NoCircularReferences", new object[]
												{
													text4
												}));
											}
											stringBuilder.Append(AssemblyResourceLoader.GetWebResourceUrlInternal(assembly, text9, false, true, null));
										}
									}
									num3 = match.Index + match.Length;
								}
								stringBuilder.Append(text8.Substring(num3, text8.Length - num3));
								StreamWriter streamWriter = new StreamWriter(context.Response.OutputStream, streamReader.CurrentEncoding);
								streamWriter.Write(stringBuilder.ToString());
								streamWriter.Flush();
							}
							else
							{
								byte[] buffer = new byte[1024];
								Stream outputStream = context.Response.OutputStream;
								int j = 1;
								while (j > 0)
								{
									j = stream.Read(buffer, 0, 1024);
									outputStream.Write(buffer, 0, j);
								}
								outputStream.Flush();
							}
						}
					}
					finally
					{
						if (streamReader != null)
						{
							streamReader.Close();
						}
						if (stream != null)
						{
							stream.Close();
						}
					}
				}
			}
			catch (Exception ex)
			{
				exception = ex;
				stream = null;
			}
			if (stream == null)
			{
				if (flag)
				{
					AssemblyResourceLoader.LogWebResourceFailure(text, exception);
				}
				throw new HttpException(404, SR.GetString("AssemblyResourceLoader_InvalidRequest"));
			}
			context.Response.IgnoreFurtherWrites();
		}

		// Token: 0x04001675 RID: 5749
		private const string _webResourceUrl = "WebResource.axd";

		// Token: 0x04001676 RID: 5750
		private static readonly Regex webResourceRegex = new WebResourceRegex();

		// Token: 0x04001677 RID: 5751
		private static IDictionary _urlCache = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04001678 RID: 5752
		private static IDictionary _assemblyInfoCache = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04001679 RID: 5753
		private static IDictionary _webResourceCache = Hashtable.Synchronized(new Hashtable());

		// Token: 0x0400167A RID: 5754
		private static IDictionary _typeAssemblyCache = Hashtable.Synchronized(new Hashtable());

		// Token: 0x0400167B RID: 5755
		private static bool _webFormsScriptChecked;

		// Token: 0x0400167C RID: 5756
		private static VirtualPath _webFormsScriptLocation;

		// Token: 0x0400167D RID: 5757
		private static bool _webUIValidationScriptChecked;

		// Token: 0x0400167E RID: 5758
		private static VirtualPath _webUIValidationScriptLocation;

		// Token: 0x0400167F RID: 5759
		private static bool _smartNavScriptChecked;

		// Token: 0x04001680 RID: 5760
		private static VirtualPath _smartNavScriptLocation;

		// Token: 0x04001681 RID: 5761
		private static bool _smartNavPageChecked;

		// Token: 0x04001682 RID: 5762
		private static VirtualPath _smartNavPageLocation;

		// Token: 0x04001683 RID: 5763
		private static bool _handlerExistenceChecked;

		// Token: 0x04001684 RID: 5764
		private static bool _handlerExists;

		// Token: 0x04001685 RID: 5765
		internal static string _applicationRootPath;
	}
}
