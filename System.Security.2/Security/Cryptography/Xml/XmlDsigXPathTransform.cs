using System;
using System.Collections;
using System.IO;
using System.Security.Permissions;
using System.Xml;
using System.Xml.XPath;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200005C RID: 92
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class XmlDsigXPathTransform : Transform
	{
		// Token: 0x06000368 RID: 872 RVA: 0x00010408 File Offset: 0x0000E608
		public XmlDsigXPathTransform()
		{
			base.Algorithm = "http://www.w3.org/TR/1999/REC-xpath-19991116";
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00010472 File Offset: 0x0000E672
		public override Type[] InputTypes
		{
			get
			{
				return this._inputTypes;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0001047A File Offset: 0x0000E67A
		public override Type[] OutputTypes
		{
			get
			{
				return this._outputTypes;
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00010484 File Offset: 0x0000E684
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
			if (nodeList == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UnknownTransform"));
			}
			foreach (object obj in nodeList)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlElement xmlElement = xmlNode as XmlElement;
				if (xmlElement != null)
				{
					if (xmlElement.LocalName == "XPath")
					{
						this._xpathexpr = xmlElement.InnerXml.Trim(null);
						XmlNodeReader xmlNodeReader = new XmlNodeReader(xmlElement);
						XmlNameTable nameTable = xmlNodeReader.NameTable;
						this._nsm = new XmlNamespaceManager(nameTable);
						if (!Utils.VerifyAttributes(xmlElement, null))
						{
							throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UnknownTransform"));
						}
						using (IEnumerator enumerator2 = xmlElement.Attributes.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								object obj2 = enumerator2.Current;
								XmlAttribute xmlAttribute = (XmlAttribute)obj2;
								if (xmlAttribute.Prefix == "xmlns")
								{
									string text = xmlAttribute.LocalName;
									string uri = xmlAttribute.Value;
									if (text == null)
									{
										text = xmlElement.Prefix;
										uri = xmlElement.NamespaceURI;
									}
									this._nsm.AddNamespace(text, uri);
								}
							}
							break;
						}
					}
					if (!Utils.GetAllowAdditionalSignatureNodes())
					{
						throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UnknownTransform"));
					}
				}
			}
			if (this._xpathexpr == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UnknownTransform"));
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00010638 File Offset: 0x0000E838
		protected override XmlNodeList GetInnerXml()
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement(null, "XPath", "http://www.w3.org/2000/09/xmldsig#");
			if (this._nsm != null)
			{
				foreach (object obj in this._nsm)
				{
					string text = (string)obj;
					if (!(text == "xml") && !(text == "xmlns") && text != null && text.Length > 0)
					{
						xmlElement.SetAttribute("xmlns:" + text, this._nsm.LookupNamespace(text));
					}
				}
			}
			xmlElement.InnerXml = this._xpathexpr;
			xmlDocument.AppendChild(xmlElement);
			return xmlDocument.ChildNodes;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0001070C File Offset: 0x0000E90C
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

		// Token: 0x0600036E RID: 878 RVA: 0x0001074C File Offset: 0x0000E94C
		private void LoadStreamInput(Stream stream)
		{
			XmlResolver xmlResolver = base.ResolverSet ? this.m_xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), base.BaseURI);
			XmlReader reader = Utils.PreProcessStreamInput(stream, xmlResolver, base.BaseURI);
			this._document = new XmlDocument();
			this._document.PreserveWhitespace = true;
			this._document.Load(reader);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x000107AC File Offset: 0x0000E9AC
		private void LoadXmlNodeListInput(XmlNodeList nodeList)
		{
			XmlResolver resolver = base.ResolverSet ? this.m_xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), base.BaseURI);
			CanonicalXml canonicalXml = new CanonicalXml(nodeList, resolver, true);
			using (MemoryStream memoryStream = new MemoryStream(canonicalXml.GetBytes()))
			{
				this.LoadStreamInput(memoryStream);
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00010814 File Offset: 0x0000EA14
		private void LoadXmlDocumentInput(XmlDocument doc)
		{
			this._document = doc;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00010820 File Offset: 0x0000EA20
		public override object GetOutput()
		{
			CanonicalXmlNodeList canonicalXmlNodeList = new CanonicalXmlNodeList();
			if (!string.IsNullOrEmpty(this._xpathexpr))
			{
				XPathNavigator xpathNavigator = this._document.CreateNavigator();
				XPathNodeIterator xpathNodeIterator = xpathNavigator.Select("//. | //@*");
				XPathExpression xpathExpression = xpathNavigator.Compile("boolean(" + this._xpathexpr + ")");
				xpathExpression.SetContext(this._nsm);
				while (xpathNodeIterator.MoveNext())
				{
					XPathNavigator xpathNavigator2 = xpathNodeIterator.Current;
					XmlNode node = ((IHasXmlNode)xpathNavigator2).GetNode();
					bool flag = (bool)xpathNodeIterator.Current.Evaluate(xpathExpression);
					if (flag)
					{
						canonicalXmlNodeList.Add(node);
					}
				}
				xpathNodeIterator = xpathNavigator.Select("//namespace::*");
				while (xpathNodeIterator.MoveNext())
				{
					XPathNavigator xpathNavigator3 = xpathNodeIterator.Current;
					XmlNode node2 = ((IHasXmlNode)xpathNavigator3).GetNode();
					canonicalXmlNodeList.Add(node2);
				}
			}
			return canonicalXmlNodeList;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x000108F4 File Offset: 0x0000EAF4
		public override object GetOutput(Type type)
		{
			if (type != typeof(XmlNodeList) && !type.IsSubclassOf(typeof(XmlNodeList)))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_TransformIncorrectInputType"), "type");
			}
			return (XmlNodeList)this.GetOutput();
		}

		// Token: 0x04000468 RID: 1128
		private Type[] _inputTypes = new Type[]
		{
			typeof(Stream),
			typeof(XmlNodeList),
			typeof(XmlDocument)
		};

		// Token: 0x04000469 RID: 1129
		private Type[] _outputTypes = new Type[]
		{
			typeof(XmlNodeList)
		};

		// Token: 0x0400046A RID: 1130
		private string _xpathexpr;

		// Token: 0x0400046B RID: 1131
		private XmlDocument _document;

		// Token: 0x0400046C RID: 1132
		private XmlNamespaceManager _nsm;
	}
}
