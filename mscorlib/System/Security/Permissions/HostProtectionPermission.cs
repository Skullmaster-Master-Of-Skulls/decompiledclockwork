using System;
using System.Globalization;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x02000633 RID: 1587
	[Serializable]
	internal sealed class HostProtectionPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x06003948 RID: 14664 RVA: 0x000C15BF File Offset: 0x000C05BF
		public HostProtectionPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.Resources = HostProtectionResource.All;
				return;
			}
			if (state == PermissionState.None)
			{
				this.Resources = HostProtectionResource.None;
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
		}

		// Token: 0x06003949 RID: 14665 RVA: 0x000C15F1 File Offset: 0x000C05F1
		public HostProtectionPermission(HostProtectionResource resources)
		{
			this.Resources = resources;
		}

		// Token: 0x0600394A RID: 14666 RVA: 0x000C1600 File Offset: 0x000C0600
		public bool IsUnrestricted()
		{
			return this.Resources == HostProtectionResource.All;
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x0600394C RID: 14668 RVA: 0x000C165B File Offset: 0x000C065B
		// (set) Token: 0x0600394B RID: 14667 RVA: 0x000C1610 File Offset: 0x000C0610
		public HostProtectionResource Resources
		{
			get
			{
				return this.m_resources;
			}
			set
			{
				if (value < HostProtectionResource.None || value > HostProtectionResource.All)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Arg_EnumIllegalVal"), new object[]
					{
						(int)value
					}));
				}
				this.m_resources = value;
			}
		}

		// Token: 0x0600394D RID: 14669 RVA: 0x000C1664 File Offset: 0x000C0664
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.m_resources == HostProtectionResource.None;
			}
			if (base.GetType() != target.GetType())
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
				{
					base.GetType().FullName
				}));
			}
			return (this.m_resources & ((HostProtectionPermission)target).m_resources) == this.m_resources;
		}

		// Token: 0x0600394E RID: 14670 RVA: 0x000C16D8 File Offset: 0x000C06D8
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			if (base.GetType() != target.GetType())
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
				{
					base.GetType().FullName
				}));
			}
			HostProtectionResource resources = this.m_resources | ((HostProtectionPermission)target).m_resources;
			return new HostProtectionPermission(resources);
		}

		// Token: 0x0600394F RID: 14671 RVA: 0x000C1748 File Offset: 0x000C0748
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			if (base.GetType() != target.GetType())
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
				{
					base.GetType().FullName
				}));
			}
			HostProtectionResource hostProtectionResource = this.m_resources & ((HostProtectionPermission)target).m_resources;
			if (hostProtectionResource == HostProtectionResource.None)
			{
				return null;
			}
			return new HostProtectionPermission(hostProtectionResource);
		}

		// Token: 0x06003950 RID: 14672 RVA: 0x000C17B6 File Offset: 0x000C07B6
		public override IPermission Copy()
		{
			return new HostProtectionPermission(this.m_resources);
		}

		// Token: 0x06003951 RID: 14673 RVA: 0x000C17C4 File Offset: 0x000C07C4
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, base.GetType().FullName);
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				securityElement.AddAttribute("Resources", XMLUtil.BitFieldEnumToString(typeof(HostProtectionResource), this.Resources));
			}
			return securityElement;
		}

		// Token: 0x06003952 RID: 14674 RVA: 0x000C1824 File Offset: 0x000C0824
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.ValidateElement(esd, this);
			if (XMLUtil.IsUnrestricted(esd))
			{
				this.Resources = HostProtectionResource.All;
				return;
			}
			string text = esd.Attribute("Resources");
			if (text == null)
			{
				this.Resources = HostProtectionResource.None;
				return;
			}
			this.Resources = (HostProtectionResource)Enum.Parse(typeof(HostProtectionResource), text);
		}

		// Token: 0x06003953 RID: 14675 RVA: 0x000C187E File Offset: 0x000C087E
		int IBuiltInPermission.GetTokenIndex()
		{
			return HostProtectionPermission.GetTokenIndex();
		}

		// Token: 0x06003954 RID: 14676 RVA: 0x000C1885 File Offset: 0x000C0885
		internal static int GetTokenIndex()
		{
			return 9;
		}

		// Token: 0x04001DB1 RID: 7601
		internal static HostProtectionResource protectedResources;

		// Token: 0x04001DB2 RID: 7602
		private HostProtectionResource m_resources;
	}
}
