using System;

namespace System.Security.Permissions
{
	// Token: 0x02000489 RID: 1161
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class TypeDescriptorPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002B10 RID: 11024 RVA: 0x000C3E8F File Offset: 0x000C208F
		public TypeDescriptorPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06002B11 RID: 11025 RVA: 0x000C3E98 File Offset: 0x000C2098
		// (set) Token: 0x06002B12 RID: 11026 RVA: 0x000C3EA0 File Offset: 0x000C20A0
		public TypeDescriptorPermissionFlags Flags
		{
			get
			{
				return this.m_flags;
			}
			set
			{
				TypeDescriptorPermission.VerifyFlags(value);
				this.m_flags = value;
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06002B13 RID: 11027 RVA: 0x000C3EAF File Offset: 0x000C20AF
		// (set) Token: 0x06002B14 RID: 11028 RVA: 0x000C3EBC File Offset: 0x000C20BC
		public bool RestrictedRegistrationAccess
		{
			get
			{
				return (this.m_flags & TypeDescriptorPermissionFlags.RestrictedRegistrationAccess) > TypeDescriptorPermissionFlags.NoFlags;
			}
			set
			{
				this.m_flags = (value ? (this.m_flags | TypeDescriptorPermissionFlags.RestrictedRegistrationAccess) : (this.m_flags & ~TypeDescriptorPermissionFlags.RestrictedRegistrationAccess));
			}
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x000C3EDA File Offset: 0x000C20DA
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new TypeDescriptorPermission(PermissionState.Unrestricted);
			}
			return new TypeDescriptorPermission(this.m_flags);
		}

		// Token: 0x04002670 RID: 9840
		private TypeDescriptorPermissionFlags m_flags;
	}
}
