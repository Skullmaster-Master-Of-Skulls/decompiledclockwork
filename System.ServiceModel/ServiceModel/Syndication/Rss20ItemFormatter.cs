using System;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel.Syndication
{
	// Token: 0x02000198 RID: 408
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[XmlRoot(ElementName = "item", Namespace = "")]
	public class Rss20ItemFormatter : SyndicationItemFormatter, IXmlSerializable
	{
		// Token: 0x06000D25 RID: 3365 RVA: 0x000302A4 File Offset: 0x0002E4A4
		public Rss20ItemFormatter() : this(typeof(SyndicationItem))
		{
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x000302B8 File Offset: 0x0002E4B8
		public Rss20ItemFormatter(Type itemTypeToCreate)
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
			this.feedSerializer = new Rss20FeedFormatter();
			this.feedSerializer.PreserveAttributeExtensions = (this.preserveAttributeExtensions = true);
			this.feedSerializer.PreserveElementExtensions = (this.preserveElementExtensions = true);
			this.feedSerializer.SerializeExtensionsAsAtom = (this.serializeExtensionsAsAtom = true);
			this.itemType = itemTypeToCreate;
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00030377 File Offset: 0x0002E577
		public Rss20ItemFormatter(SyndicationItem itemToWrite) : this(itemToWrite, true)
		{
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00030384 File Offset: 0x0002E584
		public Rss20ItemFormatter(SyndicationItem itemToWrite, bool serializeExtensionsAsAtom) : base(itemToWrite)
		{
			this.feedSerializer = new Rss20FeedFormatter();
			this.feedSerializer.PreserveAttributeExtensions = (this.preserveAttributeExtensions = true);
			this.feedSerializer.PreserveElementExtensions = (this.preserveElementExtensions = true);
			Rss20FeedFormatter rss20FeedFormatter = this.feedSerializer;
			this.serializeExtensionsAsAtom = serializeExtensionsAsAtom;
			rss20FeedFormatter.SerializeExtensionsAsAtom = serializeExtensionsAsAtom;
			this.itemType = itemToWrite.GetType();
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x000303EE File Offset: 0x0002E5EE
		// (set) Token: 0x06000D2A RID: 3370 RVA: 0x000303F6 File Offset: 0x0002E5F6
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

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x0003040B File Offset: 0x0002E60B
		// (set) Token: 0x06000D2C RID: 3372 RVA: 0x00030413 File Offset: 0x0002E613
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

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x00030428 File Offset: 0x0002E628
		// (set) Token: 0x06000D2E RID: 3374 RVA: 0x00030430 File Offset: 0x0002E630
		public bool SerializeExtensionsAsAtom
		{
			get
			{
				return this.serializeExtensionsAsAtom;
			}
			set
			{
				this.serializeExtensionsAsAtom = value;
				this.feedSerializer.SerializeExtensionsAsAtom = value;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x00030445 File Offset: 0x0002E645
		public override string Version
		{
			get
			{
				return "Rss20";
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x0003044C File Offset: 0x0002E64C
		protected Type ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00030454 File Offset: 0x0002E654
		public override bool CanRead(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			return reader.IsStartElement("item", "");
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x00030479 File Offset: 0x0002E679
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0003047C File Offset: 0x0002E67C
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

		// Token: 0x06000D34 RID: 3380 RVA: 0x000304A2 File Offset: 0x0002E6A2
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

		// Token: 0x06000D35 RID: 3381 RVA: 0x000304C8 File Offset: 0x0002E6C8
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

		// Token: 0x06000D36 RID: 3382 RVA: 0x00030521 File Offset: 0x0002E721
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			SyndicationFeedFormatter.TraceItemWriteBegin();
			writer.WriteStartElement("item", "");
			this.WriteItem(writer);
			writer.WriteEndElement();
			SyndicationFeedFormatter.TraceItemWriteEnd();
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0003055D File Offset: 0x0002E75D
		protected override SyndicationItem CreateItemInstance()
		{
			return SyndicationItemFormatter.CreateItemInstance(this.itemType);
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0003056A File Offset: 0x0002E76A
		private void ReadItem(XmlReader reader)
		{
			this.SetItem(this.CreateItemInstance());
			this.feedSerializer.ReadItemFrom(XmlDictionaryReader.CreateDictionaryReader(reader), base.Item);
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00030590 File Offset: 0x0002E790
		private void WriteItem(XmlWriter writer)
		{
			if (base.Item == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ItemFormatterDoesNotHaveItem")));
			}
			XmlDictionaryWriter writer2 = XmlDictionaryWriter.CreateDictionaryWriter(writer);
			this.feedSerializer.WriteItemContents(writer2, base.Item);
		}

		// Token: 0x040016F4 RID: 5876
		private Rss20FeedFormatter feedSerializer;

		// Token: 0x040016F5 RID: 5877
		private Type itemType;

		// Token: 0x040016F6 RID: 5878
		private bool preserveAttributeExtensions;

		// Token: 0x040016F7 RID: 5879
		private bool preserveElementExtensions;

		// Token: 0x040016F8 RID: 5880
		private bool serializeExtensionsAsAtom;
	}
}
