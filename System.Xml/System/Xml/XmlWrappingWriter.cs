using System;

namespace System.Xml
{
	// Token: 0x02000073 RID: 115
	internal class XmlWrappingWriter : XmlWriter
	{
		// Token: 0x060004DC RID: 1244 RVA: 0x00015080 File Offset: 0x00014080
		internal XmlWrappingWriter(XmlWriter baseWriter)
		{
			this.Writer = baseWriter;
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x0001508F File Offset: 0x0001408F
		public override XmlWriterSettings Settings
		{
			get
			{
				return this.writer.Settings;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x0001509C File Offset: 0x0001409C
		public override WriteState WriteState
		{
			get
			{
				return this.writer.WriteState;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x000150A9 File Offset: 0x000140A9
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.writer.XmlSpace;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x000150B6 File Offset: 0x000140B6
		public override string XmlLang
		{
			get
			{
				return this.writer.XmlLang;
			}
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x000150C3 File Offset: 0x000140C3
		public override void WriteStartDocument()
		{
			this.writer.WriteStartDocument();
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000150D0 File Offset: 0x000140D0
		public override void WriteStartDocument(bool standalone)
		{
			this.writer.WriteStartDocument(standalone);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x000150DE File Offset: 0x000140DE
		public override void WriteEndDocument()
		{
			this.writer.WriteEndDocument();
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x000150EB File Offset: 0x000140EB
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.writer.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000150FD File Offset: 0x000140FD
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.writer.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0001510D File Offset: 0x0001410D
		public override void WriteEndElement()
		{
			this.writer.WriteEndElement();
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0001511A File Offset: 0x0001411A
		public override void WriteFullEndElement()
		{
			this.writer.WriteFullEndElement();
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00015127 File Offset: 0x00014127
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.writer.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00015137 File Offset: 0x00014137
		public override void WriteEndAttribute()
		{
			this.writer.WriteEndAttribute();
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00015144 File Offset: 0x00014144
		public override void WriteCData(string text)
		{
			this.writer.WriteCData(text);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00015152 File Offset: 0x00014152
		public override void WriteComment(string text)
		{
			this.writer.WriteComment(text);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00015160 File Offset: 0x00014160
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.writer.WriteProcessingInstruction(name, text);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001516F File Offset: 0x0001416F
		public override void WriteEntityRef(string name)
		{
			this.writer.WriteEntityRef(name);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0001517D File Offset: 0x0001417D
		public override void WriteCharEntity(char ch)
		{
			this.writer.WriteCharEntity(ch);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001518B File Offset: 0x0001418B
		public override void WriteWhitespace(string ws)
		{
			this.writer.WriteWhitespace(ws);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00015199 File Offset: 0x00014199
		public override void WriteString(string text)
		{
			this.writer.WriteString(text);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x000151A7 File Offset: 0x000141A7
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.writer.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x000151B6 File Offset: 0x000141B6
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.writer.WriteChars(buffer, index, count);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x000151C6 File Offset: 0x000141C6
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.writer.WriteRaw(buffer, index, count);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x000151D6 File Offset: 0x000141D6
		public override void WriteRaw(string data)
		{
			this.writer.WriteRaw(data);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x000151E4 File Offset: 0x000141E4
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.writer.WriteBase64(buffer, index, count);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x000151F4 File Offset: 0x000141F4
		public override void Close()
		{
			this.writer.Close();
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00015201 File Offset: 0x00014201
		public override void Flush()
		{
			this.writer.Flush();
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0001520E File Offset: 0x0001420E
		public override string LookupPrefix(string ns)
		{
			return this.writer.LookupPrefix(ns);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001521C File Offset: 0x0001421C
		public override void WriteValue(object value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001522A File Offset: 0x0001422A
		public override void WriteValue(string value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00015238 File Offset: 0x00014238
		public override void WriteValue(bool value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00015246 File Offset: 0x00014246
		public override void WriteValue(DateTime value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00015254 File Offset: 0x00014254
		public override void WriteValue(double value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00015262 File Offset: 0x00014262
		public override void WriteValue(float value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00015270 File Offset: 0x00014270
		public override void WriteValue(decimal value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001527E File Offset: 0x0001427E
		public override void WriteValue(int value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0001528C File Offset: 0x0001428C
		public override void WriteValue(long value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0001529A File Offset: 0x0001429A
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.writer).Dispose();
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x000152A7 File Offset: 0x000142A7
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x000152AF File Offset: 0x000142AF
		protected XmlWriter Writer
		{
			get
			{
				return this.writer;
			}
			set
			{
				this.writer = value;
			}
		}

		// Token: 0x040005F7 RID: 1527
		protected XmlWriter writer;
	}
}
