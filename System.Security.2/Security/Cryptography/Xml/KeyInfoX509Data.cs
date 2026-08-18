using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200004C RID: 76
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class KeyInfoX509Data : KeyInfoClause
	{
		// Token: 0x06000260 RID: 608 RVA: 0x0000A5FF File Offset: 0x000087FF
		public KeyInfoX509Data()
		{
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000A828 File Offset: 0x00008A28
		public KeyInfoX509Data(byte[] rgbCert)
		{
			X509Certificate2 certificate = new X509Certificate2(rgbCert);
			this.AddCertificate(certificate);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000A849 File Offset: 0x00008A49
		public KeyInfoX509Data(X509Certificate cert)
		{
			this.AddCertificate(cert);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000A858 File Offset: 0x00008A58
		[SecuritySafeCritical]
		public KeyInfoX509Data(X509Certificate cert, X509IncludeOption includeOption)
		{
			if (cert == null)
			{
				throw new ArgumentNullException("cert");
			}
			X509Certificate2 certificate = new X509Certificate2(cert);
			switch (includeOption)
			{
			case X509IncludeOption.ExcludeRoot:
			{
				X509Chain x509Chain = new X509Chain();
				x509Chain.Build(certificate);
				if (x509Chain.ChainStatus.Length != 0 && (x509Chain.ChainStatus[0].Status & X509ChainStatusFlags.PartialChain) == X509ChainStatusFlags.PartialChain)
				{
					throw new CryptographicException(-2146762486);
				}
				X509ChainElementCollection chainElements = x509Chain.ChainElements;
				for (int i = 0; i < (X509Utils.IsSelfSigned(x509Chain) ? 1 : (chainElements.Count - 1)); i++)
				{
					this.AddCertificate(chainElements[i].Certificate);
				}
				return;
			}
			case X509IncludeOption.EndCertOnly:
				this.AddCertificate(certificate);
				return;
			case X509IncludeOption.WholeChain:
			{
				X509Chain x509Chain = new X509Chain();
				x509Chain.Build(certificate);
				if (x509Chain.ChainStatus.Length != 0 && (x509Chain.ChainStatus[0].Status & X509ChainStatusFlags.PartialChain) == X509ChainStatusFlags.PartialChain)
				{
					throw new CryptographicException(-2146762486);
				}
				X509ChainElementCollection chainElements = x509Chain.ChainElements;
				foreach (X509ChainElement x509ChainElement in chainElements)
				{
					this.AddCertificate(x509ChainElement.Certificate);
				}
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000A989 File Offset: 0x00008B89
		public ArrayList Certificates
		{
			get
			{
				return this.m_certificates;
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000A994 File Offset: 0x00008B94
		public void AddCertificate(X509Certificate certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (this.m_certificates == null)
			{
				this.m_certificates = new ArrayList();
			}
			X509Certificate2 value = new X509Certificate2(certificate);
			this.m_certificates.Add(value);
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000A9D6 File Offset: 0x00008BD6
		public ArrayList SubjectKeyIds
		{
			get
			{
				return this.m_subjectKeyIds;
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000A9DE File Offset: 0x00008BDE
		public void AddSubjectKeyId(byte[] subjectKeyId)
		{
			if (this.m_subjectKeyIds == null)
			{
				this.m_subjectKeyIds = new ArrayList();
			}
			this.m_subjectKeyIds.Add(subjectKeyId);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000AA00 File Offset: 0x00008C00
		[ComVisible(false)]
		public void AddSubjectKeyId(string subjectKeyId)
		{
			if (this.m_subjectKeyIds == null)
			{
				this.m_subjectKeyIds = new ArrayList();
			}
			this.m_subjectKeyIds.Add(X509Utils.DecodeHexString(subjectKeyId));
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000AA27 File Offset: 0x00008C27
		public ArrayList SubjectNames
		{
			get
			{
				return this.m_subjectNames;
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000AA2F File Offset: 0x00008C2F
		public void AddSubjectName(string subjectName)
		{
			if (this.m_subjectNames == null)
			{
				this.m_subjectNames = new ArrayList();
			}
			this.m_subjectNames.Add(subjectName);
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000AA51 File Offset: 0x00008C51
		public ArrayList IssuerSerials
		{
			get
			{
				return this.m_issuerSerials;
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000AA5C File Offset: 0x00008C5C
		public void AddIssuerSerial(string issuerName, string serialNumber)
		{
			BigInt bigInt = new BigInt();
			bigInt.FromHexadecimal(serialNumber);
			if (this.m_issuerSerials == null)
			{
				this.m_issuerSerials = new ArrayList();
			}
			this.m_issuerSerials.Add(new X509IssuerSerial(issuerName, bigInt.ToDecimal()));
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000AAA6 File Offset: 0x00008CA6
		internal void InternalAddIssuerSerial(string issuerName, string serialNumber)
		{
			if (this.m_issuerSerials == null)
			{
				this.m_issuerSerials = new ArrayList();
			}
			this.m_issuerSerials.Add(new X509IssuerSerial(issuerName, serialNumber));
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000AAD3 File Offset: 0x00008CD3
		// (set) Token: 0x0600026F RID: 623 RVA: 0x0000AADB File Offset: 0x00008CDB
		public byte[] CRL
		{
			get
			{
				return this.m_CRL;
			}
			set
			{
				this.m_CRL = value;
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000AAE4 File Offset: 0x00008CE4
		private void Clear()
		{
			this.m_CRL = null;
			if (this.m_subjectKeyIds != null)
			{
				this.m_subjectKeyIds.Clear();
			}
			if (this.m_subjectNames != null)
			{
				this.m_subjectNames.Clear();
			}
			if (this.m_issuerSerials != null)
			{
				this.m_issuerSerials.Clear();
			}
			if (this.m_certificates != null)
			{
				this.m_certificates.Clear();
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000AB44 File Offset: 0x00008D44
		public override XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000AB68 File Offset: 0x00008D68
		internal override XmlElement GetXml(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = xmlDocument.CreateElement("X509Data", "http://www.w3.org/2000/09/xmldsig#");
			if (this.m_issuerSerials != null)
			{
				foreach (object obj in this.m_issuerSerials)
				{
					X509IssuerSerial x509IssuerSerial = (X509IssuerSerial)obj;
					XmlElement xmlElement2 = xmlDocument.CreateElement("X509IssuerSerial", "http://www.w3.org/2000/09/xmldsig#");
					XmlElement xmlElement3 = xmlDocument.CreateElement("X509IssuerName", "http://www.w3.org/2000/09/xmldsig#");
					xmlElement3.AppendChild(xmlDocument.CreateTextNode(x509IssuerSerial.IssuerName));
					xmlElement2.AppendChild(xmlElement3);
					XmlElement xmlElement4 = xmlDocument.CreateElement("X509SerialNumber", "http://www.w3.org/2000/09/xmldsig#");
					xmlElement4.AppendChild(xmlDocument.CreateTextNode(x509IssuerSerial.SerialNumber));
					xmlElement2.AppendChild(xmlElement4);
					xmlElement.AppendChild(xmlElement2);
				}
			}
			if (this.m_subjectKeyIds != null)
			{
				foreach (object obj2 in this.m_subjectKeyIds)
				{
					byte[] inArray = (byte[])obj2;
					XmlElement xmlElement5 = xmlDocument.CreateElement("X509SKI", "http://www.w3.org/2000/09/xmldsig#");
					xmlElement5.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(inArray)));
					xmlElement.AppendChild(xmlElement5);
				}
			}
			if (this.m_subjectNames != null)
			{
				foreach (object obj3 in this.m_subjectNames)
				{
					string text = (string)obj3;
					XmlElement xmlElement6 = xmlDocument.CreateElement("X509SubjectName", "http://www.w3.org/2000/09/xmldsig#");
					xmlElement6.AppendChild(xmlDocument.CreateTextNode(text));
					xmlElement.AppendChild(xmlElement6);
				}
			}
			if (this.m_certificates != null)
			{
				foreach (object obj4 in this.m_certificates)
				{
					X509Certificate x509Certificate = (X509Certificate)obj4;
					XmlElement xmlElement7 = xmlDocument.CreateElement("X509Certificate", "http://www.w3.org/2000/09/xmldsig#");
					xmlElement7.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(x509Certificate.GetRawCertData())));
					xmlElement.AppendChild(xmlElement7);
				}
			}
			if (this.m_CRL != null)
			{
				XmlElement xmlElement8 = xmlDocument.CreateElement("X509CRL", "http://www.w3.org/2000/09/xmldsig#");
				xmlElement8.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(this.m_CRL)));
				xmlElement.AppendChild(xmlElement8);
			}
			return xmlElement;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000AE18 File Offset: 0x00009018
		public override void LoadXml(XmlElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(element.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			XmlNodeList xmlNodeList = element.SelectNodes("ds:X509IssuerSerial", xmlNamespaceManager);
			XmlNodeList xmlNodeList2 = element.SelectNodes("ds:X509SKI", xmlNamespaceManager);
			XmlNodeList xmlNodeList3 = element.SelectNodes("ds:X509SubjectName", xmlNamespaceManager);
			XmlNodeList xmlNodeList4 = element.SelectNodes("ds:X509Certificate", xmlNamespaceManager);
			XmlNodeList xmlNodeList5 = element.SelectNodes("ds:X509CRL", xmlNamespaceManager);
			if (xmlNodeList5.Count == 0 && xmlNodeList.Count == 0 && xmlNodeList2.Count == 0 && xmlNodeList3.Count == 0 && xmlNodeList4.Count == 0)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "X509Data");
			}
			this.Clear();
			if (xmlNodeList5.Count != 0)
			{
				this.m_CRL = Convert.FromBase64String(Utils.DiscardWhiteSpaces(xmlNodeList5.Item(0).InnerText));
			}
			foreach (object obj in xmlNodeList)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNode xmlNode2 = xmlNode.SelectSingleNode("ds:X509IssuerName", xmlNamespaceManager);
				XmlNode xmlNode3 = xmlNode.SelectSingleNode("ds:X509SerialNumber", xmlNamespaceManager);
				if (xmlNode2 == null || xmlNode3 == null)
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "IssuerSerial");
				}
				this.InternalAddIssuerSerial(xmlNode2.InnerText.Trim(), xmlNode3.InnerText.Trim());
			}
			foreach (object obj2 in xmlNodeList2)
			{
				XmlNode xmlNode4 = (XmlNode)obj2;
				this.AddSubjectKeyId(Convert.FromBase64String(Utils.DiscardWhiteSpaces(xmlNode4.InnerText)));
			}
			foreach (object obj3 in xmlNodeList3)
			{
				XmlNode xmlNode5 = (XmlNode)obj3;
				this.AddSubjectName(xmlNode5.InnerText.Trim());
			}
			foreach (object obj4 in xmlNodeList4)
			{
				XmlNode xmlNode6 = (XmlNode)obj4;
				this.AddCertificate(new X509Certificate2(Convert.FromBase64String(Utils.DiscardWhiteSpaces(xmlNode6.InnerText))));
			}
		}

		// Token: 0x040003F3 RID: 1011
		private ArrayList m_certificates;

		// Token: 0x040003F4 RID: 1012
		private ArrayList m_issuerSerials;

		// Token: 0x040003F5 RID: 1013
		private ArrayList m_subjectKeyIds;

		// Token: 0x040003F6 RID: 1014
		private ArrayList m_subjectNames;

		// Token: 0x040003F7 RID: 1015
		private byte[] m_CRL;
	}
}
