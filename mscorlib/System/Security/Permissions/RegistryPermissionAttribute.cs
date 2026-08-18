using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

namespace System.Security.Permissions
{
	// Token: 0x02000641 RID: 1601
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class RegistryPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x060039B1 RID: 14769 RVA: 0x000C249D File Offset: 0x000C149D
		public RegistryPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x060039B2 RID: 14770 RVA: 0x000C24A6 File Offset: 0x000C14A6
		// (set) Token: 0x060039B3 RID: 14771 RVA: 0x000C24AE File Offset: 0x000C14AE
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

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x060039B4 RID: 14772 RVA: 0x000C24B7 File Offset: 0x000C14B7
		// (set) Token: 0x060039B5 RID: 14773 RVA: 0x000C24BF File Offset: 0x000C14BF
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

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x060039B6 RID: 14774 RVA: 0x000C24C8 File Offset: 0x000C14C8
		// (set) Token: 0x060039B7 RID: 14775 RVA: 0x000C24D0 File Offset: 0x000C14D0
		public string Create
		{
			get
			{
				return this.m_create;
			}
			set
			{
				this.m_create = value;
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x060039B8 RID: 14776 RVA: 0x000C24D9 File Offset: 0x000C14D9
		// (set) Token: 0x060039B9 RID: 14777 RVA: 0x000C24E1 File Offset: 0x000C14E1
		public string ViewAccessControl
		{
			get
			{
				return this.m_viewAcl;
			}
			set
			{
				this.m_viewAcl = value;
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x060039BA RID: 14778 RVA: 0x000C24EA File Offset: 0x000C14EA
		// (set) Token: 0x060039BB RID: 14779 RVA: 0x000C24F2 File Offset: 0x000C14F2
		public string ChangeAccessControl
		{
			get
			{
				return this.m_changeAcl;
			}
			set
			{
				this.m_changeAcl = value;
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x060039BC RID: 14780 RVA: 0x000C24FB File Offset: 0x000C14FB
		// (set) Token: 0x060039BD RID: 14781 RVA: 0x000C250C File Offset: 0x000C150C
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
				this.m_create = value;
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x060039BE RID: 14782 RVA: 0x000C2523 File Offset: 0x000C1523
		// (set) Token: 0x060039BF RID: 14783 RVA: 0x000C2534 File Offset: 0x000C1534
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
				this.m_create = value;
			}
		}

		// Token: 0x060039C0 RID: 14784 RVA: 0x000C254C File Offset: 0x000C154C
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new RegistryPermission(PermissionState.Unrestricted);
			}
			RegistryPermission registryPermission = new RegistryPermission(PermissionState.None);
			if (this.m_read != null)
			{
				registryPermission.SetPathList(RegistryPermissionAccess.Read, this.m_read);
			}
			if (this.m_write != null)
			{
				registryPermission.SetPathList(RegistryPermissionAccess.Write, this.m_write);
			}
			if (this.m_create != null)
			{
				registryPermission.SetPathList(RegistryPermissionAccess.Create, this.m_create);
			}
			if (this.m_viewAcl != null)
			{
				registryPermission.SetPathList(AccessControlActions.View, this.m_viewAcl);
			}
			if (this.m_changeAcl != null)
			{
				registryPermission.SetPathList(AccessControlActions.Change, this.m_changeAcl);
			}
			return registryPermission;
		}

		// Token: 0x04001E0E RID: 7694
		private string m_read;

		// Token: 0x04001E0F RID: 7695
		private string m_write;

		// Token: 0x04001E10 RID: 7696
		private string m_create;

		// Token: 0x04001E11 RID: 7697
		private string m_viewAcl;

		// Token: 0x04001E12 RID: 7698
		private string m_changeAcl;
	}
}
