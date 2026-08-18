using System;
using System.Collections.Generic;

namespace System.Xml.Linq
{
	// Token: 0x0200002E RID: 46
	internal class XNodeBuilder : XmlWriter
	{
		// Token: 0x06000241 RID: 577 RVA: 0x00009E2D File Offset: 0x0000802D
		public XNodeBuilder(XContainer container)
		{
			this.root = container;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00009E3C File Offset: 0x0000803C
		public override XmlWriterSettings Settings
		{
			get
			{
				return new XmlWriterSettings
				{
					ConformanceLevel = ConformanceLevel.Auto
				};
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00009E57 File Offset: 0x00008057
		public override WriteState WriteState
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00009E5E File Offset: 0x0000805E
		public override void Close()
		{
			this.root.Add(this.content);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00009E71 File Offset: 0x00008071
		public override void Flush()
		{
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00009E73 File Offset: 0x00008073
		public override string LookupPrefix(string namespaceName)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00009E7A File Offset: 0x0000807A
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException(Res.GetString("NotSupported_WriteBase64"));
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00009E8B File Offset: 0x0000808B
		public override void WriteCData(string text)
		{
			this.AddNode(new XCData(text));
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00009E99 File Offset: 0x00008099
		public override void WriteCharEntity(char ch)
		{
			this.AddString(new string(ch, 1));
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00009EA8 File Offset: 0x000080A8
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.AddString(new string(buffer, index, count));
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00009EB8 File Offset: 0x000080B8
		public override void WriteComment(string text)
		{
			this.AddNode(new XComment(text));
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00009EC6 File Offset: 0x000080C6
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.AddNode(new XDocumentType(name, pubid, sysid, subset));
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00009ED8 File Offset: 0x000080D8
		public override void WriteEndAttribute()
		{
			XAttribute o = new XAttribute(this.attrName, this.attrValue);
			this.attrName = null;
			this.attrValue = null;
			if (this.parent != null)
			{
				this.parent.Add(o);
				return;
			}
			this.Add(o);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00009F21 File Offset: 0x00008121
		public override void WriteEndDocument()
		{
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00009F23 File Offset: 0x00008123
		public override void WriteEndElement()
		{
			this.parent = ((XElement)this.parent).parent;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00009F3C File Offset: 0x0000813C
		public override void WriteEntityRef(string name)
		{
			if (name == "amp")
			{
				this.AddString("&");
				return;
			}
			if (name == "apos")
			{
				this.AddString("'");
				return;
			}
			if (name == "gt")
			{
				this.AddString(">");
				return;
			}
			if (name == "lt")
			{
				this.AddString("<");
				return;
			}
			if (!(name == "quot"))
			{
				throw new NotSupportedException(Res.GetString("NotSupported_WriteEntityRef"));
			}
			this.AddString("\"");
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00009FD8 File Offset: 0x000081D8
		public override void WriteFullEndElement()
		{
			XElement xelement = (XElement)this.parent;
			if (xelement.IsEmpty)
			{
				xelement.Add(string.Empty);
			}
			this.parent = xelement.parent;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000A010 File Offset: 0x00008210
		public override void WriteProcessingInstruction(string name, string text)
		{
			if (name == "xml")
			{
				return;
			}
			this.AddNode(new XProcessingInstruction(name, text));
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000A02D File Offset: 0x0000822D
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.AddString(new string(buffer, index, count));
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000A03D File Offset: 0x0000823D
		public override void WriteRaw(string data)
		{
			this.AddString(data);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000A046 File Offset: 0x00008246
		public override void WriteStartAttribute(string prefix, string localName, string namespaceName)
		{
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			this.attrName = XNamespace.Get((prefix.Length == 0) ? string.Empty : namespaceName).GetName(localName);
			this.attrValue = string.Empty;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000A082 File Offset: 0x00008282
		public override void WriteStartDocument()
		{
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000A084 File Offset: 0x00008284
		public override void WriteStartDocument(bool standalone)
		{
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000A086 File Offset: 0x00008286
		public override void WriteStartElement(string prefix, string localName, string namespaceName)
		{
			this.AddNode(new XElement(XNamespace.Get(namespaceName).GetName(localName)));
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000A09F File Offset: 0x0000829F
		public override void WriteString(string text)
		{
			this.AddString(text);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000A0A8 File Offset: 0x000082A8
		public override void WriteSurrogateCharEntity(char lowCh, char highCh)
		{
			this.AddString(new string(new char[]
			{
				highCh,
				lowCh
			}));
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000A0C3 File Offset: 0x000082C3
		public override void WriteValue(DateTimeOffset value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000A0D1 File Offset: 0x000082D1
		public override void WriteWhitespace(string ws)
		{
			this.AddString(ws);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000A0DA File Offset: 0x000082DA
		private void Add(object o)
		{
			if (this.content == null)
			{
				this.content = new List<object>();
			}
			this.content.Add(o);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000A0FC File Offset: 0x000082FC
		private void AddNode(XNode n)
		{
			if (this.parent != null)
			{
				this.parent.Add(n);
			}
			else
			{
				this.Add(n);
			}
			XContainer xcontainer = n as XContainer;
			if (xcontainer != null)
			{
				this.parent = xcontainer;
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000A137 File Offset: 0x00008337
		private void AddString(string s)
		{
			if (s == null)
			{
				return;
			}
			if (this.attrValue != null)
			{
				this.attrValue += s;
				return;
			}
			if (this.parent != null)
			{
				this.parent.Add(s);
				return;
			}
			this.Add(s);
		}

		// Token: 0x040000B7 RID: 183
		private List<object> content;

		// Token: 0x040000B8 RID: 184
		private XContainer parent;

		// Token: 0x040000B9 RID: 185
		private XName attrName;

		// Token: 0x040000BA RID: 186
		private string attrValue;

		// Token: 0x040000BB RID: 187
		private XContainer root;
	}
}
