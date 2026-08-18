using System;
using System.IO;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000079 RID: 121
	internal class XmlEncodedRawTextWriterIndent : XmlEncodedRawTextWriter
	{
		// Token: 0x06000536 RID: 1334 RVA: 0x00015DB4 File Offset: 0x00014DB4
		public XmlEncodedRawTextWriterIndent(TextWriter writer, XmlWriterSettings settings) : base(writer, settings)
		{
			this.Init(settings);
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00015DC5 File Offset: 0x00014DC5
		public XmlEncodedRawTextWriterIndent(Stream stream, Encoding encoding, XmlWriterSettings settings, bool closeOutput) : base(stream, encoding, settings, closeOutput)
		{
			this.Init(settings);
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x00015DDC File Offset: 0x00014DDC
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

		// Token: 0x06000539 RID: 1337 RVA: 0x00015E1E File Offset: 0x00014E1E
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00015E48 File Offset: 0x00014E48
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

		// Token: 0x0600053B RID: 1339 RVA: 0x00015E99 File Offset: 0x00014E99
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

		// Token: 0x0600053C RID: 1340 RVA: 0x00015ECD File Offset: 0x00014ECD
		internal override void OnRootElement(ConformanceLevel currentConformanceLevel)
		{
			this.conformanceLevel = currentConformanceLevel;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00015ED8 File Offset: 0x00014ED8
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

		// Token: 0x0600053E RID: 1342 RVA: 0x00015F38 File Offset: 0x00014F38
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

		// Token: 0x0600053F RID: 1343 RVA: 0x00015F97 File Offset: 0x00014F97
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (this.newLineOnAttributes)
			{
				this.WriteIndent();
			}
			base.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00015FB0 File Offset: 0x00014FB0
		public override void WriteCData(string text)
		{
			this.mixedContent = true;
			base.WriteCData(text);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00015FC0 File Offset: 0x00014FC0
		public override void WriteComment(string text)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteComment(text);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00015FE5 File Offset: 0x00014FE5
		public override void WriteProcessingInstruction(string target, string text)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteProcessingInstruction(target, text);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001600B File Offset: 0x0001500B
		public override void WriteEntityRef(string name)
		{
			this.mixedContent = true;
			base.WriteEntityRef(name);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001601B File Offset: 0x0001501B
		public override void WriteCharEntity(char ch)
		{
			this.mixedContent = true;
			base.WriteCharEntity(ch);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0001602B File Offset: 0x0001502B
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.mixedContent = true;
			base.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0001603C File Offset: 0x0001503C
		public override void WriteWhitespace(string ws)
		{
			this.mixedContent = true;
			base.WriteWhitespace(ws);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0001604C File Offset: 0x0001504C
		public override void WriteString(string text)
		{
			this.mixedContent = true;
			base.WriteString(text);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001605C File Offset: 0x0001505C
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteChars(buffer, index, count);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0001606E File Offset: 0x0001506E
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteRaw(buffer, index, count);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00016080 File Offset: 0x00015080
		public override void WriteRaw(string data)
		{
			this.mixedContent = true;
			base.WriteRaw(data);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00016090 File Offset: 0x00015090
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteBase64(buffer, index, count);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x000160A4 File Offset: 0x000150A4
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

		// Token: 0x0600054D RID: 1357 RVA: 0x0001613C File Offset: 0x0001513C
		private void WriteIndent()
		{
			base.RawText(this.newLineChars);
			for (int i = this.indentLevel; i > 0; i--)
			{
				base.RawText(this.indentChars);
			}
		}

		// Token: 0x04000608 RID: 1544
		protected int indentLevel;

		// Token: 0x04000609 RID: 1545
		protected bool newLineOnAttributes;

		// Token: 0x0400060A RID: 1546
		protected string indentChars;

		// Token: 0x0400060B RID: 1547
		protected bool mixedContent;

		// Token: 0x0400060C RID: 1548
		private BitStack mixedContentStack;

		// Token: 0x0400060D RID: 1549
		protected ConformanceLevel conformanceLevel;
	}
}
