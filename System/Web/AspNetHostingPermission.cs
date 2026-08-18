using System;
using System.Security;
using System.Security.Permissions;

namespace System.Web
{
	// Token: 0x0200073C RID: 1852
	[Serializable]
	public sealed class AspNetHostingPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		// Token: 0x06003877 RID: 14455 RVA: 0x000EDFF8 File Offset: 0x000ECFF8
		internal static void VerifyAspNetHostingPermissionLevel(AspNetHostingPermissionLevel level, string arg)
		{
			if (level <= AspNetHostingPermissionLevel.Low)
			{
				if (level == AspNetHostingPermissionLevel.None || level == AspNetHostingPermissionLevel.Minimal || level == AspNetHostingPermissionLevel.Low)
				{
					return;
				}
			}
			else if (level == AspNetHostingPermissionLevel.Medium || level == AspNetHostingPermissionLevel.High || level == AspNetHostingPermissionLevel.Unrestricted)
			{
				return;
			}
			throw new ArgumentException(arg);
		}

		// Token: 0x06003878 RID: 14456 RVA: 0x000EE048 File Offset: 0x000ED048
		public AspNetHostingPermission(PermissionState state)
		{
			switch (state)
			{
			case PermissionState.None:
				this._level = AspNetHostingPermissionLevel.None;
				return;
			case PermissionState.Unrestricted:
				this._level = AspNetHostingPermissionLevel.Unrestricted;
				return;
			default:
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					state.ToString(),
					"state"
				}));
			}
		}

		// Token: 0x06003879 RID: 14457 RVA: 0x000EE0AF File Offset: 0x000ED0AF
		public AspNetHostingPermission(AspNetHostingPermissionLevel level)
		{
			AspNetHostingPermission.VerifyAspNetHostingPermissionLevel(level, "level");
			this._level = level;
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x0600387A RID: 14458 RVA: 0x000EE0C9 File Offset: 0x000ED0C9
		// (set) Token: 0x0600387B RID: 14459 RVA: 0x000EE0D1 File Offset: 0x000ED0D1
		public AspNetHostingPermissionLevel Level
		{
			get
			{
				return this._level;
			}
			set
			{
				AspNetHostingPermission.VerifyAspNetHostingPermissionLevel(value, "Level");
				this._level = value;
			}
		}

		// Token: 0x0600387C RID: 14460 RVA: 0x000EE0E5 File Offset: 0x000ED0E5
		public bool IsUnrestricted()
		{
			return this._level == AspNetHostingPermissionLevel.Unrestricted;
		}

		// Token: 0x0600387D RID: 14461 RVA: 0x000EE0F4 File Offset: 0x000ED0F4
		public override IPermission Copy()
		{
			return new AspNetHostingPermission(this._level);
		}

		// Token: 0x0600387E RID: 14462 RVA: 0x000EE104 File Offset: 0x000ED104
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			if (target.GetType() != typeof(AspNetHostingPermission))
			{
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					(target == null) ? "null" : target.ToString(),
					"target"
				}));
			}
			AspNetHostingPermission aspNetHostingPermission = (AspNetHostingPermission)target;
			if (this.Level >= aspNetHostingPermission.Level)
			{
				return new AspNetHostingPermission(this.Level);
			}
			return new AspNetHostingPermission(aspNetHostingPermission.Level);
		}

		// Token: 0x0600387F RID: 14463 RVA: 0x000EE18C File Offset: 0x000ED18C
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			if (target.GetType() != typeof(AspNetHostingPermission))
			{
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					(target == null) ? "null" : target.ToString(),
					"target"
				}));
			}
			AspNetHostingPermission aspNetHostingPermission = (AspNetHostingPermission)target;
			if (this.Level <= aspNetHostingPermission.Level)
			{
				return new AspNetHostingPermission(this.Level);
			}
			return new AspNetHostingPermission(aspNetHostingPermission.Level);
		}

		// Token: 0x06003880 RID: 14464 RVA: 0x000EE210 File Offset: 0x000ED210
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this._level == AspNetHostingPermissionLevel.None;
			}
			if (target.GetType() != typeof(AspNetHostingPermission))
			{
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					(target == null) ? "null" : target.ToString(),
					"target"
				}));
			}
			AspNetHostingPermission aspNetHostingPermission = (AspNetHostingPermission)target;
			return this.Level <= aspNetHostingPermission.Level;
		}

		// Token: 0x06003881 RID: 14465 RVA: 0x000EE288 File Offset: 0x000ED288
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw new ArgumentNullException(SR.GetString("AspNetHostingPermissionBadXml", new object[]
				{
					"securityElement"
				}));
			}
			if (!securityElement.Tag.Equals("IPermission"))
			{
				throw new ArgumentException(SR.GetString("AspNetHostingPermissionBadXml", new object[]
				{
					"securityElement"
				}));
			}
			string text = securityElement.Attribute("class");
			if (text == null)
			{
				throw new ArgumentException(SR.GetString("AspNetHostingPermissionBadXml", new object[]
				{
					"securityElement"
				}));
			}
			if (text.IndexOf(base.GetType().FullName, StringComparison.Ordinal) < 0)
			{
				throw new ArgumentException(SR.GetString("AspNetHostingPermissionBadXml", new object[]
				{
					"securityElement"
				}));
			}
			string strA = securityElement.Attribute("version");
			if (string.Compare(strA, "1", StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new ArgumentException(SR.GetString("AspNetHostingPermissionBadXml", new object[]
				{
					"version"
				}));
			}
			string text2 = securityElement.Attribute("Level");
			if (text2 == null)
			{
				this._level = AspNetHostingPermissionLevel.None;
				return;
			}
			this._level = (AspNetHostingPermissionLevel)Enum.Parse(typeof(AspNetHostingPermissionLevel), text2);
		}

		// Token: 0x06003882 RID: 14466 RVA: 0x000EE3C8 File Offset: 0x000ED3C8
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", base.GetType().FullName + ", " + base.GetType().Module.Assembly.FullName.Replace('"', '\''));
			securityElement.AddAttribute("version", "1");
			securityElement.AddAttribute("Level", Enum.GetName(typeof(AspNetHostingPermissionLevel), this._level));
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x04003259 RID: 12889
		private AspNetHostingPermissionLevel _level;
	}
}
