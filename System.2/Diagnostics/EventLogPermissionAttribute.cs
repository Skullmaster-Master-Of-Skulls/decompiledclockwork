using System;
using System.ComponentModel;
using System.Security;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x020004D2 RID: 1234
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Event, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public class EventLogPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002E82 RID: 11906 RVA: 0x000D1DC6 File Offset: 0x000CFFC6
		public EventLogPermissionAttribute(SecurityAction action) : base(action)
		{
			this.machineName = ".";
			this.permissionAccess = EventLogPermissionAccess.Write;
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06002E83 RID: 11907 RVA: 0x000D1DE2 File Offset: 0x000CFFE2
		// (set) Token: 0x06002E84 RID: 11908 RVA: 0x000D1DEA File Offset: 0x000CFFEA
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

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06002E85 RID: 11909 RVA: 0x000D1E1D File Offset: 0x000D001D
		// (set) Token: 0x06002E86 RID: 11910 RVA: 0x000D1E25 File Offset: 0x000D0025
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

		// Token: 0x06002E87 RID: 11911 RVA: 0x000D1E2E File Offset: 0x000D002E
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new EventLogPermission(PermissionState.Unrestricted);
			}
			return new EventLogPermission(this.PermissionAccess, this.MachineName);
		}

		// Token: 0x0400277F RID: 10111
		private string machineName;

		// Token: 0x04002780 RID: 10112
		private EventLogPermissionAccess permissionAccess;
	}
}
