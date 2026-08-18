using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x02000756 RID: 1878
	[Serializable]
	public sealed class EventLogPermission : ResourcePermissionBase
	{
		// Token: 0x06003999 RID: 14745 RVA: 0x000F47FF File Offset: 0x000F37FF
		public EventLogPermission()
		{
			this.SetNames();
		}

		// Token: 0x0600399A RID: 14746 RVA: 0x000F480D File Offset: 0x000F380D
		public EventLogPermission(PermissionState state) : base(state)
		{
			this.SetNames();
		}

		// Token: 0x0600399B RID: 14747 RVA: 0x000F481C File Offset: 0x000F381C
		public EventLogPermission(EventLogPermissionAccess permissionAccess, string machineName)
		{
			this.SetNames();
			this.AddPermissionAccess(new EventLogPermissionEntry(permissionAccess, machineName));
		}

		// Token: 0x0600399C RID: 14748 RVA: 0x000F4838 File Offset: 0x000F3838
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

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x0600399D RID: 14749 RVA: 0x000F4876 File Offset: 0x000F3876
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

		// Token: 0x0600399E RID: 14750 RVA: 0x000F4898 File Offset: 0x000F3898
		internal void AddPermissionAccess(EventLogPermissionEntry entry)
		{
			base.AddPermissionAccess(entry.GetBaseEntry());
		}

		// Token: 0x0600399F RID: 14751 RVA: 0x000F48A6 File Offset: 0x000F38A6
		internal new void Clear()
		{
			base.Clear();
		}

		// Token: 0x060039A0 RID: 14752 RVA: 0x000F48AE File Offset: 0x000F38AE
		internal void RemovePermissionAccess(EventLogPermissionEntry entry)
		{
			base.RemovePermissionAccess(entry.GetBaseEntry());
		}

		// Token: 0x060039A1 RID: 14753 RVA: 0x000F48BC File Offset: 0x000F38BC
		private void SetNames()
		{
			base.PermissionAccessType = typeof(EventLogPermissionAccess);
			base.TagNames = new string[]
			{
				"Machine"
			};
		}

		// Token: 0x040032CF RID: 13007
		private EventLogPermissionEntryCollection innerCollection;
	}
}
