using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000051 RID: 81
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class SignedInfo : ICollection, IEnumerable
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000C3A7 File Offset: 0x0000A5A7
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000C3AF File Offset: 0x0000A5AF
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

		// Token: 0x060002A9 RID: 681 RVA: 0x0000C3B8 File Offset: 0x0000A5B8
		public SignedInfo()
		{
			this.m_references = new ArrayList();
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000C3CB File Offset: 0x0000A5CB
		public IEnumerator GetEnumerator()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000C3CB File Offset: 0x0000A5CB
		public void CopyTo(Array array, int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000C3CB File Offset: 0x0000A5CB
		public int Count
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000C3CB File Offset: 0x0000A5CB
		public bool IsReadOnly
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000C3CB File Offset: 0x0000A5CB
		public bool IsSynchronized
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000C3CB File Offset: 0x0000A5CB
		public object SyncRoot
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000C3D2 File Offset: 0x0000A5D2
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x0000C3DA File Offset: 0x0000A5DA
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

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000C3EA File Offset: 0x0000A5EA
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x0000C400 File Offset: 0x0000A600
		public string CanonicalizationMethod
		{
			get
			{
				if (this.m_canonicalizationMethod == null)
				{
					return "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";
				}
				return this.m_canonicalizationMethod;
			}
			set
			{
				this.m_canonicalizationMethod = value;
				this.m_cachedXml = null;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000C410 File Offset: 0x0000A610
		[ComVisible(false)]
		public Transform CanonicalizationMethodObject
		{
			get
			{
				if (this.m_canonicalizationMethodTransform == null)
				{
					this.m_canonicalizationMethodTransform = Utils.CreateFromName<Transform>(this.CanonicalizationMethod);
					if (this.m_canonicalizationMethodTransform == null)
					{
						throw new CryptographicException(string.Format(CultureInfo.CurrentCulture, SecurityResources.GetResourceString("Cryptography_Xml_CreateTransformFailed"), new object[]
						{
							this.CanonicalizationMethod
						}));
					}
					this.m_canonicalizationMethodTransform.SignedXml = this.SignedXml;
					this.m_canonicalizationMethodTransform.Reference = null;
				}
				return this.m_canonicalizationMethodTransform;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000C48A File Offset: 0x0000A68A
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x0000C492 File Offset: 0x0000A692
		public string SignatureMethod
		{
			get
			{
				return this.m_signatureMethod;
			}
			set
			{
				this.m_signatureMethod = value;
				this.m_cachedXml = null;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000C4A2 File Offset: 0x0000A6A2
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x0000C4AA File Offset: 0x0000A6AA
		public string SignatureLength
		{
			get
			{
				return this.m_signatureLength;
			}
			set
			{
				this.m_signatureLength = value;
				this.m_cachedXml = null;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000C4BA File Offset: 0x0000A6BA
		public ArrayList References
		{
			get
			{
				return this.m_references;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000C4C4 File Offset: 0x0000A6C4
		internal bool CacheValid
		{
			get
			{
				if (this.m_cachedXml == null)
				{
					return false;
				}
				foreach (object obj in this.References)
				{
					Reference reference = (Reference)obj;
					if (!reference.CacheValid)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000C530 File Offset: 0x0000A730
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

		// Token: 0x060002BC RID: 700 RVA: 0x0000C560 File Offset: 0x0000A760
		internal XmlElement GetXml(XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement("SignedInfo", "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(this.m_id))
			{
				xmlElement.SetAttribute("Id", this.m_id);
			}
			XmlElement xml = this.CanonicalizationMethodObject.GetXml(document, "CanonicalizationMethod");
			xmlElement.AppendChild(xml);
			if (string.IsNullOrEmpty(this.m_signatureMethod))
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_SignatureMethodRequired"));
			}
			XmlElement xmlElement2 = document.CreateElement("SignatureMethod", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement2.SetAttribute("Algorithm", this.m_signatureMethod);
			if (this.m_signatureLength != null)
			{
				XmlElement xmlElement3 = document.CreateElement(null, "HMACOutputLength", "http://www.w3.org/2000/09/xmldsig#");
				XmlText newChild = document.CreateTextNode(this.m_signatureLength);
				xmlElement3.AppendChild(newChild);
				xmlElement2.AppendChild(xmlElement3);
			}
			xmlElement.AppendChild(xmlElement2);
			if (this.m_references.Count == 0)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_ReferenceElementRequired"));
			}
			for (int i = 0; i < this.m_references.Count; i++)
			{
				Reference reference = (Reference)this.m_references[i];
				xmlElement.AppendChild(reference.GetXml(document));
			}
			return xmlElement;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000C694 File Offset: 0x0000A894
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!value.LocalName.Equals("SignedInfo"))
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "SignedInfo");
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			int num = 0;
			this.m_id = Utils.GetAttribute(value, "Id", "http://www.w3.org/2000/09/xmldsig#");
			if (!Utils.VerifyAttributes(value, "Id"))
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "SignedInfo");
			}
			XmlNodeList xmlNodeList = value.SelectNodes("ds:CanonicalizationMethod", xmlNamespaceManager);
			if (xmlNodeList == null || xmlNodeList.Count == 0 || (!Utils.GetAllowAdditionalSignatureNodes() && xmlNodeList.Count > 1))
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "SignedInfo/CanonicalizationMethod");
			}
			XmlElement xmlElement = xmlNodeList.Item(0) as XmlElement;
			num += xmlNodeList.Count;
			this.m_canonicalizationMethod = Utils.GetAttribute(xmlElement, "Algorithm", "http://www.w3.org/2000/09/xmldsig#");
			if ((this.m_canonicalizationMethod == null && !Utils.GetSkipSignatureAttributeEnforcement()) || !Utils.VerifyAttributes(xmlElement, "Algorithm"))
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "SignedInfo/CanonicalizationMethod");
			}
			this.m_canonicalizationMethodTransform = null;
			if (xmlElement.ChildNodes.Count > 0)
			{
				this.CanonicalizationMethodObject.LoadInnerXml(xmlElement.ChildNodes);
			}
			XmlNodeList xmlNodeList2 = value.SelectNodes("ds:SignatureMethod", xmlNamespaceManager);
			if (xmlNodeList2 == null || xmlNodeList2.Count == 0 || (!Utils.GetAllowAdditionalSignatureNodes() && xmlNodeList2.Count > 1))
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "SignedInfo/SignatureMethod");
			}
			XmlElement xmlElement2 = xmlNodeList2.Item(0) as XmlElement;
			num += xmlNodeList2.Count;
			this.m_signatureMethod = Utils.GetAttribute(xmlElement2, "Algorithm", "http://www.w3.org/2000/09/xmldsig#");
			if ((this.m_signatureMethod == null && !Utils.GetSkipSignatureAttributeEnforcement()) || !Utils.VerifyAttributes(xmlElement2, "Algorithm"))
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "SignedInfo/SignatureMethod");
			}
			XmlElement xmlElement3 = xmlElement2.SelectSingleNode("ds:HMACOutputLength", xmlNamespaceManager) as XmlElement;
			if (xmlElement3 != null)
			{
				this.m_signatureLength = xmlElement3.InnerXml;
			}
			this.m_references.Clear();
			XmlNodeList xmlNodeList3 = value.SelectNodes("ds:Reference", xmlNamespaceManager);
			if (xmlNodeList3 != null)
			{
				if ((long)xmlNodeList3.Count > Utils.GetMaxReferencesPerSignedInfo())
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "SignedInfo/Reference");
				}
				foreach (object obj in xmlNodeList3)
				{
					XmlNode xmlNode = (XmlNode)obj;
					XmlElement value2 = xmlNode as XmlElement;
					Reference reference = new Reference();
					this.AddReference(reference);
					reference.LoadXml(value2);
				}
				num += xmlNodeList3.Count;
			}
			if (!Utils.GetAllowAdditionalSignatureNodes() && value.SelectNodes("*").Count != num)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "SignedInfo");
			}
			this.m_cachedXml = value;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000C9B0 File Offset: 0x0000ABB0
		public void AddReference(Reference reference)
		{
			if (reference == null)
			{
				throw new ArgumentNullException("reference");
			}
			reference.SignedXml = this.SignedXml;
			this.m_references.Add(reference);
		}

		// Token: 0x04000411 RID: 1041
		private string m_id;

		// Token: 0x04000412 RID: 1042
		private string m_canonicalizationMethod;

		// Token: 0x04000413 RID: 1043
		private string m_signatureMethod;

		// Token: 0x04000414 RID: 1044
		private string m_signatureLength;

		// Token: 0x04000415 RID: 1045
		private ArrayList m_references;

		// Token: 0x04000416 RID: 1046
		private XmlElement m_cachedXml;

		// Token: 0x04000417 RID: 1047
		private SignedXml m_signedXml;

		// Token: 0x04000418 RID: 1048
		private Transform m_canonicalizationMethodTransform;
	}
}
