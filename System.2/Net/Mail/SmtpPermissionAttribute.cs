using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net.Mail
{
	// Token: 0x02000291 RID: 657
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class SmtpPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06001881 RID: 6273 RVA: 0x0007C8F1 File Offset: 0x0007AAF1
		public SmtpPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x0007C8FA File Offset: 0x0007AAFA
		// (set) Token: 0x06001883 RID: 6275 RVA: 0x0007C902 File Offset: 0x0007AB02
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

		// Token: 0x06001884 RID: 6276 RVA: 0x0007C90C File Offset: 0x0007AB0C
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

		// Token: 0x04001867 RID: 6247
		private const string strAccess = "Access";

		// Token: 0x04001868 RID: 6248
		private string access;
	}
}
