using System;
using System.IO;
using System.Text;

namespace System.Xml
{
	// Token: 0x0200009B RID: 155
	internal class XmlUtf8RawTextWriterIndent : XmlUtf8RawTextWriter
	{
		// Token: 0x06000858 RID: 2136 RVA: 0x00027571 File Offset: 0x00026571
		public XmlUtf8RawTextWriterIndent(Stream stream, Encoding encoding, XmlWriterSettings settings, bool closeOutput) : base(stream, encoding, settings, closeOutput)
		{
			this.Init(settings);
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x00027588 File Offset: 0x00026588
		public override XmlWriterSettings Settings
		{
			get
			{
				XmlWriterSettings settings = base.Settings;
				settings.ReadOnly = false;
				settings.Indent = true;
				settings.IndentChars = this.indentChars;
				settings.NewLineOnAttributes = this.newLineOnAttributes;
				settings.ReadOnly = true;
				return settings;
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x000275CA File Offset: 0x000265CA
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x000275F4 File Offset: 0x000265F4
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			this.indentLevel++;
			this.mixedContentStack.PushBit(this.mixedContent);
			base.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00027645 File Offset: 0x00026645
		internal override void StartElementContent()
		{
			if (this.indentLevel == 1 && this.conformanceLevel == ConformanceLevel.Document)
			{
				this.mixedContent = false;
			}
			else
			{
				this.mixedContent = this.mixedContentStack.PeekBit();
			}
			base.StartElementContent();
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00027679 File Offset: 0x00026679
		internal override void OnRootElement(ConformanceLevel currentConformanceLevel)
		{
			this.conformanceLevel = currentConformanceLevel;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00027684 File Offset: 0x00026684
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			this.indentLevel--;
			if (!this.mixedContent && this.contentPos != this.bufPos && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			this.mixedContent = this.mixedContentStack.PopBit();
			base.WriteEndElement(prefix, localName, ns);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x000276E4 File Offset: 0x000266E4
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			this.indentLevel--;
			if (!this.mixedContent && this.contentPos != this.bufPos && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			this.mixedContent = this.mixedContentStack.PopBit();
			base.WriteFullEndElement(prefix, localName, ns);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00027743 File Offset: 0x00026743
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (this.newLineOnAttributes)
			{
				this.WriteIndent();
			}
			base.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0002775C File Offset: 0x0002675C
		public override void WriteCData(string text)
		{
			this.mixedContent = true;
			base.WriteCData(text);
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0002776C File Offset: 0x0002676C
		public override void WriteComment(string text)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteComment(text);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00027791 File Offset: 0x00026791
		public override void WriteProcessingInstruction(string target, string text)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteProcessingInstruction(target, text);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x000277B7 File Offset: 0x000267B7
		public override void WriteEntityRef(string name)
		{
			this.mixedContent = true;
			base.WriteEntityRef(name);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x000277C7 File Offset: 0x000267C7
		public override void WriteCharEntity(char ch)
		{
			this.mixedContent = true;
			base.WriteCharEntity(ch);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x000277D7 File Offset: 0x000267D7
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.mixedContent = true;
			base.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000277E8 File Offset: 0x000267E8
		public override void WriteWhitespace(string ws)
		{
			this.mixedContent = true;
			base.WriteWhitespace(ws);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x000277F8 File Offset: 0x000267F8
		public override void WriteString(string text)
		{
			this.mixedContent = true;
			base.WriteString(text);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00027808 File Offset: 0x00026808
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteChars(buffer, index, count);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0002781A File Offset: 0x0002681A
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteRaw(buffer, index, count);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0002782C File Offset: 0x0002682C
		public override void WriteRaw(string data)
		{
			this.mixedContent = true;
			base.WriteRaw(data);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0002783C File Offset: 0x0002683C
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteBase64(buffer, index, count);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00027850 File Offset: 0x00026850
		private void Init(XmlWriterSettings settings)
		{
			this.indentLevel = 0;
			this.indentChars = settings.IndentChars;
			this.newLineOnAttributes = settings.NewLineOnAttributes;
			this.mixedContentStack = new BitStack();
			if (this.checkCharacters)
			{
				if (this.newLineOnAttributes)
				{
					base.ValidateContentChars(this.indentChars, "IndentChars", true);
					base.ValidateContentChars(this.newLineChars, "NewLineChars", true);
					return;
				}
				base.ValidateContentChars(this.indentChars, "IndentChars", false);
				if (this.newLineHandling != NewLineHandling.Replace)
				{
					base.ValidateContentChars(this.newLineChars, "NewLineChars", false);
				}
			}
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x000278E8 File Offset: 0x000268E8
		private void WriteIndent()
		{
			base.RawText(this.newLineChars);
			for (int i = this.indentLevel; i > 0; i--)
			{
				base.RawText(this.indentChars);
			}
		}

		// Token: 0x0400079D RID: 1949
		protected int indentLevel;

		// Token: 0x0400079E RID: 1950
		protected bool newLineOnAttributes;

		// Token: 0x0400079F RID: 1951
		protected string indentChars;

		// Token: 0x040007A0 RID: 1952
		protected bool mixedContent;

		// Token: 0x040007A1 RID: 1953
		private BitStack mixedContentStack;

		// Token: 0x040007A2 RID: 1954
		protected ConformanceLevel conformanceLevel;
	}
}
