using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.WebPages.Resources;

namespace System.Web.WebPages.ApplicationParts
{
	// Token: 0x02000009 RID: 9
	internal class ApplicationPartRegistry
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00002A96 File Offset: 0x00000C96
		public ApplicationPartRegistry(DictionaryBasedVirtualPathFactory pathFactory)
		{
			this._applicationParts = new ConcurrentDictionary<IResourceAssembly, ApplicationPart>();
			this._registeredVirtualPaths = new ConcurrentDictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
			this._virtualPathFactory = pathFactory;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002AC0 File Offset: 0x00000CC0
		public IEnumerable<ApplicationPart> RegisteredParts
		{
			get
			{
				return this._applicationParts.Values;
			}
		}

		// Token: 0x17000013 RID: 19
		public ApplicationPart this[string name]
		{
			get
			{
				return this._applicationParts.Values.FirstOrDefault((ApplicationPart appPart) => appPart.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
			}
		}

		// Token: 0x17000014 RID: 20
		public ApplicationPart this[IResourceAssembly assembly]
		{
			get
			{
				ApplicationPart result;
				if (!this._applicationParts.TryGetValue(assembly, out result))
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002B44 File Offset: 0x00000D44
		public void Register(ApplicationPart applicationPart)
		{
			this.Register(applicationPart, null);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002B5C File Offset: 0x00000D5C
		internal void Register(ApplicationPart applicationPart, Func<object> registerPageAction)
		{
			if (this._applicationParts.ContainsKey(applicationPart.Assembly))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, WebPageResources.ApplicationPart_ModuleAlreadyRegistered, new object[]
				{
					applicationPart.Assembly
				}));
			}
			if (this._registeredVirtualPaths.ContainsKey(applicationPart.RootVirtualPath))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, WebPageResources.ApplicationPart_ModuleAlreadyRegisteredForVirtualPath, new object[]
				{
					applicationPart.RootVirtualPath
				}));
			}
			if (this._applicationParts.TryAdd(applicationPart.Assembly, applicationPart))
			{
				this._registeredVirtualPaths.TryAdd(applicationPart.RootVirtualPath, true);
				IEnumerable<Type> enumerable = from type in applicationPart.Assembly.GetTypes()
				where type.IsSubclassOf(ApplicationPartRegistry._webPageType)
				select type;
				foreach (Type webPageType in enumerable)
				{
					this.RegisterWebPage(applicationPart, webPageType, registerPageAction);
				}
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002C74 File Offset: 0x00000E74
		private void RegisterWebPage(ApplicationPart module, Type webPageType, Func<object> registerPageAction)
		{
			PageVirtualPathAttribute pageVirtualPathAttribute = webPageType.GetCustomAttributes(typeof(PageVirtualPathAttribute), false).Cast<PageVirtualPathAttribute>().SingleOrDefault<PageVirtualPathAttribute>();
			if (pageVirtualPathAttribute == null)
			{
				return;
			}
			string rootRelativeVirtualPath = ApplicationPartRegistry.GetRootRelativeVirtualPath(module.RootVirtualPath, pageVirtualPathAttribute.VirtualPath);
			Func<object> factory = registerPageAction ?? ApplicationPartRegistry.NewTypeInstance(webPageType);
			this._virtualPathFactory.RegisterPath(rootRelativeVirtualPath, factory);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002CCC File Offset: 0x00000ECC
		private static Func<object> NewTypeInstance(Type type)
		{
			return Expression.Lambda<Func<object>>(Expression.New(type), new ParameterExpression[0]).Compile();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002CE4 File Offset: 0x00000EE4
		internal static string GetRootRelativeVirtualPath(string rootVirtualPath, string pageVirtualPath)
		{
			string text = pageVirtualPath;
			if (text.StartsWith("~/", StringComparison.Ordinal))
			{
				text = text.Substring(2);
			}
			if (!rootVirtualPath.EndsWith("/", StringComparison.OrdinalIgnoreCase))
			{
				rootVirtualPath += "/";
			}
			return VirtualPathUtility.Combine(rootVirtualPath, text);
		}

		// Token: 0x04000012 RID: 18
		private static readonly Type _webPageType = typeof(WebPageRenderingBase);

		// Token: 0x04000013 RID: 19
		private readonly DictionaryBasedVirtualPathFactory _virtualPathFactory;

		// Token: 0x04000014 RID: 20
		private readonly ConcurrentDictionary<string, bool> _registeredVirtualPaths;

		// Token: 0x04000015 RID: 21
		private readonly ConcurrentDictionary<IResourceAssembly, ApplicationPart> _applicationParts;
	}
}
