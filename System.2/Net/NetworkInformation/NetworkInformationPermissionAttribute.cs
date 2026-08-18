using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002E1 RID: 737
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class NetworkInformationPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x060019F7 RID: 6647 RVA: 0x0007E566 File Offset: 0x0007C766
		public NetworkInformationPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060019F8 RID: 6648 RVA: 0x0007E56F File Offset: 0x0007C76F
		// (set) Token: 0x060019F9 RID: 6649 RVA: 0x0007E577 File Offset: 0x0007C777
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

		// Token: 0x060019FA RID: 6650 RVA: 0x0007E580 File Offset: 0x0007C780
		public override IPermission CreatePermission()
		{
			NetworkInformationPermission networkInformationPermission;
			if (base.Unrestricted)
			{
				networkInformationPermission = new NetworkInformationPermission(PermissionState.Unrestricted);
			}
			else
			{
				networkInformationPermission = new NetworkInformationPermission(PermissionState.None);
				if (this.access != null)
				{
					if (string.Compare(this.access, "Read", StringComparison.OrdinalIgnoreCase) == 0)
					{
						networkInformationPermission.AddPermission(NetworkInformationAccess.Read);
					}
					else if (string.Compare(this.access, "Ping", StringComparison.OrdinalIgnoreCase) == 0)
					{
						networkInformationPermission.AddPermission(NetworkInformationAccess.Ping);
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
						networkInformationPermission.AddPermission(NetworkInformationAccess.None);
					}
				}
			}
			return networkInformationPermission;
		}

		// Token: 0x04001A59 RID: 6745
		private const string strAccess = "Access";

		// Token: 0x04001A5A RID: 6746
		private string access;
	}
}
