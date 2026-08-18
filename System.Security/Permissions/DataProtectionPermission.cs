using System;
using System.Globalization;

namespace System.Security.Permissions
{
	// Token: 0x020000CF RID: 207
	[Serializable]
	public sealed class DataProtectionPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		// Token: 0x06000516 RID: 1302 RVA: 0x00019AE8 File Offset: 0x00018AE8
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

		// Token: 0x06000517 RID: 1303 RVA: 0x00019B17 File Offset: 0x00018B17
		public DataProtectionPermission(DataProtectionPermissionFlags flag)
		{
			this.Flags = flag;
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x00019B35 File Offset: 0x00018B35
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x00019B26 File Offset: 0x00018B26
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

		// Token: 0x0600051A RID: 1306 RVA: 0x00019B3D File Offset: 0x00018B3D
		public bool IsUnrestricted()
		{
			return this.m_flags == DataProtectionPermissionFlags.AllFlags;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00019B4C File Offset: 0x00018B4C
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

		// Token: 0x0600051C RID: 1308 RVA: 0x00019BCC File Offset: 0x00018BCC
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

		// Token: 0x0600051D RID: 1309 RVA: 0x00019C4C File Offset: 0x00018C4C
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

		// Token: 0x0600051E RID: 1310 RVA: 0x00019CC8 File Offset: 0x00018CC8
		public override IPermission Copy()
		{
			if (this.Flags == DataProtectionPermissionFlags.NoFlags)
			{
				return null;
			}
			return new DataProtectionPermission(this.m_flags);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00019CE0 File Offset: 0x00018CE0
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

		// Token: 0x06000520 RID: 1312 RVA: 0x00019D7C File Offset: 0x00018D7C
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

		// Token: 0x06000521 RID: 1313 RVA: 0x00019E30 File Offset: 0x00018E30
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

		// Token: 0x040005DD RID: 1501
		private DataProtectionPermissionFlags m_flags;
	}
}
