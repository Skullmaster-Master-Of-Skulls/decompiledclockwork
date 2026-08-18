using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000070 RID: 112
	internal class SerializationDictionary
	{
		// Token: 0x06000270 RID: 624 RVA: 0x0000E974 File Offset: 0x0000CB74
		public SerializationDictionary(ServiceModelDictionary dictionary)
		{
			this.XmlSchemaInstanceNamespace = dictionary.CreateString("http://www.w3.org/2001/XMLSchema-instance", 441);
			this.XmlSchemaNamespace = dictionary.CreateString("http://www.w3.org/2001/XMLSchema", 442);
			this.Nil = dictionary.CreateString("nil", 443);
			this.Type = dictionary.CreateString("type", 444);
			this.Char = dictionary.CreateString("char", 445);
			this.Boolean = dictionary.CreateString("boolean", 446);
			this.Byte = dictionary.CreateString("byte", 447);
			this.UnsignedByte = dictionary.CreateString("unsignedByte", 448);
			this.Short = dictionary.CreateString("short", 449);
			this.UnsignedShort = dictionary.CreateString("unsignedShort", 450);
			this.Int = dictionary.CreateString("int", 451);
			this.UnsignedInt = dictionary.CreateString("unsignedInt", 452);
			this.Long = dictionary.CreateString("long", 453);
			this.UnsignedLong = dictionary.CreateString("unsignedLong", 454);
			this.Float = dictionary.CreateString("float", 455);
			this.Double = dictionary.CreateString("double", 456);
			this.Decimal = dictionary.CreateString("decimal", 457);
			this.DateTime = dictionary.CreateString("dateTime", 458);
			this.String = dictionary.CreateString("string", 459);
			this.Base64Binary = dictionary.CreateString("base64Binary", 460);
			this.AnyType = dictionary.CreateString("anyType", 461);
			this.Duration = dictionary.CreateString("duration", 462);
			this.Guid = dictionary.CreateString("guid", 463);
			this.AnyURI = dictionary.CreateString("anyURI", 464);
			this.QName = dictionary.CreateString("QName", 465);
			this.Time = dictionary.CreateString("time", 466);
			this.Date = dictionary.CreateString("date", 467);
			this.HexBinary = dictionary.CreateString("hexBinary", 468);
			this.GYearMonth = dictionary.CreateString("gYearMonth", 469);
			this.GYear = dictionary.CreateString("gYear", 470);
			this.GMonthDay = dictionary.CreateString("gMonthDay", 471);
			this.GDay = dictionary.CreateString("gDay", 472);
			this.GMonth = dictionary.CreateString("gMonth", 473);
			this.Integer = dictionary.CreateString("integer", 474);
			this.PositiveInteger = dictionary.CreateString("positiveInteger", 475);
			this.NegativeInteger = dictionary.CreateString("negativeInteger", 476);
			this.NonPositiveInteger = dictionary.CreateString("nonPositiveInteger", 477);
			this.NonNegativeInteger = dictionary.CreateString("nonNegativeInteger", 478);
			this.NormalizedString = dictionary.CreateString("normalizedString", 479);
		}

		// Token: 0x04000600 RID: 1536
		public XmlDictionaryString XmlSchemaInstanceNamespace;

		// Token: 0x04000601 RID: 1537
		public XmlDictionaryString XmlSchemaNamespace;

		// Token: 0x04000602 RID: 1538
		public XmlDictionaryString Nil;

		// Token: 0x04000603 RID: 1539
		public XmlDictionaryString Type;

		// Token: 0x04000604 RID: 1540
		public XmlDictionaryString Char;

		// Token: 0x04000605 RID: 1541
		public XmlDictionaryString Boolean;

		// Token: 0x04000606 RID: 1542
		public XmlDictionaryString Byte;

		// Token: 0x04000607 RID: 1543
		public XmlDictionaryString UnsignedByte;

		// Token: 0x04000608 RID: 1544
		public XmlDictionaryString Short;

		// Token: 0x04000609 RID: 1545
		public XmlDictionaryString UnsignedShort;

		// Token: 0x0400060A RID: 1546
		public XmlDictionaryString Int;

		// Token: 0x0400060B RID: 1547
		public XmlDictionaryString UnsignedInt;

		// Token: 0x0400060C RID: 1548
		public XmlDictionaryString Long;

		// Token: 0x0400060D RID: 1549
		public XmlDictionaryString UnsignedLong;

		// Token: 0x0400060E RID: 1550
		public XmlDictionaryString Float;

		// Token: 0x0400060F RID: 1551
		public XmlDictionaryString Double;

		// Token: 0x04000610 RID: 1552
		public XmlDictionaryString Decimal;

		// Token: 0x04000611 RID: 1553
		public XmlDictionaryString DateTime;

		// Token: 0x04000612 RID: 1554
		public XmlDictionaryString String;

		// Token: 0x04000613 RID: 1555
		public XmlDictionaryString Base64Binary;

		// Token: 0x04000614 RID: 1556
		public XmlDictionaryString AnyType;

		// Token: 0x04000615 RID: 1557
		public XmlDictionaryString Duration;

		// Token: 0x04000616 RID: 1558
		public XmlDictionaryString Guid;

		// Token: 0x04000617 RID: 1559
		public XmlDictionaryString AnyURI;

		// Token: 0x04000618 RID: 1560
		public XmlDictionaryString QName;

		// Token: 0x04000619 RID: 1561
		public XmlDictionaryString Time;

		// Token: 0x0400061A RID: 1562
		public XmlDictionaryString Date;

		// Token: 0x0400061B RID: 1563
		public XmlDictionaryString HexBinary;

		// Token: 0x0400061C RID: 1564
		public XmlDictionaryString GYearMonth;

		// Token: 0x0400061D RID: 1565
		public XmlDictionaryString GYear;

		// Token: 0x0400061E RID: 1566
		public XmlDictionaryString GMonthDay;

		// Token: 0x0400061F RID: 1567
		public XmlDictionaryString GDay;

		// Token: 0x04000620 RID: 1568
		public XmlDictionaryString GMonth;

		// Token: 0x04000621 RID: 1569
		public XmlDictionaryString Integer;

		// Token: 0x04000622 RID: 1570
		public XmlDictionaryString PositiveInteger;

		// Token: 0x04000623 RID: 1571
		public XmlDictionaryString NegativeInteger;

		// Token: 0x04000624 RID: 1572
		public XmlDictionaryString NonPositiveInteger;

		// Token: 0x04000625 RID: 1573
		public XmlDictionaryString NonNegativeInteger;

		// Token: 0x04000626 RID: 1574
		public XmlDictionaryString NormalizedString;
	}
}
