using System;

namespace System.Security.Permissions
{
	// Token: 0x0200000A RID: 10
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public sealed class DataProtectionPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06000020 RID: 32 RVA: 0x0000281C File Offset: 0x00000A1C
		public DataProtectionPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002825 File Offset: 0x00000A25
		// (set) Token: 0x06000022 RID: 34 RVA: 0x0000282D File Offset: 0x00000A2D
		public DataProtectionPermissionFlags Flags
		{
			get
			{
				return this.m_flags;
			}
			set
			{
				DataProtectionPermission.VerifyFlags(value);
				this.m_flags = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000283C File Offset: 0x00000A3C
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002849 File Offset: 0x00000A49
		public bool ProtectData
		{
			get
			{
				return (this.m_flags & DataProtectionPermissionFlags.ProtectData) > DataProtectionPermissionFlags.NoFlags;
			}
			set
			{
				this.m_flags = (value ? (this.m_flags | DataProtectionPermissionFlags.ProtectData) : (this.m_flags & ~DataProtectionPermissionFlags.ProtectData));
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002867 File Offset: 0x00000A67
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002874 File Offset: 0x00000A74
		public bool UnprotectData
		{
			get
			{
				return (this.m_flags & DataProtectionPermissionFlags.UnprotectData) > DataProtectionPermissionFlags.NoFlags;
			}
			set
			{
				this.m_flags = (value ? (this.m_flags | DataProtectionPermissionFlags.UnprotectData) : (this.m_flags & ~DataProtectionPermissionFlags.UnprotectData));
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002892 File Offset: 0x00000A92
		// (set) Token: 0x06000028 RID: 40 RVA: 0x0000289F File Offset: 0x00000A9F
		public bool ProtectMemory
		{
			get
			{
				return (this.m_flags & DataProtectionPermissionFlags.ProtectMemory) > DataProtectionPermissionFlags.NoFlags;
			}
			set
			{
				this.m_flags = (value ? (this.m_flags | DataProtectionPermissionFlags.ProtectMemory) : (this.m_flags & ~DataProtectionPermissionFlags.ProtectMemory));
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000028BD File Offset: 0x00000ABD
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000028CA File Offset: 0x00000ACA
		public bool UnprotectMemory
		{
			get
			{
				return (this.m_flags & DataProtectionPermissionFlags.UnprotectMemory) > DataProtectionPermissionFlags.NoFlags;
			}
			set
			{
				this.m_flags = (value ? (this.m_flags | DataProtectionPermissionFlags.UnprotectMemory) : (this.m_flags & ~DataProtectionPermissionFlags.UnprotectMemory));
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000028E8 File Offset: 0x00000AE8
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new DataProtectionPermission(PermissionState.Unrestricted);
			}
			return new DataProtectionPermission(this.m_flags);
		}

		// Token: 0x0400005F RID: 95
		private DataProtectionPermissionFlags m_flags;
	}
}
