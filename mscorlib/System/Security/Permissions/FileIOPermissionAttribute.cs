using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.AccessControl;

namespace System.Security.Permissions
{
	// Token: 0x0200063D RID: 1597
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class FileIOPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06003979 RID: 14713 RVA: 0x000C2073 File Offset: 0x000C1073
		public FileIOPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x0600397A RID: 14714 RVA: 0x000C207C File Offset: 0x000C107C
		// (set) Token: 0x0600397B RID: 14715 RVA: 0x000C2084 File Offset: 0x000C1084
		public string Read
		{
			get
			{
				return this.m_read;
			}
			set
			{
				this.m_read = value;
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x0600397C RID: 14716 RVA: 0x000C208D File Offset: 0x000C108D
		// (set) Token: 0x0600397D RID: 14717 RVA: 0x000C2095 File Offset: 0x000C1095
		public string Write
		{
			get
			{
				return this.m_write;
			}
			set
			{
				this.m_write = value;
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x0600397E RID: 14718 RVA: 0x000C209E File Offset: 0x000C109E
		// (set) Token: 0x0600397F RID: 14719 RVA: 0x000C20A6 File Offset: 0x000C10A6
		public string Append
		{
			get
			{
				return this.m_append;
			}
			set
			{
				this.m_append = value;
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06003980 RID: 14720 RVA: 0x000C20AF File Offset: 0x000C10AF
		// (set) Token: 0x06003981 RID: 14721 RVA: 0x000C20B7 File Offset: 0x000C10B7
		public string PathDiscovery
		{
			get
			{
				return this.m_pathDiscovery;
			}
			set
			{
				this.m_pathDiscovery = value;
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06003982 RID: 14722 RVA: 0x000C20C0 File Offset: 0x000C10C0
		// (set) Token: 0x06003983 RID: 14723 RVA: 0x000C20C8 File Offset: 0x000C10C8
		public string ViewAccessControl
		{
			get
			{
				return this.m_viewAccess;
			}
			set
			{
				this.m_viewAccess = value;
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06003984 RID: 14724 RVA: 0x000C20D1 File Offset: 0x000C10D1
		// (set) Token: 0x06003985 RID: 14725 RVA: 0x000C20D9 File Offset: 0x000C10D9
		public string ChangeAccessControl
		{
			get
			{
				return this.m_changeAccess;
			}
			set
			{
				this.m_changeAccess = value;
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06003987 RID: 14727 RVA: 0x000C2100 File Offset: 0x000C1100
		// (set) Token: 0x06003986 RID: 14726 RVA: 0x000C20E2 File Offset: 0x000C10E2
		[Obsolete("Please use the ViewAndModify property instead.")]
		public string All
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_GetMethod"));
			}
			set
			{
				this.m_read = value;
				this.m_write = value;
				this.m_append = value;
				this.m_pathDiscovery = value;
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06003988 RID: 14728 RVA: 0x000C2111 File Offset: 0x000C1111
		// (set) Token: 0x06003989 RID: 14729 RVA: 0x000C2122 File Offset: 0x000C1122
		public string ViewAndModify
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_GetMethod"));
			}
			set
			{
				this.m_read = value;
				this.m_write = value;
				this.m_append = value;
				this.m_pathDiscovery = value;
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x0600398A RID: 14730 RVA: 0x000C2140 File Offset: 0x000C1140
		// (set) Token: 0x0600398B RID: 14731 RVA: 0x000C2148 File Offset: 0x000C1148
		public FileIOPermissionAccess AllFiles
		{
			get
			{
				return this.m_allFiles;
			}
			set
			{
				this.m_allFiles = value;
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x0600398C RID: 14732 RVA: 0x000C2151 File Offset: 0x000C1151
		// (set) Token: 0x0600398D RID: 14733 RVA: 0x000C2159 File Offset: 0x000C1159
		public FileIOPermissionAccess AllLocalFiles
		{
			get
			{
				return this.m_allLocalFiles;
			}
			set
			{
				this.m_allLocalFiles = value;
			}
		}

		// Token: 0x0600398E RID: 14734 RVA: 0x000C2164 File Offset: 0x000C1164
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new FileIOPermission(PermissionState.Unrestricted);
			}
			FileIOPermission fileIOPermission = new FileIOPermission(PermissionState.None);
			if (this.m_read != null)
			{
				fileIOPermission.SetPathList(FileIOPermissionAccess.Read, this.m_read);
			}
			if (this.m_write != null)
			{
				fileIOPermission.SetPathList(FileIOPermissionAccess.Write, this.m_write);
			}
			if (this.m_append != null)
			{
				fileIOPermission.SetPathList(FileIOPermissionAccess.Append, this.m_append);
			}
			if (this.m_pathDiscovery != null)
			{
				fileIOPermission.SetPathList(FileIOPermissionAccess.PathDiscovery, this.m_pathDiscovery);
			}
			if (this.m_viewAccess != null)
			{
				fileIOPermission.SetPathList(FileIOPermissionAccess.NoAccess, AccessControlActions.View, new string[]
				{
					this.m_viewAccess
				}, false);
			}
			if (this.m_changeAccess != null)
			{
				fileIOPermission.SetPathList(FileIOPermissionAccess.NoAccess, AccessControlActions.Change, new string[]
				{
					this.m_changeAccess
				}, false);
			}
			fileIOPermission.AllFiles = this.m_allFiles;
			fileIOPermission.AllLocalFiles = this.m_allLocalFiles;
			return fileIOPermission;
		}

		// Token: 0x04001DFC RID: 7676
		private string m_read;

		// Token: 0x04001DFD RID: 7677
		private string m_write;

		// Token: 0x04001DFE RID: 7678
		private string m_append;

		// Token: 0x04001DFF RID: 7679
		private string m_pathDiscovery;

		// Token: 0x04001E00 RID: 7680
		private string m_viewAccess;

		// Token: 0x04001E01 RID: 7681
		private string m_changeAccess;

		// Token: 0x04001E02 RID: 7682
		[OptionalField(VersionAdded = 2)]
		private FileIOPermissionAccess m_allLocalFiles;

		// Token: 0x04001E03 RID: 7683
		[OptionalField(VersionAdded = 2)]
		private FileIOPermissionAccess m_allFiles;
	}
}
