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
using System.Web.RegularExpressions;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Handlers
{
	// Token: 0x0200027C RID: 636
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class AssemblyResourceLoader : IHttpHandler
	{
		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x060020E4 RID: 8420 RVA: 0x0008F475 File Offset: 0x0008E475
		private static bool DebugMode
		{
			get
			{
				return HttpContext.Current.IsDebuggingEnabled;
			}
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x0008F484 File Offset: 0x0008E484
		private static int CreateWebResourceUrlCacheKey(Assembly assembly, string resourceName, bool htmlEncoded)
		{
			int h = HashCodeCombiner.CombineHashCodes(assembly.GetHashCode(), resourceName.GetHashCode());
			return HashCodeCombiner.CombineHashCodes(h, htmlEncoded.GetHashCode());
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x0008F4B0 File Offset: 0x0008E4B0
		private static void EnsureHandlerExistenceChecked()
		{
			if (!AssemblyResourceLoader._handlerExistenceChecked)
			{
				HttpContext httpContext = HttpContext.Current;
				IIS7WorkerRequest iis7WorkerRequest = (httpContext != null) ? (httpContext.WorkerRequest as IIS7WorkerRequest) : null;
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
					HttpHandlerAction httpHandlerAction = RuntimeConfig.GetConfig().HttpHandlers.FindMapping("GET", VirtualPath.Create("WebResource.axd"));
					AssemblyResourceLoader._handlerExists = (httpHandlerAction != null && httpHandlerAction.TypeInternal == typeof(AssemblyResourceLoader));
				}
				AssemblyResourceLoader._handlerExistenceChecked = true;
			}
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x0008F564 File Offset: 0x0008E564
		private static string FormatWebResourceUrl(string assemblyName, string resourceName, long assemblyDate, bool htmlEncoded)
		{
			string text = Page.EncryptString(assemblyName + "|" + resourceName);
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

		// Token: 0x060020E8 RID: 8424 RVA: 0x0008F5D0 File Offset: 0x0008E5D0
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

		// Token: 0x060020E9 RID: 8425 RVA: 0x0008F608 File Offset: 0x0008E608
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private static Pair GetAssemblyInfoWithAssertInternal(Assembly assembly)
		{
			AssemblyName name = assembly.GetName();
			long ticks = File.GetLastWriteTime(new Uri(name.CodeBase).LocalPath).Ticks;
			return new Pair(name, ticks);
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x0008F648 File Offset: 0x0008E648
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

		// Token: 0x060020EB RID: 8427 RVA: 0x0008F675 File Offset: 0x0008E675
		internal static string GetWebResourceUrl(Type type, string resourceName)
		{
			return AssemblyResourceLoader.GetWebResourceUrl(type, resourceName, false);
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x0008F680 File Offset: 0x0008E680
		internal static string GetWebResourceUrl(Type type, string resourceName, bool htmlEncoded)
		{
			Assembly assembly = (Assembly)AssemblyResourceLoader._typeAssemblyCache[type];
			if (assembly == null)
			{
				assembly = type.Assembly;
				AssemblyResourceLoader._typeAssemblyCache[type] = assembly;
			}
			if (assembly == typeof(AssemblyResourceLoader).Assembly)
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
			return UrlPath.Combine(HttpRuntime.AppDomainAppVirtualPathString, AssemblyResourceLoader.GetWebResourceUrlInternal(assembly, resourceName, htmlEncoded));
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x0008F7DC File Offset: 0x0008E7DC
		internal static string GetWebResourceUrlInternal(Assembly assembly, string resourceName, bool htmlEncoded)
		{
			AssemblyResourceLoader.EnsureHandlerExistenceChecked();
			if (!AssemblyResourceLoader._handlerExists)
			{
				throw new InvalidOperationException(SR.GetString("AssemblyResourceLoader_HandlerNotRegistered"));
			}
			Pair assemblyInfo = AssemblyResourceLoader.GetAssemblyInfo(assembly);
			AssemblyName assemblyName = (AssemblyName)assemblyInfo.First;
			long assemblyDate = (long)assemblyInfo.Second;
			string value = assemblyName.Version.ToString();
			int num = AssemblyResourceLoader.CreateWebResourceUrlCacheKey(assembly, resourceName, htmlEncoded);
			string text = (string)AssemblyResourceLoader._urlCache[num];
			if (text == null)
			{
				string assemblyName2;
				if (assembly.GlobalAssemblyCache)
				{
					if (assembly == HttpContext.SystemWebAssembly)
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
				text = AssemblyResourceLoader.FormatWebResourceUrl(assemblyName2, resourceName, assemblyDate, htmlEncoded);
				AssemblyResourceLoader._urlCache[num] = text;
			}
			return text;
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x0008F954 File Offset: 0x0008E954
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

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x060020EF RID: 8431 RVA: 0x0008F998 File Offset: 0x0008E998
		bool IHttpHandler.IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x0008F99C File Offset: 0x0008E99C
		void IHttpHandler.ProcessRequest(HttpContext context)
		{
			try
			{
				context.Response.Clear();
				NameValueCollection queryString = context.Request.QueryString;
				string text = queryString["d"];
				if (string.IsNullOrEmpty(text))
				{
					throw new HttpException(404, SR.GetString("AssemblyResourceLoader_InvalidRequest"));
				}
				string text2 = Page.DecryptString(text);
				int num = text2.IndexOf('|');
				string text3 = text2.Substring(0, num);
				if (string.IsNullOrEmpty(text3))
				{
					throw new HttpException(404, SR.GetString("AssemblyResourceLoader_AssemblyNotFound", new object[]
					{
						text3
					}));
				}
				string text4 = text2.Substring(num + 1);
				if (string.IsNullOrEmpty(text4))
				{
					throw new HttpException(404, SR.GetString("AssemblyResourceLoader_ResourceNotFound", new object[]
					{
						text4
					}));
				}
				char c = text3[0];
				text3 = text3.Substring(1);
				Assembly assembly;
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
				bool flag = false;
				bool flag2 = false;
				string text7 = string.Empty;
				if (assembly != null)
				{
					int num2 = HashCodeCombiner.CombineHashCodes(assembly.GetHashCode(), text4.GetHashCode());
					Triplet triplet = (Triplet)AssemblyResourceLoader._webResourceCache[num2];
					if (triplet != null)
					{
						flag2 = (bool)triplet.First;
						text7 = (string)triplet.Second;
						flag = (bool)triplet.Third;
					}
					else
					{
						object[] customAttributes = assembly.GetCustomAttributes(false);
						for (int j = 0; j < customAttributes.Length; j++)
						{
							WebResourceAttribute webResourceAttribute = customAttributes[j] as WebResourceAttribute;
							if (webResourceAttribute != null && string.Compare(webResourceAttribute.WebResource, text4, StringComparison.Ordinal) == 0)
							{
								text4 = webResourceAttribute.WebResource;
								flag2 = true;
								text7 = webResourceAttribute.ContentType;
								flag = webResourceAttribute.PerformSubstitution;
								break;
							}
						}
						Triplet triplet2 = new Triplet();
						triplet2.First = flag2;
						triplet2.Second = text7;
						triplet2.Third = flag;
						AssemblyResourceLoader._webResourceCache[num2] = triplet2;
					}
					if (flag2)
					{
						HttpCachePolicy cache = context.Response.Cache;
						cache.SetCacheability(HttpCacheability.Public);
						cache.VaryByParams["d"] = true;
						cache.SetOmitVaryStar(true);
						cache.SetExpires(DateTime.Now + TimeSpan.FromDays(365.0));
						cache.SetValidUntilExpires(true);
						Pair assemblyInfo = AssemblyResourceLoader.GetAssemblyInfo(assembly);
						cache.SetLastModified(new DateTime((long)assemblyInfo.Second));
						Stream stream = null;
						StreamReader streamReader = null;
						try
						{
							stream = assembly.GetManifestResourceStream(text4);
							if (stream != null)
							{
								context.Response.ContentType = text7;
								if (flag)
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
												stringBuilder.Append(AssemblyResourceLoader.GetWebResourceUrlInternal(assembly, text9, false));
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
									int k = 1;
									while (k > 0)
									{
										k = stream.Read(buffer, 0, 1024);
										outputStream.Write(buffer, 0, k);
									}
									outputStream.Flush();
								}
							}
							goto IL_58E;
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
					throw new HttpException(404, SR.GetString("AssemblyResourceLoader_InvalidRequest", new object[]
					{
						text3
					}));
				}
				IL_58E:
				context.Response.IgnoreFurtherWrites();
			}
			catch
			{
				throw new HttpException(404, SR.GetString("AssemblyResourceLoader_InvalidRequest"));
			}
		}

		// Token: 0x04001AD8 RID: 6872
		private const string _webResourceUrl = "WebResource.axd";

		// Token: 0x04001AD9 RID: 6873
		private static readonly Regex webResourceRegex = new WebResourceRegex();

		// Token: 0x04001ADA RID: 6874
		private static IDictionary _urlCache = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04001ADB RID: 6875
		private static IDictionary _assemblyInfoCache = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04001ADC RID: 6876
		private static IDictionary _webResourceCache = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04001ADD RID: 6877
		private static IDictionary _typeAssemblyCache = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04001ADE RID: 6878
		private static bool _webFormsScriptChecked;

		// Token: 0x04001ADF RID: 6879
		private static VirtualPath _webFormsScriptLocation;

		// Token: 0x04001AE0 RID: 6880
		private static bool _webUIValidationScriptChecked;

		// Token: 0x04001AE1 RID: 6881
		private static VirtualPath _webUIValidationScriptLocation;

		// Token: 0x04001AE2 RID: 6882
		private static bool _smartNavScriptChecked;

		// Token: 0x04001AE3 RID: 6883
		private static VirtualPath _smartNavScriptLocation;

		// Token: 0x04001AE4 RID: 6884
		private static bool _smartNavPageChecked;

		// Token: 0x04001AE5 RID: 6885
		private static VirtualPath _smartNavPageLocation;

		// Token: 0x04001AE6 RID: 6886
		private static bool _handlerExistenceChecked;

		// Token: 0x04001AE7 RID: 6887
		private static bool _handlerExists;
	}
}
