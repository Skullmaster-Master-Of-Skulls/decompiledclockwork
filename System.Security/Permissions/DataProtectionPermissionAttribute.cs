using System;

namespace System.Security.Permissions
{
	// Token: 0x020000D0 RID: 208
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public sealed class DataProtectionPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06000522 RID: 1314 RVA: 0x00019E6E File Offset: 0x00018E6E
		public DataProtectionPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00019E77 File Offset: 0x00018E77
		// (set) Token: 0x06000524 RID: 1316 RVA: 0x00019E7F File Offset: 0x00018E7F
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

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00019E8E File Offset: 0x00018E8E
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x00019E9E File Offset: 0x00018E9E
		public bool ProtectData
		{
			get
			{
				return (this.m_flags & DataProtectionPermissionFlags.ProtectData) != DataProtectionPermissionFlags.NoFlags;
			}
			set
			{
				this.m_flags = (value ? (this.m_flags | DataProtectionPermissionFlags.ProtectData) : (this.m_flags & ~DataProtectionPermissionFlags.ProtectData));
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x00019EBC File Offset: 0x00018EBC
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x00019ECC File Offset: 0x00018ECC
		public bool UnprotectData
		{
			get
			{
				return (this.m_flags & DataProtectionPermissionFlags.UnprotectData) != DataProtectionPermissionFlags.NoFlags;
			}
			set
			{
				this.m_flags = (value ? (this.m_flags | DataProtectionPermissionFlags.UnprotectData) : (this.m_flags & ~DataProtectionPermissionFlags.UnprotectData));
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x00019EEA File Offset: 0x00018EEA
		// (set) Token: 0x0600052A RID: 1322 RVA: 0x00019EFA File Offset: 0x00018EFA
		public bool ProtectMemory
		{
			get
			{
				return (this.m_flags & DataProtectionPermissionFlags.ProtectMemory) != DataProtectionPermissionFlags.NoFlags;
			}
			set
			{
				this.m_flags = (value ? (this.m_flags | DataProtectionPermissionFlags.ProtectMemory) : (this.m_flags & ~DataProtectionPermissionFlags.ProtectMemory));
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00019F18 File Offset: 0x00018F18
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x00019F28 File Offset: 0x00018F28
		public bool UnprotectMemory
		{
			get
			{
				return (this.m_flags & DataProtectionPermissionFlags.UnprotectMemory) != DataProtectionPermissionFlags.NoFlags;
			}
			set
			{
				this.m_flags = (value ? (this.m_flags | DataProtectionPermissionFlags.UnprotectMemory) : (this.m_flags & ~DataProtectionPermissionFlags.UnprotectMemory));
			}
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00019F46 File Offset: 0x00018F46
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new DataProtectionPermission(PermissionState.Unrestricted);
			}
			return new DataProtectionPermission(this.m_flags);
		}

		// Token: 0x040005DE RID: 1502
		private DataProtectionPermissionFlags m_flags;
	}
}
