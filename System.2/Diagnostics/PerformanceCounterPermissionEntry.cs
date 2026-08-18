using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x020004EC RID: 1260
	[Serializable]
	public class PerformanceCounterPermissionEntry
	{
		// Token: 0x06002F95 RID: 12181 RVA: 0x000D71D8 File Offset: 0x000D53D8
		public PerformanceCounterPermissionEntry(PerformanceCounterPermissionAccess permissionAccess, string machineName, string categoryName)
		{
			if (categoryName == null)
			{
				throw new ArgumentNullException("categoryName");
			}
			if ((permissionAccess & (PerformanceCounterPermissionAccess)(-8)) != PerformanceCounterPermissionAccess.None)
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[]
				{
					"permissionAccess",
					permissionAccess
				}));
			}
			if (machineName == null)
			{
				throw new ArgumentNullException("machineName");
			}
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
			this.categoryName = categoryName;
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x000D7273 File Offset: 0x000D5473
		internal PerformanceCounterPermissionEntry(ResourcePermissionBaseEntry baseEntry)
		{
			this.permissionAccess = (PerformanceCounterPermissionAccess)baseEntry.PermissionAccess;
			this.machineName = baseEntry.PermissionAccessPath[0];
			this.categoryName = baseEntry.PermissionAccessPath[1];
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06002F97 RID: 12183 RVA: 0x000D72A3 File Offset: 0x000D54A3
		public string CategoryName
		{
			get
			{
				return this.categoryName;
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x06002F98 RID: 12184 RVA: 0x000D72AB File Offset: 0x000D54AB
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x06002F99 RID: 12185 RVA: 0x000D72B3 File Offset: 0x000D54B3
		public PerformanceCounterPermissionAccess PermissionAccess
		{
			get
			{
				return this.permissionAccess;
			}
		}

		// Token: 0x06002F9A RID: 12186 RVA: 0x000D72BC File Offset: 0x000D54BC
		internal ResourcePermissionBaseEntry GetBaseEntry()
		{
			return new ResourcePermissionBaseEntry((int)this.PermissionAccess, new string[]
			{
				this.MachineName,
				this.CategoryName
			});
		}

		// Token: 0x04002812 RID: 10258
		private string categoryName;

		// Token: 0x04002813 RID: 10259
		private string machineName;

		// Token: 0x04002814 RID: 10260
		private PerformanceCounterPermissionAccess permissionAccess;
	}
}
