using System;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200009A RID: 154
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class DataObject
	{
		// Token: 0x060002DA RID: 730 RVA: 0x0000F8C3 File Offset: 0x0000E8C3
		public DataObject()
		{
			this.m_cachedXml = null;
			this.m_elData = new CanonicalXmlNodeList();
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000F8E0 File Offset: 0x0000E8E0
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

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000F937 File Offset: 0x0000E937
		// (set) Token: 0x060002DD RID: 733 RVA: 0x0000F93F File Offset: 0x0000E93F
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

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000F94F File Offset: 0x0000E94F
		// (set) Token: 0x060002DF RID: 735 RVA: 0x0000F957 File Offset: 0x0000E957
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

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000F967 File Offset: 0x0000E967
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x0000F96F File Offset: 0x0000E96F
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

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000F97F File Offset: 0x0000E97F
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x0000F988 File Offset: 0x0000E988
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

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000FA04 File Offset: 0x0000EA04
		private bool CacheValid
		{
			get
			{
				return this.m_cachedXml != null;
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000FA14 File Offset: 0x0000EA14
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

		// Token: 0x060002E6 RID: 742 RVA: 0x0000FA44 File Offset: 0x0000EA44
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

		// Token: 0x060002E7 RID: 743 RVA: 0x0000FB1C File Offset: 0x0000EB1C
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

		// Token: 0x040004FA RID: 1274
		private string m_id;

		// Token: 0x040004FB RID: 1275
		private string m_mimeType;

		// Token: 0x040004FC RID: 1276
		private string m_encoding;

		// Token: 0x040004FD RID: 1277
		private CanonicalXmlNodeList m_elData;

		// Token: 0x040004FE RID: 1278
		private XmlElement m_cachedXml;
	}
}
