using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000642 RID: 1602
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class SecurityPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x060039C1 RID: 14785 RVA: 0x000C25D9 File Offset: 0x000C15D9
		public SecurityPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x060039C2 RID: 14786 RVA: 0x000C25E2 File Offset: 0x000C15E2
		// (set) Token: 0x060039C3 RID: 14787 RVA: 0x000C25EA File Offset: 0x000C15EA
		public SecurityPermissionFlag Flags
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

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x060039C4 RID: 14788 RVA: 0x000C25F3 File Offset: 0x000C15F3
		// (set) Token: 0x060039C5 RID: 14789 RVA: 0x000C2603 File Offset: 0x000C1603
		public bool Assertion
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.Assertion) != SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.Assertion) : (this.m_flag & ~SecurityPermissionFlag.Assertion));
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x060039C6 RID: 14790 RVA: 0x000C2621 File Offset: 0x000C1621
		// (set) Token: 0x060039C7 RID: 14791 RVA: 0x000C2631 File Offset: 0x000C1631
		public bool UnmanagedCode
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.UnmanagedCode) != SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.UnmanagedCode) : (this.m_flag & ~SecurityPermissionFlag.UnmanagedCode));
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x060039C8 RID: 14792 RVA: 0x000C264F File Offset: 0x000C164F
		// (set) Token: 0x060039C9 RID: 14793 RVA: 0x000C265F File Offset: 0x000C165F
		public bool SkipVerification
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.SkipVerification) != SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.SkipVerification) : (this.m_flag & ~SecurityPermissionFlag.SkipVerification));
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x060039CA RID: 14794 RVA: 0x000C267D File Offset: 0x000C167D
		// (set) Token: 0x060039CB RID: 14795 RVA: 0x000C268D File Offset: 0x000C168D
		public bool Execution
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.Execution) != SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.Execution) : (this.m_flag & ~SecurityPermissionFlag.Execution));
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x060039CC RID: 14796 RVA: 0x000C26AB File Offset: 0x000C16AB
		// (set) Token: 0x060039CD RID: 14797 RVA: 0x000C26BC File Offset: 0x000C16BC
		public bool ControlThread
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.ControlThread) != SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.ControlThread) : (this.m_flag & ~SecurityPermissionFlag.ControlThread));
			}
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x060039CE RID: 14798 RVA: 0x000C26DB File Offset: 0x000C16DB
		// (set) Token: 0x060039CF RID: 14799 RVA: 0x000C26EC File Offset: 0x000C16EC
		public bool ControlEvidence
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.ControlEvidence) != SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.ControlEvidence) : (this.m_flag & ~SecurityPermissionFlag.ControlEvidence));
			}
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x060039D0 RID: 14800 RVA: 0x000C270B File Offset: 0x000C170B
		// (set) Token: 0x060039D1 RID: 14801 RVA: 0x000C271C File Offset: 0x000C171C
		public bool ControlPolicy
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.ControlPolicy) != SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.ControlPolicy) : (this.m_flag & ~SecurityPermissionFlag.ControlPolicy));
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x060039D2 RID: 14802 RVA: 0x000C273B File Offset: 0x000C173B
		// (set) Token: 0x060039D3 RID: 14803 RVA: 0x000C274F File Offset: 0x000C174F
		public bool SerializationFormatter
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.SerializationFormatter) != SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.SerializationFormatter) : (this.m_flag & ~SecurityPermissionFlag.SerializationFormatter));
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x060039D4 RID: 14804 RVA: 0x000C2774 File Offset: 0x000C1774
		// (set) Token: 0x060039D5 RID: 14805 RVA: 0x000C2788 File Offset: 0x000C1788
		public bool ControlDomainPolicy
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.ControlDomainPolicy) != SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.ControlDomainPolicy) : (this.m_flag & ~SecurityPermissionFlag.ControlDomainPolicy));
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x060039D6 RID: 14806 RVA: 0x000C27AD File Offset: 0x000C17AD
		// (set) Token: 0x060039D7 RID: 14807 RVA: 0x000C27C1 File Offset: 0x000C17C1
		public bool ControlPrincipal
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.ControlPrincipal) != SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.ControlPrincipal) : (this.m_flag & ~SecurityPermissionFlag.ControlPrincipal));
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x060039D8 RID: 14808 RVA: 0x000C27E6 File Offset: 0x000C17E6
		// (set) Token: 0x060039D9 RID: 14809 RVA: 0x000C27FA File Offset: 0x000C17FA
		public bool ControlAppDomain
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.ControlAppDomain) != SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.ControlAppDomain) : (this.m_flag & ~SecurityPermissionFlag.ControlAppDomain));
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x060039DA RID: 14810 RVA: 0x000C281F File Offset: 0x000C181F
		// (set) Token: 0x060039DB RID: 14811 RVA: 0x000C2833 File Offset: 0x000C1833
		public bool RemotingConfiguration
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.RemotingConfiguration) != SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.RemotingConfiguration) : (this.m_flag & ~SecurityPermissionFlag.RemotingConfiguration));
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x060039DC RID: 14812 RVA: 0x000C2858 File Offset: 0x000C1858
		// (set) Token: 0x060039DD RID: 14813 RVA: 0x000C286C File Offset: 0x000C186C
		[ComVisible(true)]
		public bool Infrastructure
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.Infrastructure) != SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.Infrastructure) : (this.m_flag & ~SecurityPermissionFlag.Infrastructure));
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x060039DE RID: 14814 RVA: 0x000C2891 File Offset: 0x000C1891
		// (set) Token: 0x060039DF RID: 14815 RVA: 0x000C28A5 File Offset: 0x000C18A5
		public bool BindingRedirects
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.BindingRedirects) != SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.BindingRedirects) : (this.m_flag & ~SecurityPermissionFlag.BindingRedirects));
			}
		}

		// Token: 0x060039E0 RID: 14816 RVA: 0x000C28CA File Offset: 0x000C18CA
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new SecurityPermission(PermissionState.Unrestricted);
			}
			return new SecurityPermission(this.m_flag);
		}

		// Token: 0x04001E13 RID: 7699
		private SecurityPermissionFlag m_flag;
	}
}
