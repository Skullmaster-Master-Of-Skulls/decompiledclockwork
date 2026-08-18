using System;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000BF RID: 191
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public abstract class EncryptedReference
	{
		// Token: 0x0600048D RID: 1165 RVA: 0x00017233 File Offset: 0x00016233
		protected EncryptedReference() : this(string.Empty, new TransformChain())
		{
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00017245 File Offset: 0x00016245
		protected EncryptedReference(string uri) : this(uri, new TransformChain())
		{
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00017253 File Offset: 0x00016253
		protected EncryptedReference(string uri, TransformChain transformChain)
		{
			this.TransformChain = transformChain;
			this.Uri = uri;
			this.m_cachedXml = null;
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x00017270 File Offset: 0x00016270
		// (set) Token: 0x06000491 RID: 1169 RVA: 0x00017278 File Offset: 0x00016278
		public string Uri
		{
			get
			{
				return this.m_uri;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(SecurityResources.GetResourceString("Cryptography_Xml_UriRequired"));
				}
				this.m_uri = value;
				this.m_cachedXml = null;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x0001729B File Offset: 0x0001629B
		// (set) Token: 0x06000493 RID: 1171 RVA: 0x000172B6 File Offset: 0x000162B6
		public TransformChain TransformChain
		{
			get
			{
				if (this.m_transformChain == null)
				{
					this.m_transformChain = new TransformChain();
				}
				return this.m_transformChain;
			}
			set
			{
				this.m_transformChain = value;
				this.m_cachedXml = null;
			}
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x000172C6 File Offset: 0x000162C6
		public void AddTransform(Transform transform)
		{
			this.TransformChain.Add(transform);
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x000172D4 File Offset: 0x000162D4
		// (set) Token: 0x06000496 RID: 1174 RVA: 0x000172DC File Offset: 0x000162DC
		protected string ReferenceType
		{
			get
			{
				return this.m_referenceType;
			}
			set
			{
				this.m_referenceType = value;
				this.m_cachedXml = null;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x000172EC File Offset: 0x000162EC
		protected internal bool CacheValid
		{
			get
			{
				return this.m_cachedXml != null;
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x000172FC File Offset: 0x000162FC
		public virtual XmlElement GetXml()
		{
			if (this.CacheValid)
			{
				return this.m_cachedXml;
			}
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0001732C File Offset: 0x0001632C
		internal XmlElement GetXml(XmlDocument document)
		{
			if (this.ReferenceType == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_ReferenceTypeRequired"));
			}
			XmlElement xmlElement = document.CreateElement(this.ReferenceType, "http://www.w3.org/2001/04/xmlenc#");
			if (!string.IsNullOrEmpty(this.m_uri))
			{
				xmlElement.SetAttribute("URI", this.m_uri);
			}
			if (this.TransformChain.Count > 0)
			{
				xmlElement.AppendChild(this.TransformChain.GetXml(document, "http://www.w3.org/2000/09/xmldsig#"));
			}
			return xmlElement;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x000173A8 File Offset: 0x000163A8
		public virtual void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.ReferenceType = value.LocalName;
			this.Uri = Utils.GetAttribute(value, "URI", "http://www.w3.org/2001/04/xmlenc#");
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			XmlNode xmlNode = value.SelectSingleNode("ds:Transforms", xmlNamespaceManager);
			if (xmlNode != null)
			{
				this.TransformChain.LoadXml(xmlNode as XmlElement);
			}
			this.m_cachedXml = value;
		}

		// Token: 0x040005B0 RID: 1456
		private string m_uri;

		// Token: 0x040005B1 RID: 1457
		private string m_referenceType;

		// Token: 0x040005B2 RID: 1458
		private TransformChain m_transformChain;

		// Token: 0x040005B3 RID: 1459
		internal XmlElement m_cachedXml;
	}
}
