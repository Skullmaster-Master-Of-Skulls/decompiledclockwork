using System;
using System.Globalization;

namespace System.Security.Permissions
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	public sealed class DataProtectionPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000024AB File Offset: 0x000006AB
		public DataProtectionPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.m_flags = DataProtectionPermissionFlags.AllFlags;
				return;
			}
			if (state == PermissionState.None)
			{
				this.m_flags = DataProtectionPermissionFlags.NoFlags;
				return;
			}
			throw new ArgumentException(SecurityResources.GetResourceString("Argument_InvalidPermissionState"));
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024DA File Offset: 0x000006DA
		public DataProtectionPermission(DataProtectionPermissionFlags flag)
		{
			this.Flags = flag;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000024F8 File Offset: 0x000006F8
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000024E9 File Offset: 0x000006E9
		public DataProtectionPermissionFlags Flags
		{
			get
			{
				return this.m_flags;
			}
			set
			{
				DataProtectionPermission.VerifyFlags(value);
				this.m_flags = value;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002500 File Offset: 0x00000700
		public bool IsUnrestricted()
		{
			return this.m_flags == DataProtectionPermissionFlags.AllFlags;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000250C File Offset: 0x0000070C
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			IPermission result;
			try
			{
				DataProtectionPermission dataProtectionPermission = (DataProtectionPermission)target;
				DataProtectionPermissionFlags dataProtectionPermissionFlags = this.m_flags | dataProtectionPermission.m_flags;
				if (dataProtectionPermissionFlags == DataProtectionPermissionFlags.NoFlags)
				{
					result = null;
				}
				else
				{
					result = new DataProtectionPermission(dataProtectionPermissionFlags);
				}
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SecurityResources.GetResourceString("Argument_WrongType"), new object[]
				{
					base.GetType().FullName
				}));
			}
			return result;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000258C File Offset: 0x0000078C
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.m_flags == DataProtectionPermissionFlags.NoFlags;
			}
			bool result;
			try
			{
				DataProtectionPermission dataProtectionPermission = (DataProtectionPermission)target;
				DataProtectionPermissionFlags flags = this.m_flags;
				DataProtectionPermissionFlags flags2 = dataProtectionPermission.m_flags;
				result = ((flags & flags2) == flags);
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SecurityResources.GetResourceString("Argument_WrongType"), new object[]
				{
					base.GetType().FullName
				}));
			}
			return result;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002608 File Offset: 0x00000808
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			IPermission result;
			try
			{
				DataProtectionPermission dataProtectionPermission = (DataProtectionPermission)target;
				DataProtectionPermissionFlags dataProtectionPermissionFlags = dataProtectionPermission.m_flags & this.m_flags;
				if (dataProtectionPermissionFlags == DataProtectionPermissionFlags.NoFlags)
				{
					result = null;
				}
				else
				{
					result = new DataProtectionPermission(dataProtectionPermissionFlags);
				}
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SecurityResources.GetResourceString("Argument_WrongType"), new object[]
				{
					base.GetType().FullName
				}));
			}
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002680 File Offset: 0x00000880
		public override IPermission Copy()
		{
			if (this.Flags == DataProtectionPermissionFlags.NoFlags)
			{
				return null;
			}
			return new DataProtectionPermission(this.m_flags);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002698 File Offset: 0x00000898
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

		// Token: 0x0600001E RID: 30 RVA: 0x00002738 File Offset: 0x00000938
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw new ArgumentNullException("securityElement");
			}
			string text = securityElement.Attribute("class");
			if (text == null || text.IndexOf(base.GetType().FullName, StringComparison.Ordinal) == -1)
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Argument_InvalidClassAttribute"), "securityElement");
			}
			string text2 = securityElement.Attribute("Unrestricted");
			if (text2 != null && string.Compare(text2, "true", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.m_flags = DataProtectionPermissionFlags.AllFlags;
				return;
			}
			this.m_flags = DataProtectionPermissionFlags.NoFlags;
			string text3 = securityElement.Attribute("Flags");
			if (text3 != null)
			{
				DataProtectionPermissionFlags flags = (DataProtectionPermissionFlags)Enum.Parse(typeof(DataProtectionPermissionFlags), text3);
				DataProtectionPermission.VerifyFlags(flags);
				this.m_flags = flags;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000027EB File Offset: 0x000009EB
		internal static void VerifyFlags(DataProtectionPermissionFlags flags)
		{
			if ((flags & ~(DataProtectionPermissionFlags.ProtectData | DataProtectionPermissionFlags.UnprotectData | DataProtectionPermissionFlags.ProtectMemory | DataProtectionPermissionFlags.UnprotectMemory)) != DataProtectionPermissionFlags.NoFlags)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SecurityResources.GetResourceString("Arg_EnumIllegalVal"), new object[]
				{
					(int)flags
				}));
			}
		}

		// Token: 0x0400005E RID: 94
		private DataProtectionPermissionFlags m_flags;
	}
}
