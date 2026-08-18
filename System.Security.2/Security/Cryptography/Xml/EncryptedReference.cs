using System;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000038 RID: 56
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public abstract class EncryptedReference
	{
		// Token: 0x0600018D RID: 397 RVA: 0x00007B48 File Offset: 0x00005D48
		protected EncryptedReference() : this(string.Empty, new TransformChain())
		{
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00007B5A File Offset: 0x00005D5A
		protected EncryptedReference(string uri) : this(uri, new TransformChain())
		{
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00007B68 File Offset: 0x00005D68
		protected EncryptedReference(string uri, TransformChain transformChain)
		{
			this.TransformChain = transformChain;
			this.Uri = uri;
			this.m_cachedXml = null;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00007B85 File Offset: 0x00005D85
		// (set) Token: 0x06000191 RID: 401 RVA: 0x00007B8D File Offset: 0x00005D8D
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

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00007BB0 File Offset: 0x00005DB0
		// (set) Token: 0x06000193 RID: 403 RVA: 0x00007BCB File Offset: 0x00005DCB
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

		// Token: 0x06000194 RID: 404 RVA: 0x00007BDB File Offset: 0x00005DDB
		public void AddTransform(Transform transform)
		{
			this.TransformChain.Add(transform);
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00007BE9 File Offset: 0x00005DE9
		// (set) Token: 0x06000196 RID: 406 RVA: 0x00007BF1 File Offset: 0x00005DF1
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

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00007C01 File Offset: 0x00005E01
		protected internal bool CacheValid
		{
			get
			{
				return this.m_cachedXml != null;
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00007C0C File Offset: 0x00005E0C
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

		// Token: 0x06000199 RID: 409 RVA: 0x00007C3C File Offset: 0x00005E3C
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

		// Token: 0x0600019A RID: 410 RVA: 0x00007CB8 File Offset: 0x00005EB8
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

		// Token: 0x040003B2 RID: 946
		private string m_uri;

		// Token: 0x040003B3 RID: 947
		private string m_referenceType;

		// Token: 0x040003B4 RID: 948
		private TransformChain m_transformChain;

		// Token: 0x040003B5 RID: 949
		internal XmlElement m_cachedXml;
	}
}
