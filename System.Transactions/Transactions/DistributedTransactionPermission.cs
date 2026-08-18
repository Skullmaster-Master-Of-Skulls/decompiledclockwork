using System;
using System.Globalization;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Transactions
{
	// Token: 0x0200006F RID: 111
	[Serializable]
	public sealed class DistributedTransactionPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		// Token: 0x06000317 RID: 791 RVA: 0x000358B4 File Offset: 0x00034CB4
		public DistributedTransactionPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.unrestricted = true;
				return;
			}
			this.unrestricted = false;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000358E4 File Offset: 0x00034CE4
		public bool IsUnrestricted()
		{
			return this.unrestricted;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00035904 File Offset: 0x00034D04
		public override IPermission Copy()
		{
			DistributedTransactionPermission distributedTransactionPermission = new DistributedTransactionPermission(PermissionState.None);
			if (this.IsUnrestricted())
			{
				distributedTransactionPermission.unrestricted = true;
			}
			else
			{
				distributedTransactionPermission.unrestricted = false;
			}
			return distributedTransactionPermission;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00035934 File Offset: 0x00034D34
		public override IPermission Intersect(IPermission target)
		{
			IPermission result;
			try
			{
				if (target == null)
				{
					result = null;
				}
				else
				{
					DistributedTransactionPermission distributedTransactionPermission = (DistributedTransactionPermission)target;
					if (!distributedTransactionPermission.IsUnrestricted())
					{
						result = distributedTransactionPermission;
					}
					else
					{
						result = this.Copy();
					}
				}
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(SR.GetString("ArgumentWrongType"), "target");
			}
			return result;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x000359A4 File Offset: 0x00034DA4
		public override IPermission Union(IPermission target)
		{
			IPermission result;
			try
			{
				if (target == null)
				{
					result = this.Copy();
				}
				else
				{
					DistributedTransactionPermission distributedTransactionPermission = (DistributedTransactionPermission)target;
					if (distributedTransactionPermission.IsUnrestricted())
					{
						result = distributedTransactionPermission;
					}
					else
					{
						result = this.Copy();
					}
				}
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(SR.GetString("ArgumentWrongType"), "target");
			}
			return result;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00035A14 File Offset: 0x00034E14
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return !this.unrestricted;
			}
			bool result;
			try
			{
				DistributedTransactionPermission distributedTransactionPermission = (DistributedTransactionPermission)target;
				if (!this.unrestricted)
				{
					result = true;
				}
				else if (distributedTransactionPermission.unrestricted)
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(SR.GetString("ArgumentWrongType"), "target");
			}
			return result;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00035A94 File Offset: 0x00034E94
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			Type type = base.GetType();
			StringBuilder stringBuilder = new StringBuilder(type.Assembly.ToString());
			stringBuilder.Replace('"', '\'');
			securityElement.AddAttribute("class", type.FullName + ", " + stringBuilder);
			securityElement.AddAttribute("version", "1");
			securityElement.AddAttribute("Unrestricted", this.unrestricted.ToString());
			return securityElement;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00035B14 File Offset: 0x00034F14
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw new ArgumentNullException("securityElement");
			}
			if (!securityElement.Tag.Equals("IPermission"))
			{
				throw new ArgumentException(SR.GetString("ArgumentWrongType"), "securityElement");
			}
			string text = securityElement.Attribute("Unrestricted");
			if (text != null)
			{
				this.unrestricted = Convert.ToBoolean(text, CultureInfo.InvariantCulture);
				return;
			}
			this.unrestricted = false;
		}

		// Token: 0x04000145 RID: 325
		private bool unrestricted;
	}
}
