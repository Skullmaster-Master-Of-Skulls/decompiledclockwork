using System;

namespace System.Xml
{
	// Token: 0x020000B2 RID: 178
	internal class XsdCachingReader : XmlReader, IXmlLineInfo
	{
		// Token: 0x060009FD RID: 2557 RVA: 0x0002F51C File Offset: 0x0002E51C
		internal XsdCachingReader(XmlReader reader, IXmlLineInfo lineInfo, CachingEventHandler handlerMethod)
		{
			this.coreReader = reader;
			this.lineInfo = lineInfo;
			this.cacheHandler = handlerMethod;
			this.attributeEvents = new ValidatingReaderNodeData[8];
			this.contentEvents = new ValidatingReaderNodeData[4];
			this.Init();
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0002F558 File Offset: 0x0002E558
		private void Init()
		{
			this.coreReaderNameTable = this.coreReader.NameTable;
			this.cacheState = XsdCachingReader.CachingReaderState.Init;
			this.contentIndex = 0;
			this.currentAttrIndex = -1;
			this.currentContentIndex = -1;
			this.attributeCount = 0;
			this.cachedNode = null;
			this.readAhead = false;
			if (this.coreReader.NodeType == XmlNodeType.Element)
			{
				ValidatingReaderNodeData validatingReaderNodeData = this.AddContent(this.coreReader.NodeType);
				validatingReaderNodeData.SetItemData(this.coreReader.LocalName, this.coreReader.Prefix, this.coreReader.NamespaceURI, this.coreReader.Depth);
				validatingReaderNodeData.SetLineInfo(this.lineInfo);
				this.RecordAttributes();
			}
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0002F60B File Offset: 0x0002E60B
		internal void Reset(XmlReader reader)
		{
			this.coreReader = reader;
			this.Init();
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x0002F61A File Offset: 0x0002E61A
		public override XmlReaderSettings Settings
		{
			get
			{
				return this.coreReader.Settings;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x0002F627 File Offset: 0x0002E627
		public override XmlNodeType NodeType
		{
			get
			{
				return this.cachedNode.NodeType;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x0002F634 File Offset: 0x0002E634
		public override string Name
		{
			get
			{
				return this.cachedNode.GetAtomizedNameWPrefix(this.coreReaderNameTable);
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x0002F647 File Offset: 0x0002E647
		public override string LocalName
		{
			get
			{
				return this.cachedNode.LocalName;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x0002F654 File Offset: 0x0002E654
		public override string NamespaceURI
		{
			get
			{
				return this.cachedNode.Namespace;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x0002F661 File Offset: 0x0002E661
		public override string Prefix
		{
			get
			{
				return this.cachedNode.Prefix;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x0002F66E File Offset: 0x0002E66E
		public override bool HasValue
		{
			get
			{
				return XmlReader.HasValueInternal(this.cachedNode.NodeType);
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0002F680 File Offset: 0x0002E680
		public override string Value
		{
			get
			{
				if (!this.returnOriginalStringValues)
				{
					return this.cachedNode.RawValue;
				}
				return this.cachedNode.OriginalStringValue;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0002F6A1 File Offset: 0x0002E6A1
		public override int Depth
		{
			get
			{
				return this.cachedNode.Depth;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0002F6AE File Offset: 0x0002E6AE
		public override string BaseURI
		{
			get
			{
				return this.coreReader.BaseURI;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0002F6BB File Offset: 0x0002E6BB
		public override bool IsEmptyElement
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0002F6BE File Offset: 0x0002E6BE
		public override bool IsDefault
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x0002F6C1 File Offset: 0x0002E6C1
		public override char QuoteChar
		{
			get
			{
				return this.coreReader.QuoteChar;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0002F6CE File Offset: 0x0002E6CE
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.coreReader.XmlSpace;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x0002F6DB File Offset: 0x0002E6DB
		public override string XmlLang
		{
			get
			{
				return this.coreReader.XmlLang;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x0002F6E8 File Offset: 0x0002E6E8
		public override int AttributeCount
		{
			get
			{
				return this.attributeCount;
			}
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0002F6F0 File Offset: 0x0002E6F0
		public override string GetAttribute(string name)
		{
			int num;
			if (name.IndexOf(':') == -1)
			{
				num = this.GetAttributeIndexWithoutPrefix(name);
			}
			else
			{
				num = this.GetAttributeIndexWithPrefix(name);
			}
			if (num < 0)
			{
				return null;
			}
			return this.attributeEvents[num].RawValue;
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0002F730 File Offset: 0x0002E730
		public override string GetAttribute(string name, string namespaceURI)
		{
			namespaceURI = ((namespaceURI == null) ? string.Empty : this.coreReaderNameTable.Get(namespaceURI));
			name = this.coreReaderNameTable.Get(name);
			for (int i = 0; i < this.attributeCount; i++)
			{
				ValidatingReaderNodeData validatingReaderNodeData = this.attributeEvents[i];
				if (Ref.Equal(validatingReaderNodeData.LocalName, name) && Ref.Equal(validatingReaderNodeData.Namespace, namespaceURI))
				{
					return validatingReaderNodeData.RawValue;
				}
			}
			return null;
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0002F7A1 File Offset: 0x0002E7A1
		public override string GetAttribute(int i)
		{
			if (i < 0 || i >= this.attributeCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			return this.attributeEvents[i].RawValue;
		}

		// Token: 0x1700022E RID: 558
		public override string this[int i]
		{
			get
			{
				return this.GetAttribute(i);
			}
		}

		// Token: 0x1700022F RID: 559
		public override string this[string name]
		{
			get
			{
				return this.GetAttribute(name);
			}
		}

		// Token: 0x17000230 RID: 560
		public override string this[string name, string namespaceURI]
		{
			get
			{
				return this.GetAttribute(name, namespaceURI);
			}
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0002F7E4 File Offset: 0x0002E7E4
		public override bool MoveToAttribute(string name)
		{
			int num;
			if (name.IndexOf(':') == -1)
			{
				num = this.GetAttributeIndexWithoutPrefix(name);
			}
			else
			{
				num = this.GetAttributeIndexWithPrefix(name);
			}
			if (num >= 0)
			{
				this.currentAttrIndex = num;
				this.cachedNode = this.attributeEvents[num];
				return true;
			}
			return false;
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0002F82C File Offset: 0x0002E82C
		public override bool MoveToAttribute(string name, string ns)
		{
			ns = ((ns == null) ? string.Empty : this.coreReaderNameTable.Get(ns));
			name = this.coreReaderNameTable.Get(name);
			for (int i = 0; i < this.attributeCount; i++)
			{
				ValidatingReaderNodeData validatingReaderNodeData = this.attributeEvents[i];
				if (Ref.Equal(validatingReaderNodeData.LocalName, name) && Ref.Equal(validatingReaderNodeData.Namespace, ns))
				{
					this.currentAttrIndex = i;
					this.cachedNode = this.attributeEvents[i];
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0002F8AD File Offset: 0x0002E8AD
		public override void MoveToAttribute(int i)
		{
			if (i < 0 || i >= this.attributeCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			this.currentAttrIndex = i;
			this.cachedNode = this.attributeEvents[i];
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0002F8DC File Offset: 0x0002E8DC
		public override bool MoveToFirstAttribute()
		{
			if (this.attributeCount == 0)
			{
				return false;
			}
			this.currentAttrIndex = 0;
			this.cachedNode = this.attributeEvents[0];
			return true;
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0002F900 File Offset: 0x0002E900
		public override bool MoveToNextAttribute()
		{
			if (this.currentAttrIndex + 1 < this.attributeCount)
			{
				this.cachedNode = this.attributeEvents[++this.currentAttrIndex];
				return true;
			}
			return false;
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0002F93E File Offset: 0x0002E93E
		public override bool MoveToElement()
		{
			if (this.cacheState != XsdCachingReader.CachingReaderState.Replay || this.cachedNode.NodeType != XmlNodeType.Attribute)
			{
				return false;
			}
			this.currentContentIndex = 0;
			this.currentAttrIndex = -1;
			this.Read();
			return true;
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0002F970 File Offset: 0x0002E970
		public override bool Read()
		{
			switch (this.cacheState)
			{
			case XsdCachingReader.CachingReaderState.Init:
				this.cacheState = XsdCachingReader.CachingReaderState.Record;
				break;
			case XsdCachingReader.CachingReaderState.Record:
				break;
			case XsdCachingReader.CachingReaderState.Replay:
				if (this.currentContentIndex >= this.contentIndex)
				{
					this.cacheState = XsdCachingReader.CachingReaderState.ReaderClosed;
					this.cacheHandler(this);
					return (this.coreReader.NodeType == XmlNodeType.Element && !this.readAhead) || this.coreReader.Read();
				}
				this.cachedNode = this.contentEvents[this.currentContentIndex];
				if (this.currentContentIndex > 0)
				{
					this.ClearAttributesInfo();
				}
				this.currentContentIndex++;
				return true;
			default:
				return false;
			}
			ValidatingReaderNodeData validatingReaderNodeData = null;
			if (this.coreReader.Read())
			{
				switch (this.coreReader.NodeType)
				{
				case XmlNodeType.Element:
					this.cacheState = XsdCachingReader.CachingReaderState.ReaderClosed;
					return false;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					validatingReaderNodeData = this.AddContent(this.coreReader.NodeType);
					validatingReaderNodeData.SetItemData(this.coreReader.Value);
					validatingReaderNodeData.SetLineInfo(this.lineInfo);
					validatingReaderNodeData.Depth = this.coreReader.Depth;
					break;
				case XmlNodeType.EndElement:
					validatingReaderNodeData = this.AddContent(this.coreReader.NodeType);
					validatingReaderNodeData.SetItemData(this.coreReader.LocalName, this.coreReader.Prefix, this.coreReader.NamespaceURI, this.coreReader.Depth);
					validatingReaderNodeData.SetLineInfo(this.lineInfo);
					break;
				}
				this.cachedNode = validatingReaderNodeData;
				return true;
			}
			this.cacheState = XsdCachingReader.CachingReaderState.ReaderClosed;
			return false;
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0002FB30 File Offset: 0x0002EB30
		internal ValidatingReaderNodeData RecordTextNode(string textValue, string originalStringValue, int depth, int lineNo, int linePos)
		{
			ValidatingReaderNodeData validatingReaderNodeData = this.AddContent(XmlNodeType.Text);
			validatingReaderNodeData.SetItemData(textValue, originalStringValue);
			validatingReaderNodeData.SetLineInfo(lineNo, linePos);
			validatingReaderNodeData.Depth = depth;
			return validatingReaderNodeData;
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0002FB60 File Offset: 0x0002EB60
		internal void SwitchTextNodeAndEndElement(string textValue, string originalStringValue)
		{
			ValidatingReaderNodeData validatingReaderNodeData = this.RecordTextNode(textValue, originalStringValue, this.coreReader.Depth + 1, 0, 0);
			int num = this.contentIndex - 2;
			ValidatingReaderNodeData validatingReaderNodeData2 = this.contentEvents[num];
			this.contentEvents[num] = validatingReaderNodeData;
			this.contentEvents[this.contentIndex - 1] = validatingReaderNodeData2;
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0002FBB0 File Offset: 0x0002EBB0
		internal void RecordEndElementNode()
		{
			ValidatingReaderNodeData validatingReaderNodeData = this.AddContent(XmlNodeType.EndElement);
			validatingReaderNodeData.SetItemData(this.coreReader.LocalName, this.coreReader.Prefix, this.coreReader.NamespaceURI, this.coreReader.Depth);
			validatingReaderNodeData.SetLineInfo(this.coreReader as IXmlLineInfo);
			if (this.coreReader.IsEmptyElement)
			{
				this.readAhead = true;
			}
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0002FC20 File Offset: 0x0002EC20
		internal string ReadOriginalContentAsString()
		{
			this.returnOriginalStringValues = true;
			string result = base.InternalReadContentAsString();
			this.returnOriginalStringValues = false;
			return result;
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x0002FC43 File Offset: 0x0002EC43
		public override bool EOF
		{
			get
			{
				return this.cacheState == XsdCachingReader.CachingReaderState.ReaderClosed && this.coreReader.EOF;
			}
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0002FC5B File Offset: 0x0002EC5B
		public override void Close()
		{
			this.coreReader.Close();
			this.cacheState = XsdCachingReader.CachingReaderState.ReaderClosed;
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x0002FC6F File Offset: 0x0002EC6F
		public override ReadState ReadState
		{
			get
			{
				return this.coreReader.ReadState;
			}
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0002FC7C File Offset: 0x0002EC7C
		public override void Skip()
		{
			switch (this.cachedNode.NodeType)
			{
			case XmlNodeType.Element:
				break;
			case XmlNodeType.Attribute:
				this.MoveToElement();
				break;
			default:
				this.Read();
				return;
			}
			if (this.coreReader.NodeType != XmlNodeType.EndElement && !this.readAhead)
			{
				int num = this.coreReader.Depth - 1;
				while (this.coreReader.Read() && this.coreReader.Depth > num)
				{
				}
			}
			this.coreReader.Read();
			this.cacheState = XsdCachingReader.CachingReaderState.ReaderClosed;
			this.cacheHandler(this);
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x0002FD17 File Offset: 0x0002ED17
		public override XmlNameTable NameTable
		{
			get
			{
				return this.coreReaderNameTable;
			}
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0002FD1F File Offset: 0x0002ED1F
		public override string LookupNamespace(string prefix)
		{
			return this.coreReader.LookupNamespace(prefix);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0002FD2D File Offset: 0x0002ED2D
		public override void ResolveEntity()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0002FD34 File Offset: 0x0002ED34
		public override bool ReadAttributeValue()
		{
			if (this.cachedNode.NodeType != XmlNodeType.Attribute)
			{
				return false;
			}
			this.cachedNode = this.CreateDummyTextNode(this.cachedNode.RawValue, this.cachedNode.Depth + 1);
			return true;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0002FD6B File Offset: 0x0002ED6B
		bool IXmlLineInfo.HasLineInfo()
		{
			return true;
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x0002FD6E File Offset: 0x0002ED6E
		int IXmlLineInfo.LineNumber
		{
			get
			{
				return this.cachedNode.LineNumber;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x0002FD7B File Offset: 0x0002ED7B
		int IXmlLineInfo.LinePosition
		{
			get
			{
				return this.cachedNode.LinePosition;
			}
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0002FD88 File Offset: 0x0002ED88
		internal void SetToReplayMode()
		{
			this.cacheState = XsdCachingReader.CachingReaderState.Replay;
			this.currentContentIndex = 0;
			this.currentAttrIndex = -1;
			this.Read();
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0002FDA6 File Offset: 0x0002EDA6
		internal XmlReader GetCoreReader()
		{
			return this.coreReader;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0002FDAE File Offset: 0x0002EDAE
		internal IXmlLineInfo GetLineInfo()
		{
			return this.lineInfo;
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0002FDB6 File Offset: 0x0002EDB6
		private void ClearAttributesInfo()
		{
			this.attributeCount = 0;
			this.currentAttrIndex = -1;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0002FDC8 File Offset: 0x0002EDC8
		private ValidatingReaderNodeData AddAttribute(int attIndex)
		{
			ValidatingReaderNodeData validatingReaderNodeData = this.attributeEvents[attIndex];
			if (validatingReaderNodeData != null)
			{
				validatingReaderNodeData.Clear(XmlNodeType.Attribute);
				return validatingReaderNodeData;
			}
			if (attIndex >= this.attributeEvents.Length - 1)
			{
				ValidatingReaderNodeData[] destinationArray = new ValidatingReaderNodeData[this.attributeEvents.Length * 2];
				Array.Copy(this.attributeEvents, 0, destinationArray, 0, this.attributeEvents.Length);
				this.attributeEvents = destinationArray;
			}
			validatingReaderNodeData = this.attributeEvents[attIndex];
			if (validatingReaderNodeData == null)
			{
				validatingReaderNodeData = new ValidatingReaderNodeData(XmlNodeType.Attribute);
				this.attributeEvents[attIndex] = validatingReaderNodeData;
			}
			return validatingReaderNodeData;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0002FE44 File Offset: 0x0002EE44
		private ValidatingReaderNodeData AddContent(XmlNodeType nodeType)
		{
			ValidatingReaderNodeData validatingReaderNodeData = this.contentEvents[this.contentIndex];
			if (validatingReaderNodeData != null)
			{
				validatingReaderNodeData.Clear(nodeType);
				this.contentIndex++;
				return validatingReaderNodeData;
			}
			if (this.contentIndex >= this.contentEvents.Length - 1)
			{
				ValidatingReaderNodeData[] destinationArray = new ValidatingReaderNodeData[this.contentEvents.Length * 2];
				Array.Copy(this.contentEvents, 0, destinationArray, 0, this.contentEvents.Length);
				this.contentEvents = destinationArray;
			}
			validatingReaderNodeData = this.contentEvents[this.contentIndex];
			if (validatingReaderNodeData == null)
			{
				validatingReaderNodeData = new ValidatingReaderNodeData(nodeType);
				this.contentEvents[this.contentIndex] = validatingReaderNodeData;
			}
			this.contentIndex++;
			return validatingReaderNodeData;
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0002FEF0 File Offset: 0x0002EEF0
		private void RecordAttributes()
		{
			this.attributeCount = this.coreReader.AttributeCount;
			if (this.coreReader.MoveToFirstAttribute())
			{
				int num = 0;
				do
				{
					ValidatingReaderNodeData validatingReaderNodeData = this.AddAttribute(num);
					validatingReaderNodeData.SetItemData(this.coreReader.LocalName, this.coreReader.Prefix, this.coreReader.NamespaceURI, this.coreReader.Depth);
					validatingReaderNodeData.SetLineInfo(this.lineInfo);
					validatingReaderNodeData.RawValue = this.coreReader.Value;
					num++;
				}
				while (this.coreReader.MoveToNextAttribute());
				this.coreReader.MoveToElement();
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0002FF94 File Offset: 0x0002EF94
		private int GetAttributeIndexWithoutPrefix(string name)
		{
			name = this.coreReaderNameTable.Get(name);
			if (name == null)
			{
				return -1;
			}
			for (int i = 0; i < this.attributeCount; i++)
			{
				ValidatingReaderNodeData validatingReaderNodeData = this.attributeEvents[i];
				if (Ref.Equal(validatingReaderNodeData.LocalName, name) && validatingReaderNodeData.Prefix.Length == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0002FFEC File Offset: 0x0002EFEC
		private int GetAttributeIndexWithPrefix(string name)
		{
			name = this.coreReaderNameTable.Get(name);
			if (name == null)
			{
				return -1;
			}
			for (int i = 0; i < this.attributeCount; i++)
			{
				ValidatingReaderNodeData validatingReaderNodeData = this.attributeEvents[i];
				if (Ref.Equal(validatingReaderNodeData.GetAtomizedNameWPrefix(this.coreReaderNameTable), name))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0003003D File Offset: 0x0002F03D
		private ValidatingReaderNodeData CreateDummyTextNode(string attributeValue, int depth)
		{
			if (this.textNode == null)
			{
				this.textNode = new ValidatingReaderNodeData(XmlNodeType.Text);
			}
			this.textNode.Depth = depth;
			this.textNode.RawValue = attributeValue;
			return this.textNode;
		}

		// Token: 0x0400088A RID: 2186
		private const int InitialAttributeCount = 8;

		// Token: 0x0400088B RID: 2187
		private const int InitialContentCount = 4;

		// Token: 0x0400088C RID: 2188
		private XmlReader coreReader;

		// Token: 0x0400088D RID: 2189
		private XmlNameTable coreReaderNameTable;

		// Token: 0x0400088E RID: 2190
		private ValidatingReaderNodeData[] contentEvents;

		// Token: 0x0400088F RID: 2191
		private ValidatingReaderNodeData[] attributeEvents;

		// Token: 0x04000890 RID: 2192
		private ValidatingReaderNodeData cachedNode;

		// Token: 0x04000891 RID: 2193
		private XsdCachingReader.CachingReaderState cacheState;

		// Token: 0x04000892 RID: 2194
		private int contentIndex;

		// Token: 0x04000893 RID: 2195
		private int attributeCount;

		// Token: 0x04000894 RID: 2196
		private bool returnOriginalStringValues;

		// Token: 0x04000895 RID: 2197
		private CachingEventHandler cacheHandler;

		// Token: 0x04000896 RID: 2198
		private int currentAttrIndex;

		// Token: 0x04000897 RID: 2199
		private int currentContentIndex;

		// Token: 0x04000898 RID: 2200
		private bool readAhead;

		// Token: 0x04000899 RID: 2201
		private IXmlLineInfo lineInfo;

		// Token: 0x0400089A RID: 2202
		private ValidatingReaderNodeData textNode;

		// Token: 0x020000B3 RID: 179
		private enum CachingReaderState
		{
			// Token: 0x0400089C RID: 2204
			None,
			// Token: 0x0400089D RID: 2205
			Init,
			// Token: 0x0400089E RID: 2206
			Record,
			// Token: 0x0400089F RID: 2207
			Replay,
			// Token: 0x040008A0 RID: 2208
			ReaderClosed,
			// Token: 0x040008A1 RID: 2209
			Error
		}
	}
}
