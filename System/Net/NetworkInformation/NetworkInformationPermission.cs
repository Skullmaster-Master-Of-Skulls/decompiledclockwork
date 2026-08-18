using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200061B RID: 1563
	[Serializable]
	public sealed class NetworkInformationPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		// Token: 0x0600301F RID: 12319 RVA: 0x000CFCF8 File Offset: 0x000CECF8
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

		// Token: 0x06003020 RID: 12320 RVA: 0x000CFD1A File Offset: 0x000CED1A
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

		// Token: 0x06003021 RID: 12321 RVA: 0x000CFD37 File Offset: 0x000CED37
		public NetworkInformationPermission(NetworkInformationAccess access)
		{
			this.access = access;
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06003022 RID: 12322 RVA: 0x000CFD46 File Offset: 0x000CED46
		public NetworkInformationAccess Access
		{
			get
			{
				return this.access;
			}
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x000CFD4E File Offset: 0x000CED4E
		public void AddPermission(NetworkInformationAccess access)
		{
			this.access |= access;
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x000CFD5E File Offset: 0x000CED5E
		public bool IsUnrestricted()
		{
			return this.unrestricted;
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x000CFD66 File Offset: 0x000CED66
		public override IPermission Copy()
		{
			if (this.unrestricted)
			{
				return new NetworkInformationPermission(true);
			}
			return new NetworkInformationPermission(this.access);
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x000CFD84 File Offset: 0x000CED84
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

		// Token: 0x06003027 RID: 12327 RVA: 0x000CFDE4 File Offset: 0x000CEDE4
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

		// Token: 0x06003028 RID: 12328 RVA: 0x000CFE40 File Offset: 0x000CEE40
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

		// Token: 0x06003029 RID: 12329 RVA: 0x000CFEA4 File Offset: 0x000CEEA4
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

		// Token: 0x0600302A RID: 12330 RVA: 0x000CFFFC File Offset: 0x000CEFFC
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

		// Token: 0x04002DE6 RID: 11750
		private NetworkInformationAccess access;

		// Token: 0x04002DE7 RID: 11751
		private bool unrestricted;
	}
}
