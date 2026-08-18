using System;
using System.IO;
using System.Xml;

namespace System.Data
{
	// Token: 0x0200013F RID: 319
	internal sealed class DataTextWriter : XmlWriter
	{
		// Token: 0x060012AC RID: 4780 RVA: 0x00093EF0 File Offset: 0x000932F0
		internal static XmlWriter CreateWriter(XmlWriter xw)
		{
			return new DataTextWriter(xw);
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x00093F04 File Offset: 0x00093304
		private DataTextWriter(XmlWriter w)
		{
			this._xmltextWriter = w;
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x00093F20 File Offset: 0x00093320
		internal Stream BaseStream
		{
			get
			{
				XmlTextWriter xmlTextWriter = this._xmltextWriter as XmlTextWriter;
				if (xmlTextWriter != null)
				{
					return xmlTextWriter.BaseStream;
				}
				return null;
			}
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x00093F44 File Offset: 0x00093344
		public override void WriteStartDocument()
		{
			this._xmltextWriter.WriteStartDocument();
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x00093F5C File Offset: 0x0009335C
		public override void WriteStartDocument(bool standalone)
		{
			this._xmltextWriter.WriteStartDocument(standalone);
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x00093F78 File Offset: 0x00093378
		public override void WriteEndDocument()
		{
			this._xmltextWriter.WriteEndDocument();
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x00093F90 File Offset: 0x00093390
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this._xmltextWriter.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x00093FB0 File Offset: 0x000933B0
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this._xmltextWriter.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x00093FCC File Offset: 0x000933CC
		public override void WriteEndElement()
		{
			this._xmltextWriter.WriteEndElement();
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x00093FE4 File Offset: 0x000933E4
		public override void WriteFullEndElement()
		{
			this._xmltextWriter.WriteFullEndElement();
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00093FFC File Offset: 0x000933FC
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this._xmltextWriter.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x00094018 File Offset: 0x00093418
		public override void WriteEndAttribute()
		{
			this._xmltextWriter.WriteEndAttribute();
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x00094030 File Offset: 0x00093430
		public override void WriteCData(string text)
		{
			this._xmltextWriter.WriteCData(text);
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x0009404C File Offset: 0x0009344C
		public override void WriteComment(string text)
		{
			this._xmltextWriter.WriteComment(text);
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x00094068 File Offset: 0x00093468
		public override void WriteProcessingInstruction(string name, string text)
		{
			this._xmltextWriter.WriteProcessingInstruction(name, text);
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00094084 File Offset: 0x00093484
		public override void WriteEntityRef(string name)
		{
			this._xmltextWriter.WriteEntityRef(name);
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x000940A0 File Offset: 0x000934A0
		public override void WriteCharEntity(char ch)
		{
			this._xmltextWriter.WriteCharEntity(ch);
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x000940BC File Offset: 0x000934BC
		public override void WriteWhitespace(string ws)
		{
			this._xmltextWriter.WriteWhitespace(ws);
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x000940D8 File Offset: 0x000934D8
		public override void WriteString(string text)
		{
			this._xmltextWriter.WriteString(text);
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x000940F4 File Offset: 0x000934F4
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this._xmltextWriter.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x00094110 File Offset: 0x00093510
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this._xmltextWriter.WriteChars(buffer, index, count);
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x0009412C File Offset: 0x0009352C
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this._xmltextWriter.WriteRaw(buffer, index, count);
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x00094148 File Offset: 0x00093548
		public override void WriteRaw(string data)
		{
			this._xmltextWriter.WriteRaw(data);
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x00094164 File Offset: 0x00093564
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this._xmltextWriter.WriteBase64(buffer, index, count);
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00094180 File Offset: 0x00093580
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			this._xmltextWriter.WriteBinHex(buffer, index, count);
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060012C5 RID: 4805 RVA: 0x0009419C File Offset: 0x0009359C
		public override WriteState WriteState
		{
			get
			{
				return this._xmltextWriter.WriteState;
			}
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x000941B4 File Offset: 0x000935B4
		public override void Close()
		{
			this._xmltextWriter.Close();
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x000941CC File Offset: 0x000935CC
		public override void Flush()
		{
			this._xmltextWriter.Flush();
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x000941E4 File Offset: 0x000935E4
		public override void WriteName(string name)
		{
			this._xmltextWriter.WriteName(name);
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x00094200 File Offset: 0x00093600
		public override void WriteQualifiedName(string localName, string ns)
		{
			this._xmltextWriter.WriteQualifiedName(localName, ns);
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x0009421C File Offset: 0x0009361C
		public override string LookupPrefix(string ns)
		{
			return this._xmltextWriter.LookupPrefix(ns);
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060012CB RID: 4811 RVA: 0x00094238 File Offset: 0x00093638
		public override XmlSpace XmlSpace
		{
			get
			{
				return this._xmltextWriter.XmlSpace;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x00094250 File Offset: 0x00093650
		public override string XmlLang
		{
			get
			{
				return this._xmltextWriter.XmlLang;
			}
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00094268 File Offset: 0x00093668
		public override void WriteNmToken(string name)
		{
			this._xmltextWriter.WriteNmToken(name);
		}

		// Token: 0x0400076A RID: 1898
		private XmlWriter _xmltextWriter;
	}
}
