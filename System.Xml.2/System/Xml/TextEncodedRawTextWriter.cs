using System;
using System.IO;

namespace System.Xml
{
	// Token: 0x020000BF RID: 191
	internal class TextEncodedRawTextWriter : XmlEncodedRawTextWriter
	{
		// Token: 0x06000687 RID: 1671 RVA: 0x00017967 File Offset: 0x00015B67
		public TextEncodedRawTextWriter(TextWriter writer, XmlWriterSettings settings) : base(writer, settings)
		{
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00017971 File Offset: 0x00015B71
		public TextEncodedRawTextWriter(Stream stream, XmlWriterSettings settings) : base(stream, settings)
		{
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001797B File Offset: 0x00015B7B
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001797D File Offset: 0x00015B7D
		internal override void WriteXmlDeclaration(string xmldecl)
		{
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001797F File Offset: 0x00015B7F
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00017981 File Offset: 0x00015B81
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00017983 File Offset: 0x00015B83
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00017985 File Offset: 0x00015B85
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00017987 File Offset: 0x00015B87
		internal override void StartElementContent()
		{
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00017989 File Offset: 0x00015B89
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.inAttributeValue = true;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00017992 File Offset: 0x00015B92
		public override void WriteEndAttribute()
		{
			this.inAttributeValue = false;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0001799B File Offset: 0x00015B9B
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x0001799D File Offset: 0x00015B9D
		internal override bool SupportsNamespaceDeclarationInChunks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x000179A0 File Offset: 0x00015BA0
		public override void WriteCData(string text)
		{
			base.WriteRaw(text);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x000179A9 File Offset: 0x00015BA9
		public override void WriteComment(string text)
		{
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x000179AB File Offset: 0x00015BAB
		public override void WriteProcessingInstruction(string name, string text)
		{
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x000179AD File Offset: 0x00015BAD
		public override void WriteEntityRef(string name)
		{
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x000179AF File Offset: 0x00015BAF
		public override void WriteCharEntity(char ch)
		{
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x000179B1 File Offset: 0x00015BB1
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x000179B3 File Offset: 0x00015BB3
		public override void WriteWhitespace(string ws)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(ws);
			}
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x000179C4 File Offset: 0x00015BC4
		public override void WriteString(string textBlock)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(textBlock);
			}
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x000179D5 File Offset: 0x00015BD5
		public override void WriteChars(char[] buffer, int index, int count)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(buffer, index, count);
			}
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x000179E8 File Offset: 0x00015BE8
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(buffer, index, count);
			}
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x000179FB File Offset: 0x00015BFB
		public override void WriteRaw(string data)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(data);
			}
		}
	}
}
