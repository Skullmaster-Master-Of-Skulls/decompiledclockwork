using System;
using System.Collections;
using System.IO;
using System.Security.Permissions;
using System.Xml;
using System.Xml.XPath;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000B2 RID: 178
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class XmlDsigXPathTransform : Transform
	{
		// Token: 0x06000405 RID: 1029 RVA: 0x00014BC8 File Offset: 0x00013BC8
		public XmlDsigXPathTransform()
		{
			base.Algorithm = "http://www.w3.org/TR/1999/REC-xpath-19991116";
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x00014C36 File Offset: 0x00013C36
		public override Type[] InputTypes
		{
			get
			{
				return this._inputTypes;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x00014C3E File Offset: 0x00013C3E
		public override Type[] OutputTypes
		{
			get
			{
				return this._outputTypes;
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00014C48 File Offset: 0x00013C48
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

		// Token: 0x06000409 RID: 1033 RVA: 0x00014DF8 File Offset: 0x00013DF8
		protected override XmlNodeList GetInnerXml()
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement(null, "XPath", "http://www.w3.org/2000/09/xmldsig#");
			if (this._nsm != null)
			{
				foreach (object obj in this._nsm)
				{
					string text = (string)obj;
					string a;
					if (((a = text) == null || (!(a == "xml") && !(a == "xmlns"))) && text != null && text.Length > 0)
					{
						xmlElement.SetAttribute("xmlns:" + text, this._nsm.LookupNamespace(text));
					}
				}
			}
			xmlElement.InnerXml = this._xpathexpr;
			xmlDocument.AppendChild(xmlElement);
			return xmlDocument.ChildNodes;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00014ED8 File Offset: 0x00013ED8
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

		// Token: 0x0600040B RID: 1035 RVA: 0x00014F18 File Offset: 0x00013F18
		private void LoadStreamInput(Stream stream)
		{
			XmlResolver xmlResolver = base.ResolverSet ? this.m_xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), base.BaseURI);
			XmlReader reader = Utils.PreProcessStreamInput(stream, xmlResolver, base.BaseURI);
			this._document = new XmlDocument();
			this._document.PreserveWhitespace = true;
			this._document.Load(reader);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00014F78 File Offset: 0x00013F78
		private void LoadXmlNodeListInput(XmlNodeList nodeList)
		{
			XmlResolver resolver = base.ResolverSet ? this.m_xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), base.BaseURI);
			CanonicalXml canonicalXml = new CanonicalXml(nodeList, resolver, true);
			using (MemoryStream memoryStream = new MemoryStream(canonicalXml.GetBytes()))
			{
				this.LoadStreamInput(memoryStream);
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00014FE0 File Offset: 0x00013FE0
		private void LoadXmlDocumentInput(XmlDocument doc)
		{
			this._document = doc;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00014FEC File Offset: 0x00013FEC
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

		// Token: 0x0600040F RID: 1039 RVA: 0x000150C0 File Offset: 0x000140C0
		public override object GetOutput(Type type)
		{
			if (type != typeof(XmlNodeList) && !type.IsSubclassOf(typeof(XmlNodeList)))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_TransformIncorrectInputType"), "type");
			}
			return (XmlNodeList)this.GetOutput();
		}

		// Token: 0x04000575 RID: 1397
		private Type[] _inputTypes = new Type[]
		{
			typeof(Stream),
			typeof(XmlNodeList),
			typeof(XmlDocument)
		};

		// Token: 0x04000576 RID: 1398
		private Type[] _outputTypes = new Type[]
		{
			typeof(XmlNodeList)
		};

		// Token: 0x04000577 RID: 1399
		private string _xpathexpr;

		// Token: 0x04000578 RID: 1400
		private XmlDocument _document;

		// Token: 0x04000579 RID: 1401
		private XmlNamespaceManager _nsm;
	}
}
