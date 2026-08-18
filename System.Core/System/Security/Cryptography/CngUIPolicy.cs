using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x020000EF RID: 239
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class CngUIPolicy
	{
		// Token: 0x06000788 RID: 1928 RVA: 0x00018872 File Offset: 0x00016A72
		public CngUIPolicy(CngUIProtectionLevels protectionLevel) : this(protectionLevel, null)
		{
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0001887C File Offset: 0x00016A7C
		public CngUIPolicy(CngUIProtectionLevels protectionLevel, string friendlyName) : this(protectionLevel, friendlyName, null)
		{
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00018887 File Offset: 0x00016A87
		public CngUIPolicy(CngUIProtectionLevels protectionLevel, string friendlyName, string description) : this(protectionLevel, friendlyName, description, null)
		{
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x00018893 File Offset: 0x00016A93
		public CngUIPolicy(CngUIProtectionLevels protectionLevel, string friendlyName, string description, string useContext) : this(protectionLevel, friendlyName, description, useContext, null)
		{
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x000188A1 File Offset: 0x00016AA1
		public CngUIPolicy(CngUIProtectionLevels protectionLevel, string friendlyName, string description, string useContext, string creationTitle)
		{
			this.m_creationTitle = creationTitle;
			this.m_description = description;
			this.m_friendlyName = friendlyName;
			this.m_protectionLevel = protectionLevel;
			this.m_useContext = useContext;
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x000188CE File Offset: 0x00016ACE
		public string CreationTitle
		{
			get
			{
				return this.m_creationTitle;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x000188D6 File Offset: 0x00016AD6
		public string Description
		{
			get
			{
				return this.m_description;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x000188DE File Offset: 0x00016ADE
		public string FriendlyName
		{
			get
			{
				return this.m_friendlyName;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x000188E6 File Offset: 0x00016AE6
		public CngUIProtectionLevels ProtectionLevel
		{
			get
			{
				return this.m_protectionLevel;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x000188EE File Offset: 0x00016AEE
		public string UseContext
		{
			get
			{
				return this.m_useContext;
			}
		}

		// Token: 0x0400062A RID: 1578
		private string m_creationTitle;

		// Token: 0x0400062B RID: 1579
		private string m_description;

		// Token: 0x0400062C RID: 1580
		private string m_friendlyName;

		// Token: 0x0400062D RID: 1581
		private CngUIProtectionLevels m_protectionLevel;

		// Token: 0x0400062E RID: 1582
		private string m_useContext;
	}
}
