using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net.Mail
{
	// Token: 0x020006D4 RID: 1748
	[Serializable]
	public sealed class SmtpPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		// Token: 0x060035F6 RID: 13814 RVA: 0x000E63A0 File Offset: 0x000E53A0
		public SmtpPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.access = SmtpAccess.ConnectToUnrestrictedPort;
				this.unrestricted = true;
				return;
			}
			this.access = SmtpAccess.None;
		}

		// Token: 0x060035F7 RID: 13815 RVA: 0x000E63C2 File Offset: 0x000E53C2
		public SmtpPermission(bool unrestricted)
		{
			if (unrestricted)
			{
				this.access = SmtpAccess.ConnectToUnrestrictedPort;
				this.unrestricted = true;
				return;
			}
			this.access = SmtpAccess.None;
		}

		// Token: 0x060035F8 RID: 13816 RVA: 0x000E63E3 File Offset: 0x000E53E3
		public SmtpPermission(SmtpAccess access)
		{
			this.access = access;
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x060035F9 RID: 13817 RVA: 0x000E63F2 File Offset: 0x000E53F2
		public SmtpAccess Access
		{
			get
			{
				return this.access;
			}
		}

		// Token: 0x060035FA RID: 13818 RVA: 0x000E63FA File Offset: 0x000E53FA
		public void AddPermission(SmtpAccess access)
		{
			if (access > this.access)
			{
				this.access = access;
			}
		}

		// Token: 0x060035FB RID: 13819 RVA: 0x000E640C File Offset: 0x000E540C
		public bool IsUnrestricted()
		{
			return this.unrestricted;
		}

		// Token: 0x060035FC RID: 13820 RVA: 0x000E6414 File Offset: 0x000E5414
		public override IPermission Copy()
		{
			if (this.unrestricted)
			{
				return new SmtpPermission(true);
			}
			return new SmtpPermission(this.access);
		}

		// Token: 0x060035FD RID: 13821 RVA: 0x000E6430 File Offset: 0x000E5430
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			SmtpPermission smtpPermission = target as SmtpPermission;
			if (smtpPermission == null)
			{
				throw new ArgumentException(SR.GetString("net_perm_target"), "target");
			}
			if (this.unrestricted || smtpPermission.IsUnrestricted())
			{
				return new SmtpPermission(true);
			}
			return new SmtpPermission((this.access > smtpPermission.access) ? this.access : smtpPermission.access);
		}

		// Token: 0x060035FE RID: 13822 RVA: 0x000E64A0 File Offset: 0x000E54A0
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			SmtpPermission smtpPermission = target as SmtpPermission;
			if (smtpPermission == null)
			{
				throw new ArgumentException(SR.GetString("net_perm_target"), "target");
			}
			if (this.IsUnrestricted() && smtpPermission.IsUnrestricted())
			{
				return new SmtpPermission(true);
			}
			return new SmtpPermission((this.access < smtpPermission.access) ? this.access : smtpPermission.access);
		}

		// Token: 0x060035FF RID: 13823 RVA: 0x000E650C File Offset: 0x000E550C
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.access == SmtpAccess.None;
			}
			SmtpPermission smtpPermission = target as SmtpPermission;
			if (smtpPermission == null)
			{
				throw new ArgumentException(SR.GetString("net_perm_target"), "target");
			}
			return (!this.unrestricted || smtpPermission.IsUnrestricted()) && smtpPermission.access >= this.access;
		}

		// Token: 0x06003600 RID: 13824 RVA: 0x000E6568 File Offset: 0x000E5568
		public override void FromXml(SecurityElement securityElement)
		{
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
				this.access = SmtpAccess.ConnectToUnrestrictedPort;
				this.unrestricted = true;
				return;
			}
			text2 = securityElement.Attribute("Access");
			if (text2 == null)
			{
				return;
			}
			if (string.Compare(text2, "Connect", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.access = SmtpAccess.Connect;
				return;
			}
			if (string.Compare(text2, "ConnectToUnrestrictedPort", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.access = SmtpAccess.ConnectToUnrestrictedPort;
				return;
			}
			if (string.Compare(text2, "None", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.access = SmtpAccess.None;
				return;
			}
			throw new ArgumentException(SR.GetString("net_perm_invalid_val_in_element"), "Access");
		}

		// Token: 0x06003601 RID: 13825 RVA: 0x000E668C File Offset: 0x000E568C
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
			if (this.access == SmtpAccess.Connect)
			{
				securityElement.AddAttribute("Access", "Connect");
			}
			else if (this.access == SmtpAccess.ConnectToUnrestrictedPort)
			{
				securityElement.AddAttribute("Access", "ConnectToUnrestrictedPort");
			}
			return securityElement;
		}

		// Token: 0x04003125 RID: 12581
		private SmtpAccess access;

		// Token: 0x04003126 RID: 12582
		private bool unrestricted;
	}
}
