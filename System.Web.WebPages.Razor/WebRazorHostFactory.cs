using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.WebPages.Razor.Configuration;
using System.Web.WebPages.Razor.Resources;
using Microsoft.Internal.Web.Utils;

namespace System.Web.WebPages.Razor
{
	// Token: 0x0200000D RID: 13
	public class WebRazorHostFactory
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00002A87 File Offset: 0x00000C87
		public static WebPageRazorHost CreateDefaultHost(string virtualPath)
		{
			return WebRazorHostFactory.CreateDefaultHost(virtualPath, null);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002A90 File Offset: 0x00000C90
		public static WebPageRazorHost CreateDefaultHost(string virtualPath, string physicalPath)
		{
			return WebRazorHostFactory.CreateHostFromConfigCore(null, virtualPath, physicalPath);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002A9A File Offset: 0x00000C9A
		public static WebPageRazorHost CreateHostFromConfig(string virtualPath)
		{
			return WebRazorHostFactory.CreateHostFromConfig(virtualPath, null);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002AA4 File Offset: 0x00000CA4
		public static WebPageRazorHost CreateHostFromConfig(string virtualPath, string physicalPath)
		{
			if (string.IsNullOrEmpty(virtualPath))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, CommonResources.Argument_Cannot_Be_Null_Or_Empty, new object[]
				{
					"virtualPath"
				}), "virtualPath");
			}
			return WebRazorHostFactory.CreateHostFromConfigCore(WebRazorHostFactory.GetRazorSection(virtualPath), virtualPath, physicalPath);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002AF0 File Offset: 0x00000CF0
		public static WebPageRazorHost CreateHostFromConfig(RazorWebSectionGroup config, string virtualPath)
		{
			return WebRazorHostFactory.CreateHostFromConfig(config, virtualPath, null);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002AFC File Offset: 0x00000CFC
		public static WebPageRazorHost CreateHostFromConfig(RazorWebSectionGroup config, string virtualPath, string physicalPath)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			if (string.IsNullOrEmpty(virtualPath))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, CommonResources.Argument_Cannot_Be_Null_Or_Empty, new object[]
				{
					"virtualPath"
				}), "virtualPath");
			}
			return WebRazorHostFactory.CreateHostFromConfigCore(config, virtualPath, physicalPath);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002B54 File Offset: 0x00000D54
		internal static WebPageRazorHost CreateHostFromConfigCore(RazorWebSectionGroup config, string virtualPath, string physicalPath)
		{
			virtualPath = WebRazorHostFactory.EnsureAppRelative(virtualPath);
			WebPageRazorHost webPageRazorHost;
			if (virtualPath.StartsWith("~/App_Code", StringComparison.OrdinalIgnoreCase))
			{
				webPageRazorHost = new WebCodeRazorHost(virtualPath, physicalPath);
			}
			else
			{
				WebRazorHostFactory webRazorHostFactory = null;
				if (config != null && config.Host != null && !string.IsNullOrEmpty(config.Host.FactoryType))
				{
					Func<WebRazorHostFactory> orAdd = WebRazorHostFactory._factories.GetOrAdd(config.Host.FactoryType, new Func<string, Func<WebRazorHostFactory>>(WebRazorHostFactory.CreateFactory));
					webRazorHostFactory = orAdd();
				}
				webPageRazorHost = (webRazorHostFactory ?? new WebRazorHostFactory()).CreateHost(virtualPath, physicalPath);
				if (config != null && config.Pages != null)
				{
					WebRazorHostFactory.ApplyConfigurationToHost(config.Pages, webPageRazorHost);
				}
			}
			return webPageRazorHost;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002BF4 File Offset: 0x00000DF4
		private static Func<WebRazorHostFactory> CreateFactory(string typeName)
		{
			Type type = WebRazorHostFactory.TypeFactory(typeName);
			if (type == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, RazorWebResources.Could_Not_Locate_FactoryType, new object[]
				{
					typeName
				}));
			}
			return Expression.Lambda<Func<WebRazorHostFactory>>(Expression.New(type), new ParameterExpression[0]).Compile();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002C58 File Offset: 0x00000E58
		public static void ApplyConfigurationToHost(RazorPagesSection config, WebPageRazorHost host)
		{
			host.DefaultPageBaseClass = config.PageBaseType;
			foreach (string item in from ns in config.Namespaces.OfType<NamespaceInfo>()
			select ns.Namespace)
			{
				host.NamespaceImports.Add(item);
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public virtual WebPageRazorHost CreateHost(string virtualPath, string physicalPath)
		{
			return new WebPageRazorHost(virtualPath, physicalPath);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002CEC File Offset: 0x00000EEC
		internal static RazorWebSectionGroup GetRazorSection(string virtualPath)
		{
			return new RazorWebSectionGroup
			{
				Host = (HostSection)WebConfigurationManager.GetSection(HostSection.SectionName, virtualPath),
				Pages = (RazorPagesSection)WebConfigurationManager.GetSection(RazorPagesSection.SectionName, virtualPath)
			};
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002D2C File Offset: 0x00000F2C
		private static string EnsureAppRelative(string virtualPath)
		{
			if (HostingEnvironment.IsHosted)
			{
				virtualPath = VirtualPathUtility.ToAppRelative(virtualPath);
			}
			else if (virtualPath.StartsWith("/", StringComparison.Ordinal))
			{
				virtualPath = "~" + virtualPath;
			}
			else if (!virtualPath.StartsWith("~/", StringComparison.Ordinal))
			{
				virtualPath = "~/" + virtualPath;
			}
			return virtualPath;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002D83 File Offset: 0x00000F83
		private static Type DefaultTypeFactory(string typeName)
		{
			return BuildManager.GetType(typeName, false, false);
		}

		// Token: 0x04000022 RID: 34
		private static ConcurrentDictionary<string, Func<WebRazorHostFactory>> _factories = new ConcurrentDictionary<string, Func<WebRazorHostFactory>>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000023 RID: 35
		internal static Func<string, Type> TypeFactory = new Func<string, Type>(WebRazorHostFactory.DefaultTypeFactory);
	}
}
