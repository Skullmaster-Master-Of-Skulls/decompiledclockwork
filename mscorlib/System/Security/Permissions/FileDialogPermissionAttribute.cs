using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x0200063C RID: 1596
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class FileDialogPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06003973 RID: 14707 RVA: 0x000C1FF2 File Offset: 0x000C0FF2
		public FileDialogPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06003974 RID: 14708 RVA: 0x000C1FFB File Offset: 0x000C0FFB
		// (set) Token: 0x06003975 RID: 14709 RVA: 0x000C200B File Offset: 0x000C100B
		public bool Open
		{
			get
			{
				return (this.m_access & FileDialogPermissionAccess.Open) != FileDialogPermissionAccess.None;
			}
			set
			{
				this.m_access = (value ? (this.m_access | FileDialogPermissionAccess.Open) : (this.m_access & ~FileDialogPermissionAccess.Open));
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06003976 RID: 14710 RVA: 0x000C2029 File Offset: 0x000C1029
		// (set) Token: 0x06003977 RID: 14711 RVA: 0x000C2039 File Offset: 0x000C1039
		public bool Save
		{
			get
			{
				return (this.m_access & FileDialogPermissionAccess.Save) != FileDialogPermissionAccess.None;
			}
			set
			{
				this.m_access = (value ? (this.m_access | FileDialogPermissionAccess.Save) : (this.m_access & ~FileDialogPermissionAccess.Save));
			}
		}

		// Token: 0x06003978 RID: 14712 RVA: 0x000C2057 File Offset: 0x000C1057
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new FileDialogPermission(PermissionState.Unrestricted);
			}
			return new FileDialogPermission(this.m_access);
		}

		// Token: 0x04001DFB RID: 7675
		private FileDialogPermissionAccess m_access;
	}
}
