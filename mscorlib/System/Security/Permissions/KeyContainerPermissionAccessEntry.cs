using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace System.Security.Permissions
{
	// Token: 0x0200065E RID: 1630
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermissionAccessEntry
	{
		// Token: 0x06003AC2 RID: 15042 RVA: 0x000C661A File Offset: 0x000C561A
		internal KeyContainerPermissionAccessEntry(KeyContainerPermissionAccessEntry accessEntry) : this(accessEntry.KeyStore, accessEntry.ProviderName, accessEntry.ProviderType, accessEntry.KeyContainerName, accessEntry.KeySpec, accessEntry.Flags)
		{
		}

		// Token: 0x06003AC3 RID: 15043 RVA: 0x000C6646 File Offset: 0x000C5646
		public KeyContainerPermissionAccessEntry(string keyContainerName, KeyContainerPermissionFlags flags) : this(null, null, -1, keyContainerName, -1, flags)
		{
		}

		// Token: 0x06003AC4 RID: 15044 RVA: 0x000C6654 File Offset: 0x000C5654
		public KeyContainerPermissionAccessEntry(CspParameters parameters, KeyContainerPermissionFlags flags) : this(((parameters.Flags & CspProviderFlags.UseMachineKeyStore) == CspProviderFlags.UseMachineKeyStore) ? "Machine" : "User", parameters.ProviderName, parameters.ProviderType, parameters.KeyContainerName, parameters.KeyNumber, flags)
		{
		}

		// Token: 0x06003AC5 RID: 15045 RVA: 0x000C668C File Offset: 0x000C568C
		public KeyContainerPermissionAccessEntry(string keyStore, string providerName, int providerType, string keyContainerName, int keySpec, KeyContainerPermissionFlags flags)
		{
			this.m_providerName = ((providerName == null) ? "*" : providerName);
			this.m_providerType = providerType;
			this.m_keyContainerName = ((keyContainerName == null) ? "*" : keyContainerName);
			this.m_keySpec = keySpec;
			this.KeyStore = keyStore;
			this.Flags = flags;
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06003AC6 RID: 15046 RVA: 0x000C66E1 File Offset: 0x000C56E1
		// (set) Token: 0x06003AC7 RID: 15047 RVA: 0x000C66EC File Offset: 0x000C56EC
		public string KeyStore
		{
			get
			{
				return this.m_keyStore;
			}
			set
			{
				if (KeyContainerPermissionAccessEntry.IsUnrestrictedEntry(value, this.ProviderName, this.ProviderType, this.KeyContainerName, this.KeySpec))
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_InvalidAccessEntry"));
				}
				if (value == null)
				{
					this.m_keyStore = "*";
					return;
				}
				if (value != "User" && value != "Machine" && value != "*")
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidKeyStore", new object[]
					{
						value
					}), "value");
				}
				this.m_keyStore = value;
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06003AC8 RID: 15048 RVA: 0x000C6787 File Offset: 0x000C5787
		// (set) Token: 0x06003AC9 RID: 15049 RVA: 0x000C6790 File Offset: 0x000C5790
		public string ProviderName
		{
			get
			{
				return this.m_providerName;
			}
			set
			{
				if (KeyContainerPermissionAccessEntry.IsUnrestrictedEntry(this.KeyStore, value, this.ProviderType, this.KeyContainerName, this.KeySpec))
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_InvalidAccessEntry"));
				}
				if (value == null)
				{
					this.m_providerName = "*";
					return;
				}
				this.m_providerName = value;
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06003ACA RID: 15050 RVA: 0x000C67E3 File Offset: 0x000C57E3
		// (set) Token: 0x06003ACB RID: 15051 RVA: 0x000C67EB File Offset: 0x000C57EB
		public int ProviderType
		{
			get
			{
				return this.m_providerType;
			}
			set
			{
				if (KeyContainerPermissionAccessEntry.IsUnrestrictedEntry(this.KeyStore, this.ProviderName, value, this.KeyContainerName, this.KeySpec))
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_InvalidAccessEntry"));
				}
				this.m_providerType = value;
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06003ACC RID: 15052 RVA: 0x000C6824 File Offset: 0x000C5824
		// (set) Token: 0x06003ACD RID: 15053 RVA: 0x000C682C File Offset: 0x000C582C
		public string KeyContainerName
		{
			get
			{
				return this.m_keyContainerName;
			}
			set
			{
				if (KeyContainerPermissionAccessEntry.IsUnrestrictedEntry(this.KeyStore, this.ProviderName, this.ProviderType, value, this.KeySpec))
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_InvalidAccessEntry"));
				}
				if (value == null)
				{
					this.m_keyContainerName = "*";
					return;
				}
				this.m_keyContainerName = value;
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06003ACE RID: 15054 RVA: 0x000C687F File Offset: 0x000C587F
		// (set) Token: 0x06003ACF RID: 15055 RVA: 0x000C6887 File Offset: 0x000C5887
		public int KeySpec
		{
			get
			{
				return this.m_keySpec;
			}
			set
			{
				if (KeyContainerPermissionAccessEntry.IsUnrestrictedEntry(this.KeyStore, this.ProviderName, this.ProviderType, this.KeyContainerName, value))
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_InvalidAccessEntry"));
				}
				this.m_keySpec = value;
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06003AD0 RID: 15056 RVA: 0x000C68C0 File Offset: 0x000C58C0
		// (set) Token: 0x06003AD1 RID: 15057 RVA: 0x000C68C8 File Offset: 0x000C58C8
		public KeyContainerPermissionFlags Flags
		{
			get
			{
				return this.m_flags;
			}
			set
			{
				KeyContainerPermission.VerifyFlags(value);
				this.m_flags = value;
			}
		}

		// Token: 0x06003AD2 RID: 15058 RVA: 0x000C68D8 File Offset: 0x000C58D8
		public override bool Equals(object o)
		{
			KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = o as KeyContainerPermissionAccessEntry;
			return keyContainerPermissionAccessEntry != null && !(keyContainerPermissionAccessEntry.m_keyStore != this.m_keyStore) && !(keyContainerPermissionAccessEntry.m_providerName != this.m_providerName) && keyContainerPermissionAccessEntry.m_providerType == this.m_providerType && !(keyContainerPermissionAccessEntry.m_keyContainerName != this.m_keyContainerName) && keyContainerPermissionAccessEntry.m_keySpec == this.m_keySpec;
		}

		// Token: 0x06003AD3 RID: 15059 RVA: 0x000C6954 File Offset: 0x000C5954
		public override int GetHashCode()
		{
			int num = 0;
			num |= (this.m_keyStore.GetHashCode() & 255) << 24;
			num |= (this.m_providerName.GetHashCode() & 255) << 16;
			num |= (this.m_providerType & 15) << 12;
			num |= (this.m_keyContainerName.GetHashCode() & 255) << 4;
			return num | (this.m_keySpec & 15);
		}

		// Token: 0x06003AD4 RID: 15060 RVA: 0x000C69C4 File Offset: 0x000C59C4
		internal bool IsSubsetOf(KeyContainerPermissionAccessEntry target)
		{
			return (!(target.m_keyStore != "*") || !(this.m_keyStore != target.m_keyStore)) && (!(target.m_providerName != "*") || !(this.m_providerName != target.m_providerName)) && (target.m_providerType == -1 || this.m_providerType == target.m_providerType) && (!(target.m_keyContainerName != "*") || !(this.m_keyContainerName != target.m_keyContainerName)) && (target.m_keySpec == -1 || this.m_keySpec == target.m_keySpec);
		}

		// Token: 0x06003AD5 RID: 15061 RVA: 0x000C6A7C File Offset: 0x000C5A7C
		internal static bool IsUnrestrictedEntry(string keyStore, string providerName, int providerType, string keyContainerName, int keySpec)
		{
			return (!(keyStore != "*") || keyStore == null) && (!(providerName != "*") || providerName == null) && providerType == -1 && (!(keyContainerName != "*") || keyContainerName == null) && keySpec == -1;
		}

		// Token: 0x04001E7A RID: 7802
		private string m_keyStore;

		// Token: 0x04001E7B RID: 7803
		private string m_providerName;

		// Token: 0x04001E7C RID: 7804
		private int m_providerType;

		// Token: 0x04001E7D RID: 7805
		private string m_keyContainerName;

		// Token: 0x04001E7E RID: 7806
		private int m_keySpec;

		// Token: 0x04001E7F RID: 7807
		private KeyContainerPermissionFlags m_flags;
	}
}
