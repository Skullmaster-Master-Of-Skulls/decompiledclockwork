using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x0200064D RID: 1613
	[ComVisible(true)]
	[Serializable]
	public sealed class ReflectionPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x06003A19 RID: 14873 RVA: 0x000C2DB4 File Offset: 0x000C1DB4
		public ReflectionPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.SetUnrestricted(true);
				return;
			}
			if (state == PermissionState.None)
			{
				this.SetUnrestricted(false);
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
		}

		// Token: 0x06003A1A RID: 14874 RVA: 0x000C2DE2 File Offset: 0x000C1DE2
		public ReflectionPermission(ReflectionPermissionFlag flag)
		{
			this.VerifyAccess(flag);
			this.SetUnrestricted(false);
			this.m_flags = flag;
		}

		// Token: 0x06003A1B RID: 14875 RVA: 0x000C2DFF File Offset: 0x000C1DFF
		private void SetUnrestricted(bool unrestricted)
		{
			if (unrestricted)
			{
				this.m_flags = (ReflectionPermissionFlag.TypeInformation | ReflectionPermissionFlag.MemberAccess | ReflectionPermissionFlag.ReflectionEmit | ReflectionPermissionFlag.RestrictedMemberAccess);
				return;
			}
			this.Reset();
		}

		// Token: 0x06003A1C RID: 14876 RVA: 0x000C2E13 File Offset: 0x000C1E13
		private void Reset()
		{
			this.m_flags = ReflectionPermissionFlag.NoFlags;
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06003A1E RID: 14878 RVA: 0x000C2E2C File Offset: 0x000C1E2C
		// (set) Token: 0x06003A1D RID: 14877 RVA: 0x000C2E1C File Offset: 0x000C1E1C
		public ReflectionPermissionFlag Flags
		{
			get
			{
				return this.m_flags;
			}
			set
			{
				this.VerifyAccess(value);
				this.m_flags = value;
			}
		}

		// Token: 0x06003A1F RID: 14879 RVA: 0x000C2E34 File Offset: 0x000C1E34
		public bool IsUnrestricted()
		{
			return this.m_flags == (ReflectionPermissionFlag.TypeInformation | ReflectionPermissionFlag.MemberAccess | ReflectionPermissionFlag.ReflectionEmit | ReflectionPermissionFlag.RestrictedMemberAccess);
		}

		// Token: 0x06003A20 RID: 14880 RVA: 0x000C2E40 File Offset: 0x000C1E40
		public override IPermission Union(IPermission other)
		{
			if (other == null)
			{
				return this.Copy();
			}
			if (!base.VerifyType(other))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
				{
					base.GetType().FullName
				}));
			}
			ReflectionPermission reflectionPermission = (ReflectionPermission)other;
			if (this.IsUnrestricted() || reflectionPermission.IsUnrestricted())
			{
				return new ReflectionPermission(PermissionState.Unrestricted);
			}
			ReflectionPermissionFlag flag = this.m_flags | reflectionPermission.m_flags;
			return new ReflectionPermission(flag);
		}

		// Token: 0x06003A21 RID: 14881 RVA: 0x000C2EC4 File Offset: 0x000C1EC4
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.m_flags == ReflectionPermissionFlag.NoFlags;
			}
			bool result;
			try
			{
				ReflectionPermission reflectionPermission = (ReflectionPermission)target;
				if (reflectionPermission.IsUnrestricted())
				{
					result = true;
				}
				else if (this.IsUnrestricted())
				{
					result = false;
				}
				else
				{
					result = ((this.m_flags & ~reflectionPermission.m_flags) == ReflectionPermissionFlag.NoFlags);
				}
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
				{
					base.GetType().FullName
				}));
			}
			return result;
		}

		// Token: 0x06003A22 RID: 14882 RVA: 0x000C2F54 File Offset: 0x000C1F54
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			if (!base.VerifyType(target))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
				{
					base.GetType().FullName
				}));
			}
			ReflectionPermission reflectionPermission = (ReflectionPermission)target;
			ReflectionPermissionFlag reflectionPermissionFlag = reflectionPermission.m_flags & this.m_flags;
			if (reflectionPermissionFlag == ReflectionPermissionFlag.NoFlags)
			{
				return null;
			}
			return new ReflectionPermission(reflectionPermissionFlag);
		}

		// Token: 0x06003A23 RID: 14883 RVA: 0x000C2FBF File Offset: 0x000C1FBF
		public override IPermission Copy()
		{
			if (this.IsUnrestricted())
			{
				return new ReflectionPermission(PermissionState.Unrestricted);
			}
			return new ReflectionPermission(this.m_flags);
		}

		// Token: 0x06003A24 RID: 14884 RVA: 0x000C2FDC File Offset: 0x000C1FDC
		private void VerifyAccess(ReflectionPermissionFlag type)
		{
			if ((type & ~(ReflectionPermissionFlag.TypeInformation | ReflectionPermissionFlag.MemberAccess | ReflectionPermissionFlag.ReflectionEmit | ReflectionPermissionFlag.RestrictedMemberAccess)) != ReflectionPermissionFlag.NoFlags)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Arg_EnumIllegalVal"), new object[]
				{
					(int)type
				}));
			}
		}

		// Token: 0x06003A25 RID: 14885 RVA: 0x000C301C File Offset: 0x000C201C
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.ReflectionPermission");
			if (!this.IsUnrestricted())
			{
				securityElement.AddAttribute("Flags", XMLUtil.BitFieldEnumToString(typeof(ReflectionPermissionFlag), this.m_flags));
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x06003A26 RID: 14886 RVA: 0x000C3078 File Offset: 0x000C2078
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.ValidateElement(esd, this);
			if (XMLUtil.IsUnrestricted(esd))
			{
				this.m_flags = (ReflectionPermissionFlag.TypeInformation | ReflectionPermissionFlag.MemberAccess | ReflectionPermissionFlag.ReflectionEmit | ReflectionPermissionFlag.RestrictedMemberAccess);
				return;
			}
			this.Reset();
			this.SetUnrestricted(false);
			string text = esd.Attribute("Flags");
			if (text != null)
			{
				this.m_flags = (ReflectionPermissionFlag)Enum.Parse(typeof(ReflectionPermissionFlag), text);
			}
		}

		// Token: 0x06003A27 RID: 14887 RVA: 0x000C30D4 File Offset: 0x000C20D4
		int IBuiltInPermission.GetTokenIndex()
		{
			return ReflectionPermission.GetTokenIndex();
		}

		// Token: 0x06003A28 RID: 14888 RVA: 0x000C30DB File Offset: 0x000C20DB
		internal static int GetTokenIndex()
		{
			return 4;
		}

		// Token: 0x04001E2D RID: 7725
		internal const ReflectionPermissionFlag AllFlagsAndMore = ReflectionPermissionFlag.TypeInformation | ReflectionPermissionFlag.MemberAccess | ReflectionPermissionFlag.ReflectionEmit | ReflectionPermissionFlag.RestrictedMemberAccess;

		// Token: 0x04001E2E RID: 7726
		private ReflectionPermissionFlag m_flags;
	}
}
