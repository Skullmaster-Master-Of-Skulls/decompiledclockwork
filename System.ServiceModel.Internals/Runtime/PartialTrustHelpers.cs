using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime
{
	// Token: 0x02000023 RID: 35
	internal static class PartialTrustHelpers
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000113 RID: 275 RVA: 0x000057C0 File Offset: 0x000039C0
		internal static bool ShouldFlowSecurityContext
		{
			[SecurityCritical]
			get
			{
				return SecurityManager.CurrentThreadRequiresSecurityContextCapture();
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000057C8 File Offset: 0x000039C8
		[SecurityCritical]
		internal static bool IsInFullTrust()
		{
			if (!SecurityManager.CurrentThreadRequiresSecurityContextCapture())
			{
				return true;
			}
			bool result;
			try
			{
				PartialTrustHelpers.DemandForFullTrust();
				result = true;
			}
			catch (SecurityException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005800 File Offset: 0x00003A00
		[SecurityCritical]
		internal static SecurityContext CaptureSecurityContextNoIdentityFlow()
		{
			if (SecurityContext.IsWindowsIdentityFlowSuppressed())
			{
				return SecurityContext.Capture();
			}
			SecurityContext result;
			using (SecurityContext.SuppressFlowWindowsIdentity())
			{
				result = SecurityContext.Capture();
			}
			return result;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005848 File Offset: 0x00003A48
		[SecurityCritical]
		internal static bool IsTypeAptca(Type type)
		{
			Assembly assembly = type.Assembly;
			return PartialTrustHelpers.IsAssemblyAptca(assembly) || !PartialTrustHelpers.IsAssemblySigned(assembly);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000033BD File Offset: 0x000015BD
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void DemandForFullTrust()
		{
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000586F File Offset: 0x00003A6F
		[SecurityCritical]
		private static bool IsAssemblyAptca(Assembly assembly)
		{
			if (PartialTrustHelpers.aptca == null)
			{
				PartialTrustHelpers.aptca = typeof(AllowPartiallyTrustedCallersAttribute);
			}
			return assembly.GetCustomAttributes(PartialTrustHelpers.aptca, false).Length != 0;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000058A0 File Offset: 0x00003AA0
		[SecurityCritical]
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private static bool IsAssemblySigned(Assembly assembly)
		{
			byte[] publicKeyToken = assembly.GetName().GetPublicKeyToken();
			return publicKeyToken != null & publicKeyToken.Length != 0;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000058C3 File Offset: 0x00003AC3
		[SecurityCritical]
		internal static bool CheckAppDomainPermissions(PermissionSet permissions)
		{
			return AppDomain.CurrentDomain.IsHomogenous && permissions.IsSubsetOf(AppDomain.CurrentDomain.PermissionSet);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000058E4 File Offset: 0x00003AE4
		[SecurityCritical]
		internal static bool HasEtwPermissions()
		{
			PermissionSet permissions = new PermissionSet(PermissionState.Unrestricted);
			return PartialTrustHelpers.CheckAppDomainPermissions(permissions);
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000058FE File Offset: 0x00003AFE
		internal static bool AppDomainFullyTrusted
		{
			[SecuritySafeCritical]
			get
			{
				if (!PartialTrustHelpers.checkedForFullTrust)
				{
					PartialTrustHelpers.inFullTrust = AppDomain.CurrentDomain.IsFullyTrusted;
					PartialTrustHelpers.checkedForFullTrust = true;
				}
				return PartialTrustHelpers.inFullTrust;
			}
		}

		// Token: 0x0400008D RID: 141
		[SecurityCritical]
		private static Type aptca;

		// Token: 0x0400008E RID: 142
		[SecurityCritical]
		private static volatile bool checkedForFullTrust;

		// Token: 0x0400008F RID: 143
		[SecurityCritical]
		private static bool inFullTrust;
	}
}
