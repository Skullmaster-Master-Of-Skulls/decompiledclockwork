using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000632 RID: 1586
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class HostProtectionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06003931 RID: 14641 RVA: 0x000C13B2 File Offset: 0x000C03B2
		public HostProtectionAttribute() : base(SecurityAction.LinkDemand)
		{
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x000C13BB File Offset: 0x000C03BB
		public HostProtectionAttribute(SecurityAction action) : base(action)
		{
			if (action != SecurityAction.LinkDemand)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"));
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06003933 RID: 14643 RVA: 0x000C13D8 File Offset: 0x000C03D8
		// (set) Token: 0x06003934 RID: 14644 RVA: 0x000C13E0 File Offset: 0x000C03E0
		public HostProtectionResource Resources
		{
			get
			{
				return this.m_resources;
			}
			set
			{
				this.m_resources = value;
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06003935 RID: 14645 RVA: 0x000C13E9 File Offset: 0x000C03E9
		// (set) Token: 0x06003936 RID: 14646 RVA: 0x000C13F9 File Offset: 0x000C03F9
		public bool Synchronization
		{
			get
			{
				return (this.m_resources & HostProtectionResource.Synchronization) != HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.Synchronization) : (this.m_resources & ~HostProtectionResource.Synchronization));
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06003937 RID: 14647 RVA: 0x000C1417 File Offset: 0x000C0417
		// (set) Token: 0x06003938 RID: 14648 RVA: 0x000C1427 File Offset: 0x000C0427
		public bool SharedState
		{
			get
			{
				return (this.m_resources & HostProtectionResource.SharedState) != HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.SharedState) : (this.m_resources & ~HostProtectionResource.SharedState));
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06003939 RID: 14649 RVA: 0x000C1445 File Offset: 0x000C0445
		// (set) Token: 0x0600393A RID: 14650 RVA: 0x000C1455 File Offset: 0x000C0455
		public bool ExternalProcessMgmt
		{
			get
			{
				return (this.m_resources & HostProtectionResource.ExternalProcessMgmt) != HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.ExternalProcessMgmt) : (this.m_resources & ~HostProtectionResource.ExternalProcessMgmt));
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x0600393B RID: 14651 RVA: 0x000C1473 File Offset: 0x000C0473
		// (set) Token: 0x0600393C RID: 14652 RVA: 0x000C1483 File Offset: 0x000C0483
		public bool SelfAffectingProcessMgmt
		{
			get
			{
				return (this.m_resources & HostProtectionResource.SelfAffectingProcessMgmt) != HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.SelfAffectingProcessMgmt) : (this.m_resources & ~HostProtectionResource.SelfAffectingProcessMgmt));
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x0600393D RID: 14653 RVA: 0x000C14A1 File Offset: 0x000C04A1
		// (set) Token: 0x0600393E RID: 14654 RVA: 0x000C14B2 File Offset: 0x000C04B2
		public bool ExternalThreading
		{
			get
			{
				return (this.m_resources & HostProtectionResource.ExternalThreading) != HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.ExternalThreading) : (this.m_resources & ~HostProtectionResource.ExternalThreading));
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x0600393F RID: 14655 RVA: 0x000C14D1 File Offset: 0x000C04D1
		// (set) Token: 0x06003940 RID: 14656 RVA: 0x000C14E2 File Offset: 0x000C04E2
		public bool SelfAffectingThreading
		{
			get
			{
				return (this.m_resources & HostProtectionResource.SelfAffectingThreading) != HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.SelfAffectingThreading) : (this.m_resources & ~HostProtectionResource.SelfAffectingThreading));
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06003941 RID: 14657 RVA: 0x000C1501 File Offset: 0x000C0501
		// (set) Token: 0x06003942 RID: 14658 RVA: 0x000C1512 File Offset: 0x000C0512
		[ComVisible(true)]
		public bool SecurityInfrastructure
		{
			get
			{
				return (this.m_resources & HostProtectionResource.SecurityInfrastructure) != HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.SecurityInfrastructure) : (this.m_resources & ~HostProtectionResource.SecurityInfrastructure));
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06003943 RID: 14659 RVA: 0x000C1531 File Offset: 0x000C0531
		// (set) Token: 0x06003944 RID: 14660 RVA: 0x000C1545 File Offset: 0x000C0545
		public bool UI
		{
			get
			{
				return (this.m_resources & HostProtectionResource.UI) != HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.UI) : (this.m_resources & ~HostProtectionResource.UI));
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06003945 RID: 14661 RVA: 0x000C156A File Offset: 0x000C056A
		// (set) Token: 0x06003946 RID: 14662 RVA: 0x000C157E File Offset: 0x000C057E
		public bool MayLeakOnAbort
		{
			get
			{
				return (this.m_resources & HostProtectionResource.MayLeakOnAbort) != HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.MayLeakOnAbort) : (this.m_resources & ~HostProtectionResource.MayLeakOnAbort));
			}
		}

		// Token: 0x06003947 RID: 14663 RVA: 0x000C15A3 File Offset: 0x000C05A3
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new HostProtectionPermission(PermissionState.Unrestricted);
			}
			return new HostProtectionPermission(this.m_resources);
		}

		// Token: 0x04001DB0 RID: 7600
		private HostProtectionResource m_resources;
	}
}
