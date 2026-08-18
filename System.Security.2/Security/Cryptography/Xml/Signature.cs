using System;
using System.Collections;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000050 RID: 80
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class Signature
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000BDD0 File Offset: 0x00009FD0
		// (set) Token: 0x06000296 RID: 662 RVA: 0x0000BDD8 File Offset: 0x00009FD8
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

		// Token: 0x06000297 RID: 663 RVA: 0x0000BDE1 File Offset: 0x00009FE1
		public Signature()
		{
			this.m_embeddedObjects = new ArrayList();
			this.m_referencedItems = new CanonicalXmlNodeList();
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000BDFF File Offset: 0x00009FFF
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000BE07 File Offset: 0x0000A007
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

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000BE10 File Offset: 0x0000A010
		// (set) Token: 0x0600029B RID: 667 RVA: 0x0000BE18 File Offset: 0x0000A018
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

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000BE42 File Offset: 0x0000A042
		// (set) Token: 0x0600029D RID: 669 RVA: 0x0000BE4A File Offset: 0x0000A04A
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

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000BE53 File Offset: 0x0000A053
		// (set) Token: 0x0600029F RID: 671 RVA: 0x0000BE6E File Offset: 0x0000A06E
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

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000BE77 File Offset: 0x0000A077
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x0000BE7F File Offset: 0x0000A07F
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

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000BE88 File Offset: 0x0000A088
		internal CanonicalXmlNodeList ReferencedItems
		{
			get
			{
				return this.m_referencedItems;
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000BE90 File Offset: 0x0000A090
		public XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000BEB4 File Offset: 0x0000A0B4
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

		// Token: 0x060002A5 RID: 677 RVA: 0x0000C004 File Offset: 0x0000A204
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

		// Token: 0x060002A6 RID: 678 RVA: 0x0000C398 File Offset: 0x0000A598
		public void AddObject(DataObject dataObject)
		{
			this.m_embeddedObjects.Add(dataObject);
		}

		// Token: 0x04000409 RID: 1033
		private string m_id;

		// Token: 0x0400040A RID: 1034
		private SignedInfo m_signedInfo;

		// Token: 0x0400040B RID: 1035
		private byte[] m_signatureValue;

		// Token: 0x0400040C RID: 1036
		private string m_signatureValueId;

		// Token: 0x0400040D RID: 1037
		private KeyInfo m_keyInfo;

		// Token: 0x0400040E RID: 1038
		private IList m_embeddedObjects;

		// Token: 0x0400040F RID: 1039
		private CanonicalXmlNodeList m_referencedItems;

		// Token: 0x04000410 RID: 1040
		private SignedXml m_signedXml;
	}
}
