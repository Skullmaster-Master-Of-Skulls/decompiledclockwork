using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security;
using System.Web.Caching;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Profile;
using System.Web.Resources;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;

namespace System.Web.Script.Services
{
	// Token: 0x020000F8 RID: 248
	internal class WebServiceData : JavaScriptTypeResolver
	{
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x0002BFA0 File Offset: 0x0002A1A0
		internal JavaScriptSerializer Serializer
		{
			get
			{
				return this._serializer;
			}
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0002BFA8 File Offset: 0x0002A1A8
		private static WebServiceData GetApplicationService(string appRelativePath)
		{
			int num = appRelativePath.LastIndexOf('/');
			if (num == 1)
			{
				string fileName = Path.GetFileName(appRelativePath);
				if (fileName.Equals("Profile_JSON_AppService.axd", StringComparison.OrdinalIgnoreCase))
				{
					return new WebServiceData(typeof(ProfileService), false);
				}
				if (fileName.Equals("Authentication_JSON_AppService.axd", StringComparison.OrdinalIgnoreCase))
				{
					return new WebServiceData(typeof(AuthenticationService), false);
				}
				if (fileName.Equals("Role_JSON_AppService.axd", StringComparison.OrdinalIgnoreCase))
				{
					return new WebServiceData(typeof(RoleService), false);
				}
			}
			return null;
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0002C027 File Offset: 0x0002A227
		internal static WebServiceData GetWebServiceData(HttpContext context, string virtualPath)
		{
			return WebServiceData.GetWebServiceData(context, virtualPath, true, false, false);
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0002C033 File Offset: 0x0002A233
		private static string GetCacheKey(string virtualPath)
		{
			return "System.Web.Script.Services.WebServiceData:" + virtualPath;
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0002C040 File Offset: 0x0002A240
		internal static WebServiceData GetWebServiceData(HttpContext context, string virtualPath, bool failIfNoData, bool pageMethods)
		{
			return WebServiceData.GetWebServiceData(context, virtualPath, failIfNoData, pageMethods, false);
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0002C04C File Offset: 0x0002A24C
		[SecuritySafeCritical]
		internal static WebServiceData GetWebServiceData(HttpContext context, string virtualPath, bool failIfNoData, bool pageMethods, bool inlineScript)
		{
			virtualPath = VirtualPathUtility.ToAbsolute(virtualPath);
			string cacheKey = WebServiceData.GetCacheKey(virtualPath);
			WebServiceData webServiceData = context.Cache[cacheKey] as WebServiceData;
			if (webServiceData == null)
			{
				if (HostingEnvironment.VirtualPathProvider.FileExists(virtualPath))
				{
					Type type = null;
					try
					{
						type = BuildManager.GetCompiledType(virtualPath);
						if (type == null)
						{
							object obj = BuildManager.CreateInstanceFromVirtualPath(virtualPath, typeof(Page));
							if (obj != null)
							{
								type = obj.GetType();
							}
						}
					}
					catch (SecurityException)
					{
					}
					if (type != null)
					{
						webServiceData = new WebServiceData(type, pageMethods);
						BuildDependencySet cachedBuildDependencySet = BuildManager.GetCachedBuildDependencySet(context, virtualPath);
						if (cachedBuildDependencySet != null)
						{
							CacheDependency cacheDependency = HostingEnvironment.VirtualPathProvider.GetCacheDependency(virtualPath, cachedBuildDependencySet.VirtualPaths, DateTime.Now);
							context.Cache.Insert(cacheKey, webServiceData, cacheDependency);
						}
					}
				}
				else if (virtualPath.EndsWith("_AppService.axd", StringComparison.OrdinalIgnoreCase))
				{
					webServiceData = WebServiceData.GetApplicationService(context.Request.AppRelativeCurrentExecutionFilePath);
					if (webServiceData != null)
					{
						context.Cache.Insert(cacheKey, webServiceData);
					}
				}
			}
			if (webServiceData != null)
			{
				return webServiceData;
			}
			if (!failIfNoData)
			{
				return null;
			}
			if (inlineScript)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.WebService_NoWebServiceDataInlineScript, new object[]
				{
					virtualPath
				}));
			}
			throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.WebService_NoWebServiceData, new object[]
			{
				virtualPath
			}));
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0002C194 File Offset: 0x0002A394
		internal WebServiceData()
		{
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0002C1A8 File Offset: 0x0002A3A8
		private WebServiceData(WebServiceTypeData typeData)
		{
			this._typeData = typeData;
			this._serializer = new JavaScriptSerializer(this);
			ScriptingJsonSerializationSection.ApplicationSettings applicationSettings = new ScriptingJsonSerializationSection.ApplicationSettings();
			this._serializer.MaxJsonLength = applicationSettings.MaxJsonLimit;
			this._serializer.RecursionLimit = applicationSettings.RecursionLimit;
			this._serializer.RegisterConverters(applicationSettings.Converters);
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0002C214 File Offset: 0x0002A414
		internal WebServiceData(Type type, bool pageMethods) : this(new WebServiceTypeData(type.Name, type.Namespace, type))
		{
			this._pageMethods = pageMethods;
			if (!this._pageMethods)
			{
				object[] customAttributes = type.GetCustomAttributes(typeof(ScriptServiceAttribute), true);
				if (customAttributes.Length == 0)
				{
					throw new InvalidOperationException(AtlasWeb.WebService_NoScriptServiceAttribute);
				}
			}
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0002C269 File Offset: 0x0002A469
		internal WebServiceData(WebServiceTypeData typeData, Dictionary<string, WebServiceMethodData> methods) : this(typeData)
		{
			this._methods = methods;
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0002C27C File Offset: 0x0002A47C
		private void AddMethod(Dictionary<string, WebServiceMethodData> methods, MethodInfo method)
		{
			object[] customAttributes = method.GetCustomAttributes(typeof(WebMethodAttribute), true);
			if (customAttributes.Length == 0)
			{
				return;
			}
			ScriptMethodAttribute scriptMethodAttribute = null;
			object[] customAttributes2 = method.GetCustomAttributes(typeof(ScriptMethodAttribute), true);
			if (customAttributes2.Length != 0)
			{
				scriptMethodAttribute = (ScriptMethodAttribute)customAttributes2[0];
			}
			WebServiceMethodData webServiceMethodData = new WebServiceMethodData(this, method, (WebMethodAttribute)customAttributes[0], scriptMethodAttribute);
			methods[webServiceMethodData.MethodName] = webServiceMethodData;
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0002C2E0 File Offset: 0x0002A4E0
		private void EnsureMethods()
		{
			if (this._methods != null || this._typeData.Type == null)
			{
				return;
			}
			lock (this)
			{
				List<Type> list = new List<Type>();
				Type type = this._typeData.Type;
				list.Add(type);
				while (type.BaseType != null)
				{
					type = type.BaseType;
					list.Add(type);
				}
				Dictionary<string, WebServiceMethodData> methods = new Dictionary<string, WebServiceMethodData>(StringComparer.OrdinalIgnoreCase);
				BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Public;
				if (this._pageMethods)
				{
					bindingFlags |= BindingFlags.Static;
				}
				else
				{
					bindingFlags |= BindingFlags.Instance;
				}
				for (int i = list.Count - 1; i >= 0; i--)
				{
					MethodInfo[] methods2 = list[i].GetMethods(bindingFlags);
					foreach (MethodInfo method in methods2)
					{
						this.AddMethod(methods, method);
					}
				}
				this._methods = methods;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x0002C3E8 File Offset: 0x0002A5E8
		internal WebServiceTypeData TypeData
		{
			get
			{
				return this._typeData;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x0002C3F0 File Offset: 0x0002A5F0
		internal ICollection<WebServiceMethodData> MethodDatas
		{
			get
			{
				this.EnsureMethods();
				return this._methods.Values;
			}
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0002C403 File Offset: 0x0002A603
		internal void ClearProcessedTypes()
		{
			this._processedTypes = null;
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x0002C40C File Offset: 0x0002A60C
		internal void Initialize(WebServiceTypeData typeData, Dictionary<string, WebServiceMethodData> methods)
		{
			Dictionary<string, WebServiceTypeData> clientTypesDictionary = new Dictionary<string, WebServiceTypeData>();
			this._clientTypesDictionary = clientTypesDictionary;
			Dictionary<string, WebServiceEnumData> enumTypesDictionary = new Dictionary<string, WebServiceEnumData>();
			this._enumTypesDictionary = enumTypesDictionary;
			this._processedTypes = new Hashtable();
			this._clientTypesProcessed = true;
			this._clientTypeNameDictionary = new Dictionary<Type, string>();
			this._typeData = typeData;
			this._methods = methods;
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x0002C460 File Offset: 0x0002A660
		internal WebServiceMethodData GetMethodData(string methodName)
		{
			this.EnsureMethods();
			WebServiceMethodData result = null;
			if (!this._methods.TryGetValue(methodName, out result))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.WebService_UnknownWebMethod, new object[]
				{
					methodName
				}), "methodName");
			}
			this.EnsureClientTypesProcessed();
			return result;
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x0002C4B0 File Offset: 0x0002A6B0
		private void EnsureClientTypesProcessed()
		{
			if (this._clientTypesProcessed)
			{
				return;
			}
			lock (this)
			{
				if (!this._clientTypesProcessed)
				{
					this.ProcessClientTypes();
				}
			}
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0002C500 File Offset: 0x0002A700
		private void ProcessClientTypes()
		{
			this._clientTypesDictionary = new Dictionary<string, WebServiceTypeData>();
			this._enumTypesDictionary = new Dictionary<string, WebServiceEnumData>();
			this._clientTypeNameDictionary = new Dictionary<Type, string>();
			try
			{
				this._processedTypes = new Hashtable();
				this.ProcessIncludeAttributes((GenerateScriptTypeAttribute[])this._typeData.Type.GetCustomAttributes(typeof(GenerateScriptTypeAttribute), true));
				foreach (WebServiceMethodData webServiceMethodData in this.MethodDatas)
				{
					this.ProcessIncludeAttributes((GenerateScriptTypeAttribute[])webServiceMethodData.MethodInfo.GetCustomAttributes(typeof(GenerateScriptTypeAttribute), true));
					foreach (WebServiceParameterData webServiceParameterData in webServiceMethodData.ParameterDatas)
					{
						this.ProcessClientType(webServiceParameterData.ParameterInfo.ParameterType);
					}
					if (!webServiceMethodData.UseXmlResponse)
					{
						this.ProcessClientType(webServiceMethodData.ReturnType);
					}
				}
				this._clientTypesProcessed = true;
			}
			catch
			{
				this._clientTypesDictionary = null;
				this._enumTypesDictionary = null;
				this._clientTypeNameDictionary = null;
				throw;
			}
			finally
			{
				this._processedTypes = null;
			}
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0002C654 File Offset: 0x0002A854
		private void ProcessIncludeAttributes(GenerateScriptTypeAttribute[] attributes)
		{
			foreach (GenerateScriptTypeAttribute generateScriptTypeAttribute in attributes)
			{
				if (!string.IsNullOrEmpty(generateScriptTypeAttribute.ScriptTypeId))
				{
					this._typeResolverSpecials[generateScriptTypeAttribute.Type.FullName] = generateScriptTypeAttribute.ScriptTypeId;
				}
				Type type = generateScriptTypeAttribute.Type;
				if (type.IsPrimitive || type == typeof(object) || type == typeof(string) || type == typeof(DateTime) || type == typeof(Guid) || typeof(IEnumerable).IsAssignableFrom(type) || typeof(IDictionary).IsAssignableFrom(type) || (type.IsGenericType && type.GetGenericArguments().Length > 1) || !System.Web.Script.Serialization.ObjectConverter.IsClientInstantiatableType(type, this._serializer))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.WebService_InvalidGenerateScriptType, new object[]
					{
						type.FullName
					}));
				}
				this.ProcessClientType(type, true);
			}
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0002C76F File Offset: 0x0002A96F
		private void ProcessClientType(Type t)
		{
			this.ProcessClientType(t, false, false);
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0002C77A File Offset: 0x0002A97A
		private void ProcessClientType(Type t, bool force)
		{
			this.ProcessClientType(t, force, false);
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0002C788 File Offset: 0x0002A988
		internal void ProcessClientType(Type t, bool force, bool isWCF)
		{
			if (!force && this._processedTypes.Contains(t))
			{
				return;
			}
			this._processedTypes[t] = null;
			if (t.IsEnum)
			{
				WebServiceEnumData webServiceEnumData;
				if (isWCF)
				{
					webServiceEnumData = (WebServiceEnumData)WebServiceTypeData.GetWebServiceTypeData(t);
				}
				else
				{
					webServiceEnumData = new WebServiceEnumData(t.Name, t.Namespace, t, Enum.GetNames(t), Enum.GetValues(t), Enum.GetUnderlyingType(t) == typeof(ulong));
				}
				this._enumTypesDictionary[this.GetTypeStringRepresentation(webServiceEnumData.TypeName, false)] = webServiceEnumData;
				return;
			}
			if (t.IsGenericType)
			{
				if (isWCF)
				{
					this.ProcessKnownTypes(t);
					return;
				}
				Type[] genericArguments = t.GetGenericArguments();
				if (genericArguments.Length > 1)
				{
					return;
				}
				this.ProcessClientType(genericArguments[0], false, isWCF);
				return;
			}
			else
			{
				if (t.IsArray)
				{
					this.ProcessClientType(t.GetElementType(), false, isWCF);
					return;
				}
				if (t.IsPrimitive || t == typeof(object) || t == typeof(string) || t == typeof(DateTime) || t == typeof(void) || t == typeof(decimal) || t == typeof(Guid) || typeof(IEnumerable).IsAssignableFrom(t) || typeof(IDictionary).IsAssignableFrom(t) || (!isWCF && !System.Web.Script.Serialization.ObjectConverter.IsClientInstantiatableType(t, this._serializer)))
				{
					return;
				}
				if (isWCF)
				{
					this.ProcessKnownTypes(t);
					return;
				}
				string typeStringRepresentation = this.GetTypeStringRepresentation(t.FullName, false);
				this._clientTypesDictionary[typeStringRepresentation] = new WebServiceTypeData(t.Name, t.Namespace, t);
				this._clientTypeNameDictionary[t] = typeStringRepresentation;
				return;
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0002C958 File Offset: 0x0002AB58
		private void ProcessKnownTypes(Type t)
		{
			WebServiceTypeData webServiceTypeData = WebServiceTypeData.GetWebServiceTypeData(t);
			bool flag = false;
			if (webServiceTypeData == null)
			{
				return;
			}
			if (!typeof(IEnumerable).IsAssignableFrom(t) && !typeof(IDictionary).IsAssignableFrom(t))
			{
				this._clientTypeNameDictionary[t] = this.GetTypeStringRepresentation(webServiceTypeData.TypeName);
				flag = this.ProcessTypeData(webServiceTypeData);
			}
			if (!flag)
			{
				IList<WebServiceTypeData> knownTypes = WebServiceTypeData.GetKnownTypes(t, webServiceTypeData);
				foreach (WebServiceTypeData typeData in knownTypes)
				{
					this.ProcessTypeData(typeData);
				}
			}
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0002CA00 File Offset: 0x0002AC00
		private bool ProcessTypeData(WebServiceTypeData typeData)
		{
			string typeStringRepresentation = this.GetTypeStringRepresentation(typeData.TypeName);
			bool result = true;
			if (typeData is WebServiceEnumData)
			{
				if (!this._enumTypesDictionary.ContainsKey(typeStringRepresentation))
				{
					this._enumTypesDictionary[typeStringRepresentation] = (WebServiceEnumData)typeData;
					result = false;
				}
			}
			else if (!this._clientTypesDictionary.ContainsKey(typeStringRepresentation))
			{
				this._clientTypesDictionary[typeStringRepresentation] = typeData;
				result = false;
			}
			return result;
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x0002CA66 File Offset: 0x0002AC66
		internal IEnumerable<WebServiceTypeData> ClientTypes
		{
			get
			{
				return this.ClientTypeDictionary.Values;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x0002CA73 File Offset: 0x0002AC73
		// (set) Token: 0x06000D2B RID: 3371 RVA: 0x0002CA81 File Offset: 0x0002AC81
		internal Dictionary<string, WebServiceTypeData> ClientTypeDictionary
		{
			get
			{
				this.EnsureClientTypesProcessed();
				return this._clientTypesDictionary;
			}
			set
			{
				this._clientTypesDictionary = value;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x0002CA8A File Offset: 0x0002AC8A
		internal Dictionary<Type, string> ClientTypeNameDictionary
		{
			get
			{
				this.EnsureClientTypesProcessed();
				return this._clientTypeNameDictionary;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x0002CA98 File Offset: 0x0002AC98
		internal IEnumerable<WebServiceEnumData> EnumTypes
		{
			get
			{
				this.EnsureClientTypesProcessed();
				return this._enumTypesDictionary.Values;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x0002CAAB File Offset: 0x0002ACAB
		// (set) Token: 0x06000D2F RID: 3375 RVA: 0x0002CAB9 File Offset: 0x0002ACB9
		internal Dictionary<string, WebServiceEnumData> EnumTypeDictionary
		{
			get
			{
				this.EnsureClientTypesProcessed();
				return this._enumTypesDictionary;
			}
			set
			{
				this._enumTypesDictionary = value;
			}
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0002CAC4 File Offset: 0x0002ACC4
		public override Type ResolveType(string id)
		{
			WebServiceTypeData webServiceTypeData = null;
			if (this.ClientTypeDictionary.TryGetValue(id, out webServiceTypeData) && webServiceTypeData != null)
			{
				return webServiceTypeData.Type;
			}
			return null;
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0002CAF0 File Offset: 0x0002ACF0
		public override string ResolveTypeId(Type type)
		{
			string typeStringRepresentation = this.GetTypeStringRepresentation(type.FullName);
			if (!this.ClientTypeDictionary.ContainsKey(typeStringRepresentation))
			{
				return null;
			}
			return typeStringRepresentation;
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0002CB1B File Offset: 0x0002AD1B
		internal string GetTypeStringRepresentation(string typeName)
		{
			return this.GetTypeStringRepresentation(typeName, true);
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0002CB28 File Offset: 0x0002AD28
		internal string GetTypeStringRepresentation(string typeName, bool ensure)
		{
			if (ensure)
			{
				this.EnsureClientTypesProcessed();
			}
			string result;
			if (this._typeResolverSpecials.TryGetValue(typeName, out result))
			{
				return result;
			}
			return typeName;
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x0002CB54 File Offset: 0x0002AD54
		internal string GetTypeStringRepresentation(WebServiceTypeData typeData)
		{
			string text = typeData.StringRepresentation;
			if (text == null)
			{
				text = this.GetTypeStringRepresentation(typeData.TypeName, true);
			}
			return text;
		}

		// Token: 0x0400039C RID: 924
		private WebServiceTypeData _typeData;

		// Token: 0x0400039D RID: 925
		private bool _pageMethods;

		// Token: 0x0400039E RID: 926
		private Dictionary<string, WebServiceMethodData> _methods;

		// Token: 0x0400039F RID: 927
		private Dictionary<string, string> _typeResolverSpecials = new Dictionary<string, string>();

		// Token: 0x040003A0 RID: 928
		private Dictionary<string, WebServiceTypeData> _clientTypesDictionary;

		// Token: 0x040003A1 RID: 929
		private Dictionary<Type, string> _clientTypeNameDictionary;

		// Token: 0x040003A2 RID: 930
		private Dictionary<string, WebServiceEnumData> _enumTypesDictionary;

		// Token: 0x040003A3 RID: 931
		private Hashtable _processedTypes;

		// Token: 0x040003A4 RID: 932
		private bool _clientTypesProcessed;

		// Token: 0x040003A5 RID: 933
		private JavaScriptSerializer _serializer;

		// Token: 0x040003A6 RID: 934
		internal const string _profileServiceFileName = "Profile_JSON_AppService.axd";

		// Token: 0x040003A7 RID: 935
		internal const string _authenticationServiceFileName = "Authentication_JSON_AppService.axd";

		// Token: 0x040003A8 RID: 936
		internal const string _roleServiceFileName = "Role_JSON_AppService.axd";
	}
}
