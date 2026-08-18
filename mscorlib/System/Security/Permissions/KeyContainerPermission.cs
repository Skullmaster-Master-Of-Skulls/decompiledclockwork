using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x02000661 RID: 1633
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x06003AEA RID: 15082 RVA: 0x000C6D28 File Offset: 0x000C5D28
		public KeyContainerPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.m_flags = KeyContainerPermissionFlags.AllFlags;
			}
			else
			{
				if (state != PermissionState.None)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
				}
				this.m_flags = KeyContainerPermissionFlags.NoFlags;
			}
			this.m_accessEntries = new KeyContainerPermissionAccessEntryCollection(this.m_flags);
		}

		// Token: 0x06003AEB RID: 15083 RVA: 0x000C6D79 File Offset: 0x000C5D79
		public KeyContainerPermission(KeyContainerPermissionFlags flags)
		{
			KeyContainerPermission.VerifyFlags(flags);
			this.m_flags = flags;
			this.m_accessEntries = new KeyContainerPermissionAccessEntryCollection(this.m_flags);
		}

		// Token: 0x06003AEC RID: 15084 RVA: 0x000C6DA0 File Offset: 0x000C5DA0
		public KeyContainerPermission(KeyContainerPermissionFlags flags, KeyContainerPermissionAccessEntry[] accessList)
		{
			if (accessList == null)
			{
				throw new ArgumentNullException("accessList");
			}
			KeyContainerPermission.VerifyFlags(flags);
			this.m_flags = flags;
			this.m_accessEntries = new KeyContainerPermissionAccessEntryCollection(this.m_flags);
			for (int i = 0; i < accessList.Length; i++)
			{
				this.m_accessEntries.Add(accessList[i]);
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06003AED RID: 15085 RVA: 0x000C6DFC File Offset: 0x000C5DFC
		public KeyContainerPermissionFlags Flags
		{
			get
			{
				return this.m_flags;
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06003AEE RID: 15086 RVA: 0x000C6E04 File Offset: 0x000C5E04
		public KeyContainerPermissionAccessEntryCollection AccessEntries
		{
			get
			{
				return this.m_accessEntries;
			}
		}

		// Token: 0x06003AEF RID: 15087 RVA: 0x000C6E0C File Offset: 0x000C5E0C
		public bool IsUnrestricted()
		{
			if (this.m_flags != KeyContainerPermissionFlags.AllFlags)
			{
				return false;
			}
			foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry in this.AccessEntries)
			{
				if ((keyContainerPermissionAccessEntry.Flags & KeyContainerPermissionFlags.AllFlags) != KeyContainerPermissionFlags.AllFlags)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003AF0 RID: 15088 RVA: 0x000C6E60 File Offset: 0x000C5E60
		private bool IsEmpty()
		{
			if (this.Flags == KeyContainerPermissionFlags.NoFlags)
			{
				foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry in this.AccessEntries)
				{
					if (keyContainerPermissionAccessEntry.Flags != KeyContainerPermissionFlags.NoFlags)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06003AF1 RID: 15089 RVA: 0x000C6EA4 File Offset: 0x000C5EA4
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.IsEmpty();
			}
			if (!base.VerifyType(target))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
				{
					base.GetType().FullName
				}));
			}
			KeyContainerPermission keyContainerPermission = (KeyContainerPermission)target;
			if ((this.m_flags & keyContainerPermission.m_flags) != this.m_flags)
			{
				return false;
			}
			foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry in this.AccessEntries)
			{
				KeyContainerPermissionFlags applicableFlags = KeyContainerPermission.GetApplicableFlags(keyContainerPermissionAccessEntry, keyContainerPermission);
				if ((keyContainerPermissionAccessEntry.Flags & applicableFlags) != keyContainerPermissionAccessEntry.Flags)
				{
					return false;
				}
			}
			foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry2 in keyContainerPermission.AccessEntries)
			{
				KeyContainerPermissionFlags applicableFlags2 = KeyContainerPermission.GetApplicableFlags(keyContainerPermissionAccessEntry2, this);
				if ((applicableFlags2 & keyContainerPermissionAccessEntry2.Flags) != applicableFlags2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003AF2 RID: 15090 RVA: 0x000C6F94 File Offset: 0x000C5F94
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
			KeyContainerPermission keyContainerPermission = (KeyContainerPermission)target;
			if (this.IsEmpty() || keyContainerPermission.IsEmpty())
			{
				return null;
			}
			KeyContainerPermissionFlags flags = keyContainerPermission.m_flags & this.m_flags;
			KeyContainerPermission keyContainerPermission2 = new KeyContainerPermission(flags);
			foreach (KeyContainerPermissionAccessEntry accessEntry in this.AccessEntries)
			{
				keyContainerPermission2.AddAccessEntryAndIntersect(accessEntry, keyContainerPermission);
			}
			foreach (KeyContainerPermissionAccessEntry accessEntry2 in keyContainerPermission.AccessEntries)
			{
				keyContainerPermission2.AddAccessEntryAndIntersect(accessEntry2, this);
			}
			if (!keyContainerPermission2.IsEmpty())
			{
				return keyContainerPermission2;
			}
			return null;
		}

		// Token: 0x06003AF3 RID: 15091 RVA: 0x000C7070 File Offset: 0x000C6070
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			if (!base.VerifyType(target))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
				{
					base.GetType().FullName
				}));
			}
			KeyContainerPermission keyContainerPermission = (KeyContainerPermission)target;
			if (this.IsUnrestricted() || keyContainerPermission.IsUnrestricted())
			{
				return new KeyContainerPermission(PermissionState.Unrestricted);
			}
			KeyContainerPermissionFlags flags = this.m_flags | keyContainerPermission.m_flags;
			KeyContainerPermission keyContainerPermission2 = new KeyContainerPermission(flags);
			foreach (KeyContainerPermissionAccessEntry accessEntry in this.AccessEntries)
			{
				keyContainerPermission2.AddAccessEntryAndUnion(accessEntry, keyContainerPermission);
			}
			foreach (KeyContainerPermissionAccessEntry accessEntry2 in keyContainerPermission.AccessEntries)
			{
				keyContainerPermission2.AddAccessEntryAndUnion(accessEntry2, this);
			}
			if (!keyContainerPermission2.IsEmpty())
			{
				return keyContainerPermission2;
			}
			return null;
		}

		// Token: 0x06003AF4 RID: 15092 RVA: 0x000C7154 File Offset: 0x000C6154
		public override IPermission Copy()
		{
			if (this.IsEmpty())
			{
				return null;
			}
			KeyContainerPermission keyContainerPermission = new KeyContainerPermission(this.m_flags);
			foreach (KeyContainerPermissionAccessEntry accessEntry in this.AccessEntries)
			{
				keyContainerPermission.AccessEntries.Add(accessEntry);
			}
			return keyContainerPermission;
		}

		// Token: 0x06003AF5 RID: 15093 RVA: 0x000C71A4 File Offset: 0x000C61A4
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.KeyContainerPermission");
			if (!this.IsUnrestricted())
			{
				securityElement.AddAttribute("Flags", this.m_flags.ToString());
				if (this.AccessEntries.Count > 0)
				{
					SecurityElement securityElement2 = new SecurityElement("AccessList");
					foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry in this.AccessEntries)
					{
						SecurityElement securityElement3 = new SecurityElement("AccessEntry");
						securityElement3.AddAttribute("KeyStore", keyContainerPermissionAccessEntry.KeyStore);
						securityElement3.AddAttribute("ProviderName", keyContainerPermissionAccessEntry.ProviderName);
						securityElement3.AddAttribute("ProviderType", keyContainerPermissionAccessEntry.ProviderType.ToString(null, null));
						securityElement3.AddAttribute("KeyContainerName", keyContainerPermissionAccessEntry.KeyContainerName);
						securityElement3.AddAttribute("KeySpec", keyContainerPermissionAccessEntry.KeySpec.ToString(null, null));
						securityElement3.AddAttribute("Flags", keyContainerPermissionAccessEntry.Flags.ToString());
						securityElement2.AddChild(securityElement3);
					}
					securityElement.AddChild(securityElement2);
				}
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x06003AF6 RID: 15094 RVA: 0x000C72D8 File Offset: 0x000C62D8
		public override void FromXml(SecurityElement securityElement)
		{
			CodeAccessPermission.ValidateElement(securityElement, this);
			if (XMLUtil.IsUnrestricted(securityElement))
			{
				this.m_flags = KeyContainerPermissionFlags.AllFlags;
				this.m_accessEntries = new KeyContainerPermissionAccessEntryCollection(this.m_flags);
				return;
			}
			this.m_flags = KeyContainerPermissionFlags.NoFlags;
			string text = securityElement.Attribute("Flags");
			if (text != null)
			{
				KeyContainerPermissionFlags flags = (KeyContainerPermissionFlags)Enum.Parse(typeof(KeyContainerPermissionFlags), text);
				KeyContainerPermission.VerifyFlags(flags);
				this.m_flags = flags;
			}
			this.m_accessEntries = new KeyContainerPermissionAccessEntryCollection(this.m_flags);
			if (securityElement.InternalChildren != null && securityElement.InternalChildren.Count != 0)
			{
				foreach (object obj in securityElement.Children)
				{
					SecurityElement securityElement2 = (SecurityElement)obj;
					if (securityElement2 != null && string.Equals(securityElement2.Tag, "AccessList"))
					{
						this.AddAccessEntries(securityElement2);
					}
				}
			}
		}

		// Token: 0x06003AF7 RID: 15095 RVA: 0x000C73AE File Offset: 0x000C63AE
		int IBuiltInPermission.GetTokenIndex()
		{
			return KeyContainerPermission.GetTokenIndex();
		}

		// Token: 0x06003AF8 RID: 15096 RVA: 0x000C73B8 File Offset: 0x000C63B8
		private void AddAccessEntries(SecurityElement securityElement)
		{
			if (securityElement.InternalChildren != null && securityElement.InternalChildren.Count != 0)
			{
				foreach (object obj in securityElement.Children)
				{
					SecurityElement securityElement2 = (SecurityElement)obj;
					if (securityElement2 != null && string.Equals(securityElement2.Tag, "AccessEntry"))
					{
						int count = securityElement2.m_lAttributes.Count;
						string keyStore = null;
						string providerName = null;
						int providerType = -1;
						string keyContainerName = null;
						int keySpec = -1;
						KeyContainerPermissionFlags flags = KeyContainerPermissionFlags.NoFlags;
						for (int i = 0; i < count; i += 2)
						{
							string a = (string)securityElement2.m_lAttributes[i];
							string text = (string)securityElement2.m_lAttributes[i + 1];
							if (string.Equals(a, "KeyStore"))
							{
								keyStore = text;
							}
							if (string.Equals(a, "ProviderName"))
							{
								providerName = text;
							}
							else if (string.Equals(a, "ProviderType"))
							{
								providerType = Convert.ToInt32(text, null);
							}
							else if (string.Equals(a, "KeyContainerName"))
							{
								keyContainerName = text;
							}
							else if (string.Equals(a, "KeySpec"))
							{
								keySpec = Convert.ToInt32(text, null);
							}
							else if (string.Equals(a, "Flags"))
							{
								flags = (KeyContainerPermissionFlags)Enum.Parse(typeof(KeyContainerPermissionFlags), text);
							}
						}
						KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(keyStore, providerName, providerType, keyContainerName, keySpec, flags);
						this.AccessEntries.Add(accessEntry);
					}
				}
			}
		}

		// Token: 0x06003AF9 RID: 15097 RVA: 0x000C7534 File Offset: 0x000C6534
		private void AddAccessEntryAndUnion(KeyContainerPermissionAccessEntry accessEntry, KeyContainerPermission target)
		{
			KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = new KeyContainerPermissionAccessEntry(accessEntry);
			keyContainerPermissionAccessEntry.Flags |= KeyContainerPermission.GetApplicableFlags(accessEntry, target);
			this.AccessEntries.Add(keyContainerPermissionAccessEntry);
		}

		// Token: 0x06003AFA RID: 15098 RVA: 0x000C756C File Offset: 0x000C656C
		private void AddAccessEntryAndIntersect(KeyContainerPermissionAccessEntry accessEntry, KeyContainerPermission target)
		{
			KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = new KeyContainerPermissionAccessEntry(accessEntry);
			keyContainerPermissionAccessEntry.Flags &= KeyContainerPermission.GetApplicableFlags(accessEntry, target);
			this.AccessEntries.Add(keyContainerPermissionAccessEntry);
		}

		// Token: 0x06003AFB RID: 15099 RVA: 0x000C75A4 File Offset: 0x000C65A4
		internal static void VerifyFlags(KeyContainerPermissionFlags flags)
		{
			if ((flags & ~(KeyContainerPermissionFlags.Create | KeyContainerPermissionFlags.Open | KeyContainerPermissionFlags.Delete | KeyContainerPermissionFlags.Import | KeyContainerPermissionFlags.Export | KeyContainerPermissionFlags.Sign | KeyContainerPermissionFlags.Decrypt | KeyContainerPermissionFlags.ViewAcl | KeyContainerPermissionFlags.ChangeAcl)) != KeyContainerPermissionFlags.NoFlags)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Arg_EnumIllegalVal"), new object[]
				{
					(int)flags
				}));
			}
		}

		// Token: 0x06003AFC RID: 15100 RVA: 0x000C75E8 File Offset: 0x000C65E8
		private static KeyContainerPermissionFlags GetApplicableFlags(KeyContainerPermissionAccessEntry accessEntry, KeyContainerPermission target)
		{
			KeyContainerPermissionFlags keyContainerPermissionFlags = KeyContainerPermissionFlags.NoFlags;
			bool flag = true;
			int num = target.AccessEntries.IndexOf(accessEntry);
			if (num != -1)
			{
				return target.AccessEntries[num].Flags;
			}
			foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry in target.AccessEntries)
			{
				if (accessEntry.IsSubsetOf(keyContainerPermissionAccessEntry))
				{
					if (!flag)
					{
						keyContainerPermissionFlags &= keyContainerPermissionAccessEntry.Flags;
					}
					else
					{
						keyContainerPermissionFlags = keyContainerPermissionAccessEntry.Flags;
						flag = false;
					}
				}
			}
			if (flag)
			{
				keyContainerPermissionFlags = target.Flags;
			}
			return keyContainerPermissionFlags;
		}

		// Token: 0x06003AFD RID: 15101 RVA: 0x000C7669 File Offset: 0x000C6669
		private static int GetTokenIndex()
		{
			return 16;
		}

		// Token: 0x04001E84 RID: 7812
		private KeyContainerPermissionFlags m_flags;

		// Token: 0x04001E85 RID: 7813
		private KeyContainerPermissionAccessEntryCollection m_accessEntries;
	}
}
