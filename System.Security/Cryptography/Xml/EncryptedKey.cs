using System;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000C4 RID: 196
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EncryptedKey : EncryptedType
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x000179EA File Offset: 0x000169EA
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x00017A05 File Offset: 0x00016A05
		public string Recipient
		{
			get
			{
				if (this.m_recipient == null)
				{
					this.m_recipient = string.Empty;
				}
				return this.m_recipient;
			}
			set
			{
				this.m_recipient = value;
				this.m_cachedXml = null;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00017A15 File Offset: 0x00016A15
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x00017A1D File Offset: 0x00016A1D
		public string CarriedKeyName
		{
			get
			{
				return this.m_carriedKeyName;
			}
			set
			{
				this.m_carriedKeyName = value;
				this.m_cachedXml = null;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00017A2D File Offset: 0x00016A2D
		public ReferenceList ReferenceList
		{
			get
			{
				if (this.m_referenceList == null)
				{
					this.m_referenceList = new ReferenceList();
				}
				return this.m_referenceList;
			}
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00017A48 File Offset: 0x00016A48
		public void AddReference(DataReference dataReference)
		{
			this.ReferenceList.Add(dataReference);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00017A57 File Offset: 0x00016A57
		public void AddReference(KeyReference keyReference)
		{
			this.ReferenceList.Add(keyReference);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00017A68 File Offset: 0x00016A68
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			EncryptedType.IncrementLoadXmlCurrentThreadDepth();
			try
			{
				XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
				xmlNamespaceManager.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
				xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
				this.Id = Utils.GetAttribute(value, "Id", "http://www.w3.org/2001/04/xmlenc#");
				this.Type = Utils.GetAttribute(value, "Type", "http://www.w3.org/2001/04/xmlenc#");
				this.MimeType = Utils.GetAttribute(value, "MimeType", "http://www.w3.org/2001/04/xmlenc#");
				this.Encoding = Utils.GetAttribute(value, "Encoding", "http://www.w3.org/2001/04/xmlenc#");
				this.Recipient = Utils.GetAttribute(value, "Recipient", "http://www.w3.org/2001/04/xmlenc#");
				XmlNode xmlNode = value.SelectSingleNode("enc:EncryptionMethod", xmlNamespaceManager);
				this.EncryptionMethod = new EncryptionMethod();
				if (xmlNode != null)
				{
					this.EncryptionMethod.LoadXml(xmlNode as XmlElement);
				}
				base.KeyInfo = new KeyInfo();
				XmlNode xmlNode2 = value.SelectSingleNode("ds:KeyInfo", xmlNamespaceManager);
				if (xmlNode2 != null)
				{
					base.KeyInfo.LoadXml(xmlNode2 as XmlElement);
				}
				XmlNode xmlNode3 = value.SelectSingleNode("enc:CipherData", xmlNamespaceManager);
				if (xmlNode3 == null)
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_MissingCipherData"));
				}
				this.CipherData = new CipherData();
				this.CipherData.LoadXml(xmlNode3 as XmlElement);
				XmlNode xmlNode4 = value.SelectSingleNode("enc:EncryptionProperties", xmlNamespaceManager);
				if (xmlNode4 != null)
				{
					XmlNodeList xmlNodeList = xmlNode4.SelectNodes("enc:EncryptionProperty", xmlNamespaceManager);
					if (xmlNodeList != null)
					{
						foreach (object obj in xmlNodeList)
						{
							XmlNode xmlNode5 = (XmlNode)obj;
							EncryptionProperty encryptionProperty = new EncryptionProperty();
							encryptionProperty.LoadXml(xmlNode5 as XmlElement);
							this.EncryptionProperties.Add(encryptionProperty);
						}
					}
				}
				XmlNode xmlNode6 = value.SelectSingleNode("enc:CarriedKeyName", xmlNamespaceManager);
				if (xmlNode6 != null)
				{
					this.CarriedKeyName = xmlNode6.InnerText;
				}
				XmlNode xmlNode7 = value.SelectSingleNode("enc:ReferenceList", xmlNamespaceManager);
				if (xmlNode7 != null)
				{
					XmlNodeList xmlNodeList2 = xmlNode7.SelectNodes("enc:DataReference", xmlNamespaceManager);
					if (xmlNodeList2 != null)
					{
						foreach (object obj2 in xmlNodeList2)
						{
							XmlNode xmlNode8 = (XmlNode)obj2;
							DataReference dataReference = new DataReference();
							dataReference.LoadXml(xmlNode8 as XmlElement);
							this.ReferenceList.Add(dataReference);
						}
					}
					XmlNodeList xmlNodeList3 = xmlNode7.SelectNodes("enc:KeyReference", xmlNamespaceManager);
					if (xmlNodeList3 != null)
					{
						foreach (object obj3 in xmlNodeList3)
						{
							XmlNode xmlNode9 = (XmlNode)obj3;
							KeyReference keyReference = new KeyReference();
							keyReference.LoadXml(xmlNode9 as XmlElement);
							this.ReferenceList.Add(keyReference);
						}
					}
				}
				this.m_cachedXml = value;
			}
			finally
			{
				EncryptedType.DecrementLoadXmlCurrentThreadDepth();
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00017DC4 File Offset: 0x00016DC4
		public override XmlElement GetXml()
		{
			if (base.CacheValid)
			{
				return this.m_cachedXml;
			}
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00017DF4 File Offset: 0x00016DF4
		internal XmlElement GetXml(XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement("EncryptedKey", "http://www.w3.org/2001/04/xmlenc#");
			if (!string.IsNullOrEmpty(this.Id))
			{
				xmlElement.SetAttribute("Id", this.Id);
			}
			if (!string.IsNullOrEmpty(this.Type))
			{
				xmlElement.SetAttribute("Type", this.Type);
			}
			if (!string.IsNullOrEmpty(this.MimeType))
			{
				xmlElement.SetAttribute("MimeType", this.MimeType);
			}
			if (!string.IsNullOrEmpty(this.Encoding))
			{
				xmlElement.SetAttribute("Encoding", this.Encoding);
			}
			if (!string.IsNullOrEmpty(this.Recipient))
			{
				xmlElement.SetAttribute("Recipient", this.Recipient);
			}
			if (this.EncryptionMethod != null)
			{
				xmlElement.AppendChild(this.EncryptionMethod.GetXml(document));
			}
			if (base.KeyInfo.Count > 0)
			{
				xmlElement.AppendChild(base.KeyInfo.GetXml(document));
			}
			if (this.CipherData == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_MissingCipherData"));
			}
			xmlElement.AppendChild(this.CipherData.GetXml(document));
			if (this.EncryptionProperties.Count > 0)
			{
				XmlElement xmlElement2 = document.CreateElement("EncryptionProperties", "http://www.w3.org/2001/04/xmlenc#");
				for (int i = 0; i < this.EncryptionProperties.Count; i++)
				{
					EncryptionProperty encryptionProperty = this.EncryptionProperties.Item(i);
					xmlElement2.AppendChild(encryptionProperty.GetXml(document));
				}
				xmlElement.AppendChild(xmlElement2);
			}
			if (this.ReferenceList.Count > 0)
			{
				XmlElement xmlElement3 = document.CreateElement("ReferenceList", "http://www.w3.org/2001/04/xmlenc#");
				for (int j = 0; j < this.ReferenceList.Count; j++)
				{
					xmlElement3.AppendChild(this.ReferenceList[j].GetXml(document));
				}
				xmlElement.AppendChild(xmlElement3);
			}
			if (this.CarriedKeyName != null)
			{
				XmlElement xmlElement4 = document.CreateElement("CarriedKeyName", "http://www.w3.org/2001/04/xmlenc#");
				XmlText newChild = document.CreateTextNode(this.CarriedKeyName);
				xmlElement4.AppendChild(newChild);
				xmlElement.AppendChild(xmlElement4);
			}
			return xmlElement;
		}

		// Token: 0x040005B5 RID: 1461
		private string m_recipient;

		// Token: 0x040005B6 RID: 1462
		private string m_carriedKeyName;

		// Token: 0x040005B7 RID: 1463
		private ReferenceList m_referenceList;
	}
}
