using System;
using System.Collections;
using System.IO;
using System.ServiceModel.Channels;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel
{
	// Token: 0x02000121 RID: 289
	[XmlSchemaProvider("GetSchema")]
	[XmlRoot("EndpointReference", Namespace = "http://www.w3.org/2005/08/addressing")]
	public class EndpointAddress10 : IXmlSerializable
	{
		// Token: 0x060007A2 RID: 1954 RVA: 0x000202C5 File Offset: 0x0001E4C5
		private EndpointAddress10()
		{
			this.address = null;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x000202D4 File Offset: 0x0001E4D4
		private EndpointAddress10(EndpointAddress address)
		{
			this.address = address;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x000202E3 File Offset: 0x0001E4E3
		public static EndpointAddress10 FromEndpointAddress(EndpointAddress address)
		{
			if (address == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("address");
			}
			return new EndpointAddress10(address);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00020304 File Offset: 0x0001E504
		public EndpointAddress ToEndpointAddress()
		{
			return this.address;
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0002030C File Offset: 0x0001E50C
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.address = EndpointAddress.ReadFrom(AddressingVersion.WSAddressing10, XmlDictionaryReader.CreateDictionaryReader(reader));
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00020324 File Offset: 0x0001E524
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.address.WriteContentsTo(AddressingVersion.WSAddressing10, XmlDictionaryWriter.CreateDictionaryWriter(writer));
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0002033C File Offset: 0x0001E53C
		private static XmlQualifiedName EprType
		{
			get
			{
				if (EndpointAddress10.eprType == null)
				{
					EndpointAddress10.eprType = new XmlQualifiedName("EndpointReferenceType", "http://www.w3.org/2005/08/addressing");
				}
				return EndpointAddress10.eprType;
			}
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00020364 File Offset: 0x0001E564
		private static XmlSchema GetEprSchema()
		{
			XmlSchema result;
			using (XmlTextReader xmlTextReader = new XmlTextReader(new StringReader("<xs:schema xmlns:xs='http://www.w3.org/2001/XMLSchema' xmlns:wsa='http://www.w3.org/2005/08/addressing' targetNamespace='http://www.w3.org/2005/08/addressing' blockDefault='#all' elementFormDefault='qualified' finalDefault='' attributeFormDefault='unqualified'>\r\n    \r\n    <!-- Constructs from the WS-Addressing Core -->\r\n\r\n    <xs:element name='EndpointReference' type='wsa:EndpointReferenceType'/>\r\n    <xs:complexType name='EndpointReferenceType' mixed='false'>\r\n        <xs:sequence>\r\n            <xs:element name='Address' type='wsa:AttributedURIType'/>\r\n            <xs:element name='ReferenceParameters' type='wsa:ReferenceParametersType' minOccurs='0'/>\r\n            <xs:element ref='wsa:Metadata' minOccurs='0'/>\r\n            <xs:any namespace='##other' processContents='lax' minOccurs='0' maxOccurs='unbounded'/>\r\n        </xs:sequence>\r\n        <xs:anyAttribute namespace='##other' processContents='lax'/>\r\n    </xs:complexType>\r\n    \r\n    <xs:complexType name='ReferenceParametersType' mixed='false'>\r\n        <xs:sequence>\r\n            <xs:any namespace='##any' processContents='lax' minOccurs='0' maxOccurs='unbounded'/>\r\n        </xs:sequence>\r\n        <xs:anyAttribute namespace='##other' processContents='lax'/>\r\n    </xs:complexType>\r\n    \r\n    <xs:element name='Metadata' type='wsa:MetadataType'/>\r\n    <xs:complexType name='MetadataType' mixed='false'>\r\n        <xs:sequence>\r\n            <xs:any namespace='##any' processContents='lax' minOccurs='0' maxOccurs='unbounded'/>\r\n        </xs:sequence>\r\n        <xs:anyAttribute namespace='##other' processContents='lax'/>\r\n    </xs:complexType>\r\n    \r\n    <xs:element name='MessageID' type='wsa:AttributedURIType'/>\r\n    <xs:element name='RelatesTo' type='wsa:RelatesToType'/>\r\n    <xs:complexType name='RelatesToType' mixed='false'>\r\n        <xs:simpleContent>\r\n            <xs:extension base='xs:anyURI'>\r\n                <xs:attribute name='RelationshipType' type='wsa:RelationshipTypeOpenEnum' use='optional' default='http://www.w3.org/2005/08/addressing/reply'/>\r\n                <xs:anyAttribute namespace='##other' processContents='lax'/>\r\n            </xs:extension>\r\n        </xs:simpleContent>\r\n    </xs:complexType>\r\n    \r\n    <xs:simpleType name='RelationshipTypeOpenEnum'>\r\n        <xs:union memberTypes='wsa:RelationshipType xs:anyURI'/>\r\n    </xs:simpleType>\r\n    \r\n    <xs:simpleType name='RelationshipType'>\r\n        <xs:restriction base='xs:anyURI'>\r\n            <xs:enumeration value='http://www.w3.org/2005/08/addressing/reply'/>\r\n        </xs:restriction>\r\n    </xs:simpleType>\r\n    \r\n    <xs:element name='ReplyTo' type='wsa:EndpointReferenceType'/>\r\n    <xs:element name='From' type='wsa:EndpointReferenceType'/>\r\n    <xs:element name='FaultTo' type='wsa:EndpointReferenceType'/>\r\n    <xs:element name='To' type='wsa:AttributedURIType'/>\r\n    <xs:element name='Action' type='wsa:AttributedURIType'/>\r\n\r\n    <xs:complexType name='AttributedURIType' mixed='false'>\r\n        <xs:simpleContent>\r\n            <xs:extension base='xs:anyURI'>\r\n                <xs:anyAttribute namespace='##other' processContents='lax'/>\r\n            </xs:extension>\r\n        </xs:simpleContent>\r\n    </xs:complexType>\r\n    \r\n    <!-- Constructs from the WS-Addressing SOAP binding -->\r\n\r\n    <xs:attribute name='IsReferenceParameter' type='xs:boolean'/>\r\n    \r\n    <xs:simpleType name='FaultCodesOpenEnumType'>\r\n        <xs:union memberTypes='wsa:FaultCodesType xs:QName'/>\r\n    </xs:simpleType>\r\n    \r\n    <xs:simpleType name='FaultCodesType'>\r\n        <xs:restriction base='xs:QName'>\r\n            <xs:enumeration value='wsa:InvalidAddressingHeader'/>\r\n            <xs:enumeration value='wsa:InvalidAddress'/>\r\n            <xs:enumeration value='wsa:InvalidEPR'/>\r\n            <xs:enumeration value='wsa:InvalidCardinality'/>\r\n            <xs:enumeration value='wsa:MissingAddressInEPR'/>\r\n            <xs:enumeration value='wsa:DuplicateMessageID'/>\r\n            <xs:enumeration value='wsa:ActionMismatch'/>\r\n            <xs:enumeration value='wsa:MessageAddressingHeaderRequired'/>\r\n            <xs:enumeration value='wsa:DestinationUnreachable'/>\r\n            <xs:enumeration value='wsa:ActionNotSupported'/>\r\n            <xs:enumeration value='wsa:EndpointUnavailable'/>\r\n        </xs:restriction>\r\n    </xs:simpleType>\r\n    \r\n    <xs:element name='RetryAfter' type='wsa:AttributedUnsignedLongType'/>\r\n    <xs:complexType name='AttributedUnsignedLongType' mixed='false'>\r\n        <xs:simpleContent>\r\n            <xs:extension base='xs:unsignedLong'>\r\n                <xs:anyAttribute namespace='##other' processContents='lax'/>\r\n            </xs:extension>\r\n        </xs:simpleContent>\r\n    </xs:complexType>\r\n    \r\n    <xs:element name='ProblemHeaderQName' type='wsa:AttributedQNameType'/>\r\n    <xs:complexType name='AttributedQNameType' mixed='false'>\r\n        <xs:simpleContent>\r\n            <xs:extension base='xs:QName'>\r\n                <xs:anyAttribute namespace='##other' processContents='lax'/>\r\n            </xs:extension>\r\n        </xs:simpleContent>\r\n    </xs:complexType>\r\n    \r\n    <xs:element name='ProblemHeader' type='wsa:AttributedAnyType'/>\r\n    <xs:complexType name='AttributedAnyType' mixed='false'>\r\n        <xs:sequence>\r\n            <xs:any namespace='##any' processContents='lax' minOccurs='1' maxOccurs='1'/>\r\n        </xs:sequence>\r\n        <xs:anyAttribute namespace='##other' processContents='lax'/>\r\n    </xs:complexType>\r\n    \r\n    <xs:element name='ProblemIRI' type='wsa:AttributedURIType'/>\r\n    \r\n    <xs:element name='ProblemAction' type='wsa:ProblemActionType'/>\r\n    <xs:complexType name='ProblemActionType' mixed='false'>\r\n        <xs:sequence>\r\n            <xs:element ref='wsa:Action' minOccurs='0'/>\r\n            <xs:element name='SoapAction' minOccurs='0' type='xs:anyURI'/>\r\n        </xs:sequence>\r\n        <xs:anyAttribute namespace='##other' processContents='lax'/>\r\n    </xs:complexType>\r\n    \r\n</xs:schema>"))
			{
				DtdProcessing = DtdProcessing.Prohibit
			})
			{
				result = XmlSchema.Read(xmlTextReader, null);
			}
			return result;
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x000203B0 File Offset: 0x0001E5B0
		public static XmlQualifiedName GetSchema(XmlSchemaSet xmlSchemaSet)
		{
			if (xmlSchemaSet == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("xmlSchemaSet");
			}
			XmlQualifiedName xmlQualifiedName = EndpointAddress10.EprType;
			XmlSchema eprSchema = EndpointAddress10.GetEprSchema();
			ICollection collection = xmlSchemaSet.Schemas("http://www.w3.org/2005/08/addressing");
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

		// Token: 0x060007AB RID: 1963 RVA: 0x00020504 File Offset: 0x0001E704
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x04000ADC RID: 2780
		private static XmlQualifiedName eprType;

		// Token: 0x04000ADD RID: 2781
		private EndpointAddress address;

		// Token: 0x04000ADE RID: 2782
		private const string Schema = "<xs:schema xmlns:xs='http://www.w3.org/2001/XMLSchema' xmlns:wsa='http://www.w3.org/2005/08/addressing' targetNamespace='http://www.w3.org/2005/08/addressing' blockDefault='#all' elementFormDefault='qualified' finalDefault='' attributeFormDefault='unqualified'>\r\n    \r\n    <!-- Constructs from the WS-Addressing Core -->\r\n\r\n    <xs:element name='EndpointReference' type='wsa:EndpointReferenceType'/>\r\n    <xs:complexType name='EndpointReferenceType' mixed='false'>\r\n        <xs:sequence>\r\n            <xs:element name='Address' type='wsa:AttributedURIType'/>\r\n            <xs:element name='ReferenceParameters' type='wsa:ReferenceParametersType' minOccurs='0'/>\r\n            <xs:element ref='wsa:Metadata' minOccurs='0'/>\r\n            <xs:any namespace='##other' processContents='lax' minOccurs='0' maxOccurs='unbounded'/>\r\n        </xs:sequence>\r\n        <xs:anyAttribute namespace='##other' processContents='lax'/>\r\n    </xs:complexType>\r\n    \r\n    <xs:complexType name='ReferenceParametersType' mixed='false'>\r\n        <xs:sequence>\r\n            <xs:any namespace='##any' processContents='lax' minOccurs='0' maxOccurs='unbounded'/>\r\n        </xs:sequence>\r\n        <xs:anyAttribute namespace='##other' processContents='lax'/>\r\n    </xs:complexType>\r\n    \r\n    <xs:element name='Metadata' type='wsa:MetadataType'/>\r\n    <xs:complexType name='MetadataType' mixed='false'>\r\n        <xs:sequence>\r\n            <xs:any namespace='##any' processContents='lax' minOccurs='0' maxOccurs='unbounded'/>\r\n        </xs:sequence>\r\n        <xs:anyAttribute namespace='##other' processContents='lax'/>\r\n    </xs:complexType>\r\n    \r\n    <xs:element name='MessageID' type='wsa:AttributedURIType'/>\r\n    <xs:element name='RelatesTo' type='wsa:RelatesToType'/>\r\n    <xs:complexType name='RelatesToType' mixed='false'>\r\n        <xs:simpleContent>\r\n            <xs:extension base='xs:anyURI'>\r\n                <xs:attribute name='RelationshipType' type='wsa:RelationshipTypeOpenEnum' use='optional' default='http://www.w3.org/2005/08/addressing/reply'/>\r\n                <xs:anyAttribute namespace='##other' processContents='lax'/>\r\n            </xs:extension>\r\n        </xs:simpleContent>\r\n    </xs:complexType>\r\n    \r\n    <xs:simpleType name='RelationshipTypeOpenEnum'>\r\n        <xs:union memberTypes='wsa:RelationshipType xs:anyURI'/>\r\n    </xs:simpleType>\r\n    \r\n    <xs:simpleType name='RelationshipType'>\r\n        <xs:restriction base='xs:anyURI'>\r\n            <xs:enumeration value='http://www.w3.org/2005/08/addressing/reply'/>\r\n        </xs:restriction>\r\n    </xs:simpleType>\r\n    \r\n    <xs:element name='ReplyTo' type='wsa:EndpointReferenceType'/>\r\n    <xs:element name='From' type='wsa:EndpointReferenceType'/>\r\n    <xs:element name='FaultTo' type='wsa:EndpointReferenceType'/>\r\n    <xs:element name='To' type='wsa:AttributedURIType'/>\r\n    <xs:element name='Action' type='wsa:AttributedURIType'/>\r\n\r\n    <xs:complexType name='AttributedURIType' mixed='false'>\r\n        <xs:simpleContent>\r\n            <xs:extension base='xs:anyURI'>\r\n                <xs:anyAttribute namespace='##other' processContents='lax'/>\r\n            </xs:extension>\r\n        </xs:simpleContent>\r\n    </xs:complexType>\r\n    \r\n    <!-- Constructs from the WS-Addressing SOAP binding -->\r\n\r\n    <xs:attribute name='IsReferenceParameter' type='xs:boolean'/>\r\n    \r\n    <xs:simpleType name='FaultCodesOpenEnumType'>\r\n        <xs:union memberTypes='wsa:FaultCodesType xs:QName'/>\r\n    </xs:simpleType>\r\n    \r\n    <xs:simpleType name='FaultCodesType'>\r\n        <xs:restriction base='xs:QName'>\r\n            <xs:enumeration value='wsa:InvalidAddressingHeader'/>\r\n            <xs:enumeration value='wsa:InvalidAddress'/>\r\n            <xs:enumeration value='wsa:InvalidEPR'/>\r\n            <xs:enumeration value='wsa:InvalidCardinality'/>\r\n            <xs:enumeration value='wsa:MissingAddressInEPR'/>\r\n            <xs:enumeration value='wsa:DuplicateMessageID'/>\r\n            <xs:enumeration value='wsa:ActionMismatch'/>\r\n            <xs:enumeration value='wsa:MessageAddressingHeaderRequired'/>\r\n            <xs:enumeration value='wsa:DestinationUnreachable'/>\r\n            <xs:enumeration value='wsa:ActionNotSupported'/>\r\n            <xs:enumeration value='wsa:EndpointUnavailable'/>\r\n        </xs:restriction>\r\n    </xs:simpleType>\r\n    \r\n    <xs:element name='RetryAfter' type='wsa:AttributedUnsignedLongType'/>\r\n    <xs:complexType name='AttributedUnsignedLongType' mixed='false'>\r\n        <xs:simpleContent>\r\n            <xs:extension base='xs:unsignedLong'>\r\n                <xs:anyAttribute namespace='##other' processContents='lax'/>\r\n            </xs:extension>\r\n        </xs:simpleContent>\r\n    </xs:complexType>\r\n    \r\n    <xs:element name='ProblemHeaderQName' type='wsa:AttributedQNameType'/>\r\n    <xs:complexType name='AttributedQNameType' mixed='false'>\r\n        <xs:simpleContent>\r\n            <xs:extension base='xs:QName'>\r\n                <xs:anyAttribute namespace='##other' processContents='lax'/>\r\n            </xs:extension>\r\n        </xs:simpleContent>\r\n    </xs:complexType>\r\n    \r\n    <xs:element name='ProblemHeader' type='wsa:AttributedAnyType'/>\r\n    <xs:complexType name='AttributedAnyType' mixed='false'>\r\n        <xs:sequence>\r\n            <xs:any namespace='##any' processContents='lax' minOccurs='1' maxOccurs='1'/>\r\n        </xs:sequence>\r\n        <xs:anyAttribute namespace='##other' processContents='lax'/>\r\n    </xs:complexType>\r\n    \r\n    <xs:element name='ProblemIRI' type='wsa:AttributedURIType'/>\r\n    \r\n    <xs:element name='ProblemAction' type='wsa:ProblemActionType'/>\r\n    <xs:complexType name='ProblemActionType' mixed='false'>\r\n        <xs:sequence>\r\n            <xs:element ref='wsa:Action' minOccurs='0'/>\r\n            <xs:element name='SoapAction' minOccurs='0' type='xs:anyURI'/>\r\n        </xs:sequence>\r\n        <xs:anyAttribute namespace='##other' processContents='lax'/>\r\n    </xs:complexType>\r\n    \r\n</xs:schema>";
	}
}
