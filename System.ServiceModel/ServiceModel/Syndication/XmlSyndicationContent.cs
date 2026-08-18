using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace System.ServiceModel.Syndication
{
	// Token: 0x0200019E RID: 414
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class XmlSyndicationContent : SyndicationContent
	{
		// Token: 0x06000D4C RID: 3404 RVA: 0x000307A0 File Offset: 0x0002E9A0
		public XmlSyndicationContent(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			SyndicationFeedFormatter.MoveToStartElement(reader);
			if (reader.HasAttributes)
			{
				while (reader.MoveToNextAttribute())
				{
					string localName = reader.LocalName;
					string namespaceURI = reader.NamespaceURI;
					string value = reader.Value;
					if (localName == "type" && namespaceURI == string.Empty)
					{
						this.type = value;
					}
					else if (!FeedUtils.IsXmlns(localName, namespaceURI))
					{
						base.AttributeExtensions.Add(new XmlQualifiedName(localName, namespaceURI), value);
					}
				}
				reader.MoveToElement();
			}
			this.type = (string.IsNullOrEmpty(this.type) ? "text/xml" : this.type);
			this.contentBuffer = new XmlBuffer(int.MaxValue);
			using (XmlDictionaryWriter xmlDictionaryWriter = this.contentBuffer.OpenSection(XmlDictionaryReaderQuotas.Max))
			{
				xmlDictionaryWriter.WriteNode(reader, false);
			}
			this.contentBuffer.CloseSection();
			this.contentBuffer.Close();
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x000308B4 File Offset: 0x0002EAB4
		public XmlSyndicationContent(string type, object dataContractExtension, XmlObjectSerializer dataContractSerializer)
		{
			this.type = (string.IsNullOrEmpty(type) ? "text/xml" : type);
			this.extension = new SyndicationElementExtension(dataContractExtension, dataContractSerializer);
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x000308DF File Offset: 0x0002EADF
		public XmlSyndicationContent(string type, object xmlSerializerExtension, XmlSerializer serializer)
		{
			this.type = (string.IsNullOrEmpty(type) ? "text/xml" : type);
			this.extension = new SyndicationElementExtension(xmlSerializerExtension, serializer);
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0003090A File Offset: 0x0002EB0A
		public XmlSyndicationContent(string type, SyndicationElementExtension extension)
		{
			if (extension == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("extension");
			}
			this.type = (string.IsNullOrEmpty(type) ? "text/xml" : type);
			this.extension = extension;
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x00030942 File Offset: 0x0002EB42
		protected XmlSyndicationContent(XmlSyndicationContent source) : base(source)
		{
			if (source == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("source");
			}
			this.contentBuffer = source.contentBuffer;
			this.extension = source.extension;
			this.type = source.type;
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000D51 RID: 3409 RVA: 0x00030982 File Offset: 0x0002EB82
		public SyndicationElementExtension Extension
		{
			get
			{
				return this.extension;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x0003098A File Offset: 0x0002EB8A
		public override string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00030992 File Offset: 0x0002EB92
		public override SyndicationContent Clone()
		{
			return new XmlSyndicationContent(this);
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0003099A File Offset: 0x0002EB9A
		public XmlDictionaryReader GetReaderAtContent()
		{
			this.EnsureContentBuffer();
			return this.contentBuffer.GetReader(0);
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x000309AE File Offset: 0x0002EBAE
		public TContent ReadContent<TContent>()
		{
			return this.ReadContent<TContent>(null);
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x000309B8 File Offset: 0x0002EBB8
		public TContent ReadContent<TContent>(XmlObjectSerializer dataContractSerializer)
		{
			if (dataContractSerializer == null)
			{
				dataContractSerializer = new DataContractSerializer(typeof(TContent));
			}
			if (this.extension != null)
			{
				return this.extension.GetObject<TContent>(dataContractSerializer);
			}
			TContent result;
			using (XmlDictionaryReader reader = this.contentBuffer.GetReader(0))
			{
				reader.ReadStartElement();
				result = (TContent)((object)dataContractSerializer.ReadObject(reader, false));
			}
			return result;
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00030A2C File Offset: 0x0002EC2C
		public TContent ReadContent<TContent>(XmlSerializer serializer)
		{
			if (serializer == null)
			{
				serializer = new XmlSerializer(typeof(TContent));
			}
			if (this.extension != null)
			{
				return this.extension.GetObject<TContent>(serializer);
			}
			TContent result;
			using (XmlDictionaryReader reader = this.contentBuffer.GetReader(0))
			{
				reader.ReadStartElement();
				result = (TContent)((object)serializer.Deserialize(reader));
			}
			return result;
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00030AA0 File Offset: 0x0002ECA0
		protected override void WriteContentsTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (this.extension != null)
			{
				this.extension.WriteTo(writer);
				return;
			}
			if (this.contentBuffer != null)
			{
				using (XmlDictionaryReader reader = this.contentBuffer.GetReader(0))
				{
					reader.MoveToStartElement();
					if (!reader.IsEmptyElement)
					{
						reader.ReadStartElement();
						while (reader.Depth >= 1 && reader.ReadState == ReadState.Interactive)
						{
							writer.WriteNode(reader, false);
						}
					}
				}
			}
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00030B38 File Offset: 0x0002ED38
		private void EnsureContentBuffer()
		{
			if (this.contentBuffer == null)
			{
				XmlBuffer xmlBuffer = new XmlBuffer(int.MaxValue);
				using (XmlDictionaryWriter xmlDictionaryWriter = xmlBuffer.OpenSection(XmlDictionaryReaderQuotas.Max))
				{
					base.WriteTo(xmlDictionaryWriter, "content", "http://www.w3.org/2005/Atom");
				}
				xmlBuffer.CloseSection();
				xmlBuffer.Close();
				this.contentBuffer = xmlBuffer;
			}
		}

		// Token: 0x04001701 RID: 5889
		private XmlBuffer contentBuffer;

		// Token: 0x04001702 RID: 5890
		private SyndicationElementExtension extension;

		// Token: 0x04001703 RID: 5891
		private string type;
	}
}
