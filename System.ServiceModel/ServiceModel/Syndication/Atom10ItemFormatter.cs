using System;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel.Syndication
{
	// Token: 0x02000184 RID: 388
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[XmlRoot(ElementName = "entry", Namespace = "http://www.w3.org/2005/Atom")]
	public class Atom10ItemFormatter : SyndicationItemFormatter, IXmlSerializable
	{
		// Token: 0x06000B7F RID: 2943 RVA: 0x0002B284 File Offset: 0x00029484
		public Atom10ItemFormatter() : this(typeof(SyndicationItem))
		{
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0002B298 File Offset: 0x00029498
		public Atom10ItemFormatter(Type itemTypeToCreate)
		{
			if (itemTypeToCreate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("itemTypeToCreate");
			}
			if (!typeof(SyndicationItem).IsAssignableFrom(itemTypeToCreate))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("itemTypeToCreate", SR.GetString("InvalidObjectTypePassed", new object[]
				{
					"itemTypeToCreate",
					"SyndicationItem"
				}));
			}
			this.feedSerializer = new Atom10FeedFormatter();
			this.feedSerializer.PreserveAttributeExtensions = (this.preserveAttributeExtensions = true);
			this.feedSerializer.PreserveElementExtensions = (this.preserveElementExtensions = true);
			this.itemType = itemTypeToCreate;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0002B344 File Offset: 0x00029544
		public Atom10ItemFormatter(SyndicationItem itemToWrite) : base(itemToWrite)
		{
			this.feedSerializer = new Atom10FeedFormatter();
			this.feedSerializer.PreserveAttributeExtensions = (this.preserveAttributeExtensions = true);
			this.feedSerializer.PreserveElementExtensions = (this.preserveElementExtensions = true);
			this.itemType = itemToWrite.GetType();
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x0002B399 File Offset: 0x00029599
		// (set) Token: 0x06000B83 RID: 2947 RVA: 0x0002B3A1 File Offset: 0x000295A1
		public bool PreserveAttributeExtensions
		{
			get
			{
				return this.preserveAttributeExtensions;
			}
			set
			{
				this.preserveAttributeExtensions = value;
				this.feedSerializer.PreserveAttributeExtensions = value;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x0002B3B6 File Offset: 0x000295B6
		// (set) Token: 0x06000B85 RID: 2949 RVA: 0x0002B3BE File Offset: 0x000295BE
		public bool PreserveElementExtensions
		{
			get
			{
				return this.preserveElementExtensions;
			}
			set
			{
				this.preserveElementExtensions = value;
				this.feedSerializer.PreserveElementExtensions = value;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x0002B3D3 File Offset: 0x000295D3
		public override string Version
		{
			get
			{
				return "Atom10";
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x0002B3DA File Offset: 0x000295DA
		protected Type ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0002B3E2 File Offset: 0x000295E2
		public override bool CanRead(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			return reader.IsStartElement("entry", "http://www.w3.org/2005/Atom");
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0002B407 File Offset: 0x00029607
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0002B40A File Offset: 0x0002960A
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			SyndicationFeedFormatter.TraceItemReadBegin();
			this.ReadItem(reader);
			SyndicationFeedFormatter.TraceItemReadEnd();
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0002B430 File Offset: 0x00029630
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			SyndicationFeedFormatter.TraceItemWriteBegin();
			this.WriteItem(writer);
			SyndicationFeedFormatter.TraceItemWriteEnd();
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0002B458 File Offset: 0x00029658
		public override void ReadFrom(XmlReader reader)
		{
			SyndicationFeedFormatter.TraceItemReadBegin();
			if (!this.CanRead(reader))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("UnknownItemXml", new object[]
				{
					reader.LocalName,
					reader.NamespaceURI
				})));
			}
			this.ReadItem(reader);
			SyndicationFeedFormatter.TraceItemReadEnd();
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0002B4B1 File Offset: 0x000296B1
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			SyndicationFeedFormatter.TraceItemWriteBegin();
			writer.WriteStartElement("entry", "http://www.w3.org/2005/Atom");
			this.WriteItem(writer);
			writer.WriteEndElement();
			SyndicationFeedFormatter.TraceItemWriteEnd();
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0002B4ED File Offset: 0x000296ED
		protected override SyndicationItem CreateItemInstance()
		{
			return SyndicationItemFormatter.CreateItemInstance(this.itemType);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0002B4FA File Offset: 0x000296FA
		private void ReadItem(XmlReader reader)
		{
			this.SetItem(this.CreateItemInstance());
			this.feedSerializer.ReadItemFrom(XmlDictionaryReader.CreateDictionaryReader(reader), base.Item);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0002B520 File Offset: 0x00029720
		private void WriteItem(XmlWriter writer)
		{
			if (base.Item == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ItemFormatterDoesNotHaveItem")));
			}
			XmlDictionaryWriter dictWriter = XmlDictionaryWriter.CreateDictionaryWriter(writer);
			this.feedSerializer.WriteItemContents(dictWriter, base.Item);
		}

		// Token: 0x0400168D RID: 5773
		private Atom10FeedFormatter feedSerializer;

		// Token: 0x0400168E RID: 5774
		private Type itemType;

		// Token: 0x0400168F RID: 5775
		private bool preserveAttributeExtensions;

		// Token: 0x04001690 RID: 5776
		private bool preserveElementExtensions;
	}
}
