using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x020004D3 RID: 1235
	[Serializable]
	public class EventLogPermissionEntry
	{
		// Token: 0x06002E88 RID: 11912 RVA: 0x000D1E50 File Offset: 0x000D0050
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

		// Token: 0x06002E89 RID: 11913 RVA: 0x000D1E90 File Offset: 0x000D0090
		internal EventLogPermissionEntry(ResourcePermissionBaseEntry baseEntry)
		{
			this.permissionAccess = (EventLogPermissionAccess)baseEntry.PermissionAccess;
			this.machineName = baseEntry.PermissionAccessPath[0];
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06002E8A RID: 11914 RVA: 0x000D1EB2 File Offset: 0x000D00B2
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06002E8B RID: 11915 RVA: 0x000D1EBA File Offset: 0x000D00BA
		public EventLogPermissionAccess PermissionAccess
		{
			get
			{
				return this.permissionAccess;
			}
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x000D1EC4 File Offset: 0x000D00C4
		internal ResourcePermissionBaseEntry GetBaseEntry()
		{
			return new ResourcePermissionBaseEntry((int)this.PermissionAccess, new string[]
			{
				this.MachineName
			});
		}

		// Token: 0x04002781 RID: 10113
		private string machineName;

		// Token: 0x04002782 RID: 10114
		private EventLogPermissionAccess permissionAccess;
	}
}
