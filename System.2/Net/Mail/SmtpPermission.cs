using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net.Mail
{
	// Token: 0x02000292 RID: 658
	[Serializable]
	public sealed class SmtpPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		// Token: 0x06001885 RID: 6277 RVA: 0x0007C9BA File Offset: 0x0007ABBA
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

		// Token: 0x06001886 RID: 6278 RVA: 0x0007C9DC File Offset: 0x0007ABDC
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

		// Token: 0x06001887 RID: 6279 RVA: 0x0007C9FD File Offset: 0x0007ABFD
		public SmtpPermission(SmtpAccess access)
		{
			this.access = access;
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001888 RID: 6280 RVA: 0x0007CA0C File Offset: 0x0007AC0C
		public SmtpAccess Access
		{
			get
			{
				return this.access;
			}
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x0007CA14 File Offset: 0x0007AC14
		public void AddPermission(SmtpAccess access)
		{
			if (access > this.access)
			{
				this.access = access;
			}
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x0007CA26 File Offset: 0x0007AC26
		public bool IsUnrestricted()
		{
			return this.unrestricted;
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x0007CA2E File Offset: 0x0007AC2E
		public override IPermission Copy()
		{
			if (this.unrestricted)
			{
				return new SmtpPermission(true);
			}
			return new SmtpPermission(this.access);
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x0007CA4C File Offset: 0x0007AC4C
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

		// Token: 0x0600188D RID: 6285 RVA: 0x0007CABC File Offset: 0x0007ACBC
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

		// Token: 0x0600188E RID: 6286 RVA: 0x0007CB28 File Offset: 0x0007AD28
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

		// Token: 0x0600188F RID: 6287 RVA: 0x0007CB84 File Offset: 0x0007AD84
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

		// Token: 0x06001890 RID: 6288 RVA: 0x0007CCA8 File Offset: 0x0007AEA8
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

		// Token: 0x04001869 RID: 6249
		private SmtpAccess access;

		// Token: 0x0400186A RID: 6250
		private bool unrestricted;
	}
}
