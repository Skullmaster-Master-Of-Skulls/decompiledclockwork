using System;
using System.IO;

namespace System.Xml
{
	// Token: 0x020000A1 RID: 161
	internal class HtmlEncodedRawTextWriterIndent : HtmlEncodedRawTextWriter
	{
		// Token: 0x06000595 RID: 1429 RVA: 0x000150FD File Offset: 0x000132FD
		public HtmlEncodedRawTextWriterIndent(TextWriter writer, XmlWriterSettings settings) : base(writer, settings)
		{
			this.Init(settings);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001510E File Offset: 0x0001330E
		public HtmlEncodedRawTextWriterIndent(Stream stream, XmlWriterSettings settings) : base(stream, settings)
		{
			this.Init(settings);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0001511F File Offset: 0x0001331F
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			base.WriteDocType(name, pubid, sysid, subset);
			this.endBlockPos = this.bufPos;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00015138 File Offset: 0x00013338
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				base.ChangeTextContentMark(false);
			}
			this.elementScope.Push((byte)this.currentElementProperties);
			if (ns.Length == 0)
			{
				this.currentElementProperties = (ElementProperties)HtmlEncodedRawTextWriter.elementPropertySearch.FindCaseInsensitiveString(localName);
				if (this.endBlockPos == this.bufPos && (this.currentElementProperties & ElementProperties.BLOCK_WS) != ElementProperties.DEFAULT)
				{
					this.WriteIndent();
				}
				this.indentLevel++;
				char[] bufChars = this.bufChars;
				int bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufChars[bufPos] = 60;
			}
			else
			{
				this.currentElementProperties = (ElementProperties)192U;
				if (this.endBlockPos == this.bufPos)
				{
					this.WriteIndent();
				}
				this.indentLevel++;
				char[] bufChars2 = this.bufChars;
				int bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufChars2[bufPos] = 60;
				if (prefix.Length != 0)
				{
					base.RawText(prefix);
					char[] bufChars3 = this.bufChars;
					bufPos = this.bufPos;
					this.bufPos = bufPos + 1;
					bufChars3[bufPos] = 58;
				}
			}
			base.RawText(localName);
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00015254 File Offset: 0x00013454
		internal override void StartElementContent()
		{
			char[] bufChars = this.bufChars;
			int bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufChars[bufPos] = 62;
			this.contentPos = this.bufPos;
			if ((this.currentElementProperties & ElementProperties.HEAD) != ElementProperties.DEFAULT)
			{
				this.WriteIndent();
				base.WriteMetaElement();
				this.endBlockPos = this.bufPos;
				return;
			}
			if ((this.currentElementProperties & ElementProperties.BLOCK_WS) != ElementProperties.DEFAULT)
			{
				this.endBlockPos = this.bufPos;
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x000152C4 File Offset: 0x000134C4
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			this.indentLevel--;
			bool flag = (this.currentElementProperties & ElementProperties.BLOCK_WS) > ElementProperties.DEFAULT;
			if (flag && this.endBlockPos == this.bufPos && this.contentPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteEndElement(prefix, localName, ns);
			this.contentPos = 0;
			if (flag)
			{
				this.endBlockPos = this.bufPos;
			}
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00015330 File Offset: 0x00013530
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (this.newLineOnAttributes)
			{
				base.RawText(this.newLineChars);
				this.indentLevel++;
				this.WriteIndent();
				this.indentLevel--;
			}
			base.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0001537C File Offset: 0x0001357C
		protected override void FlushBuffer()
		{
			this.endBlockPos = ((this.endBlockPos == this.bufPos) ? 1 : 0);
			base.FlushBuffer();
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0001539C File Offset: 0x0001359C
		private void Init(XmlWriterSettings settings)
		{
			this.indentLevel = 0;
			this.indentChars = settings.IndentChars;
			this.newLineOnAttributes = settings.NewLineOnAttributes;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x000153C0 File Offset: 0x000135C0
		private void WriteIndent()
		{
			base.RawText(this.newLineChars);
			for (int i = this.indentLevel; i > 0; i--)
			{
				base.RawText(this.indentChars);
			}
		}

		// Token: 0x04000268 RID: 616
		private int indentLevel;

		// Token: 0x04000269 RID: 617
		private int endBlockPos;

		// Token: 0x0400026A RID: 618
		private string indentChars;

		// Token: 0x0400026B RID: 619
		private bool newLineOnAttributes;
	}
}
