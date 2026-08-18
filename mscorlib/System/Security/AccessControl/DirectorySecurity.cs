using System;
using System.IO;
using System.Security.Permissions;

namespace System.Security.AccessControl
{
	// Token: 0x02000928 RID: 2344
	public sealed class DirectorySecurity : FileSystemSecurity
	{
		// Token: 0x060054A7 RID: 21671 RVA: 0x00132AAF File Offset: 0x00131AAF
		public DirectorySecurity() : base(true)
		{
		}

		// Token: 0x060054A8 RID: 21672 RVA: 0x00132AB8 File Offset: 0x00131AB8
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public DirectorySecurity(string name, AccessControlSections includeSections) : base(true, name, includeSections, true)
		{
			string fullPathInternal = Path.GetFullPathInternal(name);
			new FileIOPermission(FileIOPermissionAccess.NoAccess, AccessControlActions.View, fullPathInternal).Demand();
		}
	}
}
