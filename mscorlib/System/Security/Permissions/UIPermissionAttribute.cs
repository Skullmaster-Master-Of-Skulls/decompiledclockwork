using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000643 RID: 1603
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class UIPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x060039E1 RID: 14817 RVA: 0x000C28E6 File Offset: 0x000C18E6
		public UIPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x060039E2 RID: 14818 RVA: 0x000C28EF File Offset: 0x000C18EF
		// (set) Token: 0x060039E3 RID: 14819 RVA: 0x000C28F7 File Offset: 0x000C18F7
		public UIPermissionWindow Window
		{
			get
			{
				return this.m_windowFlag;
			}
			set
			{
				this.m_windowFlag = value;
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x060039E4 RID: 14820 RVA: 0x000C2900 File Offset: 0x000C1900
		// (set) Token: 0x060039E5 RID: 14821 RVA: 0x000C2908 File Offset: 0x000C1908
		public UIPermissionClipboard Clipboard
		{
			get
			{
				return this.m_clipboardFlag;
			}
			set
			{
				this.m_clipboardFlag = value;
			}
		}

		// Token: 0x060039E6 RID: 14822 RVA: 0x000C2911 File Offset: 0x000C1911
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new UIPermission(PermissionState.Unrestricted);
			}
			return new UIPermission(this.m_windowFlag, this.m_clipboardFlag);
		}

		// Token: 0x04001E14 RID: 7700
		private UIPermissionWindow m_windowFlag;

		// Token: 0x04001E15 RID: 7701
		private UIPermissionClipboard m_clipboardFlag;
	}
}
