using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;

namespace System.Transactions.Diagnostics
{
	// Token: 0x020000A0 RID: 160
	internal class PlainXmlWriter : XmlWriter
	{
		// Token: 0x06000475 RID: 1141 RVA: 0x0003FF64 File Offset: 0x0003F364
		public PlainXmlWriter(bool format)
		{
			this.navigator = new TraceXPathNavigator();
			this.stack = new Stack<string>();
			this.format = format;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0003FF94 File Offset: 0x0003F394
		public PlainXmlWriter() : this(false)
		{
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0003FFB4 File Offset: 0x0003F3B4
		public XPathNavigator ToNavigator()
		{
			return this.navigator;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0003FFD4 File Offset: 0x0003F3D4
		public override void WriteStartDocument()
		{
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0003FFE4 File Offset: 0x0003F3E4
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0003FFF4 File Offset: 0x0003F3F4
		public override void WriteStartDocument(bool standalone)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00040014 File Offset: 0x0003F414
		public override void WriteEndDocument()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00040034 File Offset: 0x0003F434
		public override string LookupPrefix(string ns)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x00040054 File Offset: 0x0003F454
		public override WriteState WriteState
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00040074 File Offset: 0x0003F474
		public override XmlSpace XmlSpace
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x00040094 File Offset: 0x0003F494
		public override string XmlLang
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000400B4 File Offset: 0x0003F4B4
		public override void WriteNmToken(string name)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x000400D4 File Offset: 0x0003F4D4
		public override void WriteName(string name)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x000400F4 File Offset: 0x0003F4F4
		public override void WriteQualifiedName(string localName, string ns)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00040114 File Offset: 0x0003F514
		public override void WriteValue(object value)
		{
			this.navigator.AddText(value.ToString());
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00040134 File Offset: 0x0003F534
		public override void WriteValue(string value)
		{
			this.navigator.AddText(value);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00040154 File Offset: 0x0003F554
		public override void WriteBase64(byte[] buffer, int offset, int count)
		{
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00040164 File Offset: 0x0003F564
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.navigator.AddElement(prefix, localName, ns);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00040184 File Offset: 0x0003F584
		public override void WriteFullEndElement()
		{
			this.WriteEndElement();
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x000401A4 File Offset: 0x0003F5A4
		public override void WriteEndElement()
		{
			this.navigator.CloseElement();
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000401C4 File Offset: 0x0003F5C4
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.currentAttributeName = localName;
			this.currentAttributePrefix = prefix;
			this.currentAttributeNs = ns;
			this.writingAttribute = true;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x000401F4 File Offset: 0x0003F5F4
		public override void WriteEndAttribute()
		{
			this.writingAttribute = false;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00040214 File Offset: 0x0003F614
		public override void WriteCData(string text)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00040234 File Offset: 0x0003F634
		public override void WriteComment(string text)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00040254 File Offset: 0x0003F654
		public override void WriteProcessingInstruction(string name, string text)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00040274 File Offset: 0x0003F674
		public override void WriteEntityRef(string name)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00040294 File Offset: 0x0003F694
		public override void WriteCharEntity(char ch)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x000402B4 File Offset: 0x0003F6B4
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x000402D4 File Offset: 0x0003F6D4
		public override void WriteWhitespace(string ws)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x000402F4 File Offset: 0x0003F6F4
		public override void WriteString(string text)
		{
			if (this.writingAttribute)
			{
				this.navigator.AddAttribute(this.currentAttributeName, text, this.currentAttributeNs, this.currentAttributePrefix);
				return;
			}
			this.WriteValue(text);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00040334 File Offset: 0x0003F734
		public override void WriteChars(char[] buffer, int index, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00040354 File Offset: 0x0003F754
		public override void WriteRaw(string data)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00040374 File Offset: 0x0003F774
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00040394 File Offset: 0x0003F794
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x000403B4 File Offset: 0x0003F7B4
		public override void Close()
		{
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x000403C4 File Offset: 0x0003F7C4
		public override void Flush()
		{
		}

		// Token: 0x0400026B RID: 619
		private TraceXPathNavigator navigator;

		// Token: 0x0400026C RID: 620
		private Stack<string> stack;

		// Token: 0x0400026D RID: 621
		private bool writingAttribute;

		// Token: 0x0400026E RID: 622
		private string currentAttributeName;

		// Token: 0x0400026F RID: 623
		private string currentAttributePrefix;

		// Token: 0x04000270 RID: 624
		private string currentAttributeNs;

		// Token: 0x04000271 RID: 625
		private bool format;
	}
}
