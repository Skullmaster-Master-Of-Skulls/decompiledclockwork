using System;
using System.ComponentModel;
using System.Security;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x020004EB RID: 1259
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Event, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public class PerformanceCounterPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002F8D RID: 12173 RVA: 0x000D711E File Offset: 0x000D531E
		public PerformanceCounterPermissionAttribute(SecurityAction action) : base(action)
		{
			this.categoryName = "*";
			this.machineName = ".";
			this.permissionAccess = PerformanceCounterPermissionAccess.Write;
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06002F8E RID: 12174 RVA: 0x000D7144 File Offset: 0x000D5344
		// (set) Token: 0x06002F8F RID: 12175 RVA: 0x000D714C File Offset: 0x000D534C
		public string CategoryName
		{
			get
			{
				return this.categoryName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.categoryName = value;
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06002F90 RID: 12176 RVA: 0x000D7163 File Offset: 0x000D5363
		// (set) Token: 0x06002F91 RID: 12177 RVA: 0x000D716B File Offset: 0x000D536B
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

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06002F92 RID: 12178 RVA: 0x000D719E File Offset: 0x000D539E
		// (set) Token: 0x06002F93 RID: 12179 RVA: 0x000D71A6 File Offset: 0x000D53A6
		public PerformanceCounterPermissionAccess PermissionAccess
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

		// Token: 0x06002F94 RID: 12180 RVA: 0x000D71AF File Offset: 0x000D53AF
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new PerformanceCounterPermission(PermissionState.Unrestricted);
			}
			return new PerformanceCounterPermission(this.PermissionAccess, this.MachineName, this.CategoryName);
		}

		// Token: 0x0400280F RID: 10255
		private string categoryName;

		// Token: 0x04002810 RID: 10256
		private string machineName;

		// Token: 0x04002811 RID: 10257
		private PerformanceCounterPermissionAccess permissionAccess;
	}
}
