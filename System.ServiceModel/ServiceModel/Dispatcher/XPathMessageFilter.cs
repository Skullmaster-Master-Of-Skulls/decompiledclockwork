using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.ServiceModel.Channels;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200051D RID: 1309
	[XmlSchemaProvider("StaticGetSchema")]
	[XmlRoot(ElementName = "XPathMessageFilter", Namespace = "http://schemas.microsoft.com/serviceModel/2004/05/xpathfilter")]
	public class XPathMessageFilter : MessageFilter, IXmlSerializable
	{
		// Token: 0x06003195 RID: 12693 RVA: 0x000BE62C File Offset: 0x000BC82C
		private static XmlSchemaComplexType CreateOuterType()
		{
			XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
			xmlSchemaAttribute.Name = "Dialect";
			xmlSchemaAttribute.SchemaTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
			xmlSchemaAttribute.Use = XmlSchemaUse.Optional;
			XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension = new XmlSchemaSimpleContentExtension();
			xmlSchemaSimpleContentExtension.BaseTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
			xmlSchemaSimpleContentExtension.Attributes.Add(xmlSchemaAttribute);
			XmlSchemaSimpleContent xmlSchemaSimpleContent = new XmlSchemaSimpleContent();
			xmlSchemaSimpleContent.Content = xmlSchemaSimpleContentExtension;
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			xmlSchemaComplexType.ContentModel = xmlSchemaSimpleContent;
			XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
			xmlSchemaElement.Name = "XPath";
			xmlSchemaElement.SchemaType = xmlSchemaComplexType;
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			xmlSchemaSequence.Items.Add(xmlSchemaElement);
			XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
			xmlSchemaAttribute2.Name = "NodeQuota";
			xmlSchemaAttribute2.SchemaTypeName = new XmlQualifiedName("int", "http://www.w3.org/2001/XMLSchema");
			xmlSchemaAttribute2.Use = XmlSchemaUse.Optional;
			XmlSchemaAnyAttribute anyAttribute = new XmlSchemaAnyAttribute();
			return new XmlSchemaComplexType
			{
				Name = "XPathMessageFilter",
				Particle = xmlSchemaSequence,
				Attributes = 
				{
					xmlSchemaAttribute2
				},
				AnyAttribute = anyAttribute
			};
		}

		// Token: 0x06003196 RID: 12694 RVA: 0x000BE748 File Offset: 0x000BC948
		public static XmlSchemaType StaticGetSchema(XmlSchemaSet schemas)
		{
			if (schemas == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("schemas");
			}
			XmlSchemaComplexType xmlSchemaComplexType = XPathMessageFilter.CreateOuterType();
			if (schemas.Contains("http://schemas.microsoft.com/serviceModel/2004/05/xpathfilter/"))
			{
				IEnumerator enumerator = schemas.Schemas("http://schemas.microsoft.com/serviceModel/2004/05/xpathfilter/").GetEnumerator();
				enumerator.MoveNext();
				((XmlSchema)enumerator.Current).Items.Add(xmlSchemaComplexType);
			}
			else
			{
				schemas.Add(new XmlSchema
				{
					Items = 
					{
						xmlSchemaComplexType
					},
					TargetNamespace = "http://schemas.microsoft.com/serviceModel/2004/05/xpathfilter/"
				});
			}
			return xmlSchemaComplexType;
		}

		// Token: 0x06003197 RID: 12695 RVA: 0x000BE7D3 File Offset: 0x000BC9D3
		public XPathMessageFilter() : this(string.Empty)
		{
		}

		// Token: 0x06003198 RID: 12696 RVA: 0x000BE7E0 File Offset: 0x000BC9E0
		public XPathMessageFilter(string xpath) : this(xpath, new XPathMessageContext())
		{
		}

		// Token: 0x06003199 RID: 12697 RVA: 0x000BE7EE File Offset: 0x000BC9EE
		public XPathMessageFilter(string xpath, XmlNamespaceManager namespaces)
		{
			if (xpath == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("xpath");
			}
			this.Init(xpath, namespaces);
		}

		// Token: 0x0600319A RID: 12698 RVA: 0x000BE811 File Offset: 0x000BCA11
		public XPathMessageFilter(string xpath, XsltContext context)
		{
			if (xpath == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("xpath");
			}
			this.Init(xpath, context);
		}

		// Token: 0x0600319B RID: 12699 RVA: 0x000BE834 File Offset: 0x000BCA34
		public XPathMessageFilter(XmlReader reader) : this(reader, new XPathMessageContext())
		{
		}

		// Token: 0x0600319C RID: 12700 RVA: 0x000BE842 File Offset: 0x000BCA42
		public XPathMessageFilter(XmlReader reader, XmlNamespaceManager namespaces)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			this.ReadFrom(reader, namespaces);
		}

		// Token: 0x0600319D RID: 12701 RVA: 0x000BE865 File Offset: 0x000BCA65
		public XPathMessageFilter(XmlReader reader, XsltContext context)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			this.ReadFrom(reader, context);
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x0600319E RID: 12702 RVA: 0x000BE888 File Offset: 0x000BCA88
		public XmlNamespaceManager Namespaces
		{
			get
			{
				return this.namespaces;
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x0600319F RID: 12703 RVA: 0x000BE890 File Offset: 0x000BCA90
		// (set) Token: 0x060031A0 RID: 12704 RVA: 0x000BE89D File Offset: 0x000BCA9D
		public int NodeQuota
		{
			get
			{
				return this.matcher.NodeQuota;
			}
			set
			{
				if (value <= 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("NodeQuota", value, SR.GetString("FilterQuotaRange")));
				}
				this.EnsureMatcher();
				this.matcher.NodeQuota = value;
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x060031A1 RID: 12705 RVA: 0x000BE8DA File Offset: 0x000BCADA
		public string XPath
		{
			get
			{
				return this.xpath;
			}
		}

		// Token: 0x060031A2 RID: 12706 RVA: 0x000BE8E2 File Offset: 0x000BCAE2
		private void Compile()
		{
			if (!this.matcher.IsCompiled)
			{
				this.EnsureMatcher();
				this.matcher.Compile(this.xpath, this.namespaces);
			}
		}

		// Token: 0x060031A3 RID: 12707 RVA: 0x000BE90E File Offset: 0x000BCB0E
		internal void Compile(bool internalEngine)
		{
			this.EnsureMatcher();
			if (internalEngine)
			{
				this.matcher.CompileForInternal(this.xpath, this.namespaces);
				return;
			}
			this.matcher.CompileForExternal(this.xpath, this.namespaces);
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x000BE948 File Offset: 0x000BCB48
		protected internal override IMessageFilterTable<FilterData> CreateFilterTable<FilterData>()
		{
			return new XPathMessageFilterTable<FilterData>
			{
				NodeQuota = this.NodeQuota
			};
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x000BE968 File Offset: 0x000BCB68
		private void EnsureMatcher()
		{
			if (this.matcher == XPathMessageFilter.dummyMatcher)
			{
				this.matcher = new XPathQueryMatcher(true);
			}
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x000BE983 File Offset: 0x000BCB83
		XmlSchema IXmlSerializable.GetSchema()
		{
			return this.OnGetSchema();
		}

		// Token: 0x060031A7 RID: 12711 RVA: 0x000BE98C File Offset: 0x000BCB8C
		protected virtual XmlSchema OnGetSchema()
		{
			XmlSchemaComplexType item = XPathMessageFilter.CreateOuterType();
			return new XmlSchema
			{
				Items = 
				{
					item
				},
				TargetNamespace = "http://schemas.microsoft.com/serviceModel/2004/05/xpathfilter/"
			};
		}

		// Token: 0x060031A8 RID: 12712 RVA: 0x000BE9BE File Offset: 0x000BCBBE
		private void Init(string xpath, XmlNamespaceManager namespaces)
		{
			this.xpath = xpath;
			this.namespaces = namespaces;
			this.matcher = XPathMessageFilter.dummyMatcher;
			this.Compile();
		}

		// Token: 0x060031A9 RID: 12713 RVA: 0x000BE9DF File Offset: 0x000BCBDF
		public override bool Match(Message message)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			return this.ProcessResult(this.matcher.Match(message, false));
		}

		// Token: 0x060031AA RID: 12714 RVA: 0x000BEA07 File Offset: 0x000BCC07
		public override bool Match(MessageBuffer messageBuffer)
		{
			if (messageBuffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("messageBuffer");
			}
			return this.ProcessResult(this.matcher.Match(messageBuffer));
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x000BEA2E File Offset: 0x000BCC2E
		public bool Match(XPathNavigator navigator)
		{
			if (navigator == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("navigator");
			}
			return this.ProcessResult(this.matcher.Match(navigator));
		}

		// Token: 0x060031AC RID: 12716 RVA: 0x000BEA55 File Offset: 0x000BCC55
		public bool Match(SeekableXPathNavigator navigator)
		{
			if (navigator == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("navigator");
			}
			return this.ProcessResult(this.matcher.Match(navigator));
		}

		// Token: 0x060031AD RID: 12717 RVA: 0x000BEA7C File Offset: 0x000BCC7C
		private bool ProcessResult(FilterResult result)
		{
			bool result2 = result.Result;
			this.matcher.ReleaseResult(result);
			return result2;
		}

		// Token: 0x060031AE RID: 12718 RVA: 0x000BEAA0 File Offset: 0x000BCCA0
		private void ReadFrom(XmlReader reader, XmlNamespaceManager namespaces)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (!reader.IsStartElement())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("reader", SR.GetString("FilterReaderNotStartElem"));
			}
			bool flag = false;
			string text = null;
			while (reader.MoveToNextAttribute())
			{
				if (QueryDataModel.IsAttribute(reader.NamespaceURI))
				{
					if (flag || reader.LocalName != "Dialect" || reader.NamespaceURI != "http://schemas.xmlsoap.org/ws/2004/06/eventing")
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("FilterInvalidAttribute")));
					}
					text = reader.Value;
					flag = true;
				}
			}
			if (reader.NodeType == XmlNodeType.Attribute)
			{
				reader.MoveToElement();
			}
			if (text != null && text != "http://www.w3.org/TR/1999/REC-xpath-19991116")
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("FilterInvalidDialect", new object[]
				{
					"http://www.w3.org/TR/1999/REC-xpath-19991116"
				})));
			}
			bool isEmptyElement = reader.IsEmptyElement;
			reader.ReadStartElement();
			if (isEmptyElement)
			{
				this.Init(string.Empty, namespaces);
				return;
			}
			this.ReadXPath(reader, namespaces);
			reader.ReadEndElement();
		}

		// Token: 0x060031AF RID: 12719 RVA: 0x000BEBBA File Offset: 0x000BCDBA
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.OnReadXml(reader);
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x000BEBC4 File Offset: 0x000BCDC4
		protected virtual void OnReadXml(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (!reader.IsStartElement())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("reader", SR.GetString("FilterReaderNotStartElem"));
			}
			if (reader.IsEmptyElement)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("reader", SR.GetString("FilterInvalidInner", new object[]
				{
					"XPath"
				}));
			}
			string text = null;
			while (reader.MoveToNextAttribute())
			{
				if (QueryDataModel.IsAttribute(reader.NamespaceURI) && reader.LocalName == "NodeQuota" && reader.NamespaceURI.Length == 0)
				{
					text = reader.Value;
					break;
				}
			}
			if (reader.NodeType == XmlNodeType.Attribute)
			{
				reader.MoveToElement();
			}
			int nodeQuota = (text == null) ? int.MaxValue : int.Parse(text, NumberFormatInfo.InvariantInfo);
			reader.ReadStartElement();
			reader.MoveToContent();
			if (reader.LocalName != "XPath")
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("reader", SR.GetString("FilterInvalidInner", new object[]
				{
					"XPath"
				}));
			}
			this.ReadFrom(reader, new XPathMessageContext());
			reader.MoveToContent();
			reader.ReadEndElement();
			this.NodeQuota = nodeQuota;
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x000BED04 File Offset: 0x000BCF04
		protected void ReadXPath(XmlReader reader, XmlNamespaceManager namespaces)
		{
			string text = reader.ReadString().Trim();
			if (text.Length != 0)
			{
				XPathLexer xpathLexer = new XPathLexer(text, false);
				while (xpathLexer.MoveNext())
				{
					string prefix = xpathLexer.Token.Prefix;
					if (prefix.Length > 0)
					{
						string text2 = null;
						if (namespaces != null)
						{
							text2 = namespaces.LookupNamespace(prefix);
						}
						if (text2 == null || text2.Length <= 0)
						{
							text2 = reader.LookupNamespace(prefix);
							if (text2 != null && text2.Length > 0)
							{
								if (namespaces == null)
								{
									namespaces = new XPathMessageContext();
								}
								namespaces.AddNamespace(prefix, text2);
							}
						}
					}
				}
			}
			this.Init(text, namespaces);
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x000BED93 File Offset: 0x000BCF93
		public void TrimToSize()
		{
			this.matcher.Trim();
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x000BEDA0 File Offset: 0x000BCFA0
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.OnWriteXml(writer);
		}

		// Token: 0x060031B4 RID: 12724 RVA: 0x000BEDAC File Offset: 0x000BCFAC
		protected virtual void OnWriteXml(XmlWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			writer.WriteAttributeString("NodeQuota", this.NodeQuota.ToString(NumberFormatInfo.InvariantInfo));
			this.WriteXPathTo(writer, null, "XPath", null, true);
		}

		// Token: 0x060031B5 RID: 12725 RVA: 0x000BEDFC File Offset: 0x000BCFFC
		protected void WriteXPath(XmlWriter writer, IXmlNamespaceResolver resolver)
		{
			int num = 0;
			int num2 = 0;
			string text = "";
			XPathLexer xpathLexer = new XPathLexer(this.xpath, false);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			List<string> list = new List<string>();
			while (xpathLexer.MoveNext())
			{
				string prefix = xpathLexer.Token.Prefix;
				string text2 = resolver.LookupNamespace(prefix);
				if (prefix.Length > 0 && (text2 == null || (text2 != null && text2 != this.namespaces.LookupNamespace(prefix))))
				{
					if (this.xpath[num2] == '$')
					{
						text += this.xpath.Substring(num, num2 - num + 1);
						num = num2 + 1;
					}
					else
					{
						text += this.xpath.Substring(num, num2 - num);
						num = num2;
					}
					if (!dictionary.ContainsKey(prefix))
					{
						list.Add(prefix);
						if (text2 != null)
						{
							string text3 = prefix;
							int num3 = 0;
							while (resolver.LookupNamespace(text3) != null || this.namespaces.LookupNamespace(text3) != null)
							{
								text3 += num3.ToString(NumberFormatInfo.InvariantInfo);
								num3++;
							}
							dictionary.Add(prefix, text3);
						}
						else
						{
							dictionary.Add(prefix, prefix);
						}
					}
					text += dictionary[prefix];
					num += prefix.Length;
				}
				num2 = xpathLexer.FirstTokenChar;
			}
			text += this.xpath.Substring(num);
			for (int i = 0; i < list.Count; i++)
			{
				string text4 = list[i];
				writer.WriteAttributeString("xmlns", dictionary[text4], null, this.namespaces.LookupNamespace(text4));
			}
			writer.WriteString(text);
		}

		// Token: 0x060031B6 RID: 12726 RVA: 0x000BEFB8 File Offset: 0x000BD1B8
		public void WriteXPathTo(XmlWriter writer, string prefix, string localName, string ns, bool writeNamespaces)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (localName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("localName");
			}
			if (localName.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("localName", SR.GetString("FilterEmptyString"));
			}
			if (prefix == null)
			{
				prefix = string.Empty;
			}
			if (ns == null)
			{
				ns = string.Empty;
			}
			writer.WriteStartElement(prefix, localName, ns);
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(new NameTable());
			if (!writeNamespaces)
			{
				foreach (object obj in this.namespaces)
				{
					string text = (string)obj;
					if (text != "xml" && text != "xmlns")
					{
						xmlNamespaceManager.AddNamespace(text, this.namespaces.LookupNamespace(text));
					}
				}
			}
			xmlNamespaceManager.AddNamespace(prefix, ns);
			this.WriteXPath(writer, xmlNamespaceManager);
			writer.WriteEndElement();
		}

		// Token: 0x04002670 RID: 9840
		internal const string NodeQuotaAttr = "NodeQuota";

		// Token: 0x04002671 RID: 9841
		private const string DialectAttr = "Dialect";

		// Token: 0x04002672 RID: 9842
		private const string OuterTypeName = "XPathMessageFilter";

		// Token: 0x04002673 RID: 9843
		private const string InnerElem = "XPath";

		// Token: 0x04002674 RID: 9844
		private const string XmlP = "xml";

		// Token: 0x04002675 RID: 9845
		private const string XmlnsP = "xmlns";

		// Token: 0x04002676 RID: 9846
		private const string WSEventingNamespace = "http://schemas.xmlsoap.org/ws/2004/06/eventing";

		// Token: 0x04002677 RID: 9847
		internal const string XPathDialect = "http://www.w3.org/TR/1999/REC-xpath-19991116";

		// Token: 0x04002678 RID: 9848
		private const string RootNamespace = "http://schemas.microsoft.com/serviceModel/2004/05/xpathfilter";

		// Token: 0x04002679 RID: 9849
		private const string Namespace = "http://schemas.microsoft.com/serviceModel/2004/05/xpathfilter/";

		// Token: 0x0400267A RID: 9850
		private static XPathQueryMatcher dummyMatcher = new XPathQueryMatcher(true);

		// Token: 0x0400267B RID: 9851
		private XPathQueryMatcher matcher;

		// Token: 0x0400267C RID: 9852
		internal XmlNamespaceManager namespaces;

		// Token: 0x0400267D RID: 9853
		private string xpath;
	}
}
