using System;
using System.Security;
using System.Security.Permissions;

namespace System.Web
{
	// Token: 0x020000D8 RID: 216
	internal static class InternalSecurityPermissions
	{
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06000E08 RID: 3592 RVA: 0x00027DA0 File Offset: 0x00025FA0
		internal static IStackWalk Unrestricted
		{
			get
			{
				if (InternalSecurityPermissions._unrestricted == null)
				{
					InternalSecurityPermissions._unrestricted = new PermissionSet(PermissionState.Unrestricted);
				}
				return InternalSecurityPermissions._unrestricted;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06000E09 RID: 3593 RVA: 0x00027DB9 File Offset: 0x00025FB9
		internal static IStackWalk UnmanagedCode
		{
			get
			{
				if (InternalSecurityPermissions._unmanagedCode == null)
				{
					InternalSecurityPermissions._unmanagedCode = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
				}
				return InternalSecurityPermissions._unmanagedCode;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x00027DD2 File Offset: 0x00025FD2
		internal static IStackWalk ControlPrincipal
		{
			get
			{
				if (InternalSecurityPermissions._controlPrincipal == null)
				{
					InternalSecurityPermissions._controlPrincipal = new SecurityPermission(SecurityPermissionFlag.ControlPrincipal);
				}
				return InternalSecurityPermissions._controlPrincipal;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x00027DEF File Offset: 0x00025FEF
		internal static IStackWalk Reflection
		{
			get
			{
				if (InternalSecurityPermissions._reflection == null)
				{
					InternalSecurityPermissions._reflection = new ReflectionPermission(ReflectionPermissionFlag.MemberAccess);
				}
				return InternalSecurityPermissions._reflection;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x00027E08 File Offset: 0x00026008
		internal static IStackWalk AppPathDiscovery
		{
			get
			{
				if (InternalSecurityPermissions._appPathDiscovery == null)
				{
					InternalSecurityPermissions._appPathDiscovery = new FileIOPermission(FileIOPermissionAccess.PathDiscovery, HttpRuntime.AppDomainAppPathInternal);
				}
				return InternalSecurityPermissions._appPathDiscovery;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x00027E26 File Offset: 0x00026026
		internal static IStackWalk ControlThread
		{
			get
			{
				if (InternalSecurityPermissions._controlThread == null)
				{
					InternalSecurityPermissions._controlThread = new SecurityPermission(SecurityPermissionFlag.ControlThread);
				}
				return InternalSecurityPermissions._controlThread;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x00027E40 File Offset: 0x00026040
		internal static IStackWalk AspNetHostingPermissionLevelLow
		{
			get
			{
				if (InternalSecurityPermissions._levelLow == null)
				{
					InternalSecurityPermissions._levelLow = new AspNetHostingPermission(AspNetHostingPermissionLevel.Low);
				}
				return InternalSecurityPermissions._levelLow;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06000E0F RID: 3599 RVA: 0x00027E5D File Offset: 0x0002605D
		internal static IStackWalk AspNetHostingPermissionLevelMedium
		{
			get
			{
				if (InternalSecurityPermissions._levelMedium == null)
				{
					InternalSecurityPermissions._levelMedium = new AspNetHostingPermission(AspNetHostingPermissionLevel.Medium);
				}
				return InternalSecurityPermissions._levelMedium;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x00027E7A File Offset: 0x0002607A
		internal static IStackWalk AspNetHostingPermissionLevelHigh
		{
			get
			{
				if (InternalSecurityPermissions._levelHigh == null)
				{
					InternalSecurityPermissions._levelHigh = new AspNetHostingPermission(AspNetHostingPermissionLevel.High);
				}
				return InternalSecurityPermissions._levelHigh;
			}
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x00027E97 File Offset: 0x00026097
		internal static IStackWalk FileReadAccess(string filename)
		{
			return new FileIOPermission(FileIOPermissionAccess.Read, filename);
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x00027EA0 File Offset: 0x000260A0
		internal static IStackWalk FileWriteAccess(string filename)
		{
			return new FileIOPermission(FileIOPermissionAccess.Write | FileIOPermissionAccess.Append, filename);
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00027EA9 File Offset: 0x000260A9
		internal static IStackWalk PathDiscovery(string path)
		{
			return new FileIOPermission(FileIOPermissionAccess.PathDiscovery, path);
		}

		// Token: 0x04000529 RID: 1321
		private static IStackWalk _unrestricted;

		// Token: 0x0400052A RID: 1322
		private static IStackWalk _unmanagedCode;

		// Token: 0x0400052B RID: 1323
		private static IStackWalk _controlPrincipal;

		// Token: 0x0400052C RID: 1324
		private static IStackWalk _reflection;

		// Token: 0x0400052D RID: 1325
		private static IStackWalk _appPathDiscovery;

		// Token: 0x0400052E RID: 1326
		private static IStackWalk _controlThread;

		// Token: 0x0400052F RID: 1327
		private static IStackWalk _levelLow;

		// Token: 0x04000530 RID: 1328
		private static IStackWalk _levelMedium;

		// Token: 0x04000531 RID: 1329
		private static IStackWalk _levelHigh;
	}
}
