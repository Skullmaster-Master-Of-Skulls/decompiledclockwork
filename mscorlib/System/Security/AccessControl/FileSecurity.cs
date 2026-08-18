using System;
using System.IO;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.Security.AccessControl
{
	// Token: 0x02000927 RID: 2343
	public sealed class FileSecurity : FileSystemSecurity
	{
		// Token: 0x060054A4 RID: 21668 RVA: 0x00132A53 File Offset: 0x00131A53
		public FileSecurity() : base(false)
		{
		}

		// Token: 0x060054A5 RID: 21669 RVA: 0x00132A5C File Offset: 0x00131A5C
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public FileSecurity(string fileName, AccessControlSections includeSections) : base(false, fileName, includeSections, false)
		{
			string fullPathInternal = Path.GetFullPathInternal(fileName);
			new FileIOPermission(FileIOPermissionAccess.NoAccess, AccessControlActions.View, fullPathInternal).Demand();
		}

		// Token: 0x060054A6 RID: 21670 RVA: 0x00132A87 File Offset: 0x00131A87
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		internal FileSecurity(SafeFileHandle handle, string fullPath, AccessControlSections includeSections) : base(false, handle, includeSections, false)
		{
			if (fullPath != null)
			{
				new FileIOPermission(FileIOPermissionAccess.NoAccess, AccessControlActions.View, fullPath).Demand();
				return;
			}
			new FileIOPermission(PermissionState.Unrestricted).Demand();
		}
	}
}
