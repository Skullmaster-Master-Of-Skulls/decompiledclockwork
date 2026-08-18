using System;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000BB RID: 187
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public abstract class EncryptedType
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x00016B4E File Offset: 0x00015B4E
		internal bool CacheValid
		{
			get
			{
				return this.m_cachedXml != null;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x00016B5C File Offset: 0x00015B5C
		// (set) Token: 0x0600044B RID: 1099 RVA: 0x00016B64 File Offset: 0x00015B64
		public virtual string Id
		{
			get
			{
				return this.m_id;
			}
			set
			{
				this.m_id = value;
				this.m_cachedXml = null;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00016B74 File Offset: 0x00015B74
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x00016B7C File Offset: 0x00015B7C
		public virtual string Type
		{
			get
			{
				return this.m_type;
			}
			set
			{
				this.m_type = value;
				this.m_cachedXml = null;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x00016B8C File Offset: 0x00015B8C
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x00016B94 File Offset: 0x00015B94
		public virtual string MimeType
		{
			get
			{
				return this.m_mimeType;
			}
			set
			{
				this.m_mimeType = value;
				this.m_cachedXml = null;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00016BA4 File Offset: 0x00015BA4
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x00016BAC File Offset: 0x00015BAC
		public virtual string Encoding
		{
			get
			{
				return this.m_encoding;
			}
			set
			{
				this.m_encoding = value;
				this.m_cachedXml = null;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x00016BBC File Offset: 0x00015BBC
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x00016BD7 File Offset: 0x00015BD7
		public KeyInfo KeyInfo
		{
			get
			{
				if (this.m_keyInfo == null)
				{
					this.m_keyInfo = new KeyInfo();
				}
				return this.m_keyInfo;
			}
			set
			{
				this.m_keyInfo = value;
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00016BE0 File Offset: 0x00015BE0
		internal static void IncrementLoadXmlCurrentThreadDepth()
		{
			int dangerousMaxRecursionDepth = Utils.GetDangerousMaxRecursionDepth();
			if (dangerousMaxRecursionDepth > 0 && EncryptedType.t_depth > dangerousMaxRecursionDepth)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "MAX_DEPTH_EXCEEDED");
			}
			EncryptedType.t_depth++;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00016C20 File Offset: 0x00015C20
		internal static void DecrementLoadXmlCurrentThreadDepth()
		{
			EncryptedType.t_depth--;
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x00016C2E File Offset: 0x00015C2E
		// (set) Token: 0x06000457 RID: 1111 RVA: 0x00016C36 File Offset: 0x00015C36
		public virtual EncryptionMethod EncryptionMethod
		{
			get
			{
				return this.m_encryptionMethod;
			}
			set
			{
				this.m_encryptionMethod = value;
				this.m_cachedXml = null;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x00016C46 File Offset: 0x00015C46
		public virtual EncryptionPropertyCollection EncryptionProperties
		{
			get
			{
				if (this.m_props == null)
				{
					this.m_props = new EncryptionPropertyCollection();
				}
				return this.m_props;
			}
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00016C61 File Offset: 0x00015C61
		public void AddProperty(EncryptionProperty ep)
		{
			this.EncryptionProperties.Add(ep);
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00016C70 File Offset: 0x00015C70
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x00016C8B File Offset: 0x00015C8B
		public virtual CipherData CipherData
		{
			get
			{
				if (this.m_cipherData == null)
				{
					this.m_cipherData = new CipherData();
				}
				return this.m_cipherData;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_cipherData = value;
				this.m_cachedXml = null;
			}
		}

		// Token: 0x0600045C RID: 1116
		public abstract void LoadXml(XmlElement value);

		// Token: 0x0600045D RID: 1117
		public abstract XmlElement GetXml();

		// Token: 0x0400059E RID: 1438
		[ThreadStatic]
		private static int t_depth;

		// Token: 0x0400059F RID: 1439
		private string m_id;

		// Token: 0x040005A0 RID: 1440
		private string m_type;

		// Token: 0x040005A1 RID: 1441
		private string m_mimeType;

		// Token: 0x040005A2 RID: 1442
		private string m_encoding;

		// Token: 0x040005A3 RID: 1443
		private EncryptionMethod m_encryptionMethod;

		// Token: 0x040005A4 RID: 1444
		private CipherData m_cipherData;

		// Token: 0x040005A5 RID: 1445
		private EncryptionPropertyCollection m_props;

		// Token: 0x040005A6 RID: 1446
		private KeyInfo m_keyInfo;

		// Token: 0x040005A7 RID: 1447
		internal XmlElement m_cachedXml;
	}
}
