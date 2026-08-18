using System;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x020000B8 RID: 184
	internal class WrappedXmlDictionaryReader : XmlDictionaryReader, IXmlLineInfo
	{
		// Token: 0x0600059C RID: 1436 RVA: 0x000150C9 File Offset: 0x000132C9
		public WrappedXmlDictionaryReader(XmlReader reader, XmlDictionaryReaderQuotas xmlDictionaryReaderQuotas)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (xmlDictionaryReaderQuotas == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("xmlDictionaryReaderQuotas");
			}
			this.reader = reader;
			this.xmlDictionaryReaderQuotas = xmlDictionaryReaderQuotas;
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x00015105 File Offset: 0x00013305
		public override int AttributeCount
		{
			get
			{
				return this.reader.AttributeCount;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x00015112 File Offset: 0x00013312
		public override string BaseURI
		{
			get
			{
				return this.reader.BaseURI;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0001511F File Offset: 0x0001331F
		public override bool CanReadBinaryContent
		{
			get
			{
				return this.reader.CanReadBinaryContent;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0001512C File Offset: 0x0001332C
		public override bool CanReadValueChunk
		{
			get
			{
				return this.reader.CanReadValueChunk;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00015139 File Offset: 0x00013339
		public override int Depth
		{
			get
			{
				return this.reader.Depth;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x00015146 File Offset: 0x00013346
		public override bool EOF
		{
			get
			{
				return this.reader.EOF;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00015153 File Offset: 0x00013353
		public override bool HasValue
		{
			get
			{
				return this.reader.HasValue;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00015160 File Offset: 0x00013360
		public override bool IsDefault
		{
			get
			{
				return this.reader.IsDefault;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0001516D File Offset: 0x0001336D
		public override bool IsEmptyElement
		{
			get
			{
				return this.reader.IsEmptyElement;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0001517A File Offset: 0x0001337A
		public override string LocalName
		{
			get
			{
				return this.reader.LocalName;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00015187 File Offset: 0x00013387
		public override string Name
		{
			get
			{
				return this.reader.Name;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00015194 File Offset: 0x00013394
		public override string NamespaceURI
		{
			get
			{
				return this.reader.NamespaceURI;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x000151A1 File Offset: 0x000133A1
		public override XmlNameTable NameTable
		{
			get
			{
				return this.reader.NameTable;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x000151AE File Offset: 0x000133AE
		public override XmlNodeType NodeType
		{
			get
			{
				return this.reader.NodeType;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x000151BB File Offset: 0x000133BB
		public override string Prefix
		{
			get
			{
				return this.reader.Prefix;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x000151C8 File Offset: 0x000133C8
		public override char QuoteChar
		{
			get
			{
				return this.reader.QuoteChar;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x000151D5 File Offset: 0x000133D5
		public override ReadState ReadState
		{
			get
			{
				return this.reader.ReadState;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x000151E2 File Offset: 0x000133E2
		public override string Value
		{
			get
			{
				return this.reader.Value;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x000151EF File Offset: 0x000133EF
		public override string XmlLang
		{
			get
			{
				return this.reader.XmlLang;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x000151FC File Offset: 0x000133FC
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.reader.XmlSpace;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x00015209 File Offset: 0x00013409
		public override Type ValueType
		{
			get
			{
				return this.reader.ValueType;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x00015218 File Offset: 0x00013418
		public int LineNumber
		{
			get
			{
				IXmlLineInfo xmlLineInfo = this.reader as IXmlLineInfo;
				if (xmlLineInfo == null)
				{
					return 1;
				}
				return xmlLineInfo.LineNumber;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0001523C File Offset: 0x0001343C
		public int LinePosition
		{
			get
			{
				IXmlLineInfo xmlLineInfo = this.reader as IXmlLineInfo;
				if (xmlLineInfo == null)
				{
					return 1;
				}
				return xmlLineInfo.LinePosition;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x00015260 File Offset: 0x00013460
		public override XmlDictionaryReaderQuotas Quotas
		{
			get
			{
				return this.xmlDictionaryReaderQuotas;
			}
		}

		// Token: 0x1700014D RID: 333
		public override string this[int index]
		{
			get
			{
				return this.reader[index];
			}
		}

		// Token: 0x1700014E RID: 334
		public override string this[string name]
		{
			get
			{
				return this.reader[name];
			}
		}

		// Token: 0x1700014F RID: 335
		public override string this[string name, string namespaceUri]
		{
			get
			{
				return this.reader[name, namespaceUri];
			}
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00015293 File Offset: 0x00013493
		public override void Close()
		{
			this.reader.Close();
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x000152A0 File Offset: 0x000134A0
		public override string GetAttribute(int index)
		{
			return this.reader.GetAttribute(index);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x000152AE File Offset: 0x000134AE
		public override string GetAttribute(string name)
		{
			return this.reader.GetAttribute(name);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x000152BC File Offset: 0x000134BC
		public override string GetAttribute(string name, string namespaceUri)
		{
			return this.reader.GetAttribute(name, namespaceUri);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x000152CB File Offset: 0x000134CB
		public override bool IsStartElement(string name)
		{
			return this.reader.IsStartElement(name);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x000152D9 File Offset: 0x000134D9
		public override bool IsStartElement(string localName, string namespaceUri)
		{
			return this.reader.IsStartElement(localName, namespaceUri);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x000152E8 File Offset: 0x000134E8
		public override string LookupNamespace(string namespaceUri)
		{
			return this.reader.LookupNamespace(namespaceUri);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x000152F6 File Offset: 0x000134F6
		public override void MoveToAttribute(int index)
		{
			this.reader.MoveToAttribute(index);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00015304 File Offset: 0x00013504
		public override bool MoveToAttribute(string name)
		{
			return this.reader.MoveToAttribute(name);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00015312 File Offset: 0x00013512
		public override bool MoveToAttribute(string name, string namespaceUri)
		{
			return this.reader.MoveToAttribute(name, namespaceUri);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00015321 File Offset: 0x00013521
		public override bool MoveToElement()
		{
			return this.reader.MoveToElement();
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001532E File Offset: 0x0001352E
		public override bool MoveToFirstAttribute()
		{
			return this.reader.MoveToFirstAttribute();
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0001533B File Offset: 0x0001353B
		public override bool MoveToNextAttribute()
		{
			return this.reader.MoveToNextAttribute();
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00015348 File Offset: 0x00013548
		public override bool Read()
		{
			return this.reader.Read();
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00015355 File Offset: 0x00013555
		public override bool ReadAttributeValue()
		{
			return this.reader.ReadAttributeValue();
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00015362 File Offset: 0x00013562
		public override string ReadElementString(string name)
		{
			return this.reader.ReadElementString(name);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00015370 File Offset: 0x00013570
		public override string ReadElementString(string localName, string namespaceUri)
		{
			return this.reader.ReadElementString(localName, namespaceUri);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0001537F File Offset: 0x0001357F
		public override string ReadInnerXml()
		{
			return this.reader.ReadInnerXml();
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0001538C File Offset: 0x0001358C
		public override string ReadOuterXml()
		{
			return this.reader.ReadOuterXml();
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00015399 File Offset: 0x00013599
		public override void ReadStartElement(string name)
		{
			this.reader.ReadStartElement(name);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x000153A7 File Offset: 0x000135A7
		public override void ReadStartElement(string localName, string namespaceUri)
		{
			this.reader.ReadStartElement(localName, namespaceUri);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x000153B6 File Offset: 0x000135B6
		public override void ReadEndElement()
		{
			this.reader.ReadEndElement();
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x000153C3 File Offset: 0x000135C3
		public override string ReadString()
		{
			return this.reader.ReadString();
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x000153D0 File Offset: 0x000135D0
		public override void ResolveEntity()
		{
			this.reader.ResolveEntity();
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x000153DD File Offset: 0x000135DD
		public override int ReadElementContentAsBase64(byte[] buffer, int offset, int count)
		{
			return this.reader.ReadElementContentAsBase64(buffer, offset, count);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x000153ED File Offset: 0x000135ED
		public override int ReadContentAsBase64(byte[] buffer, int offset, int count)
		{
			return this.reader.ReadContentAsBase64(buffer, offset, count);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x000153FD File Offset: 0x000135FD
		public override int ReadElementContentAsBinHex(byte[] buffer, int offset, int count)
		{
			return this.reader.ReadElementContentAsBinHex(buffer, offset, count);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0001540D File Offset: 0x0001360D
		public override int ReadContentAsBinHex(byte[] buffer, int offset, int count)
		{
			return this.reader.ReadContentAsBinHex(buffer, offset, count);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0001541D File Offset: 0x0001361D
		public override int ReadValueChunk(char[] chars, int offset, int count)
		{
			return this.reader.ReadValueChunk(chars, offset, count);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0001542D File Offset: 0x0001362D
		public override bool ReadContentAsBoolean()
		{
			return this.reader.ReadContentAsBoolean();
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0001543A File Offset: 0x0001363A
		public override DateTime ReadContentAsDateTime()
		{
			return this.reader.ReadContentAsDateTime();
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00015447 File Offset: 0x00013647
		public override decimal ReadContentAsDecimal()
		{
			return (decimal)this.reader.ReadContentAs(typeof(decimal), null);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00015464 File Offset: 0x00013664
		public override double ReadContentAsDouble()
		{
			return this.reader.ReadContentAsDouble();
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00015471 File Offset: 0x00013671
		public override int ReadContentAsInt()
		{
			return this.reader.ReadContentAsInt();
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0001547E File Offset: 0x0001367E
		public override long ReadContentAsLong()
		{
			return this.reader.ReadContentAsLong();
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001548B File Offset: 0x0001368B
		public override float ReadContentAsFloat()
		{
			return this.reader.ReadContentAsFloat();
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00015498 File Offset: 0x00013698
		public override string ReadContentAsString()
		{
			return this.reader.ReadContentAsString();
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x000154A5 File Offset: 0x000136A5
		public override object ReadContentAs(Type valueType, IXmlNamespaceResolver namespaceResolver)
		{
			return this.reader.ReadContentAs(valueType, namespaceResolver);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x000154B4 File Offset: 0x000136B4
		public bool HasLineInfo()
		{
			IXmlLineInfo xmlLineInfo = this.reader as IXmlLineInfo;
			return xmlLineInfo != null && xmlLineInfo.HasLineInfo();
		}

		// Token: 0x040004DC RID: 1244
		private XmlReader reader;

		// Token: 0x040004DD RID: 1245
		private XmlDictionaryReaderQuotas xmlDictionaryReaderQuotas;
	}
}
