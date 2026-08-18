using System;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000A1 RID: 161
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class KeyInfoEncryptedKey : KeyInfoClause
	{
		// Token: 0x06000315 RID: 789 RVA: 0x000103E3 File Offset: 0x0000F3E3
		public KeyInfoEncryptedKey()
		{
		}

		// Token: 0x06000316 RID: 790 RVA: 0x000103EB File Offset: 0x0000F3EB
		public KeyInfoEncryptedKey(EncryptedKey encryptedKey)
		{
			this.m_encryptedKey = encryptedKey;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000317 RID: 791 RVA: 0x000103FA File Offset: 0x0000F3FA
		// (set) Token: 0x06000318 RID: 792 RVA: 0x00010402 File Offset: 0x0000F402
		public EncryptedKey EncryptedKey
		{
			get
			{
				return this.m_encryptedKey;
			}
			set
			{
				this.m_encryptedKey = value;
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0001040B File Offset: 0x0000F40B
		public override XmlElement GetXml()
		{
			if (this.m_encryptedKey == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "KeyInfoEncryptedKey");
			}
			return this.m_encryptedKey.GetXml();
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00010435 File Offset: 0x0000F435
		internal override XmlElement GetXml(XmlDocument xmlDocument)
		{
			if (this.m_encryptedKey == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "KeyInfoEncryptedKey");
			}
			return this.m_encryptedKey.GetXml(xmlDocument);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00010460 File Offset: 0x0000F460
		public override void LoadXml(XmlElement value)
		{
			this.m_encryptedKey = new EncryptedKey();
			this.m_encryptedKey.LoadXml(value);
		}

		// Token: 0x04000506 RID: 1286
		private EncryptedKey m_encryptedKey;
	}
}
