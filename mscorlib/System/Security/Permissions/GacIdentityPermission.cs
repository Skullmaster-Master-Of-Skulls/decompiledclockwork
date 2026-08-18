using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x0200065C RID: 1628
	[ComVisible(true)]
	[Serializable]
	public sealed class GacIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		// Token: 0x06003AB8 RID: 15032 RVA: 0x000C64AF File Offset: 0x000C54AF
		public GacIdentityPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				if (!CodeAccessSecurityEngine.DoesFullTrustMeanFullTrust())
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_UnrestrictedIdentityPermission"));
				}
				return;
			}
			else
			{
				if (state == PermissionState.None)
				{
					return;
				}
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
			}
		}

		// Token: 0x06003AB9 RID: 15033 RVA: 0x000C64E6 File Offset: 0x000C54E6
		public GacIdentityPermission()
		{
		}

		// Token: 0x06003ABA RID: 15034 RVA: 0x000C64EE File Offset: 0x000C54EE
		public override IPermission Copy()
		{
			return new GacIdentityPermission();
		}

		// Token: 0x06003ABB RID: 15035 RVA: 0x000C64F8 File Offset: 0x000C54F8
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return false;
			}
			if (!(target is GacIdentityPermission))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
				{
					base.GetType().FullName
				}));
			}
			return true;
		}

		// Token: 0x06003ABC RID: 15036 RVA: 0x000C6544 File Offset: 0x000C5544
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			if (!(target is GacIdentityPermission))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
				{
					base.GetType().FullName
				}));
			}
			return this.Copy();
		}

		// Token: 0x06003ABD RID: 15037 RVA: 0x000C6594 File Offset: 0x000C5594
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			if (!(target is GacIdentityPermission))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
				{
					base.GetType().FullName
				}));
			}
			return this.Copy();
		}

		// Token: 0x06003ABE RID: 15038 RVA: 0x000C65EC File Offset: 0x000C55EC
		public override SecurityElement ToXml()
		{
			return CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.GacIdentityPermission");
		}

		// Token: 0x06003ABF RID: 15039 RVA: 0x000C6606 File Offset: 0x000C5606
		public override void FromXml(SecurityElement securityElement)
		{
			CodeAccessPermission.ValidateElement(securityElement, this);
		}

		// Token: 0x06003AC0 RID: 15040 RVA: 0x000C660F File Offset: 0x000C560F
		int IBuiltInPermission.GetTokenIndex()
		{
			return GacIdentityPermission.GetTokenIndex();
		}

		// Token: 0x06003AC1 RID: 15041 RVA: 0x000C6616 File Offset: 0x000C5616
		internal static int GetTokenIndex()
		{
			return 15;
		}
	}
}
