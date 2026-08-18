using System;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace System.Net.Configuration
{
	// Token: 0x0200064E RID: 1614
	internal sealed class DefaultProxySectionInternal
	{
		// Token: 0x060031FC RID: 12796 RVA: 0x000D5324 File Offset: 0x000D4324
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlPrincipal)]
		internal DefaultProxySectionInternal(DefaultProxySection section)
		{
			if (!section.Enabled)
			{
				return;
			}
			if (section.Proxy.AutoDetect == ProxyElement.AutoDetectValues.Unspecified && section.Proxy.ScriptLocation == null && string.IsNullOrEmpty(section.Module.Type) && section.Proxy.UseSystemDefault != ProxyElement.UseSystemDefaultValues.True && section.Proxy.ProxyAddress == null && section.Proxy.BypassOnLocal == ProxyElement.BypassOnLocalValues.Unspecified && section.BypassList.Count == 0)
			{
				if (section.Proxy.UseSystemDefault == ProxyElement.UseSystemDefaultValues.False)
				{
					this.webProxy = new EmptyWebProxy();
					return;
				}
				try
				{
					using (WindowsIdentity.Impersonate(IntPtr.Zero))
					{
						this.webProxy = new WebRequest.WebProxyWrapper(new WebProxy(true));
					}
					goto IL_2E3;
				}
				catch
				{
					throw;
				}
			}
			if (!string.IsNullOrEmpty(section.Module.Type))
			{
				Type type = Type.GetType(section.Module.Type, true, true);
				if ((type.Attributes & TypeAttributes.VisibilityMask) != TypeAttributes.Public)
				{
					throw new ConfigurationErrorsException(SR.GetString("net_config_proxy_module_not_public"));
				}
				if (!typeof(IWebProxy).IsAssignableFrom(type))
				{
					throw new InvalidCastException(SR.GetString("net_invalid_cast", new object[]
					{
						type.FullName,
						"IWebProxy"
					}));
				}
				this.webProxy = (IWebProxy)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, new object[0], CultureInfo.InvariantCulture);
			}
			else
			{
				if (section.Proxy.UseSystemDefault == ProxyElement.UseSystemDefaultValues.True && section.Proxy.AutoDetect == ProxyElement.AutoDetectValues.Unspecified && section.Proxy.ScriptLocation == null)
				{
					try
					{
						using (WindowsIdentity.Impersonate(IntPtr.Zero))
						{
							this.webProxy = new WebProxy(false);
						}
						goto IL_1DE;
					}
					catch
					{
						throw;
					}
				}
				this.webProxy = new WebProxy();
			}
			IL_1DE:
			WebProxy webProxy = this.webProxy as WebProxy;
			if (webProxy != null)
			{
				if (section.Proxy.AutoDetect != ProxyElement.AutoDetectValues.Unspecified)
				{
					webProxy.AutoDetect = (section.Proxy.AutoDetect == ProxyElement.AutoDetectValues.True);
				}
				if (section.Proxy.ScriptLocation != null)
				{
					webProxy.ScriptLocation = section.Proxy.ScriptLocation;
				}
				if (section.Proxy.BypassOnLocal != ProxyElement.BypassOnLocalValues.Unspecified)
				{
					webProxy.BypassProxyOnLocal = (section.Proxy.BypassOnLocal == ProxyElement.BypassOnLocalValues.True);
				}
				if (section.Proxy.ProxyAddress != null)
				{
					webProxy.Address = section.Proxy.ProxyAddress;
				}
				int count = section.BypassList.Count;
				if (count > 0)
				{
					string[] array = new string[section.BypassList.Count];
					for (int i = 0; i < count; i++)
					{
						array[i] = section.BypassList[i].Address;
					}
					webProxy.BypassList = array;
				}
				if (section.Module.Type == null)
				{
					this.webProxy = new WebRequest.WebProxyWrapper(webProxy);
				}
			}
			IL_2E3:
			if (this.webProxy != null && section.UseDefaultCredentials)
			{
				this.webProxy.Credentials = SystemNetworkCredential.defaultCredential;
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x060031FD RID: 12797 RVA: 0x000D5668 File Offset: 0x000D4668
		internal static object ClassSyncObject
		{
			get
			{
				if (DefaultProxySectionInternal.classSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange(ref DefaultProxySectionInternal.classSyncObject, value, null);
				}
				return DefaultProxySectionInternal.classSyncObject;
			}
		}

		// Token: 0x060031FE RID: 12798 RVA: 0x000D5694 File Offset: 0x000D4694
		internal static DefaultProxySectionInternal GetSection()
		{
			DefaultProxySectionInternal result;
			lock (DefaultProxySectionInternal.ClassSyncObject)
			{
				DefaultProxySection defaultProxySection = PrivilegedConfigurationManager.GetSection(ConfigurationStrings.DefaultProxySectionPath) as DefaultProxySection;
				if (defaultProxySection == null)
				{
					result = null;
				}
				else
				{
					try
					{
						result = new DefaultProxySectionInternal(defaultProxySection);
					}
					catch (Exception ex)
					{
						if (NclUtilities.IsFatal(ex))
						{
							throw;
						}
						throw new ConfigurationErrorsException(SR.GetString("net_config_proxy"), ex);
					}
					catch
					{
						throw new ConfigurationErrorsException(SR.GetString("net_config_proxy"), new Exception(SR.GetString("net_nonClsCompliantException")));
					}
				}
			}
			return result;
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x060031FF RID: 12799 RVA: 0x000D573C File Offset: 0x000D473C
		internal IWebProxy WebProxy
		{
			get
			{
				return this.webProxy;
			}
		}

		// Token: 0x04002EFE RID: 12030
		private IWebProxy webProxy;

		// Token: 0x04002EFF RID: 12031
		private static object classSyncObject;
	}
}
