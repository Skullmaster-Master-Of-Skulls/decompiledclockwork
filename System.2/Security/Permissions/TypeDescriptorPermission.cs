using System;
using System.Globalization;

namespace System.Security.Permissions
{
	// Token: 0x02000488 RID: 1160
	[Serializable]
	public sealed class TypeDescriptorPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		// Token: 0x06002B01 RID: 11009 RVA: 0x000C3ACC File Offset: 0x000C1CCC
		public TypeDescriptorPermission(PermissionState state)
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
			throw new ArgumentException(SR.GetString("Argument_InvalidPermissionState"));
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x000C3AFA File Offset: 0x000C1CFA
		public TypeDescriptorPermission(TypeDescriptorPermissionFlags flag)
		{
			this.VerifyAccess(flag);
			this.SetUnrestricted(false);
			this.m_flags = flag;
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x000C3B17 File Offset: 0x000C1D17
		private void SetUnrestricted(bool unrestricted)
		{
			if (unrestricted)
			{
				this.m_flags = TypeDescriptorPermissionFlags.RestrictedRegistrationAccess;
				return;
			}
			this.Reset();
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x000C3B2A File Offset: 0x000C1D2A
		private void Reset()
		{
			this.m_flags = TypeDescriptorPermissionFlags.NoFlags;
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06002B06 RID: 11014 RVA: 0x000C3B43 File Offset: 0x000C1D43
		// (set) Token: 0x06002B05 RID: 11013 RVA: 0x000C3B33 File Offset: 0x000C1D33
		public TypeDescriptorPermissionFlags Flags
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

		// Token: 0x06002B07 RID: 11015 RVA: 0x000C3B4B File Offset: 0x000C1D4B
		public bool IsUnrestricted()
		{
			return this.m_flags == TypeDescriptorPermissionFlags.RestrictedRegistrationAccess;
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x000C3B58 File Offset: 0x000C1D58
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			IPermission result;
			try
			{
				TypeDescriptorPermission typeDescriptorPermission = (TypeDescriptorPermission)target;
				TypeDescriptorPermissionFlags typeDescriptorPermissionFlags = this.m_flags | typeDescriptorPermission.m_flags;
				if (typeDescriptorPermissionFlags == TypeDescriptorPermissionFlags.NoFlags)
				{
					result = null;
				}
				else
				{
					result = new TypeDescriptorPermission(typeDescriptorPermissionFlags);
				}
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Argument_WrongType"), new object[]
				{
					base.GetType().FullName
				}));
			}
			return result;
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x000C3BD8 File Offset: 0x000C1DD8
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.m_flags == TypeDescriptorPermissionFlags.NoFlags;
			}
			bool result;
			try
			{
				TypeDescriptorPermission typeDescriptorPermission = (TypeDescriptorPermission)target;
				TypeDescriptorPermissionFlags flags = this.m_flags;
				TypeDescriptorPermissionFlags flags2 = typeDescriptorPermission.m_flags;
				result = ((flags & flags2) == flags);
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Argument_WrongType"), new object[]
				{
					base.GetType().FullName
				}));
			}
			return result;
		}

		// Token: 0x06002B0A RID: 11018 RVA: 0x000C3C54 File Offset: 0x000C1E54
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			IPermission result;
			try
			{
				TypeDescriptorPermission typeDescriptorPermission = (TypeDescriptorPermission)target;
				TypeDescriptorPermissionFlags typeDescriptorPermissionFlags = typeDescriptorPermission.m_flags & this.m_flags;
				if (typeDescriptorPermissionFlags == TypeDescriptorPermissionFlags.NoFlags)
				{
					result = null;
				}
				else
				{
					result = new TypeDescriptorPermission(typeDescriptorPermissionFlags);
				}
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Argument_WrongType"), new object[]
				{
					base.GetType().FullName
				}));
			}
			return result;
		}

		// Token: 0x06002B0B RID: 11019 RVA: 0x000C3CCC File Offset: 0x000C1ECC
		public override IPermission Copy()
		{
			return new TypeDescriptorPermission(this.m_flags);
		}

		// Token: 0x06002B0C RID: 11020 RVA: 0x000C3CD9 File Offset: 0x000C1ED9
		private void VerifyAccess(TypeDescriptorPermissionFlags type)
		{
			if ((type & ~TypeDescriptorPermissionFlags.RestrictedRegistrationAccess) != TypeDescriptorPermissionFlags.NoFlags)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Arg_EnumIllegalVal"), new object[]
				{
					(int)type
				}));
			}
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x000C3D0C File Offset: 0x000C1F0C
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", base.GetType().FullName + ", " + base.GetType().Module.Assembly.FullName.Replace('"', '\''));
			securityElement.AddAttribute("version", "1");
			if (!this.IsUnrestricted())
			{
				securityElement.AddAttribute("Flags", this.m_flags.ToString());
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x000C3DAC File Offset: 0x000C1FAC
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw new ArgumentNullException("securityElement");
			}
			string text = securityElement.Attribute("class");
			if (text == null || text.IndexOf(base.GetType().FullName, StringComparison.Ordinal) == -1)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidClassAttribute"), "securityElement");
			}
			string text2 = securityElement.Attribute("Unrestricted");
			if (text2 != null && string.Compare(text2, "true", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.m_flags = TypeDescriptorPermissionFlags.RestrictedRegistrationAccess;
				return;
			}
			this.m_flags = TypeDescriptorPermissionFlags.NoFlags;
			string text3 = securityElement.Attribute("Flags");
			if (text3 != null)
			{
				TypeDescriptorPermissionFlags flags = (TypeDescriptorPermissionFlags)Enum.Parse(typeof(TypeDescriptorPermissionFlags), text3);
				TypeDescriptorPermission.VerifyFlags(flags);
				this.m_flags = flags;
			}
		}

		// Token: 0x06002B0F RID: 11023 RVA: 0x000C3E5E File Offset: 0x000C205E
		internal static void VerifyFlags(TypeDescriptorPermissionFlags flags)
		{
			if ((flags & ~TypeDescriptorPermissionFlags.RestrictedRegistrationAccess) != TypeDescriptorPermissionFlags.NoFlags)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Arg_EnumIllegalVal"), new object[]
				{
					(int)flags
				}));
			}
		}

		// Token: 0x0400266F RID: 9839
		private TypeDescriptorPermissionFlags m_flags;
	}
}
