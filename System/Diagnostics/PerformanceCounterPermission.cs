using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x0200076F RID: 1903
	[Serializable]
	public sealed class PerformanceCounterPermission : ResourcePermissionBase
	{
		// Token: 0x06003AA1 RID: 15009 RVA: 0x000F97D7 File Offset: 0x000F87D7
		public PerformanceCounterPermission()
		{
			this.SetNames();
		}

		// Token: 0x06003AA2 RID: 15010 RVA: 0x000F97E5 File Offset: 0x000F87E5
		public PerformanceCounterPermission(PermissionState state) : base(state)
		{
			this.SetNames();
		}

		// Token: 0x06003AA3 RID: 15011 RVA: 0x000F97F4 File Offset: 0x000F87F4
		public PerformanceCounterPermission(PerformanceCounterPermissionAccess permissionAccess, string machineName, string categoryName)
		{
			this.SetNames();
			this.AddPermissionAccess(new PerformanceCounterPermissionEntry(permissionAccess, machineName, categoryName));
		}

		// Token: 0x06003AA4 RID: 15012 RVA: 0x000F9810 File Offset: 0x000F8810
		public PerformanceCounterPermission(PerformanceCounterPermissionEntry[] permissionAccessEntries)
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

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06003AA5 RID: 15013 RVA: 0x000F984E File Offset: 0x000F884E
		public PerformanceCounterPermissionEntryCollection PermissionEntries
		{
			get
			{
				if (this.innerCollection == null)
				{
					this.innerCollection = new PerformanceCounterPermissionEntryCollection(this, base.GetPermissionEntries());
				}
				return this.innerCollection;
			}
		}

		// Token: 0x06003AA6 RID: 15014 RVA: 0x000F9870 File Offset: 0x000F8870
		internal void AddPermissionAccess(PerformanceCounterPermissionEntry entry)
		{
			base.AddPermissionAccess(entry.GetBaseEntry());
		}

		// Token: 0x06003AA7 RID: 15015 RVA: 0x000F987E File Offset: 0x000F887E
		internal new void Clear()
		{
			base.Clear();
		}

		// Token: 0x06003AA8 RID: 15016 RVA: 0x000F9886 File Offset: 0x000F8886
		internal void RemovePermissionAccess(PerformanceCounterPermissionEntry entry)
		{
			base.RemovePermissionAccess(entry.GetBaseEntry());
		}

		// Token: 0x06003AA9 RID: 15017 RVA: 0x000F9894 File Offset: 0x000F8894
		private void SetNames()
		{
			base.PermissionAccessType = typeof(PerformanceCounterPermissionAccess);
			base.TagNames = new string[]
			{
				"Machine",
				"Category"
			};
		}

		// Token: 0x0400335B RID: 13147
		private PerformanceCounterPermissionEntryCollection innerCollection;
	}
}
