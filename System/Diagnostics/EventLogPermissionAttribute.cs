using System;
using System.ComponentModel;
using System.Security;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x02000758 RID: 1880
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Event, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public class EventLogPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x060039A2 RID: 14754 RVA: 0x000F48EF File Offset: 0x000F38EF
		public EventLogPermissionAttribute(SecurityAction action) : base(action)
		{
			this.machineName = ".";
			this.permissionAccess = EventLogPermissionAccess.Write;
		}

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x060039A3 RID: 14755 RVA: 0x000F490B File Offset: 0x000F390B
		// (set) Token: 0x060039A4 RID: 14756 RVA: 0x000F4914 File Offset: 0x000F3914
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
			set
			{
				if (!SyntaxCheck.CheckMachineName(value))
				{
					throw new ArgumentException(SR.GetString("InvalidProperty", new object[]
					{
						"MachineName",
						value
					}));
				}
				this.machineName = value;
			}
		}

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x060039A5 RID: 14757 RVA: 0x000F4954 File Offset: 0x000F3954
		// (set) Token: 0x060039A6 RID: 14758 RVA: 0x000F495C File Offset: 0x000F395C
		public EventLogPermissionAccess PermissionAccess
		{
			get
			{
				return this.permissionAccess;
			}
			set
			{
				this.permissionAccess = value;
			}
		}

		// Token: 0x060039A7 RID: 14759 RVA: 0x000F4965 File Offset: 0x000F3965
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new EventLogPermission(PermissionState.Unrestricted);
			}
			return new EventLogPermission(this.PermissionAccess, this.MachineName);
		}

		// Token: 0x040032D7 RID: 13015
		private string machineName;

		// Token: 0x040032D8 RID: 13016
		private EventLogPermissionAccess permissionAccess;
	}
}
