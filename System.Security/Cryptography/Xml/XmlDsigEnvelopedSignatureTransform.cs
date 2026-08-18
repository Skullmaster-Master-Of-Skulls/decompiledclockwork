using System;
using System.IO;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000B4 RID: 180
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class XmlDsigEnvelopedSignatureTransform : Transform
	{
		// Token: 0x170000CE RID: 206
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x000154B8 File Offset: 0x000144B8
		internal int SignaturePosition
		{
			set
			{
				this._signaturePosition = value;
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000154C4 File Offset: 0x000144C4
		public XmlDsigEnvelopedSignatureTransform()
		{
			base.Algorithm = "http://www.w3.org/2000/09/xmldsig#enveloped-signature";
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00015540 File Offset: 0x00014540
		public XmlDsigEnvelopedSignatureTransform(bool includeComments)
		{
			this._includeComments = includeComments;
			base.Algorithm = "http://www.w3.org/2000/09/xmldsig#enveloped-signature";
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x000155C2 File Offset: 0x000145C2
		public override Type[] InputTypes
		{
			get
			{
				return this._inputTypes;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x000155CA File Offset: 0x000145CA
		public override Type[] OutputTypes
		{
			get
			{
				return this._outputTypes;
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x000155D2 File Offset: 0x000145D2
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
			if (!Utils.GetAllowAdditionalSignatureNodes() && nodeList != null && nodeList.Count > 0)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UnknownTransform"));
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x000155F7 File Offset: 0x000145F7
		protected override XmlNodeList GetInnerXml()
		{
			return null;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x000155FA File Offset: 0x000145FA
		public override void LoadInput(object obj)
		{
			if (obj is Stream)
			{
				this.LoadStreamInput((Stream)obj);
				return;
			}
			if (obj is XmlNodeList)
			{
				this.LoadXmlNodeListInput((XmlNodeList)obj);
				return;
			}
			if (obj is XmlDocument)
			{
				this.LoadXmlDocumentInput((XmlDocument)obj);
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0001563C File Offset: 0x0001463C
		private void LoadStreamInput(Stream stream)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			XmlResolver xmlResolver = base.ResolverSet ? this.m_xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), base.BaseURI);
			XmlReader reader = Utils.PreProcessStreamInput(stream, xmlResolver, base.BaseURI);
			xmlDocument.Load(reader);
			this._containingDocument = xmlDocument;
			if (this._containingDocument == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_EnvelopedSignatureRequiresContext"));
			}
			this._nsm = new XmlNamespaceManager(this._containingDocument.NameTable);
			this._nsm.AddNamespace("dsig", "http://www.w3.org/2000/09/xmldsig#");
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000156D8 File Offset: 0x000146D8
		private void LoadXmlNodeListInput(XmlNodeList nodeList)
		{
			if (nodeList == null)
			{
				throw new ArgumentNullException("nodeList");
			}
			this._containingDocument = Utils.GetOwnerDocument(nodeList);
			if (this._containingDocument == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_EnvelopedSignatureRequiresContext"));
			}
			this._nsm = new XmlNamespaceManager(this._containingDocument.NameTable);
			this._nsm.AddNamespace("dsig", "http://www.w3.org/2000/09/xmldsig#");
			this._inputNodeList = nodeList;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0001574C File Offset: 0x0001474C
		private void LoadXmlDocumentInput(XmlDocument doc)
		{
			if (doc == null)
			{
				throw new ArgumentNullException("doc");
			}
			this._containingDocument = doc;
			this._nsm = new XmlNamespaceManager(this._containingDocument.NameTable);
			this._nsm.AddNamespace("dsig", "http://www.w3.org/2000/09/xmldsig#");
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0001579C File Offset: 0x0001479C
		public override object GetOutput()
		{
			if (this._containingDocument == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_EnvelopedSignatureRequiresContext"));
			}
			if (this._inputNodeList != null)
			{
				if (this._signaturePosition == 0)
				{
					return this._inputNodeList;
				}
				XmlNodeList xmlNodeList = this._containingDocument.SelectNodes("//dsig:Signature", this._nsm);
				if (xmlNodeList == null)
				{
					return this._inputNodeList;
				}
				CanonicalXmlNodeList canonicalXmlNodeList = new CanonicalXmlNodeList();
				foreach (object obj in this._inputNodeList)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode != null)
					{
						if (Utils.IsXmlNamespaceNode(xmlNode) || Utils.IsNamespaceNode(xmlNode))
						{
							canonicalXmlNodeList.Add(xmlNode);
						}
						else
						{
							try
							{
								XmlNode xmlNode2 = xmlNode.SelectSingleNode("ancestor-or-self::dsig:Signature[1]", this._nsm);
								int num = 0;
								foreach (object obj2 in xmlNodeList)
								{
									XmlNode xmlNode3 = (XmlNode)obj2;
									num++;
									if (xmlNode3 == xmlNode2)
									{
										break;
									}
								}
								if (xmlNode2 == null || (xmlNode2 != null && num != this._signaturePosition))
								{
									canonicalXmlNodeList.Add(xmlNode);
								}
							}
							catch
							{
							}
						}
					}
				}
				return canonicalXmlNodeList;
			}
			else
			{
				XmlNodeList xmlNodeList2 = this._containingDocument.SelectNodes("//dsig:Signature", this._nsm);
				if (xmlNodeList2 == null)
				{
					return this._containingDocument;
				}
				if (xmlNodeList2.Count < this._signaturePosition || this._signaturePosition <= 0)
				{
					return this._containingDocument;
				}
				xmlNodeList2[this._signaturePosition - 1].ParentNode.RemoveChild(xmlNodeList2[this._signaturePosition - 1]);
				return this._containingDocument;
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00015974 File Offset: 0x00014974
		public override object GetOutput(Type type)
		{
			if (type == typeof(XmlNodeList) || type.IsSubclassOf(typeof(XmlNodeList)))
			{
				if (this._inputNodeList == null)
				{
					this._inputNodeList = Utils.AllDescendantNodes(this._containingDocument, true);
				}
				return (XmlNodeList)this.GetOutput();
			}
			if (type != typeof(XmlDocument) && !type.IsSubclassOf(typeof(XmlDocument)))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_TransformIncorrectInputType"), "type");
			}
			if (this._inputNodeList != null)
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_TransformIncorrectInputType"), "type");
			}
			return (XmlDocument)this.GetOutput();
		}

		// Token: 0x04000580 RID: 1408
		private Type[] _inputTypes = new Type[]
		{
			typeof(Stream),
			typeof(XmlNodeList),
			typeof(XmlDocument)
		};

		// Token: 0x04000581 RID: 1409
		private Type[] _outputTypes = new Type[]
		{
			typeof(XmlNodeList),
			typeof(XmlDocument)
		};

		// Token: 0x04000582 RID: 1410
		private XmlNodeList _inputNodeList;

		// Token: 0x04000583 RID: 1411
		private bool _includeComments;

		// Token: 0x04000584 RID: 1412
		private XmlNamespaceManager _nsm;

		// Token: 0x04000585 RID: 1413
		private XmlDocument _containingDocument;

		// Token: 0x04000586 RID: 1414
		private int _signaturePosition;
	}
}
