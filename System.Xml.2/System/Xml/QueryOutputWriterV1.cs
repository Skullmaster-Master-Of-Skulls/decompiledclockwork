using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x020000B7 RID: 183
	internal class QueryOutputWriterV1 : XmlWriter
	{
		// Token: 0x0600064B RID: 1611 RVA: 0x0001687C File Offset: 0x00014A7C
		public QueryOutputWriterV1(XmlWriter writer, XmlWriterSettings settings)
		{
			this.wrapped = writer;
			this.systemId = settings.DocTypeSystem;
			this.publicId = settings.DocTypePublic;
			if (settings.OutputMethod == XmlOutputMethod.Xml)
			{
				bool flag = false;
				if (this.systemId != null)
				{
					flag = true;
					this.outputDocType = true;
				}
				if (settings.Standalone == XmlStandalone.Yes)
				{
					flag = true;
					this.standalone = settings.Standalone;
				}
				if (flag)
				{
					if (settings.Standalone == XmlStandalone.Yes)
					{
						this.wrapped.WriteStartDocument(true);
					}
					else
					{
						this.wrapped.WriteStartDocument();
					}
				}
				if (settings.CDataSectionElements != null && settings.CDataSectionElements.Count > 0)
				{
					this.bitsCData = new BitStack();
					this.lookupCDataElems = new Dictionary<XmlQualifiedName, XmlQualifiedName>();
					this.qnameCData = new XmlQualifiedName();
					foreach (XmlQualifiedName key in settings.CDataSectionElements)
					{
						this.lookupCDataElems[key] = null;
					}
					this.bitsCData.PushBit(false);
					return;
				}
			}
			else if (settings.OutputMethod == XmlOutputMethod.Html && (this.systemId != null || this.publicId != null))
			{
				this.outputDocType = true;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x000169C0 File Offset: 0x00014BC0
		public override WriteState WriteState
		{
			get
			{
				return this.wrapped.WriteState;
			}
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x000169CD File Offset: 0x00014BCD
		public override void WriteStartDocument()
		{
			this.wrapped.WriteStartDocument();
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x000169DA File Offset: 0x00014BDA
		public override void WriteStartDocument(bool standalone)
		{
			this.wrapped.WriteStartDocument(standalone);
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x000169E8 File Offset: 0x00014BE8
		public override void WriteEndDocument()
		{
			this.wrapped.WriteEndDocument();
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x000169F5 File Offset: 0x00014BF5
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			if (this.publicId == null && this.systemId == null)
			{
				this.wrapped.WriteDocType(name, pubid, sysid, subset);
			}
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00016A18 File Offset: 0x00014C18
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.EndCDataSection();
			if (this.outputDocType)
			{
				WriteState writeState = this.wrapped.WriteState;
				if (writeState == WriteState.Start || writeState == WriteState.Prolog)
				{
					this.wrapped.WriteDocType((prefix.Length != 0) ? (prefix + ":" + localName) : localName, this.publicId, this.systemId, null);
				}
				this.outputDocType = false;
			}
			this.wrapped.WriteStartElement(prefix, localName, ns);
			if (this.lookupCDataElems != null)
			{
				this.qnameCData.Init(localName, ns);
				this.bitsCData.PushBit(this.lookupCDataElems.ContainsKey(this.qnameCData));
			}
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00016ABB File Offset: 0x00014CBB
		public override void WriteEndElement()
		{
			this.EndCDataSection();
			this.wrapped.WriteEndElement();
			if (this.lookupCDataElems != null)
			{
				this.bitsCData.PopBit();
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00016AE2 File Offset: 0x00014CE2
		public override void WriteFullEndElement()
		{
			this.EndCDataSection();
			this.wrapped.WriteFullEndElement();
			if (this.lookupCDataElems != null)
			{
				this.bitsCData.PopBit();
			}
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00016B09 File Offset: 0x00014D09
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.inAttr = true;
			this.wrapped.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00016B20 File Offset: 0x00014D20
		public override void WriteEndAttribute()
		{
			this.inAttr = false;
			this.wrapped.WriteEndAttribute();
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00016B34 File Offset: 0x00014D34
		public override void WriteCData(string text)
		{
			this.wrapped.WriteCData(text);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00016B42 File Offset: 0x00014D42
		public override void WriteComment(string text)
		{
			this.EndCDataSection();
			this.wrapped.WriteComment(text);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00016B56 File Offset: 0x00014D56
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.EndCDataSection();
			this.wrapped.WriteProcessingInstruction(name, text);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00016B6B File Offset: 0x00014D6B
		public override void WriteWhitespace(string ws)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(ws);
				return;
			}
			this.wrapped.WriteWhitespace(ws);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00016B9E File Offset: 0x00014D9E
		public override void WriteString(string text)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(text);
				return;
			}
			this.wrapped.WriteString(text);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00016BD1 File Offset: 0x00014DD1
		public override void WriteChars(char[] buffer, int index, int count)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(new string(buffer, index, count));
				return;
			}
			this.wrapped.WriteChars(buffer, index, count);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00016C0D File Offset: 0x00014E0D
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteBase64(buffer, index, count);
				return;
			}
			this.wrapped.WriteBase64(buffer, index, count);
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00016C44 File Offset: 0x00014E44
		public override void WriteEntityRef(string name)
		{
			this.EndCDataSection();
			this.wrapped.WriteEntityRef(name);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00016C58 File Offset: 0x00014E58
		public override void WriteCharEntity(char ch)
		{
			this.EndCDataSection();
			this.wrapped.WriteCharEntity(ch);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00016C6C File Offset: 0x00014E6C
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.EndCDataSection();
			this.wrapped.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00016C81 File Offset: 0x00014E81
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(new string(buffer, index, count));
				return;
			}
			this.wrapped.WriteRaw(buffer, index, count);
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00016CBD File Offset: 0x00014EBD
		public override void WriteRaw(string data)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(data);
				return;
			}
			this.wrapped.WriteRaw(data);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00016CF0 File Offset: 0x00014EF0
		public override void Close()
		{
			this.wrapped.Close();
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00016CFD File Offset: 0x00014EFD
		public override void Flush()
		{
			this.wrapped.Flush();
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00016D0A File Offset: 0x00014F0A
		public override string LookupPrefix(string ns)
		{
			return this.wrapped.LookupPrefix(ns);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00016D18 File Offset: 0x00014F18
		private bool StartCDataSection()
		{
			if (this.lookupCDataElems != null && this.bitsCData.PeekBit())
			{
				this.inCDataSection = true;
				return true;
			}
			return false;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00016D39 File Offset: 0x00014F39
		private void EndCDataSection()
		{
			this.inCDataSection = false;
		}

		// Token: 0x04000293 RID: 659
		private XmlWriter wrapped;

		// Token: 0x04000294 RID: 660
		private bool inCDataSection;

		// Token: 0x04000295 RID: 661
		private Dictionary<XmlQualifiedName, XmlQualifiedName> lookupCDataElems;

		// Token: 0x04000296 RID: 662
		private BitStack bitsCData;

		// Token: 0x04000297 RID: 663
		private XmlQualifiedName qnameCData;

		// Token: 0x04000298 RID: 664
		private bool outputDocType;

		// Token: 0x04000299 RID: 665
		private bool inAttr;

		// Token: 0x0400029A RID: 666
		private string systemId;

		// Token: 0x0400029B RID: 667
		private string publicId;

		// Token: 0x0400029C RID: 668
		private XmlStandalone standalone;
	}
}
