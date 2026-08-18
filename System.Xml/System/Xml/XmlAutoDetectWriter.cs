using System;
using System.IO;
using System.Text;

namespace System.Xml
{
	// Token: 0x0200006C RID: 108
	internal class XmlAutoDetectWriter : XmlRawWriter, IRemovableWriter
	{
		// Token: 0x060003CE RID: 974 RVA: 0x0001202B File Offset: 0x0001102B
		private XmlAutoDetectWriter(XmlWriterSettings writerSettings, Encoding encoding)
		{
			this.writerSettings = writerSettings.Clone();
			this.writerSettings.Encoding = encoding;
			this.writerSettings.ReadOnly = true;
			this.eventCache = new XmlEventCache(string.Empty, true);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00012068 File Offset: 0x00011068
		public XmlAutoDetectWriter(TextWriter textWriter, XmlWriterSettings writerSettings) : this(writerSettings, textWriter.Encoding)
		{
			this.textWriter = textWriter;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0001207E File Offset: 0x0001107E
		public XmlAutoDetectWriter(Stream strm, Encoding encoding, XmlWriterSettings writerSettings) : this(writerSettings, encoding)
		{
			this.strm = strm;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0001208F File Offset: 0x0001108F
		// (set) Token: 0x060003D2 RID: 978 RVA: 0x00012097 File Offset: 0x00011097
		public OnRemoveWriter OnRemoveWriterEvent
		{
			get
			{
				return this.onRemove;
			}
			set
			{
				this.onRemove = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x000120A0 File Offset: 0x000110A0
		public override XmlWriterSettings Settings
		{
			get
			{
				return this.writerSettings;
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x000120A8 File Offset: 0x000110A8
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x000120C1 File Offset: 0x000110C1
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			if (this.wrapped == null)
			{
				if (ns.Length == 0 && XmlAutoDetectWriter.IsHtmlTag(localName))
				{
					this.CreateWrappedWriter(XmlOutputMethod.Html);
				}
				else
				{
					this.CreateWrappedWriter(XmlOutputMethod.Xml);
				}
			}
			this.wrapped.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x000120F9 File Offset: 0x000110F9
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00012110 File Offset: 0x00011110
		public override void WriteEndAttribute()
		{
			this.wrapped.WriteEndAttribute();
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0001211D File Offset: 0x0001111D
		public override void WriteCData(string text)
		{
			if (this.TextBlockCreatesWriter(text))
			{
				this.wrapped.WriteCData(text);
				return;
			}
			this.eventCache.WriteCData(text);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00012141 File Offset: 0x00011141
		public override void WriteComment(string text)
		{
			if (this.wrapped == null)
			{
				this.eventCache.WriteComment(text);
				return;
			}
			this.wrapped.WriteComment(text);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00012164 File Offset: 0x00011164
		public override void WriteProcessingInstruction(string name, string text)
		{
			if (this.wrapped == null)
			{
				this.eventCache.WriteProcessingInstruction(name, text);
				return;
			}
			this.wrapped.WriteProcessingInstruction(name, text);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00012189 File Offset: 0x00011189
		public override void WriteWhitespace(string ws)
		{
			if (this.wrapped == null)
			{
				this.eventCache.WriteWhitespace(ws);
				return;
			}
			this.wrapped.WriteWhitespace(ws);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x000121AC File Offset: 0x000111AC
		public override void WriteString(string text)
		{
			if (this.TextBlockCreatesWriter(text))
			{
				this.wrapped.WriteString(text);
				return;
			}
			this.eventCache.WriteString(text);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x000121D0 File Offset: 0x000111D0
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count));
		}

		// Token: 0x060003DE RID: 990 RVA: 0x000121E0 File Offset: 0x000111E0
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.WriteRaw(new string(buffer, index, count));
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000121F0 File Offset: 0x000111F0
		public override void WriteRaw(string data)
		{
			if (this.TextBlockCreatesWriter(data))
			{
				this.wrapped.WriteRaw(data);
				return;
			}
			this.eventCache.WriteRaw(data);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00012214 File Offset: 0x00011214
		public override void WriteEntityRef(string name)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteEntityRef(name);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00012229 File Offset: 0x00011229
		public override void WriteCharEntity(char ch)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteCharEntity(ch);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0001223E File Offset: 0x0001123E
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00012254 File Offset: 0x00011254
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteBase64(buffer, index, count);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0001226B File Offset: 0x0001126B
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteBinHex(buffer, index, count);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00012282 File Offset: 0x00011282
		public override void Close()
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.Close();
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00012296 File Offset: 0x00011296
		public override void Flush()
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.Flush();
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000122AA File Offset: 0x000112AA
		public override void WriteValue(object value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x000122BF File Offset: 0x000112BF
		public override void WriteValue(string value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x000122D4 File Offset: 0x000112D4
		public override void WriteValue(bool value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x000122E9 File Offset: 0x000112E9
		public override void WriteValue(DateTime value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000122FE File Offset: 0x000112FE
		public override void WriteValue(double value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00012313 File Offset: 0x00011313
		public override void WriteValue(float value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00012328 File Offset: 0x00011328
		public override void WriteValue(decimal value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001233D File Offset: 0x0001133D
		public override void WriteValue(int value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00012352 File Offset: 0x00011352
		public override void WriteValue(long value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00012367 File Offset: 0x00011367
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x0001236F File Offset: 0x0001136F
		internal override IXmlNamespaceResolver NamespaceResolver
		{
			get
			{
				return this.resolver;
			}
			set
			{
				this.resolver = value;
				if (this.wrapped == null)
				{
					this.eventCache.NamespaceResolver = value;
					return;
				}
				this.wrapped.NamespaceResolver = value;
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00012399 File Offset: 0x00011399
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteXmlDeclaration(standalone);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x000123AE File Offset: 0x000113AE
		internal override void WriteXmlDeclaration(string xmldecl)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteXmlDeclaration(xmldecl);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x000123C3 File Offset: 0x000113C3
		internal override void StartElementContent()
		{
			this.wrapped.StartElementContent();
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x000123D0 File Offset: 0x000113D0
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			this.wrapped.WriteEndElement(prefix, localName, ns);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x000123E0 File Offset: 0x000113E0
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			this.wrapped.WriteFullEndElement(prefix, localName, ns);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x000123F0 File Offset: 0x000113F0
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteNamespaceDeclaration(prefix, ns);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00012408 File Offset: 0x00011408
		private static bool IsHtmlTag(string tagName)
		{
			return tagName.Length == 4 && (tagName[0] == 'H' || tagName[0] == 'h') && (tagName[1] == 'T' || tagName[1] == 't') && (tagName[2] == 'M' || tagName[2] == 'm') && (tagName[3] == 'L' || tagName[3] == 'l');
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00012481 File Offset: 0x00011481
		private void EnsureWrappedWriter(XmlOutputMethod outMethod)
		{
			if (this.wrapped == null)
			{
				this.CreateWrappedWriter(outMethod);
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00012494 File Offset: 0x00011494
		private bool TextBlockCreatesWriter(string textBlock)
		{
			if (this.wrapped == null)
			{
				if (XmlCharType.Instance.IsOnlyWhitespace(textBlock))
				{
					return false;
				}
				this.CreateWrappedWriter(XmlOutputMethod.Xml);
			}
			return true;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x000124C4 File Offset: 0x000114C4
		private void CreateWrappedWriter(XmlOutputMethod outMethod)
		{
			this.writerSettings.ReadOnly = false;
			this.writerSettings.OutputMethod = outMethod;
			if (outMethod == XmlOutputMethod.Html && this.writerSettings.InternalIndent == TriState.Unknown)
			{
				this.writerSettings.Indent = true;
			}
			this.writerSettings.ReadOnly = true;
			if (this.textWriter != null)
			{
				this.wrapped = ((XmlWellFormedWriter)XmlWriter.Create(this.textWriter, this.writerSettings)).RawWriter;
			}
			else
			{
				this.wrapped = ((XmlWellFormedWriter)XmlWriter.Create(this.strm, this.writerSettings)).RawWriter;
			}
			this.eventCache.EndEvents();
			this.eventCache.EventsToWriter(this.wrapped);
			if (this.onRemove != null)
			{
				this.onRemove(this.wrapped);
			}
		}

		// Token: 0x040005D9 RID: 1497
		private XmlRawWriter wrapped;

		// Token: 0x040005DA RID: 1498
		private OnRemoveWriter onRemove;

		// Token: 0x040005DB RID: 1499
		private XmlWriterSettings writerSettings;

		// Token: 0x040005DC RID: 1500
		private XmlEventCache eventCache;

		// Token: 0x040005DD RID: 1501
		private TextWriter textWriter;

		// Token: 0x040005DE RID: 1502
		private Stream strm;
	}
}
