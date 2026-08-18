using System;
using System.Collections;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000A7 RID: 167
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class Signature
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00011AD8 File Offset: 0x00010AD8
		// (set) Token: 0x06000357 RID: 855 RVA: 0x00011AE0 File Offset: 0x00010AE0
		internal SignedXml SignedXml
		{
			get
			{
				return this.m_signedXml;
			}
			set
			{
				this.m_signedXml = value;
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00011AE9 File Offset: 0x00010AE9
		public Signature()
		{
			this.m_embeddedObjects = new ArrayList();
			this.m_referencedItems = new CanonicalXmlNodeList();
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00011B07 File Offset: 0x00010B07
		// (set) Token: 0x0600035A RID: 858 RVA: 0x00011B0F File Offset: 0x00010B0F
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

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00011B18 File Offset: 0x00010B18
		// (set) Token: 0x0600035C RID: 860 RVA: 0x00011B20 File Offset: 0x00010B20
		public SignedInfo SignedInfo
		{
			get
			{
				return this.m_signedInfo;
			}
			set
			{
				this.m_signedInfo = value;
				if (this.SignedXml != null && this.m_signedInfo != null)
				{
					this.m_signedInfo.SignedXml = this.SignedXml;
				}
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00011B4A File Offset: 0x00010B4A
		// (set) Token: 0x0600035E RID: 862 RVA: 0x00011B52 File Offset: 0x00010B52
		public byte[] SignatureValue
		{
			get
			{
				return this.m_signatureValue;
			}
			set
			{
				this.m_signatureValue = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600035F RID: 863 RVA: 0x00011B5B File Offset: 0x00010B5B
		// (set) Token: 0x06000360 RID: 864 RVA: 0x00011B76 File Offset: 0x00010B76
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

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00011B7F File Offset: 0x00010B7F
		// (set) Token: 0x06000362 RID: 866 RVA: 0x00011B87 File Offset: 0x00010B87
		public IList ObjectList
		{
			get
			{
				return this.m_embeddedObjects;
			}
			set
			{
				this.m_embeddedObjects = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00011B90 File Offset: 0x00010B90
		internal CanonicalXmlNodeList ReferencedItems
		{
			get
			{
				return this.m_referencedItems;
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00011B98 File Offset: 0x00010B98
		public XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00011BBC File Offset: 0x00010BBC
		internal XmlElement GetXml(XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement("Signature", "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(this.m_id))
			{
				xmlElement.SetAttribute("Id", this.m_id);
			}
			if (this.m_signedInfo == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_SignedInfoRequired"));
			}
			xmlElement.AppendChild(this.m_signedInfo.GetXml(document));
			if (this.m_signatureValue == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_SignatureValueRequired"));
			}
			XmlElement xmlElement2 = document.CreateElement("SignatureValue", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement2.AppendChild(document.CreateTextNode(Convert.ToBase64String(this.m_signatureValue)));
			if (!string.IsNullOrEmpty(this.m_signatureValueId))
			{
				xmlElement2.SetAttribute("Id", this.m_signatureValueId);
			}
			xmlElement.AppendChild(xmlElement2);
			if (this.KeyInfo.Count > 0)
			{
				xmlElement.AppendChild(this.KeyInfo.GetXml(document));
			}
			foreach (object obj in this.m_embeddedObjects)
			{
				DataObject dataObject = obj as DataObject;
				if (dataObject != null)
				{
					xmlElement.AppendChild(dataObject.GetXml(document));
				}
			}
			return xmlElement;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00011D0C File Offset: 0x00010D0C
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!value.LocalName.Equals("Signature"))
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "Signature");
			}
			this.m_id = Utils.GetAttribute(value, "Id", "http://www.w3.org/2000/09/xmldsig#");
			if (!Utils.VerifyAttributes(value, "Id"))
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "Signature");
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			int num = 0;
			XmlNodeList xmlNodeList = value.SelectNodes("ds:SignedInfo", xmlNamespaceManager);
			if (xmlNodeList == null || xmlNodeList.Count == 0 || (!Utils.GetAllowAdditionalSignatureNodes() && xmlNodeList.Count > 1))
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "SignedInfo");
			}
			XmlElement value2 = xmlNodeList[0] as XmlElement;
			num += xmlNodeList.Count;
			this.SignedInfo = new SignedInfo();
			this.SignedInfo.LoadXml(value2);
			XmlNodeList xmlNodeList2 = value.SelectNodes("ds:SignatureValue", xmlNamespaceManager);
			if (xmlNodeList2 == null || xmlNodeList2.Count == 0 || (!Utils.GetAllowAdditionalSignatureNodes() && xmlNodeList2.Count > 1))
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "SignatureValue");
			}
			XmlElement xmlElement = xmlNodeList2[0] as XmlElement;
			num += xmlNodeList2.Count;
			this.m_signatureValue = Convert.FromBase64String(Utils.DiscardWhiteSpaces(xmlElement.InnerText));
			this.m_signatureValueId = Utils.GetAttribute(xmlElement, "Id", "http://www.w3.org/2000/09/xmldsig#");
			if (!Utils.VerifyAttributes(xmlElement, "Id"))
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "SignatureValue");
			}
			XmlNodeList xmlNodeList3 = value.SelectNodes("ds:KeyInfo", xmlNamespaceManager);
			this.m_keyInfo = new KeyInfo();
			if (xmlNodeList3 != null)
			{
				if (!Utils.GetAllowAdditionalSignatureNodes() && xmlNodeList3.Count > 1)
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "KeyInfo");
				}
				foreach (object obj in xmlNodeList3)
				{
					XmlNode xmlNode = (XmlNode)obj;
					XmlElement xmlElement2 = xmlNode as XmlElement;
					if (xmlElement2 != null)
					{
						this.m_keyInfo.LoadXml(xmlElement2);
					}
				}
				num += xmlNodeList3.Count;
			}
			XmlNodeList xmlNodeList4 = value.SelectNodes("ds:Object", xmlNamespaceManager);
			this.m_embeddedObjects.Clear();
			if (xmlNodeList4 != null)
			{
				foreach (object obj2 in xmlNodeList4)
				{
					XmlNode xmlNode2 = (XmlNode)obj2;
					XmlElement xmlElement3 = xmlNode2 as XmlElement;
					if (xmlElement3 != null)
					{
						DataObject dataObject = new DataObject();
						dataObject.LoadXml(xmlElement3);
						this.m_embeddedObjects.Add(dataObject);
					}
				}
				num += xmlNodeList4.Count;
			}
			XmlNodeList xmlNodeList5 = value.SelectNodes("//*[@Id]", xmlNamespaceManager);
			if (xmlNodeList5 != null)
			{
				foreach (object obj3 in xmlNodeList5)
				{
					XmlNode value3 = (XmlNode)obj3;
					this.m_referencedItems.Add(value3);
				}
			}
			if (!Utils.GetAllowAdditionalSignatureNodes() && value.SelectNodes("*").Count != num)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "Signature");
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000120A0 File Offset: 0x000110A0
		public void AddObject(DataObject dataObject)
		{
			this.m_embeddedObjects.Add(dataObject);
		}

		// Token: 0x0400051F RID: 1311
		private string m_id;

		// Token: 0x04000520 RID: 1312
		private SignedInfo m_signedInfo;

		// Token: 0x04000521 RID: 1313
		private byte[] m_signatureValue;

		// Token: 0x04000522 RID: 1314
		private string m_signatureValueId;

		// Token: 0x04000523 RID: 1315
		private KeyInfo m_keyInfo;

		// Token: 0x04000524 RID: 1316
		private IList m_embeddedObjects;

		// Token: 0x04000525 RID: 1317
		private CanonicalXmlNodeList m_referencedItems;

		// Token: 0x04000526 RID: 1318
		private SignedXml m_signedXml;
	}
}
