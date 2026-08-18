using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net.Mail
{
	// Token: 0x020006D3 RID: 1747
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class SmtpPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x060035F2 RID: 13810 RVA: 0x000E62D5 File Offset: 0x000E52D5
		public SmtpPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x060035F3 RID: 13811 RVA: 0x000E62DE File Offset: 0x000E52DE
		// (set) Token: 0x060035F4 RID: 13812 RVA: 0x000E62E6 File Offset: 0x000E52E6
		public string Access
		{
			get
			{
				return this.access;
			}
			set
			{
				this.access = value;
			}
		}

		// Token: 0x060035F5 RID: 13813 RVA: 0x000E62F0 File Offset: 0x000E52F0
		public override IPermission CreatePermission()
		{
			SmtpPermission smtpPermission;
			if (base.Unrestricted)
			{
				smtpPermission = new SmtpPermission(PermissionState.Unrestricted);
			}
			else
			{
				smtpPermission = new SmtpPermission(PermissionState.None);
				if (this.access != null)
				{
					if (string.Compare(this.access, "Connect", StringComparison.OrdinalIgnoreCase) == 0)
					{
						smtpPermission.AddPermission(SmtpAccess.Connect);
					}
					else if (string.Compare(this.access, "ConnectToUnrestrictedPort", StringComparison.OrdinalIgnoreCase) == 0)
					{
						smtpPermission.AddPermission(SmtpAccess.ConnectToUnrestrictedPort);
					}
					else
					{
						if (string.Compare(this.access, "None", StringComparison.OrdinalIgnoreCase) != 0)
						{
							throw new ArgumentException(SR.GetString("net_perm_invalid_val", new object[]
							{
								"Access",
								this.access
							}));
						}
						smtpPermission.AddPermission(SmtpAccess.None);
					}
				}
			}
			return smtpPermission;
		}

		// Token: 0x04003123 RID: 12579
		private const string strAccess = "Access";

		// Token: 0x04003124 RID: 12580
		private string access;
	}
}
