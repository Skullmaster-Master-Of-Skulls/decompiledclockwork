using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Permissions
{
	// Token: 0x0200065A RID: 1626
	[ComVisible(true)]
	[Serializable]
	public sealed class ZoneIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		// Token: 0x06003AA5 RID: 15013 RVA: 0x000C603C File Offset: 0x000C503C
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				if (this.m_serializedPermission != null)
				{
					this.FromXml(SecurityElement.FromString(this.m_serializedPermission));
					this.m_serializedPermission = null;
					return;
				}
				this.SecurityZone = this.m_zone;
				this.m_zone = SecurityZone.NoZone;
			}
		}

		// Token: 0x06003AA6 RID: 15014 RVA: 0x000C608C File Offset: 0x000C508C
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				this.m_serializedPermission = this.ToXml().ToString();
				this.m_zone = this.SecurityZone;
			}
		}

		// Token: 0x06003AA7 RID: 15015 RVA: 0x000C60BA File Offset: 0x000C50BA
		[OnSerialized]
		private void OnSerialized(StreamingContext ctx)
		{
			if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				this.m_serializedPermission = null;
				this.m_zone = SecurityZone.NoZone;
			}
		}

		// Token: 0x06003AA8 RID: 15016 RVA: 0x000C60DC File Offset: 0x000C50DC
		public ZoneIdentityPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				if (CodeAccessSecurityEngine.DoesFullTrustMeanFullTrust())
				{
					this.m_zones = 31U;
					return;
				}
				throw new ArgumentException(Environment.GetResourceString("Argument_UnrestrictedIdentityPermission"));
			}
			else
			{
				if (state == PermissionState.None)
				{
					this.m_zones = 0U;
					return;
				}
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
			}
		}

		// Token: 0x06003AA9 RID: 15017 RVA: 0x000C6134 File Offset: 0x000C5134
		public ZoneIdentityPermission(SecurityZone zone)
		{
			this.SecurityZone = zone;
		}

		// Token: 0x06003AAA RID: 15018 RVA: 0x000C614A File Offset: 0x000C514A
		internal ZoneIdentityPermission(uint zones)
		{
			this.m_zones = (zones & 31U);
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06003AAC RID: 15020 RVA: 0x000C6184 File Offset: 0x000C5184
		// (set) Token: 0x06003AAB RID: 15019 RVA: 0x000C6163 File Offset: 0x000C5163
		public SecurityZone SecurityZone
		{
			get
			{
				SecurityZone securityZone = SecurityZone.NoZone;
				int num = 0;
				for (uint num2 = 1U; num2 < 31U; num2 <<= 1)
				{
					if ((this.m_zones & num2) != 0U)
					{
						if (securityZone != SecurityZone.NoZone)
						{
							return SecurityZone.NoZone;
						}
						securityZone = (SecurityZone)num;
					}
					num++;
				}
				return securityZone;
			}
			set
			{
				ZoneIdentityPermission.VerifyZone(value);
				if (value == SecurityZone.NoZone)
				{
					this.m_zones = 0U;
					return;
				}
				this.m_zones = 1U << (int)value;
			}
		}

		// Token: 0x06003AAD RID: 15021 RVA: 0x000C61BB File Offset: 0x000C51BB
		private static void VerifyZone(SecurityZone zone)
		{
			if (zone < SecurityZone.NoZone || zone > SecurityZone.Untrusted)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IllegalZone"));
			}
		}

		// Token: 0x06003AAE RID: 15022 RVA: 0x000C61D5 File Offset: 0x000C51D5
		public override IPermission Copy()
		{
			return new ZoneIdentityPermission(this.m_zones);
		}

		// Token: 0x06003AAF RID: 15023 RVA: 0x000C61E4 File Offset: 0x000C51E4
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.m_zones == 0U;
			}
			ZoneIdentityPermission zoneIdentityPermission = target as ZoneIdentityPermission;
			if (zoneIdentityPermission == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
				{
					base.GetType().FullName
				}));
			}
			return (this.m_zones & zoneIdentityPermission.m_zones) == this.m_zones;
		}

		// Token: 0x06003AB0 RID: 15024 RVA: 0x000C6250 File Offset: 0x000C5250
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			ZoneIdentityPermission zoneIdentityPermission = target as ZoneIdentityPermission;
			if (zoneIdentityPermission == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
				{
					base.GetType().FullName
				}));
			}
			uint num = this.m_zones & zoneIdentityPermission.m_zones;
			if (num == 0U)
			{
				return null;
			}
			return new ZoneIdentityPermission(num);
		}

		// Token: 0x06003AB1 RID: 15025 RVA: 0x000C62B8 File Offset: 0x000C52B8
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				if (this.m_zones == 0U)
				{
					return null;
				}
				return this.Copy();
			}
			else
			{
				ZoneIdentityPermission zoneIdentityPermission = target as ZoneIdentityPermission;
				if (zoneIdentityPermission == null)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
					{
						base.GetType().FullName
					}));
				}
				return new ZoneIdentityPermission(this.m_zones | zoneIdentityPermission.m_zones);
			}
		}

		// Token: 0x06003AB2 RID: 15026 RVA: 0x000C6328 File Offset: 0x000C5328
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.ZoneIdentityPermission");
			if (this.SecurityZone != SecurityZone.NoZone)
			{
				securityElement.AddAttribute("Zone", Enum.GetName(typeof(SecurityZone), this.SecurityZone));
			}
			else
			{
				int num = 0;
				for (uint num2 = 1U; num2 < 31U; num2 <<= 1)
				{
					if ((this.m_zones & num2) != 0U)
					{
						SecurityElement securityElement2 = new SecurityElement("Zone");
						securityElement2.AddAttribute("Zone", Enum.GetName(typeof(SecurityZone), (SecurityZone)num));
						securityElement.AddChild(securityElement2);
					}
					num++;
				}
			}
			return securityElement;
		}

		// Token: 0x06003AB3 RID: 15027 RVA: 0x000C63C4 File Offset: 0x000C53C4
		public override void FromXml(SecurityElement esd)
		{
			this.m_zones = 0U;
			CodeAccessPermission.ValidateElement(esd, this);
			string text = esd.Attribute("Zone");
			if (text != null)
			{
				this.SecurityZone = (SecurityZone)Enum.Parse(typeof(SecurityZone), text);
			}
			if (esd.Children != null)
			{
				foreach (object obj in esd.Children)
				{
					SecurityElement securityElement = (SecurityElement)obj;
					text = securityElement.Attribute("Zone");
					int num = (int)Enum.Parse(typeof(SecurityZone), text);
					if (num != -1)
					{
						this.m_zones |= 1U << num;
					}
				}
			}
		}

		// Token: 0x06003AB4 RID: 15028 RVA: 0x000C6494 File Offset: 0x000C5494
		int IBuiltInPermission.GetTokenIndex()
		{
			return ZoneIdentityPermission.GetTokenIndex();
		}

		// Token: 0x06003AB5 RID: 15029 RVA: 0x000C649B File Offset: 0x000C549B
		internal static int GetTokenIndex()
		{
			return 14;
		}

		// Token: 0x04001E6A RID: 7786
		private const uint AllZones = 31U;

		// Token: 0x04001E6B RID: 7787
		[OptionalField(VersionAdded = 2)]
		private uint m_zones;

		// Token: 0x04001E6C RID: 7788
		[OptionalField(VersionAdded = 2)]
		private string m_serializedPermission;

		// Token: 0x04001E6D RID: 7789
		private SecurityZone m_zone = SecurityZone.NoZone;
	}
}
