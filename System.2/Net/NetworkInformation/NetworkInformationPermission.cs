using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002E2 RID: 738
	[Serializable]
	public sealed class NetworkInformationPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		// Token: 0x060019FB RID: 6651 RVA: 0x0007E62E File Offset: 0x0007C82E
		public NetworkInformationPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.access = (NetworkInformationAccess.Read | NetworkInformationAccess.Ping);
				this.unrestricted = true;
				return;
			}
			this.access = NetworkInformationAccess.None;
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x0007E650 File Offset: 0x0007C850
		internal NetworkInformationPermission(bool unrestricted)
		{
			if (unrestricted)
			{
				this.access = (NetworkInformationAccess.Read | NetworkInformationAccess.Ping);
				unrestricted = true;
				return;
			}
			this.access = NetworkInformationAccess.None;
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x0007E66D File Offset: 0x0007C86D
		public NetworkInformationPermission(NetworkInformationAccess access)
		{
			this.access = access;
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060019FE RID: 6654 RVA: 0x0007E67C File Offset: 0x0007C87C
		public NetworkInformationAccess Access
		{
			get
			{
				return this.access;
			}
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x0007E684 File Offset: 0x0007C884
		public void AddPermission(NetworkInformationAccess access)
		{
			this.access |= access;
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x0007E694 File Offset: 0x0007C894
		public bool IsUnrestricted()
		{
			return this.unrestricted;
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x0007E69C File Offset: 0x0007C89C
		public override IPermission Copy()
		{
			if (this.unrestricted)
			{
				return new NetworkInformationPermission(true);
			}
			return new NetworkInformationPermission(this.access);
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x0007E6B8 File Offset: 0x0007C8B8
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			NetworkInformationPermission networkInformationPermission = target as NetworkInformationPermission;
			if (networkInformationPermission == null)
			{
				throw new ArgumentException(SR.GetString("net_perm_target"), "target");
			}
			if (this.unrestricted || networkInformationPermission.IsUnrestricted())
			{
				return new NetworkInformationPermission(true);
			}
			return new NetworkInformationPermission(this.access | networkInformationPermission.access);
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x0007E718 File Offset: 0x0007C918
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			NetworkInformationPermission networkInformationPermission = target as NetworkInformationPermission;
			if (networkInformationPermission == null)
			{
				throw new ArgumentException(SR.GetString("net_perm_target"), "target");
			}
			if (this.unrestricted && networkInformationPermission.IsUnrestricted())
			{
				return new NetworkInformationPermission(true);
			}
			return new NetworkInformationPermission(this.access & networkInformationPermission.access);
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x0007E774 File Offset: 0x0007C974
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.access == NetworkInformationAccess.None;
			}
			NetworkInformationPermission networkInformationPermission = target as NetworkInformationPermission;
			if (networkInformationPermission == null)
			{
				throw new ArgumentException(SR.GetString("net_perm_target"), "target");
			}
			return (!this.unrestricted || networkInformationPermission.IsUnrestricted()) && (this.access & networkInformationPermission.access) == this.access;
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x0007E7D8 File Offset: 0x0007C9D8
		public override void FromXml(SecurityElement securityElement)
		{
			this.access = NetworkInformationAccess.None;
			if (securityElement == null)
			{
				throw new ArgumentNullException("securityElement");
			}
			if (!securityElement.Tag.Equals("IPermission"))
			{
				throw new ArgumentException(SR.GetString("net_not_ipermission"), "securityElement");
			}
			string text = securityElement.Attribute("class");
			if (text == null)
			{
				throw new ArgumentException(SR.GetString("net_no_classname"), "securityElement");
			}
			if (text.IndexOf(base.GetType().FullName) < 0)
			{
				throw new ArgumentException(SR.GetString("net_no_typename"), "securityElement");
			}
			string text2 = securityElement.Attribute("Unrestricted");
			if (text2 != null && string.Compare(text2, "true", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.access = (NetworkInformationAccess.Read | NetworkInformationAccess.Ping);
				this.unrestricted = true;
				return;
			}
			if (securityElement.Children != null)
			{
				foreach (object obj in securityElement.Children)
				{
					SecurityElement securityElement2 = (SecurityElement)obj;
					text2 = securityElement2.Attribute("Access");
					if (string.Compare(text2, "Read", StringComparison.OrdinalIgnoreCase) == 0)
					{
						this.access |= NetworkInformationAccess.Read;
					}
					else if (string.Compare(text2, "Ping", StringComparison.OrdinalIgnoreCase) == 0)
					{
						this.access |= NetworkInformationAccess.Ping;
					}
				}
			}
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x0007E930 File Offset: 0x0007CB30
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", base.GetType().FullName + ", " + base.GetType().Module.Assembly.FullName.Replace('"', '\''));
			securityElement.AddAttribute("version", "1");
			if (this.unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
				return securityElement;
			}
			if ((this.access & NetworkInformationAccess.Read) > NetworkInformationAccess.None)
			{
				SecurityElement securityElement2 = new SecurityElement("NetworkInformationAccess");
				securityElement2.AddAttribute("Access", "Read");
				securityElement.AddChild(securityElement2);
			}
			if ((this.access & NetworkInformationAccess.Ping) > NetworkInformationAccess.None)
			{
				SecurityElement securityElement3 = new SecurityElement("NetworkInformationAccess");
				securityElement3.AddAttribute("Access", "Ping");
				securityElement.AddChild(securityElement3);
			}
			return securityElement;
		}

		// Token: 0x04001A5B RID: 6747
		private NetworkInformationAccess access;

		// Token: 0x04001A5C RID: 6748
		private bool unrestricted;
	}
}
