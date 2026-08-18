using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x0200063B RID: 1595
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class EnvironmentPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x0600396B RID: 14699 RVA: 0x000C1F58 File Offset: 0x000C0F58
		public EnvironmentPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x0600396C RID: 14700 RVA: 0x000C1F61 File Offset: 0x000C0F61
		// (set) Token: 0x0600396D RID: 14701 RVA: 0x000C1F69 File Offset: 0x000C0F69
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

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x0600396E RID: 14702 RVA: 0x000C1F72 File Offset: 0x000C0F72
		// (set) Token: 0x0600396F RID: 14703 RVA: 0x000C1F7A File Offset: 0x000C0F7A
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

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06003970 RID: 14704 RVA: 0x000C1F83 File Offset: 0x000C0F83
		// (set) Token: 0x06003971 RID: 14705 RVA: 0x000C1F94 File Offset: 0x000C0F94
		public string All
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_GetMethod"));
			}
			set
			{
				this.m_write = value;
				this.m_read = value;
			}
		}

		// Token: 0x06003972 RID: 14706 RVA: 0x000C1FA4 File Offset: 0x000C0FA4
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new EnvironmentPermission(PermissionState.Unrestricted);
			}
			EnvironmentPermission environmentPermission = new EnvironmentPermission(PermissionState.None);
			if (this.m_read != null)
			{
				environmentPermission.SetPathList(EnvironmentPermissionAccess.Read, this.m_read);
			}
			if (this.m_write != null)
			{
				environmentPermission.SetPathList(EnvironmentPermissionAccess.Write, this.m_write);
			}
			return environmentPermission;
		}

		// Token: 0x04001DF9 RID: 7673
		private string m_read;

		// Token: 0x04001DFA RID: 7674
		private string m_write;
	}
}
