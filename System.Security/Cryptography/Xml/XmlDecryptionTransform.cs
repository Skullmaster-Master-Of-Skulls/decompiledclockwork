using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000B6 RID: 182
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class XmlDecryptionTransform : Transform
	{
		// Token: 0x06000426 RID: 1062 RVA: 0x00015A24 File Offset: 0x00014A24
		public XmlDecryptionTransform()
		{
			base.Algorithm = "http://www.w3.org/2002/07/decrypt#XML";
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x00015A85 File Offset: 0x00014A85
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

		// Token: 0x06000428 RID: 1064 RVA: 0x00015AA0 File Offset: 0x00014AA0
		protected virtual bool IsTargetElement(XmlElement inputElement, string idValue)
		{
			return inputElement != null && (inputElement.GetAttribute("Id") == idValue || inputElement.GetAttribute("id") == idValue || inputElement.GetAttribute("ID") == idValue);
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00015AF0 File Offset: 0x00014AF0
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x00015B55 File Offset: 0x00014B55
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

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00015B5E File Offset: 0x00014B5E
		public override Type[] InputTypes
		{
			get
			{
				return this.m_inputTypes;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x00015B66 File Offset: 0x00014B66
		public override Type[] OutputTypes
		{
			get
			{
				return this.m_outputTypes;
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00015B6E File Offset: 0x00014B6E
		public void AddExceptUri(string uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			this.ExceptUris.Add(uri);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00015B8C File Offset: 0x00014B8C
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

		// Token: 0x0600042F RID: 1071 RVA: 0x00015CB8 File Offset: 0x00014CB8
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

		// Token: 0x06000430 RID: 1072 RVA: 0x00015D80 File Offset: 0x00014D80
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

		// Token: 0x06000431 RID: 1073 RVA: 0x00015DAC File Offset: 0x00014DAC
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

		// Token: 0x06000432 RID: 1074 RVA: 0x00015E48 File Offset: 0x00014E48
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

		// Token: 0x06000433 RID: 1075 RVA: 0x00015EA8 File Offset: 0x00014EA8
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
			byte[] decryptedData = this.EncryptedXml.DecryptData(encryptedData, decryptionKey);
			this.EncryptedXml.ReplaceData(encryptedDataElement, decryptedData);
			return true;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00015F40 File Offset: 0x00014F40
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

		// Token: 0x06000435 RID: 1077 RVA: 0x00016120 File Offset: 0x00015120
		public override object GetOutput()
		{
			if (this.m_encryptedDataList != null)
			{
				this.ProcessElementRecursively(this.m_encryptedDataList);
			}
			Utils.AddNamespaces(this.m_containingDocument.DocumentElement, base.PropagatedNamespaces);
			return this.m_containingDocument;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00016152 File Offset: 0x00015152
		public override object GetOutput(Type type)
		{
			if (type == typeof(XmlDocument))
			{
				return (XmlDocument)this.GetOutput();
			}
			throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_TransformIncorrectInputType"), "type");
		}

		// Token: 0x0400058B RID: 1419
		private const string XmlDecryptionTransformNamespaceUrl = "http://www.w3.org/2002/07/decrypt#";

		// Token: 0x0400058C RID: 1420
		private Type[] m_inputTypes = new Type[]
		{
			typeof(Stream),
			typeof(XmlDocument)
		};

		// Token: 0x0400058D RID: 1421
		private Type[] m_outputTypes = new Type[]
		{
			typeof(XmlDocument)
		};

		// Token: 0x0400058E RID: 1422
		private XmlNodeList m_encryptedDataList;

		// Token: 0x0400058F RID: 1423
		private ArrayList m_arrayListUri;

		// Token: 0x04000590 RID: 1424
		private EncryptedXml m_exml;

		// Token: 0x04000591 RID: 1425
		private XmlDocument m_containingDocument;

		// Token: 0x04000592 RID: 1426
		private XmlNamespaceManager m_nsm;

		// Token: 0x020000B7 RID: 183
		private struct ProcessElementWorkItem
		{
			// Token: 0x06000437 RID: 1079 RVA: 0x00016181 File Offset: 0x00015181
			internal ProcessElementWorkItem(XmlNode element, int depth)
			{
				this.Element = element;
				this.Depth = depth;
			}

			// Token: 0x04000593 RID: 1427
			internal readonly XmlNode Element;

			// Token: 0x04000594 RID: 1428
			internal readonly int Depth;
		}
	}
}
