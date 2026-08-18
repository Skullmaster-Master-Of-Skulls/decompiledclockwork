using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x0200004E RID: 78
	internal sealed class XPathDocumentBuilder : XmlRawWriter
	{
		// Token: 0x06000279 RID: 633 RVA: 0x0000A081 File Offset: 0x00008281
		public XPathDocumentBuilder(XPathDocument doc, IXmlLineInfo lineInfo, string baseUri, XPathDocument.LoadFlags flags)
		{
			this.nodePageFact.Init(256);
			this.nmspPageFact.Init(16);
			this.stkNmsp = new Stack<XPathNodeRef>();
			this.Initialize(doc, lineInfo, baseUri, flags);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000A0BC File Offset: 0x000082BC
		public void Initialize(XPathDocument doc, IXmlLineInfo lineInfo, string baseUri, XPathDocument.LoadFlags flags)
		{
			this.doc = doc;
			this.nameTable = doc.NameTable;
			this.atomizeNames = ((flags & XPathDocument.LoadFlags.AtomizeNames) > XPathDocument.LoadFlags.None);
			this.idxParent = (this.idxSibling = 0);
			this.elemNameIndex = new XPathNodeRef[64];
			this.textBldr.Initialize(lineInfo);
			this.lineInfo = lineInfo;
			this.lineNumBase = 0;
			this.linePosBase = 0;
			this.infoTable = new XPathNodeInfoTable();
			XPathNode[] pageText;
			int idxText = this.NewNode(out pageText, XPathNodeType.Text, string.Empty, string.Empty, string.Empty, string.Empty);
			this.doc.SetCollapsedTextNode(pageText, idxText);
			this.idxNmsp = this.NewNamespaceNode(out this.pageNmsp, this.nameTable.Add("xml"), this.nameTable.Add("http://www.w3.org/XML/1998/namespace"), null, 0);
			this.doc.SetXmlNamespaceNode(this.pageNmsp, this.idxNmsp);
			if ((flags & XPathDocument.LoadFlags.Fragment) == XPathDocument.LoadFlags.None)
			{
				this.idxParent = this.NewNode(out this.pageParent, XPathNodeType.Root, string.Empty, string.Empty, string.Empty, baseUri);
				this.doc.SetRootNode(this.pageParent, this.idxParent);
				return;
			}
			this.doc.SetRootNode(this.nodePageFact.NextNodePage, this.nodePageFact.NextNodeIndex);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000A20A File Offset: 0x0000840A
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000A20C File Offset: 0x0000840C
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.WriteStartElement(prefix, localName, ns, string.Empty);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000A21C File Offset: 0x0000841C
		public void WriteStartElement(string prefix, string localName, string ns, string baseUri)
		{
			if (this.atomizeNames)
			{
				prefix = this.nameTable.Add(prefix);
				localName = this.nameTable.Add(localName);
				ns = this.nameTable.Add(ns);
			}
			this.AddSibling(XPathNodeType.Element, localName, ns, prefix, baseUri);
			this.pageParent = this.pageSibling;
			this.idxParent = this.idxSibling;
			this.idxSibling = 0;
			int num = this.pageParent[this.idxParent].LocalNameHashCode & 63;
			this.elemNameIndex[num] = this.LinkSimilarElements(this.elemNameIndex[num].Page, this.elemNameIndex[num].Index, this.pageParent, this.idxParent);
			if (this.elemIdMap != null)
			{
				this.idAttrName = (XmlQualifiedName)this.elemIdMap[new XmlQualifiedName(localName, prefix)];
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000A305 File Offset: 0x00008505
		public override void WriteEndElement()
		{
			this.WriteEndElement(true);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000A30E File Offset: 0x0000850E
		public override void WriteFullEndElement()
		{
			this.WriteEndElement(false);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000A317 File Offset: 0x00008517
		internal override void WriteEndElement(string prefix, string localName, string namespaceName)
		{
			this.WriteEndElement(true);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000A320 File Offset: 0x00008520
		internal override void WriteFullEndElement(string prefix, string localName, string namespaceName)
		{
			this.WriteEndElement(false);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000A32C File Offset: 0x0000852C
		public void WriteEndElement(bool allowShortcutTag)
		{
			if (!this.pageParent[this.idxParent].HasContentChild)
			{
				TextBlockType textType = this.textBldr.TextType;
				if (textType == TextBlockType.Text)
				{
					if (this.lineInfo != null)
					{
						if (this.textBldr.LineNumber != this.pageParent[this.idxParent].LineNumber)
						{
							goto IL_CD;
						}
						int num = this.textBldr.LinePosition - this.pageParent[this.idxParent].LinePosition;
						if (num < 0 || num > 255)
						{
							goto IL_CD;
						}
						this.pageParent[this.idxParent].SetCollapsedLineInfoOffset(num);
					}
					this.pageParent[this.idxParent].SetCollapsedValue(this.textBldr.ReadText());
					goto IL_12D;
				}
				if (textType - TextBlockType.SignificantWhitespace > 1)
				{
					this.pageParent[this.idxParent].SetEmptyValue(allowShortcutTag);
					goto IL_12D;
				}
				IL_CD:
				this.CachedTextNode();
				this.pageParent[this.idxParent].SetValue(this.pageSibling[this.idxSibling].Value);
			}
			else if (this.textBldr.HasText)
			{
				this.CachedTextNode();
			}
			IL_12D:
			if (this.pageParent[this.idxParent].HasNamespaceDecls)
			{
				this.doc.AddNamespace(this.pageParent, this.idxParent, this.pageNmsp, this.idxNmsp);
				XPathNodeRef xpathNodeRef = this.stkNmsp.Pop();
				this.pageNmsp = xpathNodeRef.Page;
				this.idxNmsp = xpathNodeRef.Index;
			}
			this.pageSibling = this.pageParent;
			this.idxSibling = this.idxParent;
			this.idxParent = this.pageParent[this.idxParent].GetParent(out this.pageParent);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000A504 File Offset: 0x00008704
		public override void WriteStartAttribute(string prefix, string localName, string namespaceName)
		{
			if (this.atomizeNames)
			{
				prefix = this.nameTable.Add(prefix);
				localName = this.nameTable.Add(localName);
				namespaceName = this.nameTable.Add(namespaceName);
			}
			this.AddSibling(XPathNodeType.Attribute, localName, namespaceName, prefix, string.Empty);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000A554 File Offset: 0x00008754
		public override void WriteEndAttribute()
		{
			this.pageSibling[this.idxSibling].SetValue(this.textBldr.ReadText());
			if (this.idAttrName != null && this.pageSibling[this.idxSibling].LocalName == this.idAttrName.Name && this.pageSibling[this.idxSibling].Prefix == this.idAttrName.Namespace)
			{
				this.doc.AddIdElement(this.pageSibling[this.idxSibling].Value, this.pageParent, this.idxParent);
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000A60D File Offset: 0x0000880D
		public override void WriteCData(string text)
		{
			this.WriteString(text, TextBlockType.Text);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000A617 File Offset: 0x00008817
		public override void WriteComment(string text)
		{
			this.AddSibling(XPathNodeType.Comment, string.Empty, string.Empty, string.Empty, string.Empty);
			this.pageSibling[this.idxSibling].SetValue(text);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000A64B File Offset: 0x0000884B
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.WriteProcessingInstruction(name, text, string.Empty);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000A65C File Offset: 0x0000885C
		public void WriteProcessingInstruction(string name, string text, string baseUri)
		{
			if (this.atomizeNames)
			{
				name = this.nameTable.Add(name);
			}
			this.AddSibling(XPathNodeType.ProcessingInstruction, name, string.Empty, string.Empty, baseUri);
			this.pageSibling[this.idxSibling].SetValue(text);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000A6A9 File Offset: 0x000088A9
		public override void WriteWhitespace(string ws)
		{
			this.WriteString(ws, TextBlockType.Whitespace);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000A6B3 File Offset: 0x000088B3
		public override void WriteString(string text)
		{
			this.WriteString(text, TextBlockType.Text);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000A6BD File Offset: 0x000088BD
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count), TextBlockType.Text);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000A6CE File Offset: 0x000088CE
		public override void WriteRaw(string data)
		{
			this.WriteString(data, TextBlockType.Text);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000A6D8 File Offset: 0x000088D8
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count), TextBlockType.Text);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000A6E9 File Offset: 0x000088E9
		public void WriteString(string text, TextBlockType textType)
		{
			this.textBldr.WriteTextBlock(text, textType);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000A6F8 File Offset: 0x000088F8
		public override void WriteEntityRef(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000A700 File Offset: 0x00008900
		public override void WriteCharEntity(char ch)
		{
			char[] value = new char[]
			{
				ch
			};
			this.WriteString(new string(value), TextBlockType.Text);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000A728 File Offset: 0x00008928
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			char[] value = new char[]
			{
				highChar,
				lowChar
			};
			this.WriteString(new string(value), TextBlockType.Text);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000A754 File Offset: 0x00008954
		public override void Close()
		{
			if (this.textBldr.HasText)
			{
				this.CachedTextNode();
			}
			XPathNode[] array;
			int rootNode = this.doc.GetRootNode(out array);
			if (rootNode == this.nodePageFact.NextNodeIndex && array == this.nodePageFact.NextNodePage)
			{
				this.AddSibling(XPathNodeType.Text, string.Empty, string.Empty, string.Empty, string.Empty);
				this.pageSibling[this.idxSibling].SetValue(string.Empty);
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000A7D4 File Offset: 0x000089D4
		public override void Flush()
		{
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000A7D6 File Offset: 0x000089D6
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000A7D8 File Offset: 0x000089D8
		internal override void WriteXmlDeclaration(string xmldecl)
		{
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000A7DA File Offset: 0x000089DA
		internal override void StartElementContent()
		{
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000A7DC File Offset: 0x000089DC
		internal override void WriteNamespaceDeclaration(string prefix, string namespaceName)
		{
			if (this.atomizeNames)
			{
				prefix = this.nameTable.Add(prefix);
			}
			namespaceName = this.nameTable.Add(namespaceName);
			XPathNode[] array = this.pageNmsp;
			int sibling = this.idxNmsp;
			while (sibling != 0 && array[sibling].LocalName != prefix)
			{
				sibling = array[sibling].GetSibling(out array);
			}
			XPathNode[] array2;
			int num = this.NewNamespaceNode(out array2, prefix, namespaceName, this.pageParent, this.idxParent);
			if (sibling != 0)
			{
				XPathNode[] array3 = this.pageNmsp;
				int sibling2 = this.idxNmsp;
				XPathNode[] array4 = array2;
				int num2 = num;
				while (sibling2 != sibling || array3 != array)
				{
					XPathNode[] array5;
					int num3 = array3[sibling2].GetParent(out array5);
					num3 = this.NewNamespaceNode(out array5, array3[sibling2].LocalName, array3[sibling2].Value, array5, num3);
					array4[num2].SetSibling(this.infoTable, array5, num3);
					array4 = array5;
					num2 = num3;
					sibling2 = array3[sibling2].GetSibling(out array3);
				}
				sibling = array[sibling].GetSibling(out array);
				if (sibling != 0)
				{
					array4[num2].SetSibling(this.infoTable, array, sibling);
				}
			}
			else if (this.idxParent != 0)
			{
				array2[num].SetSibling(this.infoTable, this.pageNmsp, this.idxNmsp);
			}
			else
			{
				this.doc.SetRootNode(array2, num);
			}
			if (this.idxParent != 0)
			{
				if (!this.pageParent[this.idxParent].HasNamespaceDecls)
				{
					this.stkNmsp.Push(new XPathNodeRef(this.pageNmsp, this.idxNmsp));
					this.pageParent[this.idxParent].HasNamespaceDecls = true;
				}
				this.pageNmsp = array2;
				this.idxNmsp = num;
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000A9B4 File Offset: 0x00008BB4
		public void CreateIdTables(IDtdInfo dtdInfo)
		{
			foreach (IDtdAttributeListInfo dtdAttributeListInfo in dtdInfo.GetAttributeLists())
			{
				IDtdAttributeInfo dtdAttributeInfo = dtdAttributeListInfo.LookupIdAttribute();
				if (dtdAttributeInfo != null)
				{
					if (this.elemIdMap == null)
					{
						this.elemIdMap = new Hashtable();
					}
					this.elemIdMap.Add(new XmlQualifiedName(dtdAttributeListInfo.LocalName, dtdAttributeListInfo.Prefix), new XmlQualifiedName(dtdAttributeInfo.LocalName, dtdAttributeInfo.Prefix));
				}
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000AA44 File Offset: 0x00008C44
		private XPathNodeRef LinkSimilarElements(XPathNode[] pagePrev, int idxPrev, XPathNode[] pageNext, int idxNext)
		{
			if (pagePrev != null)
			{
				pagePrev[idxPrev].SetSimilarElement(this.infoTable, pageNext, idxNext);
			}
			return new XPathNodeRef(pageNext, idxNext);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000AA68 File Offset: 0x00008C68
		private int NewNamespaceNode(out XPathNode[] page, string prefix, string namespaceUri, XPathNode[] pageElem, int idxElem)
		{
			XPathNode[] array;
			int num;
			this.nmspPageFact.AllocateSlot(out array, out num);
			int lineNumOffset;
			int linePosOffset;
			this.ComputeLineInfo(false, out lineNumOffset, out linePosOffset);
			XPathNodeInfoAtom info = this.infoTable.Create(prefix, string.Empty, string.Empty, string.Empty, pageElem, array, null, this.doc, this.lineNumBase, this.linePosBase);
			array[num].Create(info, XPathNodeType.Namespace, idxElem);
			array[num].SetValue(namespaceUri);
			array[num].SetLineInfoOffsets(lineNumOffset, linePosOffset);
			page = array;
			return num;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000AAF4 File Offset: 0x00008CF4
		private int NewNode(out XPathNode[] page, XPathNodeType xptyp, string localName, string namespaceUri, string prefix, string baseUri)
		{
			XPathNode[] array;
			int num;
			this.nodePageFact.AllocateSlot(out array, out num);
			int lineNumOffset;
			int linePosOffset;
			this.ComputeLineInfo(XPathNavigator.IsText(xptyp), out lineNumOffset, out linePosOffset);
			XPathNodeInfoAtom info = this.infoTable.Create(localName, namespaceUri, prefix, baseUri, this.pageParent, array, array, this.doc, this.lineNumBase, this.linePosBase);
			array[num].Create(info, xptyp, this.idxParent);
			array[num].SetLineInfoOffsets(lineNumOffset, linePosOffset);
			page = array;
			return num;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000AB78 File Offset: 0x00008D78
		private void ComputeLineInfo(bool isTextNode, out int lineNumOffset, out int linePosOffset)
		{
			if (this.lineInfo == null)
			{
				lineNumOffset = 0;
				linePosOffset = 0;
				return;
			}
			int lineNumber;
			int linePosition;
			if (isTextNode)
			{
				lineNumber = this.textBldr.LineNumber;
				linePosition = this.textBldr.LinePosition;
			}
			else
			{
				lineNumber = this.lineInfo.LineNumber;
				linePosition = this.lineInfo.LinePosition;
			}
			lineNumOffset = lineNumber - this.lineNumBase;
			if (lineNumOffset < 0 || lineNumOffset > 16383)
			{
				this.lineNumBase = lineNumber;
				lineNumOffset = 0;
			}
			linePosOffset = linePosition - this.linePosBase;
			if (linePosOffset < 0 || linePosOffset > 65535)
			{
				this.linePosBase = linePosition;
				linePosOffset = 0;
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000AC10 File Offset: 0x00008E10
		private void AddSibling(XPathNodeType xptyp, string localName, string namespaceUri, string prefix, string baseUri)
		{
			if (this.textBldr.HasText)
			{
				this.CachedTextNode();
			}
			XPathNode[] array;
			int num = this.NewNode(out array, xptyp, localName, namespaceUri, prefix, baseUri);
			if (this.idxParent != 0)
			{
				this.pageParent[this.idxParent].SetParentProperties(xptyp);
				if (this.idxSibling != 0)
				{
					this.pageSibling[this.idxSibling].SetSibling(this.infoTable, array, num);
				}
			}
			this.pageSibling = array;
			this.idxSibling = num;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000AC94 File Offset: 0x00008E94
		private void CachedTextNode()
		{
			TextBlockType textType = this.textBldr.TextType;
			string value = this.textBldr.ReadText();
			this.AddSibling((XPathNodeType)textType, string.Empty, string.Empty, string.Empty, string.Empty);
			this.pageSibling[this.idxSibling].SetValue(value);
		}

		// Token: 0x04000105 RID: 261
		private XPathDocumentBuilder.NodePageFactory nodePageFact;

		// Token: 0x04000106 RID: 262
		private XPathDocumentBuilder.NodePageFactory nmspPageFact;

		// Token: 0x04000107 RID: 263
		private XPathDocumentBuilder.TextBlockBuilder textBldr;

		// Token: 0x04000108 RID: 264
		private Stack<XPathNodeRef> stkNmsp;

		// Token: 0x04000109 RID: 265
		private XPathNodeInfoTable infoTable;

		// Token: 0x0400010A RID: 266
		private XPathDocument doc;

		// Token: 0x0400010B RID: 267
		private IXmlLineInfo lineInfo;

		// Token: 0x0400010C RID: 268
		private XmlNameTable nameTable;

		// Token: 0x0400010D RID: 269
		private bool atomizeNames;

		// Token: 0x0400010E RID: 270
		private XPathNode[] pageNmsp;

		// Token: 0x0400010F RID: 271
		private int idxNmsp;

		// Token: 0x04000110 RID: 272
		private XPathNode[] pageParent;

		// Token: 0x04000111 RID: 273
		private int idxParent;

		// Token: 0x04000112 RID: 274
		private XPathNode[] pageSibling;

		// Token: 0x04000113 RID: 275
		private int idxSibling;

		// Token: 0x04000114 RID: 276
		private int lineNumBase;

		// Token: 0x04000115 RID: 277
		private int linePosBase;

		// Token: 0x04000116 RID: 278
		private XmlQualifiedName idAttrName;

		// Token: 0x04000117 RID: 279
		private Hashtable elemIdMap;

		// Token: 0x04000118 RID: 280
		private XPathNodeRef[] elemNameIndex;

		// Token: 0x04000119 RID: 281
		private const int ElementIndexSize = 64;

		// Token: 0x02000304 RID: 772
		private struct NodePageFactory
		{
			// Token: 0x06002D91 RID: 11665 RVA: 0x000ECB1F File Offset: 0x000EAD1F
			public void Init(int initialPageSize)
			{
				this.pageSize = initialPageSize;
				this.page = new XPathNode[this.pageSize];
				this.pageInfo = new XPathNodePageInfo(null, 1);
				this.page[0].Create(this.pageInfo);
			}

			// Token: 0x17000A16 RID: 2582
			// (get) Token: 0x06002D92 RID: 11666 RVA: 0x000ECB5D File Offset: 0x000EAD5D
			public XPathNode[] NextNodePage
			{
				get
				{
					return this.page;
				}
			}

			// Token: 0x17000A17 RID: 2583
			// (get) Token: 0x06002D93 RID: 11667 RVA: 0x000ECB65 File Offset: 0x000EAD65
			public int NextNodeIndex
			{
				get
				{
					return this.pageInfo.NodeCount;
				}
			}

			// Token: 0x06002D94 RID: 11668 RVA: 0x000ECB74 File Offset: 0x000EAD74
			public void AllocateSlot(out XPathNode[] page, out int idx)
			{
				page = this.page;
				idx = this.pageInfo.NodeCount;
				XPathNodePageInfo xpathNodePageInfo = this.pageInfo;
				int num = xpathNodePageInfo.NodeCount + 1;
				xpathNodePageInfo.NodeCount = num;
				if (num >= this.page.Length)
				{
					if (this.pageSize < 65536)
					{
						this.pageSize *= 2;
					}
					this.page = new XPathNode[this.pageSize];
					this.pageInfo.NextPage = this.page;
					this.pageInfo = new XPathNodePageInfo(page, this.pageInfo.PageNumber + 1);
					this.page[0].Create(this.pageInfo);
				}
			}

			// Token: 0x0400143C RID: 5180
			private XPathNode[] page;

			// Token: 0x0400143D RID: 5181
			private XPathNodePageInfo pageInfo;

			// Token: 0x0400143E RID: 5182
			private int pageSize;
		}

		// Token: 0x02000305 RID: 773
		private struct TextBlockBuilder
		{
			// Token: 0x06002D95 RID: 11669 RVA: 0x000ECC24 File Offset: 0x000EAE24
			public void Initialize(IXmlLineInfo lineInfo)
			{
				this.lineInfo = lineInfo;
				this.textType = TextBlockType.None;
			}

			// Token: 0x17000A18 RID: 2584
			// (get) Token: 0x06002D96 RID: 11670 RVA: 0x000ECC34 File Offset: 0x000EAE34
			public TextBlockType TextType
			{
				get
				{
					return this.textType;
				}
			}

			// Token: 0x17000A19 RID: 2585
			// (get) Token: 0x06002D97 RID: 11671 RVA: 0x000ECC3C File Offset: 0x000EAE3C
			public bool HasText
			{
				get
				{
					return this.textType > TextBlockType.None;
				}
			}

			// Token: 0x17000A1A RID: 2586
			// (get) Token: 0x06002D98 RID: 11672 RVA: 0x000ECC47 File Offset: 0x000EAE47
			public int LineNumber
			{
				get
				{
					return this.lineNum;
				}
			}

			// Token: 0x17000A1B RID: 2587
			// (get) Token: 0x06002D99 RID: 11673 RVA: 0x000ECC4F File Offset: 0x000EAE4F
			public int LinePosition
			{
				get
				{
					return this.linePos;
				}
			}

			// Token: 0x06002D9A RID: 11674 RVA: 0x000ECC58 File Offset: 0x000EAE58
			public void WriteTextBlock(string text, TextBlockType textType)
			{
				if (text.Length != 0)
				{
					if (this.textType == TextBlockType.None)
					{
						this.text = text;
						this.textType = textType;
						if (this.lineInfo != null)
						{
							this.lineNum = this.lineInfo.LineNumber;
							this.linePos = this.lineInfo.LinePosition;
							return;
						}
					}
					else
					{
						this.text += text;
						if (textType < this.textType)
						{
							this.textType = textType;
						}
					}
				}
			}

			// Token: 0x06002D9B RID: 11675 RVA: 0x000ECCD0 File Offset: 0x000EAED0
			public string ReadText()
			{
				if (this.textType == TextBlockType.None)
				{
					return string.Empty;
				}
				this.textType = TextBlockType.None;
				return this.text;
			}

			// Token: 0x0400143F RID: 5183
			private IXmlLineInfo lineInfo;

			// Token: 0x04001440 RID: 5184
			private TextBlockType textType;

			// Token: 0x04001441 RID: 5185
			private string text;

			// Token: 0x04001442 RID: 5186
			private int lineNum;

			// Token: 0x04001443 RID: 5187
			private int linePos;
		}
	}
}
