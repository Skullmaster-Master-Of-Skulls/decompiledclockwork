using System;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000034 RID: 52
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class DataObject
	{
		// Token: 0x0600015C RID: 348 RVA: 0x00006CCC File Offset: 0x00004ECC
		public DataObject()
		{
			this.m_cachedXml = null;
			this.m_elData = new CanonicalXmlNodeList();
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00006CE8 File Offset: 0x00004EE8
		public DataObject(string id, string mimeType, string encoding, XmlElement data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this.m_id = id;
			this.m_mimeType = mimeType;
			this.m_encoding = encoding;
			this.m_elData = new CanonicalXmlNodeList();
			this.m_elData.Add(data);
			this.m_cachedXml = null;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00006D3F File Offset: 0x00004F3F
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00006D47 File Offset: 0x00004F47
		public string Id
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

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00006D57 File Offset: 0x00004F57
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00006D5F File Offset: 0x00004F5F
		public string MimeType
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

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00006D6F File Offset: 0x00004F6F
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00006D77 File Offset: 0x00004F77
		public string Encoding
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

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00006D87 File Offset: 0x00004F87
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00006D90 File Offset: 0x00004F90
		public XmlNodeList Data
		{
			get
			{
				return this.m_elData;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_elData = new CanonicalXmlNodeList();
				foreach (object obj in value)
				{
					XmlNode value2 = (XmlNode)obj;
					this.m_elData.Add(value2);
				}
				this.m_cachedXml = null;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00006E0C File Offset: 0x0000500C
		private bool CacheValid
		{
			get
			{
				return this.m_cachedXml != null;
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00006E18 File Offset: 0x00005018
		public XmlElement GetXml()
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

		// Token: 0x06000168 RID: 360 RVA: 0x00006E48 File Offset: 0x00005048
		internal XmlElement GetXml(XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement("Object", "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(this.m_id))
			{
				xmlElement.SetAttribute("Id", this.m_id);
			}
			if (!string.IsNullOrEmpty(this.m_mimeType))
			{
				xmlElement.SetAttribute("MimeType", this.m_mimeType);
			}
			if (!string.IsNullOrEmpty(this.m_encoding))
			{
				xmlElement.SetAttribute("Encoding", this.m_encoding);
			}
			if (this.m_elData != null)
			{
				foreach (object obj in this.m_elData)
				{
					XmlNode node = (XmlNode)obj;
					xmlElement.AppendChild(document.ImportNode(node, true));
				}
			}
			return xmlElement;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00006F20 File Offset: 0x00005120
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.m_id = Utils.GetAttribute(value, "Id", "http://www.w3.org/2000/09/xmldsig#");
			this.m_mimeType = Utils.GetAttribute(value, "MimeType", "http://www.w3.org/2000/09/xmldsig#");
			this.m_encoding = Utils.GetAttribute(value, "Encoding", "http://www.w3.org/2000/09/xmldsig#");
			foreach (object obj in value.ChildNodes)
			{
				XmlNode value2 = (XmlNode)obj;
				this.m_elData.Add(value2);
			}
			this.m_cachedXml = value;
		}

		// Token: 0x040003A9 RID: 937
		private string m_id;

		// Token: 0x040003AA RID: 938
		private string m_mimeType;

		// Token: 0x040003AB RID: 939
		private string m_encoding;

		// Token: 0x040003AC RID: 940
		private CanonicalXmlNodeList m_elData;

		// Token: 0x040003AD RID: 941
		private XmlElement m_cachedXml;
	}
}
