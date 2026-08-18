using System;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200003C RID: 60
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public abstract class EncryptedType
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00007F5E File Offset: 0x0000615E
		internal bool CacheValid
		{
			get
			{
				return this.m_cachedXml != null;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00007F69 File Offset: 0x00006169
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00007F71 File Offset: 0x00006171
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

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00007F81 File Offset: 0x00006181
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00007F89 File Offset: 0x00006189
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

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00007F99 File Offset: 0x00006199
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00007FA1 File Offset: 0x000061A1
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

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00007FB1 File Offset: 0x000061B1
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00007FB9 File Offset: 0x000061B9
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

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00007FC9 File Offset: 0x000061C9
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00007FE4 File Offset: 0x000061E4
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

		// Token: 0x060001B4 RID: 436 RVA: 0x00007FF0 File Offset: 0x000061F0
		internal static void IncrementLoadXmlCurrentThreadDepth()
		{
			int dangerousMaxRecursionDepth = Utils.GetDangerousMaxRecursionDepth();
			if (dangerousMaxRecursionDepth > 0 && EncryptedType.t_depth > dangerousMaxRecursionDepth)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "MAX_DEPTH_EXCEEDED");
			}
			EncryptedType.t_depth++;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00008030 File Offset: 0x00006230
		internal static void DecrementLoadXmlCurrentThreadDepth()
		{
			EncryptedType.t_depth--;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000803E File Offset: 0x0000623E
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00008046 File Offset: 0x00006246
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

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00008056 File Offset: 0x00006256
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

		// Token: 0x060001B9 RID: 441 RVA: 0x00008071 File Offset: 0x00006271
		public void AddProperty(EncryptionProperty ep)
		{
			this.EncryptionProperties.Add(ep);
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00008080 File Offset: 0x00006280
		// (set) Token: 0x060001BB RID: 443 RVA: 0x0000809B File Offset: 0x0000629B
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

		// Token: 0x060001BC RID: 444
		public abstract void LoadXml(XmlElement value);

		// Token: 0x060001BD RID: 445
		public abstract XmlElement GetXml();

		// Token: 0x040003B7 RID: 951
		[ThreadStatic]
		private static int t_depth;

		// Token: 0x040003B8 RID: 952
		private string m_id;

		// Token: 0x040003B9 RID: 953
		private string m_type;

		// Token: 0x040003BA RID: 954
		private string m_mimeType;

		// Token: 0x040003BB RID: 955
		private string m_encoding;

		// Token: 0x040003BC RID: 956
		private EncryptionMethod m_encryptionMethod;

		// Token: 0x040003BD RID: 957
		private CipherData m_cipherData;

		// Token: 0x040003BE RID: 958
		private EncryptionPropertyCollection m_props;

		// Token: 0x040003BF RID: 959
		private KeyInfo m_keyInfo;

		// Token: 0x040003C0 RID: 960
		internal XmlElement m_cachedXml;
	}
}
