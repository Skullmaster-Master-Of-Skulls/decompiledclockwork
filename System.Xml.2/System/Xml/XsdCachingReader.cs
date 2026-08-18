using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x020000ED RID: 237
	internal class XsdCachingReader : XmlReader, IXmlLineInfo
	{
		// Token: 0x0600103D RID: 4157 RVA: 0x00044F2B File Offset: 0x0004312B
		internal XsdCachingReader(XmlReader reader, IXmlLineInfo lineInfo, CachingEventHandler handlerMethod)
		{
			this.coreReader = reader;
			this.lineInfo = lineInfo;
			this.cacheHandler = handlerMethod;
			this.attributeEvents = new ValidatingReaderNodeData[8];
			this.contentEvents = new ValidatingReaderNodeData[4];
			this.Init();
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x00044F68 File Offset: 0x00043168
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

		// Token: 0x0600103F RID: 4159 RVA: 0x0004501B File Offset: 0x0004321B
		internal void Reset(XmlReader reader)
		{
			this.coreReader = reader;
			this.Init();
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x0004502A File Offset: 0x0004322A
		public override XmlReaderSettings Settings
		{
			get
			{
				return this.coreReader.Settings;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06001041 RID: 4161 RVA: 0x00045037 File Offset: 0x00043237
		public override XmlNodeType NodeType
		{
			get
			{
				return this.cachedNode.NodeType;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x00045044 File Offset: 0x00043244
		public override string Name
		{
			get
			{
				return this.cachedNode.GetAtomizedNameWPrefix(this.coreReaderNameTable);
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x00045057 File Offset: 0x00043257
		public override string LocalName
		{
			get
			{
				return this.cachedNode.LocalName;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x00045064 File Offset: 0x00043264
		public override string NamespaceURI
		{
			get
			{
				return this.cachedNode.Namespace;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001045 RID: 4165 RVA: 0x00045071 File Offset: 0x00043271
		public override string Prefix
		{
			get
			{
				return this.cachedNode.Prefix;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x0004507E File Offset: 0x0004327E
		public override bool HasValue
		{
			get
			{
				return XmlReader.HasValueInternal(this.cachedNode.NodeType);
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06001047 RID: 4167 RVA: 0x00045090 File Offset: 0x00043290
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

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x000450B1 File Offset: 0x000432B1
		public override int Depth
		{
			get
			{
				return this.cachedNode.Depth;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001049 RID: 4169 RVA: 0x000450BE File Offset: 0x000432BE
		public override string BaseURI
		{
			get
			{
				return this.coreReader.BaseURI;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x000450CB File Offset: 0x000432CB
		public override bool IsEmptyElement
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x0600104B RID: 4171 RVA: 0x000450CE File Offset: 0x000432CE
		public override bool IsDefault
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x000450D1 File Offset: 0x000432D1
		public override char QuoteChar
		{
			get
			{
				return this.coreReader.QuoteChar;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x000450DE File Offset: 0x000432DE
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.coreReader.XmlSpace;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x000450EB File Offset: 0x000432EB
		public override string XmlLang
		{
			get
			{
				return this.coreReader.XmlLang;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x000450F8 File Offset: 0x000432F8
		public override int AttributeCount
		{
			get
			{
				return this.attributeCount;
			}
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x00045100 File Offset: 0x00043300
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

		// Token: 0x06001051 RID: 4177 RVA: 0x00045140 File Offset: 0x00043340
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

		// Token: 0x06001052 RID: 4178 RVA: 0x000451B1 File Offset: 0x000433B1
		public override string GetAttribute(int i)
		{
			if (i < 0 || i >= this.attributeCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			return this.attributeEvents[i].RawValue;
		}

		// Token: 0x17000319 RID: 793
		public override string this[int i]
		{
			get
			{
				return this.GetAttribute(i);
			}
		}

		// Token: 0x1700031A RID: 794
		public override string this[string name]
		{
			get
			{
				return this.GetAttribute(name);
			}
		}

		// Token: 0x1700031B RID: 795
		public override string this[string name, string namespaceURI]
		{
			get
			{
				return this.GetAttribute(name, namespaceURI);
			}
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x000451F4 File Offset: 0x000433F4
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

		// Token: 0x06001057 RID: 4183 RVA: 0x0004523C File Offset: 0x0004343C
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

		// Token: 0x06001058 RID: 4184 RVA: 0x000452BD File Offset: 0x000434BD
		public override void MoveToAttribute(int i)
		{
			if (i < 0 || i >= this.attributeCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			this.currentAttrIndex = i;
			this.cachedNode = this.attributeEvents[i];
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x000452EC File Offset: 0x000434EC
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

		// Token: 0x0600105A RID: 4186 RVA: 0x00045310 File Offset: 0x00043510
		public override bool MoveToNextAttribute()
		{
			if (this.currentAttrIndex + 1 < this.attributeCount)
			{
				ValidatingReaderNodeData[] array = this.attributeEvents;
				int num = this.currentAttrIndex + 1;
				this.currentAttrIndex = num;
				this.cachedNode = array[num];
				return true;
			}
			return false;
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0004534E File Offset: 0x0004354E
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

		// Token: 0x0600105C RID: 4188 RVA: 0x00045380 File Offset: 0x00043580
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

		// Token: 0x0600105D RID: 4189 RVA: 0x00045540 File Offset: 0x00043740
		internal ValidatingReaderNodeData RecordTextNode(string textValue, string originalStringValue, int depth, int lineNo, int linePos)
		{
			ValidatingReaderNodeData validatingReaderNodeData = this.AddContent(XmlNodeType.Text);
			validatingReaderNodeData.SetItemData(textValue, originalStringValue);
			validatingReaderNodeData.SetLineInfo(lineNo, linePos);
			validatingReaderNodeData.Depth = depth;
			return validatingReaderNodeData;
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x00045570 File Offset: 0x00043770
		internal void SwitchTextNodeAndEndElement(string textValue, string originalStringValue)
		{
			ValidatingReaderNodeData validatingReaderNodeData = this.RecordTextNode(textValue, originalStringValue, this.coreReader.Depth + 1, 0, 0);
			int num = this.contentIndex - 2;
			ValidatingReaderNodeData validatingReaderNodeData2 = this.contentEvents[num];
			this.contentEvents[num] = validatingReaderNodeData;
			this.contentEvents[this.contentIndex - 1] = validatingReaderNodeData2;
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x000455C0 File Offset: 0x000437C0
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

		// Token: 0x06001060 RID: 4192 RVA: 0x00045630 File Offset: 0x00043830
		internal string ReadOriginalContentAsString()
		{
			this.returnOriginalStringValues = true;
			string result = base.InternalReadContentAsString();
			this.returnOriginalStringValues = false;
			return result;
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x00045653 File Offset: 0x00043853
		public override bool EOF
		{
			get
			{
				return this.cacheState == XsdCachingReader.CachingReaderState.ReaderClosed && this.coreReader.EOF;
			}
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0004566B File Offset: 0x0004386B
		public override void Close()
		{
			this.coreReader.Close();
			this.cacheState = XsdCachingReader.CachingReaderState.ReaderClosed;
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x0004567F File Offset: 0x0004387F
		public override ReadState ReadState
		{
			get
			{
				return this.coreReader.ReadState;
			}
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x0004568C File Offset: 0x0004388C
		public override void Skip()
		{
			XmlNodeType nodeType = this.cachedNode.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType != XmlNodeType.Attribute)
				{
					this.Read();
					return;
				}
				this.MoveToElement();
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

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x0004571F File Offset: 0x0004391F
		public override XmlNameTable NameTable
		{
			get
			{
				return this.coreReaderNameTable;
			}
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x00045727 File Offset: 0x00043927
		public override string LookupNamespace(string prefix)
		{
			return this.coreReader.LookupNamespace(prefix);
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x00045735 File Offset: 0x00043935
		public override void ResolveEntity()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0004573C File Offset: 0x0004393C
		public override bool ReadAttributeValue()
		{
			if (this.cachedNode.NodeType != XmlNodeType.Attribute)
			{
				return false;
			}
			this.cachedNode = this.CreateDummyTextNode(this.cachedNode.RawValue, this.cachedNode.Depth + 1);
			return true;
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x00045773 File Offset: 0x00043973
		bool IXmlLineInfo.HasLineInfo()
		{
			return true;
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x0600106A RID: 4202 RVA: 0x00045776 File Offset: 0x00043976
		int IXmlLineInfo.LineNumber
		{
			get
			{
				return this.cachedNode.LineNumber;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x00045783 File Offset: 0x00043983
		int IXmlLineInfo.LinePosition
		{
			get
			{
				return this.cachedNode.LinePosition;
			}
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x00045790 File Offset: 0x00043990
		internal void SetToReplayMode()
		{
			this.cacheState = XsdCachingReader.CachingReaderState.Replay;
			this.currentContentIndex = 0;
			this.currentAttrIndex = -1;
			this.Read();
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x000457AE File Offset: 0x000439AE
		internal XmlReader GetCoreReader()
		{
			return this.coreReader;
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x000457B6 File Offset: 0x000439B6
		internal IXmlLineInfo GetLineInfo()
		{
			return this.lineInfo;
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x000457BE File Offset: 0x000439BE
		private void ClearAttributesInfo()
		{
			this.attributeCount = 0;
			this.currentAttrIndex = -1;
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x000457D0 File Offset: 0x000439D0
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

		// Token: 0x06001071 RID: 4209 RVA: 0x0004584C File Offset: 0x00043A4C
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

		// Token: 0x06001072 RID: 4210 RVA: 0x000458F8 File Offset: 0x00043AF8
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

		// Token: 0x06001073 RID: 4211 RVA: 0x0004599C File Offset: 0x00043B9C
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

		// Token: 0x06001074 RID: 4212 RVA: 0x000459F4 File Offset: 0x00043BF4
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

		// Token: 0x06001075 RID: 4213 RVA: 0x00045A45 File Offset: 0x00043C45
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

		// Token: 0x06001076 RID: 4214 RVA: 0x00045A79 File Offset: 0x00043C79
		public override Task<string> GetValueAsync()
		{
			if (this.returnOriginalStringValues)
			{
				return Task.FromResult<string>(this.cachedNode.OriginalStringValue);
			}
			return Task.FromResult<string>(this.cachedNode.RawValue);
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x00045AA4 File Offset: 0x00043CA4
		public override Task<bool> ReadAsync()
		{
			XsdCachingReader.<ReadAsync>d__100 <ReadAsync>d__;
			<ReadAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ReadAsync>d__.<>4__this = this;
			<ReadAsync>d__.<>1__state = -1;
			<ReadAsync>d__.<>t__builder.Start<XsdCachingReader.<ReadAsync>d__100>(ref <ReadAsync>d__);
			return <ReadAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00045AE8 File Offset: 0x00043CE8
		public override Task SkipAsync()
		{
			XsdCachingReader.<SkipAsync>d__101 <SkipAsync>d__;
			<SkipAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SkipAsync>d__.<>4__this = this;
			<SkipAsync>d__.<>1__state = -1;
			<SkipAsync>d__.<>t__builder.Start<XsdCachingReader.<SkipAsync>d__101>(ref <SkipAsync>d__);
			return <SkipAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x00045B2B File Offset: 0x00043D2B
		internal Task SetToReplayModeAsync()
		{
			this.cacheState = XsdCachingReader.CachingReaderState.Replay;
			this.currentContentIndex = 0;
			this.currentAttrIndex = -1;
			return this.ReadAsync();
		}

		// Token: 0x0400049B RID: 1179
		private XmlReader coreReader;

		// Token: 0x0400049C RID: 1180
		private XmlNameTable coreReaderNameTable;

		// Token: 0x0400049D RID: 1181
		private ValidatingReaderNodeData[] contentEvents;

		// Token: 0x0400049E RID: 1182
		private ValidatingReaderNodeData[] attributeEvents;

		// Token: 0x0400049F RID: 1183
		private ValidatingReaderNodeData cachedNode;

		// Token: 0x040004A0 RID: 1184
		private XsdCachingReader.CachingReaderState cacheState;

		// Token: 0x040004A1 RID: 1185
		private int contentIndex;

		// Token: 0x040004A2 RID: 1186
		private int attributeCount;

		// Token: 0x040004A3 RID: 1187
		private bool returnOriginalStringValues;

		// Token: 0x040004A4 RID: 1188
		private CachingEventHandler cacheHandler;

		// Token: 0x040004A5 RID: 1189
		private int currentAttrIndex;

		// Token: 0x040004A6 RID: 1190
		private int currentContentIndex;

		// Token: 0x040004A7 RID: 1191
		private bool readAhead;

		// Token: 0x040004A8 RID: 1192
		private IXmlLineInfo lineInfo;

		// Token: 0x040004A9 RID: 1193
		private ValidatingReaderNodeData textNode;

		// Token: 0x040004AA RID: 1194
		private const int InitialAttributeCount = 8;

		// Token: 0x040004AB RID: 1195
		private const int InitialContentCount = 4;

		// Token: 0x02000434 RID: 1076
		private enum CachingReaderState
		{
			// Token: 0x04001C0D RID: 7181
			None,
			// Token: 0x04001C0E RID: 7182
			Init,
			// Token: 0x04001C0F RID: 7183
			Record,
			// Token: 0x04001C10 RID: 7184
			Replay,
			// Token: 0x04001C11 RID: 7185
			ReaderClosed,
			// Token: 0x04001C12 RID: 7186
			Error
		}
	}
}
