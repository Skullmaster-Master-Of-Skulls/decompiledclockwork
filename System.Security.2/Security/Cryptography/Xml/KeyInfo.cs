using System;
using System.Collections;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000044 RID: 68
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class KeyInfo : IEnumerable
	{
		// Token: 0x06000227 RID: 551 RVA: 0x00009F0B File Offset: 0x0000810B
		public KeyInfo()
		{
			this.m_KeyInfoClauses = new ArrayList();
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00009F1E File Offset: 0x0000811E
		// (set) Token: 0x06000229 RID: 553 RVA: 0x00009F26 File Offset: 0x00008126
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

		// Token: 0x0600022A RID: 554 RVA: 0x00009F30 File Offset: 0x00008130
		public XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00009F54 File Offset: 0x00008154
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

		// Token: 0x0600022C RID: 556 RVA: 0x00009FCC File Offset: 0x000081CC
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

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000A154 File Offset: 0x00008354
		public int Count
		{
			get
			{
				return this.m_KeyInfoClauses.Count;
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000A161 File Offset: 0x00008361
		public void AddClause(KeyInfoClause clause)
		{
			this.m_KeyInfoClauses.Add(clause);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000A170 File Offset: 0x00008370
		public IEnumerator GetEnumerator()
		{
			return this.m_KeyInfoClauses.GetEnumerator();
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000A180 File Offset: 0x00008380
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

		// Token: 0x040003E9 RID: 1001
		private string m_id;

		// Token: 0x040003EA RID: 1002
		private ArrayList m_KeyInfoClauses;
	}
}
