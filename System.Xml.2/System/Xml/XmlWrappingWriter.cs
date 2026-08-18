using System;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x020000E3 RID: 227
	internal class XmlWrappingWriter : XmlWriter
	{
		// Token: 0x06000ECB RID: 3787 RVA: 0x0003FDE0 File Offset: 0x0003DFE0
		internal XmlWrappingWriter(XmlWriter baseWriter)
		{
			this.writer = baseWriter;
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x0003FDEF File Offset: 0x0003DFEF
		public override XmlWriterSettings Settings
		{
			get
			{
				return this.writer.Settings;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x0003FDFC File Offset: 0x0003DFFC
		public override WriteState WriteState
		{
			get
			{
				return this.writer.WriteState;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x0003FE09 File Offset: 0x0003E009
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.writer.XmlSpace;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x0003FE16 File Offset: 0x0003E016
		public override string XmlLang
		{
			get
			{
				return this.writer.XmlLang;
			}
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0003FE23 File Offset: 0x0003E023
		public override void WriteStartDocument()
		{
			this.writer.WriteStartDocument();
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0003FE30 File Offset: 0x0003E030
		public override void WriteStartDocument(bool standalone)
		{
			this.writer.WriteStartDocument(standalone);
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0003FE3E File Offset: 0x0003E03E
		public override void WriteEndDocument()
		{
			this.writer.WriteEndDocument();
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0003FE4B File Offset: 0x0003E04B
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.writer.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0003FE5D File Offset: 0x0003E05D
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.writer.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0003FE6D File Offset: 0x0003E06D
		public override void WriteEndElement()
		{
			this.writer.WriteEndElement();
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0003FE7A File Offset: 0x0003E07A
		public override void WriteFullEndElement()
		{
			this.writer.WriteFullEndElement();
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0003FE87 File Offset: 0x0003E087
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.writer.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0003FE97 File Offset: 0x0003E097
		public override void WriteEndAttribute()
		{
			this.writer.WriteEndAttribute();
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0003FEA4 File Offset: 0x0003E0A4
		public override void WriteCData(string text)
		{
			this.writer.WriteCData(text);
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0003FEB2 File Offset: 0x0003E0B2
		public override void WriteComment(string text)
		{
			this.writer.WriteComment(text);
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0003FEC0 File Offset: 0x0003E0C0
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.writer.WriteProcessingInstruction(name, text);
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0003FECF File Offset: 0x0003E0CF
		public override void WriteEntityRef(string name)
		{
			this.writer.WriteEntityRef(name);
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0003FEDD File Offset: 0x0003E0DD
		public override void WriteCharEntity(char ch)
		{
			this.writer.WriteCharEntity(ch);
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0003FEEB File Offset: 0x0003E0EB
		public override void WriteWhitespace(string ws)
		{
			this.writer.WriteWhitespace(ws);
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0003FEF9 File Offset: 0x0003E0F9
		public override void WriteString(string text)
		{
			this.writer.WriteString(text);
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0003FF07 File Offset: 0x0003E107
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.writer.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0003FF16 File Offset: 0x0003E116
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.writer.WriteChars(buffer, index, count);
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0003FF26 File Offset: 0x0003E126
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.writer.WriteRaw(buffer, index, count);
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0003FF36 File Offset: 0x0003E136
		public override void WriteRaw(string data)
		{
			this.writer.WriteRaw(data);
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0003FF44 File Offset: 0x0003E144
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.writer.WriteBase64(buffer, index, count);
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0003FF54 File Offset: 0x0003E154
		public override void Close()
		{
			this.writer.Close();
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0003FF61 File Offset: 0x0003E161
		public override void Flush()
		{
			this.writer.Flush();
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0003FF6E File Offset: 0x0003E16E
		public override string LookupPrefix(string ns)
		{
			return this.writer.LookupPrefix(ns);
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0003FF7C File Offset: 0x0003E17C
		public override void WriteValue(object value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0003FF8A File Offset: 0x0003E18A
		public override void WriteValue(string value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0003FF98 File Offset: 0x0003E198
		public override void WriteValue(bool value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0003FFA6 File Offset: 0x0003E1A6
		public override void WriteValue(DateTime value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0003FFB4 File Offset: 0x0003E1B4
		public override void WriteValue(DateTimeOffset value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x0003FFC2 File Offset: 0x0003E1C2
		public override void WriteValue(double value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0003FFD0 File Offset: 0x0003E1D0
		public override void WriteValue(float value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0003FFDE File Offset: 0x0003E1DE
		public override void WriteValue(decimal value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0003FFEC File Offset: 0x0003E1EC
		public override void WriteValue(int value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0003FFFA File Offset: 0x0003E1FA
		public override void WriteValue(long value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x00040008 File Offset: 0x0003E208
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				((IDisposable)this.writer).Dispose();
			}
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x00040018 File Offset: 0x0003E218
		public override Task WriteStartDocumentAsync()
		{
			return this.writer.WriteStartDocumentAsync();
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x00040025 File Offset: 0x0003E225
		public override Task WriteStartDocumentAsync(bool standalone)
		{
			return this.writer.WriteStartDocumentAsync(standalone);
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x00040033 File Offset: 0x0003E233
		public override Task WriteEndDocumentAsync()
		{
			return this.writer.WriteEndDocumentAsync();
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x00040040 File Offset: 0x0003E240
		public override Task WriteDocTypeAsync(string name, string pubid, string sysid, string subset)
		{
			return this.writer.WriteDocTypeAsync(name, pubid, sysid, subset);
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x00040052 File Offset: 0x0003E252
		public override Task WriteStartElementAsync(string prefix, string localName, string ns)
		{
			return this.writer.WriteStartElementAsync(prefix, localName, ns);
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x00040062 File Offset: 0x0003E262
		public override Task WriteEndElementAsync()
		{
			return this.writer.WriteEndElementAsync();
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0004006F File Offset: 0x0003E26F
		public override Task WriteFullEndElementAsync()
		{
			return this.writer.WriteFullEndElementAsync();
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0004007C File Offset: 0x0003E27C
		protected internal override Task WriteStartAttributeAsync(string prefix, string localName, string ns)
		{
			return this.writer.WriteStartAttributeAsync(prefix, localName, ns);
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0004008C File Offset: 0x0003E28C
		protected internal override Task WriteEndAttributeAsync()
		{
			return this.writer.WriteEndAttributeAsync();
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x00040099 File Offset: 0x0003E299
		public override Task WriteCDataAsync(string text)
		{
			return this.writer.WriteCDataAsync(text);
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x000400A7 File Offset: 0x0003E2A7
		public override Task WriteCommentAsync(string text)
		{
			return this.writer.WriteCommentAsync(text);
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x000400B5 File Offset: 0x0003E2B5
		public override Task WriteProcessingInstructionAsync(string name, string text)
		{
			return this.writer.WriteProcessingInstructionAsync(name, text);
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x000400C4 File Offset: 0x0003E2C4
		public override Task WriteEntityRefAsync(string name)
		{
			return this.writer.WriteEntityRefAsync(name);
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x000400D2 File Offset: 0x0003E2D2
		public override Task WriteCharEntityAsync(char ch)
		{
			return this.writer.WriteCharEntityAsync(ch);
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x000400E0 File Offset: 0x0003E2E0
		public override Task WriteWhitespaceAsync(string ws)
		{
			return this.writer.WriteWhitespaceAsync(ws);
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x000400EE File Offset: 0x0003E2EE
		public override Task WriteStringAsync(string text)
		{
			return this.writer.WriteStringAsync(text);
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x000400FC File Offset: 0x0003E2FC
		public override Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			return this.writer.WriteSurrogateCharEntityAsync(lowChar, highChar);
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x0004010B File Offset: 0x0003E30B
		public override Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			return this.writer.WriteCharsAsync(buffer, index, count);
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0004011B File Offset: 0x0003E31B
		public override Task WriteRawAsync(char[] buffer, int index, int count)
		{
			return this.writer.WriteRawAsync(buffer, index, count);
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x0004012B File Offset: 0x0003E32B
		public override Task WriteRawAsync(string data)
		{
			return this.writer.WriteRawAsync(data);
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x00040139 File Offset: 0x0003E339
		public override Task WriteBase64Async(byte[] buffer, int index, int count)
		{
			return this.writer.WriteBase64Async(buffer, index, count);
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x00040149 File Offset: 0x0003E349
		public override Task FlushAsync()
		{
			return this.writer.FlushAsync();
		}

		// Token: 0x04000440 RID: 1088
		protected XmlWriter writer;
	}
}
