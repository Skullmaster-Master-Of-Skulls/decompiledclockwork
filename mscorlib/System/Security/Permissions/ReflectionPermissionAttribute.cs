using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000640 RID: 1600
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class ReflectionPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x060039A5 RID: 14757 RVA: 0x000C23AF File Offset: 0x000C13AF
		public ReflectionPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x060039A6 RID: 14758 RVA: 0x000C23B8 File Offset: 0x000C13B8
		// (set) Token: 0x060039A7 RID: 14759 RVA: 0x000C23C0 File Offset: 0x000C13C0
		public ReflectionPermissionFlag Flags
		{
			get
			{
				return this.m_flag;
			}
			set
			{
				this.m_flag = value;
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x060039A8 RID: 14760 RVA: 0x000C23C9 File Offset: 0x000C13C9
		// (set) Token: 0x060039A9 RID: 14761 RVA: 0x000C23D9 File Offset: 0x000C13D9
		[Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
		public bool TypeInformation
		{
			get
			{
				return (this.m_flag & ReflectionPermissionFlag.TypeInformation) != ReflectionPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | ReflectionPermissionFlag.TypeInformation) : (this.m_flag & ~ReflectionPermissionFlag.TypeInformation));
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x060039AA RID: 14762 RVA: 0x000C23F7 File Offset: 0x000C13F7
		// (set) Token: 0x060039AB RID: 14763 RVA: 0x000C2407 File Offset: 0x000C1407
		public bool MemberAccess
		{
			get
			{
				return (this.m_flag & ReflectionPermissionFlag.MemberAccess) != ReflectionPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | ReflectionPermissionFlag.MemberAccess) : (this.m_flag & ~ReflectionPermissionFlag.MemberAccess));
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x060039AC RID: 14764 RVA: 0x000C2425 File Offset: 0x000C1425
		// (set) Token: 0x060039AD RID: 14765 RVA: 0x000C2435 File Offset: 0x000C1435
		public bool ReflectionEmit
		{
			get
			{
				return (this.m_flag & ReflectionPermissionFlag.ReflectionEmit) != ReflectionPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | ReflectionPermissionFlag.ReflectionEmit) : (this.m_flag & ~ReflectionPermissionFlag.ReflectionEmit));
			}
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x060039AE RID: 14766 RVA: 0x000C2453 File Offset: 0x000C1453
		// (set) Token: 0x060039AF RID: 14767 RVA: 0x000C2463 File Offset: 0x000C1463
		public bool RestrictedMemberAccess
		{
			get
			{
				return (this.m_flag & ReflectionPermissionFlag.RestrictedMemberAccess) != ReflectionPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | ReflectionPermissionFlag.RestrictedMemberAccess) : (this.m_flag & ~ReflectionPermissionFlag.RestrictedMemberAccess));
			}
		}

		// Token: 0x060039B0 RID: 14768 RVA: 0x000C2481 File Offset: 0x000C1481
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new ReflectionPermission(PermissionState.Unrestricted);
			}
			return new ReflectionPermission(this.m_flag);
		}

		// Token: 0x04001E0D RID: 7693
		private ReflectionPermissionFlag m_flag;
	}
}
