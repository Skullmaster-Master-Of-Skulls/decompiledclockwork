using System;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x02000051 RID: 81
	internal abstract class XmlRawWriter : XmlWriter
	{
		// Token: 0x06000265 RID: 613 RVA: 0x00009F4B File Offset: 0x00008F4B
		public override void WriteStartDocument()
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00009F5C File Offset: 0x00008F5C
		public override void WriteStartDocument(bool standalone)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00009F6D File Offset: 0x00008F6D
		public override void WriteEndDocument()
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00009F7E File Offset: 0x00008F7E
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00009F80 File Offset: 0x00008F80
		public override void WriteEndElement()
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00009F91 File Offset: 0x00008F91
		public override void WriteFullEndElement()
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00009FA2 File Offset: 0x00008FA2
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			if (this.base64Encoder == null)
			{
				this.base64Encoder = new XmlRawWriterBase64Encoder(this);
			}
			this.base64Encoder.Encode(buffer, index, count);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00009FC6 File Offset: 0x00008FC6
		public override string LookupPrefix(string ns)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00009FD7 File Offset: 0x00008FD7
		public override WriteState WriteState
		{
			get
			{
				throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00009FE8 File Offset: 0x00008FE8
		public override XmlSpace XmlSpace
		{
			get
			{
				throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00009FF9 File Offset: 0x00008FF9
		public override string XmlLang
		{
			get
			{
				throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000A00A File Offset: 0x0000900A
		public override void WriteNmToken(string name)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000A01B File Offset: 0x0000901B
		public override void WriteName(string name)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000A02C File Offset: 0x0000902C
		public override void WriteQualifiedName(string localName, string ns)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000A03D File Offset: 0x0000903D
		public override void WriteCData(string text)
		{
			this.WriteString(text);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000A048 File Offset: 0x00009048
		public override void WriteCharEntity(char ch)
		{
			this.WriteString(new string(new char[]
			{
				ch
			}));
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000A06C File Offset: 0x0000906C
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.WriteString(new string(new char[]
			{
				lowChar,
				highChar
			}));
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000A094 File Offset: 0x00009094
		public override void WriteWhitespace(string ws)
		{
			this.WriteString(ws);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000A09D File Offset: 0x0000909D
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count));
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000A0AD File Offset: 0x000090AD
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count));
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000A0BD File Offset: 0x000090BD
		public override void WriteRaw(string data)
		{
			this.WriteString(data);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000A0C6 File Offset: 0x000090C6
		public override void WriteValue(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.WriteString(XmlUntypedConverter.Untyped.ToString(value, this.resolver));
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000A0ED File Offset: 0x000090ED
		public override void WriteValue(string value)
		{
			this.WriteString(value);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000A0F6 File Offset: 0x000090F6
		public override void WriteAttributes(XmlReader reader, bool defattr)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000A107 File Offset: 0x00009107
		public override void WriteNode(XmlReader reader, bool defattr)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000A118 File Offset: 0x00009118
		public override void WriteNode(XPathNavigator navigator, bool defattr)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000A129 File Offset: 0x00009129
		// (set) Token: 0x06000280 RID: 640 RVA: 0x0000A131 File Offset: 0x00009131
		internal virtual IXmlNamespaceResolver NamespaceResolver
		{
			get
			{
				return this.resolver;
			}
			set
			{
				this.resolver = value;
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000A13A File Offset: 0x0000913A
		internal virtual void WriteXmlDeclaration(XmlStandalone standalone)
		{
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000A13C File Offset: 0x0000913C
		internal virtual void WriteXmlDeclaration(string xmldecl)
		{
		}

		// Token: 0x06000283 RID: 643
		internal abstract void StartElementContent();

		// Token: 0x06000284 RID: 644 RVA: 0x0000A13E File Offset: 0x0000913E
		internal virtual void OnRootElement(ConformanceLevel conformanceLevel)
		{
		}

		// Token: 0x06000285 RID: 645
		internal abstract void WriteEndElement(string prefix, string localName, string ns);

		// Token: 0x06000286 RID: 646 RVA: 0x0000A140 File Offset: 0x00009140
		internal virtual void WriteFullEndElement(string prefix, string localName, string ns)
		{
			this.WriteEndElement(prefix, localName, ns);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000A14B File Offset: 0x0000914B
		internal virtual void WriteQualifiedName(string prefix, string localName, string ns)
		{
			if (prefix.Length != 0)
			{
				this.WriteString(prefix);
				this.WriteString(":");
			}
			this.WriteString(localName);
		}

		// Token: 0x06000288 RID: 648
		internal abstract void WriteNamespaceDeclaration(string prefix, string ns);

		// Token: 0x06000289 RID: 649 RVA: 0x0000A16E File Offset: 0x0000916E
		internal virtual void WriteEndBase64()
		{
			this.base64Encoder.Flush();
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000A17B File Offset: 0x0000917B
		internal virtual void Close(WriteState currentState)
		{
			this.Close();
		}

		// Token: 0x04000525 RID: 1317
		internal const int SurHighStart = 55296;

		// Token: 0x04000526 RID: 1318
		internal const int SurHighEnd = 56319;

		// Token: 0x04000527 RID: 1319
		internal const int SurLowStart = 56320;

		// Token: 0x04000528 RID: 1320
		internal const int SurLowEnd = 57343;

		// Token: 0x04000529 RID: 1321
		internal const int SurMask = 64512;

		// Token: 0x0400052A RID: 1322
		protected XmlRawWriterBase64Encoder base64Encoder;

		// Token: 0x0400052B RID: 1323
		protected IXmlNamespaceResolver resolver;
	}
}
