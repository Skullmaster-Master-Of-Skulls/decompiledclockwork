using System;
using System.Reflection;
using System.Web.Security;

namespace System.ServiceModel.Activation
{
	// Token: 0x020005C4 RID: 1476
	internal static class SystemWebHelper
	{
		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x0600398D RID: 14733 RVA: 0x000DE75F File Offset: 0x000DC95F
		private static Type TypeOfRoles
		{
			get
			{
				if (SystemWebHelper.typeOfRoles == null)
				{
					SystemWebHelper.typeOfRoles = SystemWebHelper.GetSystemWebType("System.Web.Security.Roles");
				}
				return SystemWebHelper.typeOfRoles;
			}
		}

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x0600398E RID: 14734 RVA: 0x000DE782 File Offset: 0x000DC982
		private static Type TypeOfMembership
		{
			get
			{
				if (SystemWebHelper.typeOfMembership == null)
				{
					SystemWebHelper.typeOfMembership = SystemWebHelper.GetSystemWebType("System.Web.Security.Membership");
				}
				return SystemWebHelper.typeOfMembership;
			}
		}

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x0600398F RID: 14735 RVA: 0x000DE7A5 File Offset: 0x000DC9A5
		private static Type TypeOfWebContext
		{
			get
			{
				if (SystemWebHelper.typeOfWebContext == null)
				{
					SystemWebHelper.typeOfWebContext = SystemWebHelper.GetSystemWebType("System.Web.Configuration.WebContext");
				}
				return SystemWebHelper.typeOfWebContext;
			}
		}

		// Token: 0x06003990 RID: 14736 RVA: 0x000DE7C8 File Offset: 0x000DC9C8
		private static Type GetSystemWebType(string typeName)
		{
			return Type.GetType(typeName + ", System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", false);
		}

		// Token: 0x06003991 RID: 14737 RVA: 0x000DE7DC File Offset: 0x000DC9DC
		internal static RoleProvider GetDefaultRoleProvider()
		{
			if (SystemWebHelper.defaultRoleProviderSet)
			{
				return SystemWebHelper.defaultRoleProvider;
			}
			Type type = SystemWebHelper.TypeOfRoles;
			RoleProvider result = null;
			if (type != null)
			{
				try
				{
					PropertyInfo property = type.GetProperty("Enabled");
					if ((bool)property.GetValue(null, null))
					{
						PropertyInfo property2 = type.GetProperty("Provider");
						result = (property2.GetValue(null, null) as RoleProvider);
					}
				}
				catch (TargetInvocationException ex)
				{
					if (ex.InnerException != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex.InnerException);
					}
					throw;
				}
			}
			SystemWebHelper.defaultRoleProvider = result;
			SystemWebHelper.defaultRoleProviderSet = true;
			return result;
		}

		// Token: 0x06003992 RID: 14738 RVA: 0x000DE87C File Offset: 0x000DCA7C
		internal static RoleProvider GetRoleProvider(string roleProviderName)
		{
			Type type = SystemWebHelper.TypeOfRoles;
			if (type != null)
			{
				try
				{
					PropertyInfo property = type.GetProperty("Providers");
					object value = property.GetValue(null, null);
					PropertyInfo property2 = value.GetType().GetProperty("Item", new Type[]
					{
						typeof(string)
					});
					return (RoleProvider)property2.GetValue(value, new object[]
					{
						roleProviderName
					});
				}
				catch (TargetInvocationException ex)
				{
					if (ex.InnerException != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex.InnerException);
					}
					throw;
				}
			}
			return null;
		}

		// Token: 0x06003993 RID: 14739 RVA: 0x000DE920 File Offset: 0x000DCB20
		internal static MembershipProvider GetMembershipProvider()
		{
			Type type = SystemWebHelper.TypeOfMembership;
			if (type != null)
			{
				try
				{
					PropertyInfo property = type.GetProperty("Provider");
					return (MembershipProvider)property.GetValue(null, null);
				}
				catch (TargetInvocationException ex)
				{
					if (ex.InnerException != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex.InnerException);
					}
					throw;
				}
			}
			return null;
		}

		// Token: 0x06003994 RID: 14740 RVA: 0x000DE988 File Offset: 0x000DCB88
		internal static MembershipProvider GetMembershipProvider(string membershipProviderName)
		{
			Type type = SystemWebHelper.TypeOfMembership;
			if (type != null)
			{
				try
				{
					PropertyInfo property = type.GetProperty("Providers");
					object value = property.GetValue(null, null);
					PropertyInfo property2 = value.GetType().GetProperty("Item", new Type[]
					{
						typeof(string)
					});
					return (MembershipProvider)property2.GetValue(value, new object[]
					{
						membershipProviderName
					});
				}
				catch (TargetInvocationException ex)
				{
					if (ex.InnerException != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex.InnerException);
					}
					throw;
				}
			}
			return null;
		}

		// Token: 0x06003995 RID: 14741 RVA: 0x000DEA2C File Offset: 0x000DCC2C
		internal static bool IsWebConfigAboveApplication(object configHostingContext)
		{
			Type type = SystemWebHelper.TypeOfWebContext;
			if (configHostingContext == null || type == null || configHostingContext.GetType() != type)
			{
				return false;
			}
			bool result;
			try
			{
				PropertyInfo property = type.GetProperty("ApplicationLevel");
				result = ((int)property.GetValue(configHostingContext, null) == 10);
			}
			catch (TargetInvocationException ex)
			{
				if (ex.InnerException != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex.InnerException);
				}
				throw;
			}
			return result;
		}

		// Token: 0x040029F0 RID: 10736
		private static Type typeOfRoles;

		// Token: 0x040029F1 RID: 10737
		private static Type typeOfMembership;

		// Token: 0x040029F2 RID: 10738
		private static Type typeOfWebContext;

		// Token: 0x040029F3 RID: 10739
		private static bool defaultRoleProviderSet;

		// Token: 0x040029F4 RID: 10740
		private static RoleProvider defaultRoleProvider;
	}
}
