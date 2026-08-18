using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel.Description
{
	// Token: 0x020003E9 RID: 1001
	[XmlRoot("Metadata", Namespace = "http://schemas.xmlsoap.org/ws/2004/09/mex")]
	public class MetadataSet : IXmlSerializable
	{
		// Token: 0x060025B5 RID: 9653 RVA: 0x00087592 File Offset: 0x00085792
		public MetadataSet()
		{
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x000875B0 File Offset: 0x000857B0
		public MetadataSet(IEnumerable<MetadataSection> sections) : this()
		{
			if (sections != null)
			{
				foreach (MetadataSection item in sections)
				{
					this.sections.Add(item);
				}
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x060025B7 RID: 9655 RVA: 0x00087608 File Offset: 0x00085808
		[XmlElement("MetadataSection", Namespace = "http://schemas.xmlsoap.org/ws/2004/09/mex")]
		public Collection<MetadataSection> MetadataSections
		{
			get
			{
				return this.sections;
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x060025B8 RID: 9656 RVA: 0x00087610 File Offset: 0x00085810
		[XmlAnyAttribute]
		public Collection<XmlAttribute> Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x00087618 File Offset: 0x00085818
		public void WriteTo(XmlWriter writer)
		{
			this.WriteMetadataSet(writer, true);
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x00087624 File Offset: 0x00085824
		public static MetadataSet ReadFrom(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			MetadataSetSerializer metadataSetSerializer = new MetadataSetSerializer();
			return (MetadataSet)metadataSetSerializer.Deserialize(reader);
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x00087656 File Offset: 0x00085856
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x0008765C File Offset: 0x0008585C
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			MetadataSet metadataSet = (MetadataSet)new MetadataSetSerializer
			{
				ProcessOuterElement = false
			}.Deserialize(reader);
			this.sections = metadataSet.MetadataSections;
			this.attributes = metadataSet.Attributes;
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x000876AE File Offset: 0x000858AE
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteMetadataSet(writer, false);
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x000876B8 File Offset: 0x000858B8
		private void WriteMetadataSet(XmlWriter writer, bool processOuterElement)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (this.WriteFilter != null)
			{
				ServiceMetadataExtension.WriteFilter writeFilter = this.WriteFilter.CloneWriteFilter();
				writeFilter.Writer = writer;
				writer = writeFilter;
			}
			new MetadataSetSerializer
			{
				ProcessOuterElement = processOuterElement
			}.Serialize(writer, this);
		}

		// Token: 0x040020D5 RID: 8405
		private Collection<MetadataSection> sections = new Collection<MetadataSection>();

		// Token: 0x040020D6 RID: 8406
		private Collection<XmlAttribute> attributes = new Collection<XmlAttribute>();

		// Token: 0x040020D7 RID: 8407
		internal ServiceMetadataExtension.WriteFilter WriteFilter;
	}
}
