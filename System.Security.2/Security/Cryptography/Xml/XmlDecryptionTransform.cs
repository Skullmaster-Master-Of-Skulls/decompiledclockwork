using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000060 RID: 96
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class XmlDecryptionTransform : Transform
	{
		// Token: 0x06000389 RID: 905 RVA: 0x00011250 File Offset: 0x0000F450
		public XmlDecryptionTransform()
		{
			base.Algorithm = "http://www.w3.org/2002/07/decrypt#XML";
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600038A RID: 906 RVA: 0x000112AD File Offset: 0x0000F4AD
		private ArrayList ExceptUris
		{
			get
			{
				if (this.m_arrayListUri == null)
				{
					this.m_arrayListUri = new ArrayList();
				}
				return this.m_arrayListUri;
			}
		}

		// Token: 0x0600038B RID: 907 RVA: 0x000112C8 File Offset: 0x0000F4C8
		protected virtual bool IsTargetElement(XmlElement inputElement, string idValue)
		{
			return inputElement != null && (inputElement.GetAttribute("Id") == idValue || inputElement.GetAttribute("id") == idValue || inputElement.GetAttribute("ID") == idValue);
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00011318 File Offset: 0x0000F518
		// (set) Token: 0x0600038D RID: 909 RVA: 0x0001137D File Offset: 0x0000F57D
		public EncryptedXml EncryptedXml
		{
			get
			{
				if (this.m_exml != null)
				{
					return this.m_exml;
				}
				Reference reference = base.Reference;
				SignedXml signedXml = (reference == null) ? base.SignedXml : reference.SignedXml;
				if (signedXml == null || signedXml.EncryptedXml == null)
				{
					this.m_exml = new EncryptedXml(this.m_containingDocument);
				}
				else
				{
					this.m_exml = signedXml.EncryptedXml;
				}
				return this.m_exml;
			}
			set
			{
				this.m_exml = value;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00011386 File Offset: 0x0000F586
		public override Type[] InputTypes
		{
			get
			{
				return this.m_inputTypes;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0001138E File Offset: 0x0000F58E
		public override Type[] OutputTypes
		{
			get
			{
				return this.m_outputTypes;
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00011396 File Offset: 0x0000F596
		public void AddExceptUri(string uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			this.ExceptUris.Add(uri);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x000113B4 File Offset: 0x0000F5B4
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
			if (nodeList == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UnknownTransform"));
			}
			this.ExceptUris.Clear();
			foreach (object obj in nodeList)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlElement xmlElement = xmlNode as XmlElement;
				if (xmlElement != null)
				{
					if (xmlElement.LocalName == "Except" && xmlElement.NamespaceURI == "http://www.w3.org/2002/07/decrypt#")
					{
						string attribute = Utils.GetAttribute(xmlElement, "URI", "http://www.w3.org/2002/07/decrypt#");
						if (attribute == null || attribute.Length == 0 || attribute[0] != '#')
						{
							throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UriRequired"));
						}
						if (!Utils.VerifyAttributes(xmlElement, "URI"))
						{
							throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UnknownTransform"));
						}
						string value = Utils.ExtractIdFromLocalUri(attribute);
						this.ExceptUris.Add(value);
					}
					else if (!Utils.GetAllowAdditionalSignatureNodes())
					{
						throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UnknownTransform"));
					}
				}
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x000114DC File Offset: 0x0000F6DC
		protected override XmlNodeList GetInnerXml()
		{
			if (this.ExceptUris.Count == 0)
			{
				return null;
			}
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("Transform", "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(base.Algorithm))
			{
				xmlElement.SetAttribute("Algorithm", base.Algorithm);
			}
			foreach (object obj in this.ExceptUris)
			{
				string value = (string)obj;
				XmlElement xmlElement2 = xmlDocument.CreateElement("Except", "http://www.w3.org/2002/07/decrypt#");
				xmlElement2.SetAttribute("URI", value);
				xmlElement.AppendChild(xmlElement2);
			}
			return xmlElement.ChildNodes;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x000115A4 File Offset: 0x0000F7A4
		public override void LoadInput(object obj)
		{
			if (obj is Stream)
			{
				this.LoadStreamInput((Stream)obj);
				return;
			}
			if (obj is XmlDocument)
			{
				this.LoadXmlDocumentInput((XmlDocument)obj);
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x000115D0 File Offset: 0x0000F7D0
		private void LoadStreamInput(Stream stream)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			XmlResolver xmlResolver = base.ResolverSet ? this.m_xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), base.BaseURI);
			XmlReader reader = Utils.PreProcessStreamInput(stream, xmlResolver, base.BaseURI);
			xmlDocument.Load(reader);
			this.m_containingDocument = xmlDocument;
			this.m_nsm = new XmlNamespaceManager(this.m_containingDocument.NameTable);
			this.m_nsm.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
			this.m_encryptedDataList = xmlDocument.SelectNodes("//enc:EncryptedData", this.m_nsm);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0001166C File Offset: 0x0000F86C
		private void LoadXmlDocumentInput(XmlDocument document)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			this.m_containingDocument = document;
			this.m_nsm = new XmlNamespaceManager(document.NameTable);
			this.m_nsm.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
			this.m_encryptedDataList = document.SelectNodes("//enc:EncryptedData", this.m_nsm);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000116CC File Offset: 0x0000F8CC
		private void ReplaceEncryptedData(XmlElement encryptedDataElement, byte[] decrypted)
		{
			XmlNode parentNode = encryptedDataElement.ParentNode;
			if (parentNode.NodeType == XmlNodeType.Document)
			{
				parentNode.InnerXml = this.EncryptedXml.Encoding.GetString(decrypted);
				return;
			}
			this.EncryptedXml.ReplaceData(encryptedDataElement, decrypted);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00011710 File Offset: 0x0000F910
		private bool ProcessEncryptedDataItem(XmlElement encryptedDataElement)
		{
			if (this.ExceptUris.Count > 0)
			{
				for (int i = 0; i < this.ExceptUris.Count; i++)
				{
					if (this.IsTargetElement(encryptedDataElement, (string)this.ExceptUris[i]))
					{
						return false;
					}
				}
			}
			EncryptedData encryptedData = new EncryptedData();
			encryptedData.LoadXml(encryptedDataElement);
			SymmetricAlgorithm decryptionKey = this.EncryptedXml.GetDecryptionKey(encryptedData, null);
			if (decryptionKey == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_MissingDecryptionKey"));
			}
			byte[] decrypted = this.EncryptedXml.DecryptData(encryptedData, decryptionKey);
			this.ReplaceEncryptedData(encryptedDataElement, decrypted);
			return true;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000117A4 File Offset: 0x0000F9A4
		private void ProcessElementRecursively(XmlNodeList encryptedDatas)
		{
			if (encryptedDatas == null || encryptedDatas.Count == 0)
			{
				return;
			}
			int dangerousMaxRecursionDepth = Utils.GetDangerousMaxRecursionDepth();
			int maxDecryptedDataElements = Utils.GetMaxDecryptedDataElements();
			int num = 0;
			Queue<XmlDecryptionTransform.ProcessElementWorkItem> queue = new Queue<XmlDecryptionTransform.ProcessElementWorkItem>();
			using (IEnumerator enumerator = encryptedDatas.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					XmlNode element = (XmlNode)obj;
					queue.Enqueue(new XmlDecryptionTransform.ProcessElementWorkItem(element, 0));
				}
				goto IL_1AB;
			}
			IL_69:
			XmlDecryptionTransform.ProcessElementWorkItem processElementWorkItem = queue.Dequeue();
			XmlElement xmlElement = processElementWorkItem.Element as XmlElement;
			int depth = processElementWorkItem.Depth;
			if (xmlElement != null && xmlElement.LocalName == "EncryptedData" && xmlElement.NamespaceURI == "http://www.w3.org/2001/04/xmlenc#")
			{
				if (maxDecryptedDataElements > 0 && ++num > maxDecryptedDataElements)
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "MAX_ENCRYPTED_DATA_ELEMENTS_EXCEEDED");
				}
				XmlNode nextSibling = xmlElement.NextSibling;
				XmlNode parentNode = xmlElement.ParentNode;
				if (this.ProcessEncryptedDataItem(xmlElement))
				{
					XmlNode xmlNode = parentNode.FirstChild;
					while (xmlNode != null && xmlNode.NextSibling != nextSibling)
					{
						xmlNode = xmlNode.NextSibling;
					}
					if (xmlNode != null)
					{
						XmlNodeList xmlNodeList = xmlNode.SelectNodes("//enc:EncryptedData", this.m_nsm);
						if (xmlNodeList.Count > 0)
						{
							if (dangerousMaxRecursionDepth > 0 && depth > dangerousMaxRecursionDepth)
							{
								throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "MAX_DEPTH_EXCEEDED");
							}
							foreach (object obj2 in xmlNodeList)
							{
								XmlNode element2 = (XmlNode)obj2;
								queue.Enqueue(new XmlDecryptionTransform.ProcessElementWorkItem(element2, depth + 1));
							}
						}
					}
				}
			}
			IL_1AB:
			if (queue.Count <= 0)
			{
				return;
			}
			goto IL_69;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00011984 File Offset: 0x0000FB84
		public override object GetOutput()
		{
			if (this.m_encryptedDataList != null)
			{
				this.ProcessElementRecursively(this.m_encryptedDataList);
			}
			Utils.AddNamespaces(this.m_containingDocument.DocumentElement, base.PropagatedNamespaces);
			return this.m_containingDocument;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x000119B6 File Offset: 0x0000FBB6
		public override object GetOutput(Type type)
		{
			if (type == typeof(XmlDocument))
			{
				return (XmlDocument)this.GetOutput();
			}
			throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_TransformIncorrectInputType"), "type");
		}

		// Token: 0x0400047E RID: 1150
		private Type[] m_inputTypes = new Type[]
		{
			typeof(Stream),
			typeof(XmlDocument)
		};

		// Token: 0x0400047F RID: 1151
		private Type[] m_outputTypes = new Type[]
		{
			typeof(XmlDocument)
		};

		// Token: 0x04000480 RID: 1152
		private XmlNodeList m_encryptedDataList;

		// Token: 0x04000481 RID: 1153
		private ArrayList m_arrayListUri;

		// Token: 0x04000482 RID: 1154
		private EncryptedXml m_exml;

		// Token: 0x04000483 RID: 1155
		private XmlDocument m_containingDocument;

		// Token: 0x04000484 RID: 1156
		private XmlNamespaceManager m_nsm;

		// Token: 0x04000485 RID: 1157
		private const string XmlDecryptionTransformNamespaceUrl = "http://www.w3.org/2002/07/decrypt#";

		// Token: 0x020000DD RID: 221
		private struct ProcessElementWorkItem
		{
			// Token: 0x0600059E RID: 1438 RVA: 0x0001C0AC File Offset: 0x0001A2AC
			internal ProcessElementWorkItem(XmlNode element, int depth)
			{
				this.Element = element;
				this.Depth = depth;
			}

			// Token: 0x04000680 RID: 1664
			internal readonly XmlNode Element;

			// Token: 0x04000681 RID: 1665
			internal readonly int Depth;
		}
	}
}
