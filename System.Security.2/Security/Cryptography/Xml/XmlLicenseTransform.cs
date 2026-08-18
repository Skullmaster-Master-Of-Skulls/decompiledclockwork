using System;
using System.IO;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000061 RID: 97
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class XmlLicenseTransform : Transform
	{
		// Token: 0x0600039B RID: 923 RVA: 0x000119EC File Offset: 0x0000FBEC
		public XmlLicenseTransform()
		{
			base.Algorithm = "urn:mpeg:mpeg21:2003:01-REL-R-NS:licenseTransform";
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00011A3C File Offset: 0x0000FC3C
		public override Type[] InputTypes
		{
			get
			{
				return this.inputTypes;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600039D RID: 925 RVA: 0x00011A44 File Offset: 0x0000FC44
		public override Type[] OutputTypes
		{
			get
			{
				return this.outputTypes;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00011A4C File Offset: 0x0000FC4C
		// (set) Token: 0x0600039F RID: 927 RVA: 0x00011A54 File Offset: 0x0000FC54
		public IRelDecryptor Decryptor
		{
			get
			{
				return this.relDecryptor;
			}
			set
			{
				this.relDecryptor = value;
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00011A60 File Offset: 0x0000FC60
		private void DecryptEncryptedGrants(XmlNodeList encryptedGrantList, IRelDecryptor decryptor)
		{
			int i = 0;
			int count = encryptedGrantList.Count;
			while (i < count)
			{
				XmlElement xmlElement = encryptedGrantList[i].SelectSingleNode("//r:encryptedGrant/enc:EncryptionMethod", this.namespaceManager) as XmlElement;
				XmlElement xmlElement2 = encryptedGrantList[i].SelectSingleNode("//r:encryptedGrant/dsig:KeyInfo", this.namespaceManager) as XmlElement;
				XmlElement xmlElement3 = encryptedGrantList[i].SelectSingleNode("//r:encryptedGrant/enc:CipherData", this.namespaceManager) as XmlElement;
				if (xmlElement != null && xmlElement2 != null && xmlElement3 != null)
				{
					EncryptionMethod encryptionMethod = new EncryptionMethod();
					KeyInfo keyInfo = new KeyInfo();
					CipherData cipherData = new CipherData();
					encryptionMethod.LoadXml(xmlElement);
					keyInfo.LoadXml(xmlElement2);
					cipherData.LoadXml(xmlElement3);
					MemoryStream memoryStream = null;
					Stream stream = null;
					StreamReader streamReader = null;
					try
					{
						memoryStream = new MemoryStream(cipherData.CipherValue);
						stream = this.relDecryptor.Decrypt(encryptionMethod, keyInfo, memoryStream);
						if (stream == null || stream.Length == 0L)
						{
							throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_XrmlUnableToDecryptGrant"));
						}
						streamReader = new StreamReader(stream);
						string innerXml = streamReader.ReadToEnd();
						encryptedGrantList[i].ParentNode.InnerXml = innerXml;
					}
					finally
					{
						if (memoryStream != null)
						{
							memoryStream.Close();
						}
						if (stream != null)
						{
							stream.Close();
						}
						if (streamReader != null)
						{
							streamReader.Close();
						}
					}
				}
				i++;
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000FC6C File Offset: 0x0000DE6C
		protected override XmlNodeList GetInnerXml()
		{
			return null;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00011BE0 File Offset: 0x0000FDE0
		public override object GetOutput()
		{
			return this.license;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00011BE8 File Offset: 0x0000FDE8
		public override object GetOutput(Type type)
		{
			if (type != typeof(XmlDocument) || !type.IsSubclassOf(typeof(XmlDocument)))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_TransformIncorrectInputType"), "type");
			}
			return this.GetOutput();
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000FC47 File Offset: 0x0000DE47
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
			if (!Utils.GetAllowAdditionalSignatureNodes() && nodeList != null && nodeList.Count > 0)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UnknownTransform"));
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00011C34 File Offset: 0x0000FE34
		public override void LoadInput(object obj)
		{
			if (base.Context == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_XrmlMissingContext"));
			}
			this.license = new XmlDocument();
			this.license.PreserveWhitespace = true;
			this.namespaceManager = new XmlNamespaceManager(this.license.NameTable);
			this.namespaceManager.AddNamespace("dsig", "http://www.w3.org/2000/09/xmldsig#");
			this.namespaceManager.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
			this.namespaceManager.AddNamespace("r", "urn:mpeg:mpeg21:2003:01-REL-R-NS");
			XmlElement xmlElement = base.Context.SelectSingleNode("ancestor-or-self::r:issuer[1]", this.namespaceManager) as XmlElement;
			if (xmlElement == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_XrmlMissingIssuer"));
			}
			XmlNode xmlNode = xmlElement.SelectSingleNode("descendant-or-self::dsig:Signature[1]", this.namespaceManager) as XmlElement;
			if (xmlNode != null)
			{
				xmlNode.ParentNode.RemoveChild(xmlNode);
			}
			XmlElement xmlElement2 = xmlElement.SelectSingleNode("ancestor-or-self::r:license[1]", this.namespaceManager) as XmlElement;
			if (xmlElement2 == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_XrmlMissingLicence"));
			}
			XmlNodeList xmlNodeList = xmlElement2.SelectNodes("descendant-or-self::r:license[1]/r:issuer", this.namespaceManager);
			int i = 0;
			int count = xmlNodeList.Count;
			while (i < count)
			{
				if (xmlNodeList[i] != xmlElement && xmlNodeList[i].LocalName == "issuer" && xmlNodeList[i].NamespaceURI == "urn:mpeg:mpeg21:2003:01-REL-R-NS")
				{
					xmlNodeList[i].ParentNode.RemoveChild(xmlNodeList[i]);
				}
				i++;
			}
			XmlNodeList xmlNodeList2 = xmlElement2.SelectNodes("/r:license/r:grant/r:encryptedGrant", this.namespaceManager);
			if (xmlNodeList2.Count > 0)
			{
				if (this.relDecryptor == null)
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_XrmlMissingIRelDecryptor"));
				}
				this.DecryptEncryptedGrants(xmlNodeList2, this.relDecryptor);
			}
			this.license.InnerXml = xmlElement2.OuterXml;
		}

		// Token: 0x04000486 RID: 1158
		private Type[] inputTypes = new Type[]
		{
			typeof(XmlDocument)
		};

		// Token: 0x04000487 RID: 1159
		private Type[] outputTypes = new Type[]
		{
			typeof(XmlDocument)
		};

		// Token: 0x04000488 RID: 1160
		private XmlNamespaceManager namespaceManager;

		// Token: 0x04000489 RID: 1161
		private XmlDocument license;

		// Token: 0x0400048A RID: 1162
		private IRelDecryptor relDecryptor;

		// Token: 0x0400048B RID: 1163
		private const string ElementIssuer = "issuer";

		// Token: 0x0400048C RID: 1164
		private const string NamespaceUriCore = "urn:mpeg:mpeg21:2003:01-REL-R-NS";
	}
}
