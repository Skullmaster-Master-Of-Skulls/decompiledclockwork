using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x020004E9 RID: 1257
	[Serializable]
	public sealed class PerformanceCounterPermission : ResourcePermissionBase
	{
		// Token: 0x06002F84 RID: 12164 RVA: 0x000D7033 File Offset: 0x000D5233
		public PerformanceCounterPermission()
		{
			this.SetNames();
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x000D7041 File Offset: 0x000D5241
		public PerformanceCounterPermission(PermissionState state) : base(state)
		{
			this.SetNames();
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x000D7050 File Offset: 0x000D5250
		public PerformanceCounterPermission(PerformanceCounterPermissionAccess permissionAccess, string machineName, string categoryName)
		{
			this.SetNames();
			this.AddPermissionAccess(new PerformanceCounterPermissionEntry(permissionAccess, machineName, categoryName));
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x000D706C File Offset: 0x000D526C
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

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06002F88 RID: 12168 RVA: 0x000D70AA File Offset: 0x000D52AA
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

		// Token: 0x06002F89 RID: 12169 RVA: 0x000D70CC File Offset: 0x000D52CC
		internal void AddPermissionAccess(PerformanceCounterPermissionEntry entry)
		{
			base.AddPermissionAccess(entry.GetBaseEntry());
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x000D70DA File Offset: 0x000D52DA
		internal new void Clear()
		{
			base.Clear();
		}

		// Token: 0x06002F8B RID: 12171 RVA: 0x000D70E2 File Offset: 0x000D52E2
		internal void RemovePermissionAccess(PerformanceCounterPermissionEntry entry)
		{
			base.RemovePermissionAccess(entry.GetBaseEntry());
		}

		// Token: 0x06002F8C RID: 12172 RVA: 0x000D70F0 File Offset: 0x000D52F0
		private void SetNames()
		{
			base.PermissionAccessType = typeof(PerformanceCounterPermissionAccess);
			base.TagNames = new string[]
			{
				"Machine",
				"Category"
			};
		}

		// Token: 0x04002807 RID: 10247
		private PerformanceCounterPermissionEntryCollection innerCollection;
	}
}
