using System;
using System.Collections;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200009B RID: 155
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class KeyInfo : IEnumerable
	{
		// Token: 0x060002E8 RID: 744 RVA: 0x0000FBD4 File Offset: 0x0000EBD4
		public KeyInfo()
		{
			this.m_KeyInfoClauses = new ArrayList();
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000FBE7 File Offset: 0x0000EBE7
		// (set) Token: 0x060002EA RID: 746 RVA: 0x0000FBEF File Offset: 0x0000EBEF
		public string Id
		{
			get
			{
				return this.m_id;
			}
			set
			{
				this.m_id = value;
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000FBF8 File Offset: 0x0000EBF8
		public XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000FC1C File Offset: 0x0000EC1C
		internal XmlElement GetXml(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = xmlDocument.CreateElement("KeyInfo", "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(this.m_id))
			{
				xmlElement.SetAttribute("Id", this.m_id);
			}
			for (int i = 0; i < this.m_KeyInfoClauses.Count; i++)
			{
				XmlElement xml = ((KeyInfoClause)this.m_KeyInfoClauses[i]).GetXml(xmlDocument);
				if (xml != null)
				{
					xmlElement.AppendChild(xml);
				}
			}
			return xmlElement;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000FC94 File Offset: 0x0000EC94
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			EncryptedType.IncrementLoadXmlCurrentThreadDepth();
			try
			{
				this.m_id = Utils.GetAttribute(value, "Id", "http://www.w3.org/2000/09/xmldsig#");
				if (!Utils.VerifyAttributes(value, "Id"))
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "KeyInfo");
				}
				for (XmlNode xmlNode = value.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
				{
					XmlElement xmlElement = xmlNode as XmlElement;
					if (xmlElement != null)
					{
						string text = xmlElement.NamespaceURI + " " + xmlElement.LocalName;
						if (text == "http://www.w3.org/2000/09/xmldsig# KeyValue")
						{
							if (!Utils.VerifyAttributes(xmlElement, null))
							{
								throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "KeyInfo/KeyValue");
							}
							XmlNodeList childNodes = xmlElement.ChildNodes;
							foreach (object obj in childNodes)
							{
								XmlNode xmlNode2 = (XmlNode)obj;
								XmlElement xmlElement2 = xmlNode2 as XmlElement;
								if (xmlElement2 != null)
								{
									text = text + "/" + xmlElement2.LocalName;
									break;
								}
							}
						}
						KeyInfoClause keyInfoClause = Utils.CreateFromName<KeyInfoClause>(text);
						if (keyInfoClause == null)
						{
							keyInfoClause = new KeyInfoNode();
						}
						keyInfoClause.LoadXml(xmlElement);
						this.AddClause(keyInfoClause);
					}
				}
			}
			finally
			{
				EncryptedType.DecrementLoadXmlCurrentThreadDepth();
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000FE1C File Offset: 0x0000EE1C
		public int Count
		{
			get
			{
				return this.m_KeyInfoClauses.Count;
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000FE29 File Offset: 0x0000EE29
		public void AddClause(KeyInfoClause clause)
		{
			this.m_KeyInfoClauses.Add(clause);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000FE38 File Offset: 0x0000EE38
		public IEnumerator GetEnumerator()
		{
			return this.m_KeyInfoClauses.GetEnumerator();
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000FE48 File Offset: 0x0000EE48
		public IEnumerator GetEnumerator(Type requestedObjectType)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.m_KeyInfoClauses)
			{
				if (requestedObjectType.Equals(obj.GetType()))
				{
					arrayList.Add(obj);
				}
			}
			return arrayList.GetEnumerator();
		}

		// Token: 0x040004FF RID: 1279
		private string m_id;

		// Token: 0x04000500 RID: 1280
		private ArrayList m_KeyInfoClauses;
	}
}
