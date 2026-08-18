using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace System.ServiceModel.Syndication
{
	// Token: 0x02000193 RID: 403
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class SyndicationElementExtension
	{
		// Token: 0x06000CCC RID: 3276 RVA: 0x0002D678 File Offset: 0x0002B878
		public SyndicationElementExtension(XmlReader xmlReader)
		{
			if (xmlReader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("xmlReader");
			}
			SyndicationFeedFormatter.MoveToStartElement(xmlReader);
			this.outerName = xmlReader.LocalName;
			this.outerNamespace = xmlReader.NamespaceURI;
			this.buffer = new XmlBuffer(int.MaxValue);
			using (XmlDictionaryWriter xmlDictionaryWriter = this.buffer.OpenSection(XmlDictionaryReaderQuotas.Max))
			{
				xmlDictionaryWriter.WriteStartElement("extensionWrapper");
				xmlDictionaryWriter.WriteNode(xmlReader, false);
				xmlDictionaryWriter.WriteEndElement();
			}
			this.buffer.CloseSection();
			this.buffer.Close();
			this.bufferElementIndex = 0;
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x0002D730 File Offset: 0x0002B930
		public SyndicationElementExtension(object dataContractExtension) : this(dataContractExtension, null)
		{
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0002D73A File Offset: 0x0002B93A
		public SyndicationElementExtension(object dataContractExtension, XmlObjectSerializer dataContractSerializer) : this(null, null, dataContractExtension, dataContractSerializer)
		{
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0002D746 File Offset: 0x0002B946
		public SyndicationElementExtension(string outerName, string outerNamespace, object dataContractExtension) : this(outerName, outerNamespace, dataContractExtension, null)
		{
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0002D754 File Offset: 0x0002B954
		public SyndicationElementExtension(string outerName, string outerNamespace, object dataContractExtension, XmlObjectSerializer dataContractSerializer)
		{
			if (dataContractExtension == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dataContractExtension");
			}
			if (outerName == string.Empty)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("OuterNameOfElementExtensionEmpty"));
			}
			if (dataContractSerializer == null)
			{
				dataContractSerializer = new DataContractSerializer(dataContractExtension.GetType());
			}
			this.outerName = outerName;
			this.outerNamespace = outerNamespace;
			this.extensionData = dataContractExtension;
			this.extensionDataWriter = new SyndicationElementExtension.ExtensionDataWriter(this.extensionData, dataContractSerializer, this.outerName, this.outerNamespace);
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0002D7E4 File Offset: 0x0002B9E4
		public SyndicationElementExtension(object xmlSerializerExtension, XmlSerializer serializer)
		{
			if (xmlSerializerExtension == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("xmlSerializerExtension");
			}
			if (serializer == null)
			{
				serializer = new XmlSerializer(xmlSerializerExtension.GetType());
			}
			this.extensionData = xmlSerializerExtension;
			this.extensionDataWriter = new SyndicationElementExtension.ExtensionDataWriter(this.extensionData, serializer);
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0002D833 File Offset: 0x0002BA33
		internal SyndicationElementExtension(XmlBuffer buffer, int bufferElementIndex, string outerName, string outerNamespace)
		{
			this.buffer = buffer;
			this.bufferElementIndex = bufferElementIndex;
			this.outerName = outerName;
			this.outerNamespace = outerNamespace;
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x0002D858 File Offset: 0x0002BA58
		public string OuterName
		{
			get
			{
				if (this.outerName == null)
				{
					this.EnsureOuterNameAndNs();
				}
				return this.outerName;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x0002D86E File Offset: 0x0002BA6E
		public string OuterNamespace
		{
			get
			{
				if (this.outerName == null)
				{
					this.EnsureOuterNameAndNs();
				}
				return this.outerNamespace;
			}
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x0002D884 File Offset: 0x0002BA84
		public TExtension GetObject<TExtension>()
		{
			return this.GetObject<TExtension>(new DataContractSerializer(typeof(TExtension)));
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x0002D89C File Offset: 0x0002BA9C
		public TExtension GetObject<TExtension>(XmlObjectSerializer serializer)
		{
			if (serializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serializer");
			}
			if (this.extensionData != null && typeof(TExtension).IsAssignableFrom(this.extensionData.GetType()))
			{
				return (TExtension)((object)this.extensionData);
			}
			TExtension result;
			using (XmlReader reader = this.GetReader())
			{
				result = (TExtension)((object)serializer.ReadObject(reader, false));
			}
			return result;
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0002D920 File Offset: 0x0002BB20
		public TExtension GetObject<TExtension>(XmlSerializer serializer)
		{
			if (serializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serializer");
			}
			if (this.extensionData != null && typeof(TExtension).IsAssignableFrom(this.extensionData.GetType()))
			{
				return (TExtension)((object)this.extensionData);
			}
			TExtension result;
			using (XmlReader reader = this.GetReader())
			{
				result = (TExtension)((object)serializer.Deserialize(reader));
			}
			return result;
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0002D9A4 File Offset: 0x0002BBA4
		public XmlReader GetReader()
		{
			this.EnsureBuffer();
			XmlReader reader = this.buffer.GetReader(0);
			int num = 0;
			reader.ReadStartElement("extensionWrapper");
			while (reader.IsStartElement() && num != this.bufferElementIndex)
			{
				num++;
				reader.Skip();
			}
			return reader;
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0002D9F0 File Offset: 0x0002BBF0
		public void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (this.extensionDataWriter != null)
			{
				this.extensionDataWriter.WriteTo(writer);
				return;
			}
			using (XmlReader reader = this.GetReader())
			{
				writer.WriteNode(reader, false);
			}
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0002DA50 File Offset: 0x0002BC50
		private void EnsureBuffer()
		{
			if (this.buffer == null)
			{
				this.buffer = new XmlBuffer(int.MaxValue);
				using (XmlDictionaryWriter xmlDictionaryWriter = this.buffer.OpenSection(XmlDictionaryReaderQuotas.Max))
				{
					xmlDictionaryWriter.WriteStartElement("extensionWrapper");
					this.WriteTo(xmlDictionaryWriter);
					xmlDictionaryWriter.WriteEndElement();
				}
				this.buffer.CloseSection();
				this.buffer.Close();
				this.bufferElementIndex = 0;
			}
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0002DAD8 File Offset: 0x0002BCD8
		private void EnsureOuterNameAndNs()
		{
			this.extensionDataWriter.ComputeOuterNameAndNs(out this.outerName, out this.outerNamespace);
		}

		// Token: 0x040016C2 RID: 5826
		private XmlBuffer buffer;

		// Token: 0x040016C3 RID: 5827
		private int bufferElementIndex;

		// Token: 0x040016C4 RID: 5828
		private object extensionData;

		// Token: 0x040016C5 RID: 5829
		private SyndicationElementExtension.ExtensionDataWriter extensionDataWriter;

		// Token: 0x040016C6 RID: 5830
		private string outerName;

		// Token: 0x040016C7 RID: 5831
		private string outerNamespace;

		// Token: 0x02000AF6 RID: 2806
		private class ExtensionDataWriter
		{
			// Token: 0x06006F2B RID: 28459 RVA: 0x0019D4FB File Offset: 0x0019B6FB
			public ExtensionDataWriter(object extensionData, XmlObjectSerializer dataContractSerializer, string outerName, string outerNamespace)
			{
				this.dataContractSerializer = dataContractSerializer;
				this.extensionData = extensionData;
				this.outerName = outerName;
				this.outerNamespace = outerNamespace;
			}

			// Token: 0x06006F2C RID: 28460 RVA: 0x0019D520 File Offset: 0x0019B720
			public ExtensionDataWriter(object extensionData, XmlSerializer serializer)
			{
				this.xmlSerializer = serializer;
				this.extensionData = extensionData;
			}

			// Token: 0x06006F2D RID: 28461 RVA: 0x0019D538 File Offset: 0x0019B738
			public void WriteTo(XmlWriter writer)
			{
				if (this.xmlSerializer != null)
				{
					this.xmlSerializer.Serialize(writer, this.extensionData);
					return;
				}
				if (this.outerName != null)
				{
					writer.WriteStartElement(this.outerName, this.outerNamespace);
					this.dataContractSerializer.WriteObjectContent(writer, this.extensionData);
					writer.WriteEndElement();
					return;
				}
				this.dataContractSerializer.WriteObject(writer, this.extensionData);
			}

			// Token: 0x06006F2E RID: 28462 RVA: 0x0019D5A8 File Offset: 0x0019B7A8
			internal void ComputeOuterNameAndNs(out string name, out string ns)
			{
				if (this.outerName != null)
				{
					name = this.outerName;
					ns = this.outerNamespace;
					return;
				}
				if (this.dataContractSerializer != null)
				{
					XsdDataContractExporter xsdDataContractExporter = new XsdDataContractExporter();
					XmlQualifiedName rootElementName = xsdDataContractExporter.GetRootElementName(this.extensionData.GetType());
					if (rootElementName != null)
					{
						name = rootElementName.Name;
						ns = rootElementName.Namespace;
						return;
					}
					this.ReadOuterNameAndNs(out name, out ns);
					return;
				}
				else
				{
					XmlReflectionImporter xmlReflectionImporter = new XmlReflectionImporter();
					XmlTypeMapping xmlTypeMapping = xmlReflectionImporter.ImportTypeMapping(this.extensionData.GetType());
					if (xmlTypeMapping != null && !string.IsNullOrEmpty(xmlTypeMapping.ElementName))
					{
						name = xmlTypeMapping.ElementName;
						ns = xmlTypeMapping.Namespace;
						return;
					}
					this.ReadOuterNameAndNs(out name, out ns);
					return;
				}
			}

			// Token: 0x06006F2F RID: 28463 RVA: 0x0019D654 File Offset: 0x0019B854
			internal void ReadOuterNameAndNs(out string name, out string ns)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream))
					{
						this.WriteTo(xmlWriter);
					}
					memoryStream.Seek(0L, SeekOrigin.Begin);
					using (XmlReader xmlReader = XmlReader.Create(memoryStream))
					{
						SyndicationFeedFormatter.MoveToStartElement(xmlReader);
						name = xmlReader.LocalName;
						ns = xmlReader.NamespaceURI;
					}
				}
			}

			// Token: 0x04003F51 RID: 16209
			private readonly XmlObjectSerializer dataContractSerializer;

			// Token: 0x04003F52 RID: 16210
			private readonly object extensionData;

			// Token: 0x04003F53 RID: 16211
			private readonly string outerName;

			// Token: 0x04003F54 RID: 16212
			private readonly string outerNamespace;

			// Token: 0x04003F55 RID: 16213
			private readonly XmlSerializer xmlSerializer;
		}
	}
}
