using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000A8 RID: 168
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class SignedInfo : ICollection, IEnumerable
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000368 RID: 872 RVA: 0x000120AF File Offset: 0x000110AF
		// (set) Token: 0x06000369 RID: 873 RVA: 0x000120B7 File Offset: 0x000110B7
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

		// Token: 0x0600036A RID: 874 RVA: 0x000120C0 File Offset: 0x000110C0
		public SignedInfo()
		{
			this.m_references = new ArrayList();
		}

		// Token: 0x0600036B RID: 875 RVA: 0x000120D3 File Offset: 0x000110D3
		public IEnumerator GetEnumerator()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600036C RID: 876 RVA: 0x000120DA File Offset: 0x000110DA
		public void CopyTo(Array array, int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600036D RID: 877 RVA: 0x000120E1 File Offset: 0x000110E1
		public int Count
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600036E RID: 878 RVA: 0x000120E8 File Offset: 0x000110E8
		public bool IsReadOnly
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600036F RID: 879 RVA: 0x000120EF File Offset: 0x000110EF
		public bool IsSynchronized
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000370 RID: 880 RVA: 0x000120F6 File Offset: 0x000110F6
		public object SyncRoot
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000371 RID: 881 RVA: 0x000120FD File Offset: 0x000110FD
		// (set) Token: 0x06000372 RID: 882 RVA: 0x00012105 File Offset: 0x00011105
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

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00012115 File Offset: 0x00011115
		// (set) Token: 0x06000374 RID: 884 RVA: 0x0001212B File Offset: 0x0001112B
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

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0001213C File Offset: 0x0001113C
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

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000376 RID: 886 RVA: 0x000121B8 File Offset: 0x000111B8
		// (set) Token: 0x06000377 RID: 887 RVA: 0x000121C0 File Offset: 0x000111C0
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

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000378 RID: 888 RVA: 0x000121D0 File Offset: 0x000111D0
		// (set) Token: 0x06000379 RID: 889 RVA: 0x000121D8 File Offset: 0x000111D8
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

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600037A RID: 890 RVA: 0x000121E8 File Offset: 0x000111E8
		public ArrayList References
		{
			get
			{
				return this.m_references;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600037B RID: 891 RVA: 0x000121F0 File Offset: 0x000111F0
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

		// Token: 0x0600037C RID: 892 RVA: 0x0001225C File Offset: 0x0001125C
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

		// Token: 0x0600037D RID: 893 RVA: 0x0001228C File Offset: 0x0001128C
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

		// Token: 0x0600037E RID: 894 RVA: 0x000123C0 File Offset: 0x000113C0
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
			if ((!Utils.GetSkipSignatureAttributeEnforcement() && this.m_canonicalizationMethod == null) || !Utils.VerifyAttributes(xmlElement, "Algorithm"))
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
			if ((!Utils.GetSkipSignatureAttributeEnforcement() && this.m_signatureMethod == null) || !Utils.VerifyAttributes(xmlElement2, "Algorithm"))
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

		// Token: 0x0600037F RID: 895 RVA: 0x000126DC File Offset: 0x000116DC
		public void AddReference(Reference reference)
		{
			if (reference == null)
			{
				throw new ArgumentNullException("reference");
			}
			reference.SignedXml = this.SignedXml;
			this.m_references.Add(reference);
		}

		// Token: 0x04000527 RID: 1319
		private string m_id;

		// Token: 0x04000528 RID: 1320
		private string m_canonicalizationMethod;

		// Token: 0x04000529 RID: 1321
		private string m_signatureMethod;

		// Token: 0x0400052A RID: 1322
		private string m_signatureLength;

		// Token: 0x0400052B RID: 1323
		private ArrayList m_references;

		// Token: 0x0400052C RID: 1324
		private XmlElement m_cachedXml;

		// Token: 0x0400052D RID: 1325
		private SignedXml m_signedXml;

		// Token: 0x0400052E RID: 1326
		private Transform m_canonicalizationMethodTransform;
	}
}
