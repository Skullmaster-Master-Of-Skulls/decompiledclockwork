using System;
using System.IO;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000068 RID: 104
	internal class TextUtf8RawTextWriter : XmlUtf8RawTextWriter
	{
		// Token: 0x0600039B RID: 923 RVA: 0x00011D61 File Offset: 0x00010D61
		public TextUtf8RawTextWriter(Stream stream, Encoding encoding, XmlWriterSettings settings, bool closeOutput) : base(stream, encoding, settings, closeOutput)
		{
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00011D6E File Offset: 0x00010D6E
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00011D70 File Offset: 0x00010D70
		internal override void WriteXmlDeclaration(string xmldecl)
		{
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00011D72 File Offset: 0x00010D72
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00011D74 File Offset: 0x00010D74
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00011D76 File Offset: 0x00010D76
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00011D78 File Offset: 0x00010D78
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00011D7A File Offset: 0x00010D7A
		internal override void StartElementContent()
		{
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00011D7C File Offset: 0x00010D7C
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.inAttributeValue = true;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00011D85 File Offset: 0x00010D85
		public override void WriteEndAttribute()
		{
			this.inAttributeValue = false;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00011D8E File Offset: 0x00010D8E
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00011D90 File Offset: 0x00010D90
		public override void WriteCData(string text)
		{
			base.WriteRaw(text);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00011D99 File Offset: 0x00010D99
		public override void WriteComment(string text)
		{
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00011D9B File Offset: 0x00010D9B
		public override void WriteProcessingInstruction(string name, string text)
		{
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00011D9D File Offset: 0x00010D9D
		public override void WriteEntityRef(string name)
		{
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00011D9F File Offset: 0x00010D9F
		public override void WriteCharEntity(char ch)
		{
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00011DA1 File Offset: 0x00010DA1
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00011DA3 File Offset: 0x00010DA3
		public override void WriteWhitespace(string ws)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(ws);
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00011DB4 File Offset: 0x00010DB4
		public override void WriteString(string textBlock)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(textBlock);
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00011DC5 File Offset: 0x00010DC5
		public override void WriteChars(char[] buffer, int index, int count)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(buffer, index, count);
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00011DD8 File Offset: 0x00010DD8
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(buffer, index, count);
			}
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00011DEB File Offset: 0x00010DEB
		public override void WriteRaw(string data)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(data);
			}
		}
	}
}
