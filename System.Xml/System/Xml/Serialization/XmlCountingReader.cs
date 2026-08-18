using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x02000309 RID: 777
	internal class XmlCountingReader : XmlReader, IXmlTextParser, IXmlLineInfo
	{
		// Token: 0x06002469 RID: 9321 RVA: 0x000ACCF3 File Offset: 0x000ABCF3
		internal XmlCountingReader(XmlReader xmlReader)
		{
			if (xmlReader == null)
			{
				throw new ArgumentNullException("xmlReader");
			}
			this.innerReader = xmlReader;
			this.advanceCount = 0;
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x0600246A RID: 9322 RVA: 0x000ACD17 File Offset: 0x000ABD17
		internal int AdvanceCount
		{
			get
			{
				return this.advanceCount;
			}
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x000ACD1F File Offset: 0x000ABD1F
		private void IncrementCount()
		{
			if (this.advanceCount == 2147483647)
			{
				this.advanceCount = 0;
				return;
			}
			this.advanceCount++;
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x0600246C RID: 9324 RVA: 0x000ACD44 File Offset: 0x000ABD44
		public override XmlReaderSettings Settings
		{
			get
			{
				return this.innerReader.Settings;
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x0600246D RID: 9325 RVA: 0x000ACD51 File Offset: 0x000ABD51
		public override XmlNodeType NodeType
		{
			get
			{
				return this.innerReader.NodeType;
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x0600246E RID: 9326 RVA: 0x000ACD5E File Offset: 0x000ABD5E
		public override string Name
		{
			get
			{
				return this.innerReader.Name;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x0600246F RID: 9327 RVA: 0x000ACD6B File Offset: 0x000ABD6B
		public override string LocalName
		{
			get
			{
				return this.innerReader.LocalName;
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06002470 RID: 9328 RVA: 0x000ACD78 File Offset: 0x000ABD78
		public override string NamespaceURI
		{
			get
			{
				return this.innerReader.NamespaceURI;
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06002471 RID: 9329 RVA: 0x000ACD85 File Offset: 0x000ABD85
		public override string Prefix
		{
			get
			{
				return this.innerReader.Prefix;
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06002472 RID: 9330 RVA: 0x000ACD92 File Offset: 0x000ABD92
		public override bool HasValue
		{
			get
			{
				return this.innerReader.HasValue;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06002473 RID: 9331 RVA: 0x000ACD9F File Offset: 0x000ABD9F
		public override string Value
		{
			get
			{
				return this.innerReader.Value;
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06002474 RID: 9332 RVA: 0x000ACDAC File Offset: 0x000ABDAC
		public override int Depth
		{
			get
			{
				return this.innerReader.Depth;
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06002475 RID: 9333 RVA: 0x000ACDB9 File Offset: 0x000ABDB9
		public override string BaseURI
		{
			get
			{
				return this.innerReader.BaseURI;
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06002476 RID: 9334 RVA: 0x000ACDC6 File Offset: 0x000ABDC6
		public override bool IsEmptyElement
		{
			get
			{
				return this.innerReader.IsEmptyElement;
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06002477 RID: 9335 RVA: 0x000ACDD3 File Offset: 0x000ABDD3
		public override bool IsDefault
		{
			get
			{
				return this.innerReader.IsDefault;
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06002478 RID: 9336 RVA: 0x000ACDE0 File Offset: 0x000ABDE0
		public override char QuoteChar
		{
			get
			{
				return this.innerReader.QuoteChar;
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06002479 RID: 9337 RVA: 0x000ACDED File Offset: 0x000ABDED
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.innerReader.XmlSpace;
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x0600247A RID: 9338 RVA: 0x000ACDFA File Offset: 0x000ABDFA
		public override string XmlLang
		{
			get
			{
				return this.innerReader.XmlLang;
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x0600247B RID: 9339 RVA: 0x000ACE07 File Offset: 0x000ABE07
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.innerReader.SchemaInfo;
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x0600247C RID: 9340 RVA: 0x000ACE14 File Offset: 0x000ABE14
		public override Type ValueType
		{
			get
			{
				return this.innerReader.ValueType;
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x0600247D RID: 9341 RVA: 0x000ACE21 File Offset: 0x000ABE21
		public override int AttributeCount
		{
			get
			{
				return this.innerReader.AttributeCount;
			}
		}

		// Token: 0x17000902 RID: 2306
		public override string this[int i]
		{
			get
			{
				return this.innerReader[i];
			}
		}

		// Token: 0x17000903 RID: 2307
		public override string this[string name]
		{
			get
			{
				return this.innerReader[name];
			}
		}

		// Token: 0x17000904 RID: 2308
		public override string this[string name, string namespaceURI]
		{
			get
			{
				return this.innerReader[name, namespaceURI];
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06002481 RID: 9345 RVA: 0x000ACE59 File Offset: 0x000ABE59
		public override bool EOF
		{
			get
			{
				return this.innerReader.EOF;
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06002482 RID: 9346 RVA: 0x000ACE66 File Offset: 0x000ABE66
		public override ReadState ReadState
		{
			get
			{
				return this.innerReader.ReadState;
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06002483 RID: 9347 RVA: 0x000ACE73 File Offset: 0x000ABE73
		public override XmlNameTable NameTable
		{
			get
			{
				return this.innerReader.NameTable;
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06002484 RID: 9348 RVA: 0x000ACE80 File Offset: 0x000ABE80
		public override bool CanResolveEntity
		{
			get
			{
				return this.innerReader.CanResolveEntity;
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06002485 RID: 9349 RVA: 0x000ACE8D File Offset: 0x000ABE8D
		public override bool CanReadBinaryContent
		{
			get
			{
				return this.innerReader.CanReadBinaryContent;
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06002486 RID: 9350 RVA: 0x000ACE9A File Offset: 0x000ABE9A
		public override bool CanReadValueChunk
		{
			get
			{
				return this.innerReader.CanReadValueChunk;
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06002487 RID: 9351 RVA: 0x000ACEA7 File Offset: 0x000ABEA7
		public override bool HasAttributes
		{
			get
			{
				return this.innerReader.HasAttributes;
			}
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x000ACEB4 File Offset: 0x000ABEB4
		public override void Close()
		{
			this.innerReader.Close();
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x000ACEC1 File Offset: 0x000ABEC1
		public override string GetAttribute(string name)
		{
			return this.innerReader.GetAttribute(name);
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x000ACECF File Offset: 0x000ABECF
		public override string GetAttribute(string name, string namespaceURI)
		{
			return this.innerReader.GetAttribute(name, namespaceURI);
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x000ACEDE File Offset: 0x000ABEDE
		public override string GetAttribute(int i)
		{
			return this.innerReader.GetAttribute(i);
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x000ACEEC File Offset: 0x000ABEEC
		public override bool MoveToAttribute(string name)
		{
			return this.innerReader.MoveToAttribute(name);
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x000ACEFA File Offset: 0x000ABEFA
		public override bool MoveToAttribute(string name, string ns)
		{
			return this.innerReader.MoveToAttribute(name, ns);
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x000ACF09 File Offset: 0x000ABF09
		public override void MoveToAttribute(int i)
		{
			this.innerReader.MoveToAttribute(i);
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x000ACF17 File Offset: 0x000ABF17
		public override bool MoveToFirstAttribute()
		{
			return this.innerReader.MoveToFirstAttribute();
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x000ACF24 File Offset: 0x000ABF24
		public override bool MoveToNextAttribute()
		{
			return this.innerReader.MoveToNextAttribute();
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x000ACF31 File Offset: 0x000ABF31
		public override bool MoveToElement()
		{
			return this.innerReader.MoveToElement();
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x000ACF3E File Offset: 0x000ABF3E
		public override string LookupNamespace(string prefix)
		{
			return this.innerReader.LookupNamespace(prefix);
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x000ACF4C File Offset: 0x000ABF4C
		public override bool ReadAttributeValue()
		{
			return this.innerReader.ReadAttributeValue();
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x000ACF59 File Offset: 0x000ABF59
		public override void ResolveEntity()
		{
			this.innerReader.ResolveEntity();
		}

		// Token: 0x06002495 RID: 9365 RVA: 0x000ACF66 File Offset: 0x000ABF66
		public override bool IsStartElement()
		{
			return this.innerReader.IsStartElement();
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x000ACF73 File Offset: 0x000ABF73
		public override bool IsStartElement(string name)
		{
			return this.innerReader.IsStartElement(name);
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x000ACF81 File Offset: 0x000ABF81
		public override bool IsStartElement(string localname, string ns)
		{
			return this.innerReader.IsStartElement(localname, ns);
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x000ACF90 File Offset: 0x000ABF90
		public override XmlReader ReadSubtree()
		{
			return this.innerReader.ReadSubtree();
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x000ACF9D File Offset: 0x000ABF9D
		public override XmlNodeType MoveToContent()
		{
			return this.innerReader.MoveToContent();
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x000ACFAA File Offset: 0x000ABFAA
		public override bool Read()
		{
			this.IncrementCount();
			return this.innerReader.Read();
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x000ACFBD File Offset: 0x000ABFBD
		public override void Skip()
		{
			this.IncrementCount();
			this.innerReader.Skip();
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x000ACFD0 File Offset: 0x000ABFD0
		public override string ReadInnerXml()
		{
			if (this.innerReader.NodeType != XmlNodeType.Attribute)
			{
				this.IncrementCount();
			}
			return this.innerReader.ReadInnerXml();
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x000ACFF1 File Offset: 0x000ABFF1
		public override string ReadOuterXml()
		{
			if (this.innerReader.NodeType != XmlNodeType.Attribute)
			{
				this.IncrementCount();
			}
			return this.innerReader.ReadOuterXml();
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x000AD012 File Offset: 0x000AC012
		public override object ReadContentAsObject()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsObject();
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x000AD025 File Offset: 0x000AC025
		public override bool ReadContentAsBoolean()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsBoolean();
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x000AD038 File Offset: 0x000AC038
		public override DateTime ReadContentAsDateTime()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsDateTime();
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x000AD04B File Offset: 0x000AC04B
		public override double ReadContentAsDouble()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsDouble();
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x000AD05E File Offset: 0x000AC05E
		public override int ReadContentAsInt()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsInt();
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x000AD071 File Offset: 0x000AC071
		public override long ReadContentAsLong()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsLong();
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x000AD084 File Offset: 0x000AC084
		public override string ReadContentAsString()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsString();
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x000AD097 File Offset: 0x000AC097
		public override object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAs(returnType, namespaceResolver);
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x000AD0AC File Offset: 0x000AC0AC
		public override object ReadElementContentAsObject()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsObject();
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x000AD0BF File Offset: 0x000AC0BF
		public override object ReadElementContentAsObject(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsObject(localName, namespaceURI);
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x000AD0D4 File Offset: 0x000AC0D4
		public override bool ReadElementContentAsBoolean()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsBoolean();
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x000AD0E7 File Offset: 0x000AC0E7
		public override bool ReadElementContentAsBoolean(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsBoolean(localName, namespaceURI);
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x000AD0FC File Offset: 0x000AC0FC
		public override DateTime ReadElementContentAsDateTime()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsDateTime();
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x000AD10F File Offset: 0x000AC10F
		public override DateTime ReadElementContentAsDateTime(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsDateTime(localName, namespaceURI);
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x000AD124 File Offset: 0x000AC124
		public override double ReadElementContentAsDouble()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsDouble();
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x000AD137 File Offset: 0x000AC137
		public override double ReadElementContentAsDouble(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsDouble(localName, namespaceURI);
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x000AD14C File Offset: 0x000AC14C
		public override int ReadElementContentAsInt()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsInt();
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x000AD15F File Offset: 0x000AC15F
		public override int ReadElementContentAsInt(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsInt(localName, namespaceURI);
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x000AD174 File Offset: 0x000AC174
		public override long ReadElementContentAsLong()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsLong();
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x000AD187 File Offset: 0x000AC187
		public override long ReadElementContentAsLong(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsLong(localName, namespaceURI);
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x000AD19C File Offset: 0x000AC19C
		public override string ReadElementContentAsString()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsString();
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x000AD1AF File Offset: 0x000AC1AF
		public override string ReadElementContentAsString(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsString(localName, namespaceURI);
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x000AD1C4 File Offset: 0x000AC1C4
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAs(returnType, namespaceResolver);
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x000AD1D9 File Offset: 0x000AC1D9
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver, string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAs(returnType, namespaceResolver, localName, namespaceURI);
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x000AD1F1 File Offset: 0x000AC1F1
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsBase64(buffer, index, count);
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x000AD207 File Offset: 0x000AC207
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsBase64(buffer, index, count);
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x000AD21D File Offset: 0x000AC21D
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsBinHex(buffer, index, count);
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x000AD233 File Offset: 0x000AC233
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsBinHex(buffer, index, count);
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x000AD249 File Offset: 0x000AC249
		public override int ReadValueChunk(char[] buffer, int index, int count)
		{
			this.IncrementCount();
			return this.innerReader.ReadValueChunk(buffer, index, count);
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x000AD25F File Offset: 0x000AC25F
		public override string ReadString()
		{
			this.IncrementCount();
			return this.innerReader.ReadString();
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x000AD272 File Offset: 0x000AC272
		public override void ReadStartElement()
		{
			this.IncrementCount();
			this.innerReader.ReadStartElement();
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x000AD285 File Offset: 0x000AC285
		public override void ReadStartElement(string name)
		{
			this.IncrementCount();
			this.innerReader.ReadStartElement(name);
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x000AD299 File Offset: 0x000AC299
		public override void ReadStartElement(string localname, string ns)
		{
			this.IncrementCount();
			this.innerReader.ReadStartElement(localname, ns);
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x000AD2AE File Offset: 0x000AC2AE
		public override string ReadElementString()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementString();
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x000AD2C1 File Offset: 0x000AC2C1
		public override string ReadElementString(string name)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementString(name);
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x000AD2D5 File Offset: 0x000AC2D5
		public override string ReadElementString(string localname, string ns)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementString(localname, ns);
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x000AD2EA File Offset: 0x000AC2EA
		public override void ReadEndElement()
		{
			this.IncrementCount();
			this.innerReader.ReadEndElement();
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x000AD2FD File Offset: 0x000AC2FD
		public override bool ReadToFollowing(string name)
		{
			this.IncrementCount();
			return this.ReadToFollowing(name);
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x000AD30C File Offset: 0x000AC30C
		public override bool ReadToFollowing(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadToFollowing(localName, namespaceURI);
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x000AD321 File Offset: 0x000AC321
		public override bool ReadToDescendant(string name)
		{
			this.IncrementCount();
			return this.innerReader.ReadToDescendant(name);
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x000AD335 File Offset: 0x000AC335
		public override bool ReadToDescendant(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadToDescendant(localName, namespaceURI);
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x000AD34A File Offset: 0x000AC34A
		public override bool ReadToNextSibling(string name)
		{
			this.IncrementCount();
			return this.innerReader.ReadToNextSibling(name);
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x000AD35E File Offset: 0x000AC35E
		public override bool ReadToNextSibling(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadToNextSibling(localName, namespaceURI);
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x000AD374 File Offset: 0x000AC374
		protected override void Dispose(bool disposing)
		{
			try
			{
				IDisposable disposable = this.innerReader;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x060024CA RID: 9418 RVA: 0x000AD3AC File Offset: 0x000AC3AC
		// (set) Token: 0x060024CB RID: 9419 RVA: 0x000AD3E8 File Offset: 0x000AC3E8
		bool IXmlTextParser.Normalized
		{
			get
			{
				XmlTextReader xmlTextReader = this.innerReader as XmlTextReader;
				if (xmlTextReader == null)
				{
					IXmlTextParser xmlTextParser = this.innerReader as IXmlTextParser;
					return xmlTextParser != null && xmlTextParser.Normalized;
				}
				return xmlTextReader.Normalization;
			}
			set
			{
				XmlTextReader xmlTextReader = this.innerReader as XmlTextReader;
				if (xmlTextReader == null)
				{
					IXmlTextParser xmlTextParser = this.innerReader as IXmlTextParser;
					if (xmlTextParser != null)
					{
						xmlTextParser.Normalized = value;
						return;
					}
				}
				else
				{
					xmlTextReader.Normalization = value;
				}
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x060024CC RID: 9420 RVA: 0x000AD424 File Offset: 0x000AC424
		// (set) Token: 0x060024CD RID: 9421 RVA: 0x000AD460 File Offset: 0x000AC460
		WhitespaceHandling IXmlTextParser.WhitespaceHandling
		{
			get
			{
				XmlTextReader xmlTextReader = this.innerReader as XmlTextReader;
				if (xmlTextReader != null)
				{
					return xmlTextReader.WhitespaceHandling;
				}
				IXmlTextParser xmlTextParser = this.innerReader as IXmlTextParser;
				if (xmlTextParser != null)
				{
					return xmlTextParser.WhitespaceHandling;
				}
				return WhitespaceHandling.None;
			}
			set
			{
				XmlTextReader xmlTextReader = this.innerReader as XmlTextReader;
				if (xmlTextReader == null)
				{
					IXmlTextParser xmlTextParser = this.innerReader as IXmlTextParser;
					if (xmlTextParser != null)
					{
						xmlTextParser.WhitespaceHandling = value;
						return;
					}
				}
				else
				{
					xmlTextReader.WhitespaceHandling = value;
				}
			}
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x000AD49C File Offset: 0x000AC49C
		bool IXmlLineInfo.HasLineInfo()
		{
			IXmlLineInfo xmlLineInfo = this.innerReader as IXmlLineInfo;
			return xmlLineInfo != null && xmlLineInfo.HasLineInfo();
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x060024CF RID: 9423 RVA: 0x000AD4C0 File Offset: 0x000AC4C0
		int IXmlLineInfo.LineNumber
		{
			get
			{
				IXmlLineInfo xmlLineInfo = this.innerReader as IXmlLineInfo;
				if (xmlLineInfo != null)
				{
					return xmlLineInfo.LineNumber;
				}
				return 0;
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x060024D0 RID: 9424 RVA: 0x000AD4E4 File Offset: 0x000AC4E4
		int IXmlLineInfo.LinePosition
		{
			get
			{
				IXmlLineInfo xmlLineInfo = this.innerReader as IXmlLineInfo;
				if (xmlLineInfo != null)
				{
					return xmlLineInfo.LinePosition;
				}
				return 0;
			}
		}

		// Token: 0x04001571 RID: 5489
		private XmlReader innerReader;

		// Token: 0x04001572 RID: 5490
		private int advanceCount;
	}
}
