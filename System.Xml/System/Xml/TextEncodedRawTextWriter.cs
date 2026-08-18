using System;
using System.IO;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000067 RID: 103
	internal class TextEncodedRawTextWriter : XmlEncodedRawTextWriter
	{
		// Token: 0x06000384 RID: 900 RVA: 0x00011CBC File Offset: 0x00010CBC
		public TextEncodedRawTextWriter(TextWriter writer, XmlWriterSettings settings) : base(writer, settings)
		{
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00011CC6 File Offset: 0x00010CC6
		public TextEncodedRawTextWriter(Stream stream, Encoding encoding, XmlWriterSettings settings, bool closeOutput) : base(stream, encoding, settings, closeOutput)
		{
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00011CD3 File Offset: 0x00010CD3
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00011CD5 File Offset: 0x00010CD5
		internal override void WriteXmlDeclaration(string xmldecl)
		{
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00011CD7 File Offset: 0x00010CD7
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00011CD9 File Offset: 0x00010CD9
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00011CDB File Offset: 0x00010CDB
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00011CDD File Offset: 0x00010CDD
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00011CDF File Offset: 0x00010CDF
		internal override void StartElementContent()
		{
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00011CE1 File Offset: 0x00010CE1
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.inAttributeValue = true;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00011CEA File Offset: 0x00010CEA
		public override void WriteEndAttribute()
		{
			this.inAttributeValue = false;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00011CF3 File Offset: 0x00010CF3
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00011CF5 File Offset: 0x00010CF5
		public override void WriteCData(string text)
		{
			base.WriteRaw(text);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00011CFE File Offset: 0x00010CFE
		public override void WriteComment(string text)
		{
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00011D00 File Offset: 0x00010D00
		public override void WriteProcessingInstruction(string name, string text)
		{
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00011D02 File Offset: 0x00010D02
		public override void WriteEntityRef(string name)
		{
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00011D04 File Offset: 0x00010D04
		public override void WriteCharEntity(char ch)
		{
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00011D06 File Offset: 0x00010D06
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00011D08 File Offset: 0x00010D08
		public override void WriteWhitespace(string ws)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(ws);
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00011D19 File Offset: 0x00010D19
		public override void WriteString(string textBlock)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(textBlock);
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00011D2A File Offset: 0x00010D2A
		public override void WriteChars(char[] buffer, int index, int count)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(buffer, index, count);
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00011D3D File Offset: 0x00010D3D
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(buffer, index, count);
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00011D50 File Offset: 0x00010D50
		public override void WriteRaw(string data)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(data);
			}
		}
	}
}
