using System;
using System.IO;

namespace System.Xml
{
	// Token: 0x020000C0 RID: 192
	internal class TextUtf8RawTextWriter : XmlUtf8RawTextWriter
	{
		// Token: 0x0600069F RID: 1695 RVA: 0x00017A0C File Offset: 0x00015C0C
		public TextUtf8RawTextWriter(Stream stream, XmlWriterSettings settings) : base(stream, settings)
		{
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00017A16 File Offset: 0x00015C16
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00017A18 File Offset: 0x00015C18
		internal override void WriteXmlDeclaration(string xmldecl)
		{
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00017A1A File Offset: 0x00015C1A
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00017A1C File Offset: 0x00015C1C
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00017A1E File Offset: 0x00015C1E
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00017A20 File Offset: 0x00015C20
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00017A22 File Offset: 0x00015C22
		internal override void StartElementContent()
		{
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00017A24 File Offset: 0x00015C24
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.inAttributeValue = true;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00017A2D File Offset: 0x00015C2D
		public override void WriteEndAttribute()
		{
			this.inAttributeValue = false;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00017A36 File Offset: 0x00015C36
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x00017A38 File Offset: 0x00015C38
		internal override bool SupportsNamespaceDeclarationInChunks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00017A3B File Offset: 0x00015C3B
		public override void WriteCData(string text)
		{
			base.WriteRaw(text);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00017A44 File Offset: 0x00015C44
		public override void WriteComment(string text)
		{
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00017A46 File Offset: 0x00015C46
		public override void WriteProcessingInstruction(string name, string text)
		{
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00017A48 File Offset: 0x00015C48
		public override void WriteEntityRef(string name)
		{
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00017A4A File Offset: 0x00015C4A
		public override void WriteCharEntity(char ch)
		{
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00017A4C File Offset: 0x00015C4C
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00017A4E File Offset: 0x00015C4E
		public override void WriteWhitespace(string ws)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(ws);
			}
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00017A5F File Offset: 0x00015C5F
		public override void WriteString(string textBlock)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(textBlock);
			}
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00017A70 File Offset: 0x00015C70
		public override void WriteChars(char[] buffer, int index, int count)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(buffer, index, count);
			}
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00017A83 File Offset: 0x00015C83
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(buffer, index, count);
			}
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00017A96 File Offset: 0x00015C96
		public override void WriteRaw(string data)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(data);
			}
		}
	}
}
