using System;
using System.IO;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200005E RID: 94
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class XmlDsigEnvelopedSignatureTransform : Transform
	{
		// Token: 0x170000BD RID: 189
		// (set) Token: 0x0600037C RID: 892 RVA: 0x00010CFD File Offset: 0x0000EEFD
		internal int SignaturePosition
		{
			set
			{
				this._signaturePosition = value;
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00010D08 File Offset: 0x0000EF08
		public XmlDsigEnvelopedSignatureTransform()
		{
			base.Algorithm = "http://www.w3.org/2000/09/xmldsig#enveloped-signature";
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00010D80 File Offset: 0x0000EF80
		public XmlDsigEnvelopedSignatureTransform(bool includeComments)
		{
			this._includeComments = includeComments;
			base.Algorithm = "http://www.w3.org/2000/09/xmldsig#enveloped-signature";
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600037F RID: 895 RVA: 0x00010DFE File Offset: 0x0000EFFE
		public override Type[] InputTypes
		{
			get
			{
				return this._inputTypes;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00010E06 File Offset: 0x0000F006
		public override Type[] OutputTypes
		{
			get
			{
				return this._outputTypes;
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000FC47 File Offset: 0x0000DE47
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
			if (!Utils.GetAllowAdditionalSignatureNodes() && nodeList != null && nodeList.Count > 0)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UnknownTransform"));
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000FC6C File Offset: 0x0000DE6C
		protected override XmlNodeList GetInnerXml()
		{
			return null;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00010E10 File Offset: 0x0000F010
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
				return;
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00010E5C File Offset: 0x0000F05C
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

		// Token: 0x06000385 RID: 901 RVA: 0x00010EF8 File Offset: 0x0000F0F8
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

		// Token: 0x06000386 RID: 902 RVA: 0x00010F6C File Offset: 0x0000F16C
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

		// Token: 0x06000387 RID: 903 RVA: 0x00010FBC File Offset: 0x0000F1BC
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

		// Token: 0x06000388 RID: 904 RVA: 0x00011198 File Offset: 0x0000F398
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
			if (!(type == typeof(XmlDocument)) && !type.IsSubclassOf(typeof(XmlDocument)))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_TransformIncorrectInputType"), "type");
			}
			if (this._inputNodeList != null)
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_TransformIncorrectInputType"), "type");
			}
			return (XmlDocument)this.GetOutput();
		}

		// Token: 0x04000473 RID: 1139
		private Type[] _inputTypes = new Type[]
		{
			typeof(Stream),
			typeof(XmlNodeList),
			typeof(XmlDocument)
		};

		// Token: 0x04000474 RID: 1140
		private Type[] _outputTypes = new Type[]
		{
			typeof(XmlNodeList),
			typeof(XmlDocument)
		};

		// Token: 0x04000475 RID: 1141
		private XmlNodeList _inputNodeList;

		// Token: 0x04000476 RID: 1142
		private bool _includeComments;

		// Token: 0x04000477 RID: 1143
		private XmlNamespaceManager _nsm;

		// Token: 0x04000478 RID: 1144
		private XmlDocument _containingDocument;

		// Token: 0x04000479 RID: 1145
		private int _signaturePosition;
	}
}
