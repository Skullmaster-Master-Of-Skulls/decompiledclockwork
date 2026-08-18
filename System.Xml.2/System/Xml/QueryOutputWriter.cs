using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x020000B6 RID: 182
	internal class QueryOutputWriter : XmlRawWriter
	{
		// Token: 0x0600062B RID: 1579 RVA: 0x0001631C File Offset: 0x0001451C
		public QueryOutputWriter(XmlRawWriter writer, XmlWriterSettings settings)
		{
			this.wrapped = writer;
			this.systemId = settings.DocTypeSystem;
			this.publicId = settings.DocTypePublic;
			if (settings.OutputMethod == XmlOutputMethod.Xml)
			{
				if (this.systemId != null)
				{
					this.outputDocType = true;
					this.checkWellFormedDoc = true;
				}
				if (settings.AutoXmlDeclaration && settings.Standalone == XmlStandalone.Yes)
				{
					this.checkWellFormedDoc = true;
				}
				if (settings.CDataSectionElements.Count > 0)
				{
					this.bitsCData = new BitStack();
					this.lookupCDataElems = new Dictionary<XmlQualifiedName, int>();
					this.qnameCData = new XmlQualifiedName();
					foreach (XmlQualifiedName key in settings.CDataSectionElements)
					{
						this.lookupCDataElems[key] = 0;
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

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x00016434 File Offset: 0x00014634
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x0001643C File Offset: 0x0001463C
		internal override IXmlNamespaceResolver NamespaceResolver
		{
			get
			{
				return this.resolver;
			}
			set
			{
				this.resolver = value;
				this.wrapped.NamespaceResolver = value;
			}
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00016451 File Offset: 0x00014651
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
			this.wrapped.WriteXmlDeclaration(standalone);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001645F File Offset: 0x0001465F
		internal override void WriteXmlDeclaration(string xmldecl)
		{
			this.wrapped.WriteXmlDeclaration(xmldecl);
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x00016470 File Offset: 0x00014670
		public override XmlWriterSettings Settings
		{
			get
			{
				XmlWriterSettings settings = this.wrapped.Settings;
				settings.ReadOnly = false;
				settings.DocTypeSystem = this.systemId;
				settings.DocTypePublic = this.publicId;
				settings.ReadOnly = true;
				return settings;
			}
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000164B0 File Offset: 0x000146B0
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			if (this.publicId == null && this.systemId == null)
			{
				this.wrapped.WriteDocType(name, pubid, sysid, subset);
			}
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x000164D4 File Offset: 0x000146D4
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.EndCDataSection();
			if (this.checkWellFormedDoc)
			{
				if (this.depth == 0 && this.hasDocElem)
				{
					throw new XmlException("Xml_NoMultipleRoots", string.Empty);
				}
				this.depth++;
				this.hasDocElem = true;
			}
			if (this.outputDocType)
			{
				this.wrapped.WriteDocType((prefix.Length != 0) ? (prefix + ":" + localName) : localName, this.publicId, this.systemId, null);
				this.outputDocType = false;
			}
			this.wrapped.WriteStartElement(prefix, localName, ns);
			if (this.lookupCDataElems != null)
			{
				this.qnameCData.Init(localName, ns);
				this.bitsCData.PushBit(this.lookupCDataElems.ContainsKey(this.qnameCData));
			}
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x000165A1 File Offset: 0x000147A1
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			this.EndCDataSection();
			this.wrapped.WriteEndElement(prefix, localName, ns);
			if (this.checkWellFormedDoc)
			{
				this.depth--;
			}
			if (this.lookupCDataElems != null)
			{
				this.bitsCData.PopBit();
			}
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x000165E1 File Offset: 0x000147E1
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			this.EndCDataSection();
			this.wrapped.WriteFullEndElement(prefix, localName, ns);
			if (this.checkWellFormedDoc)
			{
				this.depth--;
			}
			if (this.lookupCDataElems != null)
			{
				this.bitsCData.PopBit();
			}
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00016621 File Offset: 0x00014821
		internal override void StartElementContent()
		{
			this.wrapped.StartElementContent();
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001662E File Offset: 0x0001482E
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.inAttr = true;
			this.wrapped.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00016645 File Offset: 0x00014845
		public override void WriteEndAttribute()
		{
			this.inAttr = false;
			this.wrapped.WriteEndAttribute();
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00016659 File Offset: 0x00014859
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
			this.wrapped.WriteNamespaceDeclaration(prefix, ns);
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x00016668 File Offset: 0x00014868
		internal override bool SupportsNamespaceDeclarationInChunks
		{
			get
			{
				return this.wrapped.SupportsNamespaceDeclarationInChunks;
			}
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00016675 File Offset: 0x00014875
		internal override void WriteStartNamespaceDeclaration(string prefix)
		{
			this.wrapped.WriteStartNamespaceDeclaration(prefix);
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00016683 File Offset: 0x00014883
		internal override void WriteEndNamespaceDeclaration()
		{
			this.wrapped.WriteEndNamespaceDeclaration();
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00016690 File Offset: 0x00014890
		public override void WriteCData(string text)
		{
			this.wrapped.WriteCData(text);
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001669E File Offset: 0x0001489E
		public override void WriteComment(string text)
		{
			this.EndCDataSection();
			this.wrapped.WriteComment(text);
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x000166B2 File Offset: 0x000148B2
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.EndCDataSection();
			this.wrapped.WriteProcessingInstruction(name, text);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x000166C7 File Offset: 0x000148C7
		public override void WriteWhitespace(string ws)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(ws);
				return;
			}
			this.wrapped.WriteWhitespace(ws);
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x000166FA File Offset: 0x000148FA
		public override void WriteString(string text)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(text);
				return;
			}
			this.wrapped.WriteString(text);
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001672D File Offset: 0x0001492D
		public override void WriteChars(char[] buffer, int index, int count)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(new string(buffer, index, count));
				return;
			}
			this.wrapped.WriteChars(buffer, index, count);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00016769 File Offset: 0x00014969
		public override void WriteEntityRef(string name)
		{
			this.EndCDataSection();
			this.wrapped.WriteEntityRef(name);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0001677D File Offset: 0x0001497D
		public override void WriteCharEntity(char ch)
		{
			this.EndCDataSection();
			this.wrapped.WriteCharEntity(ch);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00016791 File Offset: 0x00014991
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.EndCDataSection();
			this.wrapped.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x000167A6 File Offset: 0x000149A6
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(new string(buffer, index, count));
				return;
			}
			this.wrapped.WriteRaw(buffer, index, count);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x000167E2 File Offset: 0x000149E2
		public override void WriteRaw(string data)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(data);
				return;
			}
			this.wrapped.WriteRaw(data);
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00016815 File Offset: 0x00014A15
		public override void Close()
		{
			this.wrapped.Close();
			if (this.checkWellFormedDoc && !this.hasDocElem)
			{
				throw new XmlException("Xml_NoRoot", string.Empty);
			}
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00016842 File Offset: 0x00014A42
		public override void Flush()
		{
			this.wrapped.Flush();
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001684F File Offset: 0x00014A4F
		private bool StartCDataSection()
		{
			if (this.lookupCDataElems != null && this.bitsCData.PeekBit())
			{
				this.inCDataSection = true;
				return true;
			}
			return false;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00016870 File Offset: 0x00014A70
		private void EndCDataSection()
		{
			this.inCDataSection = false;
		}

		// Token: 0x04000287 RID: 647
		private XmlRawWriter wrapped;

		// Token: 0x04000288 RID: 648
		private bool inCDataSection;

		// Token: 0x04000289 RID: 649
		private Dictionary<XmlQualifiedName, int> lookupCDataElems;

		// Token: 0x0400028A RID: 650
		private BitStack bitsCData;

		// Token: 0x0400028B RID: 651
		private XmlQualifiedName qnameCData;

		// Token: 0x0400028C RID: 652
		private bool outputDocType;

		// Token: 0x0400028D RID: 653
		private bool checkWellFormedDoc;

		// Token: 0x0400028E RID: 654
		private bool hasDocElem;

		// Token: 0x0400028F RID: 655
		private bool inAttr;

		// Token: 0x04000290 RID: 656
		private string systemId;

		// Token: 0x04000291 RID: 657
		private string publicId;

		// Token: 0x04000292 RID: 658
		private int depth;
	}
}
