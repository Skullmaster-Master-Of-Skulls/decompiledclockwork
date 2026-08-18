using System;
using System.IO;

namespace System.Xml
{
	// Token: 0x020000CA RID: 202
	internal class XmlAutoDetectWriter : XmlRawWriter, IRemovableWriter
	{
		// Token: 0x060007B1 RID: 1969 RVA: 0x00019245 File Offset: 0x00017445
		private XmlAutoDetectWriter(XmlWriterSettings writerSettings)
		{
			this.writerSettings = writerSettings.Clone();
			this.writerSettings.ReadOnly = true;
			this.eventCache = new XmlEventCache(string.Empty, true);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00019276 File Offset: 0x00017476
		public XmlAutoDetectWriter(TextWriter textWriter, XmlWriterSettings writerSettings) : this(writerSettings)
		{
			this.textWriter = textWriter;
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00019286 File Offset: 0x00017486
		public XmlAutoDetectWriter(Stream strm, XmlWriterSettings writerSettings) : this(writerSettings)
		{
			this.strm = strm;
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00019296 File Offset: 0x00017496
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x0001929E File Offset: 0x0001749E
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

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x000192A7 File Offset: 0x000174A7
		public override XmlWriterSettings Settings
		{
			get
			{
				return this.writerSettings;
			}
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x000192AF File Offset: 0x000174AF
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x000192C8 File Offset: 0x000174C8
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

		// Token: 0x060007B9 RID: 1977 RVA: 0x00019300 File Offset: 0x00017500
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00019317 File Offset: 0x00017517
		public override void WriteEndAttribute()
		{
			this.wrapped.WriteEndAttribute();
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00019324 File Offset: 0x00017524
		public override void WriteCData(string text)
		{
			if (this.TextBlockCreatesWriter(text))
			{
				this.wrapped.WriteCData(text);
				return;
			}
			this.eventCache.WriteCData(text);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00019348 File Offset: 0x00017548
		public override void WriteComment(string text)
		{
			if (this.wrapped == null)
			{
				this.eventCache.WriteComment(text);
				return;
			}
			this.wrapped.WriteComment(text);
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0001936B File Offset: 0x0001756B
		public override void WriteProcessingInstruction(string name, string text)
		{
			if (this.wrapped == null)
			{
				this.eventCache.WriteProcessingInstruction(name, text);
				return;
			}
			this.wrapped.WriteProcessingInstruction(name, text);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00019390 File Offset: 0x00017590
		public override void WriteWhitespace(string ws)
		{
			if (this.wrapped == null)
			{
				this.eventCache.WriteWhitespace(ws);
				return;
			}
			this.wrapped.WriteWhitespace(ws);
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x000193B3 File Offset: 0x000175B3
		public override void WriteString(string text)
		{
			if (this.TextBlockCreatesWriter(text))
			{
				this.wrapped.WriteString(text);
				return;
			}
			this.eventCache.WriteString(text);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x000193D7 File Offset: 0x000175D7
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count));
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x000193E7 File Offset: 0x000175E7
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.WriteRaw(new string(buffer, index, count));
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x000193F7 File Offset: 0x000175F7
		public override void WriteRaw(string data)
		{
			if (this.TextBlockCreatesWriter(data))
			{
				this.wrapped.WriteRaw(data);
				return;
			}
			this.eventCache.WriteRaw(data);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0001941B File Offset: 0x0001761B
		public override void WriteEntityRef(string name)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteEntityRef(name);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00019430 File Offset: 0x00017630
		public override void WriteCharEntity(char ch)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteCharEntity(ch);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00019445 File Offset: 0x00017645
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0001945B File Offset: 0x0001765B
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteBase64(buffer, index, count);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00019472 File Offset: 0x00017672
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteBinHex(buffer, index, count);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00019489 File Offset: 0x00017689
		public override void Close()
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.Close();
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0001949D File Offset: 0x0001769D
		public override void Flush()
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.Flush();
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x000194B1 File Offset: 0x000176B1
		public override void WriteValue(object value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x000194C6 File Offset: 0x000176C6
		public override void WriteValue(string value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x000194DB File Offset: 0x000176DB
		public override void WriteValue(bool value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x000194F0 File Offset: 0x000176F0
		public override void WriteValue(DateTime value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00019505 File Offset: 0x00017705
		public override void WriteValue(DateTimeOffset value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0001951A File Offset: 0x0001771A
		public override void WriteValue(double value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0001952F File Offset: 0x0001772F
		public override void WriteValue(float value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00019544 File Offset: 0x00017744
		public override void WriteValue(decimal value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00019559 File Offset: 0x00017759
		public override void WriteValue(int value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0001956E File Offset: 0x0001776E
		public override void WriteValue(long value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x00019583 File Offset: 0x00017783
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x0001958B File Offset: 0x0001778B
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

		// Token: 0x060007D6 RID: 2006 RVA: 0x000195B5 File Offset: 0x000177B5
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteXmlDeclaration(standalone);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x000195CA File Offset: 0x000177CA
		internal override void WriteXmlDeclaration(string xmldecl)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteXmlDeclaration(xmldecl);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x000195DF File Offset: 0x000177DF
		internal override void StartElementContent()
		{
			this.wrapped.StartElementContent();
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x000195EC File Offset: 0x000177EC
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			this.wrapped.WriteEndElement(prefix, localName, ns);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x000195FC File Offset: 0x000177FC
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			this.wrapped.WriteFullEndElement(prefix, localName, ns);
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0001960C File Offset: 0x0001780C
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteNamespaceDeclaration(prefix, ns);
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x00019622 File Offset: 0x00017822
		internal override bool SupportsNamespaceDeclarationInChunks
		{
			get
			{
				return this.wrapped.SupportsNamespaceDeclarationInChunks;
			}
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0001962F File Offset: 0x0001782F
		internal override void WriteStartNamespaceDeclaration(string prefix)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteStartNamespaceDeclaration(prefix);
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00019644 File Offset: 0x00017844
		internal override void WriteEndNamespaceDeclaration()
		{
			this.wrapped.WriteEndNamespaceDeclaration();
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00019654 File Offset: 0x00017854
		private static bool IsHtmlTag(string tagName)
		{
			return tagName.Length == 4 && (tagName[0] == 'H' || tagName[0] == 'h') && (tagName[1] == 'T' || tagName[1] == 't') && (tagName[2] == 'M' || tagName[2] == 'm') && (tagName[3] == 'L' || tagName[3] == 'l');
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x000196CD File Offset: 0x000178CD
		private void EnsureWrappedWriter(XmlOutputMethod outMethod)
		{
			if (this.wrapped == null)
			{
				this.CreateWrappedWriter(outMethod);
			}
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x000196E0 File Offset: 0x000178E0
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

		// Token: 0x060007E2 RID: 2018 RVA: 0x00019710 File Offset: 0x00017910
		private void CreateWrappedWriter(XmlOutputMethod outMethod)
		{
			this.writerSettings.ReadOnly = false;
			this.writerSettings.OutputMethod = outMethod;
			if (outMethod == XmlOutputMethod.Html && this.writerSettings.IndentInternal == TriState.Unknown)
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

		// Token: 0x040002E2 RID: 738
		private XmlRawWriter wrapped;

		// Token: 0x040002E3 RID: 739
		private OnRemoveWriter onRemove;

		// Token: 0x040002E4 RID: 740
		private XmlWriterSettings writerSettings;

		// Token: 0x040002E5 RID: 741
		private XmlEventCache eventCache;

		// Token: 0x040002E6 RID: 742
		private TextWriter textWriter;

		// Token: 0x040002E7 RID: 743
		private Stream strm;
	}
}
