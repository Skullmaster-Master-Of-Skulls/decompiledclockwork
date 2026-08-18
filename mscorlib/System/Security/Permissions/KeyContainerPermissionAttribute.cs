using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x0200063E RID: 1598
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x0600398F RID: 14735 RVA: 0x000C2238 File Offset: 0x000C1238
		public KeyContainerPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06003990 RID: 14736 RVA: 0x000C224F File Offset: 0x000C124F
		// (set) Token: 0x06003991 RID: 14737 RVA: 0x000C2257 File Offset: 0x000C1257
		public string KeyStore
		{
			get
			{
				return this.m_keyStore;
			}
			set
			{
				this.m_keyStore = value;
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06003992 RID: 14738 RVA: 0x000C2260 File Offset: 0x000C1260
		// (set) Token: 0x06003993 RID: 14739 RVA: 0x000C2268 File Offset: 0x000C1268
		public string ProviderName
		{
			get
			{
				return this.m_providerName;
			}
			set
			{
				this.m_providerName = value;
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06003994 RID: 14740 RVA: 0x000C2271 File Offset: 0x000C1271
		// (set) Token: 0x06003995 RID: 14741 RVA: 0x000C2279 File Offset: 0x000C1279
		public int ProviderType
		{
			get
			{
				return this.m_providerType;
			}
			set
			{
				this.m_providerType = value;
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06003996 RID: 14742 RVA: 0x000C2282 File Offset: 0x000C1282
		// (set) Token: 0x06003997 RID: 14743 RVA: 0x000C228A File Offset: 0x000C128A
		public string KeyContainerName
		{
			get
			{
				return this.m_keyContainerName;
			}
			set
			{
				this.m_keyContainerName = value;
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06003998 RID: 14744 RVA: 0x000C2293 File Offset: 0x000C1293
		// (set) Token: 0x06003999 RID: 14745 RVA: 0x000C229B File Offset: 0x000C129B
		public int KeySpec
		{
			get
			{
				return this.m_keySpec;
			}
			set
			{
				this.m_keySpec = value;
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x0600399A RID: 14746 RVA: 0x000C22A4 File Offset: 0x000C12A4
		// (set) Token: 0x0600399B RID: 14747 RVA: 0x000C22AC File Offset: 0x000C12AC
		public KeyContainerPermissionFlags Flags
		{
			get
			{
				return this.m_flags;
			}
			set
			{
				this.m_flags = value;
			}
		}

		// Token: 0x0600399C RID: 14748 RVA: 0x000C22B8 File Offset: 0x000C12B8
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new KeyContainerPermission(PermissionState.Unrestricted);
			}
			if (KeyContainerPermissionAccessEntry.IsUnrestrictedEntry(this.m_keyStore, this.m_providerName, this.m_providerType, this.m_keyContainerName, this.m_keySpec))
			{
				return new KeyContainerPermission(this.m_flags);
			}
			KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
			KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this.m_keyStore, this.m_providerName, this.m_providerType, this.m_keyContainerName, this.m_keySpec, this.m_flags);
			keyContainerPermission.AccessEntries.Add(accessEntry);
			return keyContainerPermission;
		}

		// Token: 0x04001E04 RID: 7684
		private KeyContainerPermissionFlags m_flags;

		// Token: 0x04001E05 RID: 7685
		private string m_keyStore;

		// Token: 0x04001E06 RID: 7686
		private string m_providerName;

		// Token: 0x04001E07 RID: 7687
		private int m_providerType = -1;

		// Token: 0x04001E08 RID: 7688
		private string m_keyContainerName;

		// Token: 0x04001E09 RID: 7689
		private int m_keySpec = -1;
	}
}
