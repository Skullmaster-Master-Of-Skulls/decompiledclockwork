using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x02000759 RID: 1881
	[Serializable]
	public class EventLogPermissionEntry
	{
		// Token: 0x060039A8 RID: 14760 RVA: 0x000F4988 File Offset: 0x000F3988
		public EventLogPermissionEntry(EventLogPermissionAccess permissionAccess, string machineName)
		{
			if (!SyntaxCheck.CheckMachineName(machineName))
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[]
				{
					"MachineName",
					machineName
				}));
			}
			this.permissionAccess = permissionAccess;
			this.machineName = machineName;
		}

		// Token: 0x060039A9 RID: 14761 RVA: 0x000F49D5 File Offset: 0x000F39D5
		internal EventLogPermissionEntry(ResourcePermissionBaseEntry baseEntry)
		{
			this.permissionAccess = (EventLogPermissionAccess)baseEntry.PermissionAccess;
			this.machineName = baseEntry.PermissionAccessPath[0];
		}

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x060039AA RID: 14762 RVA: 0x000F49F7 File Offset: 0x000F39F7
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x060039AB RID: 14763 RVA: 0x000F49FF File Offset: 0x000F39FF
		public EventLogPermissionAccess PermissionAccess
		{
			get
			{
				return this.permissionAccess;
			}
		}

		// Token: 0x060039AC RID: 14764 RVA: 0x000F4A08 File Offset: 0x000F3A08
		internal ResourcePermissionBaseEntry GetBaseEntry()
		{
			return new ResourcePermissionBaseEntry((int)this.PermissionAccess, new string[]
			{
				this.MachineName
			});
		}

		// Token: 0x040032D9 RID: 13017
		private string machineName;

		// Token: 0x040032DA RID: 13018
		private EventLogPermissionAccess permissionAccess;
	}
}
