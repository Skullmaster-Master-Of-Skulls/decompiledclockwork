using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel.Description
{
	// Token: 0x020003EA RID: 1002
	internal class XmlSerializationWriterMetadataSet : XmlSerializationWriter
	{
		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x060025BF RID: 9663 RVA: 0x0008770B File Offset: 0x0008590B
		// (set) Token: 0x060025C0 RID: 9664 RVA: 0x00087713 File Offset: 0x00085913
		public bool ProcessOuterElement
		{
			get
			{
				return this.processOuterElement;
			}
			set
			{
				this.processOuterElement = value;
			}
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x0008771C File Offset: 0x0008591C
		public void Write68_Metadata(object o)
		{
			if (this.processOuterElement)
			{
				base.WriteStartDocument();
				if (o == null)
				{
					base.WriteNullTagLiteral("Metadata", "http://schemas.xmlsoap.org/ws/2004/09/mex");
					return;
				}
				base.TopLevelElement();
			}
			this.Write67_MetadataSet("Metadata", "http://schemas.xmlsoap.org/ws/2004/09/mex", (MetadataSet)o, true, false);
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x0008776C File Offset: 0x0008596C
		private void Write67_MetadataSet(string n, string ns, MetadataSet o, bool isNullable, bool needType)
		{
			if (this.processOuterElement && o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(MetadataSet)))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateUnknownTypeException(o));
				}
			}
			if (this.processOuterElement)
			{
				base.WriteStartElement(n, ns, o, false, null);
			}
			XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
			xmlSerializerNamespaces.Add("wsx", "http://schemas.xmlsoap.org/ws/2004/09/mex");
			base.WriteNamespaceDeclarations(xmlSerializerNamespaces);
			if (needType)
			{
				base.WriteXsiType("MetadataSet", "http://schemas.xmlsoap.org/ws/2004/09/mex");
			}
			Collection<XmlAttribute> attributes = o.Attributes;
			if (attributes != null)
			{
				for (int i = 0; i < ((ICollection)attributes).Count; i++)
				{
					XmlAttribute node = attributes[i];
					base.WriteXmlAttribute(node, o);
				}
			}
			Collection<MetadataSection> metadataSections = o.MetadataSections;
			if (metadataSections != null)
			{
				for (int j = 0; j < ((ICollection)metadataSections).Count; j++)
				{
					this.Write66_MetadataSection("MetadataSection", "http://schemas.xmlsoap.org/ws/2004/09/mex", metadataSections[j], false, false);
				}
			}
			if (this.processOuterElement)
			{
				base.WriteEndElement(o);
			}
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x00087880 File Offset: 0x00085A80
		private void Write66_MetadataSection(string n, string ns, MetadataSection o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(MetadataSection)))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateUnknownTypeException(o));
				}
			}
			XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
			xmlSerializerNamespaces.Add(string.Empty, string.Empty);
			base.WriteStartElement(n, ns, o, true, xmlSerializerNamespaces);
			if (needType)
			{
				base.WriteXsiType("MetadataSection", "http://schemas.xmlsoap.org/ws/2004/09/mex");
			}
			Collection<XmlAttribute> attributes = o.Attributes;
			if (attributes != null)
			{
				for (int i = 0; i < ((ICollection)attributes).Count; i++)
				{
					XmlAttribute node = attributes[i];
					base.WriteXmlAttribute(node, o);
				}
			}
			base.WriteAttribute("Dialect", "", o.Dialect);
			base.WriteAttribute("Identifier", "", o.Identifier);
			if (o.Metadata is ServiceDescription)
			{
				((ServiceDescription)o.Metadata).Write(base.Writer);
			}
			else if (o.Metadata is XmlSchema)
			{
				((XmlSchema)o.Metadata).Write(base.Writer);
			}
			else if (o.Metadata is MetadataSet)
			{
				this.Write67_MetadataSet("Metadata", "http://schemas.xmlsoap.org/ws/2004/09/mex", (MetadataSet)o.Metadata, false, false);
			}
			else if (o.Metadata is MetadataLocation)
			{
				this.Write65_MetadataLocation("Location", "http://schemas.xmlsoap.org/ws/2004/09/mex", (MetadataLocation)o.Metadata, false, false);
			}
			else if (o.Metadata is MetadataReference)
			{
				base.WriteSerializable((MetadataReference)o.Metadata, "MetadataReference", "http://schemas.xmlsoap.org/ws/2004/09/mex", false, true);
			}
			else if (o.Metadata is XmlElement)
			{
				XmlElement xmlElement = (XmlElement)o.Metadata;
				if (xmlElement == null && xmlElement != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateInvalidAnyTypeException(xmlElement));
				}
				base.WriteElementLiteral(xmlElement, "", null, false, true);
			}
			else if (o.Metadata != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateUnknownTypeException(o.Metadata));
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x00087AA4 File Offset: 0x00085CA4
		private void Write65_MetadataLocation(string n, string ns, MetadataLocation o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(MetadataLocation)))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateUnknownTypeException(o));
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("MetadataLocation", "http://schemas.xmlsoap.org/ws/2004/09/mex");
			}
			base.WriteValue(o.Location);
			base.WriteEndElement(o);
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x00087B22 File Offset: 0x00085D22
		protected override void InitCallbacks()
		{
		}

		// Token: 0x040020D8 RID: 8408
		private bool processOuterElement = true;
	}
}
