using System;
using System.Collections;
using System.IO;
using System.ServiceModel.Channels;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel
{
	// Token: 0x02000122 RID: 290
	[XmlSchemaProvider("GetSchema")]
	[XmlRoot("EndpointReference", Namespace = "http://schemas.xmlsoap.org/ws/2004/08/addressing")]
	public class EndpointAddressAugust2004 : IXmlSerializable
	{
		// Token: 0x060007AC RID: 1964 RVA: 0x00020507 File Offset: 0x0001E707
		private EndpointAddressAugust2004()
		{
			this.address = null;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00020516 File Offset: 0x0001E716
		private EndpointAddressAugust2004(EndpointAddress address)
		{
			this.address = address;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00020525 File Offset: 0x0001E725
		public static EndpointAddressAugust2004 FromEndpointAddress(EndpointAddress address)
		{
			if (address == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("address");
			}
			return new EndpointAddressAugust2004(address);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00020546 File Offset: 0x0001E746
		public EndpointAddress ToEndpointAddress()
		{
			return this.address;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0002054E File Offset: 0x0001E74E
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.address = EndpointAddress.ReadFrom(AddressingVersion.WSAddressingAugust2004, XmlDictionaryReader.CreateDictionaryReader(reader));
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00020566 File Offset: 0x0001E766
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.address.WriteContentsTo(AddressingVersion.WSAddressingAugust2004, XmlDictionaryWriter.CreateDictionaryWriter(writer));
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x0002057E File Offset: 0x0001E77E
		private static XmlQualifiedName EprType
		{
			get
			{
				if (EndpointAddressAugust2004.eprType == null)
				{
					EndpointAddressAugust2004.eprType = new XmlQualifiedName("EndpointReferenceType", "http://schemas.xmlsoap.org/ws/2004/08/addressing");
				}
				return EndpointAddressAugust2004.eprType;
			}
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x000205A8 File Offset: 0x0001E7A8
		private static XmlSchema GetEprSchema()
		{
			XmlSchema result;
			using (XmlTextReader xmlTextReader = new XmlTextReader(new StringReader("<xs:schema targetNamespace=\"http://schemas.xmlsoap.org/ws/2004/08/addressing\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:wsa=\"http://schemas.xmlsoap.org/ws/2004/08/addressing\" elementFormDefault=\"qualified\" blockDefault=\"#all\">\r\n  <!-- //////////////////// WS-Addressing //////////////////// -->\r\n  <!-- Endpoint reference -->\r\n  <xs:element name=\"EndpointReference\" type=\"wsa:EndpointReferenceType\"/>\r\n  <xs:complexType name=\"EndpointReferenceType\">\r\n    <xs:sequence>\r\n      <xs:element name=\"Address\" type=\"wsa:AttributedURI\"/>\r\n      <xs:element name=\"ReferenceProperties\" type=\"wsa:ReferencePropertiesType\" minOccurs=\"0\"/>\r\n      <xs:element name=\"ReferenceParameters\" type=\"wsa:ReferenceParametersType\" minOccurs=\"0\"/>\r\n      <xs:element name=\"PortType\" type=\"wsa:AttributedQName\" minOccurs=\"0\"/>\r\n      <xs:element name=\"ServiceName\" type=\"wsa:ServiceNameType\" minOccurs=\"0\"/>\r\n      <xs:any namespace=\"##other\" processContents=\"lax\" minOccurs=\"0\" maxOccurs=\"unbounded\">\r\n        <xs:annotation>\r\n          <xs:documentation>\r\n\t\t\t\t\t If \"Policy\" elements from namespace \"http://schemas.xmlsoap.org/ws/2002/12/policy#policy\" are used, they must appear first (before any extensibility elements).\r\n\t\t\t\t\t</xs:documentation>\r\n        </xs:annotation>\r\n      </xs:any>\r\n    </xs:sequence>\r\n    <xs:anyAttribute namespace=\"##other\" processContents=\"lax\"/>\r\n  </xs:complexType>\r\n  <xs:complexType name=\"ReferencePropertiesType\">\r\n    <xs:sequence>\r\n      <xs:any processContents=\"lax\" minOccurs=\"0\" maxOccurs=\"unbounded\"/>\r\n    </xs:sequence>\r\n  </xs:complexType>\r\n  <xs:complexType name=\"ReferenceParametersType\">\r\n    <xs:sequence>\r\n      <xs:any processContents=\"lax\" minOccurs=\"0\" maxOccurs=\"unbounded\"/>\r\n    </xs:sequence>\r\n  </xs:complexType>\r\n  <xs:complexType name=\"ServiceNameType\">\r\n    <xs:simpleContent>\r\n      <xs:extension base=\"xs:QName\">\r\n        <xs:attribute name=\"PortName\" type=\"xs:NCName\"/>\r\n        <xs:anyAttribute namespace=\"##other\" processContents=\"lax\"/>\r\n      </xs:extension>\r\n    </xs:simpleContent>\r\n  </xs:complexType>\r\n  <!-- Message information header blocks -->\r\n  <xs:element name=\"MessageID\" type=\"wsa:AttributedURI\"/>\r\n  <xs:element name=\"RelatesTo\" type=\"wsa:Relationship\"/>\r\n  <xs:element name=\"To\" type=\"wsa:AttributedURI\"/>\r\n  <xs:element name=\"Action\" type=\"wsa:AttributedURI\"/>\r\n  <xs:element name=\"From\" type=\"wsa:EndpointReferenceType\"/>\r\n  <xs:element name=\"ReplyTo\" type=\"wsa:EndpointReferenceType\"/>\r\n  <xs:element name=\"FaultTo\" type=\"wsa:EndpointReferenceType\"/>\r\n  <xs:complexType name=\"Relationship\">\r\n    <xs:simpleContent>\r\n      <xs:extension base=\"xs:anyURI\">\r\n        <xs:attribute name=\"RelationshipType\" type=\"xs:QName\" use=\"optional\"/>\r\n        <xs:anyAttribute namespace=\"##other\" processContents=\"lax\"/>\r\n      </xs:extension>\r\n    </xs:simpleContent>\r\n  </xs:complexType>\r\n  <xs:simpleType name=\"RelationshipTypeValues\">\r\n    <xs:restriction base=\"xs:QName\">\r\n      <xs:enumeration value=\"wsa:Reply\"/>\r\n    </xs:restriction>\r\n  </xs:simpleType>\r\n  <xs:element name=\"ReplyAfter\" type=\"wsa:ReplyAfterType\"/>\r\n  <xs:complexType name=\"ReplyAfterType\">\r\n    <xs:simpleContent>\r\n      <xs:extension base=\"xs:nonNegativeInteger\">\r\n        <xs:anyAttribute namespace=\"##other\"/>\r\n      </xs:extension>\r\n    </xs:simpleContent>\r\n  </xs:complexType>\r\n  <xs:simpleType name=\"FaultSubcodeValues\">\r\n    <xs:restriction base=\"xs:QName\">\r\n      <xs:enumeration value=\"wsa:InvalidMessageInformationHeader\"/>\r\n      <xs:enumeration value=\"wsa:MessageInformationHeaderRequired\"/>\r\n      <xs:enumeration value=\"wsa:DestinationUnreachable\"/>\r\n      <xs:enumeration value=\"wsa:ActionNotSupported\"/>\r\n      <xs:enumeration value=\"wsa:EndpointUnavailable\"/>\r\n    </xs:restriction>\r\n  </xs:simpleType>\r\n  <xs:attribute name=\"Action\" type=\"xs:anyURI\"/>\r\n  <!-- Common declarations and definitions -->\r\n  <xs:complexType name=\"AttributedQName\">\r\n    <xs:simpleContent>\r\n      <xs:extension base=\"xs:QName\">\r\n        <xs:anyAttribute namespace=\"##other\" processContents=\"lax\"/>\r\n      </xs:extension>\r\n    </xs:simpleContent>\r\n  </xs:complexType>\r\n  <xs:complexType name=\"AttributedURI\">\r\n    <xs:simpleContent>\r\n      <xs:extension base=\"xs:anyURI\">\r\n        <xs:anyAttribute namespace=\"##other\" processContents=\"lax\"/>\r\n      </xs:extension>\r\n    </xs:simpleContent>\r\n  </xs:complexType>\r\n</xs:schema>"))
			{
				DtdProcessing = DtdProcessing.Prohibit
			})
			{
				result = XmlSchema.Read(xmlTextReader, null);
			}
			return result;
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x000205F4 File Offset: 0x0001E7F4
		public static XmlQualifiedName GetSchema(XmlSchemaSet xmlSchemaSet)
		{
			if (xmlSchemaSet == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("xmlSchemaSet");
			}
			XmlQualifiedName xmlQualifiedName = EndpointAddressAugust2004.EprType;
			XmlSchema eprSchema = EndpointAddressAugust2004.GetEprSchema();
			ICollection collection = xmlSchemaSet.Schemas("http://schemas.xmlsoap.org/ws/2004/08/addressing");
			if (collection == null || collection.Count == 0)
			{
				xmlSchemaSet.Add(eprSchema);
			}
			else
			{
				XmlSchema xmlSchema = null;
				foreach (object obj in collection)
				{
					XmlSchema xmlSchema2 = (XmlSchema)obj;
					if (xmlSchema2.SchemaTypes.Contains(xmlQualifiedName))
					{
						xmlSchema = null;
						break;
					}
					xmlSchema = xmlSchema2;
				}
				if (xmlSchema != null)
				{
					foreach (XmlQualifiedName xmlQualifiedName2 in eprSchema.Namespaces.ToArray())
					{
						xmlSchema.Namespaces.Add(xmlQualifiedName2.Name, xmlQualifiedName2.Namespace);
					}
					foreach (XmlSchemaObject item in eprSchema.Items)
					{
						xmlSchema.Items.Add(item);
					}
					xmlSchemaSet.Reprocess(xmlSchema);
				}
			}
			return xmlQualifiedName;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00020748 File Offset: 0x0001E948
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x04000ADF RID: 2783
		private static XmlQualifiedName eprType;

		// Token: 0x04000AE0 RID: 2784
		private EndpointAddress address;

		// Token: 0x04000AE1 RID: 2785
		private const string Schema = "<xs:schema targetNamespace=\"http://schemas.xmlsoap.org/ws/2004/08/addressing\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:wsa=\"http://schemas.xmlsoap.org/ws/2004/08/addressing\" elementFormDefault=\"qualified\" blockDefault=\"#all\">\r\n  <!-- //////////////////// WS-Addressing //////////////////// -->\r\n  <!-- Endpoint reference -->\r\n  <xs:element name=\"EndpointReference\" type=\"wsa:EndpointReferenceType\"/>\r\n  <xs:complexType name=\"EndpointReferenceType\">\r\n    <xs:sequence>\r\n      <xs:element name=\"Address\" type=\"wsa:AttributedURI\"/>\r\n      <xs:element name=\"ReferenceProperties\" type=\"wsa:ReferencePropertiesType\" minOccurs=\"0\"/>\r\n      <xs:element name=\"ReferenceParameters\" type=\"wsa:ReferenceParametersType\" minOccurs=\"0\"/>\r\n      <xs:element name=\"PortType\" type=\"wsa:AttributedQName\" minOccurs=\"0\"/>\r\n      <xs:element name=\"ServiceName\" type=\"wsa:ServiceNameType\" minOccurs=\"0\"/>\r\n      <xs:any namespace=\"##other\" processContents=\"lax\" minOccurs=\"0\" maxOccurs=\"unbounded\">\r\n        <xs:annotation>\r\n          <xs:documentation>\r\n\t\t\t\t\t If \"Policy\" elements from namespace \"http://schemas.xmlsoap.org/ws/2002/12/policy#policy\" are used, they must appear first (before any extensibility elements).\r\n\t\t\t\t\t</xs:documentation>\r\n        </xs:annotation>\r\n      </xs:any>\r\n    </xs:sequence>\r\n    <xs:anyAttribute namespace=\"##other\" processContents=\"lax\"/>\r\n  </xs:complexType>\r\n  <xs:complexType name=\"ReferencePropertiesType\">\r\n    <xs:sequence>\r\n      <xs:any processContents=\"lax\" minOccurs=\"0\" maxOccurs=\"unbounded\"/>\r\n    </xs:sequence>\r\n  </xs:complexType>\r\n  <xs:complexType name=\"ReferenceParametersType\">\r\n    <xs:sequence>\r\n      <xs:any processContents=\"lax\" minOccurs=\"0\" maxOccurs=\"unbounded\"/>\r\n    </xs:sequence>\r\n  </xs:complexType>\r\n  <xs:complexType name=\"ServiceNameType\">\r\n    <xs:simpleContent>\r\n      <xs:extension base=\"xs:QName\">\r\n        <xs:attribute name=\"PortName\" type=\"xs:NCName\"/>\r\n        <xs:anyAttribute namespace=\"##other\" processContents=\"lax\"/>\r\n      </xs:extension>\r\n    </xs:simpleContent>\r\n  </xs:complexType>\r\n  <!-- Message information header blocks -->\r\n  <xs:element name=\"MessageID\" type=\"wsa:AttributedURI\"/>\r\n  <xs:element name=\"RelatesTo\" type=\"wsa:Relationship\"/>\r\n  <xs:element name=\"To\" type=\"wsa:AttributedURI\"/>\r\n  <xs:element name=\"Action\" type=\"wsa:AttributedURI\"/>\r\n  <xs:element name=\"From\" type=\"wsa:EndpointReferenceType\"/>\r\n  <xs:element name=\"ReplyTo\" type=\"wsa:EndpointReferenceType\"/>\r\n  <xs:element name=\"FaultTo\" type=\"wsa:EndpointReferenceType\"/>\r\n  <xs:complexType name=\"Relationship\">\r\n    <xs:simpleContent>\r\n      <xs:extension base=\"xs:anyURI\">\r\n        <xs:attribute name=\"RelationshipType\" type=\"xs:QName\" use=\"optional\"/>\r\n        <xs:anyAttribute namespace=\"##other\" processContents=\"lax\"/>\r\n      </xs:extension>\r\n    </xs:simpleContent>\r\n  </xs:complexType>\r\n  <xs:simpleType name=\"RelationshipTypeValues\">\r\n    <xs:restriction base=\"xs:QName\">\r\n      <xs:enumeration value=\"wsa:Reply\"/>\r\n    </xs:restriction>\r\n  </xs:simpleType>\r\n  <xs:element name=\"ReplyAfter\" type=\"wsa:ReplyAfterType\"/>\r\n  <xs:complexType name=\"ReplyAfterType\">\r\n    <xs:simpleContent>\r\n      <xs:extension base=\"xs:nonNegativeInteger\">\r\n        <xs:anyAttribute namespace=\"##other\"/>\r\n      </xs:extension>\r\n    </xs:simpleContent>\r\n  </xs:complexType>\r\n  <xs:simpleType name=\"FaultSubcodeValues\">\r\n    <xs:restriction base=\"xs:QName\">\r\n      <xs:enumeration value=\"wsa:InvalidMessageInformationHeader\"/>\r\n      <xs:enumeration value=\"wsa:MessageInformationHeaderRequired\"/>\r\n      <xs:enumeration value=\"wsa:DestinationUnreachable\"/>\r\n      <xs:enumeration value=\"wsa:ActionNotSupported\"/>\r\n      <xs:enumeration value=\"wsa:EndpointUnavailable\"/>\r\n    </xs:restriction>\r\n  </xs:simpleType>\r\n  <xs:attribute name=\"Action\" type=\"xs:anyURI\"/>\r\n  <!-- Common declarations and definitions -->\r\n  <xs:complexType name=\"AttributedQName\">\r\n    <xs:simpleContent>\r\n      <xs:extension base=\"xs:QName\">\r\n        <xs:anyAttribute namespace=\"##other\" processContents=\"lax\"/>\r\n      </xs:extension>\r\n    </xs:simpleContent>\r\n  </xs:complexType>\r\n  <xs:complexType name=\"AttributedURI\">\r\n    <xs:simpleContent>\r\n      <xs:extension base=\"xs:anyURI\">\r\n        <xs:anyAttribute namespace=\"##other\" processContents=\"lax\"/>\r\n      </xs:extension>\r\n    </xs:simpleContent>\r\n  </xs:complexType>\r\n</xs:schema>";
	}
}
