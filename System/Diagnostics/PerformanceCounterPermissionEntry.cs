using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x02000772 RID: 1906
	[Serializable]
	public class PerformanceCounterPermissionEntry
	{
		// Token: 0x06003AB2 RID: 15026 RVA: 0x000F9998 File Offset: 0x000F8998
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

		// Token: 0x06003AB3 RID: 15027 RVA: 0x000F9A37 File Offset: 0x000F8A37
		internal PerformanceCounterPermissionEntry(ResourcePermissionBaseEntry baseEntry)
		{
			this.permissionAccess = (PerformanceCounterPermissionAccess)baseEntry.PermissionAccess;
			this.machineName = baseEntry.PermissionAccessPath[0];
			this.categoryName = baseEntry.PermissionAccessPath[1];
		}

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x06003AB4 RID: 15028 RVA: 0x000F9A67 File Offset: 0x000F8A67
		public string CategoryName
		{
			get
			{
				return this.categoryName;
			}
		}

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x06003AB5 RID: 15029 RVA: 0x000F9A6F File Offset: 0x000F8A6F
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x06003AB6 RID: 15030 RVA: 0x000F9A77 File Offset: 0x000F8A77
		public PerformanceCounterPermissionAccess PermissionAccess
		{
			get
			{
				return this.permissionAccess;
			}
		}

		// Token: 0x06003AB7 RID: 15031 RVA: 0x000F9A80 File Offset: 0x000F8A80
		internal ResourcePermissionBaseEntry GetBaseEntry()
		{
			return new ResourcePermissionBaseEntry((int)this.PermissionAccess, new string[]
			{
				this.MachineName,
				this.CategoryName
			});
		}

		// Token: 0x04003366 RID: 13158
		private string categoryName;

		// Token: 0x04003367 RID: 13159
		private string machineName;

		// Token: 0x04003368 RID: 13160
		private PerformanceCounterPermissionAccess permissionAccess;
	}
}
