using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000649 RID: 1609
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public abstract class IsolatedStoragePermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06003A03 RID: 14851 RVA: 0x000C2B9A File Offset: 0x000C1B9A
		protected IsolatedStoragePermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06003A05 RID: 14853 RVA: 0x000C2BAC File Offset: 0x000C1BAC
		// (set) Token: 0x06003A04 RID: 14852 RVA: 0x000C2BA3 File Offset: 0x000C1BA3
		public long UserQuota
		{
			get
			{
				return this.m_userQuota;
			}
			set
			{
				this.m_userQuota = value;
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06003A07 RID: 14855 RVA: 0x000C2BBD File Offset: 0x000C1BBD
		// (set) Token: 0x06003A06 RID: 14854 RVA: 0x000C2BB4 File Offset: 0x000C1BB4
		public IsolatedStorageContainment UsageAllowed
		{
			get
			{
				return this.m_allowed;
			}
			set
			{
				this.m_allowed = value;
			}
		}

		// Token: 0x04001E1F RID: 7711
		internal long m_userQuota;

		// Token: 0x04001E20 RID: 7712
		internal IsolatedStorageContainment m_allowed;
	}
}
