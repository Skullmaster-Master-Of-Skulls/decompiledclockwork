using System;
using System.ComponentModel;
using System.Security;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x02000771 RID: 1905
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Event, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public class PerformanceCounterPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06003AAA RID: 15018 RVA: 0x000F98CF File Offset: 0x000F88CF
		public PerformanceCounterPermissionAttribute(SecurityAction action) : base(action)
		{
			this.categoryName = "*";
			this.machineName = ".";
			this.permissionAccess = PerformanceCounterPermissionAccess.Write;
		}

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x06003AAB RID: 15019 RVA: 0x000F98F5 File Offset: 0x000F88F5
		// (set) Token: 0x06003AAC RID: 15020 RVA: 0x000F98FD File Offset: 0x000F88FD
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

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x06003AAD RID: 15021 RVA: 0x000F9914 File Offset: 0x000F8914
		// (set) Token: 0x06003AAE RID: 15022 RVA: 0x000F991C File Offset: 0x000F891C
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

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x06003AAF RID: 15023 RVA: 0x000F995C File Offset: 0x000F895C
		// (set) Token: 0x06003AB0 RID: 15024 RVA: 0x000F9964 File Offset: 0x000F8964
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

		// Token: 0x06003AB1 RID: 15025 RVA: 0x000F996D File Offset: 0x000F896D
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new PerformanceCounterPermission(PermissionState.Unrestricted);
			}
			return new PerformanceCounterPermission(this.PermissionAccess, this.MachineName, this.CategoryName);
		}

		// Token: 0x04003363 RID: 13155
		private string categoryName;

		// Token: 0x04003364 RID: 13156
		private string machineName;

		// Token: 0x04003365 RID: 13157
		private PerformanceCounterPermissionAccess permissionAccess;
	}
}
