using System;
using System.Security;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x02000031 RID: 49
	[Serializable]
	public sealed class ConfigurationPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		// Token: 0x06000247 RID: 583 RVA: 0x00010880 File Offset: 0x0000EA80
		public ConfigurationPermission(PermissionState state)
		{
			if (state <= PermissionState.Unrestricted)
			{
				this._permissionState = state;
				return;
			}
			throw ExceptionUtil.ParameterInvalid("state");
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0001089E File Offset: 0x0000EA9E
		public bool IsUnrestricted()
		{
			return this._permissionState == PermissionState.Unrestricted;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000108A9 File Offset: 0x0000EAA9
		public override IPermission Copy()
		{
			return new ConfigurationPermission(this._permissionState);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000108B8 File Offset: 0x0000EAB8
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			if (target.GetType() != typeof(ConfigurationPermission))
			{
				throw ExceptionUtil.ParameterInvalid("target");
			}
			if (this._permissionState == PermissionState.Unrestricted)
			{
				return new ConfigurationPermission(PermissionState.Unrestricted);
			}
			ConfigurationPermission configurationPermission = (ConfigurationPermission)target;
			return new ConfigurationPermission(configurationPermission._permissionState);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00010914 File Offset: 0x0000EB14
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			if (target.GetType() != typeof(ConfigurationPermission))
			{
				throw ExceptionUtil.ParameterInvalid("target");
			}
			if (this._permissionState == PermissionState.None)
			{
				return new ConfigurationPermission(PermissionState.None);
			}
			ConfigurationPermission configurationPermission = (ConfigurationPermission)target;
			return new ConfigurationPermission(configurationPermission._permissionState);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0001096C File Offset: 0x0000EB6C
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this._permissionState == PermissionState.None;
			}
			if (target.GetType() != typeof(ConfigurationPermission))
			{
				throw ExceptionUtil.ParameterInvalid("target");
			}
			ConfigurationPermission configurationPermission = (ConfigurationPermission)target;
			return this._permissionState == PermissionState.None || configurationPermission._permissionState == PermissionState.Unrestricted;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000109C4 File Offset: 0x0000EBC4
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw new ArgumentNullException(SR.GetString("ConfigurationPermissionBadXml", new object[]
				{
					"securityElement"
				}));
			}
			if (!securityElement.Tag.Equals("IPermission"))
			{
				throw new ArgumentException(SR.GetString("ConfigurationPermissionBadXml", new object[]
				{
					"securityElement"
				}));
			}
			string text = securityElement.Attribute("class");
			if (text == null)
			{
				throw new ArgumentException(SR.GetString("ConfigurationPermissionBadXml", new object[]
				{
					"securityElement"
				}));
			}
			if (text.IndexOf(base.GetType().FullName, StringComparison.Ordinal) < 0)
			{
				throw new ArgumentException(SR.GetString("ConfigurationPermissionBadXml", new object[]
				{
					"securityElement"
				}));
			}
			string a = securityElement.Attribute("version");
			if (a != "1")
			{
				throw new ArgumentException(SR.GetString("ConfigurationPermissionBadXml", new object[]
				{
					"version"
				}));
			}
			string text2 = securityElement.Attribute("Unrestricted");
			if (text2 == null)
			{
				this._permissionState = PermissionState.None;
				return;
			}
			if (text2 == "true")
			{
				this._permissionState = PermissionState.Unrestricted;
				return;
			}
			if (!(text2 == "false"))
			{
				throw new ArgumentException(SR.GetString("ConfigurationPermissionBadXml", new object[]
				{
					"Unrestricted"
				}));
			}
			this._permissionState = PermissionState.None;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00010B1C File Offset: 0x0000ED1C
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", base.GetType().FullName + ", " + base.GetType().Module.Assembly.FullName.Replace('"', '\''));
			securityElement.AddAttribute("version", "1");
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x040001E3 RID: 483
		private PermissionState _permissionState;
	}
}
