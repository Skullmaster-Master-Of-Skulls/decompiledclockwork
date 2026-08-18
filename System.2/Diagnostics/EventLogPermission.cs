using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x020004D0 RID: 1232
	[Serializable]
	public sealed class EventLogPermission : ResourcePermissionBase
	{
		// Token: 0x06002E79 RID: 11897 RVA: 0x000D1CE1 File Offset: 0x000CFEE1
		public EventLogPermission()
		{
			this.SetNames();
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x000D1CEF File Offset: 0x000CFEEF
		public EventLogPermission(PermissionState state) : base(state)
		{
			this.SetNames();
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x000D1CFE File Offset: 0x000CFEFE
		public EventLogPermission(EventLogPermissionAccess permissionAccess, string machineName)
		{
			this.SetNames();
			this.AddPermissionAccess(new EventLogPermissionEntry(permissionAccess, machineName));
		}

		// Token: 0x06002E7C RID: 11900 RVA: 0x000D1D1C File Offset: 0x000CFF1C
		public EventLogPermission(EventLogPermissionEntry[] permissionAccessEntries)
		{
			if (permissionAccessEntries == null)
			{
				throw new ArgumentNullException("permissionAccessEntries");
			}
			this.SetNames();
			for (int i = 0; i < permissionAccessEntries.Length; i++)
			{
				this.AddPermissionAccess(permissionAccessEntries[i]);
			}
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06002E7D RID: 11901 RVA: 0x000D1D5A File Offset: 0x000CFF5A
		public EventLogPermissionEntryCollection PermissionEntries
		{
			get
			{
				if (this.innerCollection == null)
				{
					this.innerCollection = new EventLogPermissionEntryCollection(this, base.GetPermissionEntries());
				}
				return this.innerCollection;
			}
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x000D1D7C File Offset: 0x000CFF7C
		internal void AddPermissionAccess(EventLogPermissionEntry entry)
		{
			base.AddPermissionAccess(entry.GetBaseEntry());
		}

		// Token: 0x06002E7F RID: 11903 RVA: 0x000D1D8A File Offset: 0x000CFF8A
		internal new void Clear()
		{
			base.Clear();
		}

		// Token: 0x06002E80 RID: 11904 RVA: 0x000D1D92 File Offset: 0x000CFF92
		internal void RemovePermissionAccess(EventLogPermissionEntry entry)
		{
			base.RemovePermissionAccess(entry.GetBaseEntry());
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x000D1DA0 File Offset: 0x000CFFA0
		private void SetNames()
		{
			base.PermissionAccessType = typeof(EventLogPermissionAccess);
			base.TagNames = new string[]
			{
				"Machine"
			};
		}

		// Token: 0x04002777 RID: 10103
		private EventLogPermissionEntryCollection innerCollection;
	}
}
