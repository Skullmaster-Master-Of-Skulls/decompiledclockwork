using System;
using System.Globalization;

namespace System.Security.Permissions
{
	// Token: 0x02000484 RID: 1156
	[Serializable]
	public sealed class StorePermission : CodeAccessPermission, IUnrestrictedPermission
	{
		// Token: 0x06002AE3 RID: 10979 RVA: 0x000C35D4 File Offset: 0x000C17D4
		public StorePermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.m_flags = StorePermissionFlags.AllFlags;
				return;
			}
			if (state == PermissionState.None)
			{
				this.m_flags = StorePermissionFlags.NoFlags;
				return;
			}
			throw new ArgumentException(SR.GetString("Argument_InvalidPermissionState"));
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x000C3606 File Offset: 0x000C1806
		public StorePermission(StorePermissionFlags flag)
		{
			StorePermission.VerifyFlags(flag);
			this.m_flags = flag;
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06002AE6 RID: 10982 RVA: 0x000C362A File Offset: 0x000C182A
		// (set) Token: 0x06002AE5 RID: 10981 RVA: 0x000C361B File Offset: 0x000C181B
		public StorePermissionFlags Flags
		{
			get
			{
				return this.m_flags;
			}
			set
			{
				StorePermission.VerifyFlags(value);
				this.m_flags = value;
			}
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x000C3632 File Offset: 0x000C1832
		public bool IsUnrestricted()
		{
			return this.m_flags == StorePermissionFlags.AllFlags;
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x000C3644 File Offset: 0x000C1844
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			IPermission result;
			try
			{
				StorePermission storePermission = (StorePermission)target;
				StorePermissionFlags storePermissionFlags = this.m_flags | storePermission.m_flags;
				if (storePermissionFlags == StorePermissionFlags.NoFlags)
				{
					result = null;
				}
				else
				{
					result = new StorePermission(storePermissionFlags);
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

		// Token: 0x06002AE9 RID: 10985 RVA: 0x000C36C4 File Offset: 0x000C18C4
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.m_flags == StorePermissionFlags.NoFlags;
			}
			bool result;
			try
			{
				StorePermission storePermission = (StorePermission)target;
				StorePermissionFlags flags = this.m_flags;
				StorePermissionFlags flags2 = storePermission.m_flags;
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

		// Token: 0x06002AEA RID: 10986 RVA: 0x000C3740 File Offset: 0x000C1940
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			IPermission result;
			try
			{
				StorePermission storePermission = (StorePermission)target;
				StorePermissionFlags storePermissionFlags = storePermission.m_flags & this.m_flags;
				if (storePermissionFlags == StorePermissionFlags.NoFlags)
				{
					result = null;
				}
				else
				{
					result = new StorePermission(storePermissionFlags);
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

		// Token: 0x06002AEB RID: 10987 RVA: 0x000C37B8 File Offset: 0x000C19B8
		public override IPermission Copy()
		{
			return new StorePermission(this.m_flags);
		}

		// Token: 0x06002AEC RID: 10988 RVA: 0x000C37C8 File Offset: 0x000C19C8
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

		// Token: 0x06002AED RID: 10989 RVA: 0x000C3868 File Offset: 0x000C1A68
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
				this.m_flags = StorePermissionFlags.AllFlags;
				return;
			}
			this.m_flags = StorePermissionFlags.NoFlags;
			string text3 = securityElement.Attribute("Flags");
			if (text3 != null)
			{
				StorePermissionFlags flags = (StorePermissionFlags)Enum.Parse(typeof(StorePermissionFlags), text3);
				StorePermission.VerifyFlags(flags);
				this.m_flags = flags;
			}
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x000C391E File Offset: 0x000C1B1E
		internal static void VerifyFlags(StorePermissionFlags flags)
		{
			if ((flags & ~(StorePermissionFlags.CreateStore | StorePermissionFlags.DeleteStore | StorePermissionFlags.EnumerateStores | StorePermissionFlags.OpenStore | StorePermissionFlags.AddToStore | StorePermissionFlags.RemoveFromStore | StorePermissionFlags.EnumerateCertificates)) != StorePermissionFlags.NoFlags)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Arg_EnumIllegalVal"), new object[]
				{
					(int)flags
				}));
			}
		}

		// Token: 0x04002660 RID: 9824
		private StorePermissionFlags m_flags;
	}
}
