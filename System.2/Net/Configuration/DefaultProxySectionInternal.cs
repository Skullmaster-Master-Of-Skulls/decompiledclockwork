using System;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace System.Net.Configuration
{
	// Token: 0x02000330 RID: 816
	internal sealed class DefaultProxySectionInternal
	{
		// Token: 0x06001D42 RID: 7490 RVA: 0x0008B38C File Offset: 0x0008958C
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
					new SecurityPermission(SecurityPermissionFlag.UnmanagedCode | SecurityPermissionFlag.ControlPrincipal).Assert();
					using (WindowsIdentity.Impersonate(IntPtr.Zero))
					{
						CodeAccessPermission.RevertAssert();
						this.webProxy = new WebRequest.WebProxyWrapper(new WebProxy(true));
					}
					goto IL_309;
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
						new SecurityPermission(SecurityPermissionFlag.UnmanagedCode | SecurityPermissionFlag.ControlPrincipal).Assert();
						using (WindowsIdentity.Impersonate(IntPtr.Zero))
						{
							CodeAccessPermission.RevertAssert();
							this.webProxy = new WebProxy(false);
						}
						goto IL_1FE;
					}
					catch
					{
						throw;
					}
				}
				this.webProxy = new WebProxy();
			}
			IL_1FE:
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
			IL_309:
			if (this.webProxy != null && section.UseDefaultCredentials)
			{
				this.webProxy.Credentials = SystemNetworkCredential.defaultCredential;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001D43 RID: 7491 RVA: 0x0008B6F8 File Offset: 0x000898F8
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

		// Token: 0x06001D44 RID: 7492 RVA: 0x0008B724 File Offset: 0x00089924
		internal static DefaultProxySectionInternal GetSection()
		{
			object obj = DefaultProxySectionInternal.ClassSyncObject;
			DefaultProxySectionInternal result;
			lock (obj)
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
				}
			}
			return result;
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001D45 RID: 7493 RVA: 0x0008B7A8 File Offset: 0x000899A8
		internal IWebProxy WebProxy
		{
			get
			{
				return this.webProxy;
			}
		}

		// Token: 0x04001C34 RID: 7220
		private IWebProxy webProxy;

		// Token: 0x04001C35 RID: 7221
		private static object classSyncObject;
	}
}
