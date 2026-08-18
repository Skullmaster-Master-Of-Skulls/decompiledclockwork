using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x020000EB RID: 235
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class CngKeyCreationParameters
	{
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x000184D7 File Offset: 0x000166D7
		// (set) Token: 0x06000765 RID: 1893 RVA: 0x000184DF File Offset: 0x000166DF
		public CngExportPolicies? ExportPolicy
		{
			get
			{
				return this.m_exportPolicy;
			}
			set
			{
				this.m_exportPolicy = value;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x000184E8 File Offset: 0x000166E8
		// (set) Token: 0x06000767 RID: 1895 RVA: 0x000184F0 File Offset: 0x000166F0
		public CngKeyCreationOptions KeyCreationOptions
		{
			get
			{
				return this.m_keyCreationOptions;
			}
			set
			{
				this.m_keyCreationOptions = value;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x000184F9 File Offset: 0x000166F9
		// (set) Token: 0x06000769 RID: 1897 RVA: 0x00018501 File Offset: 0x00016701
		public CngKeyUsages? KeyUsage
		{
			get
			{
				return this.m_keyUsage;
			}
			set
			{
				this.m_keyUsage = value;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x0001850A File Offset: 0x0001670A
		// (set) Token: 0x0600076B RID: 1899 RVA: 0x00018512 File Offset: 0x00016712
		public IntPtr ParentWindowHandle
		{
			get
			{
				return this.m_parentWindowHandle;
			}
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
			set
			{
				this.m_parentWindowHandle = value;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x0001851B File Offset: 0x0001671B
		public CngPropertyCollection Parameters
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
			get
			{
				return this.m_parameters;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x00018523 File Offset: 0x00016723
		internal CngPropertyCollection ParametersNoDemand
		{
			get
			{
				return this.m_parameters;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x0001852B File Offset: 0x0001672B
		// (set) Token: 0x0600076F RID: 1903 RVA: 0x00018533 File Offset: 0x00016733
		public CngProvider Provider
		{
			get
			{
				return this.m_provider;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_provider = value;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x00018550 File Offset: 0x00016750
		// (set) Token: 0x06000771 RID: 1905 RVA: 0x00018558 File Offset: 0x00016758
		public CngUIPolicy UIPolicy
		{
			get
			{
				return this.m_uiPolicy;
			}
			[SecuritySafeCritical]
			[HostProtection(SecurityAction.LinkDemand, UI = true)]
			[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.SafeSubWindows)]
			set
			{
				this.m_uiPolicy = value;
			}
		}

		// Token: 0x0400061C RID: 1564
		private CngExportPolicies? m_exportPolicy;

		// Token: 0x0400061D RID: 1565
		private CngKeyCreationOptions m_keyCreationOptions;

		// Token: 0x0400061E RID: 1566
		private CngKeyUsages? m_keyUsage;

		// Token: 0x0400061F RID: 1567
		private CngPropertyCollection m_parameters = new CngPropertyCollection();

		// Token: 0x04000620 RID: 1568
		private IntPtr m_parentWindowHandle;

		// Token: 0x04000621 RID: 1569
		private CngProvider m_provider = CngProvider.MicrosoftSoftwareKeyStorageProvider;

		// Token: 0x04000622 RID: 1570
		private CngUIPolicy m_uiPolicy;
	}
}
