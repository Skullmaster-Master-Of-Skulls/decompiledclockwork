using System;
using System.Globalization;
using System.Reflection;
using System.Web.Hosting;

namespace System.Web.Administration
{
	// Token: 0x020001B4 RID: 436
	[Serializable]
	internal sealed class WebAdminConfigurationHelper : MarshalByRefObject, IRegisteredObject
	{
		// Token: 0x0600167B RID: 5755 RVA: 0x000474BC File Offset: 0x000456BC
		public WebAdminConfigurationHelper()
		{
			HostingEnvironment.RegisterObject(this);
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x0000298D File Offset: 0x00000B8D
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x000474CA File Offset: 0x000456CA
		public VirtualDirectory GetVirtualDirectory(string path)
		{
			if (HttpRuntime.NamedPermissionSet != null)
			{
				HttpRuntime.NamedPermissionSet.PermitOnly();
			}
			return HostingEnvironment.VirtualPathProvider.GetDirectory(path);
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x000474E8 File Offset: 0x000456E8
		public object CallMembershipProviderMethod(string methodName, object[] parameters, Type[] paramTypes)
		{
			Type type = typeof(HttpContext).Assembly.GetType("System.Web.Security.Membership");
			object obj = null;
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			MethodInfo method;
			if (paramTypes != null)
			{
				method = type.GetMethod(methodName, bindingAttr, null, paramTypes, null);
			}
			else
			{
				method = type.GetMethod(methodName, bindingAttr);
			}
			if (method != null)
			{
				if (HttpRuntime.NamedPermissionSet != null)
				{
					HttpRuntime.NamedPermissionSet.PermitOnly();
				}
				obj = method.Invoke(null, parameters);
			}
			object[] array = new object[parameters.Length + 1];
			array[0] = obj;
			int num = 1;
			for (int i = 0; i < parameters.Length; i++)
			{
				array[num++] = parameters[i];
			}
			return array;
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x0004758C File Offset: 0x0004578C
		public object GetMembershipProviderProperty(string propertyName)
		{
			Type type = typeof(HttpContext).Assembly.GetType("System.Web.Security.Membership");
			BindingFlags invokeAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty;
			if (HttpRuntime.NamedPermissionSet != null)
			{
				HttpRuntime.NamedPermissionSet.PermitOnly();
			}
			return type.InvokeMember(propertyName, invokeAttr, null, null, null, CultureInfo.InvariantCulture);
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x000475E0 File Offset: 0x000457E0
		public object CallRoleProviderMethod(string methodName, object[] parameters, Type[] paramTypes)
		{
			Type type = typeof(HttpContext).Assembly.GetType("System.Web.Security.Roles");
			object obj = null;
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			MethodInfo method;
			if (paramTypes != null)
			{
				method = type.GetMethod(methodName, bindingAttr, null, paramTypes, null);
			}
			else
			{
				method = type.GetMethod(methodName, bindingAttr);
			}
			if (method != null)
			{
				if (HttpRuntime.NamedPermissionSet != null)
				{
					HttpRuntime.NamedPermissionSet.PermitOnly();
				}
				obj = method.Invoke(null, parameters);
			}
			object[] array = new object[parameters.Length + 1];
			array[0] = obj;
			int num = 1;
			for (int i = 0; i < parameters.Length; i++)
			{
				array[num++] = parameters[i];
			}
			return array;
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x00047683 File Offset: 0x00045883
		void IRegisteredObject.Stop(bool immediate)
		{
			HostingEnvironment.UnregisterObject(this);
		}
	}
}
