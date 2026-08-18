using System;
using System.Globalization;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Web.Resources;
using System.Web.UI;

namespace System.Web.Script.Services
{
	// Token: 0x020000ED RID: 237
	public static class ProxyGenerator
	{
		// Token: 0x06000CDE RID: 3294 RVA: 0x0002B2DF File Offset: 0x000294DF
		public static string GetClientProxyScript(Type type, string path, bool debug)
		{
			return ProxyGenerator.GetClientProxyScript(type, path, debug, null);
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0002B2EC File Offset: 0x000294EC
		public static string GetClientProxyScript(Type type, string path, bool debug, ServiceEndpoint serviceEndpoint)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			ClientProxyGenerator clientProxyGenerator;
			WebServiceData webServiceData;
			if (ProxyGenerator.IsWebServiceType(type))
			{
				clientProxyGenerator = new WebServiceClientProxyGenerator(path, debug);
				webServiceData = new WebServiceData(type, false);
			}
			else if (ProxyGenerator.IsPageType(type))
			{
				clientProxyGenerator = new PageClientProxyGenerator(path, debug);
				webServiceData = new WebServiceData(type, true);
			}
			else
			{
				if (ProxyGenerator.IsWCFServiceType(type))
				{
					Assembly assembly = Assembly.Load("System.ServiceModel.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
					if (assembly != null)
					{
						Type type2 = assembly.GetType("System.ServiceModel.Description.WCFServiceClientProxyGenerator");
						if (type2 != null)
						{
							MethodInfo method = type2.GetMethod("GetClientProxyScript", BindingFlags.Static | BindingFlags.NonPublic);
							if (method != null)
							{
								return method.Invoke(null, new object[]
								{
									type,
									path,
									debug,
									serviceEndpoint
								}) as string;
							}
						}
					}
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ProxyGenerator_UnsupportedType, new object[]
					{
						type.FullName
					}));
				}
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ProxyGenerator_UnsupportedType, new object[]
				{
					type.FullName
				}));
			}
			return clientProxyGenerator.GetClientProxyScript(webServiceData);
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0002B41E File Offset: 0x0002961E
		private static bool IsPageType(Type type)
		{
			return typeof(Page).IsAssignableFrom(type);
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0002B430 File Offset: 0x00029630
		private static bool IsWCFServiceType(Type type)
		{
			object[] customAttributes = type.GetCustomAttributes(typeof(ServiceContractAttribute), true);
			return customAttributes.Length != 0;
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0002B454 File Offset: 0x00029654
		private static bool IsWebServiceType(Type type)
		{
			object[] customAttributes = type.GetCustomAttributes(typeof(ScriptServiceAttribute), true);
			return customAttributes.Length != 0;
		}

		// Token: 0x0400038E RID: 910
		private const string WCFProxyTypeName = "System.ServiceModel.Description.WCFServiceClientProxyGenerator";

		// Token: 0x0400038F RID: 911
		private const string WCFProxyMethodName = "GetClientProxyScript";
	}
}
